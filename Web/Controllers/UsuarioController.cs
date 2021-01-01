using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        SqlConnection conexion = new SqlConnection("server=GIOVANNIDIAZF; database=EMPLEOSCHILE; integrated security=true");
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        
        AuthController auth = new AuthController();
        // GET: Usuario
        #region metodos
        public ActionResult Perfil(string idMensaje = "0", string idReceptor = "0")
        {
            EmpresaController empresa = new EmpresaController();
            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();

            if(Session["IdUser"] == null)
            {
                auth.SignOut();
            }
            var usuario = Session["IdUser"].ToString();

            ViewBag.PublicacionUsuario = GetPublicacionUsuario(usuario);
            ViewBag.PostulacionesUsuario = GetPostulacionesUsuario(usuario);
            ViewBag.referenciaHistorialConversacion = GetHistorialConversacion(idMensaje, usuario);
            ViewBag.MensajesUsuario = GetMensajesUsuario(usuario);
            ViewBag.referenciadetalleMensaje = empresa.GetDetalleMensajeE(idMensaje);
            ViewBag.referenciaIdMensaje = idMensaje;
            ViewBag.idMensaje = idMensaje;
            ViewBag.idReceptor = idReceptor;

            // Curriculum usuario
            ViewBag.ReferenciaDatosUsuarioNormal = GetDatosUsuarioPerfil(usuario);
            ViewBag.referenciaExperienciasUsuario = empresa.GetExperienciasUsuario(usuario);
            ViewBag.referenciaEducacionUsuario = empresa.GetEducacionUsuario(usuario);
            ViewBag.referenciaIdiomasUsuario = empresa.GetIdiomasUsuario(usuario);
            ViewBag.referenciaHabilidadesUsuario = empresa.GetHabilidadesUsuario(usuario);
            ViewBag.referenciaInstituciones = GetInstituciones();
            ViewBag.referenciaIdiomas = GetIdiomas();
            ViewBag.referenciaHabilidades = GetHabilidades();
            ViewBag.referenciaPerfilProfesional = GetPerfilProfesionalUsuario(usuario);

            // Area usuario
            ViewBag.referenciaContadorPostulaciones = GetContadorPostulaciones(usuario);
            ViewBag.referenciaImagenPerfil = GetImagenDePerfilUsuario(usuario);
            ViewBag.ReferenciaEmpresasConPlan = empresa.GetEmpresasPlanesVigente();
            ViewBag.referenciaEmpleosAPerfil = GetEmpleosAdaptadosAPerfil(usuario);

            return View();
        }
        #endregion
        #region ObtencionDatos
        public List<ContadorPostulacionUsuario> GetContadorPostulaciones(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<ContadorPostulacionUsuario> clContador = new List<ContadorPostulacionUsuario>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetContadorPostulacionUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clContador.Add(
                                new ContadorPostulacionUsuario
                                {
                                    CVVistos = (int)rows["CVVistos"],
                                    EnProceso = (int)rows["EnProceso"],
                                    OfertasSolicitadas = (int)rows["OfertasSolicitadas"]
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
            return clContador;
        }
        public List<DatosUsuario> GetDatosUsuarioPerfil(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DatosUsuario> clDatosUsuario = new List<DatosUsuario>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetDatosUsuarioPerfil(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDatosUsuario.Add(
                                new DatosUsuario
                                {
                                    IdUsuario = rows["IdUsuario"].ToString(),
                                    Rut = rows["Rut"].ToString(),
                                    Nombre1 = rows["Nombre1"].ToString(),
                                    Nombre2 = rows["Nombre2"].ToString(),
                                    ApellidoP = rows["ApellidoP"].ToString(),
                                    ApellidoM = rows["ApellidoM"].ToString(),
                                    Telefono = rows["Telefono"].ToString(),
                                    Correo = rows["Correo"].ToString(),
                                    Descripcion = rows["Descripcion"].ToString(),
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
            return clDatosUsuario;
        }
        public List<DetallePublicacion> GetEmpleosAdaptadosAPerfil(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetallePublicacion> clEmpleosAdaptados = new List<DetallePublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetEmpleosAdaptadosAPerfil(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clEmpleosAdaptados.Add(
                                new DetallePublicacion
                                {
                                   TituloPublicacion = rows["TituloPublicacion"].ToString(),
                                   AutorPublicacion = rows["AutorPublicacion"].ToString(),
                                   FechaPublicacion = rows["FechaPublicacion"].ToString(),
                                   IdPublicacion = (int)rows["IdPublicacion"],
                                   IdAutor = rows["IdAutor"].ToString()
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
            return clEmpleosAdaptados;
        }
        public List<Habilidad> GetHabilidades()
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            List<Habilidad> clHabilidad = new List<Habilidad>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetHabilidades().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clHabilidad.Add(
                                new Habilidad
                                {
                                    idHabilidad = rows["IdHabilidad"].ToString(),
                                    nombreHabilidad = rows["nombreHabilidad"].ToString()
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
            return clHabilidad;
        }
        public List<Instituciones> GetInstituciones()
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            
            List<Instituciones> clInstituciones = new List<Instituciones>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetInstituciones().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clInstituciones.Add(
                                new Instituciones
                                {
                                    IdInstitucion = rows["IdInstitucion"].ToString(),
                                    nombreInstitucion = rows["nombreInstitucion"].ToString()
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
            return clInstituciones;
        }
        public List<Idioma> GetIdiomas()
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            List<Idioma> clIdiomas = new List<Idioma>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetIdiomas().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clIdiomas.Add(
                                new Idioma
                                {
                                    idIdioma = rows["IdIdioma"].ToString(),
                                    nombreIdioma = rows["nombreIdioma"].ToString()
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
            return clIdiomas;
        }
        public List<PerfilProfesionalUsuario> GetPerfilProfesionalUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<PerfilProfesionalUsuario> clPerfilProfesional = new List<PerfilProfesionalUsuario>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPerfilProfesionalUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPerfilProfesional.Add(
                                new PerfilProfesionalUsuario
                                {
                                    IdPerfil = rows["IdPerfil"].ToString(),
                                    DescripcionPerfil = rows["DescripcionPerfil"].ToString(),
                                    TituloPerfil = rows["TituloPerfil"].ToString(),
                                    
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
            return clPerfilProfesional;
        }
        public List<DetallePostulacion> GetPostulacionesUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetallePostulacion> clPostulacionUsuario = new List<DetallePostulacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPostulacionesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPostulacionUsuario.Add(
                                new DetallePostulacion
                                {
                                    EstadoSolicitud = rows["EstadoSolicitud"].ToString(),
                                    FechaSolicitud = rows["FechaSolicitud"].ToString(),
                                    idPublicacion = rows["IdPublicacion"].ToString(),
                                    IdSolicitud = rows["IdSolicitud"].ToString(),
                                    TituloPublicacion = rows["TituloPublicacion"].ToString(),
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
            return clPostulacionUsuario;
        }
        public List<DetallePublicacion> GetPublicacionUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetallePublicacion> clPublicacionUsuario = new List<DetallePublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPublicacionesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPublicacionUsuario.Add(
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
            return clPublicacionUsuario;
        }
        public List<MensajesUsuario> GetMensajesUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<MensajesUsuario> clMensajesUsuario = new List<MensajesUsuario>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetMensajesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clMensajesUsuario.Add(
                                new MensajesUsuario
                                {
                                    Mensaje = rows["Mensaje"].ToString(),
                                    AutorMensaje = rows["AutorMensaje"].ToString(),
                                    FechaMensaje = rows["FechaMensaje"].ToString(),
                                    idMensaje = rows["IdMensaje"].ToString(),
                                    IdAutor = rows["IdAutor"].ToString(),
                                    ReceptorMensaje = rows["ReceptorMensaje"].ToString(),
                                    IdReceptor = rows["IdReceptor"].ToString()
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
            return clMensajesUsuario;
        }
        public List<HistorialMensajesEmpresa> GetHistorialConversacion(string idMensaje, string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_MENSAJE";
            parametros[1] = "@ID_USUARIO";
            valores[0] = idMensaje;
            valores[1] = idUsuario;
            List<HistorialMensajesEmpresa> clHistorialMensaje = new List<HistorialMensajesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetHistorialMensajesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clHistorialMensaje.Add(
                                new HistorialMensajesEmpresa
                                {
                                    MensajeH = rows["Mensaje"].ToString(),
                                    IdMensajeH = rows["IdMensaje"].ToString(),
                                    ReceptorH = rows["ReceptorMensaje"].ToString(),
                                    EmisorH = rows["AutorMensaje"].ToString(),
                                    FechaMensajeH = rows["Fecha"].ToString(),
                                    idAutorM = rows["idAutor"].ToString()
                                });

                            mensaje = "";
                            break;
                        case "400":
                            code = rows["Code"].ToString();
                            break;

                        case "500":
                            code = rows["Code"].ToString();
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
            return clHistorialMensaje;
        }
        public List<ImagenUsuarioPerfil> GetImagenDePerfilUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<ImagenUsuarioPerfil> clImagenUsuario = new List<ImagenUsuarioPerfil>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetImagenPerfilUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clImagenUsuario.Add(
                                new ImagenUsuarioPerfil
                                {
                                    Imagen = (byte[])rows["Imagen"],
                                    NombreImagen = rows["NombreImagen"].ToString(),
                                    IdImagen = rows["IdImagen"].ToString()
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
            return clImagenUsuario;
        }
        public List<SolicitudesEmpresaPublicacion> GetSolicitudUsuario(string idUsuario, string idPublicacion)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_USUARIO";
            parametros[1] = "@ID_PUBLICACION";
            valores[0] = idUsuario;
            valores[1] = idPublicacion;
            List<SolicitudesEmpresaPublicacion> clSolicitudes = new List<SolicitudesEmpresaPublicacion>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetSolicitudUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clSolicitudes.Add(
                                new SolicitudesEmpresaPublicacion
                                {
                                    FechaSolicitud = rows["FechaSolicitud"].ToString(),
                                    EstadoSolicitud = rows["EstadoSolicitud"].ToString()
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
            return clSolicitudes;
        }
        #endregion

        #region Acciones

        [HttpPost]
        public JsonResult EliminarEducacionPerfilUsuario(string idEducacion, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[1];
                string[] valores = new string[1];

                DataSet data = new DataSet();


                parametros[0] = "@ID_EDUCACION";
                valores[0] = idEducacion;

                data = svcEmpleos.DelEducacionUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarExperienciaPerfilUsuario(string idExperiencia, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[1];
                string[] valores = new string[1];

                DataSet data = new DataSet();

               
                parametros[0] = "@ID_EXPERIENCIA";
                valores[0] = idExperiencia;
                
                data = svcEmpleos.DelExperienciasUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarHabilidadPerfilUsuario(string IdHabilidad, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[1];
                string[] valores = new string[1];

                DataSet data = new DataSet();


                parametros[0] = "@ID_HABILIDAD";
                valores[0] = IdHabilidad;

                data = svcEmpleos.DelHabilidadesUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarIdiomaPerfilUsuario(string idIdioma, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[1];
                string[] valores = new string[1];

                DataSet data = new DataSet();


                parametros[0] = "@ID_IDIOMA";
                valores[0] = idIdioma;

                data = svcEmpleos.DelIdiomasUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEnvioMensajeAEmpresa(string idEmpresa, string mensaje)
        {

            string view = string.Empty;
            var codigo = "0";
            var usuario = Session["IdUser"].ToString();
            try
            {
                string[] parametros = new string[4];
                string[] valores = new string[4];

                if (string.IsNullOrEmpty(mensaje) || string.IsNullOrWhiteSpace(mensaje)
                   )
                {
                    return Json(new { data = "-1" }, JsonRequestBehavior.AllowGet);
                }

                DataSet data = new DataSet();


                parametros[0] = "@MENSAJE";
                parametros[1] = "@IS_EMPRESA";
                parametros[2] = "@AUTOR";
                parametros[3] = "@RECEPTOR";


                valores[0] = mensaje;
                valores[1] = "0";
                valores[2] = usuario;
                valores[3] = idEmpresa;


                data = svcEmpleos.SetMensajes(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            codigo = "1";
                            break;

                        case "400":
                            codigo = "-1";
                            break;
                        case "500":
                            codigo = "-1";
                            break;
                        default:
                            codigo = "-1";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                codigo = "-1";
            }


            return Json(new { data = codigo }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult GuardarExperienciaPerfilUsuario(string empresaNombre, string recomendacion, string descripcion,  string destacoEmpresa, string mejorarEmpresa, string fechaD, string fechaH, string actualmente, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[9];
                string[] valores = new string[9];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(empresaNombre) || string.IsNullOrWhiteSpace(empresaNombre) ||
                    string.IsNullOrEmpty(destacoEmpresa) || string.IsNullOrWhiteSpace(destacoEmpresa) ||
                    string.IsNullOrEmpty(mejorarEmpresa) || string.IsNullOrWhiteSpace(mejorarEmpresa) ||
                    string.IsNullOrEmpty(fechaD) || string.IsNullOrWhiteSpace(fechaD) ||
                    string.IsNullOrEmpty(descripcion) || string.IsNullOrWhiteSpace(descripcion))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@EMPRESA_NOMBRE";
                parametros[1] = "@DESCRIPCION";
                parametros[2] = "@RECOMENDACION";
                parametros[3] = "@DESTACO";
                parametros[4] = "@MEJORAR";
                parametros[5] = "@FECHA_D";
                parametros[6] = "@FECHA_H";
                parametros[7] = "@ACTUALMENTE";
                parametros[8] = "@ID_USUARIO";


                valores[0] = empresaNombre.Trim();
                valores[1] = descripcion.Trim();
                valores[2] = recomendacion.Trim();
                valores[3] = destacoEmpresa;
                valores[4] = mejorarEmpresa;
                valores[5] = fechaD;
                valores[6] = fechaH;
                valores[7] = actualmente;
                valores[8] = Session["IdUser"].ToString();


                data = svcEmpleos.SetExperienciasUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEducacionPerfilUsuario(string centroNombre, string estadoEdu, string tituloNombreEdu, string descripcion,  string fechaD, string fechaH, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[7];
                string[] valores = new string[7];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(centroNombre) || string.IsNullOrWhiteSpace(centroNombre) ||
                    string.IsNullOrEmpty(estadoEdu) || string.IsNullOrWhiteSpace(estadoEdu) ||
                    string.IsNullOrEmpty(fechaD) || string.IsNullOrWhiteSpace(fechaD))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@CENTRO_NOMBRE";
                parametros[1] = "@ESTADO_EDUCACION";
                parametros[2] = "@TITULO";
                parametros[3] = "@DESCRIPCION";
                parametros[4] = "@FECHA_D";
                parametros[5] = "@FECHA_H";
                parametros[6] = "@ID_USUARIO";


                valores[0] = centroNombre.Trim();
                valores[1] = estadoEdu;
                valores[2] = tituloNombreEdu.Trim();
                valores[3] = descripcion;
                valores[4] = fechaD;
                valores[5] = fechaH;
                valores[6] = Session["IdUser"].ToString();


                data = svcEmpleos.SetEducacionUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarHabilidadPerfilUsuario(string idHabilidad, string nivel, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(idHabilidad) || string.IsNullOrWhiteSpace(idHabilidad) ||
                    string.IsNullOrEmpty(nivel) || string.IsNullOrWhiteSpace(nivel))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_HABILIDAD";
                parametros[1] = "@NIVEL_HABILIDAD";
                parametros[2] = "@ID_USUARIO";

                valores[0] = idHabilidad;
                valores[1] = nivel;
                valores[2] = Session["IdUser"].ToString();


                data = svcEmpleos.SetHabilidadesUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarIdiomaPerfilUsuario(string idIdioma, string nivel, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(idIdioma) || string.IsNullOrWhiteSpace(idIdioma) ||
                    string.IsNullOrEmpty(nivel) || string.IsNullOrWhiteSpace(nivel))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_IDIOMA";
                parametros[1] = "@NIVEL_IDIOMA";
                parametros[2] = "@ID_USUARIO";

                valores[0] = idIdioma;
                valores[1] = nivel;
                valores[2] = Session["IdUser"].ToString();


                data = svcEmpleos.SetIdiomaUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDatosPerfilUsuario(string nombre1, string nombre2, string apellido1, string apellido2, string telefonoPerfil, string correoPerfil, string descripcionPersonal, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[8];
                string[] valores = new string[8];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(correoPerfil) || string.IsNullOrWhiteSpace(correoPerfil) ||
                    string.IsNullOrEmpty(nombre1) || string.IsNullOrWhiteSpace(nombre1) ||
                    string.IsNullOrEmpty(nombre2) || string.IsNullOrWhiteSpace(nombre2) ||
                    string.IsNullOrEmpty(apellido1) || string.IsNullOrWhiteSpace(apellido1) ||
                    string.IsNullOrEmpty(apellido2) || string.IsNullOrWhiteSpace(apellido2))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@TELEFONO_PERFIL";
                parametros[1] = "@CORREO_PERFIL";
                parametros[2] = "@DESCRIPCION_PERFIL";
                parametros[3] = "@ID_USUARIO";
                parametros[4] = "@NOMBRE1";
                parametros[5] = "@NOMBRE2";
                parametros[6] = "@APELLIDO1";
                parametros[7] = "@APELLIDO2";

                valores[0] = telefonoPerfil;
                valores[1] = correoPerfil;
                valores[2] = descripcionPersonal;
                valores[3] = Session["IdUser"].ToString();
                valores[4] = nombre1;
                valores[5] = nombre2;
                valores[6] = apellido1;
                valores[7] = apellido2;


                data = svcEmpleos.UpdDatosUsuarioPerfil(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarDatosPerfilProfesionalUsuario(string tituloperfil, string descripcionperfil, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(tituloperfil) || string.IsNullOrWhiteSpace(tituloperfil) ||
                    string.IsNullOrEmpty(descripcionperfil) || string.IsNullOrWhiteSpace(descripcionperfil))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@TITULO_PERFIL";
                parametros[1] = "@DESCRIPCION_PERFIL";
                parametros[2] = "@ID_USUARIO";

                valores[0] = tituloperfil;
                valores[1] = descripcionperfil;
                valores[2] = Session["IdUser"].ToString();


                data = svcEmpleos.SetUpdPerfilProfesionalUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult GuardarRespuestaMensajeAReceptor(string idMensaje, string idAutor, string mensaje, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[4];
                string[] valores = new string[4];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(mensaje) || string.IsNullOrWhiteSpace(mensaje))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_MENSAJE";
                parametros[1] = "@ID_RECEPTOR";
                parametros[2] = "@MENSAJE";
                parametros[3] = "@ID_EMISOR";

                valores[0] = idMensaje;
                valores[1] = idAutor;
                valores[2] = mensaje;
                valores[3] = Session["IdUser"].ToString();


                data = svcEmpleos.SetRespuestaMensajeReceptor(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPostulacionEmpleo(string idPublicacion, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[2];
                string[] valores = new string[2];

                DataSet data = new DataSet();

               
                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Configuracion";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                
                parametros[0] = "@ID_USUARIO";
                parametros[1] = "@ID_PUBLICACION";
                valores[0] = Session["IdUser"].ToString();
                valores[1] = idPublicacion;


                data = svcEmpleos.SetPostuloEmpleo(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "NotificacionesM";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "NotificacionesM";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "NotificacionesM";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubirImagenPerfil(HttpPostedFileBase file)
        {
            string Query = "";
            byte[] archivo = null;
            Stream myStream = file.InputStream;
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);
                archivo = ms.ToArray();
            }
            string idUsuario = Session["IdUser"].ToString();
            var existe = GetImagenDePerfilUsuario(idUsuario).Count();

            if (existe == 0)
            {
                Query = "INSERT INTO IMAGEN_PERFIL_USUARIO (ID_USUARIO, NOMBRE_IMAGEN, IMAGEN) VALUES (@ID_USUARIO, @NOMBRE, @IMAGEN)";
            }
            else
            {
                Query = "UPDATE IMAGEN_PERFIL_USUARIO SET NOMBRE_IMAGEN = @NOMBRE, IMAGEN = @IMAGEN WHERE ID_USUARIO = @ID_USUARIO";
            }
            conexion.Open();
            SqlCommand comando = new SqlCommand(Query, conexion);
            comando.Parameters.AddWithValue("@ID_USUARIO", idUsuario);
            comando.Parameters.AddWithValue("@NOMBRE", file.FileName);
            comando.Parameters.AddWithValue("@IMAGEN", archivo);
            comando.ExecuteNonQuery();
            conexion.Close();


            return RedirectToAction("Perfil");
        }
        #endregion
    }
}