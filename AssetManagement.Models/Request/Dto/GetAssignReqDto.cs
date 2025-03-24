using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Dto
{
    public class GetAssignReqDto
    {
        public long? AssignId { get; set; }
        public string? ReferenceNo { get; set; }
        public long? RenterId { get; set; }
        public long? FlatId { get; set; }
    }
}