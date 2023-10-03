using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class ReworkedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "2e7f2342-7359-4fb6-9c5c-c9fa0f7bdde6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "49c5b05a-ee16-40c9-b219-15fe4c2819b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "363a608f-cc58-4a04-94cc-e284024ae2f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "83b797e0-101b-42c1-a942-05234a05a298");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8ef1dd3-06cb-4eac-ba22-dbf206c36ae9", "AQAAAAEAACcQAAAAEDSjD2aabHaSDfZkpxJsQveslA9e2EmJzPecBlxbqHS4fsR41yG4Z7OhHQKAGC+E5Q==", "25607af9-830d-4ebd-af2a-4c330e409d94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dbb46fe9-5d23-4050-b06d-8e85c8d6d727", "AQAAAAEAACcQAAAAEIHiiaX2Ay0Fuz9jLkKbkNGaG3kLSowms5TidHjOe0bGY9rvJVGqQF/YG2wLEpx68Q==", "4dc5fbfa-9205-4d2a-8432-b963faade517" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a843eb12-a963-4e22-8fa8-3506b02cef22", "AQAAAAEAACcQAAAAEEA5pSiDj+QG1gLq6+H1ORS+3sAuNrB17TuL8YO/FABaerpZ3KeZ83Y3AH/wNtzr8w==", "5572055e-0bbf-4ac1-9845-5efb39ad7b55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04e0209a-5284-42b9-8469-beee41206af8", "AQAAAAEAACcQAAAAEDJ2Gb629fnEEykUqljiVQbHPwFWlNI7IJFZRcXY5R0rrYDa6JgsZ/HUD79brRTRxA==", "b2a3d58e-ee78-42ca-927f-f9c8ba8f476e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c4e4e5b-051f-4c0c-af7b-d69961fd600c", "AQAAAAEAACcQAAAAEEDVS6CqjE4lur4O6kfAPc7SRuVx+9+ILnpJ7cD4cGCqbLylcThuVoEoge4JgGtSJA==", "28cece27-4987-463b-9cd3-318b1e1a1a46" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ae062db-ef11-4953-a371-e13fb57edc1e", "AQAAAAEAACcQAAAAEM0tIX1eY3kCln8WoHNBeBphS7kCZhPvgY1l6ZSE3ThAumlj/ucM8unfcO4h+ppPrw==", "06a2058c-651b-4866-8652-16a0284538f4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DtCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "e85556ad-a4de-4999-b4c3-530e46cac1ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "61fb69d2-0270-4769-8a7f-d360c062eb20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "1f74d015-b419-45ea-ad1c-7bf783ed5024");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "3864b5b5-09cf-4902-8581-7336c0f84f57");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Tomáš", "lekar1@test.cz" },
                    { 18, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "03.02.1995 0:00:00", "pojistovak@test.cz" },
                    { 17, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Kováčik", "pojistovak@test.cz" },
                    { 16, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Josef", "pojistovak@test.cz" },
                    { 15, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "15.07.1990 0:00:00", "admin@test.cz" },
                    { 13, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Richard", "admin@test.cz" },
                    { 12, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "04.01.1999 0:00:00", "pacient2@test.cz" },
                    { 14, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Jarůšek", "admin@test.cz" },
                    { 10, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Hana", "pacient2@test.cz" },
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "17.08.1971 0:00:00", "pacient1@test.cz" },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Pospíšil", "pacient1@test.cz" },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Jan", "pacient1@test.cz" },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "25.10.2010 0:00:00", "lekar2@test.cz" },
                    { 5, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Martan", "lekar2@test.cz" },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "Jan", "lekar2@test.cz" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", "03.02.1995 0:00:00", "lekar1@test.cz" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Jeroušek", "lekar1@test.cz" },
                    { 11, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "Novotná", "pacient2@test.cz" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bda0243d-f985-4ca6-a31e-47bb8543d8d0", "AQAAAAEAACcQAAAAECvFvVQzgm+K/hf15DsahgzH53pv8LPUQprMIUGln6GMHPNu4btD4Zm8+y6JP7kr3w==", "5340a436-8056-4c2c-a16a-21e4c198c760" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0eb8118c-1181-476d-8043-ee0af86a3f51", "AQAAAAEAACcQAAAAEOKacQ9Gk5V38DEP3SgGqRW8/kYZWyJwMmv23teZ0X7YIQd/MBepFyRt+Oklp4vnUA==", "1df08683-b40a-43c1-855b-9209beb6c881" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5541617e-ce8e-4da5-8e75-f13d37d72dfd", "AQAAAAEAACcQAAAAEHy1xBdni2ZUAzEuwOwG1MxaUpvtsyApA4lS7x5A9UoFdWOBotMhV1UPqL/cx+N+fw==", "af6f7552-71a1-4a9b-bc7f-8b806dd14adb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c87e9d3-2980-4ec1-a37e-a20a2e8c4666", "AQAAAAEAACcQAAAAEBYw+NEA+orG3B2S82ZS/SHvls1Qywqa1UBOLK3eANjQki6K8dQGQfbyWA+YDhC20w==", "e81911d8-f021-47ac-93ea-754ec6e6325c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b99ca187-7d22-4dac-9405-42a1f53af14f", "AQAAAAEAACcQAAAAELLgYQLNDa1hpq+xZ30IDSdffjxp8w1wWDFOelkoqQ91+ORPVCeKkKx1n+R3A4+l4Q==", "71dedd40-e970-40b5-9fb8-b35b67bc8d30" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ec20e0c-b57d-4ed1-b0bf-1435afac84ab", "AQAAAAEAACcQAAAAEFzgX6aNghecIo3OpIerj4mZTtGyGyWhJG7VQCkxKuK5DdUJFFPm8aPi8RFUAGy15g==", "eebbc6b2-aec6-4b5e-b857-b84b4f745332" });
        }
    }
}
