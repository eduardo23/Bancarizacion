<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" 
        CodeBehind ="ListaTiposSeguro.aspx.cs" Inherits="Demo.ListaTiposSeguro" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
    <script>
          function openEditarProducto() {
              $('#pnlEditarProducto').modal('show');
     }
    </script>
   <script>
       function openModal() {
          $('#myModal').modal('show');
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
    </script>
    <script>    
     $(function () {  
         $("[id*=grvBancos] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });  
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
                            <div class="panel-group">
                            <div class="panel panel-default">
                                    <div class="panel-heading" style="color:white"><B>Tipo de Seguro: Criterios de busqueda</B></div>
                                    <div class="panel-body">                            
                                    <div class="row">
                                    <div style="padding: 2px;">
                                        Texto de busqueda
                                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%"></asp:TextBox>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="button" OnClick="btnBuscar_Click" Text="Buscar" />
                                        <asp:Button ID="btnNuevo" runat="server" CssClass="button" OnClick="btnNuevo_Click" Style="background-color: orangered" Text="Nuevo" />
                                    </div>
                                    <div style="padding: 2px;">
                                        <div>
                                            Resultados
                                        </div>
                                        <asp:GridView ID="grvBancos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid"  OnRowCommand="grvBancos_RowCommand" Width="100%" OnPageIndexChanging="grvBancos_PageIndexChanging1" >
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="CÓDIGO" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                <asp:TemplateField HeaderText="Situación" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Convert.ToBoolean( Eval("Estado")) == true ? "ACTIVO":"NO ACTIVO"   %>' ID="lblestado">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaTipoSeguro" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar Tipo de Seguro" Width="16px" />
                                                        <asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaTipoSeguro" data-toggle="tooltip" Height="18px" ImageUrl="~/Images/deletes.png" title="Anular Tipo de Seguro" Width="18px"  visible='<%# Convert.ToBoolean(Eval("Estado")) ? true :false   %>' />
                                                        <asp:ImageButton ID="btnActivar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="ActivaTipoSeguro" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoActivo.png" title="Activar seguro" visible='<%# Convert.ToBoolean(Eval("Estado")) ? false : true   %>' Width="16px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="61px" />
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
        
        <div id="myModal" class="modal fade" role="dialog" >
            <div class="modal-dialog" style="width:80%">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Tipo Seguro</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="txtmensaje" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button class="btn btn-facebook" runat="server" ID="btnSalir" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div id="pnlEditarProducto" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Contacto</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="Nombre">
                            <label for="Nombre" class="col-lg-2 control-label">Nombre</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombre" placeholder="Ingrese Nombre" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-2 control-label">Descripción</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtDescripcion" placeholder="Ingrese Descripción" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button class="btn btn-facebook" runat="server" ID="btnRegistrarContact" Text="Registrar" OnClick="btnRegistrarContact_Click1" />
                        <asp:Button class="btn btn-soundcloud" runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click1" />
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
                                <asp:Button Text="Confirmar" ID="btnConfirmar" runat="server" class="btn btn-default" OnClick="btnConfirmar_Click"  />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </div>
    <asp:HiddenField ID="hdnidTipoSeguro" runat="server" />
       <asp:HiddenField ID="hdnEstado" runat="server" />
</asp:Content>