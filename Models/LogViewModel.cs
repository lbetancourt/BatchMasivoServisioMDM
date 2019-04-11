using System;

namespace BatchMasivoServisioMDM.Models
{
    public class LogViewModel
    {
        public decimal Codigo { get; set; }
        public string Usuario { get; set; }
        public string XmlRequest { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
    }
}