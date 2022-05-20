using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dissertation.Migrations
{
    public partial class AddPlanAndStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analysts");

            migrationBuilder.DropTable(
                name: "CyberSecuritySpecialists");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "FileIncidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "NoDefinition",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NetworkIncidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IpAddrees = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkIncidents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathMapContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(2560)", maxLength: 2560, nullable: false),
                    ResponseToolInfo = table.Column<string>(type: "nvarchar(2560)", maxLength: 2560, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathMapContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlannedResponsePlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Performance = table.Column<TimeSpan>(type: "time", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Modern"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedResponsePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathMapContentPlannedResponsePlan",
                columns: table => new
                {
                    PathMapsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlansId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathMapContentPlannedResponsePlan", x => new { x.PathMapsId, x.PlansId });
                    table.ForeignKey(
                        name: "FK_PathMapContentPlannedResponsePlan_PathMapContents_PathMapsId",
                        column: x => x.PathMapsId,
                        principalTable: "PathMapContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PathMapContentPlannedResponsePlan_PlannedResponsePlans_PlansId",
                        column: x => x.PlansId,
                        principalTable: "PlannedResponsePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaffStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RespondentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRelevance = table.Column<bool>(type: "bit", nullable: false),
                    StatisticsType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Workload = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Neutral"),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffStatistics_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespondentJobSamples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "InAcceptance"),
                    PlanUsageInformation = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlannedResponsePlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaffStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespondentJobSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespondentJobSamples_FileIncidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "FileIncidents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespondentJobSamples_NetworkIncidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "NetworkIncidents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespondentJobSamples_PlannedResponsePlans_PlannedResponsePlanId",
                        column: x => x.PlannedResponsePlanId,
                        principalTable: "PlannedResponsePlans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RespondentJobSamples_StaffStatistics_StaffStatisticId",
                        column: x => x.StaffStatisticId,
                        principalTable: "StaffStatistics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PathMapContentPlannedResponsePlan_PlansId",
                table: "PathMapContentPlannedResponsePlan",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSamples_IncidentId",
                table: "RespondentJobSamples",
                column: "IncidentId",
                unique: true,
                filter: "[IncidentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSamples_PlannedResponsePlanId",
                table: "RespondentJobSamples",
                column: "PlannedResponsePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RespondentJobSamples_StaffStatisticId",
                table: "RespondentJobSamples",
                column: "StaffStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffStatistics_StaffId",
                table: "StaffStatistics",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PathMapContentPlannedResponsePlan");

            migrationBuilder.DropTable(
                name: "RespondentJobSamples");

            migrationBuilder.DropTable(
                name: "PathMapContents");

            migrationBuilder.DropTable(
                name: "NetworkIncidents");

            migrationBuilder.DropTable(
                name: "PlannedResponsePlans");

            migrationBuilder.DropTable(
                name: "StaffStatistics");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "FileIncidents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "NoDefinition");

            migrationBuilder.CreateTable(
                name: "Analysts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CyberSecuritySpecialists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyberSecuritySpecialists", x => x.Id);
                });
        }
    }
}
