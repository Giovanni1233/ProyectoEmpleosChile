
function ajaxSignInUser(controller, user, pass) {
    $.ajax({
        type: 'POST',
        url: controller + 'SignInUser',
        data: '{ user: "' + user + '",  pass: "' + pass + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code == "200") {
                window.location.href = response.PathRedirect;
            }
            else {
                ajaxViewPartialFormSignIn(controller, response.Message, response.User.Rut);
            }
        },
        error: function (xhr) {
            $("#loginError").html('Ha ocurrido algo inesperado en la plataforma, intentelo mas tarde');
        }
    });
}

function ajaxSignOut(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'SignOut',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            window.location.href = response.Home;
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

function ajaxRegistroUsuario(controller, rut, nombre1, nombre2, apellidoP, apellidoM, correo, correoRepetir, password, passwordRepetir, fechaNacimiento) {
    $.ajax({
        type: 'POST',
        url: controller + 'RegistroUsuario',
        data: '{ rut: "' + rut + '",  nombre1: "' + nombre1 + '",  nombre2: "' + nombre2 +
            '",  apellidoP: "' + apellidoP + '",  apellidoM: "' + apellidoM +
            '",  correo: "' + correo + '",  correoRepetir: "' + correoRepetir +
            '",  password: "' + password + '",  passwordRepetir: "' + passwordRepetir +
            '",  fechaNacimiento: "' + fechaNacimiento + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code != '600') {
                if (response.Code === '200') {
                    $("#modalRegistroUsuario").modal("show");
                }
                else {
                    document.getElementById('errorRegistro').innerHTML = response.Message;
                }
            }
            else {
                document.getElementById('errorRegistro').innerHTML = response.Message;
            }
        },
        error: function (xhr) {
            document.getElementById('errorRegistro').innerHTML = response.Message;
        }
    });
}


//PartialView
function ajaxViewPartialLoadingSignIn(controller, username, pass) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoadingSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLogin").html(xhr.responseText);
            ajaxSignInUser(controller, username, pass);
        }
    });
}

function ajaxViewPartialFormSignIn(controller, message, user) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialFormSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLogin").html(xhr.responseText);
            $("#username").val(user);
            ajaxViewPartialErrorSignIn(controller, message);
        }
    });
}

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

            $("#username").focus();
        }
    });
}


// Agregado giovanni diaz 26-12-2020
function ajaxEnviarMensajeAEmpresa(controller, empresa, mensaje) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEnvioMensajeAEmpresa',
        data: '{ idEmpresa: "' + empresa + '", mensaje: "' + mensaje + '"}',
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

function ajaxResponderMensajeAReceptor(controller, idMensaje, idAutor, mensaje) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarRespuestaMensajeAReceptor',
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



// guardar experiencia en perfil usuario

function ajaxRegistroExperienciaPerfilUsuario(controller, empresaNombre, destacoEmpresa, mejorarEmpresa, fechaD, fechaH, actualmente, descripcion, recomendacion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarExperienciaPerfilUsuario',
        data: '{ empresaNombre: "' + empresaNombre + '", recomendacion: "' + recomendacion + '", descripcion: "' + descripcion + '", destacoEmpresa: "' + destacoEmpresa + '", mejorarEmpresa: "' + mejorarEmpresa + '", fechaD: "' + fechaD + '", fechaH: "' + fechaH + '", actualmente: "' + actualmente + '"}',
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


function EliminarExperiencia(idExperiencia) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    $.ajax({
        type: 'POST',
        url: controller + 'EliminarExperienciaPerfilUsuario',
        data: '{ idExperiencia: "' + idExperiencia + '"}',
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


// guardar educacion usuario perfil 
function ajaxRegistroEducacionPerfilUsuario(controller, centroNombre, estadoEdu, tituloNombreEdu, fechaD, fechaH, descripcion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEducacionPerfilUsuario',
        data: '{ centroNombre: "' + centroNombre + '", estadoEdu: "' + estadoEdu + '", tituloNombreEdu: "' + tituloNombreEdu + '", descripcion: "' + descripcion + '", fechaD: "' + fechaD + '", fechaH: "' + fechaH + '"}',
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

function EliminarEducacion(idEducacion) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    $.ajax({
        type: 'POST',
        url: controller + 'EliminarEducacionPerfilUsuario',
        data: '{ idEducacion: "' + idEducacion + '"}',
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

// // guardar idioma usuario perfil 
function ajaxRegistroIdiomaPerfilUsuario(controller, idiomaId, nivelIdioma) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarIdiomaPerfilUsuario',
        data: '{ idIdioma: "' + idiomaId + '", nivel: "' + nivelIdioma + '"}',
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

function EliminarIdioma(idIdioma) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    $.ajax({
        type: 'POST',
        url: controller + 'EliminarIdiomaPerfilUsuario',
        data: '{ idIdioma: "' + idIdioma + '"}',
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


// // guardar habilidad usuario perfil 
function ajaxRegistroHabilidadPerfilUsuario(controller, habilidadID, nivelHabilidad) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarHabilidadPerfilUsuario',
        data: '{ idHabilidad: "' + habilidadID + '", nivel: "' + nivelHabilidad + '"}',
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

function EliminarHabilidad(idHabilidad) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    $.ajax({
        type: 'POST',
        url: controller + 'EliminarHabilidadPerfilUsuario',
        data: '{ IdHabilidad: "' + idHabilidad + '"}',
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

// // guardar datos usuario perfil 
function ajaxRegistroPerfilUsuario(controller, nombre1, nombre2, apellido1, apellido2, telefono, correo, descripcion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarDatosPerfilUsuario',
        data: '{ nombre1: "' + nombre1 + '",nombre2: "' + nombre2 + '",apellido1: "' + apellido1 + '",apellido2: "' + apellido2 + '", telefonoPerfil: "' + telefono + '", correoPerfil: "' + correo + '", descripcionPersonal: "' + descripcion + '"}',
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

// // guardar datos usuario perfil 
function ajaxRegistroPerfilProfesionalUsuario(controller, tituloperfil, descripcionperfil) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarDatosPerfilProfesionalUsuario',
        data: '{ tituloperfil: "' + tituloperfil + '", descripcionperfil: "' + descripcionperfil + '" }',
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


// postulacion a empleo
function PostularEmpleo(dato1, idPublicacion) {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/App/";
    $.ajax({
        type: 'POST',
        url: controller + 'CargarDatosPreguntas',
        data: '{ idPublicacion: "' + idPublicacion + '" }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.datos != "") {
                $("#preguntasSinDescripcion").empty();
                for (var i = 0; i < response.datos.length; i++) {
                        $('#preguntasSinDescripcion').append('<label>' + response.datos[i].PreguntaNombre + ': </label><input type = "text" class="form-control" name = "inputMultiples[]"  id = "' + response.datos[i].NombreCorto + '" /> ');
                   
                }
                //$.each(response.datos, function (i, val) {
                //    if (val.TipoPregunta == "1") {

                //        $('.preguntasSinDescripcion').append('<label>' + val.PreguntaNombre + ': </label><input type = "text" class="form-control" name = "' + val.NombreCorto + '"  id = "' + val.NombreCorto + '" /> ');
                //    }

                //    if (val.TipoPregunta == "2") {
                //        $('.preguntasConDescripcion').append('<label for="val.Nombre">' + val.PreguntaNombre + ': </label><textarea class= "form-control" rows = "5" style = "resize:none; width:100%; max-width:100%;" name = "' + val.NombreCorto + '" id = "' + val.NombreCorto + '" ></textarea >')
                //    }
                   
                //});
                $("#modalPreguntasPublicacion").modal("show");
            }
        }
    });



    //if (idUsuario == 0) {
    //    Swal.fire({
    //        position: 'center',
    //        icon: 'error',
    //        title: 'Debe loguearse para postular',
    //        showConfirmButton: false,
    //        timer: 1500
    //    })
    //    return false;
    //}
    //else {

    //    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    //    $.ajax({
    //        type: 'POST',
    //        url: controller + 'GuardarPostulacionEmpleo',
    //        data: '{ idPublicacion: "' + idPublicacion + '" }',
    //        dataType: 'json',
    //        contentType: 'application/json',
    //        async: true,
    //        success: function (response) {
    //            if (response.data == 1) {
    //                window.location.reload();
    //            }
    //        }
    //    });
    //}

}



//CURRICULUM
function ajaxDeleteCV(controller, user) {
    $.ajax({
        type: 'POST',
        url: controller + 'DeleteCV',
        data: '{ user: "' + user + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            //
        }
    });
}