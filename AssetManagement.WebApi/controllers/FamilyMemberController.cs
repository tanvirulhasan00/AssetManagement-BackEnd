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
    [Route("api/v{version:apiVersion}/family-member")]
    [ApiVersion("1.0")]
    public class FamilyMemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FamilyMemberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("getall")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllFamilyMember(FamilyMemberGrtReqDto? req, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var familyMemberData = new List<FamilyMember>();
                if (req == null)
                {
                    familyMemberData = await _unitOfWork.FamilyMembers.GetAllAsync(new GenericRequest<FamilyMember>()
                    {
                        Expression = null,
                        IncludeProperties = "Renter",
                        NoTracking = true,
                        CancellationToken = cancellationToken
                    });
                }
                if (req?.FamilyMemberId > 0)
                {
                    familyMemberData = await _unitOfWork.FamilyMembers.GetAllAsync(new GenericRequest<FamilyMember>()
                    {
                        Expression = x => x.Id == req.FamilyMemberId,
                        IncludeProperties = "Renter",
                        NoTracking = true,
                        CancellationToken = cancellationToken
                    });
                }
                if (req?.RenterId > 0)
                {
                    familyMemberData = await _unitOfWork.FamilyMembers.GetAllAsync(new GenericRequest<FamilyMember>()
                    {
                        Expression = x => x.RenterId == req.RenterId,
                        IncludeProperties = "Renter",
                        NoTracking = true,
                        CancellationToken = cancellationToken
                    });
                }
                if (familyMemberData.Count == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Data";
                    response.Result = familyMemberData;
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = familyMemberData;
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

        [HttpGet]
        [Route("get")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetFamilyMember(FamilyMemberGrtReqDto req, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                var genericReqForFId = new GenericRequest<FamilyMember>()
                {
                    Expression = x => x.Id == req.FamilyMemberId,
                    IncludeProperties = "Renter",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var genericReqForRenterId = new GenericRequest<FamilyMember>()
                {
                    Expression = x => x.RenterId == req.RenterId,
                    IncludeProperties = "Renter",
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var familyMemberData = new FamilyMember();
                if (req.FamilyMemberId != 0 && req.FamilyMemberId != null)
                {
                    familyMemberData = await _unitOfWork.FamilyMembers.GetAsync(genericReqForFId);
                }
                if (req.RenterId != 0 && req.RenterId != null)
                {
                    familyMemberData = await _unitOfWork.FamilyMembers.GetAsync(genericReqForRenterId);
                }
                if (familyMemberData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "No Data";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = familyMemberData;
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
        public async Task<ApiResponse> CreateFamilyMember(FamilyMemberCreateDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var imageUrl = "";
            var nidImageUrl = "";

            var isUniqueUser = _unitOfWork.FamilyMembers.IsUniqueMember(request.NidNumber);
            if (isUniqueUser == false)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.Conflict;
                response.Message = "Person already exists with this Nid number";
                return response;
            }
            try
            {
                if (request.ImageUrl != null)
                {

                    imageUrl = await _unitOfWork.Image.ImageUpload(request.ImageUrl);
                }
                if (request.NidImageUrl != null)
                {

                    nidImageUrl = await _unitOfWork.Image.ImageUpload(request.NidImageUrl);
                }
                FamilyMember familyMemberToCreate = new()
                {
                    Name = request.Name,
                    NidNumber = request.NidNumber,
                    Occupation = request.Occupation,
                    Relation = request.Relation,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    ImageUrl = imageUrl,
                    NidImageUrl = nidImageUrl,
                    IsEmergencyContact = int.Parse(request.IsEmergencyContact),
                    RenterId = request.RenterId,
                    Active = int.Parse(request.Active),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                _unitOfWork.FamilyMembers?.AddAsync(familyMemberToCreate);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while creating family member";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.Created;
                response.Message = "Family-Member created successful";
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
        public async Task<ApiResponse> UpdateFamilyMember(FamilyMemberUpdateDto request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var imageUrl = "";
            var nidImageUrl = "";
            try
            {
                var genericReq = new GenericRequest<FamilyMember>()
                {
                    Expression = x => x.Id == request.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var familyMemberData = await _unitOfWork.FamilyMembers.GetAsync(genericReq);
                if (familyMemberData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Unsuccessful";
                    return response;
                }

                if (!string.IsNullOrEmpty(familyMemberData.ImageUrl))
                {
                    _unitOfWork.Image.ImageDelete(familyMemberData.ImageUrl);
                }
                if (!string.IsNullOrEmpty(familyMemberData.NidImageUrl))
                {
                    _unitOfWork.Image.ImageDelete(familyMemberData.NidImageUrl);
                }

                if (request.ImageUrl != null)
                {

                    imageUrl = await _unitOfWork.Image.ImageUpload(request.ImageUrl);
                }
                if (request.NidImageUrl != null)
                {

                    nidImageUrl = await _unitOfWork.Image.ImageUpload(request.NidImageUrl);
                }


                familyMemberData.Name = (request.Name == null || request.Name == "") ? familyMemberData.Name : request.Name;
                familyMemberData.NidNumber = (request.NidNumber == null || request.NidNumber == "") ? familyMemberData.NidNumber : request.NidNumber;
                familyMemberData.Occupation = (request.Occupation == null || request.Occupation == "") ? familyMemberData.Occupation : request.Occupation;
                familyMemberData.Relation = (request.Relation == null || request.Relation == "") ? familyMemberData.Relation : request.Relation;
                familyMemberData.PhoneNumber = (request.PhoneNumber == null || request.PhoneNumber == "") ? familyMemberData.PhoneNumber : request.PhoneNumber;
                familyMemberData.Address = (request.Address == null || request.Address == "") ? familyMemberData.Address : request.Address;
                familyMemberData.ImageUrl = (imageUrl == null || imageUrl == "") ? familyMemberData.ImageUrl : imageUrl;
                familyMemberData.NidImageUrl = (nidImageUrl == null || nidImageUrl == "") ? familyMemberData.NidImageUrl : nidImageUrl;
                familyMemberData.IsEmergencyContact = int.Parse(request.IsEmergencyContact) == 0 ? familyMemberData.IsEmergencyContact : int.Parse(request.IsEmergencyContact);
                familyMemberData.RenterId = request.RenterId == 0 ? familyMemberData.RenterId : request.RenterId;
                familyMemberData.Active = int.Parse(request.Active);
                familyMemberData.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.FamilyMembers?.Update(familyMemberData);
                var result = await _unitOfWork.Save();
                if (result == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Unsuccessful - Something wrong while updating family member";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Family-Member updated successfully";
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
        public async Task<ApiResponse> DeleteFamilyMember(List<string> Ids, CancellationToken cancellationToken)
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
                var genericReq = new GenericRequest<FamilyMember>
                {
                    Expression = x => Ids.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var familyMemberData = await _unitOfWork.FamilyMembers.GetAllAsync(genericReq);
                if (familyMemberData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - family member data not found with the id {Ids}";
                    return response;
                }

                foreach (var member in familyMemberData)
                {
                    if (!string.IsNullOrEmpty(member.ImageUrl))
                    {
                        _unitOfWork.Image.ImageDelete(member.ImageUrl);
                    }
                    if (!string.IsNullOrEmpty(member.NidImageUrl))
                    {
                        _unitOfWork.Image.ImageDelete(member.NidImageUrl);
                    }
                }


                _unitOfWork.FamilyMembers.RemoveRange(familyMemberData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting family member";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{familyMemberData.Count} FamilyMember(s) deleted Successfully";
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