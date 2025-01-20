using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class RenterCreateReqDto
    {
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Address { get; set; }
        public string? Occupation { get; set; }
        public string? Religion { get; set; }
        public string? Education { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? NidNumber { get; set; }
        public string? PassportNumber { get; set; }

        public string? PrevRoomOwnerName { get; set; }
        public string? PrevRoomOwnerNumber { get; set; }
        public string? PrevRoomOwnerAddress { get; set; }
        public string? ReasonToLeavePrevHome { get; set; }

        public string? ImageUrl { get; set; }
        public string? NidImageUrl { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        public int Active { get; set; }
    }
}