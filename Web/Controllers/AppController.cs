using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPI.ServicioEmpleosChile;

namespace Web.Controllers
{
    public class AppController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleosChile = new ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";


        public ActionResult Index()
        {
            ViewBag.ApplicationActive = true;
            ViewBag.ReferenciaHome = ModuleControlRetorno() + "/App/Inicio";

            return View();
        }

        public ActionResult Inicio()
        {
            ViewBag.ApplicationActive = true;
            ViewBag.ReferenciaInicio = ModuleControlRetorno() + "/App/Inicio";
            ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "/Auth/RegistroUsuario";

            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();


            return View();
        }



        private string ModuleControlRetorno()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost:44304"))
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


    }
}