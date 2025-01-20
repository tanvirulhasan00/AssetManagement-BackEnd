using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AssetManagement.Database.data;
using AssetManagement.Models.db;
using AssetManagement.Models.Request.Dto;
using AssetManagement.Models.Response.Api;
using AssetManagement.Models.Response.Dto;
using AssetManagement.Repositories.IRepos.IAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssetManagement.Repositories.Repos.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AssetManagementDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _secretKey;
        public AuthRepository(AssetManagementDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, string secretKey)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _secretKey = secretKey;
        }
        public bool IsUniqueUser(string phoneNumber)
        {
            var user = _context.ApplicationUsers?.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiResponse> Login(LoginRequestDto request)
        {
            var response = new ApiResponse();
            var loginRes = new LoginResponseDto();
            try
            {
                var user = _context.ApplicationUsers?.FirstOrDefault(u => u.UserName.ToLower() == request.UserName.ToLower());

                bool isValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (user == null || isValid == false)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Username or password is incorrect!";
                    return response;
                }

                //if user found generate jwt token 
                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                  ]),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginRes.Token = tokenHandler.WriteToken(token);
                UserDto userRes = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    ProfilePicUrl = user.ProfilePicUrl,
                };
                loginRes.User = userRes;
                var jwt = tokenHandler.ReadJwtToken(loginRes.Token);
                loginRes.Role = jwt.Claims.FirstOrDefault(x => x.Type == "role")?.Value;

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                response.Result = loginRes;
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

        public async Task<ApiResponse> Registration(RegistrationReqDto request)
        {
            var response = new ApiResponse();
            ApplicationUser user = new()
            {
                Name = request.Name,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Address = request.Address,
                ProfilePicUrl = request.ProfilePicUrl,
                NidPicUrl = request.NidPicUrl,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    // //var ff =_roleManager.FindByNameAsync("admin");
                    // if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    // {
                    //     await _roleManager.CreateAsync(new IdentityRole("admin"));
                    //     await _roleManager.CreateAsync(new IdentityRole("manager"));
                    // }
                    await _userManager.AddToRoleAsync(user, "manager");

                    response.Success = true;
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = "Successful";
                    //return response;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = "Something went wrong while creating user.";
                    response.Error = result.Errors;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }
        }

        public Task<ApiResponse> ResetPassword(ResetPassReqDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> UpdatePassword(UpdatePassReqDto request)
        {
            var response = new ApiResponse();
            try
            {
                var user = _context.ApplicationUsers?.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user?.Result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Something went wrong while updating password";
                    return response;
                }
                await _userManager.ChangePasswordAsync(user.Result, request.OldPassword, request.NewPassword);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Successful";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
                return response;
            }

        }

        public async Task<ApiResponse> UpdateUserInfo(UserInfoUpdateReqDto request)
        {
            var response = new ApiResponse();
            try
            {
                var user = _context.ApplicationUsers?.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user?.Result == null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.Message = "Something went wrong while updating user info";
                    return response;
                }
                user.Result.Name = (request.Name == null || request.Name == "") ? user.Result.Name : request.Name;
                user.Result.UserName = (request.UserName == null || request.UserName == "") ? user.Result.UserName : request.UserName;
                user.Result.Email = (request.Email == null || request.Email == "") ? user.Result.Email : request.Email;
                user.Result.PhoneNumber = (request.PhoneNumber == null || request.PhoneNumber == "") ? user.Result.PhoneNumber : request.PhoneNumber;
                user.Result.Address = (request.Address == null || request.Address == "") ? user.Result.Address : request.Address;
                user.Result.ProfilePicUrl = (request.ProfilePicUrl == null || request.ProfilePicUrl == "") ? user.Result.ProfilePicUrl : request.ProfilePicUrl;
                user.Result.NidPicUrl = (request.NidPicUrl == null || request.NidPicUrl == "") ? user.Result.NidPicUrl : request.NidPicUrl;

                await _userManager.UpdateAsync(user.Result);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Update Successful";
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
    }
}