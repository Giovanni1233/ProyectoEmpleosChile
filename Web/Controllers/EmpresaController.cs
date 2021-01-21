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
    public class EmpresaController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        SqlConnection conexion = new SqlConnection("server=GIOVANNIDIAZF; database=EMPLEOSCHILE; integrated security=true");
        List<HistorialMensajesEmpresa> historial = new List<HistorialMensajesEmpresa>();

        #region MetodosVistas
        public ActionResult CandidatosEmpresa()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaCandidatosEmpresa = GetCandidatosEmpresa(idEmpresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.Planes = GetPlanes("");
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
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.PreguntasPorPublicacionTodas = GetPreguntasPorPublicacionTodas(idEmpresa);
            ViewBag.PublicacionesEmpresa = GetPublicaciones(idEmpresa, "");
            ViewBag.Planes = GetPlanes("");

            return View();
        }
        public ActionResult DetalleTestUsuario(string idUsuario)
        {
            // ResultadosTest
            ViewBag.referenciaTestUsuario = GetResultadosTestUsuario(idUsuario);

            foreach (var item in ViewBag.referenciaTestUsuario)
            {
                ViewBag.referenciaResponsabilidad = item.Responsabilidad;
                ViewBag.referenciaRestoResponsabilidad = item.RestoResponsabilidad;
                ViewBag.referenciaAutoGestion = item.AutoGestion;
                ViewBag.referenciaRestoAutogestion = item.RestoAutogestion;
                ViewBag.referenciaLiderazgo = item.Liderazgo;
                ViewBag.referenciaRestoLiderazgo = item.RestoLiderazgo;
            }
            // Detalle respuestas test
            ViewBag.referenciaRespuestasTest = GetRespuestasTestUsuario(idUsuario);
            ViewBag.referenciaIdUsuario = idUsuario;
            return View();
        }
        public ActionResult Index()
        {

            ViewBag.Planes = GetPlanes("");
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa("Inicio");
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas("", "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas("", "2");
            // Se debe borrar pronto


            // Imagenes a cargar
            ViewBag.ImagenesAfuera = GetImagenesAfuera();

            return View();
        }
        public ActionResult NotificacionesM(string idMensaje = "0", string idReceptor = "0")
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.referenciaNotificaciones = GetNotificacion(idEmpresa);
            ViewBag.referenciaMensajes = GetMensajes(idEmpresa);
            ViewBag.referenciaHistorialConversacion = GetHistorialConversacion(idMensaje, idEmpresa);
            ViewBag.referenciaIdMensaje = idMensaje;
            ViewBag.idMensaje = idMensaje;
            ViewBag.idReceptor = idReceptor;
            ViewBag.referenciadetalleMensaje = GetDetalleMensajeE(idMensaje);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.Planes = GetPlanes("");
            return View();
        }
        public ActionResult PerfilCandidato(string idUsuario)
        {
            UsuarioController usuarioController = new UsuarioController();
            OficiosController ofi = new OficiosController();
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaDatosUsuario = GetDatosUsuario(idUsuario);
            ViewBag.ReferenciaHabilidadesUsuario = GetHabilidadesUsuario(idUsuario);
            ViewBag.ReferenciaExperienciasUsuario = GetExperienciasUsuario(idUsuario);
            ViewBag.ReferenciaIdiomasUsuario = GetIdiomasUsuario(idUsuario);
            ViewBag.ReferenciaEducacionUsuario = GetEducacionUsuario(idUsuario);
            ViewBag.referenciaperfilUser = usuarioController.GetPerfilProfesionalUsuario(idUsuario);
            foreach (var item in ViewBag.referenciaperfilUser)
            {
                ViewBag.referenciatituloperfilUsuario = item.TituloPerfil;
                ViewBag.referenciaDetallePerfil = item.DescripcionPerfil;
            }

            ViewBag.referenciaImagenPerfil = usuarioController.GetImagenDePerfilUsuario(idUsuario);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(idEmpresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(idEmpresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(idEmpresa, "3");
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();

            ViewBag.DetallePreguntasRespuestas = GetRespuestasPublicacion(idUsuario);
            ViewBag.Planes = GetPlanes("");

            // ResultadosTest
            ViewBag.referenciaTestUsuario = GetResultadosTestUsuario(idUsuario);

            foreach (var item in ViewBag.referenciaTestUsuario)
            {
                ViewBag.referenciaResponsabilidad = item.Responsabilidad;
                ViewBag.referenciaRestoResponsabilidad = item.RestoResponsabilidad;
                ViewBag.referenciaAutoGestion = item.AutoGestion;
                ViewBag.referenciaRestoAutogestion = item.RestoAutogestion;
                ViewBag.referenciaLiderazgo = item.Liderazgo;
                ViewBag.referenciaRestoLiderazgo = item.RestoLiderazgo;
            }
            ViewBag.referenciaIdUsuario = idUsuario;

            // oficios
            ViewBag.referenciaOficiosUsuario = GetOficiosUsuario(idUsuario);
            ViewBag.referenciaComentariosUsuario = ofi.GetComentariosOficio(idUsuario);

            // Subir CV
            var data = GetCurriculum(idUsuario);
            if (data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaIdUser = idUsuario;
                            ViewBag.ReferenciaDocumento = rows["Documento"].ToString();
                            ViewBag.ReferenciaUrlCV = ModuleControlRetorno() + "/Empresa/DownloadCV?url=" + rows["Url"].ToString() + "&documento=" + rows["Documento"].ToString();
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

            return View();
        }
        public ActionResult PerfilEmpresa(string idEmpresa = "")
        {
            var empresa = "";
            if (idEmpresa == "" && Session["EmpresaID"].ToString() == "")
            {
                RedirectToAction("CerrarSesion");
            }
            else
            {
                if (idEmpresa == "")
                {
                    empresa = Session["EmpresaID"].ToString();
                }
                else
                {
                    empresa = idEmpresa;
                }

            }
            ViewBag.ReferenciaDatosEmpresa = GetDatosEmpresa(empresa);
            // Se debe borrar pronto
            Session["ContadorSolicitudes"] = GetCantidadSMN(empresa, "1");
            Session["ContadorNotificaciones"] = GetCantidadSMN(empresa, "2");
            Session["ContadorMensajes"] = GetCantidadSMN(empresa, "3");
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(empresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(empresa, "").Count();
            ViewBag.ReferenciaTarjetaEmpresa = GetTarjetasEmpresa(empresa);
            ViewBag.ReferenciaImagenesEmpresa = GetImagenesBannerEmpresa(empresa);
            ViewBag.ReferenciaImagenPerfilEmpresa = GetImagenDePerfilEmpresa(empresa);
            ViewBag.Planes = GetPlanes("");
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
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.Planes = GetPlanes("");

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
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(empresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(empresa, "").Count();
            return View();
        }
        public ActionResult Principal(string NombrePublicacion = "", string FechaPublicacion = "", string Ordenamiento = "", string IdPublicacion = "0")
        {
            if (Session["EmpresaID"] != null)
            {
                var empresa = Session["EmpresaID"].ToString();
                //ViewBag.TiposPublicaciones = GetTiposPublicaciones();
                ViewBag.referenciaCandidatosPublicacion = GetUltimosCandidatosEmpresa(empresa);

                ViewBag.PublicacionesEmpresa = GetPublicacionFiltros(empresa, NombrePublicacion, FechaPublicacion, Ordenamiento, "");

                // Detalle publicacion
                ViewBag.DetallePublicacionContador = GetDetallePublicacion(IdPublicacion).Count();
                ViewBag.DetallePublicacion = GetDetallePublicacion(IdPublicacion);
                ViewBag.Candidatos = GetCandidatosPublicacion(IdPublicacion);
                ViewBag.ReferenciaComentarioPubEmpresa = GetComentariosPublicacion(IdPublicacion);
                ViewBag.IdPublicacion = IdPublicacion;
                ViewBag.VotoRealizado = GetVotoPorUsuario(empresa, IdPublicacion);

                // Foreach para obtener total votos y promedio de votos
                foreach (var item in ViewBag.DetallePublicacion)
                {
                    ViewBag.ContadorVotos = item.ContadorVotos;
                    ViewBag.PromedioVotos = item.PromedioVotos;
                }

                ViewBag.PreguntasPorPublicacionId = GetPreguntasPorPublicacionId(IdPublicacion);
                // Planes
                ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(empresa);
                ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "1");
                ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(empresa, "2");
                ViewBag.referenciaContadorPublicaciones = GetPublicaciones(empresa, "").Count();
                ViewBag.Planes = GetPlanes("");
            }
            else
            {
                ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa("Inicio");
                ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas("", "1");
                ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas("", "2");
                ViewBag.referenciaContadorPublicaciones = 0;

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
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorUsuarioEmpresaNuevo = GetContadorUsuarioEmpresa(idEmpresa);
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.Planes = GetPlanes("");
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
            ViewBag.referenciaPlanEmpresa = GetPlanesContratadosEmpresa(idEmpresa);
            ViewBag.PublicacionesPermitidasEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "1");
            ViewBag.TrabajadoresPermitidosEmpresa = GetCandiPubliTrabaPreguntPermitidas(idEmpresa, "2");
            ViewBag.referenciaContadorPublicaciones = GetPublicaciones(idEmpresa, "").Count();
            ViewBag.Planes = GetPlanes("");
            return View();
        }
        public ActionResult PagosEmpresa()
        {
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.PagosRealizadosEmpresa = GetPagosEmpresa(idEmpresa);
            ViewBag.DevolucionesEmpresa = GetDevolucionesEmpresa(idEmpresa);
            return View();
        }
        public ActionResult UsuariosOficios()
        {
            ViewBag.referenciaUsuariosOficios = GetUsuariosConOficio();
            return View();
        }
        #endregion

        #region ObtencionDatos
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
            ImagenUsuarioPerfil imagenPerfilUser = new ImagenUsuarioPerfil();
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
                                    RutCandidato = rows["Rut"].ToString(),
                                    NombreCandidato = rows["Nombre"].ToString(),
                                    ApellidosCandidatos = rows["Apellido"].ToString(),
                                    CorreoCandidato = rows["Correo"].ToString(),
                                    TituloPostulacion = rows["TituloPublicacion"].ToString(),
                                    FechaSolicitud = rows["FechaSolicitud"].ToString(),
                                    IdCandidato = rows["IdUsuarioSol"].ToString(),
                                    ImagendelUsuario = (byte[])rows["Imagen"],
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
                                    NombreCandidato = rows["NombreCandidato"].ToString(),
                                    FechaSolicitud = rows["FechaPostulacion"].ToString(),
                                    IdCandidato = rows["Id_Candidato"].ToString(),
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
        public int GetContadorUsuarioEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            var contador = 0;

            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<DetalleTrabajadores> clDetalleUsuario = new List<DetalleTrabajadores>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetContadorUsuarioEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            contador = (int)rows["Contador"];

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
            return contador;
        }
        public DataSet GetCurriculum(string user)
        {
            string[] parametros = new string[1];
            string[] valores = new string[1];
            DataSet data = new DataSet();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = user;

                data = svcEmpleos.GetCurriculum(parametros, valores).Table;

            }
            catch (Exception ex)
            {

            }
            return data;
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
                                    CorreoUsuarioE = rows["CorreoUsuarioE"].ToString(),
                                    ImagendelUsuarioE = (byte[])rows["ImagendelUsuarioE"]
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
            parametros[0] = "@ID_LOGINUSUARIO";
            valores[0] = idUsuario;
            List<PerfilCandidatoPostulacion> clUsuarioPostulado = new List<PerfilCandidatoPostulacion>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetDatosUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clUsuarioPostulado.Add(
                                new PerfilCandidatoPostulacion
                                {
                                    IdUsuario = rows["IdUsuario"].ToString(),
                                    NombreUsuario = rows["NombreUsuario"].ToString(),
                                    ApellidoPUsuario = rows["ApellidoPUsuario"].ToString(),
                                    ApellidoMUsuario = rows["ApellidoMUsuario"].ToString(),
                                    TelefonoUsuario = rows["TelefonoUsuario"].ToString(),
                                    CorreoUsuario = rows["CorreoUsuario"].ToString(),
                                    RutUsuario = rows["RutUsuario"].ToString()
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
        public List<MensajesEmpresa> GetDetalleMensajeE(string idMensaje)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_MENSAJE";
            valores[0] = idMensaje;
            List<MensajesEmpresa> clDetalleMensaje = new List<MensajesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetDetalleMensajeDelUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetalleMensaje.Add(
                                new MensajesEmpresa
                                {
                                    Mensaje = rows["Mensaje"].ToString(),
                                    AutorMensaje = rows["AutorMensaje"].ToString(),
                                    FechaMensaje = rows["FechaMensaje"].ToString(),
                                    idMensaje = rows["IdMensaje"].ToString(),
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
            return clDetalleMensaje;
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
                                    NombreDetalle = rows["DetallePlan"].ToString(),
                                    CantidadPlan = rows["Cantidad"].ToString()
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
                                    DescripcionPublicacion = rows["Descripcion"].ToString().Trim(),
                                    FechaPublicacion = rows["Fecha"].ToString(),
                                    MontoPublicacion = rows["Monto"].ToString(),
                                    IdPublicacion = (int)rows["IdPublicacion"],
                                    EstadoPublicacion = (int)rows["Estado"],
                                    ContadorVotos = rows["ContadorVotos"].ToString(),
                                    PromedioVotos = rows["PromedioVotos"].ToString(),
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
        public List<DetalleRespuestasTest> GetRespuestasTestUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetalleRespuestasTest> cldetalleTest = new List<DetalleRespuestasTest>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetRespuestasTestUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            cldetalleTest.Add(
                                new DetalleRespuestasTest
                                {
                                    DetallePregunta =  rows["DetallePregunta"].ToString(),
                                    RespuestaPregunta = rows["RespuestaPregunta"].ToString(),
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
            return cldetalleTest;
        }
        public List<DevolucionEmpresa> GetDevolucionesEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<DevolucionEmpresa> clDevolucionEmpresa = new List<DevolucionEmpresa>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetDevolucionEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDevolucionEmpresa.Add(
                                new DevolucionEmpresa
                                {
                                    MontoDevolucion = rows["MontoDevolucion"].ToString(),
                                    NumeroTarjeta = rows["NumeroTarjeta"].ToString(),
                                    Plan_Actual = rows["PlanActual"].ToString(),
                                    Plan_Anterior = rows["PlanAnterior"].ToString()
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
            return clDevolucionEmpresa;
        }
        public List<EducacionUsuario> GetEducacionUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_LOGINUSR";
            valores[0] = idUsuario;
            List<EducacionUsuario> clEducacionUsuario = new List<EducacionUsuario>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetEducacionesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clEducacionUsuario.Add(
                                new EducacionUsuario
                                {
                                    TituloEducacion = rows["TituloEducacion"].ToString(),
                                    DescripcionEducacion = rows["DescripcionEducacion"].ToString(),
                                    InstitucionEducacion = rows["InstitucionEducacion"].ToString(),
                                    FechaDesdeEducacion = rows["FechaDesde_Educacion"].ToString(),
                                    FechaHastaEducacion = rows["FechaHasta_Educacion"].ToString(),
                                    IdEducacion = rows["IdEducacion"].ToString()
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
            return clEducacionUsuario;
        }
        public List<EmpresasPlanVigentes> GetEmpresasPlanesVigente()
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            List<EmpresasPlanVigentes> clEmpresaPlanVigente = new List<EmpresasPlanVigentes>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetEmpresasPlanesVigentes().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clEmpresaPlanVigente.Add(
                                new EmpresasPlanVigentes
                                {
                                    NombreEmpresa = rows["NombreEmpresa"].ToString(),
                                    ImagenEmpresa = (byte[])rows["ImagenEmpresa"],
                                    idEmpresa = rows["IdEmpresa"].ToString()
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
            return clEmpresaPlanVigente;
        }
        public List<ExperienciaUsuario> GetExperienciasUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_LOGINUSUARIO";
            valores[0] = idUsuario;
            List<ExperienciaUsuario> clExperienciaUsuario = new List<ExperienciaUsuario>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetExperienciaUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clExperienciaUsuario.Add(
                                new ExperienciaUsuario
                                {
                                    Nombre = rows["Nombre"].ToString(),
                                    Descripcion = rows["Descripcion"].ToString(),
                                    Fecha_Inicio = rows["Fecha_Inicio"].ToString(),
                                    Fecha_Termino = rows["Fecha_Termino"].ToString(),
                                    IdExperiencia = rows["IdExperiencia"].ToString()
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
            return clExperienciaUsuario;
        }
        public string GetFechaTerminoPlan(string idEmpresa, string idPlan)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_EMPRESA";
            parametros[1] = "@ID_PLAN";
            valores[0] = idEmpresa;
            valores[1] = idPlan;
            string fecha = "";

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetFechaTerminoPlan(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            fecha = rows["Fecha_Termino"].ToString();
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
            return fecha;
        }
        public List<HabilidadesUsuario> GetHabilidadesUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_LOGINUSUARIO";
            valores[0] = idUsuario;
            List<HabilidadesUsuario> clHabilidadesUsuario = new List<HabilidadesUsuario>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetHabilidadesUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clHabilidadesUsuario.Add(
                                new HabilidadesUsuario
                                {
                                    HabilidadNombre = rows["Habilidades"].ToString(),
                                    HabilidadNivel = rows["HabilidadNivel"].ToString(),
                                    HabilidadId = rows["HabilidadId"].ToString(),
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
            return clHabilidadesUsuario;
        }
        public List<HistorialMensajesEmpresa> GetHistorialConversacion(string idMensaje, string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_MENSAJE";
            parametros[1] = "@ID_EMPRESA";
            valores[0] = idMensaje;
            valores[1] = idEmpresa;
            List<HistorialMensajesEmpresa> clHistorialMensaje = new List<HistorialMensajesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetHistorialMensajesEmpresa(parametros, valores).Table;
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
        public List<IdiomasUsuario> GetIdiomasUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_LOGINUSUARIO";
            valores[0] = idUsuario;
            List<IdiomasUsuario> clIdiomasUsuario = new List<IdiomasUsuario>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetIdiomasUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clIdiomasUsuario.Add(
                                new IdiomasUsuario
                                {
                                    NivelIdioma = rows["NivelIdioma"].ToString(),
                                    NombreIdioma = rows["NombreIdioma"].ToString(),
                                    idIdioma = rows["idIdioma"].ToString()
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
            return clIdiomasUsuario;
        }
        public List<ImagenEmpresaPerfil> GetImagenDePerfilEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<ImagenEmpresaPerfil> clImagenEmpresa = new List<ImagenEmpresaPerfil>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetImagenPerfilEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clImagenEmpresa.Add(
                                new ImagenEmpresaPerfil
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
            return clImagenEmpresa;
        }
        public List<ImagenesBannerEmpresa> GetImagenesBannerEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<ImagenesBannerEmpresa> clImagenesBanner = new List<ImagenesBannerEmpresa>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetImagenesBannerEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clImagenesBanner.Add(
                                new ImagenesBannerEmpresa
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
            return clImagenesBanner;
        }
        public List<ImagenesFuera> GetImagenesAfuera()
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            List<ImagenesFuera> clImagenesFuera = new List<ImagenesFuera>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetImagenesAfuera().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clImagenesFuera.Add(
                                new ImagenesFuera
                                {
                                    Imagen = (byte[])rows["Imagen"]

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
            return clImagenesFuera;
        }
        public List<PagosRealizadosEmpresa> GetPagosEmpresa(string parametro = "")
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = parametro;
            List<PagosRealizadosEmpresa> clPagosEmpresa = new List<PagosRealizadosEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPagosEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPagosEmpresa.Add(
                                new PagosRealizadosEmpresa
                                {
                                    FechaPago = rows["FechaPago"].ToString(),
                                    MontoPagado = rows["MontoPagado"].ToString(),
                                    NombrePlan = rows["NombrePlan"].ToString(),

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
            return clPagosEmpresa;
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
                                    estadoPlan = rows["EstadoPlan"].ToString(),
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

        public List<DetallePreguntasRespuestas> GetRespuestasPublicacion(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetallePreguntasRespuestas> clRespuestasUsuario = new List<DetallePreguntasRespuestas>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetRespuestasPublicacion(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clRespuestasUsuario.Add(
                                new DetallePreguntasRespuestas
                                {
                                    Descripcion = rows["Descripcion"].ToString(),
                                    Disponibilidad = rows["Disponibilidad"].ToString(),
                                    Sueldo = rows["Sueldo"].ToString(),
                                    nombrePublicacion = rows["nombrePublicacion"].ToString()

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
            return clRespuestasUsuario;
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
                                    FechaMensaje = rows["FechaMensaje"].ToString(),
                                    idMensaje = rows["IdMensaje"].ToString(),
                                    IdAutor = rows["IdAutor"].ToString(),
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
        public List<DetalleTagOficio> GetOficiosUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<DetalleTagOficio> clDetalleOficio = new List<DetalleTagOficio>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetOficiosUsuario(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetalleOficio.Add(
                                new DetalleTagOficio
                                {
                                    idTag = rows["IdTag"].ToString(),
                                    nombreOficio = rows["NombreOficio"].ToString()
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
            return clDetalleOficio;
        }
        public int GetPlanesContratadosEmpresa(string parametro = "")
        {
            int idplan = 0;
            if (parametro == "Inicio")
            {
                return 0;
            }
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = parametro;

            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPlanesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            idplan = (int)rows["IdPlan"];
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
            return idplan;
        }
        public int GetPlanAnteriorEmpresa(string idEmpresa)
        {
            int idplan = 0;

            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = idEmpresa;

            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetUltimoPlanEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            idplan = (int)rows["IdPlanAnterior"];
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
            return idplan;
        }
        public List<PreguntasPorPublicacion> GetPreguntasPorPublicacionId(string idPublicacion)
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = idPublicacion;
            DataSet datas = new DataSet();
            List<PreguntasPorPublicacion> clPreguntasPorPubliEmp = new List<PreguntasPorPublicacion>();
            try
            {
                datas = svcEmpleos.GetPreguntasPorPublicacionId(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPreguntasPorPubliEmp.Add(
                                new PreguntasPorPublicacion
                                {
                                    PreguntaNombre = rows["Pregunta"].ToString(),
                                    PublicacionTitulo = rows["Publicacion"].ToString(),
                                    Id = rows["IdPregunta"].ToString(),
                                    TipoPregunta = rows["TipoPregunta"].ToString(),
                                    NombreCorto = rows["NombreCorto"].ToString(),
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
            return clPreguntasPorPubliEmp;
        }
        public List<PreguntasPorPublicacion> GetPreguntasPorPublicacionTodas(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;

            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            DataSet datas = new DataSet();
            List<PreguntasPorPublicacion> clPreguntasPorPubliEmp = new List<PreguntasPorPublicacion>();
            try
            {
                datas = svcEmpleos.GetPreguntasPorPublicacionEmpresa(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPreguntasPorPubliEmp.Add(
                                new PreguntasPorPublicacion
                                {
                                    PreguntaNombre = rows["Pregunta"].ToString(),
                                    PublicacionTitulo = rows["Publicacion"].ToString(),
                                    Id = rows["IdPregunta"].ToString()
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
            return clPreguntasPorPubliEmp;
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
                                    EstadoPreguntaSeleccionada = rows["EstadoPregunta"].ToString()
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
                    if (valores[0] != "" && valores[1] != "")
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
                    else
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
            }
            catch (Exception ex)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return clPublicacionEmpresa;
        }
        public int GetCandiPubliTrabaPreguntPermitidas(string idEmpresa, string numero)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_EMPRESA";
            parametros[1] = "@NUMERO";
            valores[0] = idEmpresa;
            valores[1] = numero;
            int cantidad = 0;
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetCandiPubliTrabaPreguntPermitidas(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            cantidad = (int)rows["Cantidad"];

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
            return cantidad;
        }
        public List<DetallePublicacion> GetPublicacionFiltros(string idEmpresa, string nombrePub, string fechaPub, string ordeno, string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[5];
            string[] valores = new string[5];

            parametros[0] = "@IDL_EMPRESA";
            parametros[1] = "@NOMBREPUB_FILTRO";
            parametros[2] = "@FECHA_FILTRO";
            parametros[3] = "@ORDENAMIENTO";
            parametros[4] = "@ID_USUARIOEMPRESA";
            valores[0] = idEmpresa;
            valores[1] = nombrePub;
            valores[2] = fechaPub;
            valores[3] = ordeno;
            valores[4] = idUsuario;
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
                                    EstadoPublicacion = (int)rows["Estado"],
                                    DiscapacidadPub = rows["Discapacidad"].ToString(),
                                    ContadorVotos = rows["ContadorVotos"].ToString(),
                                    PromedioVotos = rows["PromedioVotos"].ToString(),
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

        public List<ResultadosTest> GetResultadosTestUsuario(string idUsuario)
        {
            
            string[] parametros = new string[1];
            string[] valores = new string[1];
            string code = string.Empty;
            string mensaje = string.Empty;
            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;
            List<ResultadosTest> resultado = new List<ResultadosTest>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetResultadosTest(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            resultado.Add(
                                new ResultadosTest
                                {
                                    Responsabilidad = rows["Responsabilidad"].ToString(),
                                    RestoResponsabilidad = rows["RestoResponsabilidad"].ToString(),
                                    AutoGestion = rows["AutoGestion"].ToString(),
                                    RestoAutogestion = rows["RestoAutogestion"].ToString(),
                                    Liderazgo = rows["Liderazgo"].ToString(),
                                    RestoLiderazgo = rows["RestoLiderazgo"].ToString()
                                });


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
            return resultado;
        }
        public List<Rubro> GetRubrosEmpresa()
        {
            DataSet data = new DataSet();
            List<Rubro> clrubroE = new List<Rubro>();
            string code = string.Empty;
            string mensaje = string.Empty;
            try
            {
                data = svcEmpleos.GetRubro().Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {

                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clrubroE.Add(
                                new Rubro
                                {
                                    idRubro = rows["IdRubro"].ToString(),
                                    NombreRubro = rows["NombreRubro"].ToString()
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
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return clrubroE;

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
                                    PublicacionSolicitud = rows["PublicacionSolicitud"].ToString(),
                                    DocNombre = rows["Documento"].ToString(),
                                    Documento = ModuleControlRetorno() + "/Empresa/DownloadCV?url=" + rows["Url"].ToString() + "&documento=" + rows["Documento"].ToString()

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
        public List<TarjetaEmpresa> GetTarjetasEmpresa(string idEmpresa)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_EMPRESA";
            valores[0] = idEmpresa;
            List<TarjetaEmpresa> clTarjetaEmpresa = new List<TarjetaEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetTarjetaEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clTarjetaEmpresa.Add(
                                new TarjetaEmpresa
                                {
                                    FechaExpiracion = rows["FechaExpiracion"].ToString(),
                                    NombreTarjeta = rows["NombreTarjeta"].ToString(),
                                    NumeroTarjeta = rows["NumeroTarjeta"].ToString(),
                                    TarjetaCVV = rows["TarjetaCVV"].ToString(),
                                    MontoDisponible = rows["MontoDisponible"].ToString(),
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
            return clTarjetaEmpresa;
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
                                    ImagendelUsuarioE = (byte[])rows["ImagendelUsuarioE"],
                                    IdUsuarioE = rows["IdUsuarioE"].ToString(),
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
                                    CorreoCandidato = rows["CorreoCandidato"].ToString(),
                                    ApellidosCandidatos = rows["ApellidoCandidato"].ToString(),
                                    TituloPostulacion = rows["TituloPublicacion"].ToString(),
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
            return clUltimosC;
        }

        public List<CandidatoUsuarioOficios> GetUsuariosConOficio(string parametro = "")
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@PARAMETRO";
            valores[0] = parametro;
            DataSet datas = new DataSet();
            List<CandidatoUsuarioOficios> clUsuariosOficios = new List<CandidatoUsuarioOficios>();
            try
            {
                datas = svcEmpleos.GetUsuariosConOficios(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clUsuariosOficios.Add(
                                new CandidatoUsuarioOficios
                                {
                                    IdUsuario = rows["IdUsuario"].ToString(),
                                    ApellidosUsuario = rows["ApellidosUsuario"].ToString(),
                                    NombreUsuario = rows["NombreUsuario"].ToString(),
                                    RutUsuario = rows["RutUsuario"].ToString(),
                                    TagOficios = GetOficiosUsuario(rows["IdUsuario"].ToString())
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
            return clUsuariosOficios;
        }
        public string GetVotoPorUsuario(string idUsuario, string idPublicacion)
        {
            string votoSeleccionado = "0";
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            parametros[0] = "@ID_USUARIO";
            parametros[1] = "@ID_PUBLICACION";
            valores[0] = idUsuario;
            valores[1] = idPublicacion;
            DataSet datas = new DataSet();
            List<CandidatoEmpleos> clUltimosC = new List<CandidatoEmpleos>();
            try
            {
                datas = svcEmpleos.GetVotoSeleccionadoPublicacion(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            votoSeleccionado = rows["votoSeleccionado"].ToString();

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
            return votoSeleccionado;
        }
        public string GetVotosTotales(string idPublicacion)
        {
            string votosTotales = "0";
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = idPublicacion;
            DataSet datas = new DataSet();
            List<CandidatoEmpleos> clUltimosC = new List<CandidatoEmpleos>();
            try
            {
                datas = svcEmpleos.GetVotoSeleccionadoPublicacion(parametros, valores).Table; // GetCandidatosEmpresa

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            votosTotales = rows["votosTotales"].ToString();

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
            return votosTotales;
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
            string[] parametrosUserE = new string[3];
            string[] valoresUserE = new string[3];
            var resultado = "";

            Empresa clEmpresa = new Empresa();
            Usuario clUsuario = new Usuario();

            try
            {
                DataSet data = new DataSet();
                DataSet dataUsuario = new DataSet();

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
                            resultado = "1";
                            break;
                        case "400":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            resultado = "0";
                            break;

                        case "500":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            resultado = "0";
                            break;
                        default:
                            code = "600";
                            mensaje = errorSistema;
                            resultado = "0";
                            break;
                    }
                }

                // Validamos usuarioempresa
                if (resultado == "0")
                {
                    parametrosUserE[0] = "@USUARIO";
                    parametrosUserE[1] = "@PASSWORD";
                    parametrosUserE[2] = "@TIPO";

                    valoresUserE[0] = usuario;
                    valoresUserE[1] = clave;
                    valoresUserE[2] = "E";

                    dataUsuario = svcEmpleos.ValUsuario(parametrosUserE, valoresUserE).Table;
                    foreach (DataRow rows in dataUsuario.Tables[0].Rows)
                    {
                        switch (rows["Code"].ToString())
                        {
                            case "200":
                                clUsuario.Rut = rows["Rut"].ToString();
                                clUsuario.Correo = rows["Correo"].ToString();
                                code = "800";
                                mensaje = "";
                                Session["Usuario"] = rows["Rut"].ToString();
                                Session["IdUsuario"] = rows["IdUsuario"].ToString();
                                Session["NombreUsuario"] = rows["Nombre"].ToString();
                                Session["TipoUsuario"] = rows["TipoUsuario"].ToString();
                                Session["IDempresa"] = rows["EmpresaID"].ToString();
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
            return Json(new { Code = code, Message = mensaje, Empresa = clEmpresa, UsuarioEmpresa = clUsuario });
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
        public JsonResult ActualizaEstadoProceso(string idCandidato, string estado, string idPublicacion, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(idCandidato) || string.IsNullOrWhiteSpace(idCandidato))
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "Principal";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Principal";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_CANDIDATO";
                parametros[1] = "@ESTADO";
                parametros[2] = "@ID_PUBLICACION";


                valores[0] = idCandidato;
                valores[1] = estado;
                valores[2] = idPublicacion;


                data = svcEmpleos.UpdEstadoSolicitudUsuario(parametros, valores).Table;

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
                return Json(new { data = "-1" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActualizarPublicacion(string id, string descripcion, string error = "")
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
                    view = "DetallePublicaciones";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "DetallePublicaciones";//"App/_ModalMensajeError";
                    return Json(new { data = "0" }, JsonRequestBehavior.AllowGet);
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

                            view = "DetallePublicaciones";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "DetallePublicaciones";
                            break;
                        case "500":
                            view = "DetallePublicaciones";
                            break;
                        default:
                            view = "DetallePublicaciones";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "DetallePublicaciones";
                return Json(new { data = "-1" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ActualizarPerfilEmpresa(string rut, string telefono, string correo)
        {

            string view = string.Empty;
            var codigo = "0";
            try
            {
                string[] parametros = new string[3];
                string[] valores = new string[3];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();


                parametros[0] = "@RUT_EMPRESA";
                parametros[1] = "@TELEFONO";
                parametros[2] = "@CORREO";


                valores[0] = rut;
                valores[1] = telefono;
                valores[2] = correo;


                data = svcEmpleos.UpdPerfilEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "PerfilEmpresa";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            codigo = "1";
                            break;

                        case "400":
                            view = "PerfilEmpresa";
                            codigo = "-1";
                            break;
                        case "500":
                            view = "PerfilEmpresa";
                            codigo = "-1";
                            break;
                        default:
                            view = "PerfilEmpresa";
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
        public JsonResult ActualizarConfiguracionTarjeta(string nombreT, string numeroT, string fechaT, string montoT, string error = "")
        {
            string view = string.Empty;
            var codigo = "0";
            try
            {

                string[] parametros = new string[5];
                string[] valores = new string[5];
                var IdEmpresa = Session["EmpresaID"].ToString();

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(nombreT) || string.IsNullOrWhiteSpace(nombreT) ||
                    string.IsNullOrEmpty(numeroT) || string.IsNullOrWhiteSpace(numeroT) ||
                    string.IsNullOrEmpty(fechaT) || string.IsNullOrWhiteSpace(fechaT) ||
                    string.IsNullOrEmpty(montoT) || string.IsNullOrWhiteSpace(montoT)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    codigo = "-1";//"App/_ModalMensajeError";
                    return Json(new { data = codigo }, JsonRequestBehavior.AllowGet);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    codigo = "-1";//"App/_ModalMensajeError";
                    return Json(new { data = codigo }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_EMPRESA";
                parametros[1] = "@NOMBRE_TARJETA";
                parametros[2] = "@NUMERO_TARJETA";
                parametros[3] = "@FECHA_EXPIRACION";
                parametros[4] = "@MONTO_DISPONIBLE";


                valores[0] = IdEmpresa;
                valores[1] = nombreT;
                valores[2] = numeroT;
                valores[3] = fechaT;
                valores[4] = montoT;


                data = svcEmpleos.UpdTarjetaEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            codigo = "1";//"App/_ModalMensajeError";

                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            codigo = "1";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            codigo = "1";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            codigo = "1";
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
        public JsonResult DelImagenesEmpresa(string idImagen, string error = "")
        {
            string view = string.Empty;
            var codigo = "0";
            try
            {

                string[] parametros = new string[2];
                string[] valores = new string[2];
                var IdEmpresa = Session["EmpresaID"].ToString();

                DataSet data = new DataSet();


                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    codigo = "-1";//"App/_ModalMensajeError";
                    return Json(new { data = codigo }, JsonRequestBehavior.AllowGet);
                }

                parametros[0] = "@ID_EMPRESA";
                parametros[1] = "@ID_IMAGEN";


                valores[0] = IdEmpresa;
                valores[1] = idImagen;


                data = svcEmpleos.DelImagenesEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            codigo = "1";//"App/_ModalMensajeError";

                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            codigo = "1";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            codigo = "1";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            codigo = "1";
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
        public JsonResult GuardarAsignacionPreguntas(string nombrePregunta, string tipoPregunta, string error = "")
        {
            string view = string.Empty;
            try
            {
                string[] parametros = new string[5];
                string[] valores = new string[5];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(nombrePregunta) || string.IsNullOrWhiteSpace(nombrePregunta)
                    || string.IsNullOrEmpty(tipoPregunta) || string.IsNullOrWhiteSpace(tipoPregunta))
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

                var nombrecorto = nombrePregunta.Replace(" ", String.Empty);

                parametros[0] = "@NOMBRE_PREGUNTA";
                parametros[1] = "@ID_EMPRESA";
                parametros[2] = "@ESTADO_PREGUNTA";
                parametros[3] = "@TIPO_PREGUNTA";
                parametros[4] = "@NOMBRECORTO_PREGUNTA";

                valores[0] = nombrePregunta;
                valores[1] = Session["EmpresaID"].ToString();
                valores[2] = "1";
                valores[3] = "1";
                valores[4] = nombrecorto;

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
                return Json(new { data = "-1" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = "1" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAsignacionPreguntasPublicacion(string publicacion, string pregunta)
        {

            string view = string.Empty;
            var codigo = "0";
            try
            {
                string[] parametros = new string[2];
                string[] valores = new string[2];

                // Variables vacias
                //string razonsocial = string.Empty;

                DataSet data = new DataSet();


                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@ID_PREGUNTA";


                valores[0] = publicacion;
                valores[1] = pregunta;


                data = svcEmpleos.SetPreguntasSeleccionadasPublicacionEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Configuracion";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            codigo = "1";
                            break;

                        case "400":
                            view = "Configuracion";
                            codigo = "-1";
                            break;
                        case "500":
                            view = "Configuracion";
                            codigo = "-1";
                            break;
                        default:
                            view = "Configuracion";
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
        public ActionResult GuardarConfiguracionTarjeta(string nombreT, string numeroT, string fechaT, string cvvT, string montoT, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[6];
                string[] valores = new string[6];
                var IdEmpresa = Session["EmpresaID"].ToString();

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(nombreT) || string.IsNullOrWhiteSpace(nombreT) ||
                    string.IsNullOrEmpty(numeroT) || string.IsNullOrWhiteSpace(numeroT) ||
                    string.IsNullOrEmpty(cvvT) || string.IsNullOrWhiteSpace(cvvT) ||
                    string.IsNullOrEmpty(fechaT) || string.IsNullOrWhiteSpace(fechaT) ||
                    string.IsNullOrEmpty(montoT) || string.IsNullOrWhiteSpace(montoT)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "PerfilEmpresa";//"App/_ModalMensajeError";
                    return View(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "PerfilEmpresa";//"App/_ModalMensajeError";
                    return View(view);
                }

                parametros[0] = "@ID_EMPRESA";
                parametros[1] = "@NOMBRE_TARJETA";
                parametros[2] = "@NUMERO_TARJETA";
                parametros[3] = "@FECHA_EXPIRACION";
                parametros[4] = "@CVV";
                parametros[5] = "@MONTO_TARJETA";


                valores[0] = IdEmpresa;
                valores[1] = nombreT;
                valores[2] = numeroT;
                valores[3] = fechaT;
                valores[4] = cvvT;
                valores[5] = montoT;


                data = svcEmpleos.SetTarjetaEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "PerfilEmpresa";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "PerfilEmpresa";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "PerfilEmpresa";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "PerfilEmpresa";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "PerfilEmpresa";
            }

            return RedirectToAction(view);
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

            return RedirectToAction(view);
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
                    return RedirectToAction(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Trabajadores";//"App/_ModalMensajeError";
                    return RedirectToAction(view);
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

            return RedirectToAction(view);
        }

        [HttpPost]
        public JsonResult GuardarRespuestaMensajeAUsuario(string idMensaje, string idAutor, string mensaje, string error = "")
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
                valores[3] = Session["EmpresaID"].ToString();


                data = svcEmpleos.SetRespuestaMensajeUsuario(parametros, valores).Table;

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
        public JsonResult GuardarPagoPlanEmpresa(string idPlan, string idPlanAnterior, string error = "")
        {
            string view = string.Empty;
            string dato = "0";
            var idEmpresa = Session["EmpresaID"].ToString();
            try
            {

                string[] parametros = new string[4];
                string[] valores = new string[4];

                DataSet data = new DataSet();

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "PlanesEmpresa";//"App/_ModalMensajeError";
                    return Json(new { data = dato }, JsonRequestBehavior.AllowGet);
                }

                if (idPlanAnterior == "0")
                {
                    view = "PlanesEmpresa";//"App/_ModalMensajeError";
                    return Json(new { data = dato }, JsonRequestBehavior.AllowGet);
                }

                var fechaTerminoPlan = GetFechaTerminoPlan(idEmpresa, idPlanAnterior);

                parametros[0] = "@ID_EMPRESA";
                parametros[1] = "@ID_PLAN";
                parametros[2] = "@ID_PLAN_ANTERIOR";
                parametros[3] = "@FECHA_TERMINOPLAN";

                valores[0] = idEmpresa;
                valores[1] = idPlan;
                valores[2] = idPlanAnterior;
                valores[3] = fechaTerminoPlan;


                data = svcEmpleos.SetPagosEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "PlanesEmpresa";
                            dato = "1";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "PlanesEmpresa";
                            dato = "0";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "PlanesEmpresa";
                            dato = "0";
                            break;
                        case "800":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "PlanesEmpresa";
                            dato = "0";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "PlanesEmpresa";
                            dato = "0";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "PlanesEmpresa";
            }

            return Json(new { data = dato, message = ViewBag.ReferenciaMensaje }, JsonRequestBehavior.AllowGet);
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
            ViewBag.Rubro = GetRubrosEmpresa();
            return View();
        }
        public ActionResult SubirImagenes(HttpPostedFileBase file)
        {
            byte[] archivo = null;
            Stream myStream = file.InputStream;
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);
                archivo = ms.ToArray();
            }

            string Query = "INSERT INTO IMAGENES_EMPRESA (NOMBRE_IMG, FECHA_SUBIDA, ID_EMPRESA, ARCHIVO_IMG) VALUES (@NOMBRE, @FECHA, @ID_EMPRESA, @ARCHIVO)";
            conexion.Open();
            SqlCommand comando = new SqlCommand(Query, conexion);
            comando.Parameters.AddWithValue("@NOMBRE", file.FileName);
            comando.Parameters.AddWithValue("@FECHA", DateTime.UtcNow);
            comando.Parameters.AddWithValue("@ID_EMPRESA", Session["EmpresaID"].ToString());
            comando.Parameters.AddWithValue("@ARCHIVO", archivo);
            comando.ExecuteNonQuery();
            conexion.Close();
            return RedirectToAction("PerfilEmpresa");
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
            string idEmpresa = Session["EmpresaID"].ToString();
            var existe = GetImagenDePerfilEmpresa(idEmpresa).Count();

            if (existe == 0)
            {
                Query = "INSERT INTO IMAGEN_PERFIL_EMPRESA (ID_EMPRESA, NOMBRE_IMAGEN, IMAGEN) VALUES (@ID_EMPRESA, @NOMBRE, @IMAGEN)";
            }
            else
            {
                Query = "UPDATE IMAGEN_PERFIL_EMPRESA SET NOMBRE_IMAGEN = @NOMBRE, IMAGEN = @IMAGEN WHERE ID_EMPRESA = @ID_EMPRESA";
            }
            conexion.Open();
            SqlCommand comando = new SqlCommand(Query, conexion);
            comando.Parameters.AddWithValue("@ID_EMPRESA", idEmpresa);
            comando.Parameters.AddWithValue("@NOMBRE", file.FileName);
            comando.Parameters.AddWithValue("@IMAGEN", archivo);
            comando.ExecuteNonQuery();
            conexion.Close();


            return RedirectToAction("PerfilEmpresa");
        }

       
        #endregion

        #region ControlRetorno
        [HttpPost]
        public ActionResult ViewPartialFormSignIn()
        {
            return PartialView("Empresa/_DetalleLoginEmpresa");
        }

        [HttpPost]
        public ActionResult ViewPartialErrorSignIn(string message)
        {
            ViewBag.Message = message;

            return PartialView("Auth/_ErrorSignIn");
        }

        [HttpPost]
        public ActionResult ViewPartialErrorRegistro()
        {
            return PartialView("Empresa/_ErrorRegistroEmpresa");
        }

        [HttpPost]
        public ActionResult ViewPartialErrorRegistroClaves()
        {
            return PartialView("Empresa/_ErrorRegistroEmpresaClaves");
        }

        [HttpPost]
        public ActionResult ViewPartialLoadingSignIn()
        {
            return PartialView("_LoadingLogin");
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
        #endregion

    }
}