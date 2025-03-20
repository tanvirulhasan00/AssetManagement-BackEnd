using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Repositories.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/payment")]
    [ApiVersion("1.0")]
    public class PaymentController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllPayment(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var payment = await _unitOfWork.Payment.GetAllAsync(new GenericRequest<Payment>
                {
                    Expression = null,
                    IncludeProperties = "",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (payment == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - payment data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = payment;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.GetType().Name;
                return response;
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ApiResponse> GetPayment(long id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - payment id not found";
                return response;
            }
            try
            {
                var payment = await _unitOfWork.Payment.GetAsync(new GenericRequest<Payment>
                {
                    Expression = x => x.Id == id,
                    IncludeProperties = "Assign",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (payment == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - payment data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = payment;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.GetType().Name;
                return response;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ApiResponse> CreatePayment([FromBody] PaymentCreateReqDto paymentDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var transId = GenerateInvOrTransNumber("TRANS");
            var invoiceId = GenerateInvOrTransNumber("INV");
            try
            {
                if (paymentDto.ReferenceNo == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Reference number is not provided";
                    return response;
                }
                Payment paymentToCreate = new()
                {
                    TransactionId = (paymentDto.TransactionId != null && paymentDto.TransactionId != "") ? paymentDto.TransactionId : transId,
                    InvoiceId = invoiceId,
                    PaymentMethod = paymentDto.PaymentMethod,
                    PaymentType = paymentDto.PaymentType,
                    PaymentAmount = int.Parse(paymentDto.PaymentAmount),
                    FlatUtilities = int.Parse(paymentDto.FlatUtilities),
                    PaymentDue = int.Parse(paymentDto.PaymentDue),
                    PaymentAdvance = int.Parse(paymentDto.PaymentAdvance),
                    PaymentMonth = paymentDto.PaymentMonth,
                    PaymentYear = paymentDto.PaymentYear,
                    PaymentStatus = paymentDto.PaymentStatus,
                    PaymentDate = DateTime.UtcNow,
                    ReferenceNo = paymentDto.ReferenceNo,
                    AssignId = paymentDto.AssignId,
                };
                if ((paymentDto.PaymentDue != null && paymentDto.PaymentDue != "") || (paymentDto.PaymentAdvance != null && paymentDto.PaymentAdvance != ""))
                {
                    var assignData = await _unitOfWork.Assign.GetAsync(new GenericRequest<Assign>
                    {
                        Expression = x => x.ReferenceNo == paymentDto.ReferenceNo,
                        NoTracking = true,
                        IncludeProperties = null,
                        CancellationToken = cancellationToken
                    });
                    var now = DateTime.UtcNow;
                    var year = now.Year;

                    assignData.DueRent += int.Parse(paymentDto.PaymentDue);
                    assignData.AdvanceRent += int.Parse(paymentDto.PaymentAdvance);
                    if (paymentDto.PaymentType == "duerent")
                    {
                        assignData.DueRent -= int.Parse(paymentDto.PaymentAmount);
                    }
                    _unitOfWork.Assign.Update(assignData);

                    // dynamic paymentStatusToUpdate = new ExpandoObject();
                    var paymentStatusToUpdate = await _unitOfWork.MonthlyPaymentStatus.GetAsync(new GenericRequest<MonthlyPaymentStatus>
                    {
                        Expression = x => x.AssignId == assignData.Id && x.Year == year.ToString(),
                        NoTracking = true,
                        IncludeProperties = null,
                        CancellationToken = cancellationToken
                    });




                    var propertyName = paymentDto.PaymentMonth;
                    var property = paymentStatusToUpdate.GetType().GetProperty(propertyName);

                    if (property != null && property.CanWrite)
                    {
                        property.SetValue(paymentStatusToUpdate, paymentDto.PaymentStatus);
                    }
                    else
                    {
                        Console.WriteLine($"Property '{propertyName}' not found or is not writable.");
                    }

                    // Now update it in the database
                    _unitOfWork.MonthlyPaymentStatus.Update(paymentStatusToUpdate);


                    await _unitOfWork.Save();
                }

                await _unitOfWork.Payment.AddAsync(paymentToCreate);
                int res = await _unitOfWork.Save();
                if (res > 0)
                {
                    var user = await _unitOfWork.Users.GetAsync(new GenericRequest<ApplicationUser>
                    {
                        Expression = x => x.Id == paymentDto.UserId,
                        IncludeProperties = null,
                        NoTracking = true,
                        CancellationToken = cancellationToken,
                    });
                    History historyToCreate = new()
                    {
                        ActionName = paymentDto.PaymentType,
                        ActionById = user.Id,
                        ActionByName = user.Name,
                        ActionDate = DateTime.UtcNow,
                        ActionDetails = (paymentDto.TransactionId != null && paymentDto.TransactionId != "") ? paymentDto.TransactionId : transId,
                    };
                    await _unitOfWork.Histories.AddAsync(historyToCreate);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Payment successful";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while creating payment";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }
        private static string GenerateInvOrTransNumber(string name)
        {
            return $"{name}-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()}";
        }
    }
}