using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class ComentariosPubEmpresa
    {
        public string Id_PublicacionComen { get; set; }
        public string DescripcionComentario { get; set; }
        public string FechaComentario { get; set; }
        public string NombreAutor { get; set; }
    }
}