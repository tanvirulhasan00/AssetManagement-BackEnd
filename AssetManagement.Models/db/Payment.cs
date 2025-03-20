using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string TransactionId { get; set; }
        public string? InvoiceId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentType { get; set; }
        public int PaymentAmount { get; set; }
        public int FlatUtilities { get; set; }
        public int PaymentDue { get; set; }
        public int PaymentAdvance { get; set; }
        public string? PaymentMonth { get; set; }
        public string? PaymentYear { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public string ReferenceNo { get; set; }
        public long? AssignId { get; set; }
        [ForeignKey("AssignId")]
        public Assign? Assign { get; set; }


    }
}