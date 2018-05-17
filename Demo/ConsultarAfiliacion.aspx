<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultarAfiliacion.aspx.cs" MasterPageFile="~/Home.Master" Inherits="Demo.ConsultarAfiliacion" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.5.2.min.js"></script>
    <link href="Css/standar.css" rel="stylesheet" />
  


    <script type="text/javascript">  
     $(function () {  
         $("[CodigoAfiliacion*=grvAfiliacion] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });  
 </script>  

    <script type="text/javascript">
        $(function () {
            $('#<%=grvAfiliacion.ClientID %> img').click(function () {                
                var img2 = $(this)     
                var orderid2 = $(this).attr('orderid2');             
                var tr2 = $('#<%=grvAfiliacion.ClientID %> tr[orderid2 =' + orderid2 + ']')           
                tr2.toggle();                
                if (tr2.is(':visible'))
                   img2.attr('src', 'Images/cerrar.png');
                else
                   img2.attr('src', 'Images/abrir.png');               
            });
        });
    </script>
       
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   
    
    <div>
        <asp:MultiView ID="MVTipoAfiliacion" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                                                  
                           <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color: white"><b>Consulta Afiliación</b> : Criterios de busqueda </a>
                                </div>
                                <div class="panel-body">


                                <div class="row">
                                    <div style="padding: 10px;">
                                        Texto de busqueda
                                         <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%"></asp:TextBox>
                                        <asp:Button ID="btnBuscarAfiliados" runat="server" CssClass="button"  Text="Buscar" OnClick="btnBuscarAfiliados_Click"  Style="background-color:orangered"/>
                                        <%--<asp:Button ID="btnNuevo" runat="server" CssClass="button" Style="background-color: orangered" Text="Nuevo" />--%>

                                    </div>
                                    <div style="padding: 10px;">
                                        <div>
                                            Resultados
                                        </div>
                                       <asp:GridView ID="grvAfiliacion" runat="server" AutoGenerateColumns="False" CssClass="Grid"  Width="100%" DataKeyNames="CodigoAfiliacion,IdInstitucion, FechaCreacionAfiliacion" OnRowDataBound="grvAfiliacion_RowDataBound" OnPageIndexChanging="grvAfiliacion_PageIndexChanging" PageSize="15" >
                                            <Columns>
                                               <asp:TemplateField HeaderText="Cuentas"  HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                        <p style="text-align:center">
                                                         
                                                           <%-- <asp:ImageButton ID="BtnDetalleAfiliados" runat="server" OnClick="BtnDetalleAfiliados_Click" ImageUrl='<%# Eval("CodigoAfiliacion") %>' />--%>
                                                            <img alt="" src="Images/abrir.png" orderid2="<%# Eval("CodigoAfiliacion") %>"  height="12px" width="12px"/>
                                                        </p>
                                                </ItemTemplate>
                                                  <HeaderStyle Width="10px" />
                                                </asp:TemplateField>   
                                              
                                                  
                                                <asp:BoundField HeaderText="Institución Educativa"  ReadOnly="True" DataField="NombreAfiliacion" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Fecha" DataField="FechaCreacionAfiliacion" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="300px" />
                                                </asp:BoundField>


                                               <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr style="display:none;" orderid2="<%# Eval("CodigoAfiliacion") %>">
                                                            <td colspan="100%">
                                                                <div style="position: relative; left: 15px;">
                                                                   
                                                                           <asp:GridView ID="grvDetallesAfiliado" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" DataKeyNames="MonedaId,CodAsociacion,EstadoPago"  Width="98%" EmptyDataText="No hay contactos registrados" Caption="REGISTRO DE CONTACTOS" CssClass="Grid2" OnRowDataBound="grvDetallesAfiliado_RowDataBound" ShowFooter="True" OnRowCommand="grvDetallesAfiliado_RowCommand">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Id" HeaderText="Codigo" InsertVisible="False" ReadOnly="True" SortExpression="ID">
                                                                                  <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle> 

                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Asegurado" HeaderText="Asegurado" SortExpression="Asegurado" >
                                                                            <ItemStyle Width="390px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="Beneficiario" NullDisplayText="-"  HeaderText="Beneficiario" SortExpression="Beneficiario" >
                                                                            <ItemStyle Width="180px"/>
                                                                          
                                                                            </asp:BoundField>
                                                                     


                                                                             <asp:BoundField DataField="Prima" HeaderText="Prima" SortExpression="Seguro" >
                                                                            <ItemStyle Width="140px" />
                                                                            </asp:BoundField>



                                                                            <asp:BoundField DataField="Seguro" HeaderText="Seguro" SortExpression="Seguro" >
                                                                            <ItemStyle Width="380px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="SeguroCia" HeaderText="Cia Seguro" SortExpression="SeguroCia" >
                                                                            <ItemStyle Width="390px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="MonedaId" HeaderText="Moneda" SortExpression="SeguroCia" ItemStyle-CssClass="oculto">
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>

                                                                            <asp:BoundField DataField="ImageSeguro" HeaderText="ImageSeguro" ItemStyle-CssClass="oculto">
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>

                                                                             <asp:BoundField DataField="FechaDePago" HeaderText="FechaPago" NullDisplayText="-" >
                                                                           <%-- <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>--%>
                                                                                  <ItemStyle Width="75px" />
                                                                            </asp:BoundField>  

                                                                             <asp:BoundField DataField="NombreBanco" HeaderText="Banco" NullDisplayText="-" >
                                                                           <%-- <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>--%>
                                                                                  <ItemStyle Width="375px" />
                                                                            </asp:BoundField>  



                                                                            <asp:TemplateField HeaderText="Plan">
                                                                                <ItemTemplate>                                                             
                                                                                    <asp:ImageButton ID="btnPlan" runat="server"  ImageUrl="~/Images/DescargaPDF.png" Width="16px" Height="16px" CommandName="BtnPlan" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                            </asp:TemplateField>
                                                                            
                                                                            <asp:TemplateField HeaderText="Recibo">
                                                                                <ItemTemplate>
                                                                                   <asp:ImageButton ID="BtnRecibo" runat="server" OnClick="BtnRecibo_Click" ImageUrl="~/Images/IcoCuenta.png"  Width="16px" Height="16px"/>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" Width="61px" />
                                                                            </asp:TemplateField>    
                                                                            
                                                                             <asp:BoundField DataField="CodigoInstitucion" HeaderText="CodInstitucion">
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>

                                                                             <asp:BoundField DataField="CodigoPago" HeaderText="codigoPago" >
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>
                                                                               <asp:BoundField DataField="Id" HeaderText="Id" >
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>
                                                                             <asp:BoundField DataField="EstadoPago" HeaderText="Estado" >
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>     
                                                                            
                                                                            <asp:TemplateField HeaderText="Estado">
                                                                               
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEstado" runat="server" Text="Label"></asp:Label><br />
                                                                                    &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImgEstado" runat="server" Width="16px" Height="16px" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="100px" />
                                                                                   
                                                                            </asp:TemplateField>

                                                                               <asp:BoundField DataField="ProductoId" HeaderText="Estado" >
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>  
                                                                            <asp:BoundField DataField="CodAsociacion" HeaderText="Codigo Asociacion" SortExpression="CodAsociacion" ItemStyle-CssClass="oculto">
                                                                            <HeaderStyle CssClass="oculto" />
                                                                            <ItemStyle CssClass="oculto"></ItemStyle>
                                                                            </asp:BoundField>
                                                                                                                                                                           
                                                                        </Columns>
                                                                    </asp:GridView>   
                                                            
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                     <HeaderStyle CssClass="oculto" />
                                                    <ItemStyle CssClass="oculto" Width="95%"/>
                                                  
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HdCodigoAfiliacion" runat="server" />
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
    </div>
    
    <%--<div id="pnlEditarBanco" class="modal fade" role="dialog"  >
        <div class="modal-dialog" style="width:80%">

           <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Bancos</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group" style="display:none">
                                        <label for="Codigo" class="col-lg-2 control-label">Código</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtCodigoBanco" placeholder="Ingrese Codigo" />
                                        </div>
                                    </div>
                       <div class="RazonSocial">
                            <label for="RazonSocial" class="col-lg-2 control-label">Razón Social</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtRazonSocial" placeholder="Ingrese Razón Social" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label for="NombreCorto" class="col-lg-2 control-label">Nombre Corto</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombreCorto" placeholder="Ingrese NombreCorto" />
                            </div>
                        </div>

	               <div class="form-group">
                            <label for="RUC" class="col-lg-2 control-label">RUC</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="txtRUC" placeholder="Ingrese RUC" />
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
                            <label for="Departamento" class="col-lg-2 control-label">Departamento</label>
                            <div class="col-lg-10">
                                <asp:DropDownList runat="server" class="form-control" ID="DDLDepartamento" placeholder="Ingrese Departamento" AutoPostBack="True" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged"  AppendDataBoundItems="true">
                                      <asp:ListItem Value="0" Selected="True">Seleccione Departamento</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Provincia" class="col-lg-2 control-label">Provincia</label>
                            <div class="col-lg-10">
                                <asp:DropDownList runat="server" class="form-control" ID="DDLProvincia" placeholder="Ingrese Provincia" AutoPostBack="True" OnSelectedIndexChanged="DDLProvincia_SelectedIndexChanged" >
                                          <asp:ListItem Value="0" Selected="True">Seleccione Provincia</asp:ListItem>
                               </asp:DropDownList>
                            </div>
                        </div>

                         <div class="form-group">
                            <label for="Distrito" class="col-lg-2 control-label">Distrito</label>
                            <div class="col-lg-10">
                                <asp:DropDownList runat="server" class="form-control" ID="DDLDistrito" placeholder="Ingrese Distrito">
                                                     <asp:ListItem Value="0" Selected="True">Seleccione Distrito</asp:ListItem>                     
                               </asp:DropDownList>
                            </div>
                        </div>

                                </ContentTemplate>
                        </asp:UpdatePanel>

                         <div class="form-group">
                            <label for="Direccion" class="col-lg-2 control-label">Direccion</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="txtDireccion" placeholder="Ingrese Direccion" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="Usado en Proceso Afiliación" class="col-lg-2 control-label">Proceso Afiliación</label>
                            <div class="col-lg-10">
                                <asp:CheckBox runat="server" class="form-control" ID="chkUsadoProcAfiliación"  />
                            </div>
                        </div>

                         <div class="form-group">
                            <label for="LogoBanco" class="col-lg-2 control-label">Logo Banco</label>
                            <div class="col-lg-10">                                                                
                                <asp:FileUpload runat="server"  id="file_url"  onchange="mostrarImagen(this);"  />        
                             </div>
                        </div>
   
                            <div class="form-group">                            
                            <div class="col-lg-10">                                
                        
                             <asp:Image ID="img_destino2" runat="server" style="width:200px"/>
                             </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button class="btn btn-facebook" runat="server" ID="btnRegistrarContact" Text="Registrar" OnClick="btnRegistrarBanco_Click1" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    

    <%-- <div id="pnlOpenCuenta" class="modal fade" role="dialog" style="width:70%">
            <div class="modal-dialog" >

                    <div class="modal-content">
                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Editar contacto</h4>
                            </div>
                            <div class="modal-body" >
                                     <div class="container-fluid">
                                     <div class="form-group">
                                      <label for="ejemplo_email_3" class="col-lg-2 control-label">Ape.Paterno</label>
                                      <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" id="TxtContApePateEdit"   placeholder="Apellido Paterno"/>
                                      </div>
                                  </div>
                                  <div class="form-group">
                                      <label for="ApellidoMaterno" class="col-lg-2 control-label">Ape.Materno</label>
                                      <div class="col-lg-10">
                                           <asp:TextBox runat="server" class="form-control" id="TxtContApeMateEdit"    placeholder="Apellido Materno"/>
                                      </div>
                                      <div class="form-group">
                                          <label for="Nombres" class="col-lg-2 control-label">Nombres</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="TxtContNombresEdit" placeholder="Nombres"/>
                                          </div>
                                      </div>
                                      
                                      <div class="form-group">
                                          <label for="Cargo" class="col-lg-2 control-label">Cargo</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtContCargoEdit" placeholder="Cargo"/>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Email" class="col-lg-2 control-label">Email</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtContEmailEdit" placeholder="Email"/>
                                          </div>
                                      </div>

                                      <div class="form-group" style="display:none">
                                          <label for="BancoID" class="col-lg-2 control-label">BancoID</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtBancoID" placeholder="BancoID"/>
                                          </div>
                                      </div>

                            </div>
                            <div class="modal-footer">
                                    <asp:Button   class="btn btn-facebook" runat="server"  ID="btnEditaContacto" Text="Editar" OnClick="btnEditaContacto_Click"  />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                  </div>      
               </div>                    
            </div>
         </div>  
    </div>

     <div id="pnlCuentas" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 99%">
            <div class="modal-content">
                <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Gestionar Cuenta</h4>
                </div>
                
               <div class="modal-header">
                    <h4 class="modal-title">Información de la Cuenta</h4>
                </div>

                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="Codigo" class="col-lg-2 control-label" style="left: 0px; top: 0px">Banco:</label>
                            <div class="col-lg-10">
                                      <asp:TextBox runat="server" class="form-control" ID="TxtBanco" placeholder=""  Enabled="false" Width="100%"/>
                            </div>
                        </div>
                        <div class="Nombre">
                            <label for="Nombre" class="col-lg-2 control-label">Moneda:</label>
                            <div class="col-lg-10">
                                <asp:DropDownList runat="server" class="form-control" ID="ddlMoneda" placeholder="Seleccione" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-2 control-label">Empresa recaudadora:</label>
                            <div class="col-lg-10">
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlEmpresaRecaudadora" placeholder="Seleccione" />
                            </div>
                        </div>
                    
                    <div class="form-group">
                        <label for="Descripcion" class="col-lg-2 control-label">Numero de Cuenta:</label>
                        <div class="col-lg-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtNumero" placeholder="Ingrese Numero cuenta" />
                        </div>
                    </div>
                
                       <div class="form-group">
                            <label for="Descripcion" class="col-lg-2 control-label">Predeterminado:</label>
                            <div class="col-lg-10">
                                <asp:RadioButtonList ID="rbtPrederminado" runat="server" Height="18px" RepeatDirection="Horizontal" Width="154px" >
                                    <asp:ListItem Value=1>Si</asp:ListItem>
                                    <asp:ListItem Value=0 Selected ="true">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>                    

                        <div class="form-group">
                        <label for="Descripcion" class="col-lg-2 control-label"><asp:Label runat="server" ID="lblParametro1"></asp:Label></label>
                        <div class="col-lg-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtParametro1" placeholder="" />
                        </div>
                        </div>

                        <div class="form-group">
                        <label for="Descripcion" class="col-lg-2 control-label"><asp:Label runat="server" ID="lblParametro2"></asp:Label></label>
                        <div class="col-lg-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtParametro2" placeholder="" />
                        </div>
                        </div>

                        <div class="form-group">
                        <label for="Descripcion" class="col-lg-2 control-label"><asp:Label runat="server" ID="lblParametro3"></asp:Label></label>
                        <div class="col-lg-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtParametro3" placeholder="" />
                        </div>
                        </div>

                       <div class="form-group">
                        <label for="Descripcion" class="col-lg-2 control-label"><asp:Label runat="server" ID="lblParametro4"></asp:Label></label>
                        <div class="col-lg-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtParametro4" placeholder="" />
                        </div>
                        </div>

                       <div class="modal-footer">
                          <asp:Button class="btn btn-facebook" runat="server" ID="btnGuardar" Text="Guardar" OnCommand="btnGuardar_Command"/>
                           <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                      </div>
                </div>
            </div>
        </div>        
   </div>        
    </div>--%>
</asp:Content>


