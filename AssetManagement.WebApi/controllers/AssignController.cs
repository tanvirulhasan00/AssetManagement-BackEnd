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
    [Route("api/v{version:apiVersion}/assign")]
    [ApiVersion("1.0")]
    public class AssignController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;
        public AssignController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllAssign(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Assign>
            {
                Expression = null,
                IncludeProperties = "Renter,Flat",
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var assign = await _unitOfWork.Assign.GetAllAsync(genericReq);
                if (assign == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = assign;
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
        public async Task<ApiResponse> GetAssign(long Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<Assign>
            {
                Expression = x => x.Id == Id,
                IncludeProperties = "Renter,Flat",
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var assign = await _unitOfWork.Assign.GetAsync(genericReq);
                if (assign == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = assign;
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
        [Route("create")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> CreateAssign([FromBody] AssignCreateReqDto assignDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            string reference = GenerateRef();
            try
            {
                Assign assignToCreate = new()
                {
                    ReferenceNo = reference,
                    RenterId = int.Parse(assignDto.RenterId),
                    FlatId = int.Parse(assignDto.FlatId),
                    FlatPrice = int.Parse(assignDto.FlatPrice),
                    Active = int.Parse(assignDto.Active),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                await _unitOfWork.Assign.AddAsync(assignToCreate);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went went wrong while assign flat";
                    return response;
                }

                var genericReq = new GenericRequest<Flat>
                {
                    Expression = x => x.Id == int.Parse(assignDto.FlatId),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };

                var flat = await _unitOfWork.Flats.GetAsync(genericReq);
                flat.AssignedId = reference;
                _unitOfWork.Flats.Update(flat);
                await _unitOfWork.Save();


                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Flat successfully assigned";
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

        private static string GenerateRef()
        {
            return $"STN-{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()}";
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> UpdateAssign([FromBody] AssignUpdateReqDto assignDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (assignDto.Id == 0)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - id is not valid";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<Assign>
                {
                    Expression = x => x.Id == assignDto.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var assignData = await _unitOfWork.Assign.GetAsync(genericReq);
                if (assignData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - assign data not found with the id {assignDto.Id}";
                    return response;
                }
                assignData.RenterId = int.Parse(assignDto.RenterId) == 0 ? assignData.RenterId : int.Parse(assignDto.RenterId);
                assignData.FlatId = int.Parse(assignDto.FlatId) == 0 ? assignData.FlatId : int.Parse(assignDto.FlatId);
                assignData.FlatPrice = int.Parse(assignDto.FlatPrice) == 0 ? assignData.FlatPrice : int.Parse(assignDto.FlatPrice);
                assignData.Active = int.Parse(assignDto.Active);
                assignData.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Assign.Update(assignData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while updating assign data";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Update successful";
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
        public async Task<ApiResponse> DeleteAssign(List<string> Ids, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Assign>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var assignToDelete = await _unitOfWork.Assign.GetAllAsync(genericReq);
                if (assignToDelete == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - No assign found with the provided IDs: {Ids}";
                    return response;
                }

                _unitOfWork.Assign.RemoveRange(assignToDelete);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting assign";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{assignToDelete.Count} Assign(s) deleted Successfully";
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