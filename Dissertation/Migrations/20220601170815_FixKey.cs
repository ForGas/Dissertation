using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class FixKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespondentJobSamples_NetworkIncidents_IncidentId",
                table: "RespondentJobSamples");

            migrationBuilder.AddColumn<Guid>(
                name: "NetworkIncidentId",
                table: "RespondentJobSamples",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSamples_NetworkIncidentId",
                table: "RespondentJobSamples",
                column: "NetworkIncidentId",
                unique: true,
                filter: "[NetworkIncidentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RespondentJobSamples_NetworkIncidents_NetworkIncidentId",
                table: "RespondentJobSamples",
                column: "NetworkIncidentId",
                principalTable: "NetworkIncidents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespondentJobSamples_NetworkIncidents_NetworkIncidentId",
                table: "RespondentJobSamples");

            migrationBuilder.DropIndex(
                name: "IX_RespondentJobSamples_NetworkIncidentId",
                table: "RespondentJobSamples");

            migrationBuilder.DropColumn(
                name: "NetworkIncidentId",
                table: "RespondentJobSamples");

            migrationBuilder.AddForeignKey(
                name: "FK_RespondentJobSamples_NetworkIncidents_IncidentId",
                table: "RespondentJobSamples",
                column: "IncidentId",
                principalTable: "NetworkIncidents",
                principalColumn: "Id");
        }
    }
}
