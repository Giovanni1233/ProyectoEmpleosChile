using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class DatosUsuario
    {
        public string IdUsuario { get; set; }
        public string Rut { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
    }
}