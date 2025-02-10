using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area? Area { get; set; }
        public int TotalFloor { get; set; }
        public int TotalFlat { get; set; }
        public string? Road { get; set; }
        public long PostCode { get; set; }
        public int Active { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}