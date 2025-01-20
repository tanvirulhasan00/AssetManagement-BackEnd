using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class HouseCreateReqDto
    {
        public string? Name { get; set; }
        public int AreaId { get; set; }
        public int TotalFloor { get; set; }
        public int TotalFlat { get; set; }
        public string? Road { get; set; }
        public long PostCode { get; set; }
        [Required]
        public int Active { get; set; }
    }
}