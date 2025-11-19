using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Repositories.IRepos;
using AssetManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/monthly-payment-status")]
    public class MonthlyPaymentStatusController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
        public async Task<ApiResponse> GetAllMonthlyPaymentStatus(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var payment = await _unitOfWork.MonthlyPaymentStatus.GetAllAsync(new GenericRequest<MonthlyPaymentStatus>
                {
                    Expression = null,
                    IncludeProperties = "Assign",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (payment == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - Monthly payment status not found";
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
        public async Task<ApiResponse> GetMonthlyPaymentStatus(long Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - Monthly payment status id not provided";
                return response;
            }
            try
            {
                var payment = await _unitOfWork.MonthlyPaymentStatus.GetAsync(new GenericRequest<MonthlyPaymentStatus>
                {
                    Expression = x => x.AssignId == Id,
                    IncludeProperties = "Assign",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                });
                if (payment == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - Monthly payment status not found";
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
        public async Task<ApiResponse> CreateMonthlyPaymentStatus(MonthlyPaymentStatusReqDto req)
        {
            var response = new ApiResponse();
            if (int.Parse(req.AssignId) == 0 || req.Year == "" || req.Year == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Assign id or year is not provided";
                return response;
            }
            try
            {
                MonthlyPaymentStatus monthlyPaymentStatusToCreate = new()
                {
                    AssignId = int.Parse(req.AssignId),
                    Year = req.Year,

                };

                await _unitOfWork.MonthlyPaymentStatus.AddAsync(monthlyPaymentStatusToCreate);
                int i = await _unitOfWork.Save();

                if (i > 0)
                {
                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Created monthly payment status successful";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while creating payment status";
                    return response;
                }

            }
            catch (Exception ex) { }
            return response;
        }
    }
}