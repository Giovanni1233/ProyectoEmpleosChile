using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class Usuario
    {
        public string User { get; set; }
        public string Rut { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }
        public string Tipo { get; set; }
        public string FechaNacimiento { get; set; }
        public string Vigente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}