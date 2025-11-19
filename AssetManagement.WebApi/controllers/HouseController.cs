using System.Net;
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
        public async Task<ApiResponse> GetAllHouse(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<House>()
                {
                    Expression = null,
                    IncludeProperties = "Area",
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
        public async Task<ApiResponse> CreateHouse([FromBody] HouseCreateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                House houseToCreate = new()
                {
                    Name = request.Name,
                    AreaId = int.Parse(request.AreaId),
                    TotalFloor = int.Parse(request.TotalFloor),
                    TotalFlat = int.Parse(request.TotalFlat),
                    Road = request.Road,
                    PostCode = int.Parse(request.PostCode),
                    Active = int.Parse(request.Active),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
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
                response.Message = "House created successfully";
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
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
                houseData.AreaId = int.Parse(request.AreaId) == 0 ? houseData.AreaId : int.Parse(request.AreaId);
                houseData.TotalFloor = int.Parse(request.TotalFloor) == 0 ? houseData.TotalFloor : int.Parse(request.TotalFloor);
                houseData.TotalFlat = int.Parse(request.TotalFlat) == 0 ? houseData.TotalFlat : int.Parse(request.TotalFlat);
                houseData.Road = (request.Road == null || request.Road == "") ? houseData.Road : request.Road;
                houseData.PostCode = int.Parse(request.PostCode) == 0 ? houseData.PostCode : int.Parse(request.PostCode);
                houseData.Active = int.Parse(request.Active);
                houseData.UpdatedDate = DateTime.UtcNow;

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
                response.Message = "House updated successfully";
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
        [Authorize(Roles = $"{RolesVariable.ADMIN},{RolesVariable.MANAGER}")]
        public async Task<ApiResponse> DeleteHouse(List<string> Ids, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (Ids == null || Ids.Count == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - No IDs provided";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<House>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var houseToDelete = await _unitOfWork.Houses.GetAllAsync(genericReq);
                if (houseToDelete == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - No house found with the provided IDs: {Ids}";
                    return response;
                }

                _unitOfWork.Houses.RemoveRange(houseToDelete);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting house";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{houseToDelete.Count} House(s) deleted Successfully";
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