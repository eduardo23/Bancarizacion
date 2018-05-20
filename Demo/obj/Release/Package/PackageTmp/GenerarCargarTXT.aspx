<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="GenerarCargarTXT.aspx.cs" Inherits="Demo.GenerarCargarTXT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/standar.css" rel="stylesheet" />
    <script>
        function DescargarArchivoBBVA() {
            document.form1.btnbuscar0.Click();
        }
    </script>
    <%--   <script>
    $(function() {
    $(".preload").fadeOut(2000, function() {
        $(".content2").fadeIn(1000);        
        });
    });
    </script>
    <style>
    --%>
    <%-- .preload { 
                    width:100px;
                    height: 100px;
                    position: fixed;
                    top: 50%;
                    left: 50%;
            }
        </style>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="myContent">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row">
                    <div class="panel-group" id="accordion">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" style="color: white">Generar y Asociar TXT</a>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="form-group">
                                        <label for="institucion educativa" class="col-lg-2 control-label">Banco: </label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="DDLBanco" runat="server" AppendDataBoundItems="True" class="form-control" Width="320">
                                                <asp:ListItem Selected="True" Value="0">SELECCIONE</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="cia de seguro" class="col-lg-2 control-label">Moneda: </label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="DDLMoneda" runat="server" AppendDataBoundItems="True" class="form-control" Width="320">
                                                <asp:ListItem Selected="True" Value="0">SELECCIONE</asp:ListItem>
                                                <asp:ListItem Value="3">AMBOS</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="tipo de seguro" class="col-lg-2 control-label">Empresa Recaudadora: </label>
                                        <div class="col-lg-10">
                                            <asp:DropDownList ID="DDLRecaudador" runat="server" AppendDataBoundItems="True" class="form-control" Width="320">
                                                <asp:ListItem Selected="True" Value="0">SELECCIONE</asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="col-lg-9">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label for="tipo de seguro" class="col-lg-2 control-label"></label>
                                        <div class="col-lg-10">
                                            <asp:Button class="btn btn-facebook" runat="server" ID="btnbuscar" Text="Generar TXT" OnClick="btnbuscar_Click" CssClass="button" />
                                            <asp:Button class="btn btn-soundcloud" runat="server" ID="btnCargar" Text="Cargar TXT" OnClick="btnCargar_Click" CssClass="button" Style="background-color: orangered" />
                                        </div>
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

    <%--   <div class="preload"><img src="http://i.imgur.com/KUJoe.gif">
   </div>--%>

    <div id="pnlCargarArchivo" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 35%">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cargar TXT</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label for="institucion educativa" class="col-lg-3 control-label">Seleccionar: </label>
                                    <div class="col-lg-9">
                                        <asp:FileUpload runat="server" ID="fupArchivo" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button class="btn btn-facebook" runat="server" ID="btnCargarArchivo" Text="Cargar" OnClick="btnCargarArchivo_Click" />
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="pnlResultados" class="modal fade" role="dialog">
        <div class="modal-dialog" style="width: 60%">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #D6EAF8">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Recaudacion Bancaria: Transacciones bancarias</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid">
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <div style="overflow: scroll; height: 300px">
                                        <asp:GridView ID="grvResultados" runat="server" CssClass="Grid2" Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="Bnkds" HeaderText="Banco" ReadOnly="True" SortExpression="Bnkds" />
                                                <asp:BoundField DataField="CodDeudor" HeaderText="Codigo" ReadOnly="True" SortExpression="CodDeudor" />
                                                <asp:BoundField DataField="NumOperacion" HeaderText="Operacion Bancaria" ReadOnly="True" SortExpression="NumOperacion" />
                                                <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" ReadOnly="True" SortExpression="FechaPago" />
                                                <asp:BoundField DataField="Mon" HeaderText="Moneda" ReadOnly="True" SortExpression="Mon" />
                                                <asp:BoundField DataField="ImportePagado" HeaderText="Importe Pago" ReadOnly="True" SortExpression="ImportePagado" />
                                                <asp:BoundField DataField="Cod" HeaderText="Estado Proceso" />
                                                <asp:BoundField DataField="Obs" HeaderText="Observacion" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div>
                                <asp:Label Text="" ID="lblSiProc" runat="server" class="col-lg-12 control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div>
                                <asp:Label Text="" ID="lblNoProc" runat="server" class="col-lg-12 control-label"></asp:Label></div>
                        </div>
                        <div class="row">
                            <div>
                                <asp:Label Text="" ID="lblTotReg" runat="server" class="col-lg-12 control-label"></asp:Label></div>
                        </div>
                        <div class="row">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
