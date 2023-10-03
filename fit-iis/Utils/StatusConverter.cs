/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using iis_project.Data.Enums;

namespace iis_project.Utils
{
    public static class StatusConverter
    {
        public static string ConvertStatus(StatusRecord status)
        {
            if (status == StatusRecord.Open)
                return "Otevřen";
            if (status == StatusRecord.Closed)
                return "Uzavřen";

            return "Undefined";
        }

        public static string ConvertStatus(StatusTicket status)
        {
            if (status == StatusTicket.Open)
                return "Otevřen";
            if (status == StatusTicket.Closed)
                return "Uzavřen";

            return "Undefined";
        }

        public static string ConvertStatus(StatusInsurance status)
        {
            if (status == StatusInsurance.Open)
                return "Otevřen";
            if (status == StatusInsurance.Accepted)
                return "Přijmut";
            if (status == StatusInsurance.Rejected)
                return "Odmítnut";

            return "Undefined";
        }

        public static StatusRecord NextStatus(StatusRecord status)
        {
            return StatusRecord.Closed;
        }

        public static StatusTicket NextStatus(StatusTicket status)
        {
            return StatusTicket.Closed;
        }
    }
}
