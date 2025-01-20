using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class FamilyMemberUpdateDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? NidNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Relation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? NidImageUrl { get; set; }
        public long RenterId { get; set; }
        [Required]
        public int Active { get; set; }
        public int IsEmergencyContact { get; set; }
    }
}