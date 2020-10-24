using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EmpresaController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleos = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        // GET: Empresa
        public ActionResult Index()
        {
            ViewBag.Planes = GetPlanes("");
            return View();
        }

        public ActionResult Principal()
        {
            var empresa = Session["EmpresaID"].ToString();
            //ViewBag.TiposPublicaciones = GetTiposPublicaciones();
            ViewBag.PublicacionesEmpresa = GetPublicaciones(empresa);
            ViewBag.PlanesEmpresa = GetPlanes(empresa);
            return View();
        }



        #region ObtencionDatos

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
                                    NombrePlan = rows["NombrePlan"].ToString(),
                                    PrecioPlan = rows["Precio"].ToString()
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

        public List<PublicacionesEmpresa> GetPublicaciones(string parametro)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            parametros[0] = "@IDL_EMPRESA";
            valores[0] = parametro;
            List<PublicacionesEmpresa> clPublicacionEmpresa = new List<PublicacionesEmpresa>();
            DataSet data = new DataSet();
            try
            {
                data = svcEmpleos.GetPublicacionesEmpresa(parametros, valores).Table;
                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clPublicacionEmpresa.Add(
                                new PublicacionesEmpresa
                                {

                                    NombrePublicacion = rows["Titulo"].ToString(),
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
                datas = svcEmpleos.GetCandidatosEmpresa(parametros, valores).Table;

                foreach (DataRow rows in datas.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            clCandidatos.Add(
                                new CandidatoEmpleos
                                {
                                    NombreCandidato = rows["Nombre"].ToString(),
                                    FechaPostulacion = rows["FechaPostulacion"].ToString()
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
            Empresa clEmpresa = new Empresa();

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
                            break;

                        default:
                            code = "600";
                            mensaje = errorSistema;
                            break;
                    }
                }

                /* VALIDAMOS SI ES USUARIO DE EMPRESA */
                if (code != "200")
                {
                    parametrosUserE[0] = "@USUARIO";
                    parametrosUserE[1] = "@PASSWORD";
                    parametrosUserE[2] = "@TIPO";

                    valoresUserE[0] = usuario;
                    valoresUserE[1] = clave;
                    valoresUserE[2] = "E";

                    dataUsuario = svcEmpleos.ValUsuario(parametrosUserE, valoresUserE).Table;
                    if (dataUsuario.Tables[0].Rows.Count > 0)
                    {
                        //data = svcEmpleos.ValEmpresa(parametros, valores).Table;
                        foreach (DataRow rows in dataUsuario.Tables[0].Rows)
                        {
                            switch (rows["Code"].ToString())
                            {
                                case "200":
                                    clEmpresa.RutEmpresa = rows["Rut"].ToString();
                                    clEmpresa.CorreoEmpresa = rows["Correo"].ToString();
                                    code = rows["Code"].ToString();
                                    mensaje = "";
                                    Session["Empresa"] = rows["Rut"].ToString();
                                    Session["NombreEmpresa"] = rows["Nombre"].ToString();
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
            }
            catch (Exception ex)
            {
                code = "600";
                mensaje = errorSistema;
            }
            return Json(new { Code = code, Message = mensaje, Empresa = clEmpresa });
        }
        #endregion

        #region Registro

        public ActionResult Registro()
        {
            return View();
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
                    view = "../Empresa/Registro";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "../Empresa/Registro";//"App/_ModalMensajeError";
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
                            view = ModuleControlRetorno() + "/Empresa/Registro";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = ModuleControlRetorno() + "/Empresa/Registro";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = ModuleControlRetorno() + "/Empresa/Registro";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = ModuleControlRetorno() + "/Empresa/Registro";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = ModuleControlRetorno() + "/Empresa/Registro";
            }

            return PartialView(view);
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

        #region RegistroPublicacion
        [HttpPost]
        public ActionResult GuardarPublicacion(string titulo, string id_empresa, string tipo, string descripcion, string monto, string discapacidad,
        string area, string subarea, string error = "")
        {
            string view = string.Empty;

            try
            {
                string[] parametros = new string[7];
                string[] valores = new string[7];

                DataSet data = new DataSet();

                if (string.IsNullOrEmpty(id_empresa) || string.IsNullOrWhiteSpace(id_empresa) ||
                    string.IsNullOrEmpty(titulo) || string.IsNullOrWhiteSpace(titulo) ||
                    string.IsNullOrEmpty(descripcion) || string.IsNullOrWhiteSpace(descripcion) ||
                    string.IsNullOrEmpty(tipo) || string.IsNullOrWhiteSpace(tipo) ||
                    string.IsNullOrEmpty(monto) || string.IsNullOrWhiteSpace(monto) ||
                    string.IsNullOrEmpty(subarea) || string.IsNullOrWhiteSpace(subarea)
                    )
                {

                    //ViewBag.ReferenciaMensaje = "Debe completar todos los campos oblicatorios(*)";
                    view = "../Empresa/Inicio";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                if (error == "true")
                {
                    //ViewBag.ReferenciaMensaje = "Algunos datos ingresados tienen un formato no valido.";
                    view = "../Empresa/Registro";//"App/_ModalMensajeError";
                    return PartialView(view);
                }

                parametros[0] = "@ID_LOGINEMPRESA";
                parametros[1] = "@TITULO";
                parametros[2] = "@DESCRIPCION";
                parametros[3] = "@TIPO";
                parametros[4] = "@MONTO";
                parametros[5] = "@DISCAPACIDAD";
                parametros[6] = "@SUBAREA";

                valores[0] = id_empresa;
                valores[1] = titulo;
                valores[2] = descripcion;
                valores[3] = tipo;
                valores[4] = monto;
                valores[5] = discapacidad;
                valores[6] = subarea;

                data = svcEmpleos.SetPublicacionesEmpresa(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "/Empresa/Principal";
                            //ViewBag.ReferenciaCatalogo = ModuleRetornoCatalogo();
                            break;

                        case "400":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            view = "/Empresa/Principal";
                            break;
                        case "500":
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "/Empresa/Inicio";
                            break;
                        default:
                            ViewBag.ReferenciaMensaje = errorSistema;
                            view = "/Empresa/Principal";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMensaje = errorSistema;
                view = "/Empresa/Principal";
            }

            return PartialView(view);
        }

        [HttpGet]
        public ActionResult EditarPublicacion(string id)
        {
            ViewBag.DetallePublicacion = GetDetallePublicacion(id);
            ViewBag.Candidatos = GetCandidatosPublicacion(id);
            return View();
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