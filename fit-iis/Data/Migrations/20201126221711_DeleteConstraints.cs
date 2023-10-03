using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class DeleteConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_AspNetUsers_CreatedById",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_MedicalRecords_MedicalRecordId",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_MedicalTickets_MedicalTicketId",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTickets_MedicalRecords_RecordMedicalRecordId",
                table: "MedicalTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportImages_MedicalReports_ReportMedicalReportId",
                table: "ReportImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketActs_InsuranceActs_MedicalActId",
                table: "TicketActs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketActs",
                table: "TicketActs");

            migrationBuilder.AddColumn<int>(
                name: "TicketActId",
                table: "TicketActs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "ReportMedicalReportId",
                table: "ReportImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RecordMedicalRecordId",
                table: "MedicalTickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketActs",
                table: "TicketActs",
                column: "TicketActId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TicketActs_MedicalActId",
                table: "TicketActs",
                column: "MedicalActId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_AspNetUsers_CreatedById",
                table: "MedicalReports",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_MedicalRecords_MedicalRecordId",
                table: "MedicalReports",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_MedicalTickets_MedicalTicketId",
                table: "MedicalReports",
                column: "MedicalTicketId",
                principalTable: "MedicalTickets",
                principalColumn: "MedicalTicketId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTickets_MedicalRecords_RecordMedicalRecordId",
                table: "MedicalTickets",
                column: "RecordMedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportImages_MedicalReports_ReportMedicalReportId",
                table: "ReportImages",
                column: "ReportMedicalReportId",
                principalTable: "MedicalReports",
                principalColumn: "MedicalReportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketActs_InsuranceActs_MedicalActId",
                table: "TicketActs",
                column: "MedicalActId",
                principalTable: "InsuranceActs",
                principalColumn: "MedicalActId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_AspNetUsers_CreatedById",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_MedicalRecords_MedicalRecordId",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_MedicalTickets_MedicalTicketId",
                table: "MedicalReports");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalTickets_MedicalRecords_RecordMedicalRecordId",
                table: "MedicalTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportImages_MedicalReports_ReportMedicalReportId",
                table: "ReportImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketActs_InsuranceActs_MedicalActId",
                table: "TicketActs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketActs",
                table: "TicketActs");

            migrationBuilder.DropIndex(
                name: "IX_TicketActs_MedicalActId",
                table: "TicketActs");

            migrationBuilder.DropColumn(
                name: "TicketActId",
                table: "TicketActs");

            migrationBuilder.AlterColumn<int>(
                name: "ReportMedicalReportId",
                table: "ReportImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RecordMedicalRecordId",
                table: "MedicalTickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketActs",
                table: "TicketActs",
                columns: new[] { "MedicalActId", "MedicalTicketId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_AspNetUsers_CreatedById",
                table: "MedicalReports",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_MedicalRecords_MedicalRecordId",
                table: "MedicalReports",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_MedicalTickets_MedicalTicketId",
                table: "MedicalReports",
                column: "MedicalTicketId",
                principalTable: "MedicalTickets",
                principalColumn: "MedicalTicketId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalTickets_MedicalRecords_RecordMedicalRecordId",
                table: "MedicalTickets",
                column: "RecordMedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "MedicalRecordId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportImages_MedicalReports_ReportMedicalReportId",
                table: "ReportImages",
                column: "ReportMedicalReportId",
                principalTable: "MedicalReports",
                principalColumn: "MedicalReportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketActs_InsuranceActs_MedicalActId",
                table: "TicketActs",
                column: "MedicalActId",
                principalTable: "InsuranceActs",
                principalColumn: "MedicalActId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
