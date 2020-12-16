using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class EducacionUsuario
    {
        public string TituloEducacion { get; set; }
        public string DescripcionEducacion { get; set; }
        public string InstitucionEducacion { get; set; }
        public string FechaDesdeEducacion { get; set; }
        public string FechaHastaEducacion { get; set; }
    }
}