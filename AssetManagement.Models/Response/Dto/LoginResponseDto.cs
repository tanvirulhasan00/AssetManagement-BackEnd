using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Response.Dto
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        // public string? Role { get; set; }
    }
}