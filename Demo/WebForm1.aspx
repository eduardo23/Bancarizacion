<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Demo.WebForm1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        
        <table style="width:100%">
            <tr style="background-color:orangered;color:white">
                <td>Codigo</td>
                <td>Asegurado</td>
                <td>Beneficiario</td>
                <td>Prima</td>
                <td>Tipo de Seguro</td>
                <td>Cia Seguro</td>
            </tr>
            <tr>
                <td>[CodigoPago]</td>
                <td>[Asegurado]</td>
                <td>[Beneficiario]</td>
                <td>[Prima]</td>
                <td>[Seguro]</td>
                <td>[CiaSeguro]</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" BehaviorID="TextBox1_CalendarExtender" TargetControlID="TextBox1">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
