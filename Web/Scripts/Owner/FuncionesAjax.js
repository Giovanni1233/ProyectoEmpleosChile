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