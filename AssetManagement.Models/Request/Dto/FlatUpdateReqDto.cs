using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class FlatUpdateReqDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int FloorNo { get; set; }
        public int TotalRoom { get; set; }
        public long Price { get; set; }
        [Required]
        public int Active { get; set; }
        public int CategoryId { get; set; }
        public int HouseId { get; set; }

    }
}