using System;
using System.Collections.Generic;
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
                    IncludeProperties = "Renter",
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
                    IncludeProperties = "Renter",
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
            try
            {
                Payment paymentToCreate = new()
                {
                    TransactionId = (paymentDto.TransactionId != null && paymentDto.TransactionId != "") ? paymentDto.TransactionId : GenerateInvOrTransNumber("TRANS"),
                    InvoiceId = GenerateInvOrTransNumber("INV"),
                    PaymentMethod = paymentDto.PaymentMethod,
                    PaymentAmount = int.Parse(paymentDto.PaymentAmount),
                    PaymentDueAmount = int.Parse(paymentDto.PaymentDueAmount),
                    PaymentStatus = paymentDto.PaymentStatus,
                    PaymentDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    RenterId = int.Parse(paymentDto.RenterId),
                };
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
                        ActionName = "Create Payment",
                        ActionBy = user.Id,
                        ActionByName = user.Name,
                        ActionDate = DateTime.UtcNow,
                    };
                    await _unitOfWork.Histories.AddAsync(historyToCreate);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Payment created successfully";
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