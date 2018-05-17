<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Demo.Usuarios" %>

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
         $("[id*=grvUsuarios] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });  
 </script>  
    <script type="text/javascript">
     <%--       $(document).ready(function () {
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
            },--%>
            <%--select: function (e, i) {
                $("#<%=pkSeguros.ClientID %>").val(i.item.val);
            },--%>
            minLength: 1
        });
    });
</script>
     <script>
         function openEditarUsuario() {
           $('#pnlEditarUsuario').modal('show');
     }
    </script>
         <script>
             function openModal() {
           $('#pnlMensaje').modal('show');
     }
    </script>

    <style type="text/css">
        .auto-style1 {
            left: -260px;
            top: -450px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:MultiView ID="MVUsuarios" runat="server" ActiveViewIndex="0">
             <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                    <div class="content-header">
                    <div class="container-fluid">
                    <div class="row">              
                    </div>
                    <div class="row">
                      <div class="panel panel-primary">
                            <div class="panel-heading">
                                    Usuario: Criterios de Búsqueda 
                            </div>
                      <div class="panel-body">       
                                           
                      <div class= "auto-style1" style="padding:10px;">
                          Buscar:
                          <asp:TextBox runat="server" ID="txtBusqueda" CssClass="cajaTexto"  Style="width:80%"></asp:TextBox>
                          <asp:Button ID="btnBuscar" runat="server" CssClass="button"  Text="Buscar" OnClick="btnBuscar_Click"  Style="background-color:orangered"/>
                      </div>
                    <div class="row">
                    <div class= "col-xs-12 col-sm-12 col-lg-12" style="padding:10px;">
                        <div>
                            Resultados
                        </div>
                       <%-- <asp:UpdatePanel runat="server" ID="updDatos">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>--%>
                            
                            <asp:GridView ID="grvUsuarios" runat="server" CssClass="Grid" AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="14" OnPageIndexChanging="grvUsuarios_PageIndexChanging" OnRowCommand="grvUsuarios_RowCommand" >
                            <Columns>                               
                                <asp:BoundField DataField="Id"  HeaderText="ID"  SortExpression="Id" />
                                <asp:BoundField DataField="Cliente" HeaderText="CLIENTE" SortExpression="Cliente" />
                                <asp:BoundField DataField="Dni" HeaderText="NÚMERO DE DOCUMENTO" SortExpression="Dni" />
                                <asp:BoundField DataField="Email" HeaderText="EMAIL" SortExpression="Email" />                    
                                <asp:BoundField DataField="Rol" HeaderText="ROL" SortExpression="Rol" />                    
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/Images/icoEditar.png" Width="16px" Height="16px" data-toggle="tooltip" title="Editar Usuario" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edita" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id"  HeaderText="id" SortExpression="Id" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="TipoDocumento"  HeaderText="Tipo Documento" SortExpression="Tipo Documento" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="Nombre"  HeaderText="Nombre" SortExpression="Nombre" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField> 
                                <asp:BoundField DataField="Paterno"  HeaderText="Apellido Parterno" SortExpression="Apellido Paterno" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Materno"  HeaderText="Apellido Materno" SortExpression="Apellido Materno" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="RoleId"  HeaderText="RoleId" SortExpression="RoleId" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField>                                
                                <asp:BoundField DataField="IdUser"  HeaderText="IdUser" SortExpression="IdUser" >
                                    <HeaderStyle CssClass="oculto" /><ItemStyle CssClass="oculto" />
                                </asp:BoundField>                                
                            </Columns>
                        </asp:GridView>
                             <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        </div>
                        </div>
                    </div>
                          
                                   </div>
                          </div>
                   </div>
                </div>
                </div>    
            </asp:View> 
                     
            <asp:View ID="View3" runat="server">
                <asp:HiddenField ID="pkUsuario" runat="server" />                
            </asp:View>
        </asp:MultiView>

            <div id="pnlEditarUsuario" class="modal fade" role="dialog">
            <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                            <div class="modal-header" style="background-color: #D6EAF8">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Editar Usuario</h4>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">
                                    <div class="form-group">
                                        <label for="RazonSocial" class="col-lg-4 control-label">Razón Social *</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" class="form-control" ID="txtRazonSocial" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ApellidoPaterno" class="col-lg-4 control-label">Apellido Paterno *</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" class="form-control" ID="txtApellidoPaterno" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ApellidoMaterno" class="col-lg-4 control-label">Apellido Materno *</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" class="form-control" ID="txtApellidoMaterno" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="Email" class="col-lg-4 control-label">Email *</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" class="form-control" ID="txtEmail" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="TipoDocumento" class="col-lg-4 control-label">Tipo Documento *</label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList  runat="server" class="form-control" id="DDLTipoDocumento">
                                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="Rol" class="col-lg-4 control-label">Rol *</label>
                                        <div class="col-lg-8">
                                            <asp:DropDownList  runat="server" class="form-control" id="DDLRol" >
                                                <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="NumeroDocumento" class="col-lg-4 control-label">Número Documento *</label>
                                        <div class="col-lg-8">
                                            <asp:TextBox runat="server" class="form-control" ID="txtNroDocumento" />
                                        </div>
                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button class="button" runat="server" ID="btnEditarUsuario" Text="Editar" OnClick="btnEditarUsuario_Click" />
                                        <asp:Button class="button" runat="server" ID="btnSalir" Text="Cancelar" OnClick="btnSalir_Click" Style="background-color:orangered" />
                                    </div>                   
                              </div>                    
                    </div>
            </div>
        </div>

       
        <div id="pnlMensaje" class="modal fade" role="dialog">
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
                        <asp:Button class="btn btn-soundcloud" runat="server" ID="Button1" Text="Cancelar" />
                    </div>
                </div>
            </div>
        </div>
    </asp:content>
