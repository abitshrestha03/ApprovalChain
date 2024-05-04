using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApprovalChain.Migrations
{
    /// <inheritdoc />
    public partial class initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeHandler",
                table: "ArcDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeHandler",
                table: "ArcDocuments");
        }
    }
}
