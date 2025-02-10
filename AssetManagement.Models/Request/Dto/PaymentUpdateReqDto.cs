using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class PaymentUpdateReqDto
    {
        public long Id { get; set; }
        public string PaymentDueAmount { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}