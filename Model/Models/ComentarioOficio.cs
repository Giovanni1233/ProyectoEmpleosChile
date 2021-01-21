using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class ComentarioOficio
    {
        public string Id { get; set; }
        public string IdUsuario { get; set; }

        public string NombreUsuario { get; set; }
        public string IdUsuarioComentario { get; set; }
        public string Comentario { get; set; }
        public List<ValoracionOficio> Valoracion { get; set; }
    }
}