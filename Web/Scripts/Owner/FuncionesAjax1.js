
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


// Agregado giovanni diaz 26-12-2020
function ajaxEnviarMensajeAEmpresa(controller, empresa, mensaje) {
    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEnvioMensajeAEmpresa',
        data: '{ idEmpresa: "' + empresa + '", mensaje: "' + mensaje + '"}',
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

// Responder mensaje a usuario

function ajaxResponderMensajeAReceptor(controller, idMensaje, idAutor, mensaje) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarRespuestaMensajeAReceptor',
        data: '{ idMensaje: "' + idMensaje + '", idAutor: "' + idAutor + '", mensaje: "' + mensaje + '"}',
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



// guardar experiencia en perfil usuario

function ajaxRegistroExperienciaPerfilUsuario(controller, empresaNombre, destacoEmpresa, mejorarEmpresa, fechaD, fechaH, actualmente, descripcion, recomendacion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarExperienciaPerfilUsuario',
        data: '{ empresaNombre: "' + empresaNombre + '", recomendacion: "' + recomendacion + '", descripcion: "' + descripcion + '", destacoEmpresa: "' + destacoEmpresa + '", mejorarEmpresa: "' + mejorarEmpresa + '", fechaD: "' + fechaD + '", fechaH: "' + fechaH + '", actualmente: "' + actualmente + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Experiencia agregada con éxito.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else if (response.data == 2) {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'El nombre de empresa ya se encuentra registrado.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'Ha ocurrido un error en el sistema.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
        }
    });
}


function EliminarExperiencia(idExperiencia) {
    Swal.fire({
        title: 'Desea eliminar el registro de experiencia profesional?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {

            var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
            $.ajax({
                type: 'POST',
                url: controller + 'EliminarExperienciaPerfilUsuario',
                data: '{ idExperiencia: "' + idExperiencia + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.data == 1) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Experiencia eliminada con éxito.',
                            showConfirmButton: false,
                            timer: 2000
                        })
                        window.location.reload();
                    }
                }
            });
        }
    })

}


// guardar educacion usuario perfil 
function ajaxRegistroEducacionPerfilUsuario(controller, centroNombre, estadoEdu, tituloNombreEdu, fechaD, fechaH, descripcion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarEducacionPerfilUsuario',
        data: '{ centroNombre: "' + centroNombre + '", estadoEdu: "' + estadoEdu + '", tituloNombreEdu: "' + tituloNombreEdu + '", descripcion: "' + descripcion + '", fechaD: "' + fechaD + '", fechaH: "' + fechaH + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Educación agregada con éxito.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else if (response.data == 2) {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'El nombre ingresado ya se encuentra registrado.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'Ha ocurrido un error en el sistema.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }

        }
    });
}

function EliminarEducacion(idEducacion) {
    Swal.fire({
        title: 'Desea eliminar el registro de formación academica?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
            $.ajax({
                type: 'POST',
                url: controller + 'EliminarEducacionPerfilUsuario',
                data: '{ idEducacion: "' + idEducacion + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.data == 1) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Educación eliminada con éxito.',
                            showConfirmButton: false,
                            timer: 2000
                        })
                        window.location.reload();
                    }
                }
            });
        }
    })

}

// // guardar idioma usuario perfil 
function ajaxRegistroIdiomaPerfilUsuario(controller, idiomaId, nivelIdioma) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarIdiomaPerfilUsuario',
        data: '{ idIdioma: "' + idiomaId + '", nivel: "' + nivelIdioma + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Idioma agregado con éxito.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else if (response.data == 2) {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'El idioma seleccionado ya se encuentra registrado.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'Ha ocurrido un error en el sistema.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
        }
    });
}

function EliminarIdioma(idIdioma) {
    Swal.fire({
        title: 'Desea eliminar el registro de idioma?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
            $.ajax({
                type: 'POST',
                url: controller + 'EliminarIdiomaPerfilUsuario',
                data: '{ idIdioma: "' + idIdioma + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.data == 1) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Idioma eliminado con éxito.',
                            showConfirmButton: false,
                            timer: 2000
                        })
                        window.location.reload();
                    }
                }
            });
        }
    })

}


// // guardar habilidad usuario perfil 
function ajaxRegistroHabilidadPerfilUsuario(controller, habilidadID, nivelHabilidad) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarHabilidadPerfilUsuario',
        data: '{ idHabilidad: "' + habilidadID + '", nivel: "' + nivelHabilidad + '"}',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            if (response.data == 1) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Habilidad agregada con éxito.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else if (response.data == 2) {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'La habildiad seleccionada ya se encuentra registrado.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'info',
                    title: 'Ha ocurrido un error en el sistema.',
                    showConfirmButton: false,
                    timer: 2000
                })
                setInterval(function () { window.location.reload(); }, 500);
            }
        }
    });
}

function EliminarHabilidad(idHabilidad) {
    Swal.fire({
        title: 'Desea eliminar el registro de habilidades?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            var controller = window.location.href.split('/')[0] + "//" + window.location.href.split('/')[2] + "/Usuario/";
            $.ajax({
                type: 'POST',
                url: controller + 'EliminarHabilidadPerfilUsuario',
                data: '{ IdHabilidad: "' + idHabilidad + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    if (response.data == 1) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Habilidad eliminada con éxito.',
                            showConfirmButton: false,
                            timer: 2000
                        })
                        window.location.reload();
                    }
                }
            });
        }
    })

}

// // guardar datos usuario perfil 
function ajaxRegistroPerfilUsuario(controller, nombre1, nombre2, apellido1, apellido2, telefono, correo, descripcion) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarDatosPerfilUsuario',
        data: '{ nombre1: "' + nombre1 + '",nombre2: "' + nombre2 + '",apellido1: "' + apellido1 + '",apellido2: "' + apellido2 + '", telefonoPerfil: "' + telefono + '", correoPerfil: "' + correo + '", descripcionPersonal: "' + descripcion + '"}',
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

// // guardar datos usuario perfil 
function ajaxRegistroPerfilProfesionalUsuario(controller, tituloperfil, descripcionperfil) {

    $.ajax({
        type: 'POST',
        url: controller + 'GuardarDatosPerfilProfesionalUsuario',
        data: '{ tituloperfil: "' + tituloperfil + '", descripcionperfil: "' + descripcionperfil + '" }',
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


// postulacion a empleo
function PostularEmpleo(dato1, idPublicacion) {
    if (dato1 != 0) {
        $("#idPublicacionPreguntas").val(idPublicacion);
        $("#modalPreguntasPublicacion").modal("show");
    }
    else {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Debe estar logueado para postular',
            showConfirmButton: false,
            timer: 1500
        })
        return false;
    }
}

// Charts Usuario test
function ajaxCargaCharts(controller) {
    $.ajax({
        type: 'POST',
        url: controller + 'GetResultadosTestUsuario',
        dataType: 'json',
        contentType: 'application/json',
        async: true,
        success: function (response) {
            debugger;
            if (response.data[0] != "0") {
                var respon = response.data[0].Responsabilidad.replace(',', '.');
                var restorespon = response.data[0].RestoResponsabilidad.replace(',', '.');
                var autoges = response.data[0].AutoGestion.replace(',', '.');
                var restoautoges = response.data[0].RestoAutogestion.replace(',', '.');
                var lideraz = response.data[0].Liderazgo.replace(',', '.');
                var restolideraz = response.data[0].RestoLiderazgo.replace(',', '.');
                let ctx = document.getElementById("myChartResponsabilidad").getContext('2d');
                let ctx1 = document.getElementById("myChartAutogestion").getContext('2d');
                let ctx2 = document.getElementById("myChartLiderazgo").getContext('2d');
                let labelsrespo = ['Responsabilidad', 'No Responsabilidad'];
                let colorHexrespo = ['#FF702D', '#FFD2BD'];

                let labelsges = ['AutoGestion', 'No AutoGestion'];
                let colorHexges = ['#FF702D', '#FFD2BD'];

                let labelslider = ['Liderazgo', 'No Liderazgo'];
                let colorHexlider = ['#FF702D', '#FFD2BD'];


                // Responsabilidad
                let myChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        datasets: [{
                            data: [respon, restorespon],
                            backgroundColor: colorHexrespo
                        }],
                        labels: labelsrespo
                    },
                    options: {
                        responsive: true,
                        legend: {
                            position: 'bottom'
                        },
                        plugins: {
                            datalabels: {
                                color: '#fff',
                                anchor: 'end',
                                align: 'start',
                                offset: -10,
                                borderWidth: 2,
                                borderColor: '#fff',
                                borderRadius: 25,
                                backgroundColor: (context) => {
                                    return context.dataset.backgroundColor;
                                },
                                font: {
                                    weight: 'bold',
                                    size: '10'
                                },
                                formatter: (value) => {
                                    return value + ' %';
                                }
                            }
                        }
                    }
                });

                // Autogestion
                let myChart2 = new Chart(ctx1, {
                    type: 'doughnut',
                    data: {
                        datasets: [{
                            data: [autoges, restoautoges],
                            backgroundColor: colorHexges
                        }],
                        labels: labelsges
                    },
                    options: {
                        responsive: true,
                        legend: {
                            position: 'bottom'
                        },
                        plugins: {
                            datalabels: {
                                color: '#fff',
                                anchor: 'end',
                                align: 'start',
                                offset: -10,
                                borderWidth: 2,
                                borderColor: '#fff',
                                borderRadius: 25,
                                backgroundColor: (context) => {
                                    return context.dataset.backgroundColor;
                                },
                                font: {
                                    weight: 'bold',
                                    size: '10'
                                },
                                formatter: (value) => {
                                    return value + ' %';
                                }
                            }
                        }
                    }
                });

                // liderazgo
                let myChart3 = new Chart(ctx2, {
                    type: 'doughnut',
                    data: {
                        datasets: [{
                            data: [lideraz, restolideraz],
                            backgroundColor: colorHexlider
                        }],
                        labels: labelslider
                    },
                    options: {
                        responsive: true,
                        legend: {
                            position: 'bottom'
                        },
                        plugins: {
                            datalabels: {
                                color: '#fff',
                                anchor: 'end',
                                align: 'start',
                                offset: -10,
                                borderWidth: 2,
                                borderColor: '#fff',
                                borderRadius: 25,
                                backgroundColor: (context) => {
                                    return context.dataset.backgroundColor;
                                },
                                font: {
                                    weight: 'bold',
                                    size: '10'
                                },
                                formatter: (value) => {
                                    return value + ' %';
                                }
                            }
                        }
                    }
                });
            }

        }
    });
}

//CURRICULUM
function ajaxDeleteCV(controller, user) {
    Swal.fire({
        title: 'Desea eliminar el curriculum vitae?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: controller + 'DeleteCV',
                data: '{ user: "' + user + '"}',
                dataType: 'json',
                contentType: 'application/json',
                async: true,
                success: function (response) {
                    window.location.reload();
                }
            });
        }
    })

}