using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class SeedUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09a04f51-1ffd-4671-ad6b-fbed6775605a", "AQAAAAEAACcQAAAAEO1+NSUU0N6YxkoI6lwIZJtxim0bn8tT3iEY9FMofNbQJV7xtCACOYYW5Rv9nNGz4w==", "d1e3b601-e11c-4d48-9884-61e822866e34" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06c8b417-4734-42cd-b1df-965f5fcb3971", "AQAAAAEAACcQAAAAEJKugBFrwSqm8YK2CodEnYveXG3qE+n7E8QmTLz3l+ATjgdXkolgb5o0Dzy1dxJB4w==", "160e493b-e40f-4ece-87c5-a69dab4b0634" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe2d5e8d-b81d-44d0-a127-e9d29a4b9e24", "AQAAAAEAACcQAAAAEKlOsqTAJqV95C+oZGFzb8iAsui+yAkCNjXOV0/gk7TeTol9lfSXUbusGiwWHYM4bA==", "96c2cca0-f381-4fe0-9215-58e49ebf0655" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04051a53-ae30-412b-a463-9c83d01b3e6f", "AQAAAAEAACcQAAAAEGHWBszDKcuuWPrlRPiYmjLU3BP9Yra6Lt00ZAjN0jqeAo2p6ttAcFb3vwnOGLmt5A==", "344712c5-2b34-4606-89d4-10604943175c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad657c93-4b98-4440-9d2b-c885117f1b92", "AQAAAAEAACcQAAAAENVR0pB7W3MaHTFuKTe4mZx1OZA80B6vGCEj5gAnXFVmsMiDpH7x19V0SjtgZ3K8ag==", "db25c98d-e33d-47b3-95c1-8c69002dc55b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc01424e-a40b-439b-a30e-6c9a7041c161", "AQAAAAEAACcQAAAAEMg1b9rqiFqFaKGCYQZT6TvY091iHxNft9HVeUzHsLf1L9gYmUhUa7USXZ+5GtLYYQ==", "68eb9bf4-2cf9-4e92-af01-8734700c73b0" });
        }
    }
}
