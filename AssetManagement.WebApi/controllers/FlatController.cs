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
    [Route("api/v{version:apiVersion}/flat")]
    [ApiVersion("1.0")]
    public class FlatController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FlatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<ApiResponse> GetAllFlat(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Flat>()
                {
                    Expression = null,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var flatData = await _unitOfWork.Flats.GetAllAsync(genericReq);
                if (flatData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = flatData;
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
        public async Task<ApiResponse> GetFlat(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Flat>()
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var flatData = await _unitOfWork.Flats.GetAsync(genericReq);
                if (flatData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = flatData;
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
        public async Task<ApiResponse> CreateFlat([FromBody] FlatCreateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                Flat flatToCreate = new()
                {
                    Name = request.Name,
                    FloorNo = request.FloorNo,
                    TotalRoom = request.TotalRoom,
                    Price = request.Price,
                    CategoryId = request.CategoryId,
                    HouseId = request.HouseId,
                    Active = request.Active,
                };
                _unitOfWork.Flats?.AddAsync(flatToCreate);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while creating flat";
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
        public async Task<ApiResponse> UpdateFlat([FromBody] FlatUpdateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Flat>()
                {
                    Expression = x => x.Id == request.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var flatData = await _unitOfWork.Flats.GetAsync(genericReq);
                if (flatData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }
                flatData.Name = (request.Name == null || request.Name == "") ? flatData.Name : request.Name;
                flatData.FloorNo = request.FloorNo == 0 ? flatData.FloorNo : request.FloorNo;
                flatData.TotalRoom = request.TotalRoom == 0 ? flatData.TotalRoom : request.TotalRoom;
                flatData.Price = request.Price == 0 ? flatData.Price : request.Price;
                flatData.CategoryId = request.CategoryId == 0 ? flatData.CategoryId : request.CategoryId;
                flatData.HouseId = request.HouseId == 0 ? flatData.HouseId : request.HouseId;
                flatData.Active = request.Active;

                _unitOfWork.Flats?.Update(flatData);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while updating flat";
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
        public async Task<ApiResponse> DeleteFLat(int Id, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Flat>
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var flatData = await _unitOfWork.Flats.GetAsync(genericReq);
                if (flatData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - flat data not found with the id {Id}";
                    return response;
                }

                _unitOfWork.Flats.Remove(flatData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while deleting flat";
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