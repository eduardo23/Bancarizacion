<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GestionarCorreos.aspx.cs" Inherits="Demo.GestionarCorreos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/open-iconic/font/css/open-iconic-bootstrap.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/FileStyle/bootstrap-filestyle.js"></script>
    <style>
        .btnHermes {
            background-color: orangered;
            border: none;
            color: white;
            padding: 10px 22px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
        }

        .btn-link-tabla {
            width: 100%;
            height: auto;
            background-color: orangered;
            border: none;
            color: white;
            padding: 3px 1px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 12px;
            font-family: Arial, Helvetica, sans-serif;
            /* box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19); */
            -webkit-transition-duration: 0.4s;
            transition-duration: 0.4s;
        }

        table.table-style-one {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #3A3A3A;
            border-collapse: collapse;
        }

            table.table-style-one th {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #ffffff;
                background-color: #3c454f;
                color: white;
                text-align: center;
            }

            table.table-style-one td {
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #3A3A3A;
                background-color: #ffffff;
            }

        .alert-top {
            top: 50px;
            width: 100%;
            display: none;
            text-align: center;
            margin-bottom: 0;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <div class="container-fluid">

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Gestionar Correo</b> : Criterios de busqueda </a>
                </div>
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <div class="pull-left">
                                <label for="RazonSocial" class="col-lg-4 control-label">Grupo</label>
                                <div class="col-lg-8">
                                    <select name="cbo_grupo_consultar" onchange="selectChange()" id="cbo_grupo_consultar" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Grupo">
                                    </select>
                                </div>
                            </div>
                            <div class="pull-right">
                                <button type="button" class="btnHermes" data-toggle="modal" onclick="modalCargaMasivodeCorrreo();">
                                    Carga Masivo de Corrreo
                                </button>
                                <button type="button" class="btnHermes" data-toggle="modal" onclick="modalRegistrar();">
                                    Nuevo
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <table class="table-style-one" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>Grupo</th>
                                        <th>Nombre 1</th>
                                        <th>Nombre 2</th>
                                        <th>Paterno</th>
                                        <th>Materno</th>
                                        <th>Email</th>
                                        <th>Estado</th>
                                        <th>Fecha Baja</th>
                                        <th>Acción</th>
                                    </tr>
                                </thead>
                                <tbody id="tbodygrupocorreo">
                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-12 text-center">
                            <nav aria-label="Page navigation">
                                <ul class="pagination" id="pagination"></ul>
                            </nav>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" id="myModalCargacorreoAutomatico" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Carga Correo Automatico</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <div id="alert-info-carga" class="alert alert-info alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                                <div id="alert-warn-carga" class="alert alert-warning alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                                <div id="alert-danger-carga" class="alert alert-danger alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <label>Seleccione Archivo</label>
                                <input type="file" id="input08" onchange="checkfile(this);">
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-12 col-lg-12">
                                <div class="progress" id="idprogress" style="display: none;">
                                    <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btnHermes" onclick="cargarArchivo();">Grabar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div class="modal fade" id="confirm-submit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    Mensaje de Confirmacion
                </div>
                <div class="modal-body">
                    Esta seguro que desea eliminar el registro?                
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <a id="btn-submit-confirmacion" class="btn btn-success success">Ok</a>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" tabindex="-1" role="dialog" id="myModal" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="gridSystemModalLabel">Nuevo Correo</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group row">
                            <div class="col-lg-12">
                                <div id="alert-info" class="alert alert-info alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                                <div id="alert-warn" class="alert alert-warning alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                                <div id="alert-danger" class="alert alert-danger alert-top" role="alert">
                                    <button type="button" class="close alert-close" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <span class="alert-msg"></span>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="display: none;" id="divcodigo">
                            <label for="RazonSocial" class="col-lg-4 control-label">Código</label>
                            <div class="col-lg-8">
                                <input name="txt_codigo" type="text" value="0" id="txt_codigo" readonly="readonly" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Código">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="RazonSocial" class="col-lg-4 control-label">Grupo</label>
                            <div class="col-lg-8">
                                <select name="cbo_grupo_crear" id="cbo_grupo_crear" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Grupo">
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Nombre 1</label>
                            <div class="col-lg-8">
                                <input name="txt_nombre1" type="text" value="" id="txt_nombre1" class="form-control" placeholder="Ingrese Nombre 1" data-toggle="tooltip" data-placement="left" data-original-title="Nombre 1">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Nombre 2</label>
                            <div class="col-lg-8">
                                <input name="txt_nombre2" type="text" value="" id="txt_nombre2" class="form-control" placeholder="Ingrese Nombre 2" data-toggle="tooltip" data-placement="left" data-original-title="Nombre 2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Paterno</label>
                            <div class="col-lg-8">
                                <input name="txt_paterno" type="text" value="" id="txt_paterno" class="form-control" placeholder="Ingrese apellido paterno" data-toggle="tooltip" data-placement="left" data-original-title="Ape. Paterno 2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Materno</label>
                            <div class="col-lg-8">
                                <input name="txt_materno" type="text" value="" id="txt_materno" class="form-control" placeholder="Ingrese apellido materno" data-toggle="tooltip" data-placement="left" data-original-title="Ape. Materno">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Email</label>
                            <div class="col-lg-8">
                                <input name="txt_email" type="text" value="" id="txt_email" class="form-control" placeholder="Ingrese email" data-toggle="tooltip" data-placement="left" data-original-title="Email">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="RazonSocial" class="col-lg-4 control-label">Estado</label>
                            <div class="col-lg-8">
                                <select name="cbo_estado" id="cbo_estado" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Estado">
                                </select>
                            </div>
                        </div>
                        <div class="form-inline">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-10">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btnHermes" onclick="Grabar();">Grabar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div id="myDialog" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Hermes Seguros</h4>
                </div>
                <div class="modal-body" id="getCode" style="font-size: 18px">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" name="name" id="flag" value="true" />
    <input type="hidden" name="name" id="flag_accion" value="" />
    <script type="text/javascript">
        $('#input08').filestyle({
            'placeholder': 'adjunte archivo',
            text: ' Examinar',
            btnClass: 'btn-success'
        });
        AlertCarga = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    $div.fadeIn(1000).delay(2000).fadeOut(3000);
                }
            },
            info: function (msg) {
                this.show($('#alert-info-carga'), msg);
            },
            warn: function (msg) {
                this.show($('#alert-warn-carga'), msg);
            },
            danger: function (msg) {
                this.show($('#alert-danger-carga'), msg);
            }
        }

        Alert = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    $div.fadeIn(1000).delay(2000).fadeOut(2000);
                }
            },
            info: function (msg) {
                this.show($('#alert-info'), msg);
            },
            warn: function (msg) {
                this.show($('#alert-warn'), msg);
            },
            danger: function (msg) {
                this.show($('#alert-danger'), msg);
            }
        }

        var $pagination = $('#pagination');
        var defaultOpts = {
            totalPages: 10
        };
        var RegistroXpagina = 10;
        var firstPageClick = true;

        $('#select_id').change(function () {
            alert($(this).val());
        });

        function selectChange() {
            $("#flag").val("true");
            listarCheques(1);
        }

        function checkfile(sender) {
            var validExts = new Array(".xlsx", ".xls");
            var fileExt = sender.value;
            fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
            if (validExts.indexOf(fileExt) < 0) {
                sender.value = "";
                AlertCarga.warn("El archivo seleccionado es inválido , los archivos válidos son de tipo " +
                    validExts.toString() + "");
                return false;
            }
            else return true;
        }

        function listarCheques(pagina) {
            var id_cbo_grupo_consultar = $("#cbo_grupo_consultar").val();
            if (id_cbo_grupo_consultar == null) {
                id_cbo_grupo_consultar = 0;
            }
            var currentPage = 0;
            if (firstPageClick) {
                currentPage = $pagination.twbsPagination('getCurrentPage');
            } else {
                currentPage = pagina;
            }
            $.ajax({
                type: "POST",
                url: "GestionarCorreos.aspx/getListGestionCorreo",
                data: "{'paginaActual':'" + currentPage + "','RegistroXpagina':'" + RegistroXpagina + "','id_cbo_grupo_consultar':'" + id_cbo_grupo_consultar + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var vals = new Object();
                    var result = JSON.parse(response.d.DataJson);
                    var paginacion = JSON.parse(response.d.paginacion);
                    var HTML = "";
                    var modalActualizar = "";
                    var modalEliminar = "";
                    document.getElementById("tbodygrupocorreo").innerHTML = "";
                    if (result.length > 0) {
                        for (index in result) {
                            vals = {
                                id: parseInt(result[index]["id"]),
                                Nombre1: result[index]["Nombre1"],
                                Nombre2: result[index]["Nombre2"],
                                ApePaterno: result[index]["ApePaterno"],
                                ApeMaterno: result[index]["ApeMaterno"],
                                Email: result[index]["Email"],
                                id_estado: result[index]["id_estado"],
                                id_grupo_correo: result[index].grupocorreo["id"]
                            };
                            var Vals = JSON.stringify(vals);
                            modalEliminar = "modalEliminar(" + Vals + ")";
                            modalActualizar = "modalActualizar(" + Vals + ")";
                            HTML += "<tr>";
                            HTML += "<td>" + result[index].grupocorreo["descripcion"] + "</td>";
                            HTML += "<td>" + result[index]["Nombre1"] + "</td>";
                            HTML += "<td>" + result[index]["Nombre2"] + "</td>";
                            HTML += "<td>" + result[index]["ApePaterno"] + "</td>";
                            HTML += "<td>" + result[index]["ApeMaterno"] + "</td>";
                            HTML += "<td>" + result[index]["Email"] + "</td>";
                            HTML += "<td>" + result[index]["descestado"] + "</td>";
                            HTML += "<td>" + result[index]["descorigen"] + "</td>";
                            HTML += "<td>";
                            HTML += "<a href='#' onclick='" + modalActualizar + "' title='Elimnar registro' ><span class='fa fa-trash'></span> Editar</a> ";
                            HTML += "<a href='#' onclick='" + modalEliminar + "' title='Editar registro' ><span class='fa fa-edit'></span> Eliminar</a>";
                            HTML += "</td>";
                            HTML += "</tr>";
                        }
                        document.getElementById("tbodygrupocorreo").innerHTML = HTML;
                        $pagination.twbsPagination('destroy');
                        $pagination.twbsPagination($.extend({}, defaultOpts, {
                            startPage: currentPage,
                            totalPages: paginacion.TotalPages,
                            first: "Primero",
                            prev: "Anterior",
                            next: "Siguiente",
                            last: "último",
                            onPageClick: function (event, page) {
                                var flag = $("#flag").val();
                                if (flag == "true") {
                                    $("#flag").val("false");
                                    return;
                                } else {
                                    listarCheques(page);
                                }
                                $("#flag").val("true");
                            }
                        }));

                    } else {
                        HTML += "<tr>";
                        HTML += "<td colspan='9'>No se encontraron registro.</td>";
                        HTML += "</tr>";
                        document.getElementById("tbodygrupocorreo").innerHTML = HTML;
                        $pagination.twbsPagination('destroy');
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function modalRegistrar() {
            $("#gridSystemModalLabel").text("Nuevo Correo");
            $("#txt_codigo").val("0");
            $("#txt_nombre1").val("");
            $("#txt_nombre2").val("");
            $("#txt_paterno").val("");
            $("#txt_materno").val("");
            $("#txt_email").val("");
            $("#cbo_grupo_crear").val("0");
            $("#cbo_estado").val("0");
            $("#divcodigo").css("display", "none");
            $("#flag_accion").val("INS");
            $('#myModal').modal('show');
        }

        function modalEliminar(data) {
            $("#confirm-submit").modal("show");
            $('#btn-submit-confirmacion').attr('onclick', 'btnEliminar(' + data.id + ')');
        }

        function btnEliminar(id) {
            var data = {
                id: id,
                id_estado: 0
            };
            $.ajax({
                type: "POST",
                url: "GestionarCorreos.aspx/EliminarGestionCorreo",
                contentType: "application/json",
                data: JSON.stringify({ objeto: data }),
                dataType: "json",
                success: function (response) {
                    $("#flag").val("true");
                    listarCheques(1);
                    $("#myDialog").modal("toggle");
                    $("#getCode").html(response.d.Mensaje);
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
            $("#confirm-submit").modal("hide");
        }

        function modalActualizar(data) {
            $("#gridSystemModalLabel").text("Actualizar Correo");
            $("#divcodigo").css("display", "block");
            $("#txt_codigo").val(data.id);
            $("#txt_nombre1").val(data.Nombre1);
            $("#txt_nombre2").val(data.Nombre2);
            $("#txt_paterno").val(data.ApePaterno);
            $("#txt_materno").val(data.ApeMaterno);
            $("#txt_email").val(data.Email);
            $("#cbo_grupo_crear").val(data.id_grupo_correo);
            $("#cbo_estado").val(data.id_estado);
            $("#flag_accion").val("UPD");
            $('#myModal').modal('show');
        }

        function Grabar() {
            var txt_codigo = $("#txt_codigo").val();
            var txt_nombre1 = $("#txt_nombre1").val();
            var txt_nombre2 = $("#txt_nombre2").val();
            var txt_paterno = $("#txt_paterno").val();
            var txt_materno = $("#txt_materno").val();
            var txt_email = $("#txt_email").val();
            var cbo_grupo_crear = $("#cbo_grupo_crear").val();
            var cbo_estado = $("#cbo_estado").val();

            var mensaje = "";
            if (cbo_grupo_crear == "0") {
                mensaje = mensaje + "- Seleccione  grupo \n";
                Alert.danger(mensaje);
                return false;
            }
            if (txt_nombre1 == "") {
                mensaje = mensaje + "- Ingrese Nombre 1 \n";
                Alert.danger(mensaje);
                return false;
            }
            if (txt_nombre2 == "") {
                mensaje = mensaje + "- Ingrese Nombre 1 \n";
                Alert.danger(mensaje);
                return false;
            }
            if (txt_paterno == "") {
                mensaje = mensaje + "- Ingrese Apellido Paterno \n";
                Alert.danger(mensaje);
                return false;
            }
            if (txt_materno == "") {
                mensaje = mensaje + "- Ingrese Apellido Materno \n";
                Alert.danger(mensaje);
                return false;
            }
            if (txt_email == "") {
                mensaje = mensaje + "- Ingrese email \n";
                Alert.danger(mensaje);
                return false;
            }
            if (cbo_estado == "0") {
                mensaje = mensaje + "- Seleccione estado \n";
                Alert.danger(mensaje);
                return false;
            }
            var data = {
                id: txt_codigo,
                Nombre1: txt_nombre1,
                Nombre2: txt_nombre2,
                ApePaterno: txt_paterno,
                ApeMaterno: txt_materno,
                Email: txt_email,
                id_estado: cbo_estado,
                grupocorreo: {
                    id: cbo_grupo_crear
                }
            };
            var estado = $("#flag_accion").val();
            if (estado == "UPD") {
                url = "GestionarCorreos.aspx/ActualiarGestionCorreo";
            } else if (estado == "INS") {
                url = "GestionarCorreos.aspx/InsertGestionCorreo";
            }
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json",
                data: JSON.stringify({ objeto: data }),
                dataType: "json",
                success: function (response) {
                    var result = response.d.Status;
                    var mensaje = response.d.Mensaje;
                    if (result == "OK") {
                        if (estado == "INS") {
                            $("#txt_codigo").val("0");
                            $("#txt_nombre1").val("");
                            $("#txt_nombre2").val("");
                            $("#txt_paterno").val("");
                            $("#txt_materno").val("");
                            $("#txt_email").val("");
                            $("#cbo_grupo_crear").val("0");
                            $("#cbo_estado").val("0");
                        }
                        Alert.info(mensaje);
                        $("#flag").val("true");
                        listarCheques(1);
                    } else {
                        Alert.info(mensaje);
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        Alert.info(mensaje);
                }
            });
        }

        function loadGrupoConsultar() {
            $.ajax({
                type: "POST",
                url: "GestionarCorreos.aspx/getGrupoCorreoCombo",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);
                    $('#cbo_grupo_consultar').empty();
                    $('#cbo_grupo_consultar').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].id;
                        var text = models[i].descripcion;
                        $("#cbo_grupo_consultar").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function loadGrupo() {
            $.ajax({
                type: "POST",
                url: "GestionarCorreos.aspx/getGrupoCorreoCombo",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);
                    $('#cbo_grupo_crear').empty();
                    $('#cbo_grupo_crear').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].id;
                        var text = models[i].descripcion;
                        $("#cbo_grupo_crear").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function loadEstado() {
            $.ajax({
                type: "POST",
                url: "GestionarCorreos.aspx/getListParametrosMaestro",
                data: '{skey:"ESTADO_GESTIONCORREO"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);
                    $('#cbo_estado').empty();
                    $('#cbo_estado').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].valor;
                        var text = models[i].descripcion;
                        $("#cbo_estado").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }

        function modalCargaMasivodeCorrreo() {
            $("#myModalCargacorreoAutomatico").modal("show");
        }

        function cargarArchivo() {
            var data = new FormData();

            var files = $("#input08").get(0).files;
            if (files.length > 0) {
                data.append("Filedata", files[0]);
            }
            $("#idprogress").css('display', 'block');
            $.ajax({
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    //Upload Progress
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = (evt.loaded / evt.total) * 100;
                            $('div.progress > div.progress-bar').css({ "width": percentComplete + "%" });
                        }
                    }, false);
                    //Download progress
                    xhr.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = (evt.loaded / evt.total) * 100;
                            $("div.progress > div.progress-bar").css({ "width": percentComplete + "%" });
                        }
                    },
                        false);
                    return xhr;
                },
                type: 'post',
                url: "UploadFile.ashx",
                contentType: false,
                processData: false,
                data: data,
                success: function (response) {
                    var objeto = JSON.parse(response);
                    if (objeto.Result == "Ok") {
                        AlertCarga.info(objeto.Mensaje);
                        $("div.progress > div.progress-bar").css({ "width": 0 + "%" });
                    } else {
                        AlertCarga.danger(objeto.Mensaje);
                    }
                    $("#idprogress").css('display', 'none');
                },
                error: function (error) {
                    AlertCarga.danger("Error Consulte con el Administrador de Sistema.");
                }
            });
        }

        $(document).ready(function (e) {
            loadGrupoConsultar();
            loadEstado();
            loadGrupo();

            $("#flag").val("true");
            listarCheques(1);
        });
    </script>
</asp:Content>
