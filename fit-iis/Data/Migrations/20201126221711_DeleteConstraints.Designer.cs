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
    [Migration("20201126221711_DeleteConstraints")]
    partial class DeleteConstraints
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
                            ConcurrencyStamp = "3d72bcd5-78e4-40cc-b0f0-aa3daca29632",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "DOCTOR",
                            ConcurrencyStamp = "bf8fa3cd-1c83-40c6-9028-560e0d01eead",
                            Name = "DOCTOR",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = "INSURANCE",
                            ConcurrencyStamp = "134a69ae-a3ba-4a50-b9a3-ec3d39db7821",
                            Name = "INSURANCE",
                            NormalizedName = "INSURANCE"
                        },
                        new
                        {
                            Id = "PATIENT",
                            ConcurrencyStamp = "89092610-0a78-44c4-b153-65ca01c71a9b",
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
                            ConcurrencyStamp = "b70bff88-a895-476d-b7e2-9e0cd51faecf",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lekar1@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Tomáš",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEKAR1@TEST.CZ",
                            NormalizedUserName = "LEKAR1@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEHWT3FH87L7G6CJzZtcNZNjDYA7IY7CVIXZc0M0/RqYZioP0XuoVWkfsxrBTd69UBw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "374d8c60-8c56-4934-994e-a332928971c6",
                            Surname = "Jeroušek",
                            TwoFactorEnabled = false,
                            UserName = "lekar1@test.cz"
                        },
                        new
                        {
                            Id = "lekar2@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(2010, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "884b0c75-9556-4c52-ae22-5853f9c2047a",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lekar2@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Jan",
                            LockoutEnabled = false,
                            NormalizedEmail = "LEKAR2@TEST.CZ",
                            NormalizedUserName = "LEKAR2@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAED/yEr5qeB8W7t17eR5qxIRSJZLZpQWodxrEeX29Zzd747HagITnqx2KLZNY3LxS+A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5928af22-6ff9-40e6-9b91-ded34439e6a9",
                            Surname = "Martan",
                            TwoFactorEnabled = false,
                            UserName = "lekar2@test.cz"
                        },
                        new
                        {
                            Id = "pacient1@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1971, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "0f85eb68-e79b-49fc-9c48-6f345f2c93df",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pacient1@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Jan",
                            LockoutEnabled = false,
                            NormalizedEmail = "PACIENT1@TEST.CZ",
                            NormalizedUserName = "PACIENT1@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEMge3phYLoZwpB/XRmj9HzrbJuZiB7M6frQBhRb3+T8LOZu0GbHDYruJX7BhTwnLMQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4eea10f9-9bc7-470a-bfe9-ce0a3cfc0a86",
                            Surname = "Pospíšil",
                            TwoFactorEnabled = false,
                            UserName = "pacient1@test.cz"
                        },
                        new
                        {
                            Id = "pacient2@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1999, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "dd3d0a0c-200b-42c3-89e1-5a7b1152744a",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pacient2@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Hana",
                            LockoutEnabled = false,
                            NormalizedEmail = "PACIENT2@TEST.CZ",
                            NormalizedUserName = "PACIENT2@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEGUvwTao6x/3otz2h2RsXz7ZBbGmR64UT8JOTiMR6suqmLJUsi7sPaGpCcFjfEIr/w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "632ecf9d-1b8c-464b-9855-7536f111c11f",
                            Surname = "Novotná",
                            TwoFactorEnabled = false,
                            UserName = "pacient2@test.cz"
                        },
                        new
                        {
                            Id = "admin@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1990, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "26425871-f5c6-4cd0-a223-8d08031d53f4",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Richard",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@TEST.CZ",
                            NormalizedUserName = "ADMIN@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAENqcbkcWqjd721V81/fIrktVNQEtNmgGxXp294Q8voo/aRJHGmxhWpCcwhcOf3iV7g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "946a8ca1-dfc5-454e-9b11-a7962e8b1ff2",
                            Surname = "Jarůšek",
                            TwoFactorEnabled = false,
                            UserName = "admin@test.cz"
                        },
                        new
                        {
                            Id = "pojistovak@test.cz",
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "212e2a1d-1a7f-493f-9bba-be828eceabe0",
                            DtCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "pojistovak@test.cz",
                            EmailConfirmed = false,
                            GivenName = "Josef",
                            LockoutEnabled = false,
                            NormalizedEmail = "POJISTOVAK@TEST.CZ",
                            NormalizedUserName = "POJISTOVAK@TEST.CZ",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ2lmQ80KVvb2oX2UjUh5t7sYdAsFQ6z1qyw7JX86gIQzsz0v7uJuTmm7qPTRva3Gg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b9530cff-c8e3-4657-94a9-c53005ec589d",
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

                    b.Property<int>("RecordMedicalRecordId")
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

                    b.Property<int>("ReportMedicalReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportMedicalReportId");

                    b.ToTable("ReportImages");
                });

            modelBuilder.Entity("iis_project.Data.TicketAct", b =>
                {
                    b.Property<int>("TicketActId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DtCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("MedicalActId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalTicketId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("TicketActId");

                    b.HasIndex("MedicalActId");

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
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("iis_project.Data.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalReports")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("iis_project.Data.MedicalTicket", "MedicalTicket")
                        .WithMany("MedicalReports")
                        .HasForeignKey("MedicalTicketId")
                        .OnDelete(DeleteBehavior.Cascade);

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
                        .HasForeignKey("RecordMedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Doctor");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("iis_project.Data.ReportImage", b =>
                {
                    b.HasOne("iis_project.Data.MedicalReport", "Report")
                        .WithMany("Images")
                        .HasForeignKey("ReportMedicalReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("iis_project.Data.TicketAct", b =>
                {
                    b.HasOne("iis_project.Data.MedicalAct", "MedicalAct")
                        .WithMany("TicketActs")
                        .HasForeignKey("MedicalActId")
                        .OnDelete(DeleteBehavior.Restrict)
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
