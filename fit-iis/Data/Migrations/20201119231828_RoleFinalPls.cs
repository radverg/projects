using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class RoleFinalPls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "ADMIN", "c99bf448-7dbf-4c6b-9d72-2cfa649805f8", "ADMIN", "ADMIN" },
                    { "DOCTOR", "0b3ee6d7-3928-4dc7-96a7-7649f271888c", "DOCTOR", "DOCTOR" },
                    { "INSURANCE", "d6155c77-7619-412f-b631-fea728fd7806", "INSURANCE", "INSURANCE" },
                    { "PATIENT", "56f42b83-9e24-4a7d-abe9-7698b2669b1d", "PATIENT", "PATIENT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "25b67182-031f-4df6-beeb-607572729636", "2dfd88b7-b46d-4bf2-be43-4380bcb03ae9", "ADMIN", "ADMIN" },
                    { "ba416fa9-d3af-4ae6-8ab5-f2152a227e17", "a752e3d9-a655-445a-b30f-b8e1932e8804", "DOCTOR", "DOCTOR" },
                    { "b817b37b-30e8-408b-94d7-712342364559", "dd9ce6db-d2ba-449d-a60a-9be17b4d5b52", "INSURANCE", "INSURANCE" },
                    { "d8a52040-94f3-4404-909f-241994cbb518", "1e708ca2-75bf-4478-94bc-a947200c4b9d", "PATIENT", "PATIENT" }
                });
        }
    }
}
