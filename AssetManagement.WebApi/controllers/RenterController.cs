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
    [Route("api/v{version:apiVersion}/renter")]
    [ApiVersion("1.0")]
    public class RenterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RenterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> GetAllRenter(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Renter>()
                {
                    Expression = null,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var renterData = await _unitOfWork.Renters.GetAllAsync(genericReq);
                if (renterData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = renterData;
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
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> GetRenter(int Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Renter>()
                {
                    Expression = x => x.Id == Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var renterData = await _unitOfWork.Renters.GetAsync(genericReq);
                if (renterData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - data not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = renterData;
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
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> CreateRenter(RenterCreateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var profilePicUrl = "";
                var nidPicUrl = "";

                if (request.ImageUrl != null)
                {

                    profilePicUrl = await _unitOfWork.Image.ImageUpload(request.ImageUrl);
                }
                if (request.NidImageUrl != null)
                {

                    nidPicUrl = await _unitOfWork.Image.ImageUpload(request.NidImageUrl);
                }

                Renter renterToCreate = new()
                {
                    Name = request.Name,
                    FatherName = request.FatherName,
                    MotherName = request.MotherName,
                    DateOfBirth = request.DateOfBirth,
                    MaritalStatus = request.MaritalStatus,
                    Address = request.Address,
                    Occupation = request.Occupation,
                    Religion = request.Religion,
                    Education = request.Education,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    NidNumber = request.NidNumber,
                    PassportNumber = request.PassportNumber,
                    PrevRoomOwnerName = request.PrevRoomOwnerName,
                    PrevRoomOwnerNumber = request.PrevRoomOwnerNumber,
                    PrevRoomOwnerAddress = request.PrevRoomOwnerAddress,
                    ReasonToLeavePrevHome = request.ReasonToLeavePrevHome,
                    ImageUrl = profilePicUrl,
                    NidImageUrl = nidPicUrl,
                    Active = int.Parse(request.Active),
                    StartDate = request.StartDate,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                _unitOfWork.Renters?.AddAsync(renterToCreate);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something went wrong while creating renter";
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
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> UpdateRenter(RenterUpdateReqDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReq = new GenericRequest<Renter>()
                {
                    Expression = x => x.Id == request.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var renterData = await _unitOfWork.Renters.GetAsync(genericReq);
                if (renterData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful - data not found";
                    return response;
                }

                if (!string.IsNullOrEmpty(renterData.ImageUrl))
                {
                    _unitOfWork.Image.ImageDelete(renterData.ImageUrl);
                }
                if (!string.IsNullOrEmpty(renterData.NidImageUrl))
                {
                    _unitOfWork.Image.ImageDelete(renterData.NidImageUrl);
                }

                var profilePicUrl = "";
                var nidPicUrl = "";

                if (request.ImageUrl != null)
                {

                    profilePicUrl = await _unitOfWork.Image.ImageUpload(request.ImageUrl);
                }
                if (request.NidImageUrl != null)
                {

                    nidPicUrl = await _unitOfWork.Image.ImageUpload(request.NidImageUrl);
                }



                renterData.Name = (request.Name == null || request.Name == "") ? renterData.Name : request.Name;
                renterData.FatherName = (request.FatherName == null || request.FatherName == "") ? renterData.FatherName : request.FatherName;
                renterData.MotherName = (request.MotherName == null || request.MotherName == "") ? renterData.MotherName : request.MotherName;
                renterData.DateOfBirth = request.DateOfBirth == null ? renterData.DateOfBirth : request.DateOfBirth;
                renterData.MaritalStatus = (request.MaritalStatus == null || request.MaritalStatus == "") ? renterData.MaritalStatus : request.MaritalStatus;
                renterData.Address = (request.Address == null || request.Address == "") ? renterData.Address : request.Address;
                renterData.Occupation = (request.Occupation == null || request.Occupation == "") ? renterData.Occupation : request.Occupation;
                renterData.Religion = (request.Religion == null || request.Religion == "") ? renterData.Religion : request.Religion;
                renterData.Education = (request.Education == null || request.Education == "") ? renterData.Education : request.Education;
                renterData.PhoneNumber = (request.PhoneNumber == null || request.PhoneNumber == "") ? renterData.PhoneNumber : request.PhoneNumber;
                renterData.Email = (request.Email == null || request.Email == "") ? renterData.Email : request.Email;
                renterData.NidNumber = (request.NidNumber == null || request.NidNumber == "") ? renterData.NidNumber : request.NidNumber;
                renterData.PassportNumber = (request.PassportNumber == null || request.PassportNumber == "") ? renterData.PassportNumber : request.PassportNumber;
                renterData.PrevRoomOwnerName = (request.PrevRoomOwnerName == null || request.PrevRoomOwnerName == "") ? renterData.PrevRoomOwnerName : request.PrevRoomOwnerName;
                renterData.PrevRoomOwnerNumber = (request.PrevRoomOwnerNumber == null || request.PrevRoomOwnerNumber == "") ? renterData.PrevRoomOwnerNumber : request.PrevRoomOwnerNumber;
                renterData.PrevRoomOwnerAddress = (request.PrevRoomOwnerAddress == null || request.PrevRoomOwnerAddress == "") ? renterData.PrevRoomOwnerAddress : request.PrevRoomOwnerAddress;
                renterData.ReasonToLeavePrevHome = (request.ReasonToLeavePrevHome == null || request.ReasonToLeavePrevHome == "") ? renterData.ReasonToLeavePrevHome : request.ReasonToLeavePrevHome;
                renterData.ImageUrl = (profilePicUrl == null || profilePicUrl == "") ? renterData.ImageUrl : profilePicUrl;
                renterData.NidImageUrl = (nidPicUrl == null || nidPicUrl == "") ? renterData.NidImageUrl : nidPicUrl;
                renterData.Active = int.Parse(request.Active);
                renterData.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Renters?.Update(renterData);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something went wrong while updating renter";
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
                response.Error = ex.GetType().Name;
                return response;
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteRenter(List<string> Ids, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<Renter>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var renterData = await _unitOfWork.Renters.GetAllAsync(genericReq);
                if (renterData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - renter not found with the id {Ids}";
                    return response;
                }

                foreach (var renter in renterData)
                {
                    if (!string.IsNullOrEmpty(renter.ImageUrl))
                    {
                        _unitOfWork.Image.ImageDelete(renter.ImageUrl);
                    }
                    if (!string.IsNullOrEmpty(renter.NidImageUrl))
                    {
                        _unitOfWork.Image.ImageDelete(renter.NidImageUrl);
                    }
                }


                _unitOfWork.Renters.RemoveRange(renterData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting renter";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{renterData.Count} Renter(s) deleted Successfully";
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