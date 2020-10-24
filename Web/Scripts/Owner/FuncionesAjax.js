function ajaxInicioSesion(controller, usuario, password) {
    $.ajax({
        type: 'POST',
        url: controller + 'InicioSesion',
        data: '{ usuario: "' + usuario + '",  clave: "' + password + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code == "200") {
                $("#userUsuarioHidden").val(response.Usuario.Rut);
                $("#perfilUsuarioHidden").val(response.Usuario.Perfil);

                $("#loginError").html('');
                $(".signInLoaderContent").hide();

                location.reload();
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
    debugger;
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