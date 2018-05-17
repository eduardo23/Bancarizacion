<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="EnvioEmailMasivo.aspx.cs" Inherits="Demo.EnvioEmailMasivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/standar.css" rel="stylesheet" />
  
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="myContent">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-primary">
                            <div class="panel-heading">          
                               <span class="glyphicon glyphicon-ok"></span>
                                    &nbsp;Envio de Mail Masivo
                            </div>
                            <div class="panel-body">

                                <div class="form-inline">
                                    <div class="form-group">
                                        Fecha Cancelacion<label for="email">:</label>
                                        <asp:TextBox  runat="server" class="form-control" id="txtFechaCancIni" placeholder=""  type="date">    </asp:TextBox>
                                        <asp:TextBox  runat="server" class="form-control" id="txtFechaCancFin" placeholder=""  type="date">    </asp:TextBox>                                      
                                    </div>
                                    
                                    <div class="btn btn-success glyphicon glyphicon-search">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" BackColor="Transparent" BorderWidth="0"  Font-Names="Calibri" />
                                    </div>
                                    <div class="btn btn-warning glyphicon glyphicon-envelope">
                                        <asp:Button ID="btnEnviar" runat="server" Text="Enviar" BackColor="Transparent" BorderWidth="0" Font-Names="Calibri" />
                                    </div>
                                    <div class="btn btn-primary glyphicon glyphicon-pencil">
                                        <asp:Button ID="btnEditarMail" runat="server" Text="Editar Mail" BackColor="Transparent" BorderWidth="0" Font-Names="Calibri"  />
                                    </div>
                                     </div>
                            </div>                                                              
                            </div>                                                     
                        </div>
                    </div>
                </div>
            </div>


        <div class="content-header">
            <div class="container-fluid">
                <div class="row">
                    <div class="panel-group" id="accordion2">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <span class="glyphicon glyphicon-ok"></span>
                                &nbsp;::Resultados
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grvResultados" runat="server" CssClass="Grid" Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="Sin registros" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SELECCIONE" ControlStyle-Width="20" HeaderStyle-Width="20">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccionado" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="id"  DataField="id" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto"></asp:BoundField>
                                        <asp:BoundField HeaderText="RAZON SOCIAL"  DataField="RazonSocial" >
                                             <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="NOMBRE CORTO" DataField="Nombre" >
                                               <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
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
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>    
                                 
</asp:Content>
