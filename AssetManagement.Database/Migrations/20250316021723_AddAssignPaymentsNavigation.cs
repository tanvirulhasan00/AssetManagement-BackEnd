using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignPaymentsNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments");

            migrationBuilder.CreateTable(
                name: "MonthlyPaymentStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignId = table.Column<long>(type: "bigint", nullable: false),
                    January = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    February = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    March = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    April = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    May = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    June = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    July = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    August = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    September = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    October = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    November = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    December = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyPaymentStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyPaymentStatuses_Assigns_AssignId",
                        column: x => x.AssignId,
                        principalTable: "Assigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyPaymentStatuses_AssignId",
                table: "MonthlyPaymentStatuses",
                column: "AssignId");

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

            migrationBuilder.DropTable(
                name: "MonthlyPaymentStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Assigns_AssignId",
                table: "Payments",
                column: "AssignId",
                principalTable: "Assigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
