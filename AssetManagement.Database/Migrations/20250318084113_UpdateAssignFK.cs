using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id");
        }
    }
}
