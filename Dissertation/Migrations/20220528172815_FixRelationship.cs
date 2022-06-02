using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class FixRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespondentJobSamples_StaffStatistics_StaffStatisticId",
                table: "RespondentJobSamples");

            migrationBuilder.DropIndex(
                name: "IX_RespondentJobSamples_StaffStatisticId",
                table: "RespondentJobSamples");

            migrationBuilder.DropColumn(
                name: "StaffStatisticId",
                table: "RespondentJobSamples");

            migrationBuilder.AddColumn<int>(
                name: "IncidentType",
                table: "PlannedResponsePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "PlannedResponsePlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PathMapContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Stage",
                table: "PathMapContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Initial");

            migrationBuilder.AddColumn<bool>(
                name: "IsVirusHashInfoClean",
                table: "FileIncidents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "RespondentJobSampleStaffStatistic",
                columns: table => new
                {
                    JobSamplesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffStatisticsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespondentJobSampleStaffStatistic", x => new { x.JobSamplesId, x.StaffStatisticsId });
                    table.ForeignKey(
                        name: "FK_RespondentJobSampleStaffStatistic_RespondentJobSamples_JobSamplesId",
                        column: x => x.JobSamplesId,
                        principalTable: "RespondentJobSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespondentJobSampleStaffStatistic_StaffStatistics_StaffStatisticsId",
                        column: x => x.StaffStatisticsId,
                        principalTable: "StaffStatistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSampleStaffStatistic_StaffStatisticsId",
                table: "RespondentJobSampleStaffStatistic",
                column: "StaffStatisticsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespondentJobSampleStaffStatistic");

            migrationBuilder.DropColumn(
                name: "IncidentType",
                table: "PlannedResponsePlans");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "PlannedResponsePlans");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PathMapContents");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "PathMapContents");

            migrationBuilder.DropColumn(
                name: "IsVirusHashInfoClean",
                table: "FileIncidents");

            migrationBuilder.AddColumn<Guid>(
                name: "StaffStatisticId",
                table: "RespondentJobSamples",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSamples_StaffStatisticId",
                table: "RespondentJobSamples",
                column: "StaffStatisticId");

            migrationBuilder.AddForeignKey(
                name: "FK_RespondentJobSamples_StaffStatistics_StaffStatisticId",
                table: "RespondentJobSamples",
                column: "StaffStatisticId",
                principalTable: "StaffStatistics",
                principalColumn: "Id");
        }
    }
}
