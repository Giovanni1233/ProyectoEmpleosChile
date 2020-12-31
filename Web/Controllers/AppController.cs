using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
            DataSet data = new DataSet();

            ViewBag.ApplicationActive = true;
            ViewBag.ReferenciaInicio = ModuleControlRetorno() + "/App/Inicio";
            ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "/Auth/RegistroUsuario";

            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
            {
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

                #region TRASLADAR A METODO CORRESPONDIENTE
                data = GetCurriculum(Session["IdUser"].ToString());
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaIdUser = rows["IdUsuario"].ToString();
                            ViewBag.ReferenciaDocumento = rows["Documento"].ToString();
                            ViewBag.ReferenciaUrlCV = ModuleControlRetorno() + "/App/DownloadCV?url=" + rows["Url"].ToString() + "&documento=" + rows["Documento"].ToString();
                            break;

                        case "400":
                            ViewBag.ReferenciaMsg1 = rows["Message1"].ToString();
                            ViewBag.ReferenciaMsg2 = rows["Message2"].ToString();
                            break;

                        default:
                            ViewBag.ReferenciaMsg1 = "Puedes adjuntar tu CV!!";
                            ViewBag.ReferenciaMsg2 = "(.pdf)";
                            break;
                    }
                }
                #endregion
            }
            else
            {
                ViewBag.ReferenciaMsg1 = "Puedes adjuntar tu CV!!";
                ViewBag.ReferenciaMsg2 = "(.pdf)";
            }

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();



            return View();
        }

        #region SET

        #endregion

        #region OTHERS
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
        #endregion



        #region TRASLADAR A METODO CORRESPONDIENTE
        public ActionResult SetFileCV(HttpPostedFileBase file)
        {
            string[] parametros = new string[4];
            string[] valores = new string[4];
            Curriculum curriculum = new Curriculum();
            try
            {
                DataSet data = new DataSet();
                var fileName = Convert.ToBase64String(Encoding.UTF8.GetBytes(Session["IdUser"].ToString() + file.FileName));
                file.SaveAs(Server.MapPath("~/FilesCV/" + fileName + ".pdf"));

                if (System.IO.File.Exists(Server.MapPath("~/FilesCV/" + fileName + ".pdf")))
                {
                    parametros[0] = "@USUARIO";
                    parametros[1] = "@DOCUMENTO";
                    parametros[2] = "@NOMBRE_DOCUMENTO";
                    parametros[3] = "@URL";

                    valores[0] = Session["IdUser"].ToString();
                    valores[1] = file.FileName;
                    valores[2] = fileName;
                    valores[3] = "~/FilesCV/" + fileName + ".pdf";

                    data = svcEmpleosChile.SetCurriculum(parametros, valores).Table;

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Inicio");
        }

        public ActionResult DownloadCV(string url, string documento)
        {
            MemoryStream ms = new MemoryStream();
            var pdfbyte = System.IO.File.ReadAllBytes(Server.MapPath(url));

            ms = new MemoryStream();
            ms.Write(pdfbyte, 0, pdfbyte.Length);
            ms.Position = 0;

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + documento);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            return new FileStreamResult(Response.OutputStream, "application/pdf");
        }

        public JsonResult DeleteCV(string user)
        {
            string[] parametros = new string[1];
            string[] valores = new string[1];
            DataSet data = new DataSet();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = Session["IdUser"].ToString();

                data = svcEmpleosChile.DelCurriculum(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            if (System.IO.File.Exists(Server.MapPath(rows["Url"].ToString())))
                            {
                                System.IO.File.Delete(Server.MapPath(rows["Url"].ToString()));
                            }
                            break;

                        case "400":

                            break;
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return Json("");
        }

        public DataSet GetCurriculum(string user)
        {
            string[] parametros = new string[1];
            string[] valores = new string[1];
            DataSet data = new DataSet();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = Session["IdUser"].ToString();

                data = svcEmpleosChile.GetCurriculum(parametros, valores).Table;

            }
            catch (Exception ex)
            {

            }
            return data;
        }
        #endregion

    }
}