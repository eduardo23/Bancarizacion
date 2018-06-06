<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GenerarTramaOnco.aspx.cs" Inherits="Demo.GenerarTramaOnco" %>
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
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color: white">Generar Trama Oncosalud</a>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group">
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <label  class="col-form-label">Tipo Seguro:</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="DDLTipoSeguro" runat="server" AppendDataBoundItems="True" AutoPostBack="True" class="form-control" OnSelectedIndexChanged="DDLTipoSeguro_SelectedIndexChanged" >
                                                    <asp:ListItem Selected="True" Value="0">--TODOS--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-lg-1">

                                            </div>
                                            <div class="col-lg-3">

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <label  class="col-form-label">Fecha Pago:</label>
                                        </div>
                                        <div class="col-sm-10">
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFecPagoDesde" CssClass="form-control" runat="server" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Fecha Inicio" required></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtFecPagoHasta" CssClass="form-control" runat="server" data-date-format="dd/mm/yyyy" onfocus="(this.type='date')" placeholder="Fecha Fin" required></asp:TextBox>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Button class="btn btn-soundcloud" runat="server" ID="btnGenerar" Text="Generar TXT" OnClick="btnGenerar_Click" CssClass="button" Style="background-color: orangered" />
                                            </div>
                                            <div class="col-lg-3">

                                            </div>
                                        </div>

                                    </div>

                                    <div class="form-group">

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
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
