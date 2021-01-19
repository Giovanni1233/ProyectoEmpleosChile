using Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebAPI.ServicioEmpleosChile;

namespace Web.Controllers
{
    public class OficiosController : Controller
    {
        ServicioEmpleosChileClient svcEmpleosChile = new ServicioEmpleosChileClient();
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde";

        public ActionResult Inicio()
        {
            ViewBag.ApplicationActive = true;
            ViewBag.ReferenciaInicio = ModuleControlRetorno() + "/Oficio/Inicio";
            ViewBag.ReferenciaHome = ModuleControlRetorno() + "/App/Inicio";
            ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "/Auth/RegistroUsuario";
            ViewBag.ReferenciaOficio = ModuleControlRetorno() + "/Oficios/Inicio";

            if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                ViewBag.ReferenciaUserName = Session["UserName"].ToString();

            if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                ViewBag.ReferenciaUserType = Session["UserType"].ToString();

            ViewBag.ReferenciaComuna = GetComuna("");


            return View();
        }

        public ActionResult Perfil(string idMensaje = "0", string idReceptor = "0")
        {
            try
            {
                ViewBag.ApplicationActive = true;
                ViewBag.ReferenciaInicio = ModuleControlRetorno() + "/Oficios/Inicio";
                ViewBag.ReferenciaHome = ModuleControlRetorno() + "/App/Inicio";
                ViewBag.ReferenciaRegistro = ModuleControlRetorno() + "/Auth/RegistroUsuario";
                ViewBag.ReferenciaOficio = ModuleControlRetorno() + "/Oficios/Inicio";

                if (Session["IdUser"] != null && Session["IdUser"].ToString() != "")
                    ViewBag.ReferenciaIdUser = Session["IdUser"].ToString();

                if (Session["UserName"] != null && Session["UserName"].ToString() != "")
                    ViewBag.ReferenciaUserName = Session["UserName"].ToString();

                if (Session["UserType"] != null && Session["UserType"].ToString() != "")
                    ViewBag.ReferenciaUserType = Session["UserType"].ToString();


                ViewBag.ReferenciaOficios = GetTagOficios(Session["IdUser"].ToString());
                ViewBag.ReferenciaOficiosUsuarios = GetTagOficiosUsuario(Session["IdUser"].ToString());
                ViewBag.ReferenciaDescripcionOficio = GetDescripcionOficio(Session["IdUser"].ToString());
                ViewBag.ReferenciaValoracionOficio = GetValoracionOficio(Session["IdUser"].ToString());
                ViewBag.ReferenciaComentarioOficio = GetComentariosOficio(Session["IdUser"].ToString());
            }
            catch (Exception ex)
            {
                ViewBag.ApplicationActive = false;
            }

            return View();
        }


        #region JSON
        public JsonResult SetTagOficio(string oficio)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];

            DataSet data = new DataSet();
            List<TagOficio> lstOficios = new List<TagOficio>();
            List<TagOficio> lstOficiosUser = new List<TagOficio>();

            try
            {
                parametros[0] = "@TAG";
                parametros[1] = "@USUARIO";

                valores[0] = oficio;
                valores[1] = Session["IdUser"].ToString();

                data = svcEmpleosChile.SetTagOficioUsuario(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        switch (row["Code"].ToString())
                        {
                            case "200":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            case "400":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            default:
                                code = "600";
                                message = errorSistema;
                                break;

                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "No se encuentraron oficios";
                }

                lstOficios = GetTagOficios(Session["IdUser"].ToString());
                lstOficiosUser = GetTagOficiosUsuario(Session["IdUser"].ToString());
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return Json(new { Code = code, Message = message, Oficios = lstOficios, OficiosUser = lstOficiosUser });
        }

        public JsonResult DelTagOficio(string oficio)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];

            DataSet data = new DataSet();
            List<TagOficio> lstOficios = new List<TagOficio>();
            List<TagOficio> lstOficiosUser = new List<TagOficio>();

            try
            {
                parametros[0] = "@TAG";
                parametros[1] = "@USUARIO";

                valores[0] = oficio;
                valores[1] = Session["IdUser"].ToString();

                data = svcEmpleosChile.DelTagOficioUsuario(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        switch (row["Code"].ToString())
                        {
                            case "200":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            case "400":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            default:
                                code = "600";
                                message = errorSistema;
                                break;
                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "No se encuentraron oficios";
                }

                lstOficios = GetTagOficios(Session["IdUser"].ToString());
                lstOficiosUser = GetTagOficiosUsuario(Session["IdUser"].ToString());
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return Json(new { Code = code, Message = message, Oficios = lstOficios, OficiosUser = lstOficiosUser });
        }

        public JsonResult SetDescripcionOficio(string descripcion)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[2];
            string[] valores = new string[2];
            string descripcionOficio = "";

            DataSet data = new DataSet();

            try
            {
                parametros[0] = "@USUARIO";
                parametros[1] = "@DESCRIPCION";

                valores[0] = Session["IdUser"].ToString();
                valores[1] = descripcion;

                data = svcEmpleosChile.SetDescripcionOficio(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        switch (row["Code"].ToString())
                        {
                            case "200":
                                code = row["Code"].ToString();
                                descripcionOficio = row["Descripcion"].ToString();
                                break;
                            case "400":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            default:
                                code = "600";
                                message = errorSistema;
                                break;
                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "Error al cargar descriocion";
                }
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return Json(new { Code = code, Message = message, DescripcionOficio = descripcionOficio });
        }
        #endregion



        #region LOCAL METODS
        private List<Comuna> GetComuna(string ciudad)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];
            List<Comuna> lstComuna = new List<Comuna>();

            try
            {
                parametros[0] = "@CIUDAD";
                valores[0] = ciudad;

                DataSet data = svcEmpleosChile.GetComuna(parametros, valores).Table;

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

            }

            return lstComuna;
        }

        private List<ImagenUsuarioPerfil> GetImagenDePerfilUsuario(string idUsuario)
        {
            string code = string.Empty;
            string mensaje = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];

            DataSet data = new DataSet();
            List<ImagenUsuarioPerfil> clImagenUsuario = new List<ImagenUsuarioPerfil>();

            parametros[0] = "@ID_USUARIO";
            valores[0] = idUsuario;

            try
            {
                data = svcEmpleosChile.GetImagenPerfilUsuario(parametros, valores).Table;
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

        private List<PerfilProfesionalUsuario> GetPerfilProfesionalUsuario(string idUsuario)
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
                data = svcEmpleosChile.GetPerfilProfesionalUsuario(parametros, valores).Table;
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

        private List<TagOficio> GetTagOficios(string idUsuario)
        {
            DataSet data = new DataSet();
            List<TagOficio> lstTag = new List<TagOficio>();

            string[] parametros = new string[1];
            string[] valores = new string[1];

            parametros[0] = "@USUARIO";
            valores[0] = idUsuario;

            data = svcEmpleosChile.GetTagOficio(parametros, valores).Table;
            if (data.Tables.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    lstTag.Add(new TagOficio
                    {
                        IdTag = row["IdTag"].ToString(),
                        Nombre = row["Nombre"].ToString()
                    });
                }
            }

            return lstTag;
        }

        private List<TagOficio> GetTagOficiosUsuario(string idUsuario)
        {
            DataSet data = new DataSet();
            List<TagOficio> lstTag = new List<TagOficio>();

            string[] parametros = new string[1];
            string[] valores = new string[1];

            parametros[0] = "@USUARIO";
            valores[0] = idUsuario;

            data = svcEmpleosChile.GetTagOficioUsuario(parametros, valores).Table;
            if (data.Tables.Count > 0)
            {
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    lstTag.Add(new TagOficio
                    {
                        IdTag = row["IdTag"].ToString(),
                        Nombre = row["Nombre"].ToString()
                    });
                }
            }

            return lstTag;
        }

        private List<DescripcionOficio> GetDescripcionOficio(string usuario)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];

            DataSet data = new DataSet();
            List<DescripcionOficio> descripcion = new List<DescripcionOficio>();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = Session["IdUser"].ToString();

                data = svcEmpleosChile.GetDescripcionOficio(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        switch (row["Code"].ToString())
                        {
                            case "200":
                                descripcion.Add(new DescripcionOficio
                                {
                                    Id = row["Id"].ToString(),
                                    IdUsuario = row["IdUsuario"].ToString(),
                                    Descripcion = row["Descripcion"].ToString()
                                });
                                break;
                            case "400":
                                code = row["Code"].ToString();
                                message = row["Message"].ToString();
                                break;
                            default:
                                code = "600";
                                message = errorSistema;
                                break;
                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "Error al cargar descriocion";
                }
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return descripcion;
        }

        private List<ValoracionOficio> GetValoracionOficio(string usuario)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];

            DataSet data = new DataSet();
            List<ValoracionOficio> valoracion = new List<ValoracionOficio>();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = Session["IdUser"].ToString();

                data = svcEmpleosChile.GetValoracionOficio(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        if (Convert.ToDouble(row["Porcentaje"]) > 0)
                        {
                            for (var i = 1; i < 6; i++)
                            {
                                valoracion.Add(new ValoracionOficio
                                {
                                    Total = row["Total"].ToString(),
                                    Porcentaje = row["Porcentaje"].ToString(),
                                    CantidadValoracion = row["CantidadValoracion"].ToString(),
                                    Checked = i <= Convert.ToDouble(row["Porcentaje"]) ? "checked" : ""
                                });

                                ViewBag.ReferenciaCantidadValoracion = row["CantidadValoracion"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "Error al cargar datos";
                }
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return valoracion;
        }

        private List<ComentarioOficio> GetComentariosOficio(string usuario)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] parametros = new string[1];
            string[] valores = new string[1];

            DataSet data = new DataSet();
            List<ComentarioOficio> comentario = new List<ComentarioOficio>();

            try
            {
                parametros[0] = "@USUARIO";
                valores[0] = Session["IdUser"].ToString();

                data = svcEmpleosChile.GetComentarioOficio(parametros, valores).Table;

                if (data.Tables.Count > 0)
                {
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        switch (row["Code"].ToString())
                        {
                            case "200":
                                List<ValoracionOficio> valoracion = new List<ValoracionOficio>();

                                if (Convert.ToDouble(row["Valoracion"]) > 0)
                                {
                                    for (var i = 1; i < 6; i++)
                                    {
                                        valoracion.Add(new ValoracionOficio
                                        {
                                            Checked = i <= Convert.ToDouble(row["Valoracion"]) ? "checked" : ""
                                        });
                                    }
                                }

                                comentario.Add(new ComentarioOficio
                                {
                                    Id = row["Id"].ToString(),
                                    IdUsuario = row["IdUsuario"].ToString(),
                                    IdUsuarioComentario = row["IdUsuarioComentario"].ToString(),
                                    Comentario = row["Comentario"].ToString(),
                                    Valoracion = valoracion
                                });
                                break;
                        }
                    }
                }
                else
                {
                    code = "400";
                    message = "Error al cargar datos";
                }
            }
            catch (Exception ex)
            {
                code = "600";
                message = errorSistema;
            }

            return comentario;
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


    }
}