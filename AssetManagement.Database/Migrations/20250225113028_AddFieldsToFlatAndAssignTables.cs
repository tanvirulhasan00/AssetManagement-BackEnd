using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToFlatAndAssignTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlatAdvanceAmount",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrevRentAdvanceAmount",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrevRentDuoAmount",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlatAdvanceAmountDue",
                table: "Assigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlatAdvanceAmountGiven",
                table: "Assigns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlatAdvanceAmount",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "PrevRentAdvanceAmount",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "PrevRentDuoAmount",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "FlatAdvanceAmountDue",
                table: "Assigns");

            migrationBuilder.DropColumn(
                name: "FlatAdvanceAmountGiven",
                table: "Assigns");
        }
    }
}
