$(document).ready(function () {

    $(document).on('keydown', ".soloLetras", function (e) {
        let tecla = (document.all) ? e.keyCode : e.which;
        if (tecla == 8) return true;
        patron = /[A-Za-z\s]/;
        te = String.fromCharCode(tecla);
        return patron.test(te);
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


    $(document).on('click', "#registraUsuario", function () {
        let rut = $("#rut").val();
        let nombre1 = $("#nombre1").val();
        let nombre2 = $("#nombre2").val();
        let apellidoP = $("#apellidoP").val();
        let apellidoM = $("#apellidoM").val();
        let correo = $("#correo").val();
        let correoRepetir = $("#correoRepetir").val();
        let password = $("#passWord").val();
        let passwordRepetir = $("#passWordRepetir").val();
        let fechaNacimiento = $("#fechaNacimiento").val();
        let controller = GetControllerAuth();
        let error = validateErrorIngreso(rut, nombre1, apellidoP, correo, correoRepetir, password, passwordRepetir, fechaNacimiento);

        document.getElementById('errorRegistro').innerHTML = error;
        $("#modalRegistroUsuario").modal("show");

        if (error == "" || error == null || error == undefined) {
            ajaxRegistroUsuario(controller, rut, nombre1, nombre2, apellidoP, apellidoM, correo, correoRepetir, password, passwordRepetir, fechaNacimiento);
            $("#modalRegistroUsuario").modal("show");
        }
    });

    $(document).on('click', "#terminos", function () {
        $(this).prop("checked") ? $("#registraUsuario").attr('disabled', false) : $("#registraUsuario").attr('disabled', true)
    });

    $(document).on('click', "#signInUser", function (e) {
        e.preventDefault();
        let domain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + '/Auth/';
        let user = $("#username").val();
        let pass = $("#password").val();


        ajaxViewPartialLoadingSignIn(domain, user, pass);
    });


    $(document).on('change', "#ciudad", function () {
        let ciudad = $("#ciudad").val();
        let controller = GetControllerAuth();

        if (parseInt(ciudad) > 0) {
            ajaxGetComuna(controller, ciudad);
        }
        else {
            $("#comuna").html('<option value="0">Seleccione...</option>');
        }
    });

    $(document).on('change', "#correo", function () {
        let mail = $("#correo").val();
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
        //validateErrorIngreso();
    });

    $(document).on('change', "#region", function () {
        let region = $("#region").val();
        let controller = GetControllerAuth();

        ajaxGetCiudad(controller, region);
    });

    $(document).on('change', "#rut", function () {
        let rut = $("#rut").val();
        let error = '';
        if (rut != "" && rut != null && rut != undefined) {
            if (validateRut(rut)) {
                $("#rut").val(formatRut(rut));
            }
            else {
                error = '<li>Rut ingresado no es valido.</li>';
            }
        }
        $("#errorRegistro").html(error);
        //validateErrorIngreso();
    });

    $(document).on('change', "#username", function () {
        let user = $("#username").val();

        if (user !== "" && user !== null && user !== undefined) {
            if (!user.includes("@")) {
                user = formateaRut(user);
            }
        }
    });


    //se ejecuta al cerrar un modal
    $("#modalRegistroUsuario").on('hide.bs.modal', function () {
        $("#username").val('');
        $("#password").val('');
        $("#loginError").html('');

        //$("#modalSignIn").modal("show");


    });

    $("#modalSignIn").on('hide.bs.modal', function () {

    });

});


//FUNCTIONS
function ModalShowMensajeRegistro() {
    $("#modalMensajeRegistro").modal("show");
}

function formatRut(rut) {
    let actual = rut.replace(/^0+/, "");
    if (actual != '' && actual.length > 1) {
        let sinPuntos = actual.replace(/\./g, "");
        let actualLimpio = sinPuntos.replace(/-/g, "");
        let inicio = actualLimpio.substring(0, actualLimpio.length - 1);
        let rutPuntos = "";
        let i = 0;
        let j = 1;
        for (i = inicio.length - 1; i >= 0; i--) {
            let letra = inicio.charAt(i);
            rutPuntos = letra + rutPuntos;
            if (j % 3 == 0 && j <= inicio.length - 1) {
                rutPuntos = "." + rutPuntos;
            }
            j++;
        }
        let dv = actualLimpio.substring(actualLimpio.length - 1);
        rutPuntos = rutPuntos + "-" + dv;
    }
    return rutPuntos;
}

function validateErrorIngreso(rut, nombre, apellidoP, correo, correoRepetir, password, passwordRepetir, fechaNacimiento) {
    let error = '';

    if (rut != "" && rut != null && rut != undefined) {
        if (validateRut(rut)) {
            $("#rut").val(formatRut(rut));
        }
        else {
            error += '<li>Rut ingresado no es valido.</li>';
        }
    }
    else {
        error += '<li>Debe ingresar un Rut.</li>';
    }

    if (nombre == "" || nombre == null || nombre == undefined) {
        error += '<li>Debe ingresar nombre.</li>'
    }

    if (apellidoP == "" || apellidoP == null || apellidoP == undefined) {
        error += '<li>Debe ingresar apellido.</li>'
    }

    if (correo == "" || correo == null || correo == undefined) {
        error += '<li>Debe ingresar un correo.</li>'
    }
    else {
        if (!ValidateEmail(correo)) {
            error += '<li>Correo ingresado no es valido';
        }
        else {
            if (correoRepetir == "" || correoRepetir == null || correoRepetir == undefined) {
                error += '<li>Debe repetir correo.</li>'
            }
            else {
                if (correo != correoRepetir) {
                    error += '<li>Repetición de correo no coindice.</li>'
                }
            }
        }
    }

    if (password == "" || password == null || password == undefined) {
        error += '<li>Debe ingresar una contraseña.</li>'
    }
    else {
        if (validateOnlyNumber(password)) {
            if (passwordRepetir == "" || passwordRepetir == null || passwordRepetir == undefined) {
                error += '<li>Debe repetir la contraseña.</li>'
            }
            else if (password != passwordRepetir) {
                error += '<li>Repetición de contraseña no coindice.</li>'
            }
        }
        else {
            error += '<li>La contraseña debe contener solo digitos.</li>'
        }
    }

    if (fechaNacimiento == "" || fechaNacimiento == null || fechaNacimiento == undefined) {
        error += '<li>Debe ingresar una fecha de nacimiento.</li>'
    }


    return error;
}

function validateRut(rut) {
    let valor = rut.replace(/\./g, '');
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

function ValidateEmail(inputText) {
    let mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (inputText.match(mailformat)) {
        return true;
    }
    else {
        return false;
    }
}

function validateOnlyNumber(inputText) {
    const regex = /^[0-9]*$/;
    return regex.test(inputText);
}


function GetControllerAuth() {
    let prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
    let prefix = "";

    if (window.location.href.split('/')[2].indexOf(atob("bG9jYWxob3N0OjQ0MzA0")) !== -1) {
        if ((window.location.href.split('/').length - 1) === 5) {
            prefix = prefix + "/" + window.location.href.split('/')[window.location.href.split('/').length - 1] + "/";
        } else {
            if (window.location.href.split('/')[3] != "") {
                prefix = prefix + "/" + window.location.href.split('/')[3] + "/";
            }
            else {
                prefix = prefix + "/Auth/";
            }

        }
    }
    else {
        if ((window.location.href.split('/').length - 1) === 5) {
            prefix = prefix + "/" + window.location.href.split('/')[3] + "/" + window.location.href.split('/')[window.location.href.split('/').length - 2] + "/";
        } else {
            prefix = prefix + "/" + window.location.href.split('/')[3] + "/";
        }
    }

    if (prefix == "") {
        prefix = "/Auth/";
    }

    return prefixDomain + prefix;
}