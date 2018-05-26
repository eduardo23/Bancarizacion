<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ConsultarCheque.aspx.cs" Inherits="Demo.ConsultarCheque" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/standar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.4/css/bootstrap.min.css" integrity="sha384-2hfp1SzUoho7/TsGGGDaFdsuuDL0LX2hnUp6VkX3CUQ2K4K+xjboZdsXyp4oUHZj" crossorigin="anonymous">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.0.0/jquery.min.js" integrity="sha384-THPy051/pYDQGanwU6poAc/hOdQxjnOEXzbT+OuUAFqNqFjL+4IGLBgCJC3ZOShY" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/tether/1.2.0/js/tether.min.js" integrity="sha384-Plbmg8JY28KFelvJVai01l8WyZzrYWG825m+cZ0eDDS1f7d/js6ikvy1+X+guPIB" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.4/js/bootstrap.min.js" integrity="sha384-VjEeINv9OSwtWFLAtmc4JCtEJXXBub00gtSnszmspDLCtC0I4z4nqz7rEFbIZLLU" crossorigin="anonymous"></script>

    <script src="https://esimakin.github.io/twbs-pagination/js/jquery.twbsPagination.js"></script>--%>
<script src="Scripts/moment.js"></script>
  
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
<%--                        <asp:DropDownList ID="ddlFCampana" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>
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

<%--                        <asp:DropDownList ID="ddlFTipoSeguro" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>
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

<%--                        <asp:DropDownList ID="ddlFInstitucion" runat="server" CssClass="form-control">
                        </asp:DropDownList>--%>
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

                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Fecha Inicio"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-4">
                    <div class="col-md-5">
                        <label class="control-label">Fecha Fin:</label>
                    </div>
                    <div class="col-md-7">

                        <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Fecha Fin"></asp:TextBox>

                    </div>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="col-md-12" style="margin-top:5px; text-align:right;">


                <a  Class="btn btn-primary btn-sm" id="btnBuscar" Style="background-color: orangered" href="javascript:void(0)"  >
                    <span aria-hidden="true" class="glyphicon glyphicon-search"></span> Buscar
                </a>                      
<%--                <button id="btnBuscar" Class="btn btn-primary btn-sm" Style="background-color: orangered" onclick='return false;'>
                    <span aria-hidden="true" class="glyphicon glyphicon-search"></span> Buscar
                </button>--%>
<%--                <input type="button" id="btnBuscar" value="asdasd"/>--%>
                <button id="btnNuevo" Class="btn btn-primary btn-sm" Style="background-color: orangered" onclick='javascript:LoadMntChq("NVO","");return false;'>
                    <%--data-toggle="modal" data-target="#myModalCheque"--%>
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
                            <%--<input type="text" class="form-control" id="txtFecha" title="Fecha" onfocus="(this.type='date')"  placeholder="Ingrese Fecha" data-date-format="dd/mm/yyyy" />--%>
                            <input type="text" class="form-control" id="txtFecha" title="Fecha" placeholder="Ingrese Fecha" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')"/>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Nro. Cheque:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="text" class="form-control" id="txtNroCheque" Title="Nro de Cheque" placeholder="Ingrese Nro de Cheque"/>
                        </div>                    
                    </div>
                    <div class="form-group">
                        <div class="col-md-5">
                            <label class="control-label"> Monto:</label>
                        </div>
                        <div class="col-md-7">
                            <input type="text" class="form-control" id="txtMonto" Title="Monto" data-val-required="El Monto es Obligatorio" placeholder="Ingrese Monto"/>
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
            data: "{'CampaniaID':'" + $('#ddlFCampana').val() + "','ProductoID':'" + $('#ddlFTipoSeguro').val() + "','InstitucionEducativaID':'" + $('#ddlFInstitucion').val() + "','paginaActual':'" + currentPage + "','RegistroXpagina':'" + RegistroXpagina + "'}",
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
                        HTML += "<td>" + result[index]["Monto"] + "</td>";
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
        debugger;
        $.ajax({
            type: "POST",
            url: "Services/WS_ServiceHermes.asmx/getLstInstByCampana",
            data: "{CampanaId: '" + $('#ddlCampana').val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
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
        btnEliminar(Id);
    }
    function modalActualizar(oObj) {
        LoadMntChq('UPD', oObj);
    }
    function btnGuardar() {
        debugger;
        var oparm = {
            'ID': $('#txtID').val(),
            'CampaniaID': $('#ddlCampana').val(),
            'InstitucionEducativaID': $('#ddlInstitucion').val(),
            'CIASeguroID': $('#ddlCia').val(),
            'ProductoID': $('#ddlProducto').val(),
            'BancoID': $('#ddlBanco').val(),
            'MonedaID': $('#ddlMoneda').val(),
            'Fecha': $('#txtFecha').val(),
            'NroCheque': $('#txtNroCheque').val(),
            'Monto': $('#txtMonto').val(),
            'UsuarioCreacion': 'eduardo23@gmail.com'
        };
        var pParm = JSON.stringify(oparm);
        $.ajax({
            type: "POST",
            url: "ConsultarCheque.aspx/GrabarCheque",
            data: "{'pCheque':" + pParm + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var models = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
                $("#myDialog").modal("toggle");
                $("#getCode").html(models);
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
            $('#txtMonto').val('0.00');
        }
        if (estado == 'UPD') {
            $('#txtID').val(datos.ID);
            $('#ddlCampana').val(datos.CampaniaID);

            setTimeout("loadInstitucion();", 250);
            setTimeout("$('#ddlInstitucion').val("+ datos.InstitucionEducativaID +");", 500);

            setTimeout("loadCia();", 750);
            setTimeout("$('#ddlCia').val(" + datos.CIASeguroID + ");", 1000);

            setTimeout("loadProductos();", 1250);
            setTimeout("$('#ddlProducto').val(" + datos.ProductoID + ");", 1500);

            $('#ddlBanco').val(datos.BancoID);

            setTimeout("loadMoneda();", 1750);
            setTimeout("$('#ddlMoneda').val(" + datos.MonedaID + ");", 2000);

            $('#txtFecha').val(datos.Fecha);
            $('#txtNroCheque').val(datos.NroCheque);
            $('#txtMonto').val(datos.Monto);
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
