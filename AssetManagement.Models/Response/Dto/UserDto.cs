using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Response.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? ProfilePicUrl { get; set; }
    }
}