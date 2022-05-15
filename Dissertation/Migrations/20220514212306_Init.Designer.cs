﻿// <auto-generated />
using System;
using Dissertation.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dissertation.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220514212306_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Dissertation.Persistence.Entities.Analyst", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Analysts");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.CyberSecuritySpecialist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CyberSecuritySpecialists");
                });

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

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("FileName")
                        .IsUnique()
                        .HasFilter("[FileName] IS NOT NULL");

                    b.ToTable("FileIncidents", (string)null);
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

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FileDetailsId")
                        .IsUnique();

                    b.ToTable("VirusTotalReportDetails", (string)null);
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

            modelBuilder.Entity("Dissertation.Persistence.Entities.VirusTotalReportDetails", b =>
                {
                    b.HasOne("Dissertation.Persistence.Entities.FileDetails", "FileDetails")
                        .WithOne("Report")
                        .HasForeignKey("Dissertation.Persistence.Entities.VirusTotalReportDetails", "FileDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileDetails");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileDetails", b =>
                {
                    b.Navigation("Report");
                });

            modelBuilder.Entity("Dissertation.Persistence.Entities.FileIncident", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
