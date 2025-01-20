using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldAndRemoveEmergencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "DriverAddress",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "DriverNidNumber",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "DriverPhnNumber",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "SpouseAddress",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "SpouseName",
                table: "Renters");

            migrationBuilder.DropColumn(
                name: "SpouseNidNumber",
                table: "Renters");

            migrationBuilder.RenameColumn(
                name: "SpousePhnNumber",
                table: "Renters",
                newName: "NidImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "NidNumber",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsEmergencyContact",
                table: "FamilyMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NidImageUrl",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NidNumber",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relation",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "IsEmergencyContact",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "NidImageUrl",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "NidNumber",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "Relation",
                table: "FamilyMembers");

            migrationBuilder.RenameColumn(
                name: "NidImageUrl",
                table: "Renters",
                newName: "SpousePhnNumber");

            migrationBuilder.AlterColumn<string>(
                name: "NidNumber",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DriverAddress",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverNidNumber",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverPhnNumber",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseAddress",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseName",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseNidNumber",
                table: "Renters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_RenterId",
                table: "EmergencyContacts",
                column: "RenterId");
        }
    }
}
