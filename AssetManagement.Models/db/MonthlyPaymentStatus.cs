
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AssetManagement.Models.db
{
    public class MonthlyPaymentStatus
    {
        [Key]
        public long Id { get; set; }
        public long RenterId { get; set; }
        [ForeignKey("RenterId")]
        public Renter? Renter { get; set; }
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }
        public string Year { get; set; }
    }
}