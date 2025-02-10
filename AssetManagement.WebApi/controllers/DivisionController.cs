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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/division")]
    [ApiVersion("1.0")]
    public class DivisionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DivisionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllDivision(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Division>
            {
                Expression = null,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var division = await _unitOfWork.Division.GetAllAsync(genericReq);
                if (division == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = division;
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
        public async Task<ApiResponse> GetDivision(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Division>
            {
                Expression = x => x.Id == Id,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var division = await _unitOfWork.Division.GetAsync(genericReq);
                if (division == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = division;
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
        public async Task<ApiResponse> CreateDivision([FromBody] DiviNDisCreateReqDto divisionDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (divisionDto.Name == null || divisionDto.Name == "")
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful";
                return response;
            }

            try
            {
                var genericReq = new GenericRequest<Division>
                {
                    Expression = x => x.Name == divisionDto.Name,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var duplicateCheck = await _unitOfWork.Division.GetAllAsync(genericReq);
                if (duplicateCheck.Count > 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.Conflict;
                    response.Message = "Duplicate division name found";
                    return response;
                }
                Division divisionToCreate = new()
                {
                    Name = divisionDto.Name,
                    Active = int.Parse(divisionDto.Active),
                };
                await _unitOfWork.Division.AddAsync(divisionToCreate);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while creating division";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Create Successful";
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
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> UpdateDivision([FromBody] DiviNDisReqDto divisionDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (divisionDto.Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - id is not valid";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<Division>
                {
                    Expression = x => x.Id == divisionDto.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var divisionData = await _unitOfWork.Division.GetAsync(genericReq);
                if (divisionData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - division data not found with the id {divisionDto.Id}";
                    return response;
                }
                divisionData.Name = (divisionDto.Name == null || divisionDto.Name == "") ? divisionData.Name : divisionDto.Name;
                divisionData.Active = int.Parse(divisionDto.Active);

                _unitOfWork.Division.Update(divisionData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something wrong while updating division";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Update Successful";
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

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteDivision(List<string> Ids, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Division>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var divisionToDelete = await _unitOfWork.Division.GetAllAsync(genericReq);
                if (divisionToDelete == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - No division found with the provided IDs: {Ids}";
                    return response;
                }

                _unitOfWork.Division.RemoveRange(divisionToDelete);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting division";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{divisionToDelete.Count} Division(s) deleted Successfully";
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