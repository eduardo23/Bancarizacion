<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoletaPagosAfiliados.aspx.cs" Inherits="Demo.Reportes.BoletaPagosAfiliados" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width:100%">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                   <%-- <rsweb:ReportViewer ID="RPVClientes" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="787px">
                        <LocalReport ReportPath="Informes\rptClientes.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>--%>

                  <rsweb:ReportViewer ID="RPVBoletaAfiliado" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="787px">


 <LocalReport ReportPath="Reportes\BoletaPagosAfiliados.rdlc">   </LocalReport>
                  </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                       
                </td>
            </tr>
        </table>
         <div>    
    </div>
    </form>
</body>
</html>
