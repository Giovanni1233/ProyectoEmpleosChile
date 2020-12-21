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