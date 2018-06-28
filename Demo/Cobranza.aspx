<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="Demo.Cobranza" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Css/standar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    .actualizando_principal
    {
        background-color: #333333;
        filter: alpha(opacity=60); 
        opacity: 0.60; 
        width: 100%;
        top: 0px; 
        left: 0px; 
        position: fixed; 
        height: 100%;
    }

    .actualizando
    {
        margin:auto;
        filter: alpha(opacity=100);
        opacity: 1;
        font-size:small;
        vertical-align: middle;
        top: 35%;
        position: fixed;
        right: 45%;
        margin-left:auto;
        margin-right:auto;
        text-align: center;
        background-color: #ffffff;
        height: 128px;
        width:128px;
        -webkit-border-radius: 10px 10px 10px 10px;
        border-radius: 10px 10px 10px 10px;
    }

    .actualizando img
    {
        /*width:60px;
        height:64px;
        margin-left:auto;
        margin-right:auto;
        margin-top:32px;*/
    }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods = "true">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="actualizando_principal">
                <div class="actualizando">
                    <asp:Image ID="imgEsperando" ImageUrl="Images/cargando.gif" runat="server" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="content">

        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-primary">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Criterios de busqueda</a>
                        </h4>
                    </div>
                    <div id="collapse1" class="panel-collapse collapse in">
                        <div class="panel-body">

                            <div class="form">

                                <div class="form-group row">
                                <label  class="col-sm-2 col-form-label">Campaña:</label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlFCampana" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlFCampana_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <label  class="col-sm-2 col-form-label">Estado de Pago:</label>
                                <div class="col-sm-3">
                                   <asp:CheckBox ID="chkSoloPendiente" runat="server" /> &nbsp;<label  class="col-form-label">Solo Pendiente</label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label  class="col-sm-2 col-form-label">Tipo Reporte:</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="ddlFTipoReporte" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlFTipoReporte_SelectedIndexChanged">
                                       <asp:ListItem Value="1" Text="POR COMPAÑIA DE SEGURO"></asp:ListItem>
                                       <asp:ListItem Value="2" Text="POR INSTITUCION EDUCATIVA" Selected="True"></asp:ListItem>
                                       <asp:ListItem Value="3" Text="POR TIPO DE SEGURO"></asp:ListItem>
                                            </asp:DropDownList>
                                </div>
                                <label  class="col-sm-2 col-form-label">Filtro:</label>
                                <div class="col-sm-3">
                                   <asp:DropDownList ID="ddlFiltro" runat="server" CssClass="form-control">
                                   </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button class="btn btn-facebook" runat="server" ID="btnbuscar" Text="Buscar" OnClick="btnbuscar_Click" />
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
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Resultados</a>
                        </h4>
                    </div>
                    <div id="collapse2" class="panel-collapse in">
                        <div class="panel-body">
                            <rsweb:ReportViewer ID="RptVCobranza" runat="server" Width="100%" Height="500px">
<%--                                <LocalReport ReportPath="RepCobbyPrd.rdlc"></LocalReport>--%>
                            </rsweb:ReportViewer>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
