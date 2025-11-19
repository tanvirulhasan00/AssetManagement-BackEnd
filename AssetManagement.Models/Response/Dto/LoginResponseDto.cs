using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Response.Dto
{
    public class LoginResponseDto
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpire { get; set; }
        // public string? Role { get; set; }
    }
}