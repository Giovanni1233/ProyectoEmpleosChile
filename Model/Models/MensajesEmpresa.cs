﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class MensajesEmpresa
    {
        public string idMensaje { get; set; }
        public string Mensaje { get; set; }
        public string AutorMensaje { get; set; }
        public string FechaMensaje { get; set; }
        public string IdAutor { get; set; }
        public string IdReceptor { get; set; }
    }
}