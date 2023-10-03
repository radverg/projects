using Microsoft.EntityFrameworkCore.Migrations;

namespace iis_project.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceActs",
                columns: table => new
                {
                    MedicalActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceActs", x => x.MedicalActId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DoctorId = table.Column<string>(nullable: true),
                    PatientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_AspNetUsers_PatientId",
                        column: x => x.PatientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalTickets",
                columns: table => new
                {
                    MedicalTicketId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordMedicalRecordId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalTickets", x => x.MedicalTicketId);
                    table.ForeignKey(
                        name: "FK_MedicalTickets_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalTickets_AspNetUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalTickets_MedicalRecords_RecordMedicalRecordId",
                        column: x => x.RecordMedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalReports",
                columns: table => new
                {
                    MedicalReportId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordId = table.Column<int>(nullable: true),
                    MedicalTicketId = table.Column<int>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalReports", x => x.MedicalReportId);
                    table.ForeignKey(
                        name: "FK_MedicalReports_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalReports_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "MedicalRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalReports_MedicalTickets_MedicalTicketId",
                        column: x => x.MedicalTicketId,
                        principalTable: "MedicalTickets",
                        principalColumn: "MedicalTicketId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportAct",
                columns: table => new
                {
                    MedicalReportId = table.Column<int>(nullable: false),
                    MedicalActId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_CreatedById",
                table: "MedicalReports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_MedicalRecordId",
                table: "MedicalReports",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_MedicalTicketId",
                table: "MedicalReports",
                column: "MedicalTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTickets_CreatedById",
                table: "MedicalTickets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTickets_DoctorId",
                table: "MedicalTickets",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalTickets_RecordMedicalRecordId",
                table: "MedicalTickets",
                column: "RecordMedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAct_MedicalReportId",
                table: "ReportAct",
                column: "MedicalReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportAct");

            migrationBuilder.DropTable(
                name: "InsuranceActs");

            migrationBuilder.DropTable(
                name: "MedicalReports");

            migrationBuilder.DropTable(
                name: "MedicalTickets");

            migrationBuilder.DropTable(
                name: "MedicalRecords");
        }
    }
}
