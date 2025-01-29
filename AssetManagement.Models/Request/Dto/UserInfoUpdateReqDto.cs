using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Models.Request.Dto
{
    public class UserInfoUpdateReqDto
    {
        [Required]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? NidNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePicUrl { get; set; }
        public IFormFile? NidPicUrl { get; set; }
        public string? Active { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}