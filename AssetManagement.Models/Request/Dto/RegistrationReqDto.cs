using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class RegistrationReqDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? NidPicUrl { get; set; }
    }
}