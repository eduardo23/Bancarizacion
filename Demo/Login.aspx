<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html>
<html lang="en">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Css/standar.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <title>Sistema de Recaudación Bancaria y Asignación de Códigos Online</title>     
    <style type="text/css">
        .auto-style1 {
            width: 98%;
            margin: 0 auto;
            top: 50%;
            padding: 25px;
        }
    </style>
</head>
<body style="background-color:#273746">
<form id="frmLogin" runat="server">
    
    <div style="padding:14px;background-color:white">
           <asp:Image runat="server"  ID="imgLogo" ImageUrl="~/Images/logo-hermes.png"/>
           <div>
               Sistema de Recaudación Bancaria y Asignación de Códigos Online
           </div>
    </div>
    <br /><br />
    <div class="container-fluid">
        <div class="row">
    
            <div class="col-sm-4 col-md-offset-4" style="background-color: #273746;">
                <div>
                    <div style="background-color: #FF8000; text-align: center; color: white;padding:10px">
                        <strong>Bienvenido a Hermes</strong>
                    </div>
                    
                    <div style="background-color: #D6EAF8;">
                        <div>
                            <br /><br />
                            <div style="text-align: center; color: black">¿ Aún no tienes cuenta creada ?</div>
                        </div>
                        <div style="padding: 10px">
                            <div style="text-align: center">
                                <asp:Button ID="btnRegistrate" runat="server" Text="Regístrate" OnClick="btnRegistrate_Click" CssClass="button" Width="90%" />
                            </div>
                        </div>
                        <div>
                            <div style="text-align: center; color: black">Si estoy registrado, soy usuario</div>
                        </div>
                        <div style="padding: 10px">
                            <div style="text-align: center">
                                <asp:Button ID="btnEntrar" runat="server" Text="Ingresar" CssClass="button" Style="background-color: #FF8000" Width="90%" OnClick="btnEntrar_Click" />
                            </div>
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
                <!--sdsdasdasd-->
                <div class="col-sm-4" style="background-color: #0B2161;">
                    
                </div>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
