using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class StatusTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TicketActs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MedicalTickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "4ff1d8f2-0cd8-4561-a1e8-3d7158f13ff1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "ec288297-4c4d-4785-ac32-e4ffaf2cce91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "b9ab57c8-46f7-4c0d-8a1b-1c47811f5614");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "bf78f624-cbda-4934-9b6b-5970fb9ed2f0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bd1be91-e942-4803-b967-6a0ddd66967f", "AQAAAAEAACcQAAAAEEHy9Mqa1AxU9R3KfGsLEQEdtSoRtUz2a3QIHD91/+qEhqm9UPg78NbEGSNf7T2AwQ==", "ad7a2440-2448-4930-b6cd-97ab6d025f23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90f96101-2eb2-405b-8146-e70c16fb5203", "AQAAAAEAACcQAAAAENYwABFIQPrfSV51dhlswsgIcBB9jRU3A7DSYKNw6Vlc4PAuGHubIFKSbkH5A7NyyA==", "78d4a2ed-95b5-44b0-a770-21f977e3bdd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "252b3344-f94a-4e40-aab8-117ab3b92145", "AQAAAAEAACcQAAAAENuYJCowomoslpCCZ0P6hy82A04PmYc9wj9psFHjvGwXwd0XiD3MCUkP5kXGeQYndA==", "677fbed7-6b2c-4ae1-9bcf-82e4a1f94c68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9c4def8-7d46-4c2b-a1f8-8d03089e2e92", "AQAAAAEAACcQAAAAEE0MG3eNkLtRSn9y3XwrRDljpgfdSGD+fdM7YEa5jIdMDCQ8aRabWUp7sFFfOZcdUg==", "ca291248-e16a-4d54-9bc4-d65ff95b755e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d55b4477-58b1-4c4c-990c-83aa425d86d8", "AQAAAAEAACcQAAAAEHmSMJ3D/saxMphgUUcVDugD3Es5BqBQjVfqWAgH1+cX2aBM0ZZTnnNb0QqSRiK8zA==", "2985c86e-dbca-4d3d-b7a4-995beb2b0402" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5cd00518-85c8-4968-89ca-e2356fff56d8", "AQAAAAEAACcQAAAAECFYWd3PGhJsrPuxijNmUzkYRucUJ8oKcCpOhG4cPf8OeMVzFLcSRD/EtQ37P6BIRg==", "59ddfce4-d788-4672-acbe-801239587e78" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TicketActs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MedicalTickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MedicalRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "40fabeda-571f-4ba0-b815-0670d446111e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DOCTOR",
                column: "ConcurrencyStamp",
                value: "018f170d-235d-4523-83bb-c21d76d54895");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "INSURANCE",
                column: "ConcurrencyStamp",
                value: "dbf1552d-0b1f-4ef3-8bc9-7302059d11bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "PATIENT",
                column: "ConcurrencyStamp",
                value: "39bbc269-d6bf-47a5-a881-ecda2a5d2003");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf6b6f5b-1790-4e9e-8d68-e8c0c43f6885", "AQAAAAEAACcQAAAAEKFZiYmSB5qa1Lijae2usvQfBG/9vqXY2a0TRf/a/kD4N99hIyUFcKvmIkpKz+5UtQ==", "fbb3beb5-1a57-40f4-97dd-24d7fc733876" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9aae5c6b-20cb-4fd2-a7d3-9f9f41779e49", "AQAAAAEAACcQAAAAECGH8/NYEHO9L5nkBL//YntE820CsEh/M3Q6egsMrCexU5ZHJp6iz3HRE5nDwN9W9g==", "1cfa3661-6326-4932-88d7-85ca6a63528d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67e2f29c-c922-420e-bcfa-d0fad093e3af", "AQAAAAEAACcQAAAAEKz7Qoacw7zLYHys5z8cr+VD8Rjr0n+PV+84KICQyKlYa0qr8C3G6zPrG5BiwwiACw==", "e9eb49ac-2305-4733-bc80-0222c3d1c501" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2276f0dc-0278-4cd2-8512-4f247a204e6d", "AQAAAAEAACcQAAAAECuel14kbIqpaUYLVuKZIcXzEWADLjeCp1h5cjpnHn0gs7+VHNlOY+8VNZHEHuF5WQ==", "4def7a41-2e49-495c-b449-57243b0ca632" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37f17997-4cf1-4d86-8968-13dc9dd0a749", "AQAAAAEAACcQAAAAEE1ijhZe0rEHC2jCRfeD//z8Se8xpFfl27dM/+VVFtjsw8twTEgKBxHVpo8ns9GGJw==", "f700ed21-cf55-4d3b-a6af-934d7d86b8ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c79bd61d-078e-4b5c-b71f-e294ff4aa469", "AQAAAAEAACcQAAAAEEQ7+sIczMuPqzgQrXponWN+f4hPBhpoSPHzWoyOIxK/KbI34hlu1k+FZ+xrWG8XTw==", "3fa9d271-824b-4607-b9d8-bbafece7a131" });
        }
    }
}
