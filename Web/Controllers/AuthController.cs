﻿using System.Data;
using System.Web.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient svcEmpleosChile = new WebAPI.ServicioEmpleosChile.ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult RegistroUsuario()
        {
            DataSet data = new DataSet();
            List<Pais> lstPais = new List<Pais>();
            List<Region> lstRegion = new List<Region>();


            try
            {
                ViewBag.ApplicationActive = true;
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "Auth/RegistroUsuario";
                ViewBag.ReferenciaOficio = ModuleControlRetorno() + "Oficios/Inicio";

                data = GetPais();
                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        lstPais.Add(new Pais
                        {
                            Id = row["Id"].ToString(),
                            Nombre = row["Nombre"].ToString()
                        });
                    }

                    ViewBag.ReferenciaPais = lstPais;
                }

                data = GetRegion();
                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        lstRegion.Add(new Region
                        {
                            Id = row["Id"].ToString(),
                            Nombre = row["Nombre"].ToString()
                        });
                    }

                    ViewBag.ReferenciaRegion = lstRegion;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return View();
        }

        #region JSON
        [HttpPost]
        public JsonResult RegistroUsuario(string rut, string nombre1, string nombre2, string apellidoP, string apellidoM,
            string correo, string correoRepetir, string password, string passwordRepetir, string fechaNacimiento)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string urlHome = string.Empty;
            string[] parametros = new string[12];
            string[] valores = new string[12];
            DataSet data = new DataSet();

            try
            {
                parametros[0] = "@RUT";
                parametros[1] = "@CORREO";
                parametros[2] = "@REPETIRCORREO";
                parametros[3] = "@PASSWORD";
                parametros[4] = "@REPETIRPASSWORD";
                parametros[5] = "@PERFIL";
                parametros[6] = "@TIPO";
                parametros[7] = "@NOMBRE1";
                parametros[8] = "@NOMBRE2";
                parametros[9] = "@APATERNO";
                parametros[10] = "@AMATERNO";
                parametros[11] = "@FECHANACIMIENTO";

                valores[0] = rut.Replace(".", "").Replace("-", "");
                valores[1] = correo;
                valores[2] = correoRepetir;
                valores[3] = password;
                valores[4] = passwordRepetir;
                valores[5] = "1";
                valores[6] = "N";
                valores[7] = nombre1;
                valores[8] = string.IsNullOrEmpty(nombre2) ? "" : nombre2;
                valores[9] = apellidoP;
                valores[10] = string.IsNullOrEmpty(apellidoM) ? "" : apellidoM;
                valores[11] = fechaNacimiento;

                urlHome = ModuleControlRetorno() + "App/Inicio";
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "App/Inicio";

                data = svcEmpleosChile.SetUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaMensaje = rows["Message"].ToString();
                            ViewBag.ReferenciaUsuario = rut.Replace(".", "");
                            ViewBag.ReferenciaPass = password;

                            //Session["IdUser"] = rows["Id"].ToString();
                            //Session["User"] = rows["Rut"].ToString();
                            //Session["UserName"] = rows["Nombre"].ToString();
                            //Session["UserType"] = rows["Tipo"].ToString();

                            code = rows["Code"].ToString();
                            break;

                        case "400":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            break;

                        default:
                            code = "500";
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

            return Json(new { Code = code, Message = mensaje, UrlHome = urlHome });
        }

        [HttpPost]
        public JsonResult SignInUser(string user, string pass, string controller)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string pathRedirect = "";
            string[] parametros = new string[3];
            string[] valores = new string[3];
            Usuario usuario = new Usuario();

            try
            {
                DataSet data = new DataSet();
                parametros[0] = "@USUARIO";
                parametros[1] = "@PASSWORD";
                parametros[2] = "@TIPO";

                valores[0] = user;
                valores[1] = pass;
                valores[2] = "N";

                data = svcEmpleosChile.ValUsuario(parametros, valores).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            usuario.Rut = rows["Rut"].ToString();
                            usuario.Correo = rows["Correo"].ToString();
                            code = rows["Code"].ToString();
                            mensaje = "";
                            pathRedirect = ModuleControlRetorno() + "App/Inicio";
                            var useer = rows["TipoUsuario"].ToString();
                            Session["IdUser"] = rows["IdUsuario"].ToString();
                            Session["UserName"] = rows["Nombre"].ToString();
                            Session["UserType"] = rows["TipoUsuario"].ToString();
                            break;
                        case "400":
                            code = rows["Code"].ToString();
                            mensaje = rows["Message"].ToString();
                            usuario.Rut = rows["Rut"].ToString();
                            break;

                        case "500":
                            code = rows["Code"].ToString();
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
            return Json(new { Code = code, Message = mensaje, User = usuario, PathRedirect = pathRedirect });
        }

        [HttpPost]
        public JsonResult SignOut()
        {
            var retorno = string.Empty;
            Session.Clear();

            retorno = ModuleControlRetorno() + "App/Inicio";

            return Json(new { Home = retorno });
        }
        #endregion

        #region GET
        [HttpPost]
        public JsonResult GetCiudad(string region)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            DataSet data = new DataSet();
            List<Ciudad> lstCiudad = new List<Ciudad>();

            try
            {
                parametros[0] = "@REGION";
                valores[0] = region;

                data = GetCiudad(parametros, valores);

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        code = row["Code"].ToString();
                        lstCiudad.Add(new Ciudad
                        {
                            Id = row["Id"].ToString(),
                            Nombre = row["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                message = errorSistema;
            }

            return Json(new { Code = code, Message = message, Ciudad = lstCiudad });
        }

        [HttpPost]
        public JsonResult GetComuna(string ciudad)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            DataSet data = new DataSet();
            List<Comuna> lstComuna = new List<Comuna>();

            try
            {
                parametros[0] = "@CIUDAD";
                valores[0] = ciudad;

                data = GetComuna(parametros, valores);

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        code = row["Code"].ToString();
                        lstComuna.Add(new Comuna
                        {
                            Id = row["Id"].ToString(),
                            Nombre = row["Nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                message = errorSistema;
            }

            return Json(new { Code = code, Message = message, Comuna = lstComuna });
        }


        public DataSet GetCiudad(string[] parametros, string[] valores)
        {
            DataSet data = new DataSet();

            try
            {
                data = svcEmpleosChile.GetCiudad(parametros, valores).Table;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return data;
        }

        public DataSet GetComuna(string[] parametros, string[] valores)
        {
            DataSet data = new DataSet();

            try
            {
                data = svcEmpleosChile.GetComuna(parametros, valores).Table;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return data;
        }

        public DataSet GetPais()
        {
            DataSet data = new DataSet();

            try
            {
                data = svcEmpleosChile.GetPais().Table;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return data;
        }

        public DataSet GetRegion()
        {
            DataSet data = new DataSet();

            try
            {
                data = svcEmpleosChile.GetRegion().Table;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorPlataforma = errorSistema;
            }

            return data;
        }
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

        #region PARTIALVIEWS
        [HttpPost]
        public ActionResult ViewPartialLoadingSignIn()
        {
            return PartialView("_LoadingLogin");
        }

        [HttpPost]
        public ActionResult ViewPartialFormSignIn()
        {
            return PartialView("Auth/_FormLogin");
        }

        [HttpPost]
        public ActionResult ViewPartialErrorSignIn(string message)
        {
            ViewBag.Message = message;

            return PartialView("Auth/_ErrorSignIn");
        }
        #endregion
    }
}