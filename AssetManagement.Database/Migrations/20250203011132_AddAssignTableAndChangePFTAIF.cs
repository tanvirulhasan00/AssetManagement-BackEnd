using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignTableAndChangePFTAIF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flats");

            migrationBuilder.AddColumn<long>(
                name: "AssignedId",
                table: "Flats",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NidNumber",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assigns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<long>(type: "bigint", nullable: false),
                    FlatId = table.Column<long>(type: "bigint", nullable: false),
                    FlatPrice = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assigns_Flats_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assigns_Renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_FlatId",
                table: "Assigns",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_RenterId",
                table: "Assigns",
                column: "RenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assigns");

            migrationBuilder.DropColumn(
                name: "AssignedId",
                table: "Flats");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Flats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "NidNumber",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
