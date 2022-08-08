using Microsoft.EntityFrameworkCore.Migrations;

namespace RiseTechnology.Report.API.Migrations
{
    public partial class ReportFilePathAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XLSXPath",
                schema: "RiseReport",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XLSXPath",
                schema: "RiseReport",
                table: "Report");
        }
    }
}
