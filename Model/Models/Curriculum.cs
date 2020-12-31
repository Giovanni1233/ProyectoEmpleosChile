using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class Curriculum
    {
        public string Id { get; set; }
        public string IdUsuario { get; set; }
        public string Documento { get; set; }
        public string NombreDocumento { get; set; }
        public string Url { get; set; }
    }
}