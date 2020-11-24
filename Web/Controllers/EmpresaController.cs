using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EmpresaController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";

        #region MetodosVistas

        public ActionResult CandidatosEmpresa()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaCandidatosEmpresa = GetCandidatosEmpresa(idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }
        public ActionResult Configuracion()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.referenciaPreguntas = GetPreguntasPublicacion(idEmpresa);
            ViewBag.PreguntasSeleccionadasEmpresa = GetPreguntasSeleccionEmpresa(idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");

            return View();
        }
        [HttpGet]
        public ActionResult DetallePublicaciones(string id)
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.DetallePublicacion = GetDetallePublicacion(id);
            ViewBag.Candidatos = GetCandidatosPublicacion(id);
            ViewBag.ReferenciaComentarioPubEmpresa = GetComentariosPublicacion(id);
            ViewBag.IdPublicacion = id;
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }
        public ActionResult Index()
        {
            
            ViewBag.Planes = GetPlanes("");
            // Se debe borrar pronto
           
            return View();
        }

        public ActionResult NotificacionesM()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.referenciaNotificaciones = GetNotificacion(idEmpresa);
            ViewBag.referenciaMensajes = GetMensajes(idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }
        public ActionResult PerfilCandidato(string idUsuario)
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaDatosUsuario = GetDatosUsuario(idUsuario);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }
        public ActionResult PerfilEmpresa()
        {
            var empresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaDatosEmpresa = GetDatosEmpresa(empresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(empresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(empresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(empresa, "3");
            return View();
        }
        public ActionResult PerfilUsuarioEmpresaLectura(string idUsuario)
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.PublicacionesUsuarioEmpresa = GetPublicaciones("", idUsuario);
            ViewBag.ReferenciaDatosPerfil = GetDatosUsuarioEmpresa(idUsuario, "");
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");

            return View();
        }
        public ActionResult PlanesEmpresa()
        {
            var empresa = Session["EmpresaID"].ToString();
            ViewBag.PlanesEmpresa = GetPlanes(empresa);
            ViewBag.Planes = GetPlanes("");
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(empresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(empresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(empresa, "3");
            return View();
        }
        public ActionResult Principal(string NombrePublicacion = "", string FechaPublicacion = "", string Ordenamiento = "")
        {
            if (Session["EmpresaID"] != null)
            {
                var empresa = Session["EmpresaID"].ToString();
                //ViewBag.TiposPublicaciones = GetTiposPublicaciones();
                ViewBag.referenciaCandidatosPublicacion = GetUltimosCandidatosEmpresa(empresa);
                if (NombrePublicacion == "" && FechaPublicacion == "")
                {
                    ViewBag.PublicacionesEmpresa = GetPublicaciones(empresa, "");

                }
                else
                {
                    ViewBag.PublicacionesEmpresa = GetPublicacionFiltros(empresa, NombrePublicacion, FechaPublicacion, Ordenamiento);
                }
                // Se debe borrar pronto
                Session["ContadorSolicitudes"] = GetCantidadSMN(empresa, "1");
                Session["ContadorNotificaciones"] = GetCantidadSMN(empresa, "2");
                Session["ContadorMensajes"] = GetCantidadSMN(empresa, "3");
            }
            

            return View();
        }
        public ActionResult Trabajadores()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaTrabajadores = GetTrabajadoresEmpresa("", idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }
        public ActionResult Solicitudes()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaSolicitudes = GetSolicitudesEmpresa(idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            return View();
        }

        #endregion

        #region ObtencionDatos

        public List<CandidatoEmpleos> GetCandidatosEmpresa(string id)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = id;
            DataSet datas = new DataSet();
            List<CandidatoEmpleos> clCandidatos = new List<CandidatoEmpleos>();
            try
            {
                datas = svcEmpleos.GetCandidatosEmpresa(parametros, valores).Table;

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clCandidatos.Add(
                                new CandidatoEmpleos
                                {
                                    RutCandidato = rows["RutCandidato"].ToString(),
                                    NombreCandidato = rows["NombreCandidato"].ToString(),
                                    ApellidosCandidatos = rows["ApellidosCandidatos"].ToString(),
                                    CorreoCandidato = rows["CorreoCandidato"].ToString(),
                                    TituloPostulacion = rows["TituloPostulacion"].ToString(),
                                    FechaSolicitud = rows["FechaSolicitud"].ToString()

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
            catch (Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clCandidatos;
        }
        public List<CandidatoEmpleos> GetCandidatosPublicacion(string id)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = id;
            DataSet datas = new DataSet();
            List<CandidatoEmpleos> clCandidatos = new List<CandidatoEmpleos>();
            try
            {
                datas = svcEmpleos.GetCandidatosPublicacionEmpresa(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clCandidatos.Add(
                                new CandidatoEmpleos
                                {
                                    NombreCandidato = rows["Nombre"].ToString(),
                                    FechaSolicitud = rows["FechaSolicitud"].ToString(),
                                    IdCandidato = rows["IdUsuarioSol"].ToString()
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
            catch (Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clCandidatos;
        }

        public string GetCantidadSMN(string idEmpresa, string tipo)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_EMPRESA";
            parametros[1] = "@TIPO";
            valores[0] = idEmpresa;
            valores[1] = tipo;
            string cantidad = "0";
            DataSet datas = new DataSet();
            try
            {
                datas = svcEmpleos.GetCantidadesSNM(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            cantidad = rows["Cantidad"].ToString();

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
            catch (Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return cantidad;
        }
        public List<ComentariosPubEmpresa> GetComentariosPublicacion(string id)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = id;
            List<ComentariosPubEmpresa> clComentarios = new List<ComentariosPubEmpresa>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetComentariosPublicacion(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clComentarios.Add(
                                new ComentariosPubEmpresa
                                {
                                    Id_PublicacionComen = rows["IdPublicacion"].ToString(),
                                    NombreAutor = rows["AutorComentario"].ToString(),
                                    DescripcionComentario = rows["DescripcionComentario"].ToString(),
                                    FechaComentario = rows["FechaComentario"].ToString()
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
            return clComentarios;
        }
        public List<DatosEmpresa> GetDatosEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<DatosEmpresa> clDatosEmpresa = new List<DatosEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetDatosEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDatosEmpresa.Add(
                                new DatosEmpresa
                                {
                                    correoEmpresa = rows["CorreoEmpresa"].ToString(),
                                    nombreEmpresa = rows["NombreEmpresa"].ToString(),
                                    razonEmpresa = rows["RazonEmpresa"].ToString(),
                                    rutEmpresa = rows["RutEmpresa"].ToString(),
                                    telefonoEmpresa = rows["TelefonoEmpresa"].ToString()
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
            return clDatosEmpresa;
        }
        public List<UsuarioEmpresa> GetDatosUsuarioEmpresa(string idUsuario, string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_USUARIO";
            parametros[1] = "@ID_EMPRESA";
            valores[0] = idUsuario;
            valores[1] = idEmpresa;
            List<UsuarioEmpresa> clDetalleUsuario = new List<UsuarioEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetUsuarioEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetalleUsuario.Add(
                                new UsuarioEmpresa
                                {
                                    RutUsuarioE = rows["RutUsuarioE"].ToString(),
                                    NombreUsuarioE = rows["NombreUsuarioE"].ToString(),
                                    ApellidoUsuarioE = rows["ApellidoUsuarioE"].ToString(),
                                    FechaNacUsuarioE = rows["FechaNacUsuarioE"].ToString(),
                                    TelefonoUsuarioE = rows["TelefonoUsuarioE"].ToString(),
                                    PassUsuarioE = rows["PassUsuarioE"].ToString(),
                                    CorreoUsuarioE = rows["CorreoUsuarioE"].ToString()
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
            return clDetalleUsuario;
        }
        public List<PerfilCandidatoPostulacion> GetDatosUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = idUsuario;
            List<PerfilCandidatoPostulacion> clUsuarioPostulado = new List<PerfilCandidatoPostulacion>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetDetallePublicacion(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clUsuarioPostulado.Add(
                                new PerfilCandidatoPostulacion
                                {
                                    IdUsuario = (int)rows["IdUsuario"],
                                    NombreUsuario = rows["NombreUsuario"].ToString(),
                                    ApellidoPUsuario = rows["ApellidoPUsuario"].ToString(),
                                    ApellidoMUsuario = rows["ApellidoMUsuario"].ToString(),
                                    TelefonoUsuario = rows["TelefonoUsuario"].ToString(),
                                    CorreoUsuario = rows["CorreoUsuario"].ToString()
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
            return clUsuarioPostulado;
        }
        public List<DetallePlan> GetDetallePlanes(string parametro)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PLAN";
            valores[0] = parametro;
            List<DetallePlan> clDetallePlanesEmpresa = new List<DetallePlan>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetDetallePlanes(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetallePlanesEmpresa.Add(
                                new DetallePlan
                                {
                                    NombreDetalle = rows["DetallePlan"].ToString()
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
            return clDetallePlanesEmpresa;
        }
        public List<DetallePublicacion> GetDetallePublicacion(string id)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = id;
            List<DetallePublicacion> clDetallePub = new List<DetallePublicacion>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetDetallePublicacion(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetallePub.Add(
                                new DetallePublicacion
                                {
                                    AutorPublicacion = rows["Autor"].ToString(),
                                    TituloPublicacion = rows["Titulo"].ToString(),
                                    DescripcionPublicacion = rows["Descripcion"].ToString(),
                                    FechaPublicacion = rows["Fecha"].ToString(),
                                    MontoPublicacion = rows["Monto"].ToString(),
                                    IdPublicacion = (int)rows["IdPublicacion"],
                                    EstadoPublicacion = (int)rows["Estado"]
                                });
                            ViewBag.NombrePublicacion = rows["Titulo"].ToString();
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
            return clDetallePub;
        }
        public List<PlanesEmpresa> GetPlanes(string parametro = "")
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = parametro;
            List<PlanesEmpresa> clPlanesEmpresa = new List<PlanesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPlanesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPlanesEmpresa.Add(
                                new PlanesEmpresa
                                {
                                    idPlan = rows["IdPlan"].ToString(),
                                    NombrePlan = rows["NombrePlan"].ToString(),
                                    PrecioPlan = rows["Precio"].ToString(),
                                    detallePlan = GetDetallePlanes(rows["IdPlan"].ToString())
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
            return clPlanesEmpresa;
        }
        public List<MensajesEmpresa> GetMensajes(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<MensajesEmpresa> clMensajesEmpresa = new List<MensajesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetMensajesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clMensajesEmpresa.Add(
                                new MensajesEmpresa
                                {
                                    Mensaje = rows["Mensaje"].ToString(),
                                    AutorMensaje = rows["AutorMensaje"].ToString(),
                                    FechaMensaje = rows["FechaMensaje"].ToString()
                                }) ;


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
            return clMensajesEmpresa;
        }
        public List<NotificacionEmpresa> GetNotificacion(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<NotificacionEmpresa> clNotificaciones = new List<NotificacionEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetNotificacionesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clNotificaciones.Add(
                                new NotificacionEmpresa
                                {
                                    tipoNotificacion = rows["TipoNotificacion"].ToString(),
                                    fechaNotificacion = rows["FechaNotificacion"].ToString()
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
            return clNotificaciones;
        }

        public List<PreguntasPublicacion> GetPreguntasPublicacion(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            DataSet datas = new DataSet();
            List<PreguntasPublicacion> clPreguntasEmp = new List<PreguntasPublicacion>();
            try
            {
                datas = svcEmpleos.GetPreguntasEmpresa(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPreguntasEmp.Add(
                                new PreguntasPublicacion
                                {
                                    idPregunta = (int)rows["IdPregunta"],
                                    NombrePregunta = rows["NombrePregunta"].ToString()
                                }); ;

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
            catch (Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clPreguntasEmp;
        }
        public List<PreguntasSeleccionadas> GetPreguntasSeleccionEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<PreguntasSeleccionadas> clPreguntasSeleccionadas = new List<PreguntasSeleccionadas>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPreguntasSeleccionadasEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPreguntasSeleccionadas.Add(
                                new PreguntasSeleccionadas
                                {
                                    idPreguntaSeleccionada = (int)rows["IdPregunta"],
                                    NombrePreguntaSeleccionada = rows["NombrePregunta"].ToString(),
                                    EstadoPreguntaSeleccionada = (int)rows["EstadoPregunta"]
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
            return clPreguntasSeleccionadas;
        }
        public List<DetallePublicacion> GetPublicaciones(string parametro, string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@IDL_EMPRESA";
            parametros[1] = "@ID_LOGINUSUARIO";
            valores[0] = parametro;
            valores[1] = idUsuario;
            List<DetallePublicacion> clPublicacionEmpresa = new List<DetallePublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPublicacionesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    if (valores[0] != "" && valores[1] == "")
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
                                        EstadoPublicacion = (int)rows["Estado"]
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
                    else
                    {
                        switch (rows["Code"].ToString())
                        {
                            case "200":
                                clPublicacionEmpresa.Add(
                                    new DetallePublicacion
                                    {
                                        TituloPublicacion = rows["Titulo"].ToString(),
                                        IdPublicacion = (int)rows["IdPublicacion"]

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
            }
            catch (Exception ex)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clPublicacionEmpresa;
        }
        public List<DetallePublicacion> GetPublicacionFiltros(string idEmpresa, string nombrePub, string fechaPub, string ordeno)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[4];
            string[] valores = new string[4];

            parametros[0] = "@IDL_EMPRESA";
            parametros[1] = "@NOMBREPUB_FILTRO";
            parametros[2] = "@FECHA_FILTRO";
            parametros[3] = "@ORDENAMIENTO";
            valores[0] = idEmpresa;
            valores[1] = nombrePub;
            valores[2] = fechaPub;
            valores[3] = ordeno;
            List<DetallePublicacion> clPublicacionEmpresa = new List<DetallePublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPublicacionesEmpresaFiltro(parametros, valores).Table;
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
                                    EstadoPublicacion = (int)rows["Estado"]
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
        public List<SolicitudesEmpresaPublicacion> GetSolicitudesEmpresa(string parametro)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = parametro;
            List<SolicitudesEmpresaPublicacion> clSolicitudes = new List<SolicitudesEmpresaPublicacion>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetSolicitudesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clSolicitudes.Add(
                                new SolicitudesEmpresaPublicacion
                                {
                                    IdSolicitante = rows["IdSolicitante"].ToString(),
                                    RutSolicitante = rows["RutSolicitante"].ToString(),
                                    EstadoSolicitud = rows["EstadoSolicitud"].ToString(),
                                    FechaSolicitud = rows["FechaSolicitud"].ToString(),
                                    PublicacionSolicitud = rows["PublicacionSolicitud"].ToString()
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
        public List<DetalleTrabajadores> GetTrabajadoresEmpresa(string idUsuario, string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_USUARIO";
            parametros[1] = "@ID_EMPRESA";
            valores[0] = idUsuario;
            valores[1] = idEmpresa;
            List<DetalleTrabajadores> clDetalleUsuario = new List<DetalleTrabajadores>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetUsuarioEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetalleUsuario.Add(
                                new DetalleTrabajadores
                                {
                                    RutUsuarioE = rows["RutUsuarioE"].ToString(),
                                    ApellidoUsuarioE = rows["ApellidoUsuarioE"].ToString(),
                                    NombreUsuarioE = rows["NombreUsuarioE"].ToString(),
                                    IdUsuarioE = rows["IdUsuarioE"].ToString()
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
            return clDetalleUsuario;
        }
        public List<CandidatoEmpleos> GetUltimosCandidatosEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            DataSet datas = new DataSet();
            List<CandidatoEmpleos> clUltimosC = new List<CandidatoEmpleos>();
            try
            {
                datas = svcEmpleos.GetUltimosCandidatosEmpresa(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clUltimosC.Add(
                                new CandidatoEmpleos
                                {
                                    NombreCandidato = rows["NombreCandidato"].ToString(),
                                    FechaSolicitud = rows["FechaCandidato"].ToString(),
                                    CorreoCandidato = rows["CorreoCandidato"].ToString()
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
            catch (Exception)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clUltimosC;
        }
        #endregion

        #region InicioSesion
        [HttpPost]
        public JsonResult InicioSesion(string usuario, string clave)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];

            Empresa clEmpresa = new Empresa();

            try
            {
                DataSet data = new DataSet();

                parametros[0] = "@USUARIO";
                parametros[1] = "@PASSWORD";

                valores[0] = usuario;
                valores[1] = clave;

                /* VALIDAMOS SI ES EMPRESA */

                data = svcEmpleos.ValEmpresa(parametros, valores).Table;
                //data = svcEmpleos.ValEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clEmpresa.RutEmpresa = rows["Rut"].ToString();
                            clEmpresa.CorreoEmpresa = rows["Correo"].ToString();
                            code = rows["Code"].ToString();
                            mensaje = "";
                            Session["EmpresaID"] = rows["IdEmpresa"].ToString();
                            Session["NombreEmpresa"] = rows["Nombre"].ToString();
                            Session["ContadorSolicitudes"] = GetCantidadSMN(rows["IdEmpresa"].ToString(), "1");
                            Session["ContadorNotificaciones"] = GetCantidadSMN(rows["IdEmpresa"].ToString(), "2");
                            Session["ContadorMensajes"] = GetCantidadSMN(rows["IdEmpresa"].ToString(), "3");
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
            return Json(new { Code = code, Message = mensaje, Empresa = clEmpresa });
        }
        #endregion
       
        #region cerrarSesion
        [HttpPost]
        public JsonResult CerrarSesion()
        {
            var retorno = string.Empty;
            Session.Clear();

            retorno = "/Empresa/Index"; // ModuleControlRetorno() + "/Index";

            return Json(new { Retorno = retorno });
        }
        #endregion

        #region AccionesSistema
        public ActionResult ActivarDesactivarPregunta(string idPregunta, string estado)
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[2];
                string[] valores = new string[2];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();


                parametros[0] = "@ID_PREGUNTA";
                parametros[1] = "@ESTADO";


                valores[0] = idPregunta;
                valores[1] = estado;


                data = svcEmpleos.UpdADPreguntaEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Configuracion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Configuracion";
                            break;
                        case "500":
                            view = "Configuracion";
                            break;
                        default:
                            view = "Configuracion";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Configuracion";
            }

            return RedirectToAction(view);
        }
        public ActionResult ActivarDesactivarPub(string idPublicacion, string estado)
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[2];
                string[] valores = new string[2];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();


                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@ESTADO";


                valores[0] = idPublicacion;
                valores[1] = estado;


                data = svcEmpleos.UpdADPublicacionEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Principal";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Principal";
                            break;
                        case "500":
                            view = "Principal";
                            break;
                        default:
                            view = "Principal";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Principal";
            }

            return RedirectToAction(view);
        }
        public ActionResult ActualizarPublicacion(string id, string descripcion, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[2];
                string[] valores = new string[2];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(descripcion) || string.IsNullOrWhiteSpace(descripcion))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Principal";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Principal";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@DESCRIPCION";


                valores[0] = id;
                valores[1] = descripcion.Trim();


                data = svcEmpleos.UpdPublicacionEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Principal";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Principal";
                            break;
                        case "500":
                            view = "Principal";
                            break;
                        default:
                            view = "Principal";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Principal";
            }

            return RedirectToAction(view);
        }
        [HttpPost]
        public JsonResult GuardarAsignacionPreguntas(string idPregunta, string error = "")
        {
            string view = string.Empty;
            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(idPregunta) || string.IsNullOrWhiteSpace(idPregunta))
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

                parametros[0] = "@ID_PREGUNTA";
                parametros[1] = "@ID_EMPRESA";
                parametros[2] = "@ESTADO_PREGUNTA";

                valores[0] = idPregunta;
                valores[1] = Session["EmpresaID"].ToString();
                valores[2] = "1";

                data = svcEmpleos.SetPreguntasSeleccionadasEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Configuracion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Configuracion";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Configuracion";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Configuracion";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Configuracion";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GuardarVotacionPublicacion(string votacion, string idPublicacion, string idUsuario, string error = "")
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
                valores[2] = idUsuario;


                data = svcEmpleos.SetUpdVotacionPublicacion(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "GetDetallePublicacion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "GetDetallePublicacion";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "GetDetallePublicacion";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "GetDetallePublicacion";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "GetDetallePublicacion";
            }

            return View(view, new { id = idPublicacion });
        }

        [HttpPost]
        public ActionResult GuardarComentario(string IdUsuario, string Id_Publicacion, string Comentario, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(IdUsuario) || string.IsNullOrWhiteSpace(IdUsuario) ||
                    string.IsNullOrEmpty(Id_Publicacion) || string.IsNullOrWhiteSpace(Id_Publicacion) ||
                    string.IsNullOrEmpty(Comentario) || string.IsNullOrWhiteSpace(Comentario)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "GetDetallePublicacion";//"App/_ModalMensajeError";
                    return View(view, new { id = Id_Publicacion });
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "GetDetallePublicacion";//"App/_ModalMensajeError";
                    return View(view, new { id = Id_Publicacion });
                }

                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@DESCRIPCION_COMENTARIO";
                parametros[2] = "@ID_USUARIO";


                valores[0] = Id_Publicacion;
                valores[1] = Comentario;
                valores[2] = IdUsuario;


                data = svcEmpleos.SetComentarioPublicacion(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "GetDetallePublicacion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "GetDetallePublicacion";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "GetDetallePublicacion";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "GetDetallePublicacion";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "GetDetallePublicacion";
            }

            return View(view, new { id = Id_Publicacion });
        }
        [HttpPost]
        public ActionResult GuardarEmpresa(string rut, string nombre, string razonsocial, string correo, string clave, string telefono, string direccion, string repetirclave, string rubro, string comuna, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[13];
                string[] valores = new string[13];

                // Variables vacias
                //string razonsocial = string.Empty;
                string predeterminada = "1";
                string observacion = string.Empty;

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(rut) || string.IsNullOrWhiteSpace(rut) ||
                    string.IsNullOrEmpty(nombre) || string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrEmpty(rubro) || string.IsNullOrWhiteSpace(rubro) ||
                    string.IsNullOrEmpty(correo) || string.IsNullOrWhiteSpace(correo) ||
                    string.IsNullOrEmpty(clave) || string.IsNullOrWhiteSpace(clave) ||
                    string.IsNullOrEmpty(telefono) || string.IsNullOrWhiteSpace(telefono) ||
                    string.IsNullOrEmpty(comuna) || string.IsNullOrWhiteSpace(comuna) ||
                    string.IsNullOrEmpty(direccion) || string.IsNullOrWhiteSpace(direccion) ||
                    string.IsNullOrEmpty(razonsocial) || string.IsNullOrWhiteSpace(razonsocial) ||
                    string.IsNullOrEmpty(repetirclave) || string.IsNullOrWhiteSpace(repetirclave))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Index";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "/Empresa/Registro";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                parametros[0] = "@RUT";
                parametros[1] = "@CORREO";
                parametros[2] = "@REPETIRCORREO";
                parametros[3] = "@PASSWORD";
                parametros[4] = "@REPETIRPASSWORD";
                parametros[5] = "@RUBRO";
                parametros[6] = "@NOMBRE";
                parametros[7] = "@RAZONSOCIAL";
                parametros[8] = "@TELEFONO";
                parametros[9] = "@DIRECCION";
                parametros[10] = "@PREDETERMINADA";
                parametros[11] = "@COMUNA";
                parametros[12] = "@OBSERVACION";


                valores[0] = rut;
                valores[1] = correo;
                valores[2] = correo;
                valores[3] = clave;
                valores[4] = repetirclave;
                valores[5] = rubro;
                valores[6] = nombre;
                valores[7] = razonsocial;
                valores[8] = telefono;
                valores[9] = direccion;
                valores[10] = predeterminada;
                valores[11] = comuna;
                valores[12] = observacion;

                data = svcEmpleos.SetEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Index";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Index";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Index";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Index";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Index";
            }

            return PartialView(view);
        }
        [HttpPost]
        public JsonResult GuardarPreguntaPostulacion(string nombrePregunta, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[4];
                string[] valores = new string[4];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(nombrePregunta) || string.IsNullOrWhiteSpace(nombrePregunta))
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

                parametros[0] = "@ID_EMPRESA";
                parametros[1] = "@NOMBRE";
                parametros[2] = "@FECHA";
                parametros[3] = "@ESTADO";

                valores[0] = Session["EmpresaID"].ToString(); ;
                valores[1] = nombrePregunta;
                valores[2] = DateTime.Now.ToString();
                valores[3] = "1";


                data = svcEmpleos.SetPreguntasEmpresaPostulacion(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Configuracion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Configuracion";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Configuracion";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Configuracion";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Configuracion";
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GuardarPublicacion(string titulo, string id_empresa, string id_usuario, string tipo, string descripcionNuevaPub, string monto, string discapacidad,
        string area, string subarea, string error = "")
        {
            string view = string.Empty;
            string estado = "1";
            if (discapacidad == null)
            {
                discapacidad = "0";
            }

            if (id_usuario == "")
            {
                id_usuario = id_empresa;
            }
            string fecha_max = DateTime.Today.AddDays(30).ToString();
            try
            {
                string[] parametros = new string[10];
                string[] valores = new string[10];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(id_usuario) || string.IsNullOrWhiteSpace(id_usuario) ||
                    string.IsNullOrEmpty(titulo) || string.IsNullOrWhiteSpace(titulo) ||
                    string.IsNullOrEmpty(descripcionNuevaPub) || string.IsNullOrWhiteSpace(descripcionNuevaPub) ||
                    string.IsNullOrEmpty(tipo) || string.IsNullOrWhiteSpace(tipo) ||
                    string.IsNullOrEmpty(monto) || string.IsNullOrWhiteSpace(monto) ||
                    string.IsNullOrEmpty(subarea) || string.IsNullOrWhiteSpace(subarea)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Principal";//"App/_ModalMensajeError";
                    return View(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Principal";//"App/_ModalMensajeError";
                    return View(view);
                }

                parametros[0] = "@ID_LOGINEMPRESA";
                parametros[1] = "@ID_EMPRESA";
                parametros[2] = "@TITULO";
                parametros[3] = "@DESCRIPCION";
                parametros[4] = "@TIPO";
                parametros[5] = "@MONTO";
                parametros[6] = "@DISCAPACIDAD";
                parametros[7] = "@SUBAREA";
                parametros[8] = "@ESTADO";
                parametros[9] = "@FECHA_MAX";

                valores[0] = id_usuario;
                valores[1] = id_empresa;
                valores[2] = titulo;
                valores[3] = descripcionNuevaPub;
                valores[4] = tipo;
                valores[5] = monto;
                valores[6] = discapacidad;
                valores[7] = subarea;
                valores[8] = estado;
                valores[9] = fecha_max;

                data = svcEmpleos.SetPublicacionesEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Principal";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "Principal";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Principal";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "Principal";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Principal";
            }

            return RedirectToAction(view);
        }
        [HttpPost]
        public ActionResult GuardarUsuarioEmpresa(string id_empresa, string rut, string nombres, string apellido, string perfil, string correo, string password, string passwordRepetir, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[7];
                string[] valores = new string[7];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(rut) || string.IsNullOrWhiteSpace(rut) ||
                    string.IsNullOrEmpty(nombres) || string.IsNullOrWhiteSpace(nombres) ||
                    string.IsNullOrEmpty(correo) || string.IsNullOrWhiteSpace(correo) ||
                    string.IsNullOrEmpty(apellido) || string.IsNullOrWhiteSpace(apellido) ||
                    string.IsNullOrEmpty(perfil) || string.IsNullOrWhiteSpace(perfil) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrEmpty(passwordRepetir) || string.IsNullOrWhiteSpace(passwordRepetir))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Trabajadores";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Trabajadores";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                parametros[0] = "@RUT";
                parametros[1] = "@CORREO";
                parametros[2] = "@PASSWORD";
                parametros[3] = "@NOMBRE";
                parametros[4] = "@PERFIL";
                parametros[5] = "@APELLIDO";
                parametros[6] = "@ID_EMPRESA";


                valores[0] = rut;
                valores[1] = correo;
                valores[2] = password;
                valores[3] = nombres;
                valores[4] = perfil;
                valores[5] = apellido;
                valores[6] = id_empresa;

                data = svcEmpleos.SetUsuarioEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Trabajadores";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Trabajadores";
                            break;
                        case "500":
                            view = "Trabajadores";
                            break;
                        default:
                            view = "Trabajadores";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Trabajadores";
            }

            return PartialView(view);
        }
        
        public ActionResult ProcesoSeleccion(string idUsuario, string idEmpresa, string idPublicacion)
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();


                parametros[0] = "@ID_USUARIO";
                parametros[1] = "@ID_EMPRESA";
                parametros[2] = "@ID_PUBLICACION";


                valores[0] = idUsuario;
                valores[1] = idEmpresa;
                valores[2] = idPublicacion;


                //data = svcEmpleos.GuardarSeleccionUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Principal";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Principal";
                            break;
                        case "500":
                            view = "Configuracion";
                            break;
                        default:
                            view = "Principal";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "Principal";
            }

            return RedirectToAction(view);
        }
        
        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult SubirImagenes(HttpPostedFileBase file)
        {
           
            var empresa = Session["EmpresaID"].ToString();
            try
            {
                string[] parametros = new string[4];
                string[] valores = new string[4];

                DataSet data = new DataSet();

                byte[] dato = null;
                Stream myStream = file.InputStream;
                using (MemoryStream ms = new MemoryStream())
                {
                    myStream.CopyTo(ms);
                    dato = ms.ToArray();
                }

                

                parametros[0] = "@IMAGEN";
                parametros[1] = "@ID_EMPRESA";
                parametros[2] = "@NOMBREIMG";
                parametros[3] = "@FECHA";



                valores[0] = dato.ToString();
                valores[1] = empresa;
                valores[2] = file.FileName;
                valores[3] = DateTime.Now.ToString();


                data = svcEmpleos.SetImagenesEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                         
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                          
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                          
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                           
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                
            }


            return RedirectToAction("PerfilEmpresa");
        }
        #endregion

        #region ControlRetorno
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
        #endregion

    }
}