using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public class PlanesEmpresa
    {
        public string idPlan { get; set; }
        public string NombrePlan { get; set; }
        public string PrecioPlan { get; set; }

        public string estadoPlan { get; set; }
        public List<DetallePlan> detallePlan { get; set; }

    }
}