<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="EmailMasivo.aspx.cs" Inherits="Demo.EmailMasivo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="Css/standar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type = "text/javascript">

function checkAll(objRef)
{
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i=0;i<inputList.length;i++)
    {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
        {
            if (objRef.checked)
            {
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                //row.style.backgroundColor = "aqua";
                inputList[i].checked=true;
            }
            else
            {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original
                //if(row.rowIndex % 2 == 0)
                //{
                //   //Alternating Row Color
                //   row.style.backgroundColor = "#C2D69B";
                //}
                //else
                //{
                //   row.style.backgroundColor = "white";
                //}
                inputList[i].checked=false;
            }
        }
    }
}

function Check_Click(objRef) {
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    if (objRef.checked) {
        //If checked change color to Aqua
        //row.style.backgroundColor = "aqua";
    }
    else {
        //If not checked change back to original color
        //if (row.rowIndex % 2 == 0) {
        //    //Alternating Row Color
        //    row.style.backgroundColor = "#C2D69B";
        //}
        //else {
        //    row.style.backgroundColor = "white";
        //}
    }


    //Get the reference of GridView
    var GridView = row.parentNode;

    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];

        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
            if (!inputList[i].checked) {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;
}
</script> 

    <div class="panel-group" id="accordion">                 
        <div class="panel panel-default">
            <div class="panel-primary">
                <div class="panel-heading">
                <a data-toggle="collapse" href="#collapse1" style="color:white">Criterios de Búsqueda</a>
                </div>
                <div id="collapse1" class="panel-collapse collapse in">
                    <div class="panel-body">     
                        <div class="row">
                            <div class="form-group">
                                <div class="col-lg-12" style="padding:5px;">
                                    <div class="col-sm-2">
                                        <label  class="col-form-label">Campaña:</label>
                                    </div>
                                    <div class="col-sm-10">
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlFCampana" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12" style="padding:5px;">
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
                                        <div class="col-sm-4">
                                            <div class="col-sm-4">
                                                <asp:Button ID="btnBuscar" runat="server" CssClass="button" Text="Buscar" OnClick="btnBuscar_Click" />
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Button ID="btnEnviar" runat="server" CssClass="button" Text="Enviar" style="background-color: orangered" OnClick="btnEnviar_Click"/>
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
        <div class="panel panel-default">
            <div class="panel-primary">
                <div class="panel-heading">
                    <a data-toggle="collapse" href="#collapse2" style="color:white">Resultados de Busqueda</a>
                </div>
                <div id="collapse2" class="panel-collapse collapse in" style="padding:10px;">
                    <div class="panel-body" runat="server" style="overflow-y: scroll; height: 400px">
                        <div class="mid-width wrapItems" style="background-color:white; height:400px">
                            <asp:GridView ID="grvInst" runat="server" AutoGenerateColumns="False" CssClass="Grid" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelAll" runat="server" onclick = "checkAll(this);"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSel" runat="server" onclick = "Check_Click(this)"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="TotalPagos" HeaderText="TotalPagos" SortExpression="TotalPagos" />
                                    <asp:BoundField DataField="ID"  HeaderText="ID" SortExpression="ID" >
                                    <HeaderStyle CssClass="oculto" />
                                    <ItemStyle CssClass="oculto" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
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
