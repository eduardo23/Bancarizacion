<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="Demo.InicioSesion" %>
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
     function openModal()
        {            
           $('#myModal').modal('show');
      }
    </script>
  
</head>

<%--<body style="background-color:#273746" onkeypress="return EntReturnKey();">--%>
  <body style="background-color:#273746">
<form id="frmLogin" runat="server" defaultbutton = "btnRegistrar">    
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <div style="background-color: #FF8000;;padding:17px;text-align:center;color:white">
                        Inicio de Sesión
                    </div>
                    
                    <div style="background-color: #D6EAF8;padding:20px">
                     <div class="row">
                           <div class="form-group">
                               <%--<label for="RazonSocial" class="col-lg-2 control-label">Apellido paterno</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtusuarioNombre" runat="server" required="true" placeholder="ESCRIBA SU USUARIO" 
                                       class="form-control" Width="100%" ></asp:TextBox>
                               </div>
                         
                         
                         
                           </div>

                           <div class="form-group">
                             <%--  <label for="RazonSocial" class="col-lg-2 control-label">Apellido Materno</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtpasswordUsuario" runat="server"  required="true"  TextMode="Password" class="form-control"
                                       placeholder="ESCRIBA SU PASSWORD"  Width="100%">123456</asp:TextBox>
                               </div>
                           </div>
                            <div class="form-group">
                                      <div class="col-lg-12">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>                        
                                        <Captcha:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="None" CaptchaLength="6"
                                        CaptchaHeight="60" CaptchaWidth="230" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                        FontColor="#D20B0C" NoiseColor="#B1B1B1" CaptchaChars="ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789"/>
                                </ContentTemplate>
                      </asp:UpdatePanel>
                      </div>
</div>

                          <div class="form-group">

                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtCaptcha" runat="server"  required="true"  class="form-control"
                                       placeholder="Ingrese Texto que aparece en la imágen"  Width="100%"></asp:TextBox>
                               </div>
                           </div>

                         <div class="form-group">
                            <%--   <label for="RazonSocial" class="col-lg-2 control-label"></label>--%>
                               <div class="col-lg-12">
                                   <asp:CheckBox  ID="chkAcepto" runat="server" Text="Mantener correo y contraseña" />
                               </div>
                           </div>
                         


                       


                         <div class="col-lg-12" style="text-align:right">
                                 <div class="col-lg-12" style="text-align:right">
                                   <asp:Button ID="btnAtras" runat="server" Text="Atras" class="button"  Style="background-color:orangered"  formnovalidate  OnClick="btnAtras_Click"/>
                                   <asp:Button ID="btnRegistrar" runat="server" Text="Iniciar Sesión" class="button"  OnClick="btnRegistrar_Click" />                               
                                </div>
                         </div>
                         <br />
                         <div class="col-lg-12" style="text-align:center">
                                   <asp:LinkButton runat="server" Text="Olvidaste la contraseña" ID="lblOlvidaste" OnClick="lblOlvidaste_Click"  formnovalidate></asp:LinkButton>
                         </div>

                         </div>
                    </div>
                </div>                
                <div class="col-sm-4" style="background-color: #0B2161;">
                </div>

            </div>
        </div>
    </div>

       <div id="myModal" class="modal fade" role="dialog" >
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Hermes | Seguros</h4>
                    </div>
                    <div class="modal-body">
                        <div class="msgcentrado">
                            <asp:Label Text="" ID="txtmensaje" runat="server" Style="font-size: 18px"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                               <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
   
    </form>
</body>
</html>
