using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class DetallePublicacion
    {
        public int IdPublicacion { get; set; }
        public string AutorPublicacion { get; set; }
        public string TituloPublicacion { get; set; }
        public string DescripcionPublicacion { get; set; }
        public string MontoPublicacion { get; set; }
        public string FechaPublicacion { get; set; }
    }
}