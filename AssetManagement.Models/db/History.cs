using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class History
    {
        [Key]
        public long Id { get; set; }
        public string ActionName { get; set; }
        [Required]
        public string ActionBy { get; set; }
        public string? ActionByName { get; set; }
        public DateTime ActionDate { get; set; }
    }
}