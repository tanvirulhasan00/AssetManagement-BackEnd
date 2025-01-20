using System.Net;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/house")]
    [ApiVersion("1.0")]
    public class HouseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public HouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllHouse(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<House>()
                {
                    Expression = null,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var houseData = await _unitOfWork.Houses.GetAllAsync(genericReq);
                if (houseData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = houseData;
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<ApiResponse> GetHouse(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<House>()
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var houseData = await _unitOfWork.Houses.GetAsync(genericReq);
                if (houseData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = houseData;
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ApiResponse> CreateHouse([FromBody] HouseCreateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                House houseToCreate = new()
                {
                    Name = request.Name,
                    AreaId = request.AreaId,
                    TotalFloor = request.TotalFloor,
                    TotalFlat = request.TotalFlat,
                    Road = request.Road,
                    PostCode = request.PostCode,
                    Active = request.Active,
                };
                _unitOfWork.Houses?.AddAsync(houseToCreate);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while creating house";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Successful";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ApiResponse> UpdateHouse([FromBody] HouseUpdateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<House>()
                {
                    Expression = x => x.Id == request.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var houseData = await _unitOfWork.Houses.GetAsync(genericReq);
                if (houseData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                houseData.Name = (request.Name == null || request.Name == "") ? houseData.Name : request.Name;
                houseData.AreaId = request.AreaId == 0 ? houseData.AreaId : request.AreaId;
                houseData.TotalFloor = request.TotalFloor == 0 ? houseData.TotalFloor : request.TotalFloor;
                houseData.TotalFlat = request.TotalFlat == 0 ? houseData.TotalFlat : request.TotalFlat;
                houseData.Road = (request.Road == null || request.Road == "") ? houseData.Road : request.Road;
                houseData.PostCode = request.PostCode == 0 ? houseData.PostCode : request.PostCode;
                houseData.Active = request.Active;

                _unitOfWork.Houses?.Update(houseData);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while updating house";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                return response;

            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteHouse(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<House>
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var houseData = await _unitOfWork.Houses.GetAsync(genericReq);
                if (houseData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - house data not found with the id {Id}";
                    return response;
                }

                _unitOfWork.Houses.Remove(houseData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while deleting house";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
                return response;

            }
        }
    }
}