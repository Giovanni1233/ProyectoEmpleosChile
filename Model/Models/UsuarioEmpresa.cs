using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class UsuarioEmpresa
    {
        public string RutUsuarioE { get; set; }
        public string NombreUsuarioE { get; set; }

        public string ApellidoUsuarioE { get; set; }
        public string FechaNacUsuarioE { get; set; }

        public string TelefonoUsuarioE { get; set; }

        public string PassUsuarioE { get; set; }

        public string CorreoUsuarioE { get; set; }
        public byte[] ImagendelUsuarioE { get; set; }


    }
}