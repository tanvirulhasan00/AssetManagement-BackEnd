using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class MonthlyPaymentStatusReqDto
    {
        public string AssignId { get; set; }
        public string? Year { get; set; }

    }
}