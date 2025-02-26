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
        public int FlatAdvanceAmount { get; set; } = 0;
        public int PrevRentDuoAmount { get; set; } = 0;
        public int PrevRentAdvanceAmount { get; set; } = 0;
        public int Active { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public int HouseId { get; set; }
        [ForeignKey("HouseId")]
        public House? House { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}