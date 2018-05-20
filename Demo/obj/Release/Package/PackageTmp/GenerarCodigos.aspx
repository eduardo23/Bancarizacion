<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GenerarCodigos.aspx.cs" Inherits="Demo.GenerarCodigos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="Css/standar.css" rel="stylesheet" />
  <%--Inicio Autocompele script id   --%>  
       
    <script type="text/javascript">
        $(function () {
            $('[id*=ContentPlaceHolder1_btnGenerador]').bind('click', function () {
                var progress = setInterval(function () {
                    var $bar = $('.bar');
                    if ($bar.width() >= 400) {
                        clearInterval(progress);
                        $('.progress').removeClass('active');
                    } else {
                        $bar.width($bar.width() + 40);
                    }
                    $bar.text($bar.width() / 4 + "%");
                }, 540);
            });
        });           
    </script>
     

    <script>
      function OnClienteSelected(source, e) {
            var idx = source._selectIndex;
            var clientes = source.get_completionList().childNodes;
            var value = clientes[idx]._value;
            var text = clientes[idx].firstChild.nodeValue;
            source.get_element().value = text;
            $get("<%=hdnIDCliente1.ClientID %>").value = e.get_value();
              $('#<%=btnFiltrarSeguros.ClientID%>').click();            
        }

        function ClientItemSelected(sender, e) {
            $get("<%=hdnIDCliente1.ClientID %>").value = e.get_value();              
        
        }
  </script>
 <%--Fin Autocompele script id   --%>         
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
         <asp:ScriptManager ID="ScriptManager1" runat="server">          </asp:ScriptManager>        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


                            
        <asp:MultiView runat="server" ID="MVCodigos" ActiveViewIndex="0">
          <asp:View runat="server" ID="vwDefault" >    
                <div class="myContent"  style="width:100%">                                               
                 
                  <br />                
                   <div class="panel-group" id="accordion">
                      <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color:white">Generar Codigos : Criterios de Búsqueda</a>
                            </div>
                            <div class="panel-body">                      
                                
                <%--<div class="panel-group" id="accordion">
                    <div class="Panel with panel-info class">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Criterios de Búsqueda</a>
                            </h4>
                        </div>--%>

                        <div id="collapse1" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <asp:Panel ID="pBodySeccion1" runat="server" Width="100%" CssClass="wrap">
                                    <div>
                                        <div class="form-group">
                                            <label for="Institución Educativa:" class="col-lg-2 control-label">Institución Educativa:</label>
                                            <div class="col-lg-10">
                                                <%--<asp:DropDownList runat="server" class="form-control" ID="DDLInEducativa" placeholder="Ingrese Institucion educativa" AppendDataBoundItems="True" Width="400px">
                                                    <asp:ListItem Value="0" Selected="True">Seleccione  Institución Educativa:</asp:ListItem>
                                                </asp:DropDownList>--%>
                                                <asp:TextBox runat="server" ID="txtSearch"  class="form-control" AutoPostBack="True" placeholder="Institucion Educativa"  Width="250" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="10" OnClientItemSelected = "ClientItemSelected"                                           
                                                                                                
                                                  BehaviorID="txtSearch_AutoCompleteExtender" 
                                                  DelimiterCharacters="2"
                                                 ServiceMethod="SearchClientes"
                                                  MinimumPrefixLength = "2"                                                 
                                                  TargetControlID="txtSearch"></ajaxToolkit:AutoCompleteExtender>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="Seguro" class="col-lg-2 control-label">Seguro:</label>
                                            <div class="col-lg-10">
                                                <asp:DropDownList runat="server" class="combo" ID="DDLSeguro" placeholder="Ingrese Seguro" AppendDataBoundItems="True" Width="400px">
                                                    <asp:ListItem Value="0" Selected="True">Seleccione Seguro</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button ID="btnFiltrarSeguros" runat="server" OnClick="btnFiltrarSeguros_Click" Text="Cancelar" Style="display:none" />
                                            </div>
                                        </div>

                                        <div style="text-align: left">
                                            <div class="form-group">
                                                <label for="Seguro" class="col-lg-2 control-label"></label>
                                                <div class="col-lg-10">
                                                    <asp:HiddenField ID="hdnCantidad" runat="server" />
                                                    <asp:HiddenField ID="hdnAsociacionID" runat="server" />
                                                    <asp:HiddenField ID="hdnProductoID" runat="server" />
                                                    <asp:HiddenField ID="hdnCiaSeguro" runat="server" />
                                                    <asp:HiddenField ID="hdnInstitucionEducativa" runat="server" />
                                                    <asp:HiddenField ID="hdnCodigo" runat="server" />
                                                    <asp:HiddenField ID="hdnIDCliente1" runat="server" />                                                 

                                                    <asp:HiddenField ID="hdnTipoCargaCodigo" runat="server" />

                                                    <asp:Button runat="server" class="form-control" Text="Buscar" ID="btnBuscar" CssClass="button" 
                                                        OnClick="btnBuscar_Click" ></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                           </div>
                   </div>
          </div>
  </div>
  </div>               
      <div>
     <%--      <div class="panel-group" id="accordion2">
                    <div class="Panel with panel-info class">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2">Resultados</a>
                            </h4>
                        </div>--%>

                    <div class="panel-group" id="accordion2">
                      <div class="panel panel-primary">
                            <div class="panel-heading" style="background-color:#FF8000">
                                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color:white">Resultados</a>
                            </div>
                      <div class="panel-body">       
                            <div id="collapse2" class="panel-collapse collapse in">                            
                                <div class="panel-body">
                                <asp:Panel ID="pBodySeccion2" runat="server" Width="100%" CssClass="wrap" >
                        
                                    <div style="width: 100%">
                                        <div class="form-group">
                                            <label for="RazonSocial" class="col-lg-2 control-label">Razón Social:</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtRznSocial" placeholder="Ingrese Razón Social" >&nbsp;
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="NombreCorto" class="col-lg-2 control-label">Nombre Corto:</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtNombreCorto" placeholder="Ingrese Nombre corto" >
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="Seguro" class="col-lg-2 control-label">CIA de Seguros</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtCiaSeguro" placeholder="Ingrese Compañia de seguro" >
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="TipoSeguro" class="col-lg-2 control-label">Tipo de Seguro</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtTipoSeguro" placeholder="Ingrese Tipo de Seguro" > 
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="NumeroCodigo" class="col-lg-2 control-label">Números de Codigo</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtNumerosCodigos" 
                                                    placeholder="Ingrese Numeros de codigos"  type="number"> 
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="Prima" class="col-lg-2 control-label">Prima</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtPrima" placeholder="Ingrese Prima"  type="number"> 
                                                
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="Prima" class="col-lg-2 control-label">Descripción</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox runat="server" class="form-control" ID="txtDescripcion" placeholder="Ingrese Descripcion" > 
                                                
                                                </asp:TextBox>
                                            </div>
                                       </div>                                              

                                        <div class="form-group">
                                            <label for="Prima" class="col-lg-2 control-label">&nbsp;</label>
                                            <div class="col-lg-4">
                                                     <label for="Prima" class="col-lg-2 control-label">&nbsp; &nbsp;&nbsp;</label>
                                            </div>
                                       </div>                                              

                                        <div class="form-group">   
                                                 <label for="Prima" class="col-lg-2 control-label">  </label>                                                                                                           
                                                 <div class="col-lg-10">&nbsp;
                                                     
                                                                    <asp:Button runat="server" class="form-control" Text="Generar Codigos" ID="btnGenerador" CssClass="button" 
                                                                    OnClick="btnGenerador_Click" style="background-color:orangered"></asp:Button>
                                                            <asp:Button ID="btnEliminar" runat="server" class="form-control" CssClass="button"  Text="Eliminar Codigos" OnClick="btnEliminar_Click" />
                                                            </div>                                                     
                                             </div>                                       
                                        </div>                                                                                                              
                                </asp:Panel>
                        </div>
                    </div>
                </div>
                </div>
                </div>              
                <div>
                    </div>
          
                   
             <div class="panel-group" id="accordion3">
                      <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion3" href="#collapse3" style="color:white">Resumen</a>
                            </div>
                            <div class="panel-body">       
                        <div id="collapse3" class="panel-collapse collapse in">
                                <div class="panel-body">
                            <asp:Panel ID="pBodySeccion3" runat="server" Width="100%" CssClass="wrap">

                                <asp:GridView ID="grvBancos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid"
                                    PageSize="14" Width="99%" OnRowCommand="grvBancos_RowCommand" OnPageIndexChanging="grvBancos_PageIndexChanging"  >
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                        <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                        <%--      <asp:TemplateField HeaderText="Cuentas"  HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                        <p style="text-align:center">
                                                            <img alt="" src="Images/abrir.png" orderid2="<%# Eval("id") %>"  height="16px" width="16px"/>
                                                        </p>
                                                </ItemTemplate>
                                                  <HeaderStyle Width="10px" />
                                                </asp:TemplateField>--%>

                                        <asp:BoundField DataField="CodigoAfiliacion" HeaderText="Codigo" ReadOnly="True" SortExpression="CodigoAfiliacion"
                                                HeaderStyle-Width="16px"><HeaderStyle Width="16px" /></asp:BoundField>

                                      
                                        <asp:BoundField DataField="InstitucionEducativa" HeaderText="Institución Educativa" SortExpression="InstitucionEducativa" />
                                        <asp:BoundField DataField="TipoSeguro" HeaderText="Tipo de Seguro" SortExpression="TipoSeguro" />
                                        <asp:BoundField DataField="CiaSeguros" HeaderText="CiaSeguro" SortExpression="CiaSeguro" />
                                       <asp:BoundField DataField="Simbolo" HeaderText="Moneda" SortExpression="Simbolo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Prima" HeaderText="Prima" SortExpression="Prima" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad"  ItemStyle-HorizontalAlign="Right" ItemStyle-ForeColor="Green"/>
                                          <asp:BoundField DataField="FechaGeneracion" HeaderText="Fecha Generación" SortExpression="FechaGeneracion"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false"  ItemStyle-HorizontalAlign="Right"/>
                                        <asp:TemplateField HeaderText="Ver"><ItemTemplate><asp:ImageButton ID="btnVerCodigos" runat="server"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="VerCodigos" data-toggle="tooltip" Height="16px"
                                                    ImageUrl="~/Images/VerCodigos.png" title="Ver Codigos" Width="16px" /></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="61px" /></asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        </div>
                   </div>
                </div>
                </div>
            </div>
              </div>
              
          </asp:View>        

    <asp:View runat="server" ID="vwCodigos">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <div class="container-fluid">
                        <div class="row">
                          <div class="panel panel-primary">
                            <div class="panel-heading">Códigos Generados</div>
                            <div class="panel-body">                                                                              
                                <div>   Texto de busqueda   <div>
                                    <div>
                                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 80%"></asp:TextBox>
                                    <asp:Button ID="BtnBuscarCodigo" runat="server" CssClass="button" Text="Buscar" OnClick="BtnBuscarCodigo_Click" />
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="button" Style="background-color: orangered" Text="Cancelar" OnClick="btnCancelar_Click" />
                                        </div>
                              </div>
                            </div>
                        </div>
                                                            
                    <div style="padding: 10px;">
                        <asp:UpdatePanel  runat="server"  id="updDatosCodigos" UpdateMode="Conditional">
                            <Triggers><asp:AsyncPostBackTrigger ControlID="grvCodigos"  EventName="PageIndexChanging"/></Triggers>
                            <ContentTemplate>
                            <asp:GridView ID="grvCodigos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid"   PageSize="14" Width="100%" 
                                OnPageIndexChanging="grvCodigos_PageIndexChanging" OnSelectedIndexChanged="grvCodigos_SelectedIndexChanged"
                                EnableSortingAndPagingCallbacks="true" >
                            <Columns>
                                
                                
                                <asp:BoundField DataField="CodigoId" HeaderText="Id" SortExpression="Codigoid"  ItemStyle-Width="120px">
                                    <ItemStyle CssClass="oculto" />
                                    <HeaderStyle CssClass="oculto" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Codigo" HeaderText="Código de Pago" SortExpression="Codigo"  ItemStyle-Width="120px"/>
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"   ItemStyle-Width="120px"/>
                                 <asp:TemplateField HeaderText="Recibido Banco"><ItemTemplate><asp:Label runat="server" ID="lblRecibido" Text='<% # Convert.ToBoolean( Eval("RecibidoBanco")) == true ?"SI" :"NO"%>'  Width="100px" ></asp:Label></ItemTemplate><ItemStyle HorizontalAlign="Center" Width="61px" /></asp:TemplateField>                                
                                <asp:BoundField DataField="Asegurado" HeaderText="Asegurado" SortExpression="Asegurado" />
                                <asp:BoundField DataField="Beneficiario" HeaderText="Beneficiario" SortExpression="Beneficiario" />
                            </Columns>
                        </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                </div>     
            </div>   
          </div>
          </div>
        
        </asp:View>        
    </asp:MultiView>
      </ContentTemplate>

    </asp:UpdatePanel>     
  <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:UpdatePanel runat="server" ID="updmessage">
                                <ContentTemplate>
                                       <asp:Label Text="" ID="txtmensaje" runat="server" Style="font-size: 18px"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button id="btnCerrar" text="Cerrar" runat="server" CssClass="button" Style="background-color:orangered" />                    </div>

                </div>
            </div>
   </div>    
                     
       <div id="pnlEliminarCodigos" class="modal fade" role="dialog">
            <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                            <div class="modal-header" style="background-color:#D6EAF8">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title" style="background-color:#D6EAF8" >Eliminar Codigos por Rango</h4>
                            </div>
                            <div class="modal-body" >
                                     <div class="container-fluid">
                                     <div class="form-group">
                                      <label for="ejemplo_email_3" class="col-lg-2 control-label">Desde</label>
                                      <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" id="TxtDesde"   placeholder="Numero de Inicio"  Type="number"/>
                                      </div>
                                  </div>
                                  <div class="form-group">
                                      <label for="ApellidoMaterno" class="col-lg-2 control-label">Hasta</label>
                                      <div class="col-lg-10">
                                           <asp:TextBox runat="server" class="form-control" id="TxtHasta"    placeholder="Numero de Fin" Type="number"/>
                                      </div>
                                      
                                          </div>
                                         </div>                                
                            <div class="modal-footer">
                                    <%--<asp:Button   class="btn btn-facebook" runat="server"  ID="btnActualizar" Text="Guardar" OnClick="btnActualizar_Click"  />--%>
                                    <asp:Button ID="btnEliminarCodigos" runat="server" Text="Eliminar"  class="button" OnClick="btnEliminarCodigos_Click" />                                    
                                     <asp:Button  runat="server" ID="btncerrarGenerar" Text="Cerrar" CssClass="button"  Style="background-color:orangered"   />                                                    
                            </div>
                  </div>      
               </div>                    
            </div>
    </div>  

</asp:Content>
