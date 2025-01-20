using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Response.Api;

namespace AssetManagement.Repositories.IRepos.IAuth
{
    public interface IAuthRepository
    {
        bool IsUniqueUser(string phoneNumber);
        Task<ApiResponse> Login(LoginRequestDto request);
        Task<ApiResponse> Registration(RegistrationReqDto request);
        Task<ApiResponse> ResetPassword(ResetPassReqDto request);
        Task<ApiResponse> UpdatePassword(UpdatePassReqDto request);
        Task<ApiResponse> UpdateUserInfo(UserInfoUpdateReqDto request);
    }
}