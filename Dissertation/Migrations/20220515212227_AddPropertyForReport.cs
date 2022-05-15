using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class AddPropertyForReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JsonContent",
                table: "VirusTotalReportDetails",
                type: "nvarchar(3584)",
                maxLength: 3584,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirusTotalReportDetails_Resource",
                table: "VirusTotalReportDetails",
                column: "Resource",
                filter: "[Resource] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VirusTotalReportDetails_Resource",
                table: "VirusTotalReportDetails");

            migrationBuilder.DropColumn(
                name: "JsonContent",
                table: "VirusTotalReportDetails");
        }
    }
}
