<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ConsultarCheque.aspx.cs" Inherits="Demo.ConsultarCheque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/standar.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<script src="Scripts/moment.js"></script>
<script src="Scripts/accounting.js"></script>
  
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
                        <label class="control-label">Campaña:</label>
                    </div>
                    <div class="col-md-7">
                            <select id="ddlFCampana" title="Campaña" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-5">
                        <label class="control-label">Tipo de Seguro:</label>
                    </div>
                    <div class="col-md-7">

                            <select id="ddlFTipoSeguro" title="Tipo Seguro" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>

                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-5">
                        <label class="control-label">Institucion Educativa:</label>
                    </div>
                    <div class="col-md-7">

                            <select id="ddlFInstitucion" title="Institucion Educativa" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-4">
                    <div class="col-md-5">
                        <label class="control-label">Fecha Inicio:</label>
                    </div>
                    <div class="col-md-7">
                        <input type="date" id="txtFechaInicial" Class="form-control" data-date-format="dd/mm/yyyy" placeholder="Fecha Inicio" />
                        <%--<asp:TextBox ID="txtFechaInicial" CssClass="form-control" runat="server" placeholder="Fecha Inicio"></asp:TextBox>--%>

                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-5">
                        <label class="control-label">Fecha Fin:</label>
                    </div>
                    <div class="col-md-7">
                        <input type="date" id="txtFechaFinal" Class="form-control" data-date-format="dd/mm/yyyy" placeholder="Fecha Fin" />
                        <%--<asp:TextBox ID="txtFechaFinal" CssClass="form-control" runat="server" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Fecha Fin"></asp:TextBox>--%>

                    </div>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="col-md-12" style="margin-top:5px; text-align:right;">


                <a  Class="btn btn-primary btn-sm" id="btnBuscar" Style="background-color: orangered" href="javascript:void(0)"  >
                    <span aria-hidden="true" class="glyphicon glyphicon-search"></span> Buscar
                </a>                      

                <button id="btnNuevo" Class="btn btn-primary btn-sm" Style="background-color: orangered" onclick='javascript:LoadMntChq("NVO","");return false;'>
          
                    <span aria-hidden="true" class="glyphicon glyphicon-file"></span> Nuevo
                </button>
                       
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
                <th>Campaña</th>
                <th>Institucion Educativa</th>
                <th>Aseguradora</th>
                <th>Producto</th>
                <th>Banco</th>
                <th>Concepto</th>
                <th>Tipo Moneda</th>
                <th>Monto</th>
                <th>Nro Cheque</th>
                <th>Fecha</th>
                <th>Acciones</th>
            </tr>
            </thead>
            <tbody id="tbodycheque">
                    <tr>
                    <td colspan="11">No se encontraron registro.</td>
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

<div id="myModalCheque" class="modal fade" role="dialog">
<div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header" style="background-color: #D6EAF8">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Gestionar Cheque</h4>
        </div>
        <div class="modal-body">
            <div class="tab-content"">
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Campaña:</label>
                            <input type="text" class="oculto" id="txtID"/>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlCampana" title="Campaña" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Institucion Educativa:</label>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlInstitucion" title="Institucion Educativa" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Aseguradora:</label>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlCia" title="Aseguradora" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Producto:</label>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlProducto" title="Producto" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Banco:</label>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlBanco" title="Banco" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Moneda:</label>
                        </div>
                        <div class="col-md-7">
                            <select id="ddlMoneda" title="Moneda" data-toggle="tooltip" class="form-control" data-placement="left">
                            <option value="0">--SELECCIONE--</option>
                            </select>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Fecha:</label>
                        </div>
                        <div class="col-md-7">
                             <input type="date" class="form-control" id="txtFecha" title="Fecha" data-toggle="tooltip" data-placement="left" placeholder="Ingrese Fecha" required />
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Nro. Cheque:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="text" class="form-control" id="txtNroCheque" Title="Nro de Cheque" data-toggle="tooltip" data-placement="left" placeholder="Ingrese Nro de Cheque" required/>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Monto:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="number" step="0.01" class="form-control" id="txtMonto" Title="Monto" data-toggle="tooltip" data-placement="left" data-val-required="El Monto es Obligatorio" placeholder="0.00" required/>
                        </div>                    
                    </div>
                    </div>
            </div>
        </div>
        <div class="modal-footer">
            <div class="form-inline">
                <Button ID="btnGuardar" Class="btn btn-primary btn-sm" onclick='return false;' data-dismiss="modal">
                <span  class="glyphicon glyphicon-ok"></span> Guardar
                </Button>
                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-primary btn-sm" Style="background-color: orangered" data-dismiss="modal">
                <span aria-hidden="true" class="glyphicon glyphicon-remove"></span> Cancelar
                </asp:LinkButton>
            </div>
        </div>                
    </div>
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
            <button type="button"  class="button" data-dismiss="modal">Cerrar</button>
        </div>
    </div>
</div>
</div>

<div id="myModal1" class="modal fade" role="dialog">
<div class="modal-dialog">

<!-- Modal content-->
<div class="modal-content">
<div class="modal-header">
<button type="button" class="close" data-dismiss="modal">&times;</button>
<h4 class="modal-title">Modal Header</h4>
</div>
<div class="modal-body">
<p>Some text in the modal.</p>
</div>
<div class="modal-footer">
<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
</div>
</div>
    <input type="hidden" name="name" id="flag" value="true" />
</div>
</div>

<script type="text/javascript">
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
            url: "ConsultarCheque.aspx/listar_reporte",
            data: "{'CampaniaID':'" + $('#ddlFCampana').val() + "','ProductoID':'" + $('#ddlFTipoSeguro').val() + "','InstitucionEducativaID':'" + $('#ddlFInstitucion').val() + "','FechaInicial':'" + $('#txtFechaInicial').val() + "','FechaFinal':'" + $('#txtFechaFinal').val() + "','paginaActual':'" + currentPage + "','RegistroXpagina':'" + RegistroXpagina + "'}",
            //data: { 'paginaActual': currentPage, 'RegistroXpagina': RegistroXpagina },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var vals = new Object();

                var result = JSON.parse(response.d.DataJson);
                var paginacion = JSON.parse(response.d.paginacion);
                var HTML = "";
                var modalActualizar = "";
                var modalEliminar = "";
                document.getElementById("tbodycheque").innerHTML = "";
                if (result.length > 0) {
                    for (index in result) {

                        //var ID = parseInt(result[index]["ID"]);
                        //var vals = result.find(book => book.ID === ID);
                        vals = {
                            ID: parseInt(result[index]["ID"]),
                            CampaniaID: result[index]["CampaniaID"],
                            InstitucionEducativaID: result[index]["InstitucionEducativaID"],
                            CIASeguroID: result[index]["CIASeguroID"],
                            ProductoID: result[index]["ProductoID"],
                            BancoID: result[index]["BancoID"],
                            MonedaID: result[index]["MonedaID"],
                            Fecha: moment(result[index]["Fecha"]).format('DD/MM/YYYY'),
                            NroCheque: result[index]["NroCheque"],
                            Monto: result[index]["Monto"]
                        };
                        var Vals = JSON.stringify(vals);
                        modalEliminar = "modalEliminar(" + parseInt(result[index]["ID"]) + ")";
                        modalActualizar = "modalActualizar(" + Vals + ")";
                        HTML += "<tr>";
                        HTML += "<td>" + result[index].Campania["Nombre"] + "</td>";
                        HTML += "<td>" + result[index].InstitucionEducativa["Nombre"] + "</td>";
                        HTML += "<td>" + result[index].CIASeguro["Nombre"] + "</td>";
                        HTML += "<td>" + result[index].Producto["Nombre"] + "</td>";
                        HTML += "<td>" + result[index].Banco["Nombre"] + "</td>";
                        HTML += "<td>" + result[index]["Concepto"] + "</td>";
                        HTML += "<td>" + result[index].Moneda["Nombre"] + "</td>";
                        HTML += "<td>" + accounting.formatNumber(result[index]["Monto"],2) + "</td>";
                        HTML += "<td>" + result[index]["NroCheque"] + "</td>";
                        HTML += "<td>" + moment(result[index]["Fecha"]).format('DD/MM/YYYY') + "</td>";
                        HTML += "<td>";
                        HTML += "<a href='#' onclick='" + modalActualizar + "' title='Editar registro' ><span class='fa fa-trash'></span> Editar</a> ";
                        HTML += "<a href='#' onclick='" + modalEliminar + "' title='Eliminar registro' ><span class='fa fa-edit'></span> Eliminar</a>";
                        HTML += "</td>";
                        HTML += "</tr>";
                    }
                    document.getElementById("tbodycheque").innerHTML = HTML;
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
                    HTML += "<td colspan='11'>No se encontraron registro.</td>";
                    HTML += "</tr>";
                    document.getElementById("tbodycheque").innerHTML = HTML;
                    $pagination.twbsPagination('destroy');
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }

    function loadFCampana() {
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/GetFLstCampana",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlFCampana").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadFTipoSeguro() {
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/GetFLstProducto",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlFTipoSeguro").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadFInstitucion() {
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/GetFLstInstitucion",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlFInstitucion").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadCampana() {
        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstCampana",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $('#ddlCampana').empty();
                $('#ddlCampana').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlCampana").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadInstitucion() {

        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstInstByCampana",
            data: "{CampanaId: '" + $('#ddlCampana').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $('#ddlInstitucion').empty();
                $('#ddlInstitucion').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlInstitucion").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadCia() {
        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstCiabyInst",
            data: "{InstId: '" + $('#ddlInstitucion').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

                $('#ddlCia').empty();
                $('#ddlCia').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlCia").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadProductos() {

        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstProdbyInstCia",
            data: "{'InstId': '" + $('#ddlInstitucion').val() + "','CiaId': '" + $('#ddlCia').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

                $('#ddlProducto').empty();
                $('#ddlProducto').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlProducto").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadBanco() {
        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstBanco",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $('#ddlBanco').empty();
                $('#ddlBanco').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlBanco").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function loadMoneda() {
        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstMonbyInstAsegProd",
            data: "{'InstId': '" + $('#ddlInstitucion').val() + "','CiaId': '" + $('#ddlCia').val() + "','ProdId': '" + $('#ddlProducto').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $('#ddlMoneda').empty();
                $('#ddlMoneda').append("<option value='0'>--SELECCIONE--</option>");
                for (var i = 0; i < models.length; i++) {
                    var valor = models[i].ID;
                    var text = models[i].Nombre;
                    $("#ddlMoneda").append($("<option></option>").val(valor).html(text));
                }
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }
    function modalEliminar(Id) {
        $("#confirm-submit").modal("show");
        $('#btn-submit-confirmacion').attr('onclick', 'btnEliminar(' + Id + ')');
    }
    function modalActualizar(oObj) {
        LoadMntChq('UPD', oObj);
    }
    function btnGuardar() {

        /*var oparm = {
            'ID': $('#txtID').val(),
            'CampaniaID': $('#ddlCampana').val(),
            'InstitucionEducativaID': $('#ddlInstitucion').val(),
            'CIASeguroID': $('#ddlCia').val(),
            'ProductoID': $('#ddlProducto').val(),
            'BancoID': $('#ddlBanco').val(),
            'MonedaID': $('#ddlMoneda').val(),
            'Fecha': $('#txtFecha').val(),
            'NroCheque': $('#txtNroCheque').val(),
            'Monto': $('#txtMonto').val()
        };*/

        var obj = new Array();
        obj[0] = $('#txtID').val();
        obj[1] = $('#ddlCampana').val();
        obj[2] = $('#ddlInstitucion').val();
        obj[3] = $('#ddlCia').val();
        obj[4] = $('#ddlProducto').val();
        obj[5] = $('#ddlBanco').val();
        obj[6] = $('#ddlMoneda').val();
        obj[7] = $('#txtFecha').val();
        obj[8] = $('#txtNroCheque').val();
        obj[9] = $('#txtMonto').val();
        /*  datos.ID = $('#txtID').val();
          datos.CampaniaID = $('#ddlCampana').val();
          datos.InstitucionEducativaID = $('#ddlInstitucion').val();
          datos.CIASeguroID = $('#ddlCia').val();
          datos.ProductoID = $('#ddlProducto').val();
          datos.BancoID = $('#ddlBanco').val();
          datos.MonedaID = $('#ddlMoneda').val();
          datos.Fecha = $('#txtFecha').val();
          datos.NroCheque = $('#txtNroCheque').val();
          datos.Monto= $('#txtMonto').val();*/

        // var pParm = JSON.stringify(datos);
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/GrabarCheque",
            data: JSON.stringify({ arr: obj }),
            // data: "{'pCheque':" + pParm + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
                var result = response.d.Status;
                var mensaje = response.d.Mensaje;
                if (result == "OK") {
                    $("#myDialog").modal("toggle");
                    $("#getCode").html(mensaje);
                } else {
                    $("#myDialog").modal("toggle");
                    $("#getCode").html(mensaje);
                }

                /* var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                 $("#myDialog").modal("toggle");
                 $("#getCode").html(models);*/
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
    }

    function btnEliminar(Id) {
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/EliminarCheque",
            data: "{Id:'" + Id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#myDialog").modal("toggle");
                $("#getCode").html(response.d);
                $("#flag").val("true");
                listarCheques(1);
            },
            error: function (response) {
                if (response.length != 0)
                    alert(response);
            }
        });
        $("#confirm-submit").modal("hide");
    }

    $(document).ready(function () {
        loadFCampana();
        loadFTipoSeguro();
        loadFInstitucion();
        $("#btnBuscar").click(function (e) {
            $("#flag").val("true");
            e.preventDefault();
            listarCheques(1);
        });

        loadCampana();
        $("#ddlCampana").change(loadInstitucion);
        $("#ddlInstitucion").change(loadCia);
        $("#ddlCia").change(loadProductos);
        loadBanco();
        $("#ddlProducto").change(loadMoneda);
        $("#btnGuardar").click(btnGuardar);
    });

    function LoadMntChq(estado, datos) {

        if (estado == 'NVO') {
            $('#txtID').val(0);
            $('#ddlCampana').val(0);
            $('#ddlInstitucion').val(0);
            $('#ddlCia').val(0);
            $('#ddlProducto').val(0);
            $('#ddlBanco').val(0);
            $('#ddlMoneda').val(0);
            $('#txtFecha').val('');
            $('#txtNroCheque').val('');
            $('#txtMonto').val('0');
        }
        if (estado == 'UPD') {
            $.ajax({
                type: "POST",
                url: "ConsultarCheque.aspx/getChequeXId",
                data: "{'id':'" + datos.ID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var vals = new Object();
                    var result = JSON.parse(response.d.DataJson);
                    console.log(result);
                    $('#ddlCampana').empty();
                    $('#ddlCampana').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listCampañas.length; i++) {
                        var valor = result.listCampañas[i].ID;
                        var text = result.listCampañas[i].Nombre;
                        $("#ddlCampana").append($("<option></option>").val(valor).html(text));
                    }


                    $('#ddlInstitucion').empty();
                    $('#ddlInstitucion').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listInstitucionEducativa.length; i++) {
                        var valor = result.listInstitucionEducativa[i].ID;
                        var text = result.listInstitucionEducativa[i].Nombre;
                        $("#ddlInstitucion").append($("<option></option>").val(valor).html(text));
                    }

                    $('#ddlCia').empty();
                    $('#ddlCia').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listCIASeguro.length; i++) {
                        var valor = result.listCIASeguro[i].ID;
                        var text = result.listCIASeguro[i].Nombre;
                        $("#ddlCia").append($("<option></option>").val(valor).html(text));
                    }

                    $('#ddlProducto').empty();
                    $('#ddlProducto').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listProducto.length; i++) {
                        var valor = result.listProducto[i].ID;
                        var text = result.listProducto[i].Nombre;
                        $("#ddlProducto").append($("<option></option>").val(valor).html(text));
                    }

                    $('#ddlBanco').empty();
                    $('#ddlBanco').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listBanco.length; i++) {
                        var valor = result.listBanco[i].ID;
                        var text = result.listBanco[i].Nombre;
                        $("#ddlBanco").append($("<option></option>").val(valor).html(text));
                    }

                    $('#ddlMoneda').empty();
                    $('#ddlMoneda').append("<option value='0'>--SELECCIONE--</option>");
                    for (var i = 0; i < result.listMoneda.length; i++) {
                        var valor = result.listMoneda[i].ID;
                        var text = result.listMoneda[i].Nombre;
                        $("#ddlMoneda").append($("<option></option>").val(valor).html(text));
                    }

                    $('#txtID').val(result.ID);
                    $('#ddlCampana').val(result.CampaniaID);

                    // setTimeout("loadInstitucion();", 250);
                    $('#ddlInstitucion').val(result.InstitucionEducativaID);
                   // setTimeout("$('#ddlInstitucion').val(" + datos.InstitucionEducativaID + ");", 500);

                    $('#ddlCia').val(result.CIASeguroID);
                    //setTimeout("loadCia();", 750);
                    //setTimeout("$('#ddlCia').val(" + datos.CIASeguroID + ");", 1000);

                    $('#ddlProducto').val(result.ProductoID);
                    //setTimeout("loadProductos();", 1250);
                    //setTimeout("$('#ddlProducto').val(" + datos.ProductoID + ");", 1500);

                    $('#ddlBanco').val(result.BancoID);

                    $('#ddlMoneda').val(result.MonedaID);
                    //setTimeout("loadMoneda();", 1750);
                    //setTimeout("$('#ddlMoneda').val(" + datos.MonedaID + ");", 2000);
                    debugger;
                    $('#txtFecha').val(moment(result.Fecha).format('YYYY-MM-DD'));
                    //$('#txtFecha').val(result.Fecha);
                    $('#txtNroCheque').val(result.NroCheque);
                    $('#txtMonto').val(parseFloat(result.Monto).toFixed(2));


                },
                error: function (response) {
                    if (response.length != 0)
                        alert(response);
                }
            });

            /*
            $('#txtID').val(datos.ID);
            $('#ddlCampana').val(datos.CampaniaID);

            setTimeout("loadInstitucion();", 250);
            setTimeout("$('#ddlInstitucion').val(" + datos.InstitucionEducativaID + ");", 500);

            setTimeout("loadCia();", 750);
            setTimeout("$('#ddlCia').val(" + datos.CIASeguroID + ");", 1000);

            setTimeout("loadProductos();", 1250);
            setTimeout("$('#ddlProducto').val(" + datos.ProductoID + ");", 1500);

            $('#ddlBanco').val(datos.BancoID);

            setTimeout("loadMoneda();", 1750);
            setTimeout("$('#ddlMoneda').val(" + datos.MonedaID + ");", 2000);
            debugger;
            $('#txtFecha').val(datos.Fecha);
            $('#txtNroCheque').val(datos.NroCheque);
            $('#txtMonto').val(datos.Monto);*/
            //var Vals = JSON.stringify(datos);
            //$('#btnGuardar').attr('onclick', 'btnGuardar(' + Vals + ')');
        }
        setTimeout("$('#myModalCheque').modal('show');", 1000);

        //var miaplicacion = { queue: [] };
        //if (estado == 'UPD') {
        //    debugger;
        //    $('#txtID').val(datos.ID);
        //    $('#ddlCampana').val(datos.CampaniaID);
        //    $('#ddlBanco').val(datos.BancoID);
        //    miaplicacion.queue.push(function() { loadInstitucion();});
        //    miaplicacion.queue.push(function() { $('#ddlInstitucion').val(datos.InstitucionEducativaID);});
        //    miaplicacion.queue.push(function() { loadCia();});
        //    miaplicacion.queue.push(function() { $('#ddlCia').val(datos.CIASeguroID);});
        //    miaplicacion.queue.push(function() { loadProductos();});
        //    miaplicacion.queue.push(function() { $('#ddlProducto').val(datos.ProductoID);});
        //    miaplicacion.queue.push(function() { loadMoneda();});
        //    miaplicacion.queue.push(function() { $('#ddlMoneda').val(datos.MonedaID);});
        //    $('#txtFecha').val(datos.Fecha);
        //    $('#txtNroCheque').val(datos.NroCheque);
        //    $('#txtMonto').val(datos.Monto);

        //    var l = miaplicacion.queue.length;
        //    for(var i = 0; i < l; i++) {
        //        miaplicacion.queue[i]();
        //    }
        //    $('#myModalCheque').modal('show');
        //}
    }
</script>  

</asp:Content>
