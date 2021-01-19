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
        EmpresaController empresa = new EmpresaController();
        UsuarioController usuario = new UsuarioController();


        public ActionResult Index()
        {
            ViewBag.ApplicationActive = true;
            ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
            ViewBag.ReferenciaHome = ModuleControlRetorno() + "App/Inicio";

            return View();
        }

        public ActionResult Inicio()
        {
            try
            {
                DataSet data = new DataSet();

                ViewBag.ApplicationActive = true;
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaHome = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "Auth/RegistroUsuario";
                ViewBag.ReferenciaOficio = ModuleControlRetorno() + "Oficios/Inicio";

                if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                {
                    ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();

            ViewBag.ReferenciaEmpresasConPlan = empresa.GetEmpresasPlanesVigente();


            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
            {
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

                    #region TRASLADAR A METODO CORRESPONDIENTE
                    data = GetCurriculum(Session["IdUser"].ToString());
                    if (data.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow rows in data.Tables[0].Rows)
                        {
                            switch (rows["Code"].ToString())
                            {
                                case "200":
                                    ViewBag.ReferenciaIdUser = rows["IdUsuario"].ToString();
                                    ViewBag.ReferenciaDocumento = rows["Documento"].ToString();
                                    ViewBag.ReferenciaUrlCV = ModuleControlRetorno() + "App/DownloadCV?url=" + rows["Url"].ToString() + "&documento=" + rows["Documento"].ToString();
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

                ViewBag.ReferenciaEmpresasConPlan = empresa.GetEmpresasPlanesVigente();
            }
            catch (Exception ex)
            {

            }

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
                //if (!url.Split('/')[2].Contains("localhost:44304"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                //if (!url.Split('/')[2].Contains("localhost"))
                {
                    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                }
                else
                {
                    domainReal = "localhost";
                }

                domain = "http://" + domainReal + "/";
                prefixDomain = Request.Url.AbsoluteUri.Split('/')[3] + "/";
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2] + "/";
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }

        private bool ModuleApplicationActive()
        {
            return Session["IdUser"] != null;
        }

        private string EncodeToBase64(string encode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(encode));
        }

        private string DecodeFromBase64(string decode)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(decode));
        }
        #endregion



        #region TRASLADAR A METODO CORRESPONDIENTE
        public ActionResult SetFileCV(HttpPostedFileBase file)
        {
            string[] parametros = new string[3];
            string[] valores = new string[3];
            Curriculum curriculum = new Curriculum();

            try
            {
                DataSet data = new DataSet();
                var fileName = EncodeToBase64(Session["IdUser"].ToString() + file.FileName);

                file.SaveAs(Server.MapPath("~/FilesCV/" + fileName + ".pdf"));
                if (System.IO.File.Exists(Server.MapPath("~/FilesCV/" + fileName + ".pdf")))
                {
                    parametros[0] = "@USUARIO";
                    parametros[1] = "@DOCUMENTO";
                    parametros[2] = "@URL";

                    valores[0] = Session["IdUser"].ToString();
                    valores[1] = file.FileName;
                    valores[2] = "~/FilesCV/" + fileName + ".pdf";

                    data = svcEmpleosChile.SetCurriculum(parametros, valores).Table;

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ViewBag.ApplicationActive = ModuleApplicationActive();
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
            }
            return RedirectToAction("Inicio");
        }

        #endregion

        public ActionResult Empleos(string nombrePublicacion = "", string comuna = "", string idPublicacion = "", string fecha = "", string sueldo = "")
        {
            try
            {
                string idusuario = "";

                ViewBag.ApplicationActive = true;
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaHome = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "Auth/RegistroUsuario";
                ViewBag.ReferenciaOficio = ModuleControlRetorno() + "Oficios/Inicio";

                if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                    ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

                if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                    ViewBag.ReferenciaUserName = Session["UserName"].ToString();

                if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                    ViewBag.ReferenciaUserType = Session["UserType"].ToString();

                ViewBag.ReferenciaBusquedaEmpleos = GetOfertasEmpleos(nombrePublicacion, comuna, fecha, sueldo);
                ViewBag.ReferenciaComentarioPubEmpresa = empresa.GetComentariosPublicacion(idPublicacion);
                ViewBag.DetallePublicacionContador = empresa.GetDetallePublicacion(idPublicacion).Count();
                ViewBag.DetallePublicacion = empresa.GetDetallePublicacion(idPublicacion);
                ViewBag.PreguntasPorPublicacionId = empresa.GetPreguntasPorPublicacionId(idPublicacion);



                ViewBag.IdPublicacion = idPublicacion;

                if (Session["IdUser"] == null)
                {
                    idusuario = "";
                }
                else
                {
                    idusuario = Session["IdUser"].ToString();
                }
                ViewBag.ApplicationActive = ModuleApplicationActive();
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";

                ViewBag.referenciaSolicitud = usuario.GetSolicitudUsuario(idusuario, idPublicacion);
                ViewBag.VotoRealizado = empresa.GetVotoPorUsuario(idusuario, idPublicacion);
            }
            catch (Exception ex)
            {
                ViewBag.ApplicationActive = ModuleApplicationActive();
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
            }

            
            return View();
        }

        public ActionResult PerfilEmpresaLectura(string idEmpresa = "")
        {
            ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
            ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "Auth/RegistroUsuario";
            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();

            ViewBag.ReferenciaDatosEmpresa = empresa.GetDatosEmpresa(idEmpresa);
            ViewBag.ReferenciaImagenPerfilEmpresa = empresa.GetImagenDePerfilEmpresa(idEmpresa);
            ViewBag.referenciaIdEmpresa = idEmpresa;
            foreach (var item in ViewBag.ReferenciaDatosEmpresa)
            {
                ViewBag.referenciaNombreEmpresa = item.nombreEmpresa;
            }
            ViewBag.PublicacionesEmpresa = empresa.GetPublicaciones(idEmpresa, "");

            return View();
        }

        #region ObtencionDatos
        public List<DetallePublicacion> GetOfertasEmpleos(string nombrePublicacion, string comuna, string fecha = "", string sueldo = "")
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[4];
            string[] valores = new string[4];
            parametros[0] = "@NOMBRE_PUBLICACION";
            parametros[1] = "@COMUNA";
            parametros[2] = "@FECHA";
            parametros[3] = "@SUELDO";
            valores[0] = nombrePublicacion;
            valores[1] = comuna;
            valores[2] = fecha;
            valores[3] = sueldo;

            List<DetallePublicacion> clPublicacionEmpresa = new List<DetallePublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleosChile.GetOfertasEmpleos(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPublicacionEmpresa.Add(
                                new DetallePublicacion
                                {
                                    TituloPublicacion = rows["Titulo"].ToString(),
                                    IdPublicacion = (int)rows["IdPublicacion"],
                                    AutorPublicacion = rows["Autor"].ToString(),
                                    FechaPublicacion = rows["Fecha"].ToString(),
                                    EstadoPublicacion = (int)rows["Estado"],
                                    DiscapacidadPub = rows["Discapacidad"].ToString(),
                                    ContadorVotos = rows["ContadorVotos"].ToString(),
                                    PromedioVotos = rows["PromedioVotos"].ToString(),
                                    DescripcionPublicacion = rows["Descripcion"].ToString(),
                                    MontoPublicacion = rows["Monto"].ToString()
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
            return clPublicacionEmpresa;
        }

        public JsonResult CargarDatosPreguntas(string idPublicacion)
        {
            try
            {
                var preguntas = empresa.GetPreguntasPorPublicacionId(idPublicacion);
                return Json(new { datos = preguntas }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { datos = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        #endregion

        #region metodosGuardarDatos
        [HttpPost]
        public ActionResult GuardarComentario(string Id_Publicacion, string Comentario, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(Id_Publicacion) || string.IsNullOrWhiteSpace(Id_Publicacion) ||
                    string.IsNullOrEmpty(Comentario) || string.IsNullOrWhiteSpace(Comentario)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Empleos";//"App/_ModalMensajeError";
                    return View(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Empleos";//"App/_ModalMensajeError";
                    return View(view);
                }

                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@DESCRIPCION_COMENTARIO";
                parametros[2] = "@ID_USUARIO";


                valores[0] = Id_Publicacion;
                valores[1] = Comentario;
                valores[2] = Session["IdUser"].ToString();


                data = svcEmpleosChile.SetComentarioPublicacion(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Empleos";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Empleos";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Empleos";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Empleos";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Empleos";
            }

            return RedirectToAction(view, new { nombrePublicacion = "", comuna = "", idPublicacion = Id_Publicacion, fecha = "", sueldo = "" });
        }


        [HttpPost]
        public JsonResult GuardarVotacionPublicacion(string votacion, string idPublicacion, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                parametros[0] = "@VOTACION";
                parametros[1] = "@ID_PUBLICACION";
                parametros[2] = "@ID_USUARIO";


                valores[0] = votacion;
                valores[1] = idPublicacion;
                valores[2] = Session["IdUser"].ToString();


                data = svcEmpleosChile.SetUpdVotacionPublicacion(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Inicio";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Inicio";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Inicio";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Inicio";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Inicio";
            }
            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

       
       
    }
}