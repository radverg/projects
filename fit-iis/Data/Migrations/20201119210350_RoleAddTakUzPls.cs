using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class RoleAddTakUzPls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "ffe74332-8d8a-4bf2-9aaf-562d4c4e1ec7", "89c37bef-f070-4823-8dc5-1a0b29df26ed", "ADMIN", "ADMIN" },
                    { "27723f06-0c85-4d77-a9d8-789eb53bcfb4", "480cb5a0-171d-4f83-b174-c1f2fc2249ea", "DOCTOR", "DOCTOR" },
                    { "289997d7-c293-4ecf-ac15-a0139ed735ea", "34d9995f-53ed-43a6-a95c-8845a45abbd8", "INSURANCE", "INSURANCE" },
                    { "f1f52fd4-3de6-40e9-80a4-36a2d963821e", "d5cf58c5-8c95-4285-943e-c8d60ba3af6a", "PATIENT", "PATIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27723f06-0c85-4d77-a9d8-789eb53bcfb4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "289997d7-c293-4ecf-ac15-a0139ed735ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1f52fd4-3de6-40e9-80a4-36a2d963821e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe74332-8d8a-4bf2-9aaf-562d4c4e1ec7");

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
    }
}
