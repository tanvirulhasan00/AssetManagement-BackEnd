using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Models.db
{
    public class Assign
    {
        [Key]
        public long Id { get; set; }
        [Required]

        public string ReferenceNo { get; set; }
        public long? RenterId { get; set; }
        [ForeignKey("RenterId")]
        public Renter? Renter { get; set; }
        public long? FlatId { get; set; }
        [ForeignKey("FlatId")]
        public Flat? Flat { get; set; }
        public long FlatRent { get; set; }
        public long DueRent { get; set; }
        public long AdvanceRent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Active { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}