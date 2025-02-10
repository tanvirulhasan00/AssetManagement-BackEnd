using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Models.Request.Dto
{
    public class RenterUpdateReqDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Religion { get; set; }
        public string? Education { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? NidNumber { get; set; }
        public string? PassportNumber { get; set; }

        public string? PrevRoomOwnerName { get; set; }
        public string? PrevRoomOwnerNumber { get; set; }
        public string? PrevRoomOwnerAddress { get; set; }
        public string? ReasonToLeavePrevHome { get; set; }

        public IFormFile? ImageUrl { get; set; }
        public IFormFile? NidImageUrl { get; set; }
        public string? Active { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}