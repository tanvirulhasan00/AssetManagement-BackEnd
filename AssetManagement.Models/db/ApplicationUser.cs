using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AssetManagement.Models.db
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        [Required]
        public string? NidNumber { get; set; }
        public string? ProfilePicUrl { get; set; }
        public string? NidPicUrl { get; set; }
        [Required]
        public int Active { get; set; }
    }
}