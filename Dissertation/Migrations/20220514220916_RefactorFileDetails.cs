using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class RefactorFileDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permalink",
                table: "VirusTotalReportDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resource",
                table: "VirusTotalReportDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScanId",
                table: "VirusTotalReportDetails",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Md5",
                table: "FileDetails",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sha1",
                table: "FileDetails",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permalink",
                table: "VirusTotalReportDetails");

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "VirusTotalReportDetails");

            migrationBuilder.DropColumn(
                name: "ScanId",
                table: "VirusTotalReportDetails");

            migrationBuilder.DropColumn(
                name: "Md5",
                table: "FileDetails");

            migrationBuilder.DropColumn(
                name: "Sha1",
                table: "FileDetails");
        }
    }
}
