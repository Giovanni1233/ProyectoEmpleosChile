using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class EmpresasPlanVigentes
    {
        public string idEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string RubroEmpresa { get; set; }
        public byte[] ImagenEmpresa { get; set; }
    }
}