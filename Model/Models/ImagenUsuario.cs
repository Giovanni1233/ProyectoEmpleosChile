using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class ImagenUsuario
    {
        public string IdImagen { get; set; }
        public byte[] Imagen { get; set; }
        public string NombreImagen { get; set; }
    }
}