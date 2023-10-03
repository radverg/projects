/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;
using System.Collections.Generic;

namespace iis_project.Data
{
    public class MedicalReport
    {
        public int MedicalReportId { get; set; }

        public int? MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
        public int? MedicalTicketId { get; set; }
        public MedicalTicket MedicalTicket { get; set; }

        

        public ApplicationUser CreatedBy { get; set; }
        public DateTime DtCreated { get; set; }

        public string Header { get; set; }
        public string Content { get; set; }
        public List<ReportImage> Images { get; set; }
    }
}
