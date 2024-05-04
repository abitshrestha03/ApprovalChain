using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApprovalChain.Migrations
{
    /// <inheritdoc />
    public partial class new0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ArcDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ArcDocuments");
        }
    }
}
