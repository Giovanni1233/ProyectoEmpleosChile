using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class TarjetaEmpresa
    {
        public string NombreTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string FechaExpiracion { get; set; }
        public string TarjetaCVV { get; set; }
        public string MontoDisponible { get; set; }
    }
}