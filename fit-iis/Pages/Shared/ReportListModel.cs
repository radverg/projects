/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using iis_project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iis_project.Pages.Shared
{
    public class ReportListModel
    {
        public MedicalReport SelectedReport { get; set; }
        public IEnumerable<MedicalReport> Reports { get; set; }
    }
}
