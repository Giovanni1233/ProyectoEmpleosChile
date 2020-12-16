function ajaxInicioSesion(controller, usuario, password) {
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
            if (response.Code == "200") {
                $("#userEmpresaHidden").val(response.Empresa.Rut);
                $("#loginError").html('');

                location.href = "/Empresa/Principal";
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

function ajaxRegistroPreguntasEmpresa(controller, nombrePregunta) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarAsignacionPreguntas',
        data: '{ nombrePregunta: "' + nombrePregunta + '"}',
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

//PartialView
function ajaxViewPartialErrorSignIn(controller, message) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialErrorSignIn',
        data: '{ message: "' + message + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#errorSignIn").html(xhr.responseText);
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
function ajaxGuardarValoracionPublicacion(controller, valorvotacion, idPublicacion, idUsuario) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarVotacionPublicacion',
        data: '{ votacion: "' + valorvotacion + '", idPublicacion: "' + idPublicacion + '", idUsuario: "' + idUsuario + '"}',
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