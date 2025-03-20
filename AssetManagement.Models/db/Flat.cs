using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AssetManagement.Models.db
{
    public class Flat
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
        public int FloorNo { get; set; }
        public int TotalRoom { get; set; }
        public string? AssignedId { get; set; }
        public int FlatAdvance { get; set; } = 0;

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public int? HouseId { get; set; }
        [ForeignKey("HouseId")]
        public House? House { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Active { get; set; }
    }
}