
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AssetManagement.Models.db
{
    public class MonthlyPaymentStatus
    {
        [Key]
        public long Id { get; set; }
        public long? AssignId { get; set; }
        [ForeignKey("AssignId")]
        public Assign? Assign { get; set; }
        public string? January { get; set; }
        public string? February { get; set; }
        public string? March { get; set; }
        public string? April { get; set; }
        public string? May { get; set; }
        public string? June { get; set; }
        public string? July { get; set; }
        public string? August { get; set; }
        public string? September { get; set; }
        public string? October { get; set; }
        public string? November { get; set; }
        public string? December { get; set; }
        public string Year { get; set; }
    }
}