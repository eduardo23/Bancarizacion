<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="MantGrupoCorreo.aspx.cs" Inherits="Demo.MantGrupoCorre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Consulta Grupo de Correo</b> : Criterios de busqueda </a>
                </div>
                <div class="panel-body">
                    <div class="form-group row">
                        <div class="col-sm-12 col-lg-12">
                            <div class="pull-left"></div>
                            <div class="pull-right">
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
                                        <th>Estado</th>
                                        <th>Origen</th>
                                        <th>Acción</th>
                                    </tr>
                                </thead>
                                <tbody id="tbodygrupocorreo">
                                    <!--<tr>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Eliminar</a></td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Anular</a></td>
                                    </tr>
                                    <tr>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Eliminar</a></td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Anular</a></td>
                                    </tr>
                                    <tr>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td>XXXXXXX</td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Eliminar</a></td>
                                        <td><a href="javascript:void();" class="btn-link-tabla">Anular</a></td>
                                    </tr>-->
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
    <div class="modal fade" tabindex="-1" role="dialog" id="myModal" aria-labelledby="gridSystemModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="gridSystemModalLabel">Nuevo Grupo</h4>
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
                                <input name="txt_grupo" type="text" value="" id="txt_grupo" class="form-control" placeholder="Ingrese Grupo" data-toggle="tooltip" data-placement="left" data-original-title="Grupo">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Estado</label>
                            <div class="col-lg-8">
                                <select name="cbo_estado" id="cbo_estado" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Estado">
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Departamento" class="col-lg-4 control-label">Origen</label>
                            <div class="col-lg-8">
                                <select name="cbo_origen" id="cbo_origen" class="form-control" data-toggle="tooltip" data-placement="left" data-original-title="Origen">
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
                        <!-- <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>-->
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
    <!-- /.modal -->
    <script type="text/javascript">
        Alert = {
            show: function ($div, msg) {
                $div.find('.alert-msg').text(msg);
                if ($div.css('display') === 'none') {
                    // fadein, fadeout.
                    //$div.fadeIn(1000).delay(2000).fadeOut(2000);
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

        function listarCheques(pagina) {
            var currentPage = 0;
            if (firstPageClick) {
                currentPage = $pagination.twbsPagination('getCurrentPage');
            } else {
                currentPage = pagina;
            }
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/getListGrupoCorreo",
                data: "{'paginaActual':'" + currentPage + "','RegistroXpagina':'" + RegistroXpagina + "'}",
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
                                estado: result[index]["estado"],
                                origen: result[index]["origen"],
                                descripcion: result[index]["descripcion"]
                            };
                            var Vals = JSON.stringify(vals);
                            //modalEliminar = "modalEliminar(" + parseInt(result[index]["ID"]) + ")";
                            modalEliminar = "modalEliminar(" + Vals + ")";
                            modalActualizar = "modalActualizar(" + Vals + ")";
                            HTML += "<tr>";
                            HTML += "<td>" + result[index]["descripcion"] + "</td>";
                            HTML += "<td>" + result[index]["descestado"] + "</td>";
                            HTML += "<td>" + result[index]["descorigen"] + "</td>";
                            HTML += "<td>";
                            HTML += "<a href='#' onclick='" + modalActualizar + "' title='Elimnar registro' ><span class='fa fa-trash'></span> Editar</a> ";
                            HTML += "<a href='#' onclick='" + modalEliminar + "' title='Editar registro' ><span class='fa fa-edit'></span> Eliminar</a>";
                            HTML += "</td>";
                            HTML += "</tr>";
                        }
                        debugger;
                        document.getElementById("tbodygrupocorreo").innerHTML = HTML;
                        /*if ($pagination.data("twbs-pagination"))
                            $pagination.twbsPagination('destroy');*/
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
                        HTML += "<td colspan='4'>No se encontraron registro.</td>";
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
        $(document).ready(function (e) {
            loadEstado();
            loadOrigen();
            $("#flag").val("true");
            //e.preventDefault();
            listarCheques(1);
        });

        function loadEstado() {
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/getListParametrosMaestro",
                data: '{skey:"ESTADO_GRUPOCORREO"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var models = JSON.parse(response.d.DataJson);//(typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
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
        function loadOrigen() {
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/getListParametrosMaestro",
                data: '{skey:"ORIGEN_GRUPOCORREO"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var models = JSON.parse(response.d.DataJson);// (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                    $('#cbo_origen').empty();
                    $('#cbo_origen').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < models.length; i++) {
                        var valor = models[i].valor;
                        var text = models[i].descripcion;
                        $("#cbo_origen").append($("<option></option>").val(valor).html(text));
                    }
                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });
        }


        function Grabar() {
            var grupo = $("#txt_grupo").val();
            var estado = $("#cbo_estado").val();
            var origen = $("#cbo_origen").val();

            var mensaje = "";
            if (grupo == "") {
                mensaje = mensaje + "- Ingrese grupo \n";
                Alert.danger(mensaje);
                return false;
            }
            if (estado == "0") {
                mensaje = mensaje + "- Seleccione Estado \n";
                Alert.danger(mensaje);
                return false;
            }
            if (origen == "0") {
                mensaje = mensaje + "- Seleccione Origen \n";
                Alert.danger(mensaje);
                return false;
            }
            var data = {
                descripcion: $("#txt_grupo").val(),
                estado: $("#cbo_estado").val(),
                origen: $("#cbo_origen").val(),
                codigo: $("#txt_codigo").val()
            };
            var estado = $("#flag_accion").val();

            if (estado == "UPD") {
                url = "MantGrupoCorreo.aspx/ActualiarGrupoCorreo";

            } else if (estado == "INS") {
                url = "MantGrupoCorreo.aspx/InsertGrupoCorreo";
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
                            $("#txt_grupo").val("");
                            $("#cbo_estado").val("0");
                            $("#cbo_origen").val("0");
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
            //listarCheques(1);
        }

        function modalEliminar(data) {
            var data = {
                id: data.id,
                estado: 0
            };
            $.ajax({
                type: "POST",
                url: "MantGrupoCorreo.aspx/EliminarGrupoCorreo",
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

        }

        function modalActualizar(data) {
            $("#gridSystemModalLabel").text("Actualizar Grupo");
            $("#divcodigo").css("display", "block");
            $("#txt_grupo").val(data.descripcion);
            $("#cbo_estado").val(data.estado);
            $("#cbo_origen").val(data.origen);
            $("#txt_codigo").val(data.id);
            $("#flag_accion").val("UPD");
            $('#myModal').modal('show');
        }
        function modalRegistrar() {
            $("#gridSystemModalLabel").text("Nuevo Grupo");
            $("#txt_grupo").val("");
            $("#cbo_estado").val("0");
            $("#cbo_origen").val("0");
            $("#flag_accion").val("INS");
            $("#divcodigo").css("display", "none");
            $('#myModal').modal('show');
        }


    </script>
</asp:Content>
