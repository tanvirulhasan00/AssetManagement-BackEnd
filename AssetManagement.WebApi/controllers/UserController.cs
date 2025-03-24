using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Request.Generic;
using AssetManagement.Models.Response.Api;
using AssetManagement.Models.Response.Dto;
using AssetManagement.Repositories.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.WebApi.controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        public UserController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }
        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "admin,manager")]
        public async Task<ApiResponse> GetAllUser(CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<ApplicationUser>
            {
                Expression = null,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync(genericReq);
                if (users == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "User not found";
                    response.Result = users;
                    return response;
                }
                var userRes = users.Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    UserName = s.UserName,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    NidNumber = s.NidNumber,
                    ProfilePicUrl = s.ProfilePicUrl,
                    Email = s.Email,
                    Active = s.Active,
                });
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = userRes;
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
        public async Task<ApiResponse> GetUser(string Id, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var genericReq = new GenericRequest<ApplicationUser>
            {
                Expression = x => x.Id == Id,
                IncludeProperties = null,
                NoTracking = true,
                CancellationToken = cancellationToken
            };
            try
            {
                var user = await _unitOfWork.Users.GetAsync(genericReq);
                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "User not found";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = user;
                return response;
            }
            catch (TaskCanceledException ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.RequestTimeout;
                response.Message = ex.Message;
                response.Error = ex.GetType().Name;
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
        [Route("update")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> UpdateUser(UserInfoUpdateReqDto userDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (userDto.Id == "" || userDto.Id == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - userId is not provided";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<ApplicationUser>
                {
                    Expression = x => x.Id == userDto.Id,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var user = await _unitOfWork.Users.GetAsync(genericReq);
                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - user not found with the id {userDto.Id}";
                    return response;
                }

                if (!string.IsNullOrEmpty(user.ProfilePicUrl))
                {
                    _unitOfWork.Image.ImageDelete(user.ProfilePicUrl);
                }
                if (!string.IsNullOrEmpty(user.NidPicUrl))
                {
                    _unitOfWork.Image.ImageDelete(user.NidPicUrl);
                }

                var profilePicUrl = "";
                var nidPicUrl = "";

                if (userDto.ProfilePicUrl != null)
                {

                    profilePicUrl = await _unitOfWork.Image.ImageUpload(userDto.ProfilePicUrl);
                }
                if (userDto.NidPicUrl != null)
                {

                    nidPicUrl = await _unitOfWork.Image.ImageUpload(userDto.NidPicUrl);
                }


                user.Name = (userDto.Name == null || userDto.Name == "") ? user.Name : userDto.Name;
                user.UserName = (userDto.UserName == null || userDto.UserName == "") ? user.UserName : userDto.UserName;
                user.PhoneNumber = (userDto.PhoneNumber == null || userDto.PhoneNumber == "") ? user.PhoneNumber : userDto.PhoneNumber;
                user.Email = (userDto.Email == null || userDto.Email == "") ? user.Email : userDto.Email;
                user.Address = (userDto.Address == null || userDto.Address == "") ? user.Address : userDto.Address;
                user.NidNumber = (userDto.NidNumber == null || userDto.NidNumber == "") ? user.NidNumber : userDto.NidNumber;
                user.ProfilePicUrl = (profilePicUrl == null || profilePicUrl == "") ? user.ProfilePicUrl : profilePicUrl;
                user.NidPicUrl = (nidPicUrl == null || nidPicUrl == "") ? user.NidPicUrl : nidPicUrl;
                user.Active = int.Parse(userDto.Active);
                user.UpdatedDate = DateTime.UtcNow;
                _unitOfWork.Users.Update(user);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while updating user.";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "User updated successfully";
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
        public async Task<ApiResponse> DeleteUser(List<string> userIds, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (userIds.Count == 0 || userIds == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - No UserIDs provided";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<ApplicationUser>
                {
                    Expression = x => userIds.Contains(x.Id.ToString()),
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var userData = await _unitOfWork.Users.GetAllAsync(genericReq);
                if (userData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - user not found with the id {userIds}";
                    return response;
                }

                foreach (var user in userData)
                {
                    if (!string.IsNullOrEmpty(user.ProfilePicUrl))
                    {
                        _unitOfWork.Image.ImageDelete(user.ProfilePicUrl);
                    }
                    if (!string.IsNullOrEmpty(user.NidPicUrl))
                    {
                        _unitOfWork.Image.ImageDelete(user.NidPicUrl);
                    }
                }

                _unitOfWork.Users.RemoveRange(userData);
                int res = await _unitOfWork.Save();
                if (res == 0)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while deleting user";
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = $"{userData.Count} User(s) deleted Successfully";
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