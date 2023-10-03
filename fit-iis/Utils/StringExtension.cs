/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System;

namespace iis_project.Utils
{
    public static class StringExtension
    {
        public static string Trunc(this string val, int max)
        {
            if (max < 3)
                throw new ArgumentOutOfRangeException("max", max, "Minimum trunc lenght string is 4.");

            if (val.Length > max)
            {
                return val.Substring(0, max - 3) + "...";
            }

            return val;
        }
    }
}
