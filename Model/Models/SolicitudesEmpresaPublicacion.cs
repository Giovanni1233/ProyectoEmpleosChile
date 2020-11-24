using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class SolicitudesEmpresaPublicacion
    {
        public string RutSolicitante { get; set; }
        public string FechaSolicitud { get; set; }
        public string IdSolicitante { get; set; }
        public string EstadoSolicitud { get; set; }
        public string PublicacionSolicitud { get; set; }
    }
}