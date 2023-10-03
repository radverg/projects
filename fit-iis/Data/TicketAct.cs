/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using iis_project.Data.Enums;
using System;

namespace iis_project.Data
{
    public class TicketAct
    {
        public int TicketActId { get; set; }
        public int MedicalTicketId { get; set; }
        public MedicalTicket MedicaTicket { get; set; }
        public int MedicalActId { get; set; }
        public MedicalAct MedicalAct { get; set; }

        public DateTime DtCreated { get; set; }
        public StatusInsurance Status { get; set; }
    }
}
