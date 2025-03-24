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
        public string? InvoiceId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public string? PaymentAmount { get; set; }
        public string? FlatUtilities { get; set; }
        public string? PaymentDue { get; set; }
        public string? PaymentAdvance { get; set; }
        public string? PaymentMonth { get; set; }
        public string? PaymentYear { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public string ReferenceNo { get; set; }
        public long AssignId { get; set; }

    }
}