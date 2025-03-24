using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Models.Request.Dto
{
    public class FamilyMemberGrtReqDto
    {
        public long FamilyMemberId { get; set; }
        public long RenterId { get; set; }

    }
}