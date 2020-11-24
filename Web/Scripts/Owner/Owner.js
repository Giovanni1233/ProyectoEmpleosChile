$(document).ready(function () {
    // cargamos el ordenamiento principal
    var ordenamiento = $('input[type = radio][name = radioOrdenamiento]').val();
    $("#hiddenOrdenamiento").val(ordenamiento);

    $('.collapse').collapse()

    // Validar datos
    $(document).on('change', "#rut", function () {
        var rut = $("#rut").val();
        var error = '';
        if (rut != "" && rut != null && rut != undefined) {
            if (validaRut(rut)) {
                $("#rut").val(formateaRut(rut));
            }
            else {
                error = '<li>Rut ingresado no es valido</li>';
            }
        }
        $("#errorRegistro").html(error);
        //validarErrorIngreso();
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
        //validarErrorIngreso();
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
        var rut = $("#username").val();
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

    // Modificar publicacion
    $(document).on('click', ".btnModificarPublicacion", function () {
        var controller = '';
        var valor = $("#valor").val();
        var descripcion = $("#descripcionPublicacion").val();
        var id = $("#IdPublicacion").val();


        if (valor == 1) // empresa
        {
            controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
        } else { // usuario empresa
            controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/UsuarioEmpresa/";
        }

        ajaxActualizarPublicacion(controller, id, descripcion);
    });

    // Modal edicion publicacion
    $(document).on('click', "#EditarPublicacion", function () {
        $("#modalEdicionPublicacion").modal("show");
    });


    // Modal registro usuario empresa
    $(document).on('click', ".BtnNuevoU", function () {

        $("#modalRegistroUsuarioEmpresa").modal("show");
    });

    // Modal planes empresa 
    $(document).on('click', ".PlanesEmpresa", function () {
        $("#modalPlanesEmpresa").modal("show");
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

    $(document).on('change', "#region", function () {
        var region = $("#region").val();
        var controller = GetControllerAuth();

        ajaxGetCiudad(controller, region);
    });

    $(document).on('change', "#ciudad", function () {
        var ciudad = $("#ciudad").val();
        var controller = GetControllerAuth();

        if (parseInt(ciudad) > 0) {
            ajaxGetComuna(controller, ciudad);
        }
        else {
            $("#comuna").html('<option value="0">Seleccione...</option>');
        }
    });


    // Filtros de busqueda publicaciones
    //$(document).on('click', "#btnGenerar", function () {

    //    var nombre = $("#NombrePublicacion").val();
    //    var fecha = $("#FechaPublicacion").val();
    //    controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
    //    ajaxGetPublicacionFiltros(controller, nombre, fecha);
    //});

    // Modal Pregunta Postulacion
    $(document).on('click', ".btnModalNuevaPregunta", function () {
        $("#nombrePregunta").val('');
        $("#modalPreguntaPostulacion").modal("show");
    });

    // Guardar pregunta postulacion
    $(document).on('click', ".btnGuardarPregunta", function () {

        var controller = '';
        var valor = $("#nombrePregunta").val();
        controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxGuardarPreguntaPostulacion(controller, valor);
    });

    // Seccion de activacion / desactivacion de publicacion
    $(document).on('click', ".ActivarPublicacion", function () {
        var idPublicacion = $(this).attr("id");
        var estado = "1";
        var url = $("#url").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { 'idPublicacion': idPublicacion, 'estado': estado },
            success: function () {
                window.location.reload();
            }
        });

    });

    $(document).on('click', ".DesactivarPublicacion", function () {
        var idPublicacion = $(this).attr("id");
        var estado = "0";
        var url = $("#url").val();
        Swal.fire({
            title: '¿Desea desactivar la publicación?',
            text: "Al desactivar la publicación, esta ya no será visible en la página",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, desactivar',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: url,
                    data: { 'idPublicacion': idPublicacion, 'estado': estado },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        })
    });



    // Seccion de activacion / desactivacion de preguntas en configuracion
    $(document).on('click', ".ActivarPregunta", function () {
        var idPregunta = $(this).attr("id");
        var estado = "1";
        var url = $("#url").val();
        $.ajax({
            type: "POST",
            url: url,
            data: { 'idPregunta': idPregunta, 'estado': estado },
            success: function () {
                window.location.reload();
            }
        });

    });

    $(document).on('click', ".DesactivarPregunta", function () {
        var idPregunta = $(this).attr("id");
        var estado = "0";
        var url = $("#url").val();
        Swal.fire({
            title: '¿Desea desactivar la pregunta?',
            text: "Al desactivar la pregunta, no podrá visualizarse en el ingreso de publicación",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, desactivar',
            cancelButtonText: 'No'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: url,
                    data: { 'idPregunta': idPregunta, 'estado': estado },
                    success: function () {
                        window.location.reload();
                    }
                });
            }
        })
    });

    // Modificar publicacion
    $(document).on('click', ".btnAgregarPregunta", function () {
        var controller = '';
        var idPregunta = $("#PreguntaSeleccion").val();

        controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxRegistroPreguntasEmpresa(controller, idPregunta);
    });

    // Escritura de combos
    $(document).on('keyup', "#NombrePublicacion", function () {
        var nombre = $("#NombrePublicacion").val();
        
        $("#hiddenNombre").val(nombre);

    });

    $(document).on('keyup', "#FechaPublicacion", function () {
        var fecha = $("#FechaPublicacion").val();
        
        $("#hiddenFecha").val(fecha);

    });

    $('input[type=radio][name=radioOrdenamiento]').change(function () {
        var ordenamiento = $(this).val();
        $("#hiddenOrdenamiento").val(ordenamiento);

    });

    $(document).on('click', "#FiltroGatillo", function () {
        $("#buscarFiltros").trigger("click");

    });


    // Modal notificaciones
    $(document).on('click', ".notificacionEmpresa", function () {
      
        $("#modalNotificacionesEmpresa").modal("show");
    });


    // Radios votacion
    $('input[type=radio][name=radioVotacion]').change(function () {
        var valoracion = $(this).val();
        var urlValoracion = $("#urlVotacion").val();
        var idPub = $("#idPublicacionV").val();
        var idUsuario = $("#idUsuario").val();
        $.ajax({
            type: "POST",
            url: urlValoracion,
            data: { 'votacion': valoracion, 'idPublicacion': idPub, 'idUsuario': idUsuario },
            success: function () {
                window.location.reload();
            }
        });

    });




    $(document).on('click', "#registraUsuario", function () {
        var rut = $("#rut").val();
        var nombre1 = $("#nombre1").val();
        var nombre2 = $("#nombre2").val();
        var apellidoP = $("#apellidoP").val();
        var apellidoM = $("#apellidoM").val();
        var correo = $("#correo").val();
        var correoRepetir = $("#correoRepetir").val();
        var password = $("#passWord").val();
        var passwordRepetir = $("#passWordRepetir").val();
        var fechaNacimiento = $("#fechaNacimiento").val();
        var controller = GetControllerAuth();
        var error = validarErrorIngreso();

        document.getElementById('errorRegistro').innerHTML = error;

        //if (error == "" || error == null || error == undefined) {

        //ajaxRegistroUsuario(controller, rut, nombre1, nombre2, apellidoP, apellidoM,
        //    correo, correoRepetir, password, passwordRepetir, fechaNacimiento);
        $("#modalRegistroUsuario").modal("show");
        //}
    });

    //se ejecuta al cerrar un modal
    $("#modalRegistroUsuario").on('hide.bs.modal', function () {
        $("#username").val('');
        $("#password").val('');
        $("#loginError").html('');

        $("#modalSignIn").modal("show");
    });

    $("#modalSignIn").on('hide.bs.modal', function () {

    });

});

function ModalShowMensajeRegistro() {
    $("#modalMensajeRegistro").modal("show");
}

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
    var rut = $("#rut").val().replace(/\./g, '');;
    var nombre = $("#nombre1").val();
    var apellidoP = $("#apellidoP").val();
    var correo = $("#correo").val();
    var correoRepetir = $("#correoRepetir").val();
    var password = $("#passWord").val();
    var passwordRepetir = $("#passWordRepetir").val();
    var fechaNacimiento = $("#fechaNacimiento").val();

    var error = '';
    if (rut != "" && rut != null && rut != undefined) {
        if (validaRut(rut)) {
            $("#rut").val(formateaRut(rut));
        }
        else {
            error += '<li>Rut ingresado no es valido</li>';
        }
    }
    else {
        error += '<li>Debe ingresar un Rut</li>';
    }

    if (nombre == "" || nombre == null || nombre == undefined) {
        error += '<li>Debe ingresar nombre</li>'
    }

    if (apellidoP == "" || apellidoP == null || apellidoP == undefined) {
        error += '<li>Debe ingresar apellido</li>'
    }

    if (correo == "" || correo == null || correo == undefined) {
        error += '<li>Debe ingresar un correo</li>'
    }
    else {
        if (!ValidateEmail(correo)) {
            error += '<li>Correo ingresado no es valido';
        }
        else {
            if (correoRepetir == "" || correoRepetir == null || correoRepetir == undefined) {
                error += '<li>Debe repetir correo</li>'
            }
            else {
                if (correo != correoRepetir) {
                    error += '<li>Repetición de correo no coindice</li>'
                }
            }
        }
    }

    if (password == "" || password == null || password == undefined) {
        error += '<li>Debe ingresar una contraseña</li>'
    }
    else {
        if (passwordRepetir == "" || passwordRepetir == null || passwordRepetir == undefined) {
            error += '<li>Debe repetir la contraseña</li>'
        }
        else {
            if (password != passwordRepetir) {
                error += '<li>Repetición de contraseñaW no coindice</li>'
            }
        }
    }

    if (fechaNacimiento == "" || fechaNacimiento == null || fechaNacimiento == undefined) {
        error += '<li>Debe ingresar una fecha de nacimiento</li>'
    }


    return error;
}



function GetControllerAuth() {
    var prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];
    var prefix = "";

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