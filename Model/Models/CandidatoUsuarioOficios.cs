using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class CandidatoUsuarioOficios
    {
        public string IdUsuario { get; set; }
        public string RutUsuario { get; set; }

        public string CorreoUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string ApellidosUsuario { get; set; }
        
        public byte[] ImagendelUsuario { get; set; }
        public List<DetalleTagOficio> TagOficios { get; set; }
    }
}