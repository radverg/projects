using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class RoleTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "25b67182-031f-4df6-beeb-607572729636", "2dfd88b7-b46d-4bf2-be43-4380bcb03ae9", "ADMIN", "ADMIN" },
                    { "ba416fa9-d3af-4ae6-8ab5-f2152a227e17", "a752e3d9-a655-445a-b30f-b8e1932e8804", "DOCTOR", "DOCTOR" },
                    { "b817b37b-30e8-408b-94d7-712342364559", "dd9ce6db-d2ba-449d-a60a-9be17b4d5b52", "INSURANCE", "INSURANCE" },
                    { "d8a52040-94f3-4404-909f-241994cbb518", "1e708ca2-75bf-4478-94bc-a947200c4b9d", "PATIENT", "PATIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25b67182-031f-4df6-beeb-607572729636");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b817b37b-30e8-408b-94d7-712342364559");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba416fa9-d3af-4ae6-8ab5-f2152a227e17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8a52040-94f3-4404-909f-241994cbb518");

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
    }
}
