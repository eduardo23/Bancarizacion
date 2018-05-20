<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"  Async="true" CodeBehind="CargarCodigos.aspx.cs" Inherits="Demo.CargarCodigos" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
        <link href="Css/standar.css" rel="stylesheet" />
    <style>
 .modales
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;*/
}
.center
{
    z-index: 1000;
    margin: 200px auto;
    padding: 10px;
    /*width:230px;*/
    /*background-color: White;*/  
    /*filter: alpha(opacity=100);*/
    /*opacity: 1;
    -moz-opacity: 1;*/
}
.center img
{
    height: 280px;
    width: 280px;
}
 </style>
    
    <%--<script type="text/javascript">   
        $(function () {            
            $('#' + '<%= btnCargarValidar.ClientID %>').bind('click', function () {
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
   <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
    --%>
   </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
    //function ShowProgress() {
    //    setTimeout(function () {
    //        var modal = $('<div />');
    //        modal.addClass("modal");
    //        $('body').append(modal);
    //        var loading = $(".loading");
    //        loading.show();
    //        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
    //        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
    //        loading.css({ top: top, left: left });
    //    }, 200);
    //}
    //$('form').live("submit", function () {
   //     ShowProgress();
   // });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"  >
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    


    <asp:MultiView ID="MVCodigos" runat="server" ActiveViewIndex="0">
    <asp:View runat="server" ID="vwPrincipal">        
    
 <ul class="nav nav-tabs">
          <li><a data-toggle="tab" href="#home">Depurador Datos</a></li>
          <li style="height: 40px" class="active"><a data-toggle="tab" href="#menu1">Asegurados</a></li>
          <li><a data-toggle="tab" href="#menu2">Historial</a></li>
   </ul>
  
 <div class="tab-content">  
     <div id="home" class="tab-pane fade">
         <div class="panel-group" id="accordion">
             <div class="panel panel-primary">
                 <div class="panel-heading">
                     <h4 class="panel-title" style="color:white">
                         <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Cargar Archivo
                         </a>
                     </h4>
                 </div>

                 <div id="collapse1" class="panel-collapse collapse in">
                  
                     <div class="panel-body">
                                                  
                         <div class="form-group" >
                             <label for="Descripcion" class="control-label col-md-3">Institución Educativa:</label>
                             <div class="col-md-3">
                                 <asp:TextBox runat="server" class="form-control" ID="txtIE" placeholder="" disabled="true" OnTextChanged="txtIE_TextChanged" />
                             </div>

                             <label for="Descripcion" class="col-md-2">Tipo de Seguros:</label>
                             <div class="col-md-4">
                                 <asp:TextBox runat="server" class="form-control" ID="txtTipoSeguro" placeholder="" disabled="true" />
                             </div>
                         </div>
                    
                         <div class="form-group"  >
                             <label for="Descripcion" class="control-label col-md-3">CIA de Seguros:</label>
                             <div class="col-md-3">
                                 <asp:TextBox runat="server" class="form-control" ID="txtCiaSeguro" placeholder="" disabled="true" />
                             </div>
                         </div>

                         <label for="Descripcion" class="control-label col-md-3">Cargar archivo:</label>
                         <div class="col-md-9">
                             <div>
                                 <asp:FileUpload ID="FUFile" runat="server" Width="700px" />
                             </div>
                         </div>
                      

                         <div class="form-group">
                             <label for="Descripcion" class="col-lg-12 control-label"></label>
                             <div class="col-lg-10">
                                 <asp:Button runat="server" class="form-control" ID="btnCargarValidar" Text="Cargar y Validar"  data-loading-text="<i class='fa fa-circle-o-notch fa-spin'></i> Processing Order"
                                      Width="131px"  CssClass="button" OnClick="btnCargarValidar_Click"   style="background-color:orangered"/>
                             </div>
                             <asp:HiddenField ID="hdnCodigoGen" runat="server" />
                             <asp:HiddenField ID="hdnUsuario" runat="server" />
                         </div>
                     </div>
                 </div>
              </div>
        
    <div class="panel panel-primary">
          <div class="panel-heading">
              <h4 class="panel-title" style="color:white">
                  <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Visualizar datos</a>
              </h4>
          </div>
          <div id="collapse2" class="panel-collapse collapse in"  >
              <div class="panel-body">
                  <div class="form-group">
                      <label for="co" class="col-lg-4 control-label" style="color:white">Total Registros:    
                      <asp:Label ID="lblCantidadCargados" runat="server" Text="0"></asp:Label>
                      </label>
                      <div style="background-color:#585858">                      
                      <div class="form-group">
                          <div style="overflow:scroll;width:100%;height:350px;"> 
                            <asp:GridView ID="GrvPositivaAccidentes" runat="server" Width="100%" CssClass="Grid2"                               
                              AutoGenerateColumns="false" OnRowDataBound="GrvPositivaAccidentes_RowDataBound" 
                                OnSelectedIndexChanged="GrvPositivaAccidentes_SelectedIndexChanged">
                              <Columns>
                                  <asp:TemplateField><ItemTemplate><asp:Label runat="server" ID="lblid"></asp:Label></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>
                                  <asp:BoundField DataField="Item" HeaderText="Item" />
                                  <asp:BoundField DataField="Apellido Paterno" HeaderText="Apellido Paterno" />
                                  <asp:BoundField DataField="Apellido Materno" HeaderText="Apellido Materno" />
                                  <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                  <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                  <asp:BoundField DataField="Fecha_Nacimiento" HeaderText="Fecha Nacimiento" htmlencode="true"  DataFormatString="{0:d}"  />
                                  <asp:BoundField DataField="DNI" HeaderText="DNI" />
                                  <asp:BoundField DataField="Grado" HeaderText="Grado" />
                                  <asp:BoundField DataField="Sección" HeaderText="Sección" />
                                  <asp:BoundField DataField="Direccion" HeaderText="Dirección" /> 
                                  <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" /> 
                                  <asp:TemplateField HeaderText="Estado"><ItemTemplate><asp:Image runat="server"  ID="imgAdvertencia" ImageUrl="~/Images/icoactivo.png"  Width="16px"  data-placement="left"
                                              Height="16px" Visible="false"  data-toggle="popover" data-trigger="hover" data-content="Debe corregir el error"/></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>                                  

                              <asp:TemplateField HeaderText="Error"><ItemTemplate><asp:Label runat="server" id="lblerror" text="">
                                    &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                    </asp:Label></ItemTemplate><HeaderStyle Width="10px" CssClass="oculto" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center" CssClass="oculto"/></asp:TemplateField>                                  
                              </Columns>                              
                          </asp:GridView>
                              

<%--                              <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updPanel">
                                    <ProgressTemplate>
                                                    <div class="modal">
                                                    <div class="center">
                                                                <img alt="" src="tenor.gif" />
                                                    </div>
                                                    </div>
                                    </ProgressTemplate>
                            </asp:UpdateProgress>--%>

                           
                             
                              <asp:GridView ID="GrvRimacAccidentes" runat="server" Width="100%" CssClass="Grid2"                               
                              AutoGenerateColumns="false" OnRowDataBound="GrvRimacAccidentes_RowDataBound">
                              <Columns>
                                  <asp:TemplateField><ItemTemplate><asp:Label runat="server" ID="lblid"></asp:Label></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>
                                  <asp:BoundField DataField="Item" HeaderText="Item" />
                                  <asp:BoundField DataField="plan" HeaderText="Plan" />
                                  <asp:BoundField DataField="Tipo_Documento" HeaderText="Tipo_Documento" />
                                  <asp:BoundField DataField="Num#_Documento" HeaderText="Num. Documento" />
                                  <asp:BoundField DataField="Apellido Paterno" HeaderText="Apellido Paterno" />
                                  <asp:BoundField DataField="Apellido Materno" HeaderText="Apellido Materno" />
                                  <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                  <asp:BoundField DataField="Fecha_Nacimiento" HeaderText="Fecha Nacimiento" htmlencode="true"  DataFormatString="{0:d}"  />
                                  <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" />
                                  <asp:BoundField DataField="Profesión /_Ocupación" HeaderText="Profesión" />
                                  <asp:BoundField DataField="Becado" HeaderText="Becado" />
                                  <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                                  <asp:BoundField DataField="Estado_Civil" HeaderText="Estado_Civil" /> 
                                  <asp:BoundField DataField="Grado" HeaderText="Grado" /> 
                                  <asp:BoundField DataField="Sección" HeaderText="Sección" /> 
                                  <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" /> 
                                  
                                  <asp:TemplateField HeaderText="Estado"><ItemTemplate><asp:Image runat="server"  ID="imgAdvertencia" ImageUrl="~/Images/icoactivo.png"  Width="16px"  data-placement="left"
                                              Height="16px" Visible="false"  data-toggle="popover" data-trigger="hover" data-content="Debe corregir el error"/></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>                                  

                                  <asp:TemplateField HeaderText="Error"><ItemTemplate><asp:Label runat="server" id="lblerror" text="">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                            </asp:Label></ItemTemplate><HeaderStyle Width="10px" CssClass="oculto" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center" CssClass="oculto"/></asp:TemplateField>                                  
                              </Columns>                              
                          </asp:GridView>
                      
                                      <%--</ContentTemplate>
                              </asp:UpdatePanel>--%>


                           <asp:GridView ID="grvPacificoAccidentes" runat="server" Width="100%" CssClass="Grid2" 
                            
                              AutoGenerateColumns="false" OnRowDataBound="grvPacificoAccidentes_RowDataBound">
                              <Columns>
                                  <asp:TemplateField><ItemTemplate><asp:Label runat="server" ID="lblid"></asp:Label></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>
                                  <asp:BoundField DataField="Item" HeaderText="Item" />
                                  <asp:BoundField DataField="Apellido Paterno" HeaderText="Apellido Paterno" />
                                  <asp:BoundField DataField="Apellido Materno" HeaderText="Apellido Materno" />
                                  <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                                  <asp:BoundField DataField="Fecha de Nacimiento" HeaderText="Fecha Nacimiento" htmlencode="true"  DataFormatString="{0:d}"  />
                                  <asp:BoundField DataField="sexo" HeaderText="Sexo" />
                                  <asp:BoundField DataField="DNI" HeaderText="Numero de Documento" />
                                  <asp:BoundField DataField="Poliza" HeaderText="Poliza" />
                                  <asp:BoundField DataField="Inicio Vigencia" HeaderText="Inicio Vigencia" />
                                  <asp:BoundField DataField="Fin Vigencia" HeaderText="Fin Vigencia" /> 
                                  <asp:BoundField DataField="Profesión /_Ocupación" HeaderText="Profesión" /> 
                                  <asp:BoundField DataField="becado" HeaderText="Becado" /> 
                                  <asp:BoundField DataField="Tipo Documento" HeaderText="Tipo Documento" /> 
                                  <asp:BoundField DataField="Grado" HeaderText="Grado" /> 
                                  <asp:BoundField DataField="seccion" HeaderText="Sección" /> 
                                  <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" /> 
                                  <asp:TemplateField HeaderText="Estado"><ItemTemplate><asp:Image runat="server"  ID="imgAdvertencia" ImageUrl="~/Images/icoactivo.png"  Width="16px"  data-placement="left"
                                              Height="16px" Visible="false"  data-toggle="popover" data-trigger="hover" data-content="Debe corregir el error"/></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>                                  
                                  <asp:TemplateField HeaderText="Error"><ItemTemplate><asp:Label runat="server" id="lblerror" text="">
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;
                                    </asp:Label></ItemTemplate><HeaderStyle Width="10px" CssClass="oculto" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center" CssClass="oculto"/></asp:TemplateField>                                  
                              </Columns>                              
                          </asp:GridView>

                          <asp:GridView ID="grvDatosIE_RENTA" runat="server" Width="100%" CssClass="Grid2"   AutoGenerateColumns="false" 
                              OnRowDataBound="grvDatosIE_RENTA_RowDataBound1">
                              <Columns>
                                  <asp:TemplateField><ItemTemplate><asp:Label runat="server" ID="lblid"></asp:Label></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>
                                  <asp:BoundField DataField="Item" HeaderText="Item" />
                                  <asp:BoundField DataField="Tipo Documento" HeaderText="TipoDoc.Aseg" />
                                  <asp:BoundField DataField="Num# Documento" HeaderText="NumDoc.Aseg" />
                                  <asp:BoundField DataField="Apellido Paterno" HeaderText="ApeMaterno.Aseg" />
                                  <asp:BoundField DataField="Apellido Materno" HeaderText="ApeMaterno.Aseg" />
                                  <asp:BoundField DataField="Nombres" HeaderText="Nombres.Aseg" />
                                  <asp:BoundField DataField="Fecha Nacimiento" HeaderText="FechaNac.Aseg" htmlencode="true"  DataFormatString="{0:d}"  />
                                  <asp:BoundField DataField="sexo" HeaderText="Sexo.Aseg" />
                                  <asp:BoundField DataField="Parentesco" HeaderText="Parentesco.Aseg" />
                                  <asp:BoundField DataField="Tipo Documento1" HeaderText="TipoDoc.Ben" />
                                  <asp:BoundField DataField="Num# Documento1" HeaderText="NumDoc.Ben" />
                                  <asp:BoundField DataField="Apellido Paterno1" HeaderText="ApePaterno.Ben" /> 
                                  <asp:BoundField DataField="Apellido Materno1" HeaderText="ApeMaterno.Ben" /> 
                                  <asp:BoundField DataField="Nombres1" HeaderText="Nombres.Ben" /> 
                                  <asp:BoundField DataField="Fecha Nacimiento1" HeaderText="FechaNac.Ben" /> 
                                  <asp:BoundField DataField="sexo1" HeaderText="Sexo.Ben" /> 
                                  <asp:BoundField DataField="Grado" HeaderText="Grado.Ben" /> 
                                  <asp:BoundField DataField="Seccion" HeaderText="Sección.Ben" /> 
                                  <asp:BoundField DataField="Beneficiario Por" HeaderText="Beneficiario Por" /> 
                                  <asp:TemplateField HeaderText="Estado"><ItemTemplate><asp:Image runat="server"  ID="imgAdvertencia" ImageUrl="~/Images/icoactivo.png"  Width="16px"  data-placement="left"
                                              Height="16px" Visible="false"  data-toggle="popover" data-trigger="hover" data-content="Debe corregir el error"/></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Error"><ItemTemplate><asp:Label runat="server" id="lblerror" text="">
                                  </asp:Label></ItemTemplate><HeaderStyle Width="10px" CssClass="oculto" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center" CssClass="oculto"/></asp:TemplateField>                                  
                              </Columns>                              
                          </asp:GridView>
                      </div>
                      </div>
                          </div>
                        <%--  <asp:UpdateProgress ID="UpdateProgress1"  runat="server" AssociatedUpdatePanelID="pnl1"  >
                              <ProgressTemplate >
                                  <div class="modales">
                                      <div class="center">
                                          <img src="Images/tenor.gif"  />          
                                      </div>
                                  </div>
                              </ProgressTemplate>
                          </asp:UpdateProgress>--%>

                       <%--  <asp:UpdatePanel runat="server" ID="pnl1"  BackgroundCssClass="modal">
                              <ContentTemplate>--%>
                              <div class="form-group">
                              <label for="Descripcion" class="col-lg-12 control-label"></label>
                              <div class="col-lg-10">
                                  <asp:Button runat="server" class="form-control" ID="btnDepurarGenerar" Text="Depurar y Generar" Width="131px" 
                                      CssClass="button" OnClick="btnDepurarGenerar_Click" Enabled="False" />
                                  <asp:Button runat="server" class="form-control" ID="btnCancelar" Text="Cancelar" Width="131px" CssClass="button" Style="background-color: orangered" OnClick="btnCancelar_Click" />
                                  <asp:HiddenField ID="hdnCantidad" runat="server" OnValueChanged="hdnCantidad_ValueChanged" />
                                  <asp:HiddenField ID="hdnAlumnoSelect" runat="server" />
                                  <asp:HiddenField ID="hdnEliminaOpcion" runat="server" />
                                  
                              </div>
                                </div>

<%--                             </ContentTemplate>
                          </asp:UpdatePanel>--%>
                      </div>
                  </div>
              </div>
              </div>
            </div>
         </div>
  <%--</div>--%>
     
     <!--Tab2-->
   <div id="menu1" class="tab-pane fade in active">  
        <div class="panel-group" id="accordion2">
        <div class="panel panel-primay">
        <div class="panel-heading">
        <h4 class="panel-title">
            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse3">
               Criterios de Búsqueda</a>
        </h4>
        </div>
            <div id="collapse3" class="panel-collapse collapse in">
                <div class="panel-body">

                    <div class="form-group">
                        <label for="Descripcion" class="control-label col-md-2">Institución Educativa:</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" class="form-control" ID="txtInstitucionIEMn1" placeholder="" disabled="true" />
                        </div>
                        <label for="Descripcion" class="control-label col-md-2">Tipo de seguro:</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" class="form-control" ID="txtTipoSegurosMn1" placeholder="" disabled="true" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="Descripcion" class="control-label col-md-2">CIA de Seguros:</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtCiaSeguroMn1" placeholder="" disabled="true"  Width="320px" />
                        </div>
                    </div>
                                               
                        
                      <div class="form-group">                        
                        <label for="Descripcion" class="control-label col-md-2">Apellidos y Nombres:</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" class="form-control" ID="txtApeNombresMn1" Style="text-transform: uppercase"  Width="320px" />
                        </div>
                    </div>                           
                
                   <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-2"></label>
                             <div class="col-md-9">
                              <asp:updatepanel id="UpdateBuscar" runat="server">
                                <contenttemplate>
                                <asp:Button runat="server" class="form-control" ID="btnBuscarAseguradosMn1" Text="Buscar"  Style="background-color:orangered"  Width="131px" CssClass="button"  OnClick="btnBuscarAseguradosMn1_Click"  />
                                </contenttemplate>
                                <triggers><asp:asyncpostbacktrigger controlid="btnBuscarAseguradosMn1" eventname="Click" /></triggers>
                                </asp:updatepanel>
                             </div>
                  </div>
                           
            </div>
      </div>
            
</div>
      
 <div class="panel panel-primary">
          <div class="panel-heading">
              <h4 class="panel-title">
                  <a data-toggle="collapse" data-parent="#accordion2" href="#collapse4">Visualizar datos</a>
              </h4>
          </div>
          
        <div id="collapse4" class="panel-collapse collapse in">
              <div class="panel-body">
                  <div class="form-group">
                      
                      <asp:UpdatePanel ID="upCantidad" runat="server">
                          <ContentTemplate>
                              Resultados de Búsqueda:
                              <asp:Label runat="server" ID="lblCantidad" Text="0"></asp:Label>&nbsp; Registros
                              </ContentTemplate>
                       </asp:UpdatePanel>
                                                                              
                      <div class="form-group">
                          <label for="Descripcion" class="col-lg-12 control-label"></label>
                               <asp:HiddenField ID="hdnProductoID" runat="server" />
                               <asp:HiddenField ID="hdnCiaSeguro" runat="server" />
                               <asp:HiddenField ID="hdnInstitucionEducativa" runat="server" />
                               <asp:HiddenField ID="hdnAsociacionID" runat="server" />
                               <asp:HiddenField ID="hdnAfiliacionSeguroID" runat="server" />
                               <asp:HiddenField ID="hdnAlumnoID" runat="server" />
                               <asp:HiddenField ID="hdnPadreID" runat="server" />
                                                                               
                             <asp:UpdatePanel runat="server" ID="updAsegurados" UpdateMode="Conditional">
                              <Triggers><asp:AsyncPostBackTrigger ControlID="btnBuscarAseguradosMn1" EventName="Click" /><asp:PostBackTrigger ControlID="grvAsegurados" /></Triggers>
                              <ContentTemplate>                            
                                               
                             <asp:GridView runat="server" ID="grvAsegurados" ShowHeader="true" ShowHeaderWhenEmpty="true"
                              AutoGenerateColumns="False" Width="100%" CssClass="Grid" EmptyDataText="No hay Datos" AllowPaging="true" PageSize="10"
                                  OnPageIndexChanging="grvAsegurados_PageIndexChanging" OnRowCommand="grvAsegurados_RowCommand"
                                 ChildernAsTriggers="True" UpdateMode="Always" >
                              <Columns>
                                  <asp:BoundField DataField="Codigo" HeaderText="Codigo" InsertVisible="False" ReadOnly="True"  SortExpression="Codigo" />
                                  <asp:BoundField DataField="ApellidoPaternoview" HeaderText="Apellido Paterno" SortExpression="ApePaterno" />
                                  <asp:BoundField DataField="ApellidoMaternoview" HeaderText="Apellido Materno" SortExpression="ApeMaterno" />
                                  <asp:BoundField DataField="Nombreview" HeaderText="Nombres" SortExpression="Nombres" />
                                  <asp:BoundField DataField="FechaNacimientoview" HeaderText="Fecha Nacimiento" SortExpression="FechaNacimiento" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}"  />
                                  <asp:BoundField DataField="TipoDocumentoIDview" HeaderText="Tipo Documento" SortExpression="Tipo Documento" ItemStyle-HorizontalAlign="Center" />
                                  <asp:BoundField DataField="NumeroDocumentoview" HeaderText="Nro. Documento" SortExpression="NroDocumento" />
                                  <asp:BoundField DataField="ApellidoPaternoben" HeaderText="Apellido Paterno" SortExpression="ApePaterno" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="ApellidoMaternoben" HeaderText="Apellido Materno" SortExpression="ApeMaterno" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="Nombreben" HeaderText="Nombres" SortExpression="Nombres" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="FechaNacimientoben" HeaderText="Fecha Nacimiento" SortExpression="FechaNacimiento" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="TipoDocumentoIDben" HeaderText="Tipo Documento" SortExpression="Tipo Documento" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="NumeroDocumentoben" HeaderText="Nro. Documento" SortExpression="NroDocumento" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="BeneficiarioID" HeaderText="Beneficiario" SortExpression="Beneficiario" />
                                  <asp:TemplateField HeaderText="Situación"><ItemTemplate><asp:Label runat="server" ID="lblSituacion"  Text='<%# Convert.ToBoolean( Eval("Situacion"))==true ? "ACTIVO" : "NO ACTIVO"   %>'></asp:Label></ItemTemplate></asp:TemplateField>                               
                                  <asp:BoundField DataField="CodigoDetalleID" HeaderText="CodigoDetalleID" SortExpression="CodigoDetalleID"  ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="AlumnoID" HeaderText="AlumnoID" SortExpression="AlumnoID" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="GradoIDben" HeaderText="Grado" SortExpression="GradoID" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>                                  
                                  <asp:BoundField DataField="seccionben" HeaderText="Seccion" SortExpression="Seccion" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="sexoben" HeaderText="Sexo" SortExpression="Sexo"  ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto"/>
                                  <asp:BoundField DataField="PadreId" HeaderText="PadreId" SortExpression="PadreId" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="apellidoPaternoafi" HeaderText="apellidoPaternoafi" SortExpression="apellidoPaternoafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="ApellidoMaternoafi" HeaderText="ApellidoMaternoafi" SortExpression="ApellidoMaternoafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="Nombreafi" HeaderText="Nombreafi" SortExpression="Nombreafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="TipoDocumentoIDafi" HeaderText="TipoDocumentoIDafi" SortExpression="TipoDocumentoIDafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="NumeroDocumentoafi" HeaderText="NumeroDocumentoafi" SortExpression="NumeroDocumentoafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:BoundField DataField="FechaNacimientoafi" HeaderText="FechaNacimientoafi" SortExpression="FechaNacimientoafi" ItemStyle-CssClass="oculto" HeaderStyle-CssClass="oculto" />
                                  <asp:TemplateField HeaderText="Acciones"><ItemTemplate><asp:ImageButton ID="btnEditar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EditarAsegurado" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEditar.png" title="EditarAsegurado" Width="16px" /><asp:ImageButton ID="btnAnular" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="AnularAsegurado" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/deletes.png" title="AnularAsegurado" Width="16px" /><asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EliminarAsegurado" data-toggle="tooltip" Height="16px" ImageUrl="~/Images/icoEliminar.png" title="EliminarAsegurado" Width="16px" /></ItemTemplate></asp:TemplateField>
                              </Columns>
                          </asp:GridView>
                         </ContentTemplate>
                          </asp:UpdatePanel>

                          <div class="form-group">
                              <label for="Descripcion" class="col-lg-12 control-label"></label>
                              <div class="col-lg-10">
                                  <asp:Button runat="server" class="form-control" ID="btnDepurar" Text="Agregar Asegurados" Width="139px" CssClass="button" />
                                  <asp:Button runat="server" class="form-control" ID="btnCancelarDepurar" Text="Exportar Excel" Width="131px" CssClass="button" style="background-color:orangered" OnClick="btnCancelarDepurar_Click" />
                              </div>
                          </div>
                      </div>
                  </div>
              </div>
          </div>
      </div>
  </div>
    </div>  


     <!-- Tab3-->
     <div id="menu2" class="tab-pane fade">  
         <div class="panel-group" id="accordion3">
             <div class="panel panel-primary">
                 <div class="panel-heading">
                     <h4 class="panel-title">
                         <a data-toggle="collapse" data-parent="#accordion3" href="#collapse5">Datos de la Asociación
                         </a>
                     </h4>
                 </div>

                 <div id="collapse5" class="panel-collapse collapse in">
                      <div class="panel-body">
                        <div class="form-group">
                        <label for="Descripcion" class="col-lg-12 control-label">Institución Educativa:</label>
                        <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" ID="txtIEMn2"  placeholder="" disabled="true" Width="450" />
                    </div>                   
                </div>

              <div class="form-group">
                   <label for="Descripcion" class="col-lg-12 control-label">Tipo de Seguros:</label>                  
                    <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" ID="txtTipoSeguroMn2" placeholder="" disabled="true" Width="450" />             
                  </div>
                </div>
                        
               <div class="form-group">
                   <label for="Descripcion" class="col-lg-12 control-label">CIA de Seguros:</label>                  
                    <div class="col-lg-12">
                        <asp:TextBox runat="server" class="form-control" ID="txtCiaSeguroMn2" placeholder="" disabled="true" Width="450" />             
                  </div>                            
              </div>             
                                             
          <div class="form-group">              
              <%--<label for="Descripcion" class="col-lg-12 control-label"></label>
              <div class="col-lg-10">
                  <asp:Button runat="server" class="form-control" ID="Button1" Text="Cargar y Validar"  Width="131px"  CssClass="btn-success" OnClick="btnCargarValidar_Click"/>
              </div>                                                      --%>
          </div>          
      </div>
    </div>
  </div>
      
    <div class="panel panel-primary">
          <div class="panel-heading">
              <h4 class="panel-title">
                  <a data-toggle="collapse" data-parent="#accordion3" href="#collapse6">Historial de Cargas</a>
              </h4>
          </div>
          <div id="collapse6" class="panel-collapse collapse in">
              <div class="panel-body">
                  <div class="form-group">
                      <label for="co" class="col-lg-2 control-label">Datos</label>
                      <div class="form-group">
                       <asp:GridView ID="grvHistorialCargas" runat="server" Width="100%" CssClass="Grid"     AutoGenerateColumns="false" EmptyDataText="Sin Registros" ShowHeader="true"  ShowHeaderWhenEmpty="true">
                              <Columns>
                                  <%--<asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Label runat="server" ID="lblid"></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="10px" />
                                      <ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/>
                                  </asp:TemplateField>--%>
                                  
                                  <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                  <asp:BoundField DataField="InstitucionEducativa" HeaderText="Institución Educativa" />
                                  <asp:BoundField DataField="TipoSeguro" HeaderText="Tipo Seguro" />
                                  <asp:BoundField DataField="codigoID" HeaderText="Cantidad" />
                                  <asp:BoundField DataField="UsuarioCarga" HeaderText="Usuario Carga" />
                                  <asp:BoundField DataField="FechaCarga" HeaderText="Fecha Carga" htmlencode="true"   />                                                                    
                                  <asp:TemplateField HeaderText="Acciones"><ItemTemplate><asp:Image runat="server"  ID="imgAdvertencia" ImageUrl="~/Images/deletes.png" Width="16px"  data-placement="left"
                                              Height="16px"  data-toggle="popover" data-trigger="hover" data-content=""/></ItemTemplate><HeaderStyle Width="10px" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign="Center"/></asp:TemplateField>                                  
                              </Columns>
                              
                          </asp:GridView>
                          <div class="form-group">
                              <label for="Descripcion" class="col-lg-12 control-label"></label>
                              <div class="col-lg-10">
                              <%--    <asp:Button runat="server" class="form-control" ID="Button2" Text="Depurar y Generar" Width="131px" CssClass="btn-success" />
                                  <asp:Button runat="server" class="form-control" ID="Button3" Text="Cancelar" Width="131px" CssClass="btn-warning" />--%>
                              </div>
                          </div>                     
                           </div>
                      </div>
                  </div>
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
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
      </div>         
  
   </asp:View>
    <asp:View runat="server" ID="VwEditAsegurado">                
                     <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color:white">
                                        <asp:Label runat="server" Text="" ID="lblhijo"></asp:Label>
                                     </a>
                            </div>
                            <div class="panel-body">       


                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Apellido Paterno:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtApePaterno" placeholder="" Style="text-transform: uppercase"/>
                             </div>
                         </div>
                     <div class="form-group">
                          <label for="Descripcion" class="col-md-3">Apellido Materno:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtApeMaterno" placeholder="" Style="text-transform: uppercase" />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Nombres:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtNombres" placeholder="" Style="text-transform: uppercase" />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Tipo de Documento:</label>
                             <div class="col-md-9">
                                 <asp:DropDownList runat="server" class="form-control" ID="ddlTipoDocumento" />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Numero de Documento:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtNumeroDocumento" placeholder=""  />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Fecha de Nacimiento:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtFechaNacimiento" placeholder=""  Type="date"/>
                             </div>
                         </div>
                         
                     <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Grado que cursará:</label>
                             <div class="col-md-9">
                                 <asp:DropDownList runat="server" class="form-control" ID="ddlGrado" />
                             </div>
                     </div>

                    <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Sección:</label>
                             <div class="col-md-9">
                                        <asp:TextBox runat="server" class="form-control" ID="txtSeccion" placeholder="" Style="text-transform: uppercase" />
                             </div>
                     </div>

                    <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Género:</label>
                             <div class="col-md-9">
                                 <asp:RadioButtonList runat="server" id="rbtGenero" RepeatDirection="Horizontal" Width="240px">
                                     <asp:ListItem Value="1">Masculino</asp:ListItem>
                                     <asp:ListItem Value="2">Femenino</asp:ListItem>
                                 </asp:RadioButtonList>
                             </div>
                    </div>

                     <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Observación:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtObservacion" placeholder="" Style="text-transform: uppercase" />
                             </div>
                     </div>
        </div>
    </div>

        <asp:Panel runat="server" ID="pnlBeneficiario2" Visible="false">
            <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2" style="color:white">
                                        <asp:Label runat="server" ID="lblpadre" Text=""></asp:Label>
                                    </a>
                            </div>
                            <div class="panel-body">       
                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Apellido Paterno:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtApePaterno2" placeholder="" />
                             </div>
                         </div>
                     <div class="form-group">
                          <label for="Descripcion" class="col-md-3">Apellido Materno:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtApeMaterno2" placeholder=""  />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Nombres:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtNombres2" placeholder=""  />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Tipo de Documento:</label>
                             <div class="col-md-9">
                                 <asp:DropDownList runat="server" class="form-control" ID="ddlTipoDocumento2" />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Numero de Documento:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtNumeroDocumento2" placeholder=""  />
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Fecha de Nacimiento:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtFechaNacimiento2" placeholder="" />
                             </div>
                         </div>
                         
                  <%--   <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Grado que cursará:</label>
                             <div class="col-md-9">
                                 <asp:DropDownList runat="server" class="form-control" ID="dllGrado2" />
                             </div>
                     </div>

                      <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Sección:</label>
                             <div class="col-md-9">
                                        <asp:TextBox runat="server" class="form-control" ID="txtSeccion2" placeholder="" />
                             </div>
                     </div>

                    <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Género:</label>
                             <div class="auto-style1">
                                 <asp:RadioButtonList runat="server" id="RbtGenero2" RepeatDirection="Horizontal" Width="240px">
                                     <asp:ListItem Value="1">Masculino</asp:ListItem>
                                     <asp:ListItem Value="2">Femenino</asp:ListItem>
                                 </asp:RadioButtonList>
                             </div>
                    </div>--%>

                     <div class="form-group">
                             <label for="Descripcion" class="control-label col-md-3">Observación:</label>
                             <div class="col-md-9">
                                 <asp:TextBox runat="server" class="form-control" ID="txtObServacion2" placeholder="" />
                             </div>
                     </div>
        </div>                
    </div>            
    </asp:Panel>

        <div class="form-group" style="text-align:right">
                        <asp:Button  runat="server" ID="btnGuardarCiaSeguro" Text="Guardar" CssClass="button" OnClick="btnGuardarCiaSeguro_Click"   /> 
                        <asp:Button  runat="server" ID="btnCancelarCiaSeguro" Text="Cancelar" CssClass="button"  Style="background-color: orangered" CausesValidation="False" OnClick="btnCancelarCiaSeguro_Click" />
               </div>
    </asp:View>
    </asp:MultiView>
    
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
                                  <asp:Button Text="Cerrar" ID="btnquit" runat="server" class="button"/>
                                
                            </div>
                        </div>
                    </div>
                </div>


            <div id="pnlResultados" class="modal fade" role="dialog" style="height:480px">
            <div class="modal-dialog" >
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes Seguros : Reporte de Errores </h4>
                    </div>
                    <div class="modal-body">
                        <%--<div class="msgcentrado"  >--%>
                            <div style="overflow:scroll;height:300px">
                                    <asp:GridView ID="grvResultados" runat="server" CssClass="Grid2" OnRowDataBound="grvResultados_RowDataBound" Width="100%" >
                                    </asp:GridView>
                            </div>
                        </div>
                 <%--   </div>--%>
                    <div class="modal-footer">
                        <asp:Button  runat="server"  ID="btnCerrarRes" Text="Cerrar" CssClass="button"  Style="background-color:orangered"/> 
                    </div>
                </div>
            </div>
      </div>         
 <%--   <div class="loading" align="center"  style="width:250;height:250">        
            <img src="Images/tenor.gif" />    
     </div>--%>
    </asp:Content>
