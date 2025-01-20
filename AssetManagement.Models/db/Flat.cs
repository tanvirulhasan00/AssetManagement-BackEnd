using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Models.db
{
    public class Flat
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public int FloorNo { get; set; }
        public int TotalRoom { get; set; }
        public long Price { get; set; }
        [Required]
        public int Active { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Categories { get; set; }

        public int HouseId { get; set; }
        [ForeignKey("HouseId")]
        public House? HouseDetails { get; set; }
    }
}