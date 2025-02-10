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
        public int PaymentAmount { get; set; }
        public int PaymentDueAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public string? PaymentStatus { get; set; }
        public long RenterId { get; set; }
        [ForeignKey("RenterId")]
        public Renter? Renter { get; set; }
    }
}