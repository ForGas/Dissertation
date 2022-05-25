using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class RefactorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IpAddrees",
                table: "NetworkIncidents",
                newName: "IpAddress");

            migrationBuilder.RenameColumn(
                name: "IpAddrees",
                table: "FileIncidents",
                newName: "IpAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "NetworkIncidents",
                newName: "IpAddrees");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "FileIncidents",
                newName: "IpAddrees");
        }
    }
}
