using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class FamilyMember
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? NidNumber { get; set; }
        public string? Occupation { get; set; }
        public string? Relation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? NidImageUrl { get; set; }
        public int Active { get; set; }
        public int IsEmergencyContact { get; set; }

        public long RenterId { get; set; }
        [ForeignKey("RenterId")]
        public Renter? Renter { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}