/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System.Collections.Generic;

namespace iis_project.Data
{
    public class MedicalAct
    {
        public int MedicalActId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public List<TicketAct> TicketActs { get; set; }
    }
}
