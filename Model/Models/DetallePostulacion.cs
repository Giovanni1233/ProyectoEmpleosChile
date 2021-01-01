using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class DetallePostulacion
    {
        public string idPublicacion { get; set; }
        public string TituloPublicacion { get; set; }
        public string FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string IdSolicitud { get; set; }
    }
}