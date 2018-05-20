<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ConsultaPagos.aspx.cs" Inherits="Demo.ConsultaPagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="Scripts/jquery-1.5.2.min.js"></script>--%> 
    <link href="Css/standar.css" rel="stylesheet" />
    <%-- <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" SortExpression="Beneficiario" />--%>  
  <%--Inicio Autocompele script id   --%>  
<%--    <script>
      function OnClienteSelected(source, e) {
            var idx = source._selectIndex;
            var clientes = source.get_completionList().childNodes;
            var value = clientes[idx]._value;
            var text = clientes[idx].firstChild.nodeValue;
            source.get_element().value = text;
            $get("<%=hdnIDCliente1.ClientID %>").value = e.get_value();
        }
  </script>--%>
 <%--Fin Autocompele script id   --%>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
<style>
    .actualizando_principal
    {
        background-color: #333333;
        filter: alpha(opacity=60); 
        opacity: 0.60; 
        width: 100%;
        top: 0px; 
        left: 0px; 
        position: fixed; 
        height: 100%;
    }

    .actualizando
    {
        margin:auto;
        filter: alpha(opacity=100);
        opacity: 1;
        font-size:small;
        vertical-align: middle;
        top: 35%;
        position: fixed;
        right: 45%;
        margin-left:auto;
        margin-right:auto;
        text-align: center;
        background-color: #ffffff;
        height: 128px;
        width:128px;
        -webkit-border-radius: 10px 10px 10px 10px;
        border-radius: 10px 10px 10px 10px;
    }

    .actualizando img
    {
        /*width:60px;
        height:64px;
        margin-left:auto;
        margin-right:auto;
        margin-top:32px;*/
    }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true" AsyncPostBackTimeOut= "360000">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="actualizando_principal">
                <div class="actualizando">
                    <asp:Image ID="imgEsperando" ImageUrl="Images/cargando.gif" runat="server" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel-group" id="accordion">                 
                 <div class="panel panel-default">
                 <div class="panel-primary">
                    <div class="panel-heading">
                        <a data-toggle="collapse" href="#collapse1" style="color:white">Criterios de Búsqueda</a>
                    </div>
                     <div id="collapse1" class="panel-collapse collapse in">
                    <div class="panel-body">     
                    <div class="row">           
                    <div class="form-group">    
                        <label for ="institucion educativa" class="col-lg-3 control-label">Institución  Educativa: *</label>
                            <div class="col-lg-9">                            
                                <%--<asp:HiddenField ID="hdnIDCliente1" runat="server" />
                                <asp:TextBox ID="txtSearch" class="form-control" placeholder="Institucion Educativa" Width="250" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender
                                    ServiceMethod="SearchClientes"
                                    MinimumPrefixLength = "1" 
                                    CompletionInterval="10"
                                    EnableCaching="false"
                                    CompletionSetCount="10"
                                    TargetControlID="txtSearch"
                                    ID="txtSearch_AutoCompleteExtender"
                                    runat="server"
                                    FirstRowSelected="false"
                                    OnClientItemSelected = "OnClienteSelected">
                                </ajaxToolkit:AutoCompleteExtender>--%>
                                <asp:DropDownList ID="ddlInstitucion" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control"  Width="250" >
                                    <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                                           
                        </div>
                    </div>
                      
                    <div class="form-group">
                        <label for="cia de seguro" class="col-lg-3 control-label">CIA de Seguros: *</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="DDLCiaSeguro" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control"  Width="250" >
                                    <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="tipo de seguro" class="col-lg-3 control-label">Tipo de Seguro: *</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="DDLTipoSeguro" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control"  Width="250" >
                                    <asp:ListItem Selected="True" Value="0">SELECCIONE</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="pago" class="col-lg-3 control-label">Solo Pago</label>
                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" class="form-control" ID="chkPago"   Width="50" />
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="banco" class="col-lg-3 control-label">Banco: </label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="DDLBanco" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control" placeholder="Banco"   Width="250">
                                    <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="moneda" class="col-lg-3 control-label">Moneda: </label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="DDLMoneda" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control"   Width="250">
                                    <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>
               

                   <div class="form-group">
                        <label for="VigenciaBanco" class="col-lg-3 control-label">Fecha  Inicio</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtFechaInicio" ToolTip="Fecha Inicio"   data-placement="left"  Width="250"
                                             placeholder="Ingrese Fecha Inicio" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')"/>
                            </div>
                    </div>

                      <div class="form-group">
                        <label for="VigenciaBanco" class="col-lg-3 control-label">Fecha Final</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtFechaFinal" ToolTip="Fecha Final"   data-placement="left"  Width="250"
                                             placeholder="Ingrese Fecha Final" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')"/>
                            </div>
                    </div>



                    <div class="form-group">
                        <label for="apellido nombres" class="col-lg-3 control-label">Apellidos y/o Nombres: *</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtApenombres" placeholder="Apellidos y/o Nombres"  Width="250" />
                                    <asp:Button class="btn btn-facebook" runat="server" ID="btnbuscar" Text="Buscar" OnClick="btnbuscar_Click" />
                                    
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
                         <a data-toggle="collapse" href="#collapse2" style="color:white">Resultados de Busqueda</a>
                     </div>
                    <div id="collapse2" class="panel-collapse collapse in" style="padding:10px;">
                        <asp:GridView ID="grvSeguros" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" PageSize="10" Width="100%" OnPageIndexChanging="grvSeguros_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo" />
                                <asp:BoundField DataField="AlumnoApellidoPaterno" HeaderText="Apellido Paterno" SortExpression="ApellidoPaterno" />
                                <asp:BoundField DataField="AlumnoApellidoMaterno" HeaderText="Apellido Materno" SortExpression="ApellidoMaterno" />
                                <asp:BoundField DataField="AlumnoNombre" HeaderText="Nombres" SortExpression="Nombres" />
                                <asp:BoundField DataField="AlumnoFechaNacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:dd/MM/yyyy}" SortExpression="FechaNacimiento" />
                                <asp:BoundField DataField="AlumnoTipoDocumentoDsc" HeaderText="Tipo Documento" SortExpression="TipoDocumentoDsc" />
                                <asp:BoundField DataField="AlumnoNumeroDocumento" HeaderText="Nro. Documento" SortExpression="NroDocumento" />
                                <%-- <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" SortExpression="Beneficiario" />--%>
                                <asp:BoundField DataField="OperacionBancaria" HeaderText="Número de Operación" SortExpression="NumeroOperacion" />
                                <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}" SortExpression="FechaPago" />
                                <asp:BoundField DataField="MonedaPagoNombre" HeaderText="Moneda" SortExpression="Moneda" />
                                <asp:BoundField DataField="BancoPagoNombre" HeaderText="Banco" SortExpression="Banco" />
                                <asp:BoundField DataField="InstitucionEducativaNombre" HeaderText="Institucion Educativa" SortExpression="InstitucionEducativaNombre" />
                                <asp:BoundField DataField="Situacion" HeaderText="Situación" SortExpression="Situacion" />
                                <asp:BoundField DataField="IsPagadoDsc" HeaderText="Estado" SortExpression="IsPagadoDsc" />
                                <asp:BoundField DataField="Id"  HeaderText="id" SortExpression="Id" >
                                <HeaderStyle CssClass="oculto" />
                                <ItemStyle CssClass="oculto" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                 </div>
                 </div>
            </div>

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Hermes Seguros</h4>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button class="btn btn-soundcloud" runat="server" ID="btnExportarExcel" Text="Exportar Excel" OnClick="btnExportarExcel_Click" />
</asp:Content>
