/*
 * IIS Projekt - Nemocnice
 * Listopad 2020
 * Autoři: Radek Veverka (xvever13)
 *         Adam Sedmík (xsedmi04)
 */

using System.ComponentModel.DataAnnotations.Schema;

namespace iis_project.Data
{
    public class ReportImage
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public string UrlRelative { get => $"ReportImages/{Id}_{Name}"; }
        public MedicalReport Report { get; set; }
    }
}
