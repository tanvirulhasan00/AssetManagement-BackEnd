using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class DiviNDisCreateReqDto
    {
        public string? Name { get; set; }
        [Required]
        public int Active { get; set; }
    }
}