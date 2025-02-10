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
        public string AreaId { get; set; }
        public string? TotalFloor { get; set; }
        public string? TotalFlat { get; set; }
        public string? Road { get; set; }
        public string? PostCode { get; set; }
        public string? Active { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}