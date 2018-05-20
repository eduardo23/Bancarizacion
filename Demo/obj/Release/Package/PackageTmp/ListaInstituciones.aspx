<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ListaInstituciones.aspx.cs" Inherits="Demo.ListaInstituciones" %>
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
   function openModalIE() {
     $('#iexist').modal('show');
  }
</script>
    <script>
    $(document).ready(function () {
        $(window).load(function () {           
           $("#<%= txtApellidoPaterno.ClientID %>").attr("disabled", "disabled");
           $("#<%=txtApellidoMaterno.ClientID%>").attr("disabled", "disabled");
           $("#<%=txtNombre.ClientID %>").attr("disabled", "disabled");
        });
        $(this).load(function () {
            $("#<%= txtApellidoPaterno.ClientID %>").attr("disabled", "disabled");
           $("#<%=txtApellidoMaterno.ClientID%>").attr("disabled", "disabled");
           $("#<%=txtNombre.ClientID %>").attr("disabled", "disabled");
        });
        $(document).load(function () {
             $("#<%= txtApellidoPaterno.ClientID %>").attr("disabled", "disabled");
           $("#<%=txtApellidoMaterno.ClientID%>").attr("disabled", "disabled");
           $("#<%=txtNombre.ClientID %>").attr("disabled", "disabled");
        });
    })
</script>  
    <script type="text/javascript">  
     $(function () {  
         $("[id*=grvSeguros] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });  
 </script>
 <script>
    $(document).ready(function () {
    $('#<%= rbtTipoEmpresa.ClientID %> input').change(function () {    
       
       if ($(this).val() == 2) {        
           $("#<%=txtApellidoPaterno.ClientID %>").val('');
           $("#<%=txtApellidoMaterno.ClientID %>").val('');
           $("#<%=txtNombre.ClientID %>").val('');
           $("#<%=txtRazonSocial.ClientID %>").val('');
            $("#<%=txtnombrocorto.ClientID %>").val('');

           $("#<%= txtApellidoPaterno.ClientID %>").attr("disabled", "disabled");
           $("#<%=txtApellidoMaterno.ClientID%>").attr("disabled", "disabled");
           $("#<%=txtNombre.ClientID %>").attr("disabled", "disabled");
           $("#<%=txtRazonSocial.ClientID %>").removeAttr("disabled");
           $("#<%=txtnombrocorto.ClientID %>").removeAttr("disabled");
       } else if ($(this).val() == 1) {
          $("#<%=txtApellidoPaterno.ClientID %>").val('');
           $("#<%=txtApellidoMaterno.ClientID %>").val('');
           $("#<%=txtNombre.ClientID %>").val('');
           $("#<%=txtRazonSocial.ClientID %>").val('');
            $("#<%=txtnombrocorto.ClientID %>").val('');
           
            $("#<%= txtApellidoPaterno.ClientID %>").removeAttr("disabled");
           $("#<%=txtApellidoMaterno.ClientID%>").removeAttr("disabled");
            $("#<%=txtNombre.ClientID %>").removeAttr("disabled");
           $("#<%=txtRazonSocial.ClientID %>").attr("disabled", "disabled");
            $("#<%=txtnombrocorto.ClientID %>").attr("disabled", "disabled");
            
                   

     }
    
     
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
    
 
    <script type="text/javascript">
        $(function () {
            $('#<%=grvSeguros.ClientID %> img').click(function () {                
                var img = $(this)     
                var orderid = $(this).attr('orderid');             
                var tr = $('#<%=grvSeguros.ClientID %> tr[orderid =' + orderid + ']')           
                tr.toggle();                
                if (tr.is(':visible'))
                    img.attr('src', 'Images/hidencontacts.png');
                else
                    img.attr('src', 'Images/ViewContacts.png');
            });
        });
    </script>
   <script>
       $(document).ready(function () {
            varid = $("#<%=DDLTipoDocumento.ClientID %>").on('change', function () {
        if ($(varid).val() == 1) {
           $("#<%=txtNumeroDocumento.ClientID %>").val('');
          $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 8);
           
        } else if ($(varid).val() == 4) {
             $("#<%=txtNumeroDocumento.ClientID %>").val('');
      $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 11);
        } else if ($(varid).val() == 5) {
             $("#<%=txtNumeroDocumento.ClientID %>").val('');
        $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 9);
        } else {
             $("#<%=txtNumeroDocumento.ClientID %>").val('');
                 $("#<%=txtNumeroDocumento.ClientID %>").attr('maxlength', 50);
        }
}); 
       });
       function validNumericos(evt) {

           if ($(varid).val() == 1) {

               var charCode = (evt.which) ? evt.which : event.keyCode
               if (((charCode == 8) || (charCode == 46)
               || (charCode >= 35 && charCode <= 40)
                   || (charCode >= 48 && charCode <= 57)
                   || (charCode >= 96 && charCode <= 105))) {

                   return true;
               }
               else {
                   return false;
               }
           } else if ($(varid).val() == 4) {

               var charCode = (evt.which) ? evt.which : event.keyCode
               if (((charCode == 8) || (charCode == 46)
               || (charCode >= 35 && charCode <= 40)
                   || (charCode >= 48 && charCode <= 57)
                   || (charCode >= 96 && charCode <= 105))) {

                   return true;
               }
               else {
                   return false;
               }
           }

       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>



    <div>
        <asp:MultiView ID="MVInstituciones" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <span class="fa-inverse"></span>
                                        <a data-parent="#accordion" data-toggle="collapse" href="#collapse1" style="color: white"><b>Institución Educativa</b>&nbsp; : Criterios de Búsqueda</a>
                                    </div>
                                    <div class="panel-body">
                                        <div style="padding: 2px;">
                                            Texto de busqueda
                                            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%; text-transform: uppercase;"></asp:TextBox> 
                                            <asp:Button ID="btnBuscar" runat="server" CssClass="button" OnClick="btnBuscar_Click" Text="Buscar" />
                                            <asp:Button ID="btnNuevo" runat="server" CssClass="button" OnClick="btnNuevo_Click" Style="background-color: orangered" Text="Nuevo" />
                                        </div>
                          <div style="padding:2px;">
                              <div>
                                  Resultados
                              </div>
                              <asp:GridView ID="grvSeguros" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="Grid" OnPageIndexChanging="grvSeguros_PageIndexChanging" OnRowCommand="grvSeguros_RowCommand" PageSize="20" Width="99%" OnRowDataBound="grvSeguros_RowDataBound" OnSelectedIndexChanged="grvSeguros_SelectedIndexChanged">
                                  <Columns>
                                      <asp:TemplateField HeaderText="Añadir" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnAgregar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AgregarContacto" data-toggle="tooltip" Height="32px" ImageUrl="~/Images/addContact.png" title="Agregar Contacto" ToolTip="Agregar Contacto" Width="32px" />   
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" />
                                      </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Ver">
                                          <ItemTemplate>                                              
                                              <img alt="" src="Images/ViewContacts.png" orderid="<%# Eval("id") %>"  height="16px" width="16px"/>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                                                          
                                      <asp:BoundField DataField="Codigo" HeaderText="Código Afiliación" SortExpression="Codigo" />
                                      <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" />
                                      <asp:BoundField DataField="NombreNatural" HeaderText="Nombre institución" SortExpression="Nombre" />
                                      <asp:BoundField DataField="TipoInstEdu" HeaderText="Tipo Institución" SortExpression="TipoInstEdu" />
                                      <asp:BoundField DataField="NumeroDocumento" HeaderText="Nº Documento" SortExpression="NumeroDocumento" />
                                      <asp:BoundField DataField="Direccion" HeaderText="Dirección" SortExpression="Direccion" />
                                      <asp:TemplateField HeaderText="Situación">
                                          <ItemTemplate>
                                              <asp:Label ID="lblestado" runat="server" Text='<%# Convert.ToBoolean(Eval("Activo")) ? "Activo" : "No Activo"   %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      
                                      <asp:TemplateField HeaderText="Acciones">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                  CommandName="Edita" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar institución educativa" 
                                                  Width="16px" OnClick="btnEditar_Click" />
                                              <asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                  CommandName="Anula" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="Eliminar institución educativa" 
                                                   visible='<%# Convert.ToBoolean(Eval("Activo")) ? true : false  %>' 
                                                  Width="16px" />
                                              <asp:ImageButton ID="btnActivar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                  CommandName="Activa" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoActivo.png" title="Activar institución educativa" 
                                                  visible='<%# Convert.ToBoolean(Eval("Activo")) ? false : true   %>' Width="16px" />
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:BoundField DataField="Id"  HeaderText="id" SortExpression="Id" >
                                      <HeaderStyle CssClass="oculto" />
                                      <ItemStyle CssClass="oculto" />
                                      </asp:BoundField>
                       <asp:TemplateField>
                        <ItemTemplate>
                           <tr  style="display:none;" orderid="<%# Eval("Id") %>">
                            <td colspan="100%">
                                <div style="position:relative;left:25px;">
                                    <asp:GridView ID="grvContactos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="80%" 
                                        OnRowCommand="grvContactos_RowCommand"   EmptyDataText="No hay contactos registrados "  CssClass="Grid2">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />   
                                            <asp:BoundField DataField="Contacto" HeaderText="Nombre" SortExpression="Nombre" />
                                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />        
                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />        
                                            <asp:BoundField DataField="ApellidoPaterno" HeaderText="ApellidoPaterno" SortExpression="ApellidoPaterno"  HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"/>        
                                            <asp:BoundField DataField="ApellidoMaterno" HeaderText="ApellidoMaterno" SortExpression="ApellidoMaterno"  HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"/>        
                                            <asp:BoundField DataField="InstitucionEducativaID" HeaderText="InstitucionEducativaID" SortExpression="InstitucionEducativaID"  ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>  

                                         <asp:TemplateField HeaderText="Acciones">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditaContacto" 
                                                  data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="Editar contacto" Width="16px" />
                                              <asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnulaContacto" 
                                                  data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="Anular contacto" Width="16px" />                                             
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
            <asp:View ID="VwNuevoInstitucion" runat="server">
                        <div>
                             <div class="panel panel-primary">
                            <div class="panel-heading">Datos de la Institución Educativa</div>
                            <div class="panel-body">                                                              
                                  <div class="form-group">
                                      <label for="CodigoAfiliacion" class="col-lg-2 control-label">Código de Afiliación: </label>
                                      <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" id="txtCodigoAfiliacion" ToolTip="Ingresa el codigo de afiliación"  data-toggle="tooltip" title="El codigo de afiliacion es obligatorio" data-placement="left"
                                              placeholder="Código de Afiliación" required="true" Width="350px"/>
                                      </div>
                                  </div>

                                  <div class="form-group">
                                      <label for="TipoInstitucion" class="col-lg-2 control-label">Tipo de Institucion: </label>
                                      <div class="col-lg-10" >
                                          <asp:DropDownList runat="server" class="form-control" ID="DDLTipoInstitucionEducativa" placeholder="TipoInstitucionEducativa" AppendDataBoundItems="True" Width="350px">
                                              <asp:ListItem Selected="True" Value="0">Seleccione Tipo  de Institución Educativa</asp:ListItem>
                                          </asp:DropDownList>
                                      </div>
                                  </div>

                                  <div class="form-group">
                                          <label for="RazonSocial" class="col-lg-2 control-label">Razón Social: </label>
                                          <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" ID="txtRazonSocial" placeholder="Razón Social" ToolTip="Razón social" data-toggle="tooltip" title="La razón social es obligatoria" data-placement="left" required="true"  Width="350px"/> 
                                      </div>
                                 </div>

                                  <div class="form-group">
                                      <label for="Tipoempresa" class="col-lg-2 control-label">Tipo de empresa: </label>
                                      <div class="col-lg-10">                                          
                                              <asp:RadioButtonList ID="rbtTipoEmpresa" runat="server" RepeatDirection="Horizontal" Width="350px">
                                                  <asp:ListItem Selected="True" Value="2">Juridica</asp:ListItem>
                                                  <asp:ListItem Value="1">Natural</asp:ListItem>
                                              </asp:RadioButtonList>
                                          </div>                                      
                                  </div>

                                      <div class="form-group">
                                          <label for="NombreCorto" class="col-lg-2 control-label"  data-toggle="tooltip" title="Nombre corto es obligatorio" data-placement="left" >Nombre Corto: </label>
                                          <div class="col-lg-10">
                                              <asp:TextBox runat="server" class="form-control" id="txtnombrocorto" placeholder="Nombro corto" required="true"  Width="350px"/>
                                          </div>
                                      </div>
                                                                          
                                        <div class="form-group">
                                          <label for="Tipo Documento" class="col-lg-2 control-label">Tipo de Documento:</label>
                                          <div class="col-lg-9">
                                               <asp:DropDownList  runat="server" class="form-control" id="DDLTipoDocumento" placeholder="Tipo de Documento" AppendDataBoundItems="True"   Width="350px">
                                                   <asp:ListItem Selected="True" Value="0">Seleccione Tipo  de Documento</asp:ListItem>
                                               </asp:DropDownList>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Número Documento" class="col-lg-2 control-label">Número Documento:</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox  runat="server" class="form-control" id="txtNumeroDocumento" placeholder="Número Documento" required="true"  Width="350px" onkeydown="return validNumericos(event)"/>
                                          </div>
                                      </div>

                                      <div class="form-group">
                                          <label for="Apellido Paterno" class="col-lg-2 control-label">Apellido Paterno: </label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtApellidoPaterno" placeholder="Apellido Paterno"  Width="350px"/>
                                          </div>
                                      </div>

                                         <div class="form-group">
                                          <label for="Apellido Materno" class="col-lg-2 control-label">Apellido Materno :</label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtApellidoMaterno" placeholder="Apellido Materno"   Width="350px"/>
                                          </div>
                                      </div>

                                   <div class="form-group">
                                          <label for="Nombre" class="col-lg-2 control-label">Nombre: </label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtNombre" placeholder="Nombre"  Width="350px"/>
                                          </div>
                                      </div>

                                       <div class="form-group">
                                          <label for="Dirección" class="col-lg-2 control-label">Dirección: </label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtDireccion" placeholder="Dirección"  required="true"  Width="350px"/>
                                          </div>
                                      </div>

                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                       <div class="form-group">
                                          <label for="" class="col-lg-2 control-label">Departamento: </label>
                                          <div class="col-lg-10">
                                               <asp:DropDownList runat="server" class="form-control" id="DDLDepartamento" placeholder="Departamento" AutoPostBack="True" OnSelectedIndexChanged="DDLDepartamento_SelectedIndexChanged" AppendDataBoundItems="True" Width="350px">
                                                     <asp:ListItem Value="0" Selected="True">Seleccione Departamento</asp:ListItem>
                                                </asp:DropDownList>
                                          </div>
                                      </div>
                                           

                                       <div class="form-group">
                                          <label for="" class="col-lg-2 control-label">Provincia: </label>
                                          <div class="col-lg-10">
                                               <asp:DropDownList runat="server" class="form-control" id="DDLProvincia" placeholder="Provincia" OnSelectedIndexChanged="DDLProvincia_SelectedIndexChanged" AutoPostBack="True" Width="350px">
                                                     <asp:ListItem Value="0">Seleccione Provincia</asp:ListItem>
                                                </asp:DropDownList>
                                          </div>
                                      </div>

                                  <div class="form-group">
                                      <label for="" class="col-lg-2 control-label">Distrito: </label>
                                      <div class="col-lg-10">
                                          <asp:DropDownList runat="server" class="form-control" ID="DDLDistrito" placeholder="Distrito" Width="350px">
                                              <asp:ListItem Value="0">Seleccione Distrito</asp:ListItem>
                                          </asp:DropDownList>
                                      </div>
                                  </div>
                                             </ContentTemplate>
                                      </asp:UpdatePanel> 

                                    <div class="form-group">
                                          <label for="Teléfono" class="col-lg-2 control-label">Teléfono: </label>
                                          <div class="col-lg-10">
                                               <asp:TextBox runat="server" class="form-control" id="txtTelefono" placeholder="Teléfono" required="true" Width="350px"/>
                                          </div>
                                   </div>
                                  
                                    <div class="form-group">
                                          <label for="FAX" class="col-lg-2 control-label">FAX: </label>                                         
                                          <div class="col-lg-10">
                                          <asp:TextBox runat="server" class="form-control" id="txtFAX" placeholder="FAX" Width="350px"/>
                                           </div>
                                        </div>    
                                           
                                  </div>
                                  </div>
                                 </div>                
                            <asp:HiddenField ID="hdnIdAsociacion" runat="server" />
                            <br /> 
                            <div class="container-fluid">
                            <div class="div_title" style="height:120px;background-color:lightgray">
                                   <div class="tablaCentrada" style="text-align:left;padding:20px">
                                                   &nbsp;
                                                   <asp:Button ID="btnGrabar" runat="server" CssClass="button" Height="50px" OnClick="btnGrabar_Click" Text="Guardar" Width="120px" />
                                                   <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" CssClass="button" formnovalidate="" Height="50px" OnClick="btnCancelar_Click" Style="background-color: orangered" Text="Cancelar" Width="120px" />
                                               </div>
                           </div>
                       </div>                                
            </asp:View>
            <asp:View ID="View3" runat="server">
                <asp:HiddenField ID="pkInstitucionEducativa" runat="server" />
            </asp:View>
        </asp:MultiView>


         <div id="myModal" class="modal fade" role="dialog">  
            <div class="modal-dialog">             
                     <div class="modal-content">
                                 <div class="modal-header" style="background-color: #D6EAF8">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>  
                                          <h4 class="modal-title">Hermes Seguros</h4>
                                </div>
                                <div class="modal-body">
                                            <div class="msgcentrado">
                                                    <asp:Label Text="" ID="txtmensaje" runat="server"  Style="font-size:18px"></asp:Label>
                                             </div>
                                </div>                   
                               <div class="modal-footer">
                                      <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                               </div>
                   </div>
         </div>
  </div>
 </div>              

        <div id="pnlEditarAlumno" class="modal fade" role="dialog"  >
            <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                          
                            <div class="modal-header" style="background-color: #D6EAF8">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Registrar Contacto</h4>
                            </div>
                            <div class="modal-body" >
                                <div class="container-fluid">
                                    <div class="form-group">
                                        <label for="ejemplo_email_3" class="col-lg-2 control-label">Ape.Paterno</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtApePateContact" placeholder="Ingrese Apellido Paterno" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ApellidoMaterno" class="col-lg-2 control-label">Ape.Materno</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtApeMateContact" placeholder="Ingrese Apellido Materno" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="Nombres" class="col-lg-2 control-label">Nombres</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="TxtNombresContact" placeholder="Ingrese Nombres" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="TipoDocumento" class="col-lg-2 control-label">Email</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="txtEmailContact" placeholder="Ingrese Email" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="NumeroDocumento" class="col-lg-2 control-label">Cargo</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="txtCargoContact" placeholder="Ingrese cargo" />
                                        </div>
                                    </div>

                                    <div class="form-group" style="display:none">
                                        <label for="institucionEducativaID" class="col-lg-2 control-label">institucionEducativaID</label>
                                        <div class="col-lg-10">
                                            <asp:TextBox runat="server" class="form-control" ID="txtinstitucionEducativaID" placeholder="institucionEducativaID" />
                                        </div>
                                    </div>

                               </div>
                                    <div class="modal-footer">
                                        <asp:Button class="btn btn-facebook" runat="server" ID="btnRegistrarContact" Text="Registrar" OnClick="btnRegistrarContact_Click1"  />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                    </div>                   
                              </div>                    
                 </div>
           </div>
     </div>

    <div id="pnlEditarContacto" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Editar contacto</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="ejemplo_email_3" class="col-lg-2 control-label">Ape.Paterno</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtContApePateEdit" placeholder="Apellido Paterno" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ApellidoMaterno" class="col-lg-2 control-label">Ape.Materno</label>
                            <div class="col-lg-10">
                                <asp:TextBox runat="server" class="form-control" ID="TxtContApeMateEdit" placeholder="Apellido Materno" />
                            </div>
                            <div class="form-group">
                                <label for="Nombres" class="col-lg-2 control-label">Nombres</label>
                                <div class="col-lg-10">
                                    <asp:TextBox runat="server" class="form-control" ID="TxtContNombresEdit" placeholder="Nombres" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="Cargo" class="col-lg-2 control-label">Cargo</label>
                                <div class="col-lg-10">
                                    <asp:TextBox runat="server" class="form-control" ID="txtContCargoEdit" placeholder="Cargo" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="Email" class="col-lg-2 control-label">Email</label>
                                <div class="col-lg-10">
                                    <asp:TextBox runat="server" class="form-control" ID="txtContEmailEdit" placeholder="Email" />
                                </div>
                            </div>

                            <div class="form-group" style="display: none">
                                <label for="InstitucionEducativaID" class="col-lg-2 control-label">InstitucionEducativaID</label>
                                <div class="col-lg-10">
                                    <asp:TextBox runat="server" class="form-control" ID="txtInstEduID" placeholder="InstitucionEducativaID" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button class="btn btn-facebook" runat="server" ID="btnEditaContacto" Text="Editar" OnClick="btnEditaContacto_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
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
                                <asp:Button Text="Confirmar" ID="btnConfirmar" runat="server" class="btn btn-default" OnClick="btnConfirmar_Click"  />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
           <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlId="rbtTipoEmpresa" EventName="SelectedIndexChanged" />               
            </Triggers>
    </asp:UpdatePanel>--%>

    <div id="iexist" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="lblIE" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
      </div>
</asp:Content>
