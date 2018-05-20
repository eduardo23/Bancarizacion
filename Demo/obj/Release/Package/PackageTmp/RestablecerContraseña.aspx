<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestablecerContraseña.aspx.cs" Inherits="Demo.RestablecerContraseña" %>
    <%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="Captcha" %>
<!DOCTYPE html>
<html lang="en">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <link href="Css/standar.css" rel="stylesheet" />
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <title>Sistema de Recaudación Bancaria y Asignación de Códigos Online</title>     
    <script>
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
</head>

<body style="background-color:#273746">
<form id="frmRestablecer" runat="server" defaultbutton = "btnRestablecer">    
    <asp:ScriptManager ID="ScriptManager1Rest" runat="server"></asp:ScriptManager>
    <div style="padding:14px;background-color:white">
           <asp:Image runat="server"  ID="imgLogoRest" ImageUrl="~/Images/logo-hermes.png"/>
           <div>
               Sistema de Recaudación Bancaria y Asignación de Códigos Online
           </div>
    </div>
    <br /><br />
    <div class="container-fluid">
        <div class="row">
    <div class="col-sm-4" style="background-color: #0B2161;">
    </div>
            <div class="col-sm-4" style="background-color: #273746;">
                <div>
                    <div style="background-color: #FF8000;;padding:17px;text-align:center;color:white">
                        Olvidé mi Contraseña
                    </div>
                    
                    <div style="background-color: #D6EAF8;padding:20px">
                     <div class="row">
                           <div class="form-group">
                               <%--<label for="RazonSocial" class="col-lg-2 control-label">Apellido paterno</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtusuarioNombreRest" runat="server" required="true" placeholder="INGRESE CORREO ELECTRÓNICO REGISTRADO *" 
                                       CssClass="cajaTexto" Width="100%" ></asp:TextBox>
                               </div>
                         
                         
                         
                           </div>        

                          <br /><br />
                         <div class="col-lg-12" style="text-align:right">
                                 <div class="col-lg-12" style="text-align:right">
                                   <asp:Button ID="btnAtrasRest" runat="server" Text="Atrás" class="button"  Style="background-color:orangered"  OnClick="btnAtrasRest_Click" formnovalidate/>
                                   <asp:Button ID="btnRestablecer" runat="server" Text="Enviar Contraseña" class="button" OnClick="btnRestablecer_Click" />                               
                         </div>
                         </div>                        
                         <br />
                         <asp:Label ID="Label1" runat="server" style="color:red" Text="(*) Campos obligatorios"></asp:Label>
                         </div>
                    </div>
                </div>                
                <div class="col-sm-4" style="background-color: #0B2161;">
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
                            <asp:Label Text="" ID="txtmensajeRest" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
      </div>    
   
    </form>
</body>
</html>
