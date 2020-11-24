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
            window.location.reload();
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

function ajaxRegistroPreguntasEmpresa(controller, id) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarAsignacionPreguntas',
        data: '{ idPregunta: "' + id + '"}',
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