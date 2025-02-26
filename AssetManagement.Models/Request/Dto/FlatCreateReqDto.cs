using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class FlatCreateReqDto
    {
        public string? Name { get; set; }
        public string? FloorNo { get; set; }
        public string? TotalRoom { get; set; }
        public string FlatAdvanceAmount { get; set; }
        public string PrevRentDuoAmount { get; set; }
        public string PrevRentAdvanceAmount { get; set; }
        public string Active { get; set; }
        public string CategoryId { get; set; }
        public string HouseId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}