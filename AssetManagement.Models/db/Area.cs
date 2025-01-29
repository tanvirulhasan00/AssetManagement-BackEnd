using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District? DistrictName { get; set; }
        public int DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public Division? DivisionName { get; set; }
        public string? SubDistrict { get; set; }
        public string? Thana { get; set; }
        public string? Mouza { get; set; }
        [Required]
        public int Active { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}