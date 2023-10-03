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
    public class MedicalTicket
    {
        public int MedicalTicketId { get; set; }
        public MedicalRecord Record { get; set; }
        public ApplicationUser Doctor { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public string Description { get; set; }
        public DateTime DtCreated { get; set; }
        public List<MedicalReport> MedicalReports { get; set; }

        public List<TicketAct> TicketActs { get; set; }

        public StatusTicket Status { get; set; }
    }
}
