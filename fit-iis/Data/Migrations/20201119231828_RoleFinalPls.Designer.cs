﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iis_project.Data;

namespace iis_project.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201119231828_RoleFinalPls")]
    partial class RoleFinalPls
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "ADMIN",
                            ConcurrencyStamp = "c99bf448-7dbf-4c6b-9d72-2cfa649805f8",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "DOCTOR",
                            ConcurrencyStamp = "0b3ee6d7-3928-4dc7-96a7-7649f271888c",
                            Name = "DOCTOR",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = "INSURANCE",
                            ConcurrencyStamp = "d6155c77-7619-412f-b631-fea728fd7806",
                            Name = "INSURANCE",
                            NormalizedName = "INSURANCE"
                        },
                        new
                        {
                            Id = "PATIENT",
                            ConcurrencyStamp = "56f42b83-9e24-4a7d-abe9-7698b2669b1d",
                            Name = "PATIENT",
                            NormalizedName = "PATIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("iis_project.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("iis_project.Data.MedicalAct", b =>
                {
                    b.Property<int>("MedicalActId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MedicalActId");

                    b.ToTable("InsuranceActs");
                });

            modelBuilder.Entity("iis_project.Data.MedicalRecord", b =>
                {
                    b.Property<int>("MedicalRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicalRecordId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("iis_project.Data.MedicalReport", b =>
                {
                    b.Property<int>("MedicalReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MedicalRecordId")
                        .HasColumnType("int");

                    b.Property<int?>("MedicalTicketId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicalReportId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("MedicalRecordId");

                    b.HasIndex("MedicalTicketId");

                    b.ToTable("MedicalReports");
                });

            modelBuilder.Entity("iis_project.Data.MedicalTicket", b =>
                {
                    b.Property<int>("MedicalTicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("RecordMedicalRecordId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicalTicketId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DoctorId");

                    b.HasIndex("RecordMedicalRecordId");

                    b.ToTable("MedicalTickets");
                });

            modelBuilder.Entity("iis_project.Data.ReportAct", b =>
                {
                    b.Property<int>("MedicalActId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalReportId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicalActId", "MedicalReportId");

                    b.HasIndex("MedicalReportId");

                    b.ToTable("ReportAct");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iis_project.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("iis_project.Data.MedicalRecord", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("iis_project.Data.ApplicationUser", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("iis_project.Data.MedicalReport", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("iis_project.Data.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalReports")
                        .HasForeignKey("MedicalRecordId");

                    b.HasOne("iis_project.Data.MedicalTicket", "MedicalTicket")
                        .WithMany("MedicalReports")
                        .HasForeignKey("MedicalTicketId");

                    b.Navigation("CreatedBy");

                    b.Navigation("MedicalRecord");

                    b.Navigation("MedicalTicket");
                });

            modelBuilder.Entity("iis_project.Data.MedicalTicket", b =>
                {
                    b.HasOne("iis_project.Data.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("iis_project.Data.ApplicationUser", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("iis_project.Data.MedicalRecord", "Record")
                        .WithMany()
                        .HasForeignKey("RecordMedicalRecordId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Doctor");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("iis_project.Data.ReportAct", b =>
                {
                    b.HasOne("iis_project.Data.MedicalAct", "MedicalAct")
                        .WithMany("ReportActs")
                        .HasForeignKey("MedicalActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iis_project.Data.MedicalReport", "MedicalReport")
                        .WithMany("ReportActs")
                        .HasForeignKey("MedicalReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalAct");

                    b.Navigation("MedicalReport");
                });

            modelBuilder.Entity("iis_project.Data.MedicalAct", b =>
                {
                    b.Navigation("ReportActs");
                });

            modelBuilder.Entity("iis_project.Data.MedicalRecord", b =>
                {
                    b.Navigation("MedicalReports");
                });

            modelBuilder.Entity("iis_project.Data.MedicalReport", b =>
                {
                    b.Navigation("ReportActs");
                });

            modelBuilder.Entity("iis_project.Data.MedicalTicket", b =>
                {
                    b.Navigation("MedicalReports");
                });
#pragma warning restore 612, 618
        }
    }
}
