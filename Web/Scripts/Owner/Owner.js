$(document).ready(function () {
    // Validar datos
    $(document).on('change', "#rut", function () {
        var rut = $("#rut").val();
        if (rut != "" && rut != null && rut != undefined) {
            if (validaRut(rut)) {
                $("#rut").val(formateaRut(rut));
                $("#rutError").html('');
            }
            else {
                $("#rutError").html('Rut ingresado no es valido');
            }
        }
        else {
            $("#rutError").html('');
        }
        validarErrorIngreso();
    });

    $(document).on('change', "#correo", function () {
        var mail = $("#correo").val();
        if (mail != "" && mail != null && mail != undefined) {
            if (ValidateEmail(mail)) {
                $("#mailError").html('');
            }
            else {
                $("#mailError").html('Correo ingresado no es valido');
            }
        }
        else {
            $("#mailError").html('');
        }
        validarErrorIngreso();
    });

    $(document).on('blur', "#repetirclave", function () {
        var clave = $("#clave").val();
        var repetir = $("#repetirclave").val();
        if (clave == "" && repetir != clave && clave == undefined) {
            $("#repetirError").html('Las contraseñas ingresadas no coinciden');

        }
        validarErrorIngreso();
    });

    // Inicio sesion usuario
    $(document).on('click', "#inicioSesion", function () {
        var controller = '';
        var rut = $("#username").val().replace(/\./g, '');
        var password = $("#password").val();

        if (rut == "" || rut == null || rut == undefined || password == "" || password == null || password == undefined) {
            alert('Ingrese su rut y contraseña para iniciar sesión');
            //$("#loginError").html('Ingrese su rut y contraseña para iniciar sesión');
            return;
        }
        controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Auth/";
        ajaxInicioSesion(controller, rut, password);
    });

    $(document).on('click', ".signIn", function () {
        $("#username").val('');
        $("#password").val('');
        $("#loginError").html('');

        $("#modalSignIn").modal("show");
    });

    // Cerrar sesion usuario
    $(document).on('click', ".signOut", function () {
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Auth/";

        ajaxCierraSesion(controller);
    });

    // Modal Inicio sesion empresa
    $(document).on('click', ".signInEmpresa", function () {
        $("#username").val('');
        $("#password").val('');
        $("#loginError").html('');

        $("#modalSignInEmpresa").modal("show");
    });


    // Inicio sesion empresa
    $(document).on('click', "#inicioSesionEmpresa", function () {
        var controller = '';
        var rut = $("#username").val().replace(/\./g, '');
        var password = $("#password").val();

        if (rut == "" || rut == null || rut == undefined || password == "" || password == null || password == undefined) {
            alert('Ingrese su rut y contraseña para iniciar sesión');
            return;
        }
        controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
        ajaxInicioSesionEmpresa(controller, rut, password);
    });

    // Cerrar sesion usuario
    $(document).on('click', ".signOutEmpresa", function () {
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxCierraSesionEmpresa(controller);
    });


    $(document).on('keydown', ".soloLetras", function (e) {
        var tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[A-Za-z\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
    });

    // Modal Registro Publicaciones
    $(document).on('click', ".NewPublicacion", function () {
        $("#titulo").val('');
        $("#descripcion").val('');
        $("#monto").val('');
        $("#modalRegistroPublicacion").modal("show");
    });

    $(document).on('keypress', ".soloNumeros", function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) {
            return true;
        }
        patron = /[0-9]/;
        tecla_final = String.fromCharCode(tecla);
        return patron.test(tecla_final);
    });

    $(document).on('keypress', ".soloRut", function (e) {
        tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) {
            return true;
        }
        patron = /[0-9kK]/;
        tecla_final = String.fromCharCode(tecla);
        return patron.test(tecla_final);
    });


    $(document).on("keyup", "#password", function (e) {
        var code = e.keyCode ? e.keyCode : e.which;
        if (code === 13) {
            $("#inicioSesionEmpresa").trigger("click");
        }
    });

    $(document).on('keyup', "#nombre", function () {
        var nombre = $("#nombre").val();
        var code = e.keyCode ? e.keyCode : e.which;

        if (code === 13) {
           
            $("#inicioSesionEmpresa").trigger("click");
        }
    });

});

function validaRut(rut) {
    var valor = rut.replace(/\./g, '');
    valor = valor.replace(/\-/g, '');

    cuerpo = valor.slice(0, -1);
    dv = valor.slice(-1).toUpperCase();

    rut = cuerpo + '-' + dv

    if (cuerpo.length < 7) {
        return false;
    }

    suma = 0;
    multiplo = 2;

    for (i = 1; i <= cuerpo.length; i++) {
        index = multiplo * valor.charAt(cuerpo.length - i);
        suma = suma + index;
        if (multiplo < 7) {
            multiplo = multiplo + 1;
        }
        else {
            multiplo = 2;
        }
    }

    dvEsperado = 11 - (suma % 11);

    dv = (dv == 'K') ? 10 : dv;
    dv = (dv == 0) ? 11 : dv;

    if (dvEsperado != dv) {
        return false;
    }

    return true;
}

function formateaRut(rut) {
    var actual = rut.replace(/^0+/, "");
    if (actual != '' && actual.length > 1) {
        var sinPuntos = actual.replace(/\./g, "");
        var actualLimpio = sinPuntos.replace(/-/g, "");
        var inicio = actualLimpio.substring(0, actualLimpio.length - 1);
        var rutPuntos = "";
        var i = 0;
        var j = 1;
        for (i = inicio.length - 1; i >= 0; i--) {
            var letra = inicio.charAt(i);
            rutPuntos = letra + rutPuntos;
            if (j % 3 == 0 && j <= inicio.length - 1) {
                rutPuntos = "." + rutPuntos;
            }
            j++;
        }
        var dv = actualLimpio.substring(actualLimpio.length - 1);
        rutPuntos = rutPuntos + "-" + dv;
    }
    return rutPuntos;
}

function ValidateEmail(inputText) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.match(mailformat)) {
        return true;
    }
    else {
        return false;
    }
}

function validarErrorIngreso() {
    var rutError = $("#rutError").text();
    var mailError = $("#rutError").text();
    var repetirPassError = $("#repetirError").text();
    var error = false;


    if (rutError != '' && rutError != null && rutError != undefined) {
        error = true;
    }
    if (mailError != '' && mailError != null && mailError != undefined) {
        error = true;
    }
    if (repetirPassError != '' && repetirPassError != null && repetirPassError != undefined) {
        error = true;
    }

    $("#error").val(error);
}