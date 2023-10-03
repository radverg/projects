/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iis_project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<MedicalReport> MedicalReports { get; set; }
        public virtual DbSet<MedicalTicket> MedicalTickets { get; set; }
        public virtual DbSet<MedicalAct> InsuranceActs { get; set; }
        public virtual DbSet<TicketAct> TicketActs { get; set; }
        public virtual DbSet<ReportImage> ReportImages { get; set; }
        public object MedicalTicket { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TicketAct>()
                .Property(t => t.DtCreated)
                .HasDefaultValueSql("getdate()");

            builder.Entity<MedicalRecord>(e =>
            {
                e.Property(t => t.DtCreated)
                    .HasDefaultValueSql("getdate()");

                e.HasMany(mr => mr.MedicalTickets)
                    .WithOne(mr => mr.Record)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                //e.HasOne(mr => mr.Patient)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);
                //
                //e.HasOne(mr => mr.Doctor)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<MedicalReport>(e =>
            {
                e.Property(t => t.DtCreated)
                    .HasDefaultValueSql("getdate()");

                e.HasMany(r => r.Images)
                    .WithOne(im => im.Report)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(r => r.CreatedBy)
                    .WithMany()
                    .OnDelete(DeleteBehavior.SetNull);

                e.HasOne(r => r.MedicalTicket)
                    .WithMany(t => t.MedicalReports)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(r => r.MedicalRecord)
                    .WithMany(mr => mr.MedicalReports)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<MedicalTicket>(e =>
            {
                e.Property(t => t.DtCreated)
                    .HasDefaultValueSql("getdate()");

                //e.HasOne(t => t.Doctor)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);
                //
                //e.HasOne(t => t.CreatedBy)
                //    .WithMany()
                //    .OnDelete(DeleteBehavior.SetNull);
            });


            builder.Entity<ApplicationUser>(b =>
            {
                b.HasMany(e => e.Roles)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();
            });

            builder.Entity<TicketAct>()
                .HasOne(ra => ra.MedicalAct)
                .WithMany(a => a.TicketActs)
                .HasForeignKey(ra => ra.MedicalActId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketAct>()
                .HasOne(ra => ra.MedicaTicket)
                .WithMany(a => a.TicketActs)
                .HasForeignKey(ra => ra.MedicalTicketId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Seed default roles
            var roles = RolesMetadata.RolesNameMap.Keys.Select(r => new IdentityRole(r) { Id = r, NormalizedName = r });
            builder.Entity<IdentityRole>().HasData(roles);

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            // Seed test users - create three for each role
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<IdentityUserRole<string>> rolemap = new List<IdentityUserRole<string>>();

            var seeddata = new[]
            {
                new { 
                    Email = "lekar1@test.cz", 
                    Role = RolesMetadata.DOCTOR, 
                    Firstname = "Tomáš", 
                    Surname = "Jeroušek", 
                    BirthDate = new DateTime(1995, 2, 3) 
                },
                new { 
                    Email = "lekar2@test.cz", 
                    Role = RolesMetadata.DOCTOR, 
                    Firstname = "Jan", 
                    Surname = "Martan", 
                    BirthDate = new DateTime(2010, 10, 25) 
                },
                new { 
                    Email = "pacient1@test.cz", 
                    Role = RolesMetadata.PATIENT, 
                    Firstname = "Jan", 
                    Surname = "Pospíšil", 
                    BirthDate = new DateTime(1971, 8, 17) 
                },
                new { 
                    Email = "pacient2@test.cz", 
                    Role = RolesMetadata.PATIENT, 
                    Firstname = "Hana", 
                    Surname = "Novotná", 
                    BirthDate = new DateTime(1999, 1, 4)
                },
                new { 
                    Email = "admin@test.cz", 
                    Role = RolesMetadata.ADMIN, 
                    Firstname = "Richard", 
                    Surname = "Jarůšek", 
                    BirthDate = new DateTime(1990, 7, 15) 
                },
                new { 
                    Email = "pojistovak@test.cz", 
                    Role = RolesMetadata.INSURANCE_EMPLOYEE, 
                    Firstname = "Josef", 
                    Surname = "Kováčik", 
                    BirthDate = new DateTime(1995, 2, 3) 
                },
            }.ToList();

            seeddata.ForEach(d =>
            {
                var u = new ApplicationUser()
                {
                    Email = d.Email,
                    NormalizedEmail = d.Email.ToUpper(),
                    Id = d.Email,
                    UserName = d.Email,
                    NormalizedUserName = d.Email.ToUpper(),
                    Surname = d.Surname,
                    GivenName = d.Firstname,
                    BirthDate = d.BirthDate
                };

                u.PasswordHash = passwordHasher.HashPassword(u, "iisiis");

                users.Add(u);
                rolemap.Add(new IdentityUserRole<string>() { RoleId = d.Role, UserId = d.Email });
            });

            builder.Entity<ApplicationUser>().HasData(users);
            builder.Entity<IdentityUserRole<string>>().HasData(rolemap);


            //builder.Entity<MedicalRecord>().HasData(
            //    new []
            //    {
            //        new MedicalRecord()
            //        {
            //            Description = "Toto je zdravotní záznam číslo 1.",
            //            
            //        }
            //    }
            //);




        }




}
}
