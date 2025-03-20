using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class PreventCasecadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments");

            migrationBuilder.AlterColumn<long>(
                name: "AssignId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments");

            migrationBuilder.AlterColumn<long>(
                name: "AssignId",
                table: "Payments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id");
        }
    }
}
