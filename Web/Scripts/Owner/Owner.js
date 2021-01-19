$(document).ready(function () {

    // cargamos el ordenamiento principal
    var ordenamiento = $('input[type = radio][name = radioOrdenamiento]').val();
    $("#hiddenOrdenamiento").val(ordenamiento);

    $('input[type=radio][name=radioVotacion]').change(function () {
        $("#hiddenvotoIndicado").val(this.value);
    });

    $('.mdb-select').materialSelect();
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
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

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
            $("#errorInicioSesionEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar las credenciales.</div>');
            setInterval(function () { $("#errorInicioSesionEmpresa").html(''); }, 3000);
            return false;
        }

        ajaxViewPartialLoadingEmpresa(rut, password);

    });

    // Cerrar sesion usuario
    $(document).on('click', ".signOutEmpresa", function () {
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxCierraSesionEmpresa(controller);
    });

    // Modal Inicio usuario empresa
    $(document).on('click', ".signInUsuarioEmpresa", function () {
        $("#username").val('');
        $("#password").val('');
        $("#loginError").html('');

        $("#modalSignInUsuarioEmpresa").modal("show");
    });

    // Inicio sesion usuario empresa
    $(document).on('click', "#inicioSesionUsuarioEmpresa", function () {
        var controller = '';
        var rut = $("#usernameUE").val().replace(/\./g, '');
        var password = $("#passwordUE").val();

        if (rut == "" || rut == null || rut == undefined || password == "" || password == null || password == undefined) {
            $("#errorInicioSesionEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar las credenciales.</div>');
            setInterval(function () { $("#errorInicioSesionEmpresa").html(''); }, 3000);
            return false;
        }
        ajaxViewPartialLoadingUsuarioEmpresa(rut, password);
        //controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/UsuarioEmpresa/";
        //ajaxInicioSesionUsuarioEmpresa(controller, rut, password);
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

    $(document).on('click', "#PlanesEmpresaError", function () {
        $("#modalPlanesEmpresa").modal("show");
    });

    $(document).on('click', "#PlanAfuera", function () {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Debe loguearse para contratar un plan',
            showConfirmButton: false,
            timer: 1500
        })
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

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Publicación actualizada con éxito',
                    showConfirmButton: false,
                    timer: 1500
                })
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
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Publicación actualizada con éxito',
                            showConfirmButton: false,
                            timer: 1500
                        })
                        window.location.reload();
                    }
                });
            }
        })
    });

    // Modificar publicacion
    $(document).on('click', ".btnAgregarPregunta", function () {
        var controller = '';
        var nombrePregunta = $("#nombrePregunta").val();
        var tipoPregunta = $("#tipoPregunta").val();
        if (nombrePregunta.trim() == "") {
            $(".errorPreguntaEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar una pregunta.</div>');
            setInterval(function () { $(".errorPreguntaEmpresa").html(''); }, 3000);
            return false;
        }
        controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxRegistroPreguntasEmpresa(controller, nombrePregunta, tipoPregunta);
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
        let timerInterval
        Swal.fire({
            title: 'Cargando...',
            html: '<div class="spinner-border text-primary ml-auto" role="status" aria-hidden="true" style="width: 3rem; height: 3rem;" id="spinner"></div>',
            timer: 2000,
            timerProgressBar: true,
            showConfirmButton: false,
            willClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {
                //console.log('I was closed by the timer')
            }
        })
        $("#buscarFiltros").trigger("click");

    });


    // Modal notificaciones
    $(document).on('click', ".notificacionEmpresa", function () {

        $("#modalNotificacionesEmpresa").modal("show");
    });


    // Perfil de la empresa
    $(document).on('click', "#btnGuardarPerfilEmpresa", function () {

        var rut = $("#rut").val();
        var telefono = $("#telefono").val();
        var correo = $("#correo").val();
        if (telefono.trim() == "" || correo.trim() == "") {
            $(".errorGuardarPerfilEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar el telefono y el correo.</div>');
            setInterval(function () { $(".errorGuardarPerfilEmpresa").html(''); }, 3000);
            return false;
        }

        if (correo != "" && correo != null && correo != undefined) {
            if (ValidateEmail(correo)) {
                $(".errorGuardarPerfilEmpresa").html('');
            }
            else {
                $(".errorGuardarPerfilEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Correo ingresado no es valido.</div>');
                setInterval(function () { $(".errorGuardarPerfilEmpresa").html(''); }, 3000);
                return false;
            }
        }
        else {
            $(".errorGuardarPerfilEmpresa").html('');
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxGuardaPerfilEmpresa(controller, rut, telefono, correo);
    });

    // Responder mensaje
    $(document).on('click', "#MensajeGu", function () {
        var idMensaje = $("#idMensaje").val();
        var idAutor = $("#idReceptor").val();
        var mensaje = $("#MensajeHM").val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxResponderMensajeAUsuario(controller, idMensaje, idAutor, mensaje);
    });

    // Valoracion publicacion
    $(document).on('click', "#valorarPublicacion", function () {

        var valorvotacion = $('#hiddenvotoIndicado').val();
        var idPublicacion = $("#idPublicacionV").val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/App/";

        ajaxGuardarValoracionPublicacion(controller, valorvotacion, idPublicacion);
    });


    // Modal Pregunta Postulacion
    $(document).on('click', "#BtnNuevaT", function () {
        $("#modalConfiguracionTarjeta").modal("show");
    });

    $(document).on('click', '#btnActualizarTarjetas', function () {
        var nombreTar = $('#nombreT').val();
        var numeroTar = $("#numeroT").val();
        var fechaTar = $("#fechaT").val();
        var montoTar = $("#montoD").val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxActualizarTarjetaEmpresa(controller, nombreTar, numeroTar, fechaTar, montoTar);
    });


    // Pago plan
    $(document).on('click', '#btnPagar', function () {
        var idplan = $('#idPlan').val();
        var idPlanAnterior = $('#idPlanAnterior').val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxPagoPlanEmpresa(controller, idplan, idPlanAnterior);
    });

    // Perfil del usuario empresa
    $(document).on('click', "#btnGuardarPerfilUsuarioEmpresa", function () {

        var telefono = $("#telefono").val();
        var correo = $("#correo").val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/UsuarioEmpresa/";

        ajaxGuardaPerfilUsuarioEmpresa(controller, telefono, correo);
    });


    // Asignacion de preguntas para publicaciones
    $(document).on('click', ".GuardarAsignacionPP", function () {

        var publicacion = $("#publicacion").val();
        var pregunta = $("#pregunta").val();
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";

        ajaxGuardaAsignacionPreguntaPublicacion(controller, publicacion, pregunta);
    });

    // popover informativo planes
    $('[data-toggle="popover"]').popover();

});
function CambioValor(valor) {
    var valoracion = valor;
    $("#hiddenvotoIndicado").val(valoracion);
}

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


// Filtros App

// Escritura de combos
$(document).on('keyup', "#nombrePublicacion", function () {
    var nombre = $("#nombrePublicacion").val();

    $("#hiddenNombreOferta").val(nombre);

});

$(document).on('change', "#comuna", function () {
    var fecha = $("#comuna").val();

    $("#hiddenComuna").val(fecha);

});

$(document).on('click', "#buscarFiltrosApp", function () {
    let timerInterval
    Swal.fire({
        title: 'Cargando...',
        html: '<div class="spinner-border text-primary ml-auto" role="status" aria-hidden="true" style="width: 3rem; height: 3rem;" id="spinner"></div>',
        timer: 2000,
        timerProgressBar: true,
        showConfirmButton: false,
        willClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            //console.log('I was closed by the timer')
        }
    })
    $("#buscarFiltrosApp2").trigger("click");

});

// filtros pagina 2
$(document).on('click', "#buscarFiltrosAppPublicacion", function () {
    let timerInterval
    Swal.fire({
        title: 'Cargando!',
        html: '<div class="spinner-border text-primary ml-auto" role="status" aria-hidden="true" style="width: 3rem; height: 3rem;" id="spinner"></div>',
        timer: 2000,
        timerProgressBar: true,
        showConfirmButton: false,
        willClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            //console.log('I was closed by the timer')
        }
    })
    $("#buscarFiltrosPagEmpleos").trigger("click");

});

// Escritura de combos
$(document).on('keyup', "#nombrePublicacionPag2", function () {
    var nombre = $("#nombrePublicacionPag2").val();

    $("#hiddenNombre2").val(nombre);

});

$(document).on('change', "#sueldoBusqueda", function () {
    var sueldo = $("#sueldoBusqueda").val();

    $("#hiddenSueldo").val(sueldo);

});

$(document).on('change', "#comuna2", function () {
    var sueldo = $("#comuna2").val();

    $("#hiddenComuna2").val(sueldo);

});

$(document).on('click', ".guardarEmpresa", function () {
    controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Empresa/";
    var rut = $("#rut").val();
    var rubro = $("#rubro").val();
    var nombre = $("#nombre").val();
    var razon = $("#razonsocial").val();
    var email = $("#correoReg").val();
    var telefono = $("#telefono").val();
    var comuna = $("#comuna").val();
    var direccion = $("#direccion").val();
    var clave = $("#clave").val();
    var repetirclave = $("#repetirclave").val();

    if (rut == "" ||
        rubro == "" ||
        nombre == "" ||
        razon == "" ||
        email == "" ||
        telefono == "" ||
        comuna == "" ||
        direccion == "" ||
        clave == "" ||
        repetirclave == "") {
        $.ajax({
            type: 'POST',
            url: controller + 'ViewPartialErrorRegistro',
            data: '{ }',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            error: function (xhr) {
                $(".errorRegistroEmpresa").html(xhr.responseText);
                setInterval(function () { $(".errorRegistroEmpresa").html(''); }, 3000);
                return false;
            }
        });
    }
    else {
        var error = '<div class="alert alert-danger text-center"><strong> Error!</strong> Correo ingresado no es valido.</div>';
        if (email != "" && email != null && email != undefined) {
            if (ValidateEmail(email)) {
                $(".errorRegistroEmpresa").html('');
            }
            else {
                $(".errorRegistroEmpresa").html(error);
                return false;
            }
        }
        else {
            $(".errorRegistroEmpresa").html('');
        }

        if (clave != repetirclave) {
            $.ajax({
                type: 'POST',
                url: controller + 'ViewPartialErrorRegistroClaves',
                data: '{ }',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                error: function (xhr) {
                    $(".errorRegistroEmpresa").html(xhr.responseText)
                    setInterval(function () { $(".errorRegistroEmpresa").html(''); }, 3000);
                    return false;
                }
            });

        }
        else {
            $('.guardarEmpresa').get(0).type = 'submit';
            $(".guardarEmpresa").trigger('click');
        }
    }

});

// validacion publicacion empresa

$(document).on('click', '.guardarPublicacion', function () {

    var titulo = $("#titulo").val();
    var descripcion = $("#descripcionNuevaPub").val();
    var area = $("#area").val();
    var subarea = $("#subarea").val();
    var tipo = $("#tipo").val();
    var monto = $("#monto").val();

    if (titulo == "" ||
        descripcion == "" ||
        area == "" ||
        subarea == "" ||
        tipo == "" ||
        monto == "") {
        $(".errorRegistroPublicacion").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Todos los campos son obligatorios.</div>');
        setInterval(function () { $(".errorRegistroPublicacion").html(''); }, 3000);
        //Swal.fire({
        //    position: 'center',
        //    icon: 'error',
        //    title: 'Todos los campos son obligatorios',
        //    showConfirmButton: false,
        //    timer: 1500
        //})
        return false;
    }
    else {


        $(".guardarPublicacion").css('display', 'none');
        $("#spinner").css('display', 'block');
        $("#textoSpinner").css('display', 'block');
        $('.guardarPublicacion').get(0).type = 'submit';
        setInterval(function () { $(".guardarPublicacion").trigger('click'); }, 3000);

    }



});

// validacion registro trabajadores
$(document).on('click', '.guardarUsuarioEmpresa', function () {

    var rut = $("#rut").val();
    var nombres = $("#nombres").val();
    var apellido = $("#apellido").val();
    var perfil = $("#perfil").val();
    var password = $("#password1").val();
    var passwordRepetir = $("#passwordRepetir").val();
    var correo = $("#correo").val();

    if (correo != "" && correo != null && correo != undefined) {
        if (ValidateEmail(mail)) {
            $(".errorRegistroUsuarioEmpresa").html('');
        }
        else {
            $(".errorRegistroUsuarioEmpresa").html('Correo ingresado no es valido');
        }
    }
    else {
        $(".errorRegistroUsuarioEmpresa").html('');
    }

    if (rut.trim() == "" ||
        nombres.trim() == "" ||
        apellido.trim() == "" ||
        perfil.trim() == "" ||
        password.trim() == "" ||
        passwordRepetir.trim() == "" ||
        correo.trim() == "") {
        $(".errorRegistroUsuarioEmpresa").html('<div class="alert alert-danger  text-center"><strong> Error!</strong> Todos los campos son obligatorios.</div>');
        setInterval(function () { $(".errorRegistroUsuarioEmpresa").html(''); }, 3000);
        //Swal.fire({
        //    position: 'center',
        //    icon: 'error',
        //    title: 'Todos los campos son obligatorios',
        //    showConfirmButton: false,
        //    timer: 1500
        //})
        return false;
    }
    else {
        if (password != passwordRepetir) {
            $(".errorRegistroUsuarioEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Las contraseñas no coinciden.</div>');
            setInterval(function () { $(".errorRegistroUsuarioEmpresa").html(''); }, 3000);
            return false;
        }
        else {
            $(".guardarPublicacion").css('display', 'none');
            $("#spinner").css('display', 'block');
            $("#textoSpinner").css('display', 'block');
            $('.guardarUsuarioEmpresa').get(0).type = 'submit';
            setInterval(function () { $(".guardarUsuarioEmpresa").trigger('click'); }, 3000);
        }

    }



});

// validacion imagen perfil empresa
$(document).on('click', "#uploadfileImgPerfilEmp", function () {
    $("#fileImgPerfilEmpresa").trigger("click");
});
function uploadImgPerfilEmpresa(file) {
    let filename = file.files[0].name;
    let extension = filename.slice(filename.lastIndexOf("."), filename.length);

    if (extension !== ".png" && extension !== ".jpg" && extension !== ".jpeg") {
        document.getElementById(file.id).value = "";
    }
    else {
        $("#spinnerImgPerfilEmpresa").css('display', 'block');
        $("#textoSpinnerImgPerfilEmpresa").css('display', 'block');
        $('#uploadFileImgPerfilEmpresa').get(0).type = 'submit';
        setInterval(function () { $("#uploadFileImgPerfilEmpresa").trigger('click'); }, 3000);
    }
}
//$(document).on('click', '#btnImagenPerfilEmpresa', function () {
//    var archivo = $(".archivoImagenPerfil").val();
//    if (archivo == "" || archivo == null || archivo == undefined) {
//        $(".ErrorImagenPerfil").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Debe seleccionar una imagen.</div>');
//        setInterval(function () { $(".ErrorImagenPerfil").html(''); }, 3000);
//        return false;
//    }
//    else {
        
//        $("#btnImagenPerfilEmpresa").css('display', 'none');
//        $("#spinnerImgPerfil").css('display', 'block');
//        $("#textoSpinnerImgPerfil").css('display', 'block');
//        $('#btnImagenPerfilEmpresa').get(0).type = 'submit';
//        setInterval(function () { $("#btnImagenPerfilEmpresa").trigger('click'); }, 6000);
//    }
//});


// validacion imagen banner empresa
$(document).on('click', "#uploadfileBanner", function () {
    $("#fileBanner").trigger("click");
});
function uploadImgBanner(file) {
    let filename = file.files[0].name;
    let extension = filename.slice(filename.lastIndexOf("."), filename.length);

    if (extension !== ".png" && extension !== ".jpg" && extension !== ".jpeg") {
        document.getElementById(file.id).value = "";
    }
    else {
        $("#apartadoBotonBanner").css('display', 'none');
        $("#spinnerBanner").css('display', 'block');
        $("#textoSpinnerBanner").css('display', 'block');
        $('#uploadFileBannerEmpresa').get(0).type = 'submit';
        setInterval(function () { $("#uploadFileBannerEmpresa").trigger('click'); }, 3000);
    }
}
//$(document).on('click', '#btnImagenBannerEmpresa', function () {
//    var archivo = $(".archivoBannerEmpresa").val();
//    if (archivo == "" || archivo == null || archivo == undefined) {
//        $(".ErrorImagenBannerEmpresa").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Debe seleccionar una imagen.</div>');
//        setInterval(function () { $(".ErrorImagenBannerEmpresa").html(''); }, 3000);
//        return false;
//    }
//    else {

//        $("#btnImagenBannerEmpresa").css('display', 'none');
//        $("#spinnerBanner").css('display', 'block');
//        $("#textoSpinnerBanner").css('display', 'block');
//        $('#btnImagenBannerEmpresa').get(0).type = 'submit';
//        setInterval(function () { $("#btnImagenBannerEmpresa").trigger('click'); }, 6000);
//    }
//});

// validacion configuracion tarjeta
$(document).on('click', '.guardarTarjetas', function () {
    var nombreT = $("#nombreT").val();
    var numeroT = $("#numeroT").val();
    var fechaT = $("#fechaT").val();
    var cvvT = $("#cvvT").val();
    var montoDisponible = $("#montoT").val();
    if (nombreT.trim() == "" || numeroT.trim() == "" || fechaT.trim() == "" || cvvT.trim() == "" || montoDisponible.trim() == "") {
        $(".errorConfiguracionTarjeta").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Todos los campos son obligatorios</div>');
        setInterval(function () { $(".errorConfiguracionTarjeta").html(''); }, 3000);
        return false;
    }
    else {
        $('.guardarTarjetas').get(0).type = 'submit';
        let timerInterval
        Swal.fire({
            title: 'Cargando...',
            html: '<div class="spinner-border text-primary ml-auto" role="status" aria-hidden="true" style="width: 3rem; height: 3rem;" id="spinner"></div>',
            timer: 2000,
            timerProgressBar: true,
            showConfirmButton: false,
            willClose: () => {
                clearInterval(timerInterval)
            }
        }).then((result) => {
            /* Read more about handling dismissals below */
            if (result.dismiss === Swal.DismissReason.timer) {
                //console.log('I was closed by the timer')
            }
        })
        $(".guardarTarjetas").trigger('click');
    }
});
