using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Models.Request.Dto
{
    public class FamilyMemberCreateDto
    {
        public string? Name { get; set; }
        [Required]
        public string NidNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Relation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public IFormFile? NidImageUrl { get; set; }
        public long RenterId { get; set; }
        [Required]
        public int Active { get; set; }
        public int IsEmergencyContact { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}