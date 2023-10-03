/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using iis_project.Data.Enums;
using System;
using System.Collections.Generic;

namespace iis_project.Data
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public StatusRecord Status { get; set; }

        public DateTime DtCreated { get; set; }

        public List<MedicalReport> MedicalReports { get; set; }
        public List<MedicalTicket> MedicalTickets { get; set; }

        public ApplicationUser Doctor { get; set; }
        public ApplicationUser Patient { get; set; }
    }
}
