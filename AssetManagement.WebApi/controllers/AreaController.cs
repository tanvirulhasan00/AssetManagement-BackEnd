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
    [Route("api/v{version:apiVersion}/area")]
    [ApiVersion("1.0")]
    public class AreaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AreaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("getall")]
        // [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllArea(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Area>
            {
                Expression = null,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var area = await _unitOfWork.Areas.GetAllAsync(genericReq);
                if (area == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = area;
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
        // [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetArea(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Area>
            {
                Expression = x => x.Id == Id,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var area = await _unitOfWork.Areas.GetAsync(genericReq);
                if (area == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = area;
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
        // [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> CreateArea([FromBody] AreaCreateReqDto areaDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (areaDto.Name == null || areaDto.Name == "")
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful";
                return response;
            }

            try
            {
                var genericReq = new GenericRequest<Area>
                {
                    Expression = x => x.Name == areaDto.Name,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var duplicateCheck = await _unitOfWork.Areas.GetAllAsync(genericReq);
                if (duplicateCheck.Count > 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.Conflict;
                    response.Message = "Duplicate area name found";
                    return response;
                }
                Area areaToCreate = new()
                {
                    Name = areaDto.Name,
                    DistrictId = areaDto.DistrictId,
                    DivisionId = areaDto.DivisionId,
                    SubDistrict = areaDto.SubDistrict,
                    Thana = areaDto.Thana,
                    Mouza = areaDto.Mouza,
                    Active = areaDto.Active,
                };
                await _unitOfWork.Areas.AddAsync(areaToCreate);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while creating area";
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
        // [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> UpdateArea([FromBody] AreaUpdateReqDto areaDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (areaDto.Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - id is not valid";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<Area>
                {
                    Expression = x => x.Id == areaDto.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var areaData = await _unitOfWork.Areas.GetAsync(genericReq);
                if (areaData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - area data not found with the id {areaDto.Id}";
                    return response;
                }
                areaData.Name = (areaDto.Name == null || areaDto.Name == "") ? areaData.Name : areaDto.Name;
                areaData.DistrictId = areaDto.DistrictId == 0 ? areaData.DistrictId : areaDto.DistrictId;
                areaData.DivisionId = areaDto.DivisionId == 0 ? areaData.DivisionId : areaDto.DivisionId;
                areaData.SubDistrict = (areaDto.SubDistrict == null || areaDto.SubDistrict == "") ? areaData.SubDistrict : areaDto.SubDistrict;
                areaData.Thana = (areaDto.Thana == null || areaDto.Thana == "") ? areaData.Thana : areaDto.Thana;
                areaData.Mouza = (areaDto.Mouza == null || areaDto.Mouza == "") ? areaData.Mouza : areaDto.Mouza;
                areaData.Active = areaDto.Active;

                _unitOfWork.Areas.Update(areaData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while updating area";
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
        // [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> DeleteArea(int Id, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Area>
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var areaData = await _unitOfWork.Areas.GetAsync(genericReq);
                if (areaData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - area data not found with the id {Id}";
                    return response;
                }

                _unitOfWork.Areas.Remove(areaData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while deleting area";
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