function ajaxSignInUser(controller, usuario, password) {
    $.ajax({
        type: 'POST',
        url: controller + 'SignInUser',
        data: '{ usuario: "' + btoa(usuario) + '",  clave: "' + btoa(password) + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.Code == "200") {
                window.location.href = response.PathRedirect;
            }
            else {
                ajaxViewPartialFormSignIn(controller, response.Message, atob(response.Correo));
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

            $("#modalRegistroUsuario").modal("show");
        },
        error: function (xhr) {

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


function ajaxViewPartialLoadingSignIn(controller, username, password) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialLoadingSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLogin").html(xhr.responseText);

            ajaxSignInUser(controller, username, password);
        }
    });
}

function ajaxViewPartialFormSignIn(controller, message, correo) {
    $.ajax({
        type: 'POST',
        url: controller + 'ViewPartialFormSignIn',
        data: '{ }',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        error: function (xhr) {
            $("#contentLogin").html(xhr.responseText);
            $("#username").val(correo);
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
        }
    });
}