<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" MasterPageFile="~/Home.Master" Inherits="Demo.Cobranza.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
 <link href="Css/standar.css" rel="stylesheet" />    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>                    
    
    <asp:MultiView runat="server" ID="MVCodigos" ActiveViewIndex="0">
          <asp:View runat="server" ID="vwDefault" >    
            <div>
                <div class="myContent"  style="width:100%">                        
                            <div style="background-color:#2C3E50;padding:4px;color:white">                                
                                    <h4>Generar códigos </h4>                                
                            </div>
                        </div>                    
                <br />                
                   <div class="panel-group" id="accordion">
                      <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color:white">Criterios de Búsqueda</a>
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
                                            <label for="Institución Educativa:" class="col-lg-3 control-label">Seleccione una Campaña</label>
                                            <div class="col-lg-9">
                                                <asp:DropDownList runat="server" class="form-control" ID="DDLCampaña" Width="420px" AutoPostBack="True" OnSelectedIndexChanged="DDLCampaña_SelectedIndexChanged">
                                               
                                                </asp:DropDownList>

                                            </div>
                                        </div></div><br />

                                       <div class="form-group form-inline" style="border-bottom: 1px solid #ddd;padding-left:5px;">
                                    <div id="maxTotalesDic">
                                        <div class="widget-row">
    <input data-val="true" data-val-number="The field CampaniaID must be a number." data-val-required="El campo CampaniaID es obligatorio." id="CampaniaID" name="CampaniaID" type="hidden" value="2" />
    <input data-val="true" data-val-number="The field TipoReporte must be a number." data-val-required="El campo TipoReporte es obligatorio." id="TipoReporte" name="TipoReporte" type="hidden" value="1" />
    <input data-val="true" data-val-number="The field Int32 must be a number." data-val-required="El campo Int32 es obligatorio." id="TipoReporteCIASeguro" name="TipoReporteCIASeguro" type="hidden" value="1" />
    <input data-val="true" data-val-number="The field Int32 must be a number." data-val-required="El campo Int32 es obligatorio." id="TipoReporteInstitucionEducativa" name="TipoReporteInstitucionEducativa" type="hidden" value="2" />
    <input data-val="true" data-val-number="The field Int32 must be a number." data-val-required="El campo Int32 es obligatorio." id="TipoReporteProducto" name="TipoReporteProducto" type="hidden" value="3" />
    <div class="col-md-4">
        <div id="activeTotalCIASeguro" class="widget-thumb widget-bg-color-white text-uppercase margin-bottom-10 bordered" style="height:120px;cursor:pointer;">
            <h4 class="widget-thumb-heading" style="margin:0px 0px 10px 0px;">POR COMPAÑIA DE SEGURO</h4>
            <div class="widget-thumb-wrap">
                <i class="widget-thumb-icon bg-green icon-list"></i>
                <div class="widget-thumb-body">
                    <span class="widget-thumb-subtitle">RIMAC SEGUROS Y REASEGUROS</span><br />
                    <span style="float:left;"><strong><asp:Label ID="lblSoles" runat="server" Text="lblSoles"></asp:Label> </strong></span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup"><asp:Label ID="lblTotalSoles" runat="server" Text="lblTotalSoles"></asp:Label> </span><br />
                    <span style="float:left;"><strong><asp:Label ID="lblDolares" runat="server" Text="Label"></asp:Label> </strong></span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup"><asp:Label ID="lblTotalDolares" runat="server" Text="Label"></asp:Label> </span>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div id="activeTotalInstitucionEducativa" class="widget-thumb widget-bg-color-white text-uppercase margin-bottom-10 bordered" style="height:120px;cursor:pointer;">
            <h4 class="widget-thumb-heading" style="margin:0px 0px 10px 0px;">POR INSTITUCIÓN EDUCATIVA</h4>
            <div class="widget-thumb-wrap">
                <i class="widget-thumb-icon bg-red icon-graduation"></i>
                <div class="widget-thumb-body">
                    <span class="widget-thumb-subtitle">SAN IGNACIO DE RECALDE</span><br />
                    <span style="float:left;width:30px;"><strong><asp:Label ID="lblSolesIE" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp; </strong></span>
                    <span style="float:left;text-transform:capitalize;"><asp:Label ID="lblTotalSolesAseguradosIE" runat="server" Text="Label"></asp:Label> </span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup"><asp:Label ID="lblTotalSolesIE" runat="server" Text="Label"></asp:Label></span><br />
                    <span style="float:left;width:30px;"><strong>  <asp:Label ID="lblDolatresIE" runat="server" Text="Label"></asp:Label> </strong></span>
                    <span style="float:left;text-transform:capitalize;"><asp:Label ID="lblTotalDolaresAsegurasIE" runat="server" Text="Label"></asp:Label> </span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup">
                        <asp:Label ID="lblTotalDolaresIE" runat="server" Text="Label"></asp:Label> </span>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div id="activeTotalProducto" class="widget-thumb widget-bg-color-white text-uppercase margin-bottom-10 bordered" style="height:120px;cursor:pointer;" onclick="showReportByTipoSeguro(this, 3)">
            <h4 class="widget-thumb-heading" style="margin:0px 0px 10px 0px;">POR TIPO DE SEGURO</h4>
            <div class="widget-thumb-wrap">
                <i class="widget-thumb-icon bg-purple icon-pie-chart"></i>
                <div class="widget-thumb-body">
                    <span class="widget-thumb-subtitle">SEGURO DE ACCIDENTES ESTUDIANTILES</span><br />
                    <span style="float:left;"><strong><asp:Label ID="lblSolesTipoSeguro" runat="server" Text="lblSoles"></asp:Label> </strong></span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup"><asp:Label ID="lblTotalSolesTipoSeguro" runat="server" Text="lblTotalSoles"></asp:Label> </span><br />
                    <span style="float:left;"><strong><asp:Label ID="lblDolaresTipoSeguro" runat="server" Text="Label"></asp:Label> </strong></span>
                    <span style="float:right;" class="widget-thumb-body-stat" data-counter="counterup"><asp:Label ID="lblTotalDolaresTipoSeguro" runat="server" Text="Label"></asp:Label> </span>

                </div>
            </div>
        </div>
    </div>
</div>

                                        <div style="text-align: left">
                                            <div class="form-group">
                                                <label for="Seguro" class="col-lg-2 control-label"></label>
                                                <div class="col-lg-10">
                                                   <div class="form-actions">

<%--<form  id="frm-enviar" method="post">  
     <div class="row">
                       <div class="form-group form-inline">  
                         <div class="form-group">
                                            <label for="Institución Educativa:" class="col-lg-2 control-label">Cia Seguros</label>
                                            <div class="col-lg-2">
                                                <asp:DropDownList runat="server" class="form-control" ID="DropDownList1" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="DDLCampaña_SelectedIndexChanged">
                                               
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                <div class="col-md-8">
                                    <label class="control-label col-md-2">
                                        Fecha Inicio:
                                    </label>
                                    <div class="col-md-2 form-inline">

                                      
                                    </div>
                                     <label class="control-label col-md-2">
                                        Fecha Final:
                                    </label>
                                    <div class="col-md-2 form-inline">
                                        <asp:TextBox ID="txtFechaFinal" runat="server" class="form-control date-picker" value="" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    </div>
                                </div>

                       </div>  </div>
     <div class="row">
                                <div class="col-md-4">
                                    <button type="button" id="btn-edit-body" class="btn btn-danger" style="float:right; margin-left:10px;"><i class="fa fa-file-o"></i>  Editar Mail</button>
                                    <button type="button" id="btn-send-mail" class="btn btn-success" style="float:right; margin-left:10px;"><i class="fa fa-gift"></i>  Enviar</button>
                                    <button type="button" id="btn-search" class="btn btn-default" style="float:right; margin-left:10px;"><i class="fa fa-check"></i>  Buscar</button>
                                </div></div>
                          
</form>    --%>   



                            <div class="form-group form-inline" style="border-bottom: 1px solid #ddd;padding-left:5px;padding-bottom:10px;">
                                   
                                    <div id="divFiltrosCIASeguro">
                                        <div class="col-md-6">
                                            <div class="col-md-5" style="padding-left:0px;">
                                                <label class="control-label">
                                                    CIA Seguro:
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                              <asp:DropDownList runat="server" class="form-control" ID="DropDownList1" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="DDLCampaña_SelectedIndexChanged">
                                               
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Inicio
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtFechaInicio" runat="server" class="form-control date-picker" value="" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Fin
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="txtFechaFinal" runat="server" class="form-control date-picker" value="" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div><br />
                                <%--    <div style="display:none;" id="divFiltrosInstitucionEducativa">
                                        <div class="col-md-6">
                                            <div class="col-md-5" style="padding-left:0px;">
                                                <label class="control-label">
                                                    Institución Educativa:
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                


                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Inicio
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <input value="" class="form-control form-control-inline date-picker" data-val="true" data-val-date="The field FechaInicioInstitucionEducativa must be a date." data-val-required="El campo FechaInicioInstitucionEducativa es obligatorio." id="FechaInicioInstitucionEducativa" name="FechaInicioInstitucionEducativa" type="text" placeholder="DD/MM/YYYY">
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Fin
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <input value="" class="form-control form-control-inline date-picker" data-val="true" data-val-date="The field FechaFinInstitucionEducativa must be a date." data-val-required="El campo FechaFinInstitucionEducativa es obligatorio." id="FechaFinInstitucionEducativa" name="FechaFinInstitucionEducativa" type="text" placeholder="DD/MM/YYYY">
                                            </div>
                                        </div>
                                    </div><br />
                                    <div style="display:none;" id="divFiltrosTipoProducto">
                                        <div class="col-md-6">
                                            <div class="col-md-5" style="padding-left:0px;">
                                                <label class="control-label">
                                                    Producto :
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                              
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Inicio
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <input value="" class="form-control form-control-inline date-picker" data-val="true" data-val-date="The field FechaInicioProducto must be a date." data-val-required="El campo FechaInicioProducto es obligatorio." id="FechaInicioProducto" name="FechaInicioProducto" type="text" placeholder="DD/MM/YYYY">
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="col-md-5">
                                                <label class="control-label">
                                                    Fecha Fin
                                                </label>
                                            </div>
                                            <div class="col-md-7">
                                                <input value="" class="form-control form-control-inline date-picker" data-val="true" data-val-date="The field FechaFinProducto must be a date." data-val-required="El campo FechaFinProducto es obligatorio." id="FechaFinProducto" name="FechaFinProducto" type="text" placeholder="DD/MM/YYYY">
                                            </div>
                                        </div>
                                    </div>--%>

                                </div>





                                                   </div>
                                                   <%-- <asp:Button runat="server" class="form-control" Text="Buscar" ID="btnBuscar" CssClass="btn-success"
                                                       ></asp:Button>--%>
                                                </div>
                                                <asp:Button ID="btnReporte" runat="server" OnClick="btnReporte_Click" Text="Reporte" />
                                            </div>
                                        </div>
                                    </div>
 
                                       </div></asp:Panel>
                               
                           </div>
                   </div>


          </div>
  </div>
  </div>               
      <div>
                 
                <div>
                    </div>
       <%--   <div id="ajaxloader-background">
        <div id="ajaxLoader" class="ui-corner-all">
            <img style="height:100%;" src="../Images/ajax-loader.gif"/>
        </div>
             
    </div>--%>

                   
             <%--<div class="panel-group" id="accordion3">
                      <div class="panel panel-primary">
                            <div class="panel-heading">
                                    <a data-toggle="collapse" data-parent="#accordion3" href="#collapse3" style="color:white">Resumen</a>
                            </div>
                            <div class="panel-body">       
                        <div id="collapse3" class="panel-collapse collapse in">
                                <div class="panel-body">
                            <asp:Panel ID="pBodySeccion3" runat="server" Width="100%" CssClass="wrap">
                                   <iframe src="<%=HdRuta.Value %>"  height="1100" class="img-bordepdf"
                          frameborder="0" scrolling="auto"></iframe>
                                
                            </asp:Panel> 
                                    
                                    <asp:HiddenField ID="HdRuta" runat="server"/>
                        </div>
                        </div>
                   </div>
                </div>
                </div>--%>
            </div>
              </div>
              
          </asp:View>        

            
    </asp:MultiView>

         <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
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
                     
</asp:Content>



