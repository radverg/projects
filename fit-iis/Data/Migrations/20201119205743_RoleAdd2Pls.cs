using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class RoleAdd2Pls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d2b15e07-c738-4826-ae2f-2d0170789b04", "c750ba3d-fd5f-4f6d-8c34-09eb2a3be0d8", "ADMIN", null },
                    { "24bd8b08-6bd2-4106-b033-cba386fef186", "1b3d8d9b-2eb9-4576-9f7d-8f3a066e7ff5", "DOCTOR", null },
                    { "aa7e453d-bca0-49b2-9407-2dfc8919265d", "6a36933b-b207-4d07-9a8e-138ffbc611ba", "INSURANCE", null },
                    { "ccedeb83-3d81-4eb1-9e08-d4fca80b1caa", "c7139781-204b-4050-a881-2a7114af2693", "PATIENT", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24bd8b08-6bd2-4106-b033-cba386fef186");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa7e453d-bca0-49b2-9407-2dfc8919265d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccedeb83-3d81-4eb1-9e08-d4fca80b1caa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2b15e07-c738-4826-ae2f-2d0170789b04");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ADMIN", "5464bfd5-7b31-41b5-9d2c-8105dd2d92e6", "ADMIN", null },
                    { "DOCTOR", "b5013421-55c7-4e17-a602-f8ee402e3f8e", "DOCTOR", null },
                    { "INSURANCE", "48138d08-6ebe-401e-bef9-5ecbbdfeb118", "INSURANCE", null },
                    { "PATIENT", "443a1fa4-ef97-4924-a422-bea2cdd124fd", "PATIENT", null }
                });
        }
    }
}
