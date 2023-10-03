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
    [Migration("20201125154540_StatusTest")]
    partial class StatusTest
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
                            ConcurrencyStamp = "4ff1d8f2-0cd8-4561-a1e8-3d7158f13ff1",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "DOCTOR",
                            ConcurrencyStamp = "ec288297-4c4d-4785-ac32-e4ffaf2cce91",
                            Name = "DOCTOR",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = "INSURANCE",
                            ConcurrencyStamp = "b9ab57c8-46f7-4c0d-8a1b-1c47811f5614",
                            Name = "INSURANCE",
                            NormalizedName = "INSURANCE"
                        },
                        new
                        {
                            Id = "PATIENT",
                            ConcurrencyStamp = "bf78f624-cbda-4934-9b6b-5970fb9ed2f0",
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

                    b.HasData(
                        new
                        {
                            UserId = "lekar1@test.cz",
                            RoleId = "DOCTOR"
                        },
                        new
                        {
                            UserId = "lekar2@test.cz",
                            RoleId = "DOCTOR"
                        },
                        new
                        {
                            UserId = "pacient1@test.cz",
                            RoleId = "PATIENT"
                        },
                        new
                        {
                            UserId = "pacient2@test.cz",
                            RoleId = "PATIENT"
                        },
                        new
                        {
                            UserId = "admin@test.cz",
                            RoleId = "ADMIN"
                        },
                        new
                        {
                            UserId = "pojistovak@test.cz",
                            RoleId = "INSURANCE"
                        });
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

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Surname")
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

                    b.HasData(
                        new
                        {
                            Id = "lekar1@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "90f96101-2eb2-405b-8146-e70c16fb5203",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lekar1@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Tomáš",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEKAR1@TEST.CZ",
                            NormalizedUserName = "LEKAR1@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAENYwABFIQPrfSV51dhlswsgIcBB9jRU3A7DSYKNw6Vlc4PAuGHubIFKSbkH5A7NyyA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "78d4a2ed-95b5-44b0-a770-21f977e3bdd4",
                            Surname = "Jeroušek",
                            TwoFactorEnabled = false,
                            UserName = "lekar1@test.cz"
                        },
                        new
                        {
                            Id = "lekar2@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(2010, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "252b3344-f94a-4e40-aab8-117ab3b92145",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lekar2@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Jan",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEKAR2@TEST.CZ",
                            NormalizedUserName = "LEKAR2@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAENuYJCowomoslpCCZ0P6hy82A04PmYc9wj9psFHjvGwXwd0XiD3MCUkP5kXGeQYndA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "677fbed7-6b2c-4ae1-9bcf-82e4a1f94c68",
                            Surname = "Martan",
                            TwoFactorEnabled = false,
                            UserName = "lekar2@test.cz"
                        },
                        new
                        {
                            Id = "pacient1@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1971, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "b9c4def8-7d46-4c2b-a1f8-8d03089e2e92",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pacient1@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Jan",
                            LockoutEnabled = false,
                            NormalizedEmail = "PACIENT1@TEST.CZ",
                            NormalizedUserName = "PACIENT1@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEE0MG3eNkLtRSn9y3XwrRDljpgfdSGD+fdM7YEa5jIdMDCQ8aRabWUp7sFFfOZcdUg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ca291248-e16a-4d54-9bc4-d65ff95b755e",
                            Surname = "Pospíšil",
                            TwoFactorEnabled = false,
                            UserName = "pacient1@test.cz"
                        },
                        new
                        {
                            Id = "pacient2@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1999, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "d55b4477-58b1-4c4c-990c-83aa425d86d8",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pacient2@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Hana",
                            LockoutEnabled = false,
                            NormalizedEmail = "PACIENT2@TEST.CZ",
                            NormalizedUserName = "PACIENT2@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEHmSMJ3D/saxMphgUUcVDugD3Es5BqBQjVfqWAgH1+cX2aBM0ZZTnnNb0QqSRiK8zA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2985c86e-dbca-4d3d-b7a4-995beb2b0402",
                            Surname = "Novotná",
                            TwoFactorEnabled = false,
                            UserName = "pacient2@test.cz"
                        },
                        new
                        {
                            Id = "admin@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1990, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "2bd1be91-e942-4803-b967-6a0ddd66967f",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Richard",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@TEST.CZ",
                            NormalizedUserName = "ADMIN@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEEHy9Mqa1AxU9R3KfGsLEQEdtSoRtUz2a3QIHD91/+qEhqm9UPg78NbEGSNf7T2AwQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ad7a2440-2448-4930-b6cd-97ab6d025f23",
                            Surname = "Jarůšek",
                            TwoFactorEnabled = false,
                            UserName = "admin@test.cz"
                        },
                        new
                        {
                            Id = "pojistovak@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "5cd00518-85c8-4968-89ca-e2356fff56d8",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pojistovak@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Josef",
                            LockoutEnabled = false,
                            NormalizedEmail = "POJISTOVAK@TEST.CZ",
                            NormalizedUserName = "POJISTOVAK@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAECFYWd3PGhJsrPuxijNmUzkYRucUJ8oKcCpOhG4cPf8OeMVzFLcSRD/EtQ37P6BIRg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "59ddfce4-d788-4672-acbe-801239587e78",
                            Surname = "Kováčik",
                            TwoFactorEnabled = false,
                            UserName = "pojistovak@test.cz"
                        });
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

                    b.Property<DateTime>("DtCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

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

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DtCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MedicalRecordId")
                        .HasColumnType("int");

                    b.Property<int?>("MedicalTicketId")
                        .HasColumnType("int");

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

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DtCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("RecordMedicalRecordId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MedicalTicketId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DoctorId");

                    b.HasIndex("RecordMedicalRecordId");

                    b.ToTable("MedicalTickets");
                });

            modelBuilder.Entity("iis_project.Data.ReportImage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReportMedicalReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportMedicalReportId");

                    b.ToTable("ReportImages");
                });

            modelBuilder.Entity("iis_project.Data.TicketAct", b =>
                {
                    b.Property<int>("MedicalActId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalTicketId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DtCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MedicalActId", "MedicalTicketId");

                    b.HasIndex("MedicalTicketId");

                    b.ToTable("TicketActs");
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
                        .WithMany("Roles")
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
                        .WithMany("MedicalTickets")
                        .HasForeignKey("RecordMedicalRecordId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Doctor");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("iis_project.Data.ReportImage", b =>
                {
                    b.HasOne("iis_project.Data.MedicalReport", "Report")
                        .WithMany("Images")
                        .HasForeignKey("ReportMedicalReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("iis_project.Data.TicketAct", b =>
                {
                    b.HasOne("iis_project.Data.MedicalAct", "MedicalAct")
                        .WithMany("TicketActs")
                        .HasForeignKey("MedicalActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("iis_project.Data.MedicalTicket", "MedicaTicket")
                        .WithMany("TicketActs")
                        .HasForeignKey("MedicalTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalAct");

                    b.Navigation("MedicaTicket");
                });

            modelBuilder.Entity("iis_project.Data.ApplicationUser", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("iis_project.Data.MedicalAct", b =>
                {
                    b.Navigation("TicketActs");
                });

            modelBuilder.Entity("iis_project.Data.MedicalRecord", b =>
                {
                    b.Navigation("MedicalReports");

                    b.Navigation("MedicalTickets");
                });

            modelBuilder.Entity("iis_project.Data.MedicalReport", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("iis_project.Data.MedicalTicket", b =>
                {
                    b.Navigation("MedicalReports");

                    b.Navigation("TicketActs");
                });
#pragma warning restore 612, 618
        }
    }
}