using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeFkIsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Districts_DistrictId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Divisions_DivisionId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Flats_FlatId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Renters_RenterId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Renters_RenterId",
                table: "FamilyMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Categories_CategoryId",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Houses_HouseId",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Areas_AreaId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyPaymentStatuses_Assigns_AssignId",
                table: "MonthlyPaymentStatuses");

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

            migrationBuilder.AlterColumn<long>(
                name: "AssignId",
                table: "MonthlyPaymentStatuses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Houses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Flats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Flats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "RenterId",
                table: "FamilyMembers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "RenterId",
                table: "Assigns",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "FlatId",
                table: "Assigns",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "Areas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Areas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Districts_DistrictId",
                table: "Areas",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Divisions_DivisionId",
                table: "Areas",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Flats_FlatId",
                table: "Assigns",
                column: "FlatId",
                principalTable: "Flats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Renters_RenterId",
                table: "Assigns",
                column: "RenterId",
                principalTable: "Renters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Renters_RenterId",
                table: "FamilyMembers",
                column: "RenterId",
                principalTable: "Renters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Categories_CategoryId",
                table: "Flats",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Houses_HouseId",
                table: "Flats",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Areas_AreaId",
                table: "Houses",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyPaymentStatuses_Assigns_AssignId",
                table: "MonthlyPaymentStatuses",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Districts_DistrictId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Divisions_DivisionId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Flats_FlatId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Assigns_Renters_RenterId",
                table: "Assigns");

            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Renters_RenterId",
                table: "FamilyMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Categories_CategoryId",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Houses_HouseId",
                table: "Flats");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Areas_AreaId",
                table: "Houses");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyPaymentStatuses_Assigns_AssignId",
                table: "MonthlyPaymentStatuses");

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

            migrationBuilder.AlterColumn<long>(
                name: "AssignId",
                table: "MonthlyPaymentStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HouseId",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RenterId",
                table: "FamilyMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RenterId",
                table: "Assigns",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FlatId",
                table: "Assigns",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DivisionId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Districts_DistrictId",
                table: "Areas",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Divisions_DivisionId",
                table: "Areas",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Flats_FlatId",
                table: "Assigns",
                column: "FlatId",
                principalTable: "Flats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assigns_Renters_RenterId",
                table: "Assigns",
                column: "RenterId",
                principalTable: "Renters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Renters_RenterId",
                table: "FamilyMembers",
                column: "RenterId",
                principalTable: "Renters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Categories_CategoryId",
                table: "Flats",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Houses_HouseId",
                table: "Flats",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Areas_AreaId",
                table: "Houses",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyPaymentStatuses_Assigns_AssignId",
                table: "MonthlyPaymentStatuses",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
