<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ConsultaLogPromo.aspx.cs" Inherits="Demo.ConsultaLogPromo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="content">     
    <div class="panel-group" id="accordion">   
    <div class="panel panel-default">
    <div class="panel-primary">
    <div class="panel-heading">
        <h4 class="panel-title">
        <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">
        Criterios de busqueda</a>
        </h4>
    </div>
    <div id="collapse1" class="panel-collapse collapse in">
        <div class="panel-body">            
            <div class="form">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <div class="col-md-5">
                            <label class="control-label">Remitente:</label>
                        </div>
                        <div class="col-md-7">
                                <select id="ddlRemitente" title="Remitente" class="form-control" data-placement="left">
                                <option value="">--SELECCIONE--</option>
                                </select>
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-4">
                        <div class="col-md-5">
                            <label class="control-label">Fecha Inicio:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="date" id="txtFechaInicial" Class="form-control" data-date-format="dd/mm/yyyy" placeholder="Fecha Inicio" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="col-md-5">
                            <label class="control-label">Fecha Fin:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="date" id="txtFechaFinal" Class="form-control" data-date-format="dd/mm/yyyy" placeholder="Fecha Fin" />
                        </div>
                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-1">
                    <a  Class="btn btn-primary btn-sm" id="btnBuscar" Style="background-color: orangered" href="javascript:void(0)"  >
                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span> Buscar
                    </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </div>

<div class="panel panel-default">
<div class="panel-primary">
<div class="panel-heading">
    <h4 class="panel-title">
    <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">
    Resultados</a>
    </h4>
</div>
<div id="collapse2" class="panel-collapse in">
    <div class="panel-body">
        <table class="table table-striped table-bordered table-hover">
            <thead>
            <tr class="header">
                <th>Fecha</th>
                <th>Usuario</th>
                <th>Plantilla</th>
                <th>Asunto</th>
                <th>Grupo Correo</th>
                <th>Nro Destinatarios</th>
            </tr>
            </thead>
            <tbody id="tbodyLogPromo">
                    <tr>
                    <td colspan="6">No se encontraron registro.</td>
                    </tr>
            </tbody>
        </table>
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
</div>
<input type="hidden" name="name" id="flag" value="true" />
<script type="text/javascript">
    var $pagination = $('#pagination');
    var defaultOpts = {
        totalPages: 10
    };

    var RegistroXpagina = 10;
    var firstPageClick = true;

    function listarLogPromo(pagina) {
        debugger;
        var currentPage = 0;
        if (firstPageClick) {
            currentPage = $pagination.twbsPagination('getCurrentPage');
        } else {
            currentPage = pagina;
        }
        $.ajax({
            type: "POST",
            url: "ConsultaLogPromo.aspx/listar_reporte",
            data: "{'remitente':'" + $('#ddlRemitente').val() + "','FechaInicial':'" + $('#txtFechaInicial').val() + "','FechaFinal':'" + $('#txtFechaFinal').val() + "','paginaActual':'" + currentPage + "','RegistroXpagina':'" + RegistroXpagina + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var vals = new Object();

                var result = JSON.parse(response.d.DataJson);
                var paginacion = JSON.parse(response.d.paginacion);
                var HTML = "";
                document.getElementById("tbodyLogPromo").innerHTML = "";
                if (result.length > 0) {
                    for (index in result) {
                        HTML += "<tr>";
                        HTML += "<td>" + result[index].LogPromo["fecha"] + "</td>";
                        HTML += "<td>" + result[index].LogPromo.Users["Nombre"] + "</td>";
                        HTML += "<td>" + result[index].LogPromo.Plantilla["descripcion"] + "</td>";
                        HTML += "<td>" + result[index].LogPromo["asunto"] + "</td>";
                        HTML += "<td>" + result[index].GrupoCorreo["descripcion"] + "</td>";
                        HTML += "<td>" + result[index]["Cantidad"] + "</td>";
                        HTML += "</tr>";
                    }
                    document.getElementById("tbodyLogPromo").innerHTML = HTML;
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
                                listarLogPromo(page);
                            }
                            $("#flag").val("true");
                        }
                    }));

                } else {
                    HTML += "<tr>";
                    HTML += "<td colspan='6'>No se encontraron registro.</td>";
                    HTML += "</tr>";
                    document.getElementById("tbodyLogPromo").innerHTML = HTML;
                    $pagination.twbsPagination('destroy');
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }

    function loadRemitente() {
        $.ajax({
            type: "POST",
            url: "ConsultaLogPromo.aspx/listar_remitente",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = JSON.parse(response.d.DataJson);
                $('#ddlRemitente').empty();
                $('#ddlRemitente').append("<option value=''>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].LogPromo.remitente;
                    var text = models[i].LogPromo.remitente;
                    $("#ddlRemitente").append($("<option></option>").val(valor).html(text));
                }
                $('#ddlRemitente option[value="1"]').attr("selected", "selected");
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }

    $(document).ready(function () {
        loadRemitente();

        $("#btnBuscar").click(function (e) {
            $("#flag").val("true");
            e.preventDefault();
            listarLogPromo(1);
        });

    });

</script> 
</asp:Content>
