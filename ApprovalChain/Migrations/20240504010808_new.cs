using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApprovalChain.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ArcDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ArcDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArcDocuments_EmployeeId",
                table: "ArcDocuments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArcDocuments_Employees_EmployeeId",
                table: "ArcDocuments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArcDocuments_Employees_EmployeeId",
                table: "ArcDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ArcDocuments_EmployeeId",
                table: "ArcDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ArcDocuments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ArcDocuments");
        }
    }
}
