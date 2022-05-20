﻿// <auto-generated />
using System;
using Dissertation.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dissertation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FileIncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Md5")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Sha1")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Sha256")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.HasKey("Id");

                    b.HasIndex("FileIncidentId")
                        .IsUnique();

                    b.HasIndex("Sha256")
                        .HasFilter("[Sha256] IS NOT NULL");

                    b.ToTable("FileDetails", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileIncident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Domain")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("IpAddrees")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsSystemScanClean")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("NoDefinition");

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique()
                        .HasFilter("[FileName] IS NOT NULL");

                    b.ToTable("FileIncidents", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.NetworkIncident", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Domain")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("IpAddrees")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NetworkIncidents", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.PathMapContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResponseToolInfo")
                        .IsRequired()
                        .HasMaxLength(2560)
                        .HasColumnType("nvarchar(2560)");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(2560)
                        .HasColumnType("nvarchar(2560)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("PathMapContents", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.PlannedResponsePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Performance")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Modern");

                    b.HasKey("Id");

                    b.ToTable("PlannedResponsePlans", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.RespondentJobSample", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("IncidentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlanUsageInformation")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid?>("PlannedResponsePlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StaffStatisticId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("InAcceptance");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId")
                        .IsUnique()
                        .HasFilter("[IncidentId] IS NOT NULL");

                    b.HasIndex("PlannedResponsePlanId");

                    b.HasIndex("StaffStatisticId");

                    b.ToTable("RespondentJobSamples", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Staffs", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.StaffStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRelevance")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RespondentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatisticsType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Workload")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Neutral");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("StaffStatistics", (string)null);
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.VirusTotalReportDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FileDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JsonContent")
                        .HasMaxLength(3584)
                        .HasColumnType("nvarchar(3584)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Permalink")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Resource")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ScanId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FileDetailsId")
                        .IsUnique();

                    b.HasIndex("Resource")
                        .HasFilter("[Resource] IS NOT NULL");

                    b.ToTable("VirusTotalReportDetails", (string)null);
                });

            modelBuilder.Entity("PathMapContentPlannedResponsePlan", b =>
                {
                    b.Property<Guid>("PathMapsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlansId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PathMapsId", "PlansId");

                    b.HasIndex("PlansId");

                    b.ToTable("PathMapContentPlannedResponsePlan");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileDetails", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.FileIncident", "Incident")
                        .WithOne("Details")
                        .HasForeignKey("Dissertation.Persistence.Entities.FileDetails", "FileIncidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.RespondentJobSample", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.FileIncident", "FileIncident")
                        .WithOne("JobSample")
                        .HasForeignKey("Dissertation.Persistence.Entities.RespondentJobSample", "IncidentId");

                    b.HasOne("Dissertation.Persistence.Entities.NetworkIncident", "NetworkIncident")
                        .WithOne("JobSample")
                        .HasForeignKey("Dissertation.Persistence.Entities.RespondentJobSample", "IncidentId");

                    b.HasOne("Dissertation.Persistence.Entities.PlannedResponsePlan", "PlannedResponsePlan")
                        .WithMany("RespondentJobSamples")
                        .HasForeignKey("PlannedResponsePlanId");

                    b.HasOne("Dissertation.Persistence.Entities.StaffStatistic", "StaffStatistic")
                        .WithMany("JobSamples")
                        .HasForeignKey("StaffStatisticId");

                    b.Navigation("FileIncident");

                    b.Navigation("NetworkIncident");

                    b.Navigation("PlannedResponsePlan");

                    b.Navigation("StaffStatistic");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.StaffStatistic", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.Staff", "Staff")
                        .WithMany("Statistics")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.VirusTotalReportDetails", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.FileDetails", "FileDetails")
                        .WithOne("Report")
                        .HasForeignKey("Dissertation.Persistence.Entities.VirusTotalReportDetails", "FileDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileDetails");
                });

            modelBuilder.Entity("PathMapContentPlannedResponsePlan", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.PathMapContent", null)
                        .WithMany()
                        .HasForeignKey("PathMapsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dissertation.Persistence.Entities.PlannedResponsePlan", null)
                        .WithMany()
                        .HasForeignKey("PlansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileDetails", b =>
                {
                    b.Navigation("Report");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileIncident", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("JobSample");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.NetworkIncident", b =>
                {
                    b.Navigation("JobSample");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.PlannedResponsePlan", b =>
                {
                    b.Navigation("RespondentJobSamples");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.Staff", b =>
                {
                    b.Navigation("Statistics");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.StaffStatistic", b =>
                {
                    b.Navigation("JobSamples");
                });
#pragma warning restore 612, 618
        }
    }
}
