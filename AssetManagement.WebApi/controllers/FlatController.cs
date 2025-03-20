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
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllFlat(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Flat>()
                {
                    Expression = null,
                    IncludeProperties = "Category,House",
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
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetFlat(long Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Flat>()
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = "Category,House",
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
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> CreateFlat([FromBody] FlatCreateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                Flat flatToCreate = new()
                {
                    Name = request.Name,
                    FloorNo = int.Parse(request.FloorNo),
                    TotalRoom = int.Parse(request.TotalRoom),
                    AssignedId = string.Empty,
                    FlatAdvance = int.Parse(request.FlatAdvance),
                    CategoryId = int.Parse(request.CategoryId),
                    HouseId = int.Parse(request.HouseId),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    Active = int.Parse(request.Active),
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
                response.Message = "Flat created successfully";
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
        [Authorize(Roles = "admin")]
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
                // flatData.FloorNo = int.Parse(request.FloorNo) == 0 ? flatData.FloorNo : int.Parse(request.FloorNo);
                flatData.TotalRoom = int.Parse(request.TotalRoom) == 0 ? flatData.TotalRoom : int.Parse(request.TotalRoom);
                flatData.FlatAdvance = int.Parse(request.FlatAdvance) == 0 ? flatData.FlatAdvance : int.Parse(request.FlatAdvance);
                flatData.CategoryId = int.Parse(request.CategoryId) == 0 ? flatData.CategoryId : int.Parse(request.CategoryId);
                flatData.UpdatedDate = DateTime.UtcNow;
                flatData.Active = int.Parse(request.Active);

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
                response.Message = "Flat updated successfully";
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
        public async Task<ApiResponse> DeleteFlat(List<string> Ids, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Flat>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var flatToDelete = await _unitOfWork.Flats.GetAllAsync(genericReq);
                if (flatToDelete == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - No flat found with the provided IDs: {Ids}";
                    return response;
                }
                var assignId = flatToDelete.Select(x => x.AssignedId);
                if (assignId != null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"Unsuccessful - flat already assigned";
                    return response;
                }

                _unitOfWork.Flats.RemoveRange(flatToDelete);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting flat";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{flatToDelete.Count} Flat(s) deleted Successfully";
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