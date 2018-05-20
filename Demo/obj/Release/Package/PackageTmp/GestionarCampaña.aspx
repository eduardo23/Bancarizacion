<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestionarCampaña.aspx.cs" MasterPageFile="~/Home.Master" Inherits="Demo.GestionarCampaña" %>

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

        .auto-style1 {
            left: 0px;
            top: 0px;
        }

 </style>  
    <script type="text/javascript">
     $(function () {  
         $("[id*=grvGestionarCampañas] td").hover(function () {
             $("td", $(this).closest("tr")).addClass("hover_row");  
         }, function () {  
             $("td", $(this).closest("tr")).removeClass("hover_row");  
         });  
     });

     function SeleccionarTodos(objRef) {
               var GridView = objRef.parentNode.parentNode.parentNode;
               var inputList = GridView.getElementsByTagName("input");
               for (var i = 0; i < inputList.length; i++) {
                   var row = inputList[i].parentNode.parentNode;
                   if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                       if (objRef.checked) {
                           inputList[i].checked = true;
                       }
                       else {
                           inputList[i].checked = false;
                       }
                   }
               }
           }
             </script>
      <script>
             function openModal() {
                 $('#myModal').modal('show');
             }
    </script>

   <%-- <script>
        function openAperturarCampaña() {
                 $('#pnlAperturarCampaña').modal('show');
             }
    </script>--%>        
  <%-- <script>pnlCierreInstituciones
       function openCierreCampaña() {
           $('#pnlCierreCampaña').modal('show');
       }

    </script>--%>

      <script>
       
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>   

    <div>
        <asp:MultiView ID="MVTipoCampañas" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwBusqueda" runat="server">
                
                <div class="myContent">
                    <div class="content-header">
                        <div class="container-fluid">
                            <div class="row">
                             <div class="panel panel-primary">
                                <div class="panel-heading">Gestionar Campaña : Criterios de busqueda</div>
                                <div class="panel-body">                                                        
                                <div class="row">
                                    <div style="padding: 10px;">
                                       Texto de busqueda
                                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="cajaTexto" Style="width: 70%; text-transform: uppercase;"></asp:TextBox>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="button" OnClick="btnBuscar_Click" Text="Buscar" />
                                        <asp:Button ID="btnNuevo" runat="server" CssClass="button" OnClick="btnNuevo_Click" Style="background-color: orangered" Text="Nuevo" />
                                    </div>
                                    <div style="padding:10px;">                                        
                                            Resultados                                        
                                        <asp:GridView ID="grvGestionarCampañas" runat="server" AllowPaging="True" PageSize="10" AutoGenerateColumns="False" CssClass="Grid"  Width="100%" OnRowDataBound="grvGestionarCampañas_RowDataBound" DataKeyNames="ID" OnPageIndexChanging="grvGestionarCampañas_PageIndexChanging" OnSelectedIndexChanged="grvGestionarCampañas_SelectedIndexChanged"  >
                                            <Columns>                                                  
                                                <asp:BoundField HeaderText="Código" InsertVisible="False" ReadOnly="True" SortExpression="Codigo" DataField="ID" >
                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Campaña" SortExpression="Campaña" DataField="NombreCampaña" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Inicio Vigencia" SortExpression="InicioVigencia" DataField="InicioVigencia" DataFormatString="{0:dd/MM/yyyy}" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Fin Vigencia" SortExpression="FinVigencia" DataField="FinVigencia" DataFormatString="{0:dd/MM/yyyy}" >

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>

<%--                                                <asp:BoundField HeaderText="Situación" SortExpression="Situación" DataField="Situacion" >
                                                <HeaderStyle HorizontalAlign="Center"  Width="15px"/>
                                                </asp:BoundField>--%>
                                                <asp:BoundField HeaderText="Estado" SortExpression="Estado" DataField="Estado" >

                                                <HeaderStyle HorizontalAlign="Center" Width="15px"/>
                                                </asp:BoundField>

                                                <asp:TemplateField HeaderText="Apertura" ItemStyle-Width="40px"><ItemTemplate>
                                                    <asp:ImageButton ID="BtnApertura" runat="server" OnClick="BtnApertura_Click"  ImageUrl="~/Images/Apertura.png" Width="16px" Height="16px"  data-toggle="tooltip" title="Aperturar Campaña"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" /></asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cierre" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnCierre" runat="server" OnClick="BtnCierre_Click" ImageUrl="~/Images/Finalizar.png"  Width="16px" Height="16px" data-toggle="tooltip" title="Cerrar Campaña"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnEditarCampañas" runat="server" OnClick="BtnEditarCampañas_Click" ImageUrl="~/Images/edit.png"  Width="16px" Height="16px"  data-toggle="tooltip" title="Editar Campaña"/>
                                                        &nbsp;
                                                        <asp:ImageButton ID="BtnEliminarCampañas" runat="server" ImageUrl="~/Images/deletes.png"  Width="16px" Height="16px" data-toggle="tooltip" title="Anular Campaña" OnClick="BtnEliminarCampañas_Click"/>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center"  /></asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="HdCodigoCampaña" value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
        </div>                
                
      </asp:View>                                   
     <asp:View runat="server" ID="vwAperturarCampaña">    
     <div class="panel panel-primary">
         <div class="panel-heading" style="background-color:#D6EAF8;color:black">Aperturar Campaña :  <asp:Label ID="lblCampañaSel" runat="server" Text=""></asp:Label> </div>      
        <div class="modal-body" style="background-color:#ffffff;"><div class="bootbox-body">       
         <div class="form-horizontal" role="form">
               
         
            <div class="col-md-12">                
                <div class="bootstrap-duallistbox-container row">
                    
                    <div class="box1 col-md-6">
                        <label for="bootstrap-duallistbox-nonselected-list_CampaniaDetalle.AsociacionID" style="display: none;"></label>
                    <%--    <span class="info-container">
                            <span class="info">Instituciones Educativas </span>
                        </span>--%>
                         <asp:Label ID="lblIeAsociados" runat="server" Text="Label"></asp:Label>        
                         <asp:DropDownList ID="ddlTipoInstitucion" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control "  Width="100%" OnSelectedIndexChanged="ddlTipoInstitucion_SelectedIndexChanged" ToolTip="Filtro por Tipo de Institucion Educativa" >
                         <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                         </asp:DropDownList>
                        <div class="btn-group buttons">
                            <asp:Button runat="server" ToolTip="Mover Todos " Text=" >> " Style="font-weight: bold" ID="btnAgregarTodos" OnClick="btnAgregarTodos_Click"></asp:Button>
                            <asp:Button runat="server" ToolTip="Mover seleccionado" Text=" > " Style="font-weight: bold" ID="btnAgregar" OnClick="btnAgregar_Click"></asp:Button>
                        </div>
                        <asp:ListBox runat="server" ID="ddlInstAsociadas" Width="100%" Height="152px"></asp:ListBox>
                    </div>

                    <div class="box2 col-md-6">
                        <label for="bootstrap-duallistbox-selected-list_CampaniaDetalle.AsociacionID" style="display: none;"></label>  
                        <asp:Label ID="lblie" runat="server" Text="Label"></asp:Label>                   
                        <%--<input class="filter form-control" type="text" placeholder="Filtro">--%>
                         <asp:DropDownList ID="ddlTipoInstitucionAsig" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control "  Width="100%" OnSelectedIndexChanged="ddlTipoInstitucionAsig_SelectedIndexChanged" ToolTip="Filtro por Tipo de Institucion Educativa" >
                         <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
                         </asp:DropDownList>
                        <div class="btn-group buttons">
                                <asp:Button runat="server" ToolTip="Mover Todos " Text=" << " Style="font-weight: bold" ID="btnQuitarTodos" OnClick="btnQuitarTodos_Click"></asp:Button>
                                <asp:Button runat="server" ToolTip="Mover seleccionado" Text=" < " Style="font-weight: bold" ID="btnQuitar" OnClick="btnQuitar_Click"></asp:Button>
                        </div>
                        <asp:ListBox runat="server" ID="ddlInstAsociadasSEL" Width="100%" Height="152px"></asp:ListBox>
                    </div>
                </div>
                <%--<asp:DropDownList runat="server" ID="ddlAfiliados"   style="height: 152px;">                                             
                             </asp:DropDownList>--%>
            </div>
        </div>
        </div>
     </div>

           <div  style="background-color:#ffffff;padding:26px;text-align:right" >
             <asp:HiddenField ID="hdnOpcion" runat="server" />
            <%--<asp:Button  runat="server"   class="button" Text="Asociar" ID="btnAsociar" OnClick="btnAsociar_Click" />--%>
            <asp:Button  runat="server"   class="button" Text="Cancelar" Style="background-color:orangered" OnClick="Unnamed1_Click" />
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
    
    <%--<div id="pnlOpenCampaña" class="modal fade" role="dialog"  >
        <div class="modal-dialog" style="width:80%">
            <!-- Modal content-->
           <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Campañas</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                       
                       <div class="RazonSocial">
                            <label for="RazonSocial" class="col-lg-2 control-label">Nombre</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombreCampaña" placeholder="Ingrese Razón Social" />
                            </div>
                        </div>
                          <br />   
                        <div class="form-group">
                            <label for="NombreCorto" class="col-lg-2 control-label">Inicio Vigencia</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="TxtFechaInicioVigencia" ToolTip="Fecha Inicio de vigencia"  data-toggle="tooltip" data-placement="left"
                                             placeholder="Fecha Inicio de vigencia" required="true"  Type="date"/>
                            </div>
                        </div>
                          <br />   
	               <div class="form-group">
                            <label for="RUC" class="col-lg-2 control-label">Fin de Vigencia</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="TxtFechaFinVigencia" ToolTip="Fecha Fin de vigencia"  data-toggle="tooltip" data-placement="left"
                                             placeholder="Fecha Fin de vigencia" required="true"  Type="date"/>
                            </div>
                        </div>
                        <br />                        


                    </div>
                    <div class="modal-footer">
                        <asp:Button class="btn btn-facebook" runat="server" ID="btnRegistrarContact" Text="Registrar" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    

                             
<div id="pnlEditarCampañas" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Nueva Campaña</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                     
                        <div class="RazonSocial">
                            <label for="RazonSocial" class="col-lg-3 control-label">Nombre</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" ID="TxtNombreCampaña" placeholder="Ingrese Nombre" />
                            </div>
                        </div>
                          <br />   
                        <div class="form-group">
                            <label for="NombreCorto" class="col-lg-3 control-label">Inicio Vigencia</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="TxtFechaIniVigencia" ToolTip="Fecha Inicio de vigencia"  data-toggle="tooltip"
                                             placeholder="Fecha Inicio de vigencia" Type="date"/>
                            </div>
                        </div>
                          <br />   
	               <div class="form-group">
                            <label for="RUC" class="col-lg-3 control-label">Fin de Vigencia</label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" class="form-control" id="TxtFechaFinalVigencia" ToolTip="Fecha Fin de vigencia"  data-toggle="tooltip"
                                             placeholder="Fecha Fin de vigencia" Type="date"/>
                            </div>
                        </div>
                    </div>                    
                      <div class="modal-footer">
                                   <asp:Button   class="button" runat="server"  ID="btnEditaContacto" Text="Guardar" OnClick="btnEditaContacto_Click" />
                                  <asp:Button   class="button" runat="server"  ID="btncerraredit" Text="Cerrar"  style="background-color:orangered" />                                   
                            </div>
                </div>
            </div>
        </div>        
   </div>    

     <div id="pnlCuentas" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 99%">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#D6EAF8" >
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
                           <asp:Button class="btn btn-facebook" runat="server" ID="btnGuardar" Text="Guardar"/>
                           <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                      </div>
                </div>
            </div>
        </div>        
   </div>        
    </div>
               
    <div id="pnlCierreInstituciones" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Seguro de Cerrar la Campaña? </h4>
                </div>
                <div class="modal-body" >
                    <div class="container-fluid" style="height: 250px; overflow-y: scroll;">
                       <asp:GridView ID="grvCierreCampañas" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="Grid" PageSize="14" Width="100%" OnRowDataBound="grvCierreCampañas_RowDataBound" DataKeyNames="EstadoCierre,CodAsociacion,CodInstitucion,productId,CodigoCampaña" OnPageIndexChanging="grvCierreCampañas_PageIndexChanging">
                                            <Columns>
                                                  
                                                <asp:TemplateField HeaderText="Seleccionar" ItemStyle-Width="40px">
                                                    <HeaderTemplate>
                                                     <asp:CheckBox ID="chkAll" runat="server"  enabled="false" onclick ="SeleccionarTodos(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" enabled="false"/>
                                                    </ItemTemplate>

                                                    <ItemStyle Width="40px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="Institución Educativa" InsertVisible="False" ReadOnly="True" SortExpression="Codigo" DataField="NombreInstitucion">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Seguro" SortExpression="Campaña" DataField="SEGURO" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Cia de Segúros" SortExpression="InicioVigencia" DataField="CIASEGURO" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Bancos" SortExpression="FinVigencia" DataField="Bancos" >

                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="150px" />
                                                </asp:BoundField>

                                                <asp:BoundField HeaderText="Estado" SortExpression="Estado" DataField="EstadoCierre" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="CodigoCampaña"  DataField="CodigoCampaña" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"></asp:BoundField>
                                                <asp:BoundField HeaderText="CodInstitucion"  DataField="CodInstitucion" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"></asp:BoundField>
                                                <asp:BoundField HeaderText="productId"  DataField="productId" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"></asp:BoundField>

                                            </Columns>
                                        </asp:GridView>

                    </div>
                    <div class="modal-footer">
                      <asp:Button class="btn btn-facebook" runat="server" ID="btnSeleccionar" Text="Cerrar Campaña" OnClick="btnSeleccionar_Click" />
                        <asp:Button class="btn btn-facebook" runat="server" ID="BtnCancelar" Text="Cancelar"  Style="background-color:orangered"/>
                    </div>
                </div>
            </div>
        </div>        
   </div>      


    <div id="pnlAperturarCampaña" class="modal fade" role="dialog">                   
                <%--<div class="modal-header"  style="background-color:#D6EAF8">                    --%>
                     <div class="panel panel-primary">
                     <div class="panel-heading">Panel with panel-primary class</div>
                    <div class="panel-body">Aperturar Campaña</div>
                    
                   <div class="modal-body">
                    <div class="container-fluid">
                        <div class="form-group">
                            <label for="Codigo" class="col-lg-4 control-label" style="left: 0px; top: 0px">No Póliza:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TextBox1" placeholder="Ingrese Codigo" />
                            </div>
                        </div>
                        <div class="Nombre">
                            <label for="Nombre" class="col-lg-4 control-label">Código Contratante:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TextBox2" placeholder="Código contratante" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="Descripcion" class="col-lg-4 control-label">Nombre contratante:</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="TextBox3" placeholder="Ingrese Nombre contratante" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button class="btn btn-facebook" runat="server" ID="Button1" Text="Asociar" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>        
   </div>    

     <div id="pnlActualizarCampaña" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 40%">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8" >
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Editar Campaña</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">                        
                      <div class="RazonSocial">
                            <label for="RazonSocial" class="col-lg-4 control-label">Nombre</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" ID="txtNombreEdita" placeholder="Ingrese Razón Social" />
                            </div>
                        </div>
                         
                        <div class="form-group">
                            <label for="NombreCorto" class="col-lg-4 control-label">Inicio Vigencia</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" id="txtFechaVigenciaInicioEdita" ToolTip="Fecha Inicio de vigencia"  data-toggle="tooltip"
                                             placeholder="Fecha Inicio de vigencia" Type="date"/>
                            </div>
                        </div>
                         
	               <div class="form-group">
                            <label for="RUC" class="col-lg-4 control-label">Fin de Vigencia</label>
                            <div class="col-lg-8">
                                <asp:TextBox runat="server" class="form-control" id="txtFechaVigenciaFinEdita" ToolTip="Fecha Fin de vigencia"  data-toggle="tooltip"
                                             placeholder="Fecha Fin de vigencia" Type="date"/>
                            </div>
                        </div>                       
                    </div>                    
                        <div class="modal-footer">
                                   <asp:Button   class="button" runat="server"  ID="btnGuardarCampaña" Text="Guardar" OnClick="btnEditaContacto_Click" />
                                  <asp:Button    class="button" runat="server"  ID="btnCerrar" Text="Cerrar"  style="background-color:orangered" />
                                  
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
                                <asp:Button Text="Confirmar" ID="btnConfirmar" runat="server" class="button" OnClick="btnConfirmar_Click" />
                                <asp:Button Text="Cancelar"  runat="server" ID="btnCancelarEliminar" class="button"  Style="background-color:orangered" />  
                            </div>
                        </div>
                    </div>
                </div>
       
                 
</asp:Content>