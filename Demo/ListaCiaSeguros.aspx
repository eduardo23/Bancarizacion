<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ListaCiaSeguros.aspx.cs" Inherits="Demo.ListaCiaSeguros" %>
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
            $('#<%=grvCiaSeguros.ClientID %> img').click(function () {
                var img = $(this)
                var orderid = $(this).attr('orderid');
                var tr = $('#<%=grvCiaSeguros.ClientID %> tr[orderid =' + orderid + ']')
                tr.toggle();
                if (tr.is(':visible'))
                    img.attr('src', 'images/hidencontacts.png');
                else
                    img.attr('src', 'images/ViewContacts.png');
            });
        });

    </script>

     <script type="text/javascript">
        $(function () {
            $('#<%=grvCiaSeguros.ClientID %> img').click(function () {
                var img2 = $(this)
                var orderid2 = $(this).attr('orderid2');
                var tr2 = $('#<%=grvCiaSeguros.ClientID %> tr[orderid2 =' + orderid2 + ']')
                tr2.toggle();
                if (tr2.is(':visible'))
                    img2.attr('src', 'images/hidencontacts.png');
                else
                    img2.attr('src', 'images/ViewContacts.png');
            });
        });
    </script>
    <script>    
     $(function () {  
         $("[id*=grvCiaSeguros] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });  
 </script>  
<%--    

    <script type="text/javascript">
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
            <%--select: function (e, i) {
                $("#<%=pkSeguros.ClientID %>").val(i.item.val);
            },
            minLength: 1
        });
    });
</script>--%>
     <script>
         function openEditarTipoSeguro() {
           $('#pnlRegistrarTrama').modal('show');
     }
    </script>
   <script>
             function openModal() {
           $('#pnlMensaje').modal('show');
     }
    </script>

    <style type="text/css">      
        .auto-style3 {
            background-color: #1C2833;
            color: white;
            padding: 4px;
            height: 120px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:MultiView ID="MVSeguros" runat="server" ActiveViewIndex="0">
             <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                        <div class="content-header">
                        <div class="container-fluid">
                         
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    Compañías de Seguros
                                </div>
                             <div class="panel-body">
                                    
                          <div  style="padding:2px;">
                          Buscar:
                          <asp:TextBox runat="server" ID="txtBusqueda" CssClass="cajaTexto"  Style="width:80%"></asp:TextBox>
                          <asp:Button runat="server"  ID="btnNuevo" Text="Nuevo" CssClass="button"  Style="background-color:orangered" OnClick="btnNuevo_Click" />
                      </div>
                    <div class="row">
                    <div class= "col-xs-12 col-sm-12 col-lg-12" style="padding:10px;">
                        <div>
                            Resultados
                        </div>
                        <asp:GridView ID="grvCiaSeguros" runat="server" CssClass="Grid" AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="10" OnPageIndexChanging="grvCiaSeguros_PageIndexChanging" OnRowDataBound="grvCiaSeguros_RowDataBound" OnRowCommand="grvCiaSeguros_RowCommand" >
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <div style="text-align:center">
                                                <img alt="" src="Images/ViewContacts.png" orderid="<%# Eval("id") %>" height="16px" width="16px" />                                
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="id"  HeaderText="Codigo"  SortExpression="Codigo" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre Compañía" SortExpression="NombreCIA" />
                                <asp:BoundField DataField="Ruc" HeaderText="RUC" SortExpression="Ruc" />
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" />
                                <asp:TemplateField HeaderText="Situación"><ItemTemplate>
                                    <asp:Label ID="lblestado" runat="server" Text='<%# Convert.ToBoolean(Eval("Estado")) ? "ACTIVO" : "NO ACTIVO"   %>'></asp:Label>
                                   </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Tipo de Seguros"  ItemStyle-Width="20px" >
                                    <ItemTemplate>
                                        <div style="text-align:center;vertical-align:middle">
                                                <asp:ImageButton ID="btnTipo" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Agregar" 
                                                ImageUrl="~/Images/ViewContacts.png" Width="16px" Height="16px" ToolTip="Agregar tipo seguro" data-toggle="tooltip" title="Agregar tipos seguro" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Bancos"   ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate ><img alt="" src="Images/ViewContacts.png" orderid2="<%# Eval("id") %>" height="16px" width="16px" /></ItemTemplate></asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/icoEditar.png" Width="16px" Height="16px" data-toggle="tooltip" title="Editar Cia Seguro" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"   CommandName="Edita" />
                                        <asp:ImageButton ID="btnAnular" runat="server"  ImageUrl="~/Images/deletes.png" Width="20px" Height="20px"  data-toggle="tooltip" title="Eliminar Cia Seguro" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"   CommandName="Anula" /></ItemTemplate></asp:TemplateField>
                                         <asp:BoundField DataField="Id"  HeaderText="id" SortExpression="Id" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr style="display:none;" orderid="<%# Eval("id") %>">
                                            <td colspan="100%">
                                                <div style="position:relative;left:25px;">
                                                <asp:GridView ID="grvTipoSeguro" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                                              CellPadding ="3" ForeColor="Black" GridLines="Vertical" OnRowCommand="grvTipoSeguro_RowCommand" ShowHeaderWhenEmpty="true" Width="80%" EmptyDataText="No hay Tipo de Seguros registrados " CssClass="Grid2">
                                                    <Columns>
                                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" InsertVisible ="false" ReadOnly="true" SortExpression="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
                                                        <asp:BoundField DataField ="Seguro" HeaderText="Seguro" SortExpression ="Seguro" />
                                                        <asp:BoundField DataField ="Tipo" HeaderText="Tipo" SortExpression ="Tipo" />
                                                        <asp:BoundField DataField ="Trama" HeaderText="Trama" SortExpression ="Trama" />
                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaTipoSeguro" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar tipo seguro" Width="16px" />
                                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaTipoSeguro" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="Eliminar tipo seguro" Width="16px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                  </asp:GridView>
                                                  </div>
                                             </td>
                                          </tr>
                                       </ItemTemplate>
                                   </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr style="display:none;" orderid2="<%# Eval("id") %>">
                                            <td colspan="100%">
                                                <div style="position:relative;left:25px;">
                                                    <asp:GridView ID="grvTipoCuenta" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"  CssClass="Grid2"
                                                                  CellPadding ="3" ForeColor="Black" GridLines="Vertical" ShowHeaderWhenEmpty="true" EmptyDataText="No hay Tipo de Bancos registrados ">
                                                        <Columns>
                                                            <asp:BoundField DataField ="Banco" HeaderText="Banco" SortExpression ="Banco" />
                                                            <asp:BoundField DataField ="Moneda" HeaderText="Moneda" SortExpression ="Moneda" />
                                                            <asp:BoundField DataField ="Numero" HeaderText="Numero" SortExpression ="Numero" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>
                       </div>
                    </div>
                </div>
               </div>          
             </div>
           </asp:View>
          
          <asp:View ID="VwDatosCompania" runat="server">
              <div>
                   <div class="myContent">
                <div class="content-header">
                 <div class="container-fluid">
                               <div class="panel panel-primary">
                                            <div class="panel-heading">
                                    <span class="fa-inverse"></span>
                                  Datos de la Compañía de Seguros
                              </div>
                              <div class="panel-body">
                                        
                    <div class="form-group">
                        <label for="RazonSocial" class="col-lg-3 control-label">Razón Social</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtRazonSocial" ToolTip="Seleccione razon social"  data-toggle="tooltip" data-placement="left"      placeholder="razon social" required="true" Width="380px"/>
                            </div>
                    </div>

                     <div class="form-group">
                        <label for="Ruc" class="col-lg-3 control-label">RUC</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtRuc" ToolTip="Seleccione ruc"  data-toggle="tooltip" data-placement="left"
                                             placeholder="ruc" required="true" Width="380px"/>
                            </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="Direccion" class="col-lg-3 control-label">Dirección</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="txtDireccion" ToolTip="Seleccione dirección"  data-toggle="tooltip" data-placement="left"
                                             placeholder="direccion" required="true"  Width="380px"/>
                            </div>
                    </div>

                                  <div class="form-group">
                                      <label for="LogoBanco" class="col-lg-3 control-label">Logo Banco</label>
                                      <div class="col-lg-18">
                                        &nbsp;&nbsp;  &nbsp;&nbsp;   <asp:FileUpload runat="server" ID="file_url" onchange="mostrarImagen(this);" />
                                      </div>                                       
                                  </div>

                                  <div class="form-group">
                                      <label for="departamento" class="col-lg-3 control-label">LogoTipo</label>
                                      <div class="col-lg-9">
                                          <asp:Image ID="img_destinoCia" runat="server" Style="width: 200px" />
                                      </div>
                                  </div>

                                  <div class="form-group">
                                      <label for="departamento" class="col-lg-3 control-label">Departamento: </label>
                                      <div class="col-lg-9">
                                          <asp:DropDownList ID="DDLDepartamento" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control" placeholder="Departamento" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged" Width="380px">
                                              <asp:ListItem Selected="True" Value="0">Seleccione Departamento</asp:ListItem>
                                          </asp:DropDownList>
                                      </div>
                                  </div>
                      
                    <div class="form-group">
                        <label for="provincia" class="col-lg-3 control-label">Provincia: </label>
                            <div class="col-lg-9">
                                <asp:DropDownList runat="server" class="form-control" id="DDLProvincia" placeholder="Provincia"  AutoPostBack="True" OnSelectedIndexChanged="DDLProvincia_SelectedIndexChanged" Width="380px">
                                    <asp:ListItem Value="0">Seleccione Provincia</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="distrito" class="col-lg-3 control-label">Distrito: </label>
                            <div class="col-lg-9">
                                <asp:DropDownList runat="server" class="form-control" id="DDLDistrito" placeholder="Distrito"  Width="380px">
                                    <asp:ListItem Value="0">Seleccione Distrito</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                    </div>

                    <div class="form-group">
                        <label for="Empresa Recaudadora" class="col-lg-3 control-label">Empresa Recaudadora</label>
                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" class="form-control" ID="chkEmpRecaudadora"  width="180px"/>
                            </div>
                    </div>

                    <div class="form-group">

                        <label for="termino" class="col-lg-3 control-label">Termino y Condición * </label>
                            <div class="col-lg-9">
                                 <asp:TextBox runat="server" class="form-control" id="txtTerminoCondicion" data-toggle="tooltip" data-placement="left"
                                             placeholder="terminos y condiciones" required="true" Height="118px" Width="90%" />

                                 <ajaxToolkit:HtmlEditorExtender ID="txtTerminoCondicion_HtmlEditorExtender" runat="server" BehaviorID="txtTerminoCondicion_HtmlEditorExtender" TargetControlID="txtTerminoCondicion" EnableSanitization="false">
                                 </ajaxToolkit:HtmlEditorExtender>
                            </div>
                    </div>
                    </div>
                  </div>
                    <br />
                     <div class="container-fluid">
                        <div  style="background-color:lightgray;    background-color: #1C2833;  color: white;   padding: 4px;  height: 120px;">
                        <div  class="tablaCentrada" style="text-align:left;padding:20px">
                              &nbsp;  
                            <asp:Button  runat="server" ID="btnGuardarCiaSeguro" Text="Guardar" CssClass="button" Height="50px" Width="120px" OnClick="btnGuardarCiaSeguro_Click" /> 
                            <asp:Button  runat="server" ID="btnCancelarCiaSeguro" Text="Cancelar" CssClass="button" Height="50px" Width="120px" 
                                Style="background-color: orangered" CausesValidation="False" OnClick="btnCancelarCiaSeguro_Click" /> 
                        </div>
                     </div>
                </div>
                        </div>
                      </div>
                    </div>
                  </div>
              
               </asp:View>
             <asp:View ID="View3" runat="server">
                <asp:HiddenField ID="pkCiaseguro" runat="server" />
                 
            </asp:View>
        </asp:MultiView>

            <div id="pnlRegistrarTrama" class="modal fade" role="dialog">
            <div class="modal-dialog" style="width:45%" >
                    <!-- Modal content-->
                    <div class="modal-content">
                            <div class="modal-header" style="background-color: #D6EAF8">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Registrar Trama</h4>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">
                                    <div class="form-group">
                                        <label for="Ciaseguro" class="col-lg-3 control-label">CIA Seguro </label>
                                        <div class="col-lg-9">
                                            <asp:TextBox runat="server" class="form-control" ID="txtTipoSeguro" placeholder="Ingrese Cia Seguro"  Width="380px"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="TipoSeguro" class="col-lg-3 control-label">Tipo Seguro </label>
                                        <div class="col-lg-9">
                                            <asp:DropDownList  runat="server" class="form-control" id="DDLTipoSeguro">
                                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="Trama" class="col-lg-3 control-label">Trama </label>
                                        <div class="col-lg-9">
                                            <asp:DropDownList  runat="server" class="form-control" id="DDLTrama" >
                                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button class="btn btn-facebook" runat="server" ID="btnRegistrarTipoSeguro" Text="Registrar" />
                                        <asp:Button class="btn btn-soundcloud" runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>                   
                              </div>                    
                    </div>
            </div>
        </div>
        </div>


           <div id="myConfirm" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #D6EAF8">
                                <button type="button" class="close" data-dismiss="modal" >&times;</button>
                                <h4 class="modal-title"><asp:Label Text="" ID="lblTitleConfirm" runat="server" Style="font-size: 18px"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="msgcentrado">
                                    <asp:Label Text="" ID="lblmsgConfirm" runat="server" Style="font-size: 18px"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button Text="Confirmar" ID="btnConfirmar" runat="server" class="btn btn-default" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
    <asp:HiddenField ID="hdnIdCompañiaSeguro" runat="server" />
    <asp:HiddenField ID="hdnEstado" runat="server" />
    </asp:content>