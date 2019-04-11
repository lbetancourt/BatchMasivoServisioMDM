using System;
using System.Collections.Generic;

namespace BatchMasivoServisioMDM.Models
{
    public partial class LogIntegraciones
    {
        public decimal Codigo { get; set; }
        public string Usuario { get; set; }
        public string XmlRequest { get; set; }
        public string XmlResponse { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? CodigoSistemaOrigen { get; set; }
        public decimal? CodigoSistemaDestino { get; set; }
        public decimal? CodigoTipoIntegracion { get; set; }
        public decimal? CodigoCanalSistema { get; set; }
        public decimal CodigoEstado { get; set; }
        public string Operacion { get; set; }
    }
}
