using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "d600167c-95f8-44a1-a261-71f756cd717c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "5cb2e792-43e7-4edd-963c-fc3ca8e7f911");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "08209722-40d5-4aeb-b81a-0d3c772577a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "5f58c957-3852-4ec1-8135-1561ac14d087");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "lekar1@test.cz", 0, "06c8b417-4734-42cd-b1df-965f5fcb3971", "lekar1@test.cz", false, false, null, "LEKAR1@TEST.CZ", "LEKAR1@TEST.CZ", "AQAAAAEAACcQAAAAEJKugBFrwSqm8YK2CodEnYveXG3qE+n7E8QmTLz3l+ATjgdXkolgb5o0Dzy1dxJB4w==", null, false, "160e493b-e40f-4ece-87c5-a69dab4b0634", false, "lekar1@test.cz" },
                    { "lekar2@test.cz", 0, "fe2d5e8d-b81d-44d0-a127-e9d29a4b9e24", "lekar2@test.cz", false, false, null, "LEKAR2@TEST.CZ", "LEKAR2@TEST.CZ", "AQAAAAEAACcQAAAAEKlOsqTAJqV95C+oZGFzb8iAsui+yAkCNjXOV0/gk7TeTol9lfSXUbusGiwWHYM4bA==", null, false, "96c2cca0-f381-4fe0-9215-58e49ebf0655", false, "lekar2@test.cz" },
                    { "pacient1@test.cz", 0, "04051a53-ae30-412b-a463-9c83d01b3e6f", "pacient1@test.cz", false, false, null, "PACIENT1@TEST.CZ", "PACIENT1@TEST.CZ", "AQAAAAEAACcQAAAAEGHWBszDKcuuWPrlRPiYmjLU3BP9Yra6Lt00ZAjN0jqeAo2p6ttAcFb3vwnOGLmt5A==", null, false, "344712c5-2b34-4606-89d4-10604943175c", false, "pacient1@test.cz" },
                    { "pacient2@test.cz", 0, "ad657c93-4b98-4440-9d2b-c885117f1b92", "pacient2@test.cz", false, false, null, "PACIENT2@TEST.CZ", "PACIENT2@TEST.CZ", "AQAAAAEAACcQAAAAENVR0pB7W3MaHTFuKTe4mZx1OZA80B6vGCEj5gAnXFVmsMiDpH7x19V0SjtgZ3K8ag==", null, false, "db25c98d-e33d-47b3-95c1-8c69002dc55b", false, "pacient2@test.cz" },
                    { "admin@test.cz", 0, "09a04f51-1ffd-4671-ad6b-fbed6775605a", "admin@test.cz", false, false, null, "ADMIN@TEST.CZ", "ADMIN@TEST.CZ", "AQAAAAEAACcQAAAAEO1+NSUU0N6YxkoI6lwIZJtxim0bn8tT3iEY9FMofNbQJV7xtCACOYYW5Rv9nNGz4w==", null, false, "d1e3b601-e11c-4d48-9884-61e822866e34", false, "admin@test.cz" },
                    { "pojistovak@test.cz", 0, "cc01424e-a40b-439b-a30e-6c9a7041c161", "pojistovak@test.cz", false, false, null, "POJISTOVAK@TEST.CZ", "POJISTOVAK@TEST.CZ", "AQAAAAEAACcQAAAAEMg1b9rqiFqFaKGCYQZT6TvY091iHxNft9HVeUzHsLf1L9gYmUhUa7USXZ+5GtLYYQ==", null, false, "68eb9bf4-2cf9-4e92-af01-8734700c73b0", false, "pojistovak@test.cz" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Tomáš", "lekar1@test.cz" },
                    { 13, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Richard", "admin@test.cz" },
                    { 12, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "04.01.1999 0:00:00", "pacient2@test.cz" },
                    { 11, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Novotná", "pacient2@test.cz" },
                    { 10, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Hana", "pacient2@test.cz" },
                    { 18, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "03.02.1995 0:00:00", "pojistovak@test.cz" },
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "17.08.1971 0:00:00", "pacient1@test.cz" },
                    { 14, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Jarůšek", "admin@test.cz" },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Pospíšil", "pacient1@test.cz" },
                    { 16, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Josef", "pojistovak@test.cz" },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "25.10.2010 0:00:00", "lekar2@test.cz" },
                    { 5, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Martan", "lekar2@test.cz" },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Jan", "lekar2@test.cz" },
                    { 17, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Kováčik", "pojistovak@test.cz" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "03.02.1995 0:00:00", "lekar1@test.cz" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Jeroušek", "lekar1@test.cz" },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Jan", "pacient1@test.cz" },
                    { 15, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "15.07.1990 0:00:00", "admin@test.cz" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ADMIN", "admin@test.cz" },
                    { "PATIENT", "pacient1@test.cz" },
                    { "DOCTOR", "lekar2@test.cz" },
                    { "DOCTOR", "lekar1@test.cz" },
                    { "PATIENT", "pacient2@test.cz" },
                    { "INSURANCE", "pojistovak@test.cz" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ADMIN", "admin@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DOCTOR", "lekar1@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DOCTOR", "lekar2@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "PATIENT", "pacient1@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "PATIENT", "pacient2@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "INSURANCE", "pojistovak@test.cz" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "c99bf448-7dbf-4c6b-9d72-2cfa649805f8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "0b3ee6d7-3928-4dc7-96a7-7649f271888c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "d6155c77-7619-412f-b631-fea728fd7806");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "56f42b83-9e24-4a7d-abe9-7698b2669b1d");
        }
    }
}
