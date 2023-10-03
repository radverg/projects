using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class ReworkedIdentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "970c3ab5-04f8-4f5c-9043-632cc33a4725");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "73e1f6fd-59a4-4a6c-9670-d73c60e4148b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "3a5bc40b-38b0-4fb4-ad6a-5701a4f96004");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "07de523e-081e-4157-84ef-34952662a521");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1990, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "4d5cd767-b93f-45ee-ad9b-95697b7b6ed3", "Richard", "AQAAAAEAACcQAAAAEFUTfcQqmj6w83vRbPMoy1iIflfmMWk93Vnl/+H5vxkPCmyA1fKqPzrlmRkwyqv9gA==", "d998c301-b82e-4b50-a5d0-183b99c14304", "Jarůšek" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "f2b9d24b-91b0-4ce9-b89d-067303a152ca", "Tomáš", "AQAAAAEAACcQAAAAEP4J/juSYFrJmr8sRDhoU1cj9d3ecPDV8VIzKMPmtOngS0FKaKlna8WkbGJ226o5VQ==", "2030ef47-78d3-4fce-9e5f-5696e6767d8a", "Jeroušek" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(2010, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "42d76c10-66f6-4313-922f-a8d58e582676", "Jan", "AQAAAAEAACcQAAAAEHyunUR4XS1m+hdBdnf/JGB5DQjmfO+6ur3m32KcEdfFWIi1rJk59rMfNVWkEdskGg==", "9421b3c1-f53b-4678-a139-2f93c47186fd", "Martan" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1971, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "0f2fa05b-ff4f-4aad-be81-7708cdfbfd62", "Jan", "AQAAAAEAACcQAAAAEBgJIogi0b/EvgM9/d6/t1Y6ub73nzZazEBPH2EHKBV7Ba0VGnukJ38OjyZNo95J9g==", "43defc28-dfa4-48b9-93e6-c65bce385d3c", "Pospíšil" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1999, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "98cde3bf-92df-4420-a61e-94fe60b66059", "Hana", "AQAAAAEAACcQAAAAEHuE3BnASVwigWhYSqsjQ2Z5JMePno0WcO0Iw1ZMZc/gtN+PX0/cA7g24xaUMb9ZKA==", "8c272925-7938-49e4-b609-350addebd7cb", "Novotná" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1995, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "d2f95953-15f7-402d-b3cd-429276fc910c", "Josef", "AQAAAAEAACcQAAAAELS4j86T6HZU6eagDEl91LcxQaXxJbQI5kyO0ICaUYm/D7Po++IajvilDF4UqpM93w==", "ed06c65a-fb44-4e5e-a872-04b679456ea6", "Kováčik" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f8ef1dd3-06cb-4eac-ba22-dbf206c36ae9", null, "AQAAAAEAACcQAAAAEDSjD2aabHaSDfZkpxJsQveslA9e2EmJzPecBlxbqHS4fsR41yG4Z7OhHQKAGC+E5Q==", "25607af9-830d-4ebd-af2a-4c330e409d94", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dbb46fe9-5d23-4050-b06d-8e85c8d6d727", null, "AQAAAAEAACcQAAAAEIHiiaX2Ay0Fuz9jLkKbkNGaG3kLSowms5TidHjOe0bGY9rvJVGqQF/YG2wLEpx68Q==", "4dc5fbfa-9205-4d2a-8432-b963faade517", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a843eb12-a963-4e22-8fa8-3506b02cef22", null, "AQAAAAEAACcQAAAAEEA5pSiDj+QG1gLq6+H1ORS+3sAuNrB17TuL8YO/FABaerpZ3KeZ83Y3AH/wNtzr8w==", "5572055e-0bbf-4ac1-9845-5efb39ad7b55", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "04e0209a-5284-42b9-8469-beee41206af8", null, "AQAAAAEAACcQAAAAEDJ2Gb629fnEEykUqljiVQbHPwFWlNI7IJFZRcXY5R0rrYDa6JgsZ/HUD79brRTRxA==", "b2a3d58e-ee78-42ca-927f-f9c8ba8f476e", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7c4e4e5b-051f-4c0c-af7b-d69961fd600c", null, "AQAAAAEAACcQAAAAEEDVS6CqjE4lur4O6kfAPc7SRuVx+9+ILnpJ7cD4cGCqbLylcThuVoEoge4JgGtSJA==", "28cece27-4987-463b-9cd3-318b1e1a1a46", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "BirthDate", "ConcurrencyStamp", "GivenName", "PasswordHash", "SecurityStamp", "Surname" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4ae062db-ef11-4953-a371-e13fb57edc1e", null, "AQAAAAEAACcQAAAAEM0tIX1eY3kCln8WoHNBeBphS7kCZhPvgY1l6ZSE3ThAumlj/ucM8unfcO4h+ppPrw==", "06a2058c-651b-4866-8652-16a0284538f4", null });
        }
    }
}
