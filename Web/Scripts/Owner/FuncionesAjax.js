﻿function ajaxInicioSesion(controller, usuario, password) {
    if (!usuario.includes("@")) {
        if (validaRut(usuario)) {
            usuario = formateaRut(usuario);
        }
    }

    $.ajax({
        type: 'POST',
        url: controller + 'InicioSesion',
        data: '{ usuario: "' + usuario + '",  clave: "' + password + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code == "200") {
                if (response.tipo == "E") {
                    $("#userUsuarioHidden").val(response.Usuario.Rut);
                    $("#perfilUsuarioHidden").val(response.Usuario.Perfil);

                    $("#loginError").html('');
                    $(".signInLoaderContent").hide();

                    location.href = "/UsuarioEmpresa/Index";
                }
                else {
                    $("#userUsuarioHidden").val(response.Usuario.Rut);
                    $("#perfilUsuarioHidden").val(response.Usuario.Perfil);

                    $("#loginError").html('');
                    $(".signInLoaderContent").hide();

                    location.reload();
                }

            }
            else {
                $("#loginError").html(response.Message);
                $(".signInLoaderContent").hide();
            }
        },
        error: function (xhr) {
            $("#loginError").html('Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde');
        }
    });
}

function ajaxCierraSesion(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'CerrarSesion',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            window.location.href = response.Retorno;
        }
    });
}

// Empresa

function ajaxInicioSesionEmpresa(controller, usuario, password) {

    if (!usuario.includes("@")) {
        if (validaRut(usuario)) {
            usuario = formateaRut(usuario);
        }
    }
    $.ajax({
        type: 'POST',
        url: controller + 'InicioSesion',
        data: '{ usuario: "' + usuario + '",  clave: "' + password + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            //debugger;
            if (response.Code == "200") {
                $("#userEmpresaHidden").val(response.Empresa.Rut);
                $("#loginError").html('');

                location.href = "/Empresa/Principal";
            }
            else if (response.Code == "800") // Usuario empresa
            {
                location.href = "/UsuarioEmpresa/Index";
            }
            else {
                ajaxViewPartialFormSignInEmpresa(controller, response.Message);
            }
        },
        error: function (xhr) {
            $("#loginError").html('Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde');
        }
    });
}


function ajaxViewPartialFormSignInEmpresa(controller, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialFormSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLoginE").html(xhr.responseText);
            ajaxViewPartialErrorSignInEmpresa(controller, message);
        }
    });
}

function ajaxViewPartialErrorSignInEmpresa(controller, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialErrorSignIn',
        data: '{ message: "' + message + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#errorInicioSesionEmpresa").html(xhr.responseText);
            $("#username").focus();
            setInterval(function () { $("#errorInicioSesionEmpresa").html(''); },3000);

        }
    });
}



function ajaxCierraSesionEmpresa(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'CerrarSesion',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            window.location.href = response.Retorno;
        }
    });
}

function ajaxGetCiudad(controller, region) {
    $.ajax({
        type: 'POST',
        url: controller + 'GetCiudad',
        data: '{ region: "' + region + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {

        }
    });
}

function ajaxGetComuna(controller, ciudad) {
    $.ajax({
        type: 'POST',
        url: controller + 'GetComuna',
        data: '{ ciudad: "' + ciudad + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code !== "600") {
                if (response.Code === "200") {
                    var comuna = '<option value="0">Seleccione...</option>';
                    for (i = 0; i < response.Comuna.length; i++) {
                        comuna = comuna + '<option value="' + response.Comuna[i].Id + '">' + response.Comuna[i].Nombre + '</option>';
                    }
                    $("#comuna").html(comuna);
                }
            }
        }
    });
}

function ajaxActualizarPublicacion(controller, id, descripcion) {
    $.ajax({
        type: 'POST',
        url: controller + 'ActualizarPublicacion',
        data: '{ id: "' + id + '", descripcion: "' + descripcion + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Publicación actualizada con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
            else if (response.data == 0) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'El campo comentario no puede estar vacio',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Ha ocurrido un error en la plataforma, favor contace a un Administrador',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
        }
    });
}

function ajaxRegistroUsuario(controller, rut, nombre1, nombre2, apellidoP, apellidoM, correo, correoRepetir, password, passwordRepetir, fechaNacimiento) {
    $.ajax({
        type: 'POST',
        url: controller + 'RegistroUsuario',
        data: '{ rut: "' + rut + '",  nombre1: "' + nombre1 + '",  nombre2: "' + nombre2 + '",  apellidoP: "' + apellidoP + '",  apellidoM: "' + apellidoM + '",  password: "' + password + '",  passwordRepetir: "' + passwordRepetir + '",  fechaNacimiento: "' + fechaNacimiento + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {

        },
        error: function (xhr) {

        }
    });
}


// Configuracion Seleccion de preguntas

function ajaxRegistroPreguntasEmpresa(controller, nombrePregunta, tipoPregunta) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarAsignacionPreguntas',
        data: '{ nombrePregunta: "' + nombrePregunta + '", tipoPregunta: "' + tipoPregunta + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }

        }
    });
}


// Filtros de busqueda

//function ajaxGetPublicacionFiltros(controller, nombrePub, fechaPub) {
//    $.ajax({
//        type: 'POST',
//        url: controller + 'Principal',
//        data: '{ nombrePub: "' + nombrePub + '", fechaPub: "' + fechaPub + '"}',
//        dataType: 'json',
//        contentType: 'application/json',
//        async: true,
//        success: function (response) {
//        }
//    });
//}

// Perfil empresa
function ajaxGuardaPerfilEmpresa(controller, rutE, telefonoE, correoE) {
    $.ajax({
        type: 'POST',
        url: controller + 'ActualizarPerfilEmpresa',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        data: '{ rut: "' + rutE + '", telefono: "' + telefonoE + '", correo: "' + correoE + '" }',
        success: function (response) {
            if (response.data == "1") {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Perfil actualizado con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
            else if (response.data == "-1") {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Ha ocurrido un error al actualizar.',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }

        }
    });
}

// Nueva Pregunta Postulacion

function ajaxGuardarPreguntaPostulacion(controller, valor) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarPreguntaPostulacion',
        data: '{ nombrePregunta: "' + valor + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

// Responder mensaje a usuario

function ajaxResponderMensajeAUsuario(controller, idMensaje, idAutor, mensaje) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarRespuestaMensajeAUsuario',
        data: '{ idMensaje: "' + idMensaje + '", idAutor: "' + idAutor + '", mensaje: "' + mensaje + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

// Votacion
function ajaxGuardarValoracionPublicacion(controller, valorvotacion, idPublicacion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarVotacionPublicacion',
        data: '{ votacion: "' + valorvotacion + '", idPublicacion: "' + idPublicacion + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

// Actualizar tarjeta empresa
function ajaxActualizarTarjetaEmpresa(controller, nombre, numero, fecha, monto) {

    $.ajax({
        type: 'POST',
        url: controller + 'ActualizarConfiguracionTarjeta',
        data: '{ nombreT: "' + nombre + '", numeroT: "' + numero + '", fechaT: "' + fecha + '", montoT: "' + monto + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Tarjeta actualizada con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
        }
    });
}

// Pago plan empresa
function ajaxPagoPlanEmpresa(controller, idPlan, idPlanAnterior) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarPagoPlanEmpresa',
        data: '{ idPlan: "' + idPlan + '", idPlanAnterior: "' + idPlanAnterior + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {

            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Pago realizado con éxito.',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.href = "Empresa/PlanesEmpresa";
            }
            else if (response.data == "-1") {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'No se puede contratar el mismo plan 2 veces',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.href = "Empresa/PlanesEmpresa";
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: response.message,
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.href = "Empresa/PlanesEmpresa";
            }
        }
    });
}


// Actualizar estado solicitud
function ActualizarEstadoSolicitud(idCandidato, estado, idPublicacion) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
    $.ajax({
        type: 'POST',
        url: controller + 'ActualizaEstadoProceso',
        data: '{ idCandidato: "' + idCandidato + '", estado: "' + estado + '", idPublicacion: "' + idPublicacion + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

// Eliminar imagen
function EliminarImagen(idImagen) {
    Swal.fire({
        title: 'Desea eliminar la imagen?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
            $.ajax({
                type: 'POST',
                url: controller + 'DelImagenesEmpresa',
                data: '{ idImagen: "' + idImagen + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.data == 1) {
                        window.location.reload();
                    }
                }
            });
        }
    })
    
}


// Perfil usuario empresa
function ajaxGuardaPerfilUsuarioEmpresa(controller, telefonoE, correoE) {

    $.ajax({
        type: 'POST',
        url: controller + 'ActualizarPerfilUsuarioEmpresa',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        data: '{ telefono: "' + telefonoE + '", correo: "' + correoE + '" }',
        success: function (response) {
            if (response.data == "1") {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Perfil actualizado con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
            else if (response.data == "-1") {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Ha ocurrido un error al actualizar.',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }

        }
    });
}


// Asignacion de preguntas por publicacion
function ajaxGuardaAsignacionPreguntaPublicacion(controller, publicacion, pregunta) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarAsignacionPreguntasPublicacion',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        data: '{ publicacion: "' + publicacion + '", pregunta: "' + pregunta + '" }',
        success: function (response) {
            if (response.data == "1") {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Pregunta asignada con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }
            else if (response.data == "-1") {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Ha ocurrido un error al asignar.',
                    showConfirmButton: false,
                    timer: 1500
                })
                window.location.reload();
            }

        }
    });
}




// Inicio Sesion Usuario Empresa

function ajaxInicioSesionUsuarioEmpresa(controller, usuario, password) {

    if (!usuario.includes("@")) {
        if (validaRut(usuario)) {
            usuario = formateaRut(usuario);
        }
    }
    $.ajax({
        type: 'POST',
        url: controller + 'InicioSesion',
        data: '{ usuario: "' + usuario + '",  clave: "' + password + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code == "200") {

                location.href = "/UsuarioEmpresa/Index";
            }
            else {
                $("#loginError").html(response.Message);
                $(".signInLoaderContent").hide();
            }
        },
        error: function (xhr) {
            $("#loginError").html('Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde');
        }
    });
}

function ajaxCierraSesionEmpresa(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'CerrarSesion',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            window.location.href = response.Retorno;
        }
    });
}

// Loading empresa
function ajaxViewPartialLoadingEmpresa(username, password) {
    controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoadingSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLoginE").html(xhr.responseText);
            ajaxInicioSesionEmpresa(controller, username, password);
            
        }
    });

}

// Loading usuario empresa
function ajaxViewPartialLoadingEmpresa(username, password) {
    controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoadingSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLoginE").html(xhr.responseText);
            var tiempo = setInterval(Tiempo(), 3000);

            if (tiempo > 0) {
                ajaxInicioSesionEmpresa(controller, username, password);
            }
        }
    });

}


// Loading usuario empresa
function ajaxViewPartialLoadingUsuarioEmpresa(username, password) {
    controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/UsuarioEmpresa/";

    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoadingSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLoginUE").html(xhr.responseText);
            var tiempo = setInterval(Tiempo(), 3000);

            if (tiempo > 0) {

                ajaxInicioSesionUsuarioEmpresa(controller, username, password);
            }
        }
    });

}

function Tiempo() {
    var valor = 1;
    return valor;
}

function validoArchivo(archivo) {

    var extensiones_permitidas = [".jpg", ".jpeg", ".png"];
    var rutayarchivo = archivo.value;
    archivoNombre = archivo.files[0].name;
    var ultimo_punto = archivo.value.lastIndexOf(".");
    var extension = rutayarchivo.slice(ultimo_punto, rutayarchivo.length);
    if (extensiones_permitidas.indexOf(extension) == -1) {
        Swal.fire({
            position: 'center',
            type: 'warning',
            title: 'Extensión de archivo no valida.',
            showConfirmButton: false,
            timer: 2500
        })

        document.getElementById(archivo.id).value = "";
        return;
    }
}

// envio de mensajes a otro usuario
function ajaxEnviarMensajeAUsuario(controller, usuario, mensaje) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEnvioMensajeAUsuario',
        data: '{ idUsuario: "' + usuario + '", mensaje: "' + mensaje + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

// envio de comentario a otro usuario
function ajaxEnviarComentarioAUsuario(controller, usuario, comentario) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEnvioComentarioAUsuario',
        data: '{ idUsuario: "' + usuario + '", comentario: "' + comentario + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                window.location.reload();
            }
        }
    });
}

