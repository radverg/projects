/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System.Collections.Generic;

namespace iis_project.Data
{
    public class RolesMetadata
    {
        public const string ADMIN = "ADMIN";
        public const string DOCTOR = "DOCTOR";
        public const string INSURANCE_EMPLOYEE = "INSURANCE";
        public const string PATIENT = "PATIENT";

        public static readonly Dictionary<string, string> RolesNameMap = new Dictionary<string, string>
        {
            { ADMIN, "Administrátor" },
            { DOCTOR, "Lékař" },
            { INSURANCE_EMPLOYEE, "Pracovník zdravotní pojišťovny" },
            { PATIENT, "Pacient" }
        };
    }
}
