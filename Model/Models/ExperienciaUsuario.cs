using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class ExperienciaUsuario
    {
        public string IdExperiencia { get; set;}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Termino { get; set; }
    }
}