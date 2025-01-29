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
        // [Authorize(Roles = "admin,manager")]
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
                    return response;
                }
                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Sucessful";
                response.Result = users;
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
        //[Authorize(Roles = "admin")]
        public async Task<ApiResponse> UpdateUser(UserInfoUpdateReqDto userDto, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (userDto.Id == "" || userDto.Id == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - userId is not valid";
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
        // [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (userId == "" || userId == null)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Unsuccessful - UserId needed to delete";
                return response;
            }
            try
            {
                var genericReq = new GenericRequest<ApplicationUser>
                {
                    Expression = x => x.Id == userId,
                    IncludeProperties = null,
                    NoTracking = true,
                    CancellationToken = cancellationToken
                };
                var userData = await _unitOfWork.Users.GetAsync(genericReq);
                if (userData == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Unsuccessful - user not found with the id {userId}";
                    return response;
                }

                // Get the root path of wwwroot
                var rootPath = _env.WebRootPath;

                // Delete old profile picture if it exists
                if (!string.IsNullOrEmpty(userData.ProfilePicUrl))
                {
                    var oldProfilePicPath = Path.Combine(rootPath, userData.ProfilePicUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldProfilePicPath))
                    {
                        System.IO.File.Delete(oldProfilePicPath);
                    }
                }

                // Delete old NID picture if it exists
                if (!string.IsNullOrEmpty(userData.NidPicUrl))
                {
                    var oldNidPicPath = Path.Combine(rootPath, userData.NidPicUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldNidPicPath))
                    {
                        System.IO.File.Delete(oldNidPicPath);
                    }
                }


                _unitOfWork.Users.Remove(userData);
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
                response.Message = "User deleted successfully";
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