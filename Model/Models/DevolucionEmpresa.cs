using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class DevolucionEmpresa
    {
        public string NumeroTarjeta { get; set; }
        public string MontoDevolucion { get; set; }
        public string Plan_Anterior { get; set; }
        public string Plan_Actual { get; set; }
    }
}