using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class AssignCreateReqDto
    {
        public string RenterId { get; set; }
        public string FlatId { get; set; }
        public string FlatPrice { get; set; }
        public string FlatAdvanceAmountGiven { get; set; }
        public string FlatAdvanceAmountDue { get; set; }
        public string Active { get; set; }

    }
}