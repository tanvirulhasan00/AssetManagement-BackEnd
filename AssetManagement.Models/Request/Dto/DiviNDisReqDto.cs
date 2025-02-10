using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class DiviNDisReqDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Active { get; set; }
    }
}