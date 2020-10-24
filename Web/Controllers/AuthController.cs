using System.Data;
using System.Web.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        // GET: Auth
        public ActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        public JsonResult InicioSesion(string usuario, string clave)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[3];
            string[] valores = new string[3];
            Usuario clUsuario = new Usuario();

            try
            {
                DataSet data = new DataSet();
                parametros[0] = "@USUARIO";
                parametros[1] = "@PASSWORD";
                parametros[2] = "@TIPO";

                valores[0] = usuario;
                valores[1] = clave;
                valores[2] = "N";

                data = svcEmpleos.ValUsuario(parametros, valores).Table;
                foreach(DataRow rows in data.Tables[0].Rows)
                {
                    switch(rows["Code"].ToString())
                    {
                        case "200":
                            clUsuario.Rut = rows["Rut"].ToString();
                            clUsuario.Correo = rows["Correo"].ToString();
                            code = rows["Code"].ToString();
                            mensaje = "";
                            Session["Usuario"] = rows["Rut"].ToString();
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
            catch(Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
          return Json(new { Code = code, Message = mensaje, Usuario = clUsuario });
        }

        private string ModuleControlRetorno()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost:44392"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                {
                    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                }
                else
                {
                    domainReal = "localhost";
                }

                domain = "http://" + domainReal + "/";
                prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }

        [HttpPost]
        public JsonResult CerrarSesion()
        {
            var retorno = string.Empty;
            Session.Clear();

            retorno = "/App/Index"; // ModuleControlRetorno() + "/Index";

            return Json(new { Retorno = retorno });
        }
    }
}