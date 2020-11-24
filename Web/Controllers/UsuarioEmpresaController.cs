using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        #region Metodos
        public ActionResult Index()
        {
            var idUsuario = Session["IdUsuario"].ToString();
            var idEmpresa = Session["EmpresaID"].ToString();
            ViewBag.ReferenciaGetDetalleTrabajadores = GetDetalleTrabajador(idUsuario);
            ViewBag.PublicacionesEmpresa = GetPublicaciones("", idUsuario);
            return View();
        }

        public ActionResult Perfil(string idUsuario)
        {
            ViewBag.PublicacionesEmpresa = GetPublicaciones("", idUsuario);
            ViewBag.ReferenciaDatosPerfil = GetDatosUsuarioEmpresa(idUsuario, "");
            return View();
        }

        [HttpGet]
        public ActionResult DetallePublicaciones(string id)
        {
            ViewBag.DetallePublicacion = GetDetallePublicacion(id);
            ViewBag.Candidatos = GetCandidatosPublicacion(id);
            return View();
        }

        public ActionResult PerfilCandidato(string idUsuario)
        {
            ViewBag.ReferenciaDatosUsuario = GetDatosUsuario(idUsuario);
            return View();
        }

        #endregion

        #region ObtencionDatos

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
        public List<DetalleTrabajadores> GetDetalleTrabajador(string id_Usuario = "")
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_LOGINUSUARIO";
            valores[0] = id_Usuario;
            List<DetalleTrabajadores> clDetalleUsuarioE = new List<DetalleTrabajadores>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetDetalleTrabajador(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clDetalleUsuarioE.Add(
                                new DetalleTrabajadores
                                {
                                    NombreUsuarioE = rows["NombreUsuario"].ToString(),
                                    ApellidoUsuarioE = rows["ApellidoUsuario"].ToString(),
                                    RutUsuarioE = rows["RutUsuario"].ToString()
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
            return clDetalleUsuarioE;
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

       
        public List<DetallePublicacion> GetDetallePublicacion(string id)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@ID_PUBLICACION";
            valores[0] = id;
            List<DetallePublicacion> clPlanesEmpresa = new List<DetallePublicacion>();

            DataSet data = new DataSet();

            try
            {
                data = svcEmpleos.GetDetallePublicacion(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPlanesEmpresa.Add(
                                new DetallePublicacion
                                {
                                    AutorPublicacion = rows["Autor"].ToString(),
                                    TituloPublicacion = rows["Titulo"].ToString(),
                                    DescripcionPublicacion = rows["Descripcion"].ToString(),
                                    FechaPublicacion = rows["Fecha"].ToString(),
                                    MontoPublicacion = rows["Monto"].ToString(),
                                    IdPublicacion = (int)rows["IdPublicacion"]
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
            return clPlanesEmpresa;
        }

        // Candidatos generales
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
        #endregion

        #region registros
        public ActionResult GuardarPublicacion(string titulo, string id_empresa, string id_usuario, string tipo, string descripcionNuevaPub, string monto, string discapacidad,
        string area, string subarea, string error = "")
        {
            string view = string.Empty;
            string estado = "1";
            if(discapacidad == null)
            {
                discapacidad = "0";
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
                    view = "Index";//"App/_ModalMensajeError";
                    return View(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Index";//"App/_ModalMensajeError";
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
                    view = "Index";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "Index";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                parametros[0] = "@ID_PUBLICACION";
                parametros[1] = "@DESCRIPCION";


                valores[0] = id;
                valores[1] = descripcion;


                data = svcEmpleos.UpdPublicacionEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":

                            view = "Index";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            view = "Index";
                            break;
                        case "500":
                            view = "Index";
                            break;
                        default:
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
        #endregion

    }
}