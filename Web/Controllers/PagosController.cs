using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class PagosController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        // GET: Pagos
        public ActionResult Index(string idPlan)
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            EmpresaController empresa = new EmpresaController();
            ViewBag.Planes = empresa.GetPlanes("");
            ViewBag.referenciaPlanSeleccionado = GetPlanSeleccionado(idEmpresa, idPlan);
            ViewBag.referenciaPlanEmpresa = empresa.GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.referenciaTarjetaEmpresa = empresa.GetTarjetasEmpresa(idEmpresa);
            ViewBag.idPlan = idPlan;
            ViewBag.idPlanAnterior = empresa.GetPlanAnteriorEmpresa(idEmpresa);
            return View();
        }

        public List<PlanesEmpresa> GetPlanSeleccionado(string idEmpresa = "", string idplan = "")
        {
            EmpresaController emp = new EmpresaController();
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@IDL_EMPRESA";
            parametros[1] = "@ID_PLAN";
            valores[0] = idEmpresa;
            valores[1] = idplan;
            List<PlanesEmpresa> clDetallePlanEmpresa = new List<PlanesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPlanSeleccionadoEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetallePlanEmpresa.Add(
                                new PlanesEmpresa
                                {
                                    idPlan = rows["IdPlan"].ToString(),
                                    NombrePlan = rows["NombrePlan"].ToString(),
                                    PrecioPlan = rows["Precio"].ToString(),
                                    detallePlan = emp.GetDetallePlanes(rows["IdPlan"].ToString())
                                });

                            mensaje = "";
                            break;
                        case "400":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            break;

                        case "500":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            break;

                        default:
                            code = "600";
                            mensaje = errorSistema;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clDetallePlanEmpresa;
        }
    }


}