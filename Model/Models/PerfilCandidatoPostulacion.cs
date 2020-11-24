using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class PerfilCandidatoPostulacion
    {
        public int IdUsuario { get; set; }
        public string RutUsuario { get; set; }
        public string NombreUsuario { get; set; }

        public string ApellidoPUsuario { get; set; }
        public string ApellidoMUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string TelefonoUsuario { get; set; }

    }
}