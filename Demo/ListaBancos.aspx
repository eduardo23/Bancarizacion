<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="ListaBancos.aspx.cs" Inherits="Demo.ListaBancos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="Scripts/jquery-1.5.2.min.js"></script>
        <link href="Css/standar.css" rel="stylesheet" />
    <style type="text/css">
        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=grvBancos] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>

    <%--   <script type="text/javascript">
            $(document).ready(function () {
            $("#<%=txtBusqueda.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({                                                          
                    url: 'WS_ServiceHermes.asmx/GetInstitucionesEducativas") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("#<%=pkInstitucionEducativa.ClientID %>").val(i.item.val);
            },
            minLength: 1
        });
    });       
</script>--%>

    <%--<script>    
     function openEditarProducto() {
         $('#pnlEditarProducto').modal('show');
     }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#<%=grvBancos.ClientID %> img').click(function () {                
                var img = $(this)     
                var orderid = $(this).attr('orderid');             
                var tr = $('#<%=grvBancos.ClientID %> tr[orderid =' + orderid + ']')           
                tr.toggle();                
                if (tr.is(':visible'))
                    img.attr('src', 'Images/icoContacto2.png');
                else
                    img.attr('src', 'Images/icoContacto.png');               
            });
        });
    </script>--%>
    <script type="text/javascript">
        $(function () {
            $('#<%=grvBancos.ClientID %> img').click(function () {
                var img = $(this)
                var orderid = $(this).attr('orderid');
                var tr = $('#<%=grvBancos.ClientID %> tr[orderid =' + orderid + ']')
                tr.toggle();
                if (tr.is(':visible'))
                    img.attr('src', 'Images/cerrar.png');
                else
                    img.attr('src', 'Images/abrir.png');

            });

        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('#<%=grvBancos.ClientID %> img').click(function () {
                var img2 = $(this)
                var orderid2 = $(this).attr('orderid2');
                var tr2 = $('#<%=grvBancos.ClientID %> tr[orderid2 =' + orderid2 + ']')
                tr2.toggle();
                if (tr2.is(':visible'))
                    img2.attr('src', 'Images/cerrar.png');
                else
                    img2.attr('src', 'Images/abrir.png');
            });
        });
    </script>


    <script type="text/javascript">
        function fn_ValidaCamposContacto() {
            debugger;
            var strapellidoPaterno = $("#TxtapellidoPaterno").val();
            var strapellidoMaterno = $("#TxtapellidoMaterno").val();
            var strNombres = $("#TxtNombres").val();
            var strCorreo = $("#TxtCorreoElectronico").val();
            var strCargo = $("#TxtCargo").val();
            var msg = "";
            if (strapellidoPaterno == "" || strapellidoPaterno == undefined) {
                msg += "<br> * Ingrese apellido paterno";
            }
            if (strapellidoMaterno == "" || strapellidoMaterno == undefined) {
                msg += "<br> * Ingrese apellido materno";
            }
            if (strNombres == "" || strNombres == undefined) {
                msg += "<br> * Ingrese nombre";
            }
            if (strCorreo == "" || strCorreo == undefined) {
                msg += "<br> * Ingrese correo electronico";
            }
            if (strCargo == "" || strCargo == undefined) {
                msg += "<br> * Ingrese cargo";
            }
            if (msg != "") {

                bootbox.alert({
                    message: msg,
                    size: 'small'
                });
                //bootbox.alert(msg);

            } else {

                $("#Button1").click();

            }
        }
    </script>

    <script type="text/javascript">
        function fn_ValidaCamposCuenta() {
            debugger;
            var strmoneda = $("#ddlMoneda").val();
            var strempresarecaudadora = $("#ddlEmpresaRecaudadora").val();
            var strNumero = $("#txtNumero").val();
            var strparametro1 = $("#txtParametro1").val();
            var strparametro2 = $("#txtParametro2").val();
            var strparametro3 = $("#txtParametro3").val();
            var strparametro4 = $("#txtParametro4").val();

            var msg = "";
            if (strmoneda == "" || strmoneda == undefined) {
                msg += "<br> * Seleccione la moneda";
            }
            if (strempresarecaudadora == "" || strempresarecaudadora == undefined) {
                msg += "<br> * Seleccione la empresa recaudadora";
            }
            if (strNumero == "" || strNumero == undefined) {
                msg += "<br> * Ingrese numero de cuenta";
            }
            if (strparametro1 == "" || strparametro1 == undefined) {
                msg += "<br> * Ingrese Cod. Concepto S/.";
            }
            if (strparametro2 == "" || strparametro2 == undefined) {
                msg += "<br> * Ingrese Clave";
            }
            if (strparametro3 == "" || strparametro3 == undefined) {
                msg += "<br> * Ingrese Nro. Cuenta S/.";
            }
            if (strparametro4 == "" || strparametro4 == undefined) {
                msg += "<br> * Ingrese Cod Secuencia S/.";
            }

            var rb = document.getElementById("<%=rbtPrederminado.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked) {
                msg += "<br> * Seleccione predeterminado";
            }

            if (msg != "") {

                bootbox.alert({
                    message: msg,
                    size: 'small'
                });
                //bootbox.alert(msg);

            } else {

                $("#btnGuardar").click();

            }
        }
    </script>

    <script type="text/javascript">
        function fn_ValidaCamposBanco() {
            debugger;
            var strRazonSocial = $("#TxtRazonSocial").val();
            var strNombreCorto = $("#TxtNombreCorto").val();
            var strRUC = $("#TxtRUC").val();
            var strDepartamento = $("#DDLDepartamento").val();
            var strProvincia = $("#DDLProvincia").val();
            var strDistrito = $("#DDLDistrito").val();
            var strDireccion = $("#txtDireccion").val();
            var strUsadoProcAfiliacio = $("#chkUsadoProcAfiliacion").val();

            var msg = "";
            if (strRazonSocial == "" || strRazonSocial == undefined) {
                msg += "<br> * Ingrese razon social";
            }
            if (strNombreCorto == "" || strNombreCorto == undefined) {
                msg += "<br> * Ingrese nombre corto";
            }
            if (strRUC == "" || strRUC == undefined) {
                msg += "<br> * Ingrese RUC";
            }
            if (strDepartamento == "" || strDepartamento == undefined) {
                msg += "<br> * Seleccione departamento";
            }
            if (strProvincia == "" || strProvincia == undefined) {
                msg += "<br> * Seleccione provincia";
            }
            if (strDistrito == "" || strDistrito == undefined) {
                msg += "<br> * Seleccione distrito";
            }
            if (strDireccion == "" || strDireccion == undefined) {
                msg += "<br> * Ingrese dirección";
            }

            if (msg != "") {
                bootbox.alert({
                    message: msg,
                    size: 'small'
                });
                //bootbox.alert(msg);

            } else {

                $("#btnRegistrarBanco_Click1").click();

            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div>
        <asp:MultiView ID="MVTipoBancos" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                            <div class="panel panel-primary">
                                <div class="panel-heading" >
                                  <b>BANCO</b>: Criterios de busqueda 
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div style="padding: 10px;">
                                            Texto de busqueda
                                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%"></asp:TextBox>
                                            <asp:Button ID="btnBuscar" runat="server" CssClass="button" OnClick="btnBuscar_Click" Text="Buscar" />
                                            <asp:Button ID="btnNuevo" runat="server" CssClass="button" OnClick="btnNuevo_Click" Style="background-color: orangered" Text="Nuevo" />
                                        </div>
                                        <div style="padding: 10px;">
                                            <div>
                                                Resultados
                                            </div>
                                            <asp:HiddenField ID="hdnId" runat="server" />
                                            <asp:HiddenField ID="hdnIdBanco" runat="server" />
                                            <asp:GridView ID="grvBancos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" OnRowCommand="grvBancos_RowCommand"
                                                PageSize="10" Width="100%" OnRowDataBound="grvBancos_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cuentas" HeaderStyle-Width="10px">
                                                        <ItemTemplate>
                                                            <p style="text-align: center">
                                                                <img alt="" src="Images/abrir.png" orderid2="<%# Eval("id") %>" height="11px" width="11px" />
                                                            </p>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10px" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ID" HeaderText="Código" ReadOnly="True" SortExpression="ID" HeaderStyle-Width="16px">
                                                        <HeaderStyle Width="16px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nombre" HeaderText="Razón Social" SortExpression="Nombre" />
                                                    <asp:BoundField DataField="RUC" HeaderText="RUC" SortExpression="RUC" />
                                                    <asp:TemplateField HeaderText="Situación" ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Convert.ToBoolean( Eval("Estado")) == true ? "Activo":"No Activo"   %>' ID="lblestado">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NombreCorto" HeaderText="NombreCorto" SortExpression="NombreCorto" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CodigoUbigeo" HeaderText="CodigoUbigeo" SortExpression="CodigoUbigeo" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProcesoAfiliacion" HeaderText="ProcesoAfiliacion" SortExpression="ProcesoAfiliacion" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">

                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Contactos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <p style="text-align: center">
                                                                <img alt="" src="Images/Abrir.png" orderid="<%# Eval("id") %>"  height="11px" width="11px" />
                                                                
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="61px" />
                                                        <HeaderStyle Width="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Contactos" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>                                                          
                                                                <asp:ImageButton ID="btnAgregar" runat="server" 
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                                     CommandName="AgregarContacto" data-toggle="tooltip" 
                                                                     ImageUrl="~/Images/addcontact.png" title="Agregar Contacto" Width="22px"  Height="22px"/>
                                                            </p>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="61px" />
                                                        <HeaderStyle Width="10px" />
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Cuentas" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnAgregarCuentas" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AgregarCuentas" data-toggle="tooltip" Height="14px" ImageUrl="~/Images/newaccount.png" title="Agregar cuentas" Width="14px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="61px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaBanco" data-toggle="tooltip" Height="14px" ImageUrl="~/Images/icoEditar.png" title="Editar Banco" Width="14px" OnClick="btnEditar_Click" />
                                                            <asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaBanco" data-toggle="tooltip" Height="17px" ImageUrl="~/Images/deletes.png" title="Anular Banco" Width="17px" />
                                                            <asp:ImageButton ID="btnActivar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ActivaBanco" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoActivo.png" title="Activar Banco" Visible='<%# Convert.ToBoolean(Eval("Estado")) ? false : true   %>' Width="16px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="61px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr style="display: none;" orderid="<%# Eval("ID") %>">
                                                                <td colspan="100%">
                                                                    <div style="position: relative; left: 25px;">
                                                                        <asp:GridView ID="grvContactos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="80%" 
                                                                            EmptyDataText="No hay contactos registrados " OnRowCommand="grvContactos_RowCommand" 
                                                                            Caption="REGISTRO DE CONTACTOS" CssClass="Grid2">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="Contacto" HeaderText="Nombre" SortExpression="Nombre" />
                                                                                <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                                                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="ApellidoPaterno" HeaderText="ApellidoPaterno" SortExpression="ApellidoPaterno" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="ApellidoMaterno" HeaderText="ApellidoMaterno" SortExpression="ApellidoMaterno" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="BancoID" HeaderText="BancoID" SortExpression="BancoID" />
                                                                                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto">
                                                                                    <HeaderStyle CssClass="oculto" />
                                                                                    <ItemStyle CssClass="oculto" />
                                                                                </asp:BoundField>
                                                                                <asp:TemplateField HeaderText="Acciones">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaContacto" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar contacto" Width="16px" />
                                                                                        <asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaContacto" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="Anular contacto" Width="16px" />
                                                                                        <asp:ImageButton ID="btnActivar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ActivaContacto" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoActivo.png" title="Activar Contacto" Visible='<%# Convert.ToBoolean(Eval("Estado")) ? false : true   %>' Width="16px" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr style="display: none;" orderid2="<%# Eval("ID") %>">
                                                                <td colspan="100%">
                                                                    <div style="position: relative; left: 25px;">
                                                                        <asp:GridView ID="grvCuentas" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"
                                                                            Width="80%" EmptyDataText="No hay contactos registrados " Caption="REGISTRO DE CUENTAS"
                                                                            CssClass="Grid2" OnRowCommand="grvCuentas_RowCommand">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="Número" HeaderText="Numero" SortExpression="Numero" />
                                                                                <asp:BoundField DataField="Tipo_de_Moneda" HeaderText="Tipo de Moneda" SortExpression="Tipo_de_Moneda" />
                                                                                <asp:BoundField DataField="Empresa_Recaudadora" HeaderText="Empresa Recaudadora" SortExpression="Empresa_Recaudadora" />
                                                                                <asp:BoundField DataField="bancoId" HeaderText="bancoid" SortExpression="bancoid" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                                                <asp:BoundField DataField="Nombre" HeaderText="Banco" SortExpression="Nombre" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />

                                                                                <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="16px">
                                                                                    <ItemTemplate>
                                                                                        <p style="text-align: center">
                                                                                            <asp:ImageButton ID="btnEditarCuenta" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditarCuenta" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar Cuenta" Width="16px" />
                                                                                        </p>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Eliminar" HeaderStyle-Width="16px">
                                                                                    <ItemTemplate>
                                                                                        <p style="text-align: center">
                                                                                            <asp:ImageButton ID="btnAnularCuenta" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnularCuenta" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="Eliminar Cuenta" Width="16px" />
                                                                                        </p>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>

                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="oculto" />
                                                        <ItemStyle CssClass="oculto" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>


            </asp:View>
        </asp:MultiView>

        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Bancos</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="txtmensaje" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        |
    </div>

    <div id="pnlEditarBanco" class="modal fade" role="dialog">
        <div class="modal-dialog" >
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Banco</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">

                        <div class="form-group" style="display: none">
                            <label for="Codigo" class="col-lg-4 control-label">Código</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtCodigoBanco" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="RazonSocial" class="col-lg-4 control-label">Razón Social</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtRazonSocial" placeholder="Ingrese Razón Social"
                                    ToolTip="Razon Social" data-toggle="tooltip" data-placement="left" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="NombreCorto" class="col-lg-4 control-label">Nombre Corto</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombreCorto" placeholder="Ingrese NombreCorto"
                                    ToolTip="Nombre corto" data-toggle="tooltip" data-placement="left" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="RUC" class="col-lg-4 control-label">RUC</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtRUC" placeholder="Ingrese RUC"
                                    ToolTip="RUC" data-toggle="tooltip" data-placement="left" />
                            </div>
                        </div>
                        <br />
                    <asp:UpdatePanel runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLDepartamento" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="DDLProvincia" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>

                                <div class="form-group">
                                    <label for="Departamento" class="col-lg-4 control-label">Departamento</label>
                                    <div class="col-lg-8">
                                        <asp:DropDownList runat="server" class="form-control" ID="DDLDepartamento" placeholder="Ingrese Departamento" AutoPostBack="True" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged" AppendDataBoundItems="true" ToolTip="Departamento" data-toggle="tooltip" data-placement="left">
                                            <asp:ListItem Value="0" Selected="True">Seleccione Departamento</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="Provincia" class="col-lg-4 control-label">Provincia</label>
                                    <div class="col-lg-8">
                                        <asp:DropDownList runat="server" class="form-control" ID="DDLProvincia" placeholder="Ingrese Provincia" AutoPostBack="True" OnSelectedIndexChanged="DDLProvincia_SelectedIndexChanged" ToolTip="Provincia" data-toggle="tooltip" data-placement="left">
                                            <asp:ListItem Value="0" Selected="True">Seleccione Provincia</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="Distrito" class="col-lg-4 control-label">Distrito</label>
                                    <div class="col-lg-8">
                                        <asp:DropDownList runat="server" class="form-control" ID="DDLDistrito" placeholder="Ingrese Distrito"
                                            ToolTip="Distrito" data-toggle="tooltip" data-placement="left">
                                            <asp:ListItem Value="0" Selected="True">Seleccione Distrito</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                       </ContentTemplate>
                   </asp:UpdatePanel>

                        <div class="form-group">
                            <label for="Direccion" class="col-lg-4 control-label">Direccion</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtDireccion" placeholder="Ingrese Direccion"
                                    ToolTip="Dirección" data-toggle="tooltip" data-placement="left" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Usado en Proceso Afiliación" class="col-lg-4 control-label">Proceso Afiliación</label>
                            <div class="col-lg-8">
                                <asp:CheckBox runat="server" class="form-control" ID="chkUsadoProcAfiliación"
                                    ToolTip="Proceso afiliación" data-toggle="tooltip" data-placement="left" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="LogoBanco" class="col-lg-4 control-label">Logo Banco</label>
                            <div class="col-lg-8">
                                <asp:FileUpload runat="server" ID="file_url" onchange="mostrarImagen(this);" />
                            </div>
                        </div>

                        <div class="form-inline">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-10">
                                <%-- <img id="img_destino" alt="" style="width:200px"  /> --%>
                                <asp:Image ID="img_destino2" runat="server" Style="width: 200px" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <%--<a class="btn btn-facebook" onclick="fn_ValidaCamposBanco();">&nbsp; Registrar Banco :)</a>--%>
                        <div <%--style="display: none;"--%>>
                            <asp:Button class="button" runat="server" ID="btnRegistrarContact" Text=" Registrar Banco" OnClick="btnRegistrarBanco_Click1" 
                                ClientIDMode="Static" />
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="pnlEditarContacto" class="modal fade" role="dialog">
        <div class="modal-dialog" >

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Contacto</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group" style="display: none">
                            <label for="Codigo" class="col-lg-4 control-label">Codigo</label>
                            <div class="col-lg-8">
                                <asp:HiddenField ID="hdfidContacto" runat="server" Value="0" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ApellidoPaterno" class="col-lg-4 control-label">Apellido Paterno</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtapellidoPaterno" placeholder="Ingrese apellido paterno" 
                                    ToolTip="Apellido paterno" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />

                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ApellidoMaterno" class="col-lg-4 control-label">Apellido Materno</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtapellidoMaterno" placeholder="Ingrese apellido materno" 
                                    ToolTip="Apellido Materno" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />

                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Nombres" class="col-lg-4 control-label">Nombres </label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombres" placeholder="Ingrese nombre" 
                                    ToolTip="Nombres" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />

                            </div>
                        </div>

                        <div class="form-group">
                            <label for="CorreoElectronico" class="col-lg-4 control-label">Correo Electrónico</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtCorreoElectronico" placeholder="Ingrese correo electrónico" 
                                    ToolTip="Correo electrónico" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />

                            </div>
                        </div>
                        <br />
                        <div class="form-group">
                            <label for="Cargo" class="col-lg-4 control-label">Cargo</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtCargo" placeholder="Ingrese cargo" 
                                    ToolTip="Cargo" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />

                            </div>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <%--<a class="facebook" onclick="fn_ValidaCamposContacto();">&nbsp; Registrar</a>--%>                        
                        <asp:Button class="button" runat="server" ID="Button1" Text="Registrar" OnClick="btnEditaContacto_Click" ClientIDMode="Static" />                       
                        <%--<button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>--%>
                        <asp:Button Style="background-color: orangered" class="button" ID="btnCancelar" runat="server" data-dismiss="modal" Text="Cancelar" />
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="pnlCuentas" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Gestionar Cuenta</h4>
                </div>

                <div class="modal-header">
                    <h4 class="modal-title">Información de la Cuenta</h4>
                </div>

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="Codigo" class="col-lg-4 control-label" style="left: 0px; top: 0px">Banco:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TxtBanco" ReadOnly="true" Width="100%" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Nombre" class="col-lg-4 control-label">Moneda:</label>
                            <div class="col-lg-8">
                                <asp:DropDownList runat="server" class="form-control" ID="ddlMoneda" placeholder="Seleccione"
                                    ToolTip="Moneda" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">Empresa recaudadora:</label>
                            <div class="col-lg-8">
                                <asp:DropDownList runat="server" class="form-control" ID="ddlEmpresaRecaudadora" placeholder="Seleccione"
                                    ToolTip="Empresa recaudadora" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">Numero de Cuenta:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtNumero" placeholder="Ingrese Numero cuenta"
                                    ToolTip="Numero de cuenta" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">Predeterminado:</label>
                            <div class="col-lg-8">
                                <asp:RadioButtonList ID="rbtPrederminado" runat="server" Height="18px" RepeatDirection="Horizontal" Width="154px"
                                    ToolTip="Predeterminado" data-toggle="tooltip" data-placement="left" ClientIDMode="Static">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="true">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">
                                <asp:Label runat="server" ID="lblParametro1"></asp:Label></label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtParametro1" placeholder=""
                                    ToolTip="Cod. Concepto S/." data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">
                                <asp:Label runat="server" ID="lblParametro2"></asp:Label></label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtParametro2" placeholder=""
                                    ToolTip="Clave" data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">
                                <asp:Label runat="server" ID="lblParametro3"></asp:Label></label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtParametro3" placeholder=""
                                    ToolTip="Nro. Cuenta S/." data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">
                                <asp:Label runat="server" ID="lblParametro4"></asp:Label></label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtParametro4" placeholder=""
                                    ToolTip="Cod Secuencia S/." data-toggle="tooltip" data-placement="left" ClientIDMode="Static" />
                            </div>
                        </div>

                        <div class="modal-footer">
                            <a class="btn btn-facebook" onclick="fn_ValidaCamposCuenta();">&nbsp; Registrar</a>
                            <div style="display: none;">
                                <asp:Button class="button" runat="server" ID="btnGuardar" Text="Guardar" OnCommand="btnGuardar_Command" ClientIDMode="Static" />
                            </div>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
