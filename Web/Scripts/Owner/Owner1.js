var seleccionado;
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


    $(document).on('keyup', "#username", function (e) {
        var code = e.keyCode ? e.keyCode : e.which;
        if (code === 13) {
            $("#signInUser").trigger("click");
        }
    });

    $(document).on('keyup', "#userPassword", function (e) {
        var code = e.keyCode ? e.keyCode : e.which;
        if (code === 13) {
            $("#signInUser").trigger("click");
        }
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
        let domain = getController('/Auth/');
        let error = validateErrorIngreso(rut, nombre1, apellidoP, correo, correoRepetir, password, passwordRepetir, fechaNacimiento);

        if (error == "" || error == null || error == undefined) {
            ajaxRegistroUsuario(domain, rut, nombre1, nombre2, apellidoP, apellidoM, correo, correoRepetir, password, passwordRepetir, fechaNacimiento);
        }
        else {
            if (error == null || error == undefined) {
                document.getElementById('errorRegistro').innerHTML = `<ul style="background-color: #FFDADA; width: 100%; padding: 15px; border-radius: 25px; list-style-type:none"><li>Ha ocurrido un error inesperado, favor intetarlo nuevamente!!</li></ul>`;
            }
            else {
                $("#errorRegistro").html('<div class="alert alert-danger  text-center"><strong> Error!</strong> Todos los campos son obligatorios.</div>');
                setInterval(function () { $("#errorRegistro").html(''); }, 3000);
            }
        }
    });

    $(document).on('click', "#terminos", function () {
        $(this).prop("checked") ? $("#registraUsuario").attr('disabled', false) : $("#registraUsuario").attr('disabled', true)
    });

    $(document).on('click', "#signInUser", function (e) {
        e.preventDefault();
        let domain = getController('/Auth/');
        let user = $("#username").val();
        let pass = $("#userPassword").val();


        ajaxViewPartialLoadingSignIn(domain, user, pass);
    });

    $(document).on('click', "#SignInUser", function () {
        $("#username").val('');
        $("#signInUser").val('');
        $("#loginError").html('');

        $("#modalSignIn").modal("show");
    });

    $(document).on('click', "#signOutUser", function () {
        let domain = getController('/Auth/');

        ajaxSignOut(domain);
    });

    $(document).on('click', "#guardaTagOficio", function () {
        let domain = getController('/Oficios/');
        let oficio = $("#tagOficio").val();

        if (oficio !== null && oficio !== '' && oficio !== undefined) {

            ajaxSetTagOficio(domain, oficio);
        }

    });

    $(document).on('click', "#setDescripcionOficio", function () {
        let descripcion = $("#oficioDescripcion").val();
        let domain = getController('/Oficios/');

        if (descripcion != null && descripcion != "" && descripcion != undefined) {
            ajaxSetDescripcionOficio(domain, descripcion);
        }
    });

    $(document).on('click', "#updDescripcionOficio", function () {
        let descripcion = $("#oficioDescripcion").val();
        let domain = getController('/Oficios/');

        if (descripcion != null && descripcion != "" && descripcion != undefined) {
            ajaxSetDescripcionOficio(domain, descripcion);
        }
    });

    $('#oficioDescripcion').on('input', function () {
        this.style.height = 'auto';
        this.style.height = (this.scrollHeight) + 'px';
    });


    $(document).on('change', "#ciudad", function () {
        let ciudad = $("#ciudad").val();
        let domain = getController('/Auth/');

        if (parseInt(ciudad) > 0) {
            ajaxGetComuna(domain, ciudad);
        }
        else {
            $("#comuna").html('<option value="0">Seleccione...</option>');
        }
    });

    $(document).on('change', "#correo", function () {
        let mail = $("#correo").val();
        if (mail != "" && mail != null && mail != undefined) {
            if (validateEmail(mail)) {
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
        let domain = getController('/Auth/');

        ajaxGetCiudad(domain, region);
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
                user = formatRut(user);
                $("#username").val(user);
            }
        }
    });


    //se ejecuta al cerrar un modal
    $("#modalRegistroUsuario").on('hide.bs.modal', function () {
        window.location.href = getController('/App/') + "Inicio";
    });

    $("#modalSignIn").on('hide.bs.modal', function () {
        $("#username").val('');
        $("#signInUser").val('');
        $("#errorSignIn").html('');
    });


    // Navbar agregado 26/12/2020
    const triggerTabList = [].slice.call(document.querySelectorAll('#myTab a'))
    triggerTabList.forEach((triggerEl) => {
        const tabTrigger = new mdb.Tab(triggerEl)

        triggerEl.addEventListener('click', (e) => {
            e.preventDefault()
            tabTrigger.show()
        })
    })

    // agregado 26/12/2020
    $(document).on('click', "#pills-area-tab", function () {
        $("#pills-postulaciones-tab").css("color", "#007bff");
        $("#pills-curriculum-tab").css("color", "#007bff");
        $("#pills-publicaciones-tab").css("color", "#007bff");
        $("#pills-oficios-tab").css("color", "#007bff");
        $("#pills-mensajes-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });
    $(document).on('click', "#pills-postulaciones-tab", function () {
        $("#pills-area-tab").css("color", "#007bff");
        $("#pills-curriculum-tab").css("color", "#007bff");
        $("#pills-publicaciones-tab").css("color", "#007bff");
        $("#pills-oficios-tab").css("color", "#007bff");
        $("#pills-mensajes-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });
    $(document).on('click', "#pills-curriculum-tab", function () {
        $("#pills-postulaciones-tab").css("color", "#007bff");
        $("#pills-area-tab").css("color", "#007bff");
        $("#pills-publicaciones-tab").css("color", "#007bff");
        $("#pills-oficios-tab").css("color", "#007bff");
        $("#pills-mensajes-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });
    $(document).on('click', "#pills-publicaciones-tab", function () {
        $("#pills-postulaciones-tab").css("color", "#007bff");
        $("#pills-curriculum-tab").css("color", "#007bff");
        $("#pills-area-tab").css("color", "#007bff");
        $("#pills-oficios-tab").css("color", "#007bff");
        $("#pills-mensajes-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });
    $(document).on('click', "#pills-oficios-tab", function () {
        $("#pills-postulaciones-tab").css("color", "#007bff");
        $("#pills-curriculum-tab").css("color", "#007bff");
        $("#pills-publicaciones-tab").css("color", "#007bff");
        $("#pills-area-tab").css("color", "#007bff");
        $("#pills-mensajes-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });
    $(document).on('click', "#pills-mensajes-tab", function () {
        $("#pills-postulaciones-tab").css("color", "#007bff");
        $("#pills-curriculum-tab").css("color", "#007bff");
        $("#pills-publicaciones-tab").css("color", "#007bff");
        $("#pills-oficios-tab").css("color", "#007bff");
        $("#pills-area-tab").css("color", "#007bff");
        $(this).css("color", "#ffffff");
    });

    $(document).on('click', "#uploadfile", function () {
        $("#fileCV").trigger("click");
    });
    // Funciones agregadas Giovanni Diaz

    // Modal mensaje a empresa
    $(document).on('click', '#btnEnviarMensaje', function () {
        var usuario = $("#UsuarioSesion").val();
        if (usuario != "") {
            $("#modalMensajeAEmpresa").modal("show");
        }
        else {
            // Levanta modal
            $("#modalSignIn").modal("show");
        }

    });

    $(document).on('click', '.btnGuardarMensaje', function () {
        var idEmpresa = $("#idEmpresa").val();
        var mensaje = $("#mensajeEmpresa").val();
        if (mensaje.trim() == "") {
            $(".divErrorMensajeEmpresa").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar un mensaje.</div>');
            setInterval(function () { $(".divErrorMensajeEmpresa").html(''); }, 3000);
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
        ajaxEnviarMensajeAEmpresa(controller, idEmpresa, mensaje);
    });


    // Responder mensaje
    $(document).on('click', "#MensajeGuardarReceptor", function () {
        var idMensaje = $("#idMensajeUS").val();
        var idAutor = $("#idReceptorUS").val();
        var mensaje = $("#MensajeHMUS").val();
        if (mensaje.trim() == "") {
            $(".ErrorMensajeReceptor").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar un comentario.</div>');
            setInterval(function () { $(".ErrorMensajeReceptor").html(''); }, 3000);
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
        ajaxResponderMensajeAReceptor(controller, idMensaje, idAutor, mensaje);
    });


    // guardar experiencias perfil usuario
    $(document).on('click', "#btnAgregarExperiencia", function () {

        $("#btnAgregarExperiencia").css('display', 'none');
        $("#spinnerExperiencia").css('display', 'block');
        $("#textoSpinnerExperiencia").css('display', 'block');

        var empresaNombre = $("#empresaNombre").val();
        var destacoEmpresa = $("#destacoEmpresa").val();
        var mejorarEmpresa = $("#mejorarEmpresa").val();
        var fechaD = $("#fechaDesde").val();
        var fechaH = $("#fechaHasta").val();
        var actualmente = $("#actualmente").val();
        var recomendacion = $("#recomendacion").val();
        var descripcion = $("#descripcionExperiencia").val();
        if (empresaNombre.trim() == "" || destacoEmpresa.trim() == "" || mejorarEmpresa.trim() == "" || fechaD == "" || fechaH == "" || recomendacion.trim() == "") {
            $(".errorExperienciaPerfil").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar todos los datos.</div>');
            setInterval(function () { $(".errorExperienciaPerfil").html(''); }, 3000);
            $("#btnAgregarExperiencia").css('display', 'block');
            $("#spinnerExperiencia").css('display', 'none');
            $("#textoSpinnerExperiencia").css('display', 'none');
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        setInterval(function () { ajaxRegistroExperienciaPerfilUsuario(controller, empresaNombre, destacoEmpresa, mejorarEmpresa, fechaD, fechaH, actualmente, descripcion, recomendacion); }, 3000);
        
    });

    // guardar educacion perfil usuario
    $(document).on('click', "#btnAgregarEducacion", function () {

        $("#btnAgregarEducacion").css('display', 'none');
        $("#spinnerEducacion").css('display', 'block');
        $("#textoSpinnerEducacion").css('display', 'block');


        var centroNombre = $("#centroNombre").val();
        var estadoEdu = $("#estadoEdu").val();
        var tituloNombreEdu = $("#tituloNombreEdu").val();
        var fechaD = $("#fechaDesdeEdu").val();
        var fechaH = $("#fechaHastaEdu").val();
        var descripcion = $("#descripcionEducacion").val();
        if (centroNombre == "" || centroNombre == null || estadoEdu == "" || estadoEdu == null || tituloNombreEdu.trim() == "" || fechaD == "" || fechaH == "" || descripcion.trim() == "") {
            $(".errorEducacionPerfil").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar todos los datos.</div>');
            setInterval(function () { $(".errorEducacionPerfil").html(''); }, 3000);
            $("#btnAgregarEducacion").css('display', 'block');
            $("#spinnerEducacion").css('display', 'none');
            $("#textoSpinnerEducacion").css('display', 'none');

            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        setInterval(function () { ajaxRegistroEducacionPerfilUsuario(controller, centroNombre, estadoEdu, tituloNombreEdu, fechaD, fechaH, descripcion); }, 3000);
    });

    $(document).on('change', '#estadoEdu', function () {
        var estado = $("#estadoEdu").val();
        if (estado == 0) {
            $("#tituloEducacionDiv").css("display", "block");
        }
        else {
            $("#tituloEducacionDiv").css("display", "none");
        }
    });


    // guardar idioma perfil usuario
    $(document).on('click', "#btnAgregarIdioma", function () {

        $("#btnAgregarIdioma").css('display', 'none');
        $("#spinnerIdioma").css('display', 'block');
        $("#textoSpinnerIdioma").css('display', 'block');

        var idiomaId = $("#idiomaId").val();
        var nivelIdioma = $("#nivelIdioma").val();
        if (idiomaId == "" || idiomaId == null || nivelIdioma == "" || nivelIdioma == null) {
            $(".errorIdiomaPerfil").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe seleccionar un nivel y una habilidad.</div>');
            setInterval(function () { $(".errorIdiomaPerfil").html(''); }, 3000);
            $("#btnAgregarIdioma").css('display', 'block');
            $("#spinnerIdioma").css('display', 'none');
            $("#textoSpinnerIdioma").css('display', 'none');
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        setInterval(function () { ajaxRegistroIdiomaPerfilUsuario(controller, idiomaId, nivelIdioma); }, 3000);
    });

    // guardar habilidad perfil usuario
    $(document).on('click', "#btnAgregarHabilidad", function () {

        $("#btnAgregarHabilidad").css('display', 'none');
        $("#spinnerHabilidad").css('display', 'block');
        $("#textoSpinnerHabilidad").css('display', 'block');

        var habilidadID = $("#habilidadID").val();
        var nivelHabilidad = $("#nivelHabilidad").val();
        if (habilidadID == null || habilidadID == "" || nivelHabilidad == "" || nivelIdioma == null) {
            $(".errorHabilidadesPerfil").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe seleccionar un nivel y un idioma.</div>');
            setInterval(function () { $(".errorHabilidadesPerfil").html(''); }, 3000);
            $("#btnAgregarHabilidad").css('display', 'block');
            $("#spinnerHabilidad").css('display', 'none');
            $("#textoSpinnerHabilidad").css('display', 'none');
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        setInterval(function () { ajaxRegistroHabilidadPerfilUsuario(controller, habilidadID, nivelHabilidad); },3000)

    });

    // guardar perfil usuario
    $(document).on('click', "#btnGuardarPerfilUsuario", function () {
        var nombre1 = $("#nombre1").val();
        var nombre2 = $("#nombre2").val();
        var apellido1 = $("#apellido1").val();
        var apellido2 = $("#apellido2").val();
        var telefono = $("#telefonoPerfil").val();
        var correo = $("#correoPerfil").val();
        var descripcion = $("#descripcionPersonal").val();

        if (nombre1.trim() == "" || nombre2.trim() == "" || apellido1.trim() == "" || apellido2.trim() == "" || telefono.trim() == "" || correo.trim() == "" || descripcion.trim() == "") {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Debe ingresar todos los campos de los datos personales',
                showConfirmButton: false,
                timer: 1500
            })
            return false;
        }

        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        ajaxRegistroPerfilUsuario(controller, nombre1, nombre2, apellido1, apellido2, telefono, correo, descripcion);

    });

    // perfil profesional usuario
    $(document).on('click', "#btnAgregarPerfilProfesional", function () {

        $("#btnAgregarPerfilProfesional").css('display', 'none');
        $("#spinnerPerfilProfesional").css('display', 'block');
        $("#textoSpinnerPerfilProfesional").css('display', 'block');

        var tituloperfil = $("#tituloPerfilProfesional").val();
        var descripcionperfil = $("#descripcionPerfilProfesional").val();

        if (tituloperfil.trim() == "" || descripcionperfil.trim() == "") {
            $(".errorPerfilProfesional").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar todos los datos.</div>');
            setInterval(function () { $(".errorPerfilProfesional").html(''); }, 3000);
            $("#btnAgregarPerfilProfesional").css('display', 'block');
            $("#spinnerPerfilProfesional").css('display', 'none');
            $("#textoSpinnerPerfilProfesional").css('display', 'none');
            return false;
        }
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";

        setInterval(function () { ajaxRegistroPerfilProfesionalUsuario(controller, tituloperfil, descripcionperfil); }, 3000);


    });

    // validacion imagen usuario perfil
    $(document).on('click', "#GuardarImagendePerfilUsuario", function () {
        $("#filePerfilUsuarioEmpresa").trigger("click");
    });

    //$(document).on('click', '#GuardarImagendePerfilUsuario', function () {
    //    var imagen = $(".imagenPerfilUsuarioEmpresa").val();
    //    if (imagen == "" || imagen == null || imagen == undefined) {
    //        $(".errorImagenPerfilUsuario").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Debe seleccionar una imagen.</div>');
    //        setInterval(function () { $(".errorImagenPerfilUsuario").html(''); }, 3000);
    //        return false;
    //    }
    //    else {
    //        $('#GuardarImagendePerfilUsuario').get(0).type = 'submit';
    //        $("#GuardarImagendePerfilUsuario").trigger('click');
    //    }
    //});

    // funcionamiento tabs

    $(document).on('click', "#pills-area-tab", function () {
        if (window.location.pathname != window.location.href + "#pills-area-tab") {
            var url = window.location.pathname + "#pills-area-tab";
            window.location.href = url;

        }

    });

    if (window.location.hash == "#pills-area-tab") { $("#pills-area-tab").trigger('click'); }

    $(document).on('click', "#pills-postulaciones-tab", function () {
        if (window.location.pathname != window.location.href + "#pills-postulaciones-tab") {
            var url = window.location.pathname + "#pills-postulaciones-tab";
            window.location.href = url;
        }

    });
    if (window.location.hash == "#pills-postulaciones-tab") { $("#pills-postulaciones-tab").trigger('click'); }

    $(document).on('click', "#pills-curriculum-tab", function () {
        if (window.location.pathname != window.location.href + "#pills-curriculum-tab") {
            var url = window.location.pathname + "#pills-curriculum-tab";
            window.location.href = url;
        }
    });
    if (window.location.hash == "#pills-curriculum-tab") { $("#pills-curriculum-tab").trigger('click'); }


    $(document).on('click', '#ComentarioGu', function () {
        var comentario = $("#ComentarioPubliC").val();
        if (comentario.trim() == "") {
            $(".errorRegistroComentariosEnPub").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe ingresar un comentario.</div>');
            setInterval(function () { $(".errorRegistroComentariosEnPub").html(''); }, 3000);
            //Swal.fire({
            //    position: 'center',
            //    icon: 'error',
            //    title: 'Debe ingresar un comentario',
            //    showConfirmButton: false,
            //    timer: 1500
            //})
            return false;
        }
        else {
            $('#ComentarioGu').get(0).type = 'submit';
            $("#ComentarioGu").trigger('click');
        }
    });

    $(document).on('click', '.btnEnviarPreguntasDesa', function () {

        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
        var disponibilidad = $("#Disponibilidad").val();
        var sueldo = $("#Sueldo").val();
        var descripcion = $("#descripcionEmpleo").val();
        var envioCV = $("#cv_adjuntar").val();
        var idPublicacion = $("#idPublicacionPreguntas").val();

        if (disponibilidad.trim() == "" || disponibilidad == null || disponibilidad == undefined
            || sueldo.trim() == "" || sueldo == null || sueldo == undefined
            || descripcion.trim() == "" || descripcion == null || descripcion == undefined) {
            $(".errorEnviarPreguntas").html('<div class="alert alert-danger text-center"><strong> Error!</strong> Debe rellenar todos los campos.</div>');
            setInterval(function () { $(".errorEnviarPreguntas").html(''); }, 3000);
            return false;
        }
        else {
            $.ajax({
                type: 'POST',
                url: controller + 'GuardarPostulacionEmpleo',
                data: '{ idPublicacion: "' + idPublicacion + '", disponibilidad: "' + disponibilidad + '", sueldo: "' + sueldo + '", descripcion: "' + descripcion + '", envioCV: "' + envioCV + '" }',
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



    });

    // Charts
    CargaCharts();

    // Funcionamiento de test de personalidad formulario

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".nextResponsabilidad").click(function () {
        var resultado = validoResponsabilidad();
        if (resultado == 1) {
            return false;
        }

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".nextAutogestion").click(function () {
        var resultado = validoAutogestion();
        if (resultado == 1) {
            return false;
        }

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    // Usuario test de personalidad
    $(document).on('click', '#btnGuardarRespuestasTest', function () {
        //debugger;
        var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
        var array = [];

        resultado = validoLiderazgo();
        if (resultado == 1) {
            return false;
        }

        var responsabilidad1 = $('input:radio[name = radioresponsabilidad1]:checked').val();
        var responsabilidad2 = $('input:radio[name = radioresponsabilidad2]:checked').val();
        var responsabilidad3 = $('input:radio[name = radioresponsabilidad3]:checked').val();
        var responsabilidad4 = $('input:radio[name = radioresponsabilidad4]:checked').val();
        var responsabilidad5 = $('input:radio[name = radioresponsabilidad5]:checked').val();
        var autogestion1 = $('input:radio[name = radiogestion1]:checked').val();
        var autogestion2 = $('input:radio[name = radiogestion2]:checked').val();
        var autogestion3 = $('input:radio[name = radiogestion3]:checked').val();
        var autogestion4 = $('input:radio[name = radiogestion4]:checked').val();
        var autogestion5 = $('input:radio[name = radiogestion5]:checked').val();
        var liderazgo1 = $('input:radio[name = radioliderazgo1]:checked').val();
        var liderazgo2 = $('input:radio[name = radioliderazgo2]:checked').val();
        var liderazgo3 = $('input:radio[name = radioliderazgo3]:checked').val();
        var liderazgo4 = $('input:radio[name = radioliderazgo4]:checked').val();
        var liderazgo5 = $('input:radio[name = radioliderazgo5]:checked').val();

        array.push(responsabilidad1);
        array.push(responsabilidad2);
        array.push(responsabilidad3);
        array.push(responsabilidad4);
        array.push(responsabilidad5);

        array.push(autogestion1);
        array.push(autogestion2);
        array.push(autogestion3);
        array.push(autogestion4);
        array.push(autogestion5);

        array.push(liderazgo1);
        array.push(liderazgo2);
        array.push(liderazgo3);
        array.push(liderazgo4);
        array.push(liderazgo5);

        // envio a guardar los datos del test
        $.ajax({
            type: 'POST',
            url: controller + 'GuardarRespuestasTest',
            data: '{respuestas: "' + array + '"}',
            dataType: 'json',
            contentType: 'application/json',
            async: true,
            success: function (response) {
                if (response.data == 1) {
                    window.location.href = "Perfil";
                }
            }
        });

    });

});

// Funcion validar radiobutton test
function validoResponsabilidad() {
    var responsabilidad1 = $('input:radio[name = radioresponsabilidad1]:checked').val();
    var responsabilidad2 = $('input:radio[name = radioresponsabilidad2]:checked').val();
    var responsabilidad3 = $('input:radio[name = radioresponsabilidad3]:checked').val();
    var responsabilidad4 = $('input:radio[name = radioresponsabilidad4]:checked').val();
    var responsabilidad5 = $('input:radio[name = radioresponsabilidad5]:checked').val();

    // validacion responsabilidad
    if (responsabilidad1 == "" || responsabilidad2 == "" || responsabilidad3 == "" || responsabilidad4 == "" || responsabilidad5 == "" ||
        responsabilidad1 == undefined || responsabilidad2 == undefined || responsabilidad3 == undefined || responsabilidad4 == undefined || responsabilidad5 == undefined) {
        $(".ErrorResponsabilidad").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Todos las opciones deben ser seleccionadas.</div>');
        setInterval(function () { $(".ErrorResponsabilidad").html(''); }, 3000);
        return 1;
    }
    
    return 0;

}

function validoAutogestion() {
    var autogestion1 = $('input:radio[name = radiogestion1]:checked').val();
    var autogestion2 = $('input:radio[name = radiogestion2]:checked').val();
    var autogestion3 = $('input:radio[name = radiogestion3]:checked').val();
    var autogestion4 = $('input:radio[name = radiogestion4]:checked').val();
    var autogestion5 = $('input:radio[name = radiogestion5]:checked').val();
    // validacion autogestion
    if (autogestion1 == "" || autogestion2 == "" || autogestion3 == "" || autogestion4 == "" || autogestion5 == "" ||
        autogestion1 == undefined || autogestion2 == undefined || autogestion3 == undefined || autogestion4 == undefined || autogestion5 == undefined) {
        $(".ErrorAutogestion").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Todos las opciones deben ser seleccionadas.</div>');
        setInterval(function () { $(".ErrorAutogestion").html(''); }, 3000);
        return 1;
    }
    return 0;
}

function validoLiderazgo() {

    var liderazgo1 = $('input:radio[name = radioliderazgo1]:checked').val();
    var liderazgo2 = $('input:radio[name = radioliderazgo2]:checked').val();
    var liderazgo3 = $('input:radio[name = radioliderazgo3]:checked').val();
    var liderazgo4 = $('input:radio[name = radioliderazgo4]:checked').val();
    var liderazgo5 = $('input:radio[name = radioliderazgo5]:checked').val();
    // validacion liderazgo
    if (liderazgo1 == "" || liderazgo2 == "" || liderazgo3 == "" || liderazgo4 == "" || liderazgo5 == "" ||
        liderazgo1 == undefined || liderazgo2 == undefined || liderazgo3 == undefined || liderazgo4 == undefined || liderazgo5 == undefined) {
        $(".ErrorLiderazgo").html('<div class="alert alert-danger text-center" style="margin-top:20px;"><strong> Error!</strong> Todos las opciones deben ser seleccionadas.</div>');
        setInterval(function () { $(".ErrorLiderazgo").html(''); }, 3000);
        return 1;
    }
    return 0;
}

// Guardar imagen de perfil usuario empresa
function uploadImgPerfilUsuarioEmpresa(file) {
    let filename = file.files[0].name;
    let extension = filename.slice(filename.lastIndexOf("."), filename.length);

    if (extension !== ".png" && extension !== ".jpg" && extension !== ".jpeg") {
        document.getElementById(file.id).value = "";
    }
    else {
        $("#GuardarImagendePerfilUsuario").css('display', 'none');
        $("#spinnerUsuarioEmpresa").css('display', 'block');
        $("#textoSpinnerUsuarioEmpresa").css('display', 'block');
        //$('#uploadFileUsuarioEmpresa').get(0).type = 'submit';
        setInterval(function () { $("#uploadFileUsuarioEmpresa").trigger('click'); }, 3000);
    }
}


//FUNCTIONS
function modalShowMensajeRegistro() {
    $("#modalMensajeRegistro").modal("show");
}

function formatRut(rut) {
    let rutPuntos = "";
    let actual = rut.replace(/^0+/, "");
    if (actual != '' && actual.length > 1) {
        let sinPuntos = actual.replace(/\./g, "");
        let actualLimpio = sinPuntos.replace(/-/g, "");
        let inicio = actualLimpio.substring(0, actualLimpio.length - 1);

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
        if (!validateEmail(correo)) {
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

function validateEmail(inputText) {
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

function delTagOficio(e) {
    let domain = getController('/Oficios/');
    let tag = e.dataset.tag;

    ajaxDelTagOficio(domain, tag);
}


function getController(prefix) {
    let prefixDomain = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2];

    return prefixDomain + prefix;
}

// CHARTS USUARIO TEST
function CargaCharts() {
    var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
    ajaxCargaCharts(controller);
}


//CURRICULUM
function uploadCV(file) {
    let domain = getController('/App/');
    let filename = file.files[0].name;
    let extension = filename.slice(filename.lastIndexOf("."), filename.length);

    if (extension !== ".pdf") {
        e.preventDefault();
        document.getElementById(file.id).value = "";
    }
    else {
        $("#uploadCVFile").trigger("click");
    }
}


function deleteCV(e) {
    let user = e.dataset.user;
    let domain = getController('/App/');

    ajaxDeleteCV(domain, user);
}




function handleRating(e) {
    let element = e.attributes.for.value;
    console.log($("#"+ element).val());
}