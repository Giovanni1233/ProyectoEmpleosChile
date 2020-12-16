using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class CandidatoEmpleos
    {
        public string IdCandidato { get; set; }
        public string RutCandidato { get; set; }

        public string CorreoCandidato { get; set; }

        public string NombreCandidato { get; set; }

        public string ApellidosCandidatos { get; set; }
        public string TituloPostulacion { get; set; }

         public string FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
}
}