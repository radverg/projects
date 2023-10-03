using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class CreateTimestamps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportAct");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "MedicalReports",
                newName: "Header");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MedicalTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCreated",
                table: "MedicalTickets",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "MedicalReports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCreated",
                table: "MedicalReports",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DtCreated",
                table: "MedicalRecords",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.CreateTable(
                name: "ReportImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportMedicalReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportImages_MedicalReports_ReportMedicalReportId",
                        column: x => x.ReportMedicalReportId,
                        principalTable: "MedicalReports",
                        principalColumn: "MedicalReportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketActs",
                columns: table => new
                {
                    MedicalTicketId = table.Column<int>(type: "int", nullable: false),
                    MedicalActId = table.Column<int>(type: "int", nullable: false),
                    DtCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketActs", x => new { x.MedicalActId, x.MedicalTicketId });
                    table.ForeignKey(
                        name: "FK_TicketActs_InsuranceActs_MedicalActId",
                        column: x => x.MedicalActId,
                        principalTable: "InsuranceActs",
                        principalColumn: "MedicalActId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketActs_MedicalTickets_MedicalTicketId",
                        column: x => x.MedicalTicketId,
                        principalTable: "MedicalTickets",
                        principalColumn: "MedicalTicketId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ReportImages_ReportMedicalReportId",
                table: "ReportImages",
                column: "ReportMedicalReportId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketActs_MedicalTicketId",
                table: "TicketActs",
                column: "MedicalTicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportImages");

            migrationBuilder.DropTable(
                name: "TicketActs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MedicalTickets");

            migrationBuilder.DropColumn(
                name: "DtCreated",
                table: "MedicalTickets");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "MedicalReports");

            migrationBuilder.DropColumn(
                name: "DtCreated",
                table: "MedicalReports");

            migrationBuilder.DropColumn(
                name: "DtCreated",
                table: "MedicalRecords");

            migrationBuilder.RenameColumn(
                name: "Header",
                table: "MedicalReports",
                newName: "Text");

            migrationBuilder.CreateTable(
                name: "ReportAct",
                columns: table => new
                {
                    MedicalActId = table.Column<int>(type: "int", nullable: false),
                    MedicalReportId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAct", x => new { x.MedicalActId, x.MedicalReportId });
                    table.ForeignKey(
                        name: "FK_ReportAct_InsuranceActs_MedicalActId",
                        column: x => x.MedicalActId,
                        principalTable: "InsuranceActs",
                        principalColumn: "MedicalActId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportAct_MedicalReports_MedicalReportId",
                        column: x => x.MedicalReportId,
                        principalTable: "MedicalReports",
                        principalColumn: "MedicalReportId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d5cd767-b93f-45ee-ad9b-95697b7b6ed3", "AQAAAAEAACcQAAAAEFUTfcQqmj6w83vRbPMoy1iIflfmMWk93Vnl/+H5vxkPCmyA1fKqPzrlmRkwyqv9gA==", "d998c301-b82e-4b50-a5d0-183b99c14304" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2b9d24b-91b0-4ce9-b89d-067303a152ca", "AQAAAAEAACcQAAAAEP4J/juSYFrJmr8sRDhoU1cj9d3ecPDV8VIzKMPmtOngS0FKaKlna8WkbGJ226o5VQ==", "2030ef47-78d3-4fce-9e5f-5696e6767d8a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "lekar2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42d76c10-66f6-4313-922f-a8d58e582676", "AQAAAAEAACcQAAAAEHyunUR4XS1m+hdBdnf/JGB5DQjmfO+6ur3m32KcEdfFWIi1rJk59rMfNVWkEdskGg==", "9421b3c1-f53b-4678-a139-2f93c47186fd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient1@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f2fa05b-ff4f-4aad-be81-7708cdfbfd62", "AQAAAAEAACcQAAAAEBgJIogi0b/EvgM9/d6/t1Y6ub73nzZazEBPH2EHKBV7Ba0VGnukJ38OjyZNo95J9g==", "43defc28-dfa4-48b9-93e6-c65bce385d3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pacient2@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98cde3bf-92df-4420-a61e-94fe60b66059", "AQAAAAEAACcQAAAAEHuE3BnASVwigWhYSqsjQ2Z5JMePno0WcO0Iw1ZMZc/gtN+PX0/cA7g24xaUMb9ZKA==", "8c272925-7938-49e4-b609-350addebd7cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "pojistovak@test.cz",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2f95953-15f7-402d-b3cd-429276fc910c", "AQAAAAEAACcQAAAAELS4j86T6HZU6eagDEl91LcxQaXxJbQI5kyO0ICaUYm/D7Po++IajvilDF4UqpM93w==", "ed06c65a-fb44-4e5e-a872-04b679456ea6" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportAct_MedicalReportId",
                table: "ReportAct",
                column: "MedicalReportId");
        }
    }
}
