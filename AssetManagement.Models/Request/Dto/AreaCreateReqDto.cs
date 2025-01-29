using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class AreaCreateReqDto
    {
        public string? Name { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        public string? SubDistrict { get; set; }
        public string? Thana { get; set; }
        public string? Mouza { get; set; }
        [Required]
        public int Active { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}