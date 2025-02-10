using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class PaymentCreateReqDto
    {
        public string? TransactionId { get; set; }
        public string? UserId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentAmount { get; set; }
        public string? PaymentDueAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string? PaymentStatus { get; set; }
        [Required]
        public string RenterId { get; set; }

    }
}