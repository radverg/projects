using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class ActActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "InsuranceActs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "deadb340-b6d3-4bf3-82d0-bff666338fa2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "604a6d94-e04b-426a-aaa5-c4c01c0e0780");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "b46fdfdf-e35e-4543-ad94-a6283b8ccc63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "4feeb005-191f-4c1a-9c84-5f05ca84835a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "237513a0-ddd8-4289-abdd-982b7bf69f83", "AQAAAAEAACcQAAAAECBdLrBkC6vVI7bJhZk4bpIrjYkQb0B5RNbdL7LrCmBLIIFEMUFnCbcEASxt7z3JOA==", "5761e97f-1d91-4544-9585-f52f554b3e54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "03268372-b9a0-422c-b72d-6d41c786e336", "AQAAAAEAACcQAAAAEGfbu46XRBGlgZXmB90Ja7n33xa5Ad6r3mCpRa0ZuHLpSd5NXbERGuRbOMCWH21R4A==", "02e19972-ed73-4dda-8360-36512b1e2ce8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "032f96ac-0e33-4cc6-8f11-9b8fa458e310", "AQAAAAEAACcQAAAAEDenmHRxa27Ii1os8DGDtwhUtaajiqRrOIAWbxNNN7uNcheyOedAAIjFmPs1WOvJ9g==", "513e0308-bd15-4840-81ad-dae66654b807" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fe273d5-b910-4207-b6ba-ec4479df6e18", "AQAAAAEAACcQAAAAEOzW5beYhTx/kJAnZgSZw47HzuIT3HgNqjhtZ1ZMaIi+k2JylhIbqEE2U0NIf64/Gw==", "f9afd2dc-70b1-436e-b91f-48409173fab3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "621c4a3f-93ea-413f-b7d6-d217b9311d85", "AQAAAAEAACcQAAAAEBWsB0QOsXQJ9EzQw/8h2Rn7qBgcfuu8CuO/njlA/VoZHLODcNMAzryFO9QgUOqnSQ==", "398220b1-a1dc-4ebc-a69d-eb44384da496" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a45d1c0-b445-46ae-8dd9-c8397d1799c0", "AQAAAAEAACcQAAAAEP7XJVMQcgF5ixP9ubLqEbijl3yRj935YWTNe6QnAbFmP8nXXWaNYmpaIRS/Jin3Pg==", "b84c8571-c713-439a-acaf-4bcd2b770578" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "InsuranceActs");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "3d72bcd5-78e4-40cc-b0f0-aa3daca29632");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "bf8fa3cd-1c83-40c6-9028-560e0d01eead");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "134a69ae-a3ba-4a50-b9a3-ec3d39db7821");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "89092610-0a78-44c4-b153-65ca01c71a9b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26425871-f5c6-4cd0-a223-8d08031d53f4", "AQAAAAEAACcQAAAAENqcbkcWqjd721V81/fIrktVNQEtNmgGxXp294Q8voo/aRJHGmxhWpCcwhcOf3iV7g==", "946a8ca1-dfc5-454e-9b11-a7962e8b1ff2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b70bff88-a895-476d-b7e2-9e0cd51faecf", "AQAAAAEAACcQAAAAEHWT3FH87L7G6CJzZtcNZNjDYA7IY7CVIXZc0M0/RqYZioP0XuoVWkfsxrBTd69UBw==", "374d8c60-8c56-4934-994e-a332928971c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "884b0c75-9556-4c52-ae22-5853f9c2047a", "AQAAAAEAACcQAAAAED/yEr5qeB8W7t17eR5qxIRSJZLZpQWodxrEeX29Zzd747HagITnqx2KLZNY3LxS+A==", "5928af22-6ff9-40e6-9b91-ded34439e6a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f85eb68-e79b-49fc-9c48-6f345f2c93df", "AQAAAAEAACcQAAAAEMge3phYLoZwpB/XRmj9HzrbJuZiB7M6frQBhRb3+T8LOZu0GbHDYruJX7BhTwnLMQ==", "4eea10f9-9bc7-470a-bfe9-ce0a3cfc0a86" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd3d0a0c-200b-42c3-89e1-5a7b1152744a", "AQAAAAEAACcQAAAAEGUvwTao6x/3otz2h2RsXz7ZBbGmR64UT8JOTiMR6suqmLJUsi7sPaGpCcFjfEIr/w==", "632ecf9d-1b8c-464b-9855-7536f111c11f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "212e2a1d-1a7f-493f-9bba-be828eceabe0", "AQAAAAEAACcQAAAAEJ2lmQ80KVvb2oX2UjUh5t7sYdAsFQ6z1qyw7JX86gIQzsz0v7uJuTmm7qPTRva3Gg==", "b9530cff-c8e3-4657-94a9-c53005ec589d" });
        }
    }
}
