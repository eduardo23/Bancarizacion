<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="Demo.Usuario.RegistrarUsuario" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="Captcha" %>
<!DOCTYPE html>
<html lang="en">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="Css/standar.css" rel="stylesheet" />
    <title>Hermes | Seguros</title>
<script>

    
    $(document).ready(function () {
        $("#<%=txtCorreoElectronicoConfirmar.ClientID %>").on('paste', function (e) {
            e.preventDefault();
        })
        $("#<%=txtCorreoElectronicoConfirmar.ClientID %>").on('copy', function (e) {
            e.preventDefault();
        })
    });

   <%-- $(document).ready(function () {
    varid = $("#<%=ddlTipoDoc.ClientID %>").on('change', function () {
        if ($(varid).val() == 1) {
           $("#<%=txtnumerodoc.ClientID %>").val('');
          $("#<%=txtnumerodoc.ClientID %>").attr('maxlength', 8);
           
        } else if ($(varid).val() == 4) {
             $("#<%=txtnumerodoc.ClientID %>").val('');
      $("#<%=txtnumerodoc.ClientID %>").attr('maxlength', 11);
        } else if ($(varid).val() == 5) {
             $("#<%=txtnumerodoc.ClientID %>").val('');
        $("#<%=txtnumerodoc.ClientID %>").attr('maxlength', 9);
        } else {
             $("#<%=txtnumerodoc.ClientID %>").val('');
                 $("#<%=txtnumerodoc.ClientID %>").attr('maxlength', 50);
        }
    });
    });--%>
    //function validNumericos(evt) {

    //    if ($(varid).val() == 1) {

    //        var charCode = (evt.which) ? evt.which : event.keyCode
    //        if (((charCode == 8) || (charCode == 46)
    //        || (charCode >= 35 && charCode <= 40)
    //            || (charCode >= 48 && charCode <= 57)
    //            || (charCode >= 96 && charCode <= 105))) {

    //            return true;
    //        }
    //        else {
    //            return false;
    //        }
    //    } else if ($(varid).val() == 4) {

    //        var charCode = (evt.which) ? evt.which : event.keyCode
    //        if (((charCode == 8) || (charCode == 46)
    //        || (charCode >= 35 && charCode <= 40)
    //            || (charCode >= 48 && charCode <= 57)
    //            || (charCode >= 96 && charCode <= 105))) {

    //            return true;
    //        }
    //        else {
    //            return false;
    //        }
    //    }

    //}
</script>
<script language="C#" runat="server">

    void LinkButton_Click(Object sender, EventArgs e)
    {        
        string jss = "openModalTerminos();";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
    }

</script>
    <style>
    #lista2 {
    counter-reset: li; 
    list-style: none; 
    *list-style: decimal; 
    font: 15px 'trebuchet MS', 'lucida sans';
    padding: 0;
    margin-bottom: 4em;
    text-shadow: 0 1px 0 rgba(255,255,255,.5);
}

#lista2 ol {
    margin: 0 0 0 2em; 
}

#lista2 li{
    position: relative;
    display: block;
    padding: .4em .4em .4em 2em;
    *padding: .4em;
    margin: .5em 0;
    background: #ddd;
    color: #444;
    text-decoration: none;
    border-radius: .3em;
    transition: all .3s ease-out;   
}

#lista2 li:hover{
    background: #eee;
}

#lista2 li:hover:before{
    transform: rotate(360deg);  
}

#lista2 li:before{
    content: counter(li);
    counter-increment: li;
    position: absolute; 
    left: -1.3em;
    top: 50%;
    margin-top: -1.3em;
    background: #87ceeb;
    height: 2em;
    width: 2em;
    line-height: 2em;
    border: .3em solid #fff;
    text-align: center;
    font-weight: bold;
    border-radius: 2em;
    transition: all .3s ease-out;
}
</style>
    <script>
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
<script>
        function openModalTerminos() {
            $('#pnlTerminos').modal('show');
        }
    </script>
 </head>


<body style="background-color: #273746">
    <form id="frmRegistro" runat="server" >    
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>       
  <div class="row" style="background-color:#273746;height:50px" >   
  </div>
  <div class="container-fluid" style="background-color:#273746" >
    <div class="form-horizontal" style="background-color:#273746">
           <div class="row" style="background-color:#273746">              
               <div class="col-sm-2" style="padding:2px">
               </div>

               <div class="col-sm-4" style="background-color:#273746;">
                   <div class="panel panel-primary">
                       <div class="panel-heading" style="background-color: #FF8000;">
                           <b>Instrucciones</b>
                       </div>
                       <div class="panel-body" style="height:608px; background-color:#D6EAF8">
                           <br /><br /> 
                           <div class="col-sm-12">
                               <asp:Image runat="server" src="images/logoHermes.png" Width="220px"></asp:Image>
                           </div>
                          
                           <div class="container-fluid" style="padding: 25px">
                                <%--<h5>&nbsp; </h5>--%>
                                <h5>&nbsp; </h5>

<%--                               <ol id="lista2">
                                            <li> Ingrese todos los campos obligatorios, aquellos que tenga el siguiente carácter </li>
                                            <li>Lea y acepte los términos y condiciones</li>
                                            <li>Haga click sobre el botón <strong>Registrar</strong></li>
                                           <li>¡ <strong>Atento</strong> ! Recibirá un mensaje con un link: "<strong>Afiliación Web</strong>", al correo  electrónico registrado</li>
                                           <li>Debe ingresar al link <strong>Afiliación Web</strong>" para confirmar su cuenta.</li>                                           
                                </ol>--%>
                               <ol id ="lista2">
                                   <li>Ingrese todos los campos del formulario.</li>
                                   <li>Lea y acepte los términos y condiciones.</li>
                                   <li>Haga click sobre el botón Registrar.</li>
                                   <li>Por favor ingresar el Código de afiliación web de la Institución Educativa.</li>
                                   <li>Seleccionar el tipo de Seguro que desee contratar.</li>
                                   <li>Completar el(los) formulario(s) con los datos requeridos, y darle click al botón finalizar.</li>
                                   <li>Si todos los datos ingresados se encuentran correctamente, click en Confirmar.</li>
                                   <li>Recibirás a través de tu correo electrónico (archivo ajunto) los códigos generados impresos en un PDF.</li>
                                   <li><strong>¡Listo!</strong>, Usted ya puede realizar el pago del seguro con los códigos generados en las entidades bancarias indicadas en el recibo.</li>
                               </ol>
                               <%--<h5 style="text-align: center">¡Listo ! ya es usuario, ya puede iniciar sesión</h5>
                               <br />  <br />--%>
                           </div>
                       </div>
                   </div>
               </div>


        <div class="col-sm-4" style="background-color: #273746;">
                   <div class="panel panel-primary">
                       <div class="panel-heading" style="background-color: #FF8000;">
                           <b>Registro de Usuario</b>
                       </div>
                       <div class="panel-body">
                           <div class="form-group">
                               <label for="RazonSocial" class="col-lg-12 control-label" style="text-align: left">INGRESE SUS DATOS PERSONALES</label>
                           </div>

                           <div class="form-group">
                               <%--<label for="RazonSocial" class="col-lg-2 control-label">Apellido paterno</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtapepaterno" runat="server"  placeholder="ESCRIBA SU APELLIDO PATERNO" class="form-control" Width="100%" required/>
                               </div>
                           </div>

                           <div class="form-group">
                               <%--  <label for="RazonSocial" class="col-lg-2 control-label">Apellido Materno</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtapematerno" runat="server"  placeholder="ESCRIBA SU APELLIDO MATERNO" class="form-control" Width="100%" required/>
                               </div>
                           </div>

                           <div class="form-group">
                               <%--      <label for="RazonSocial" class="col-lg-2 control-label">Nombres</label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtnombres" runat="server" required="true" placeholder="ESCRIBA SU NOMBRE" class="form-control" Width="100%"></asp:TextBox>
                               </div>
                           </div>

                           <%--<div class="form-group">--%>
                               <%--       <label for="RazonSocial" class="col-lg-2 control-label">Tipo de documento  </label>--%>
                               <%--<div class="col-lg-12">
                                   <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="combo" Width="100%" AppendDataBoundItems="True" >
                                       <asp:ListItem Selected="True" Value="0">SELECCIONE TIPO DOCUMENTO</asp:ListItem>
                                   </asp:DropDownList>
                               </div>
                           </div>--%>

                           <%--<div class="form-group">--%>
                               <%-- <label for="RazonSocial" class="col-lg-2 control-label">Numero de documento </label>--%>
                               <%--<div class="col-lg-12">
                                   <asp:TextBox ID="txtnumerodoc" runat="server" placeholder="ESCRIBA SU NÚMERO DE DOCUMENTO" class="form-control" Width="100%" onkeydown="return validNumericos(event)" />
                               </div>
                           </div>--%>

                           <div class="form-group">
                               <%--<label for="RazonSocial" class="col-lg-2 control-label">Correo electrónico  </label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtCorreoElectronico" runat="server" Type="email" required="true" placeholder="ESCRIBA SU CORREO ELECTRÓNICO" class="form-control" Width="100%"></asp:TextBox>
                               </div>
                           </div>

                           <div class="form-group">
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtCorreoElectronicoConfirmar" runat="server" Type="email" required="true" placeholder="CONFIRME SU CORREO ELECTRÓNICO" class="form-control" Width="100%"></asp:TextBox>
                               </div>
                           </div>

                           <div class="form-group">
                               <%--  <label for="RazonSocial" class="col-lg-2 control-label">Contraseña </label>--%>
                               <div class="col-lg-12">
                                   <asp:TextBox ID="txtcontraseña" runat="server" placeholder="ESCRIBA SU CONTRASEÑA" class="form-control"  TextMode="Password" Width="100%"  pattern=".{5,20}" required title="Se requiere 5 caractéres como minimo"></asp:TextBox>
                               </div>
                           </div>

                           <%--<div class="form-group">--%>
                               <%--   <label for="RazonSocial" class="col-lg-2 control-label">Confirmar contraseña </label>--%>
                               <%--<div class="col-lg-12">
                                   <asp:TextBox ID="txtconfirmar" runat="server" TextMode="Password" required="true" placeholder="CONFIRME SU CONTRASEÑA" class="form-control" Width="100%"></asp:TextBox>
                               </div>
                           </div>--%>

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
                                   <asp:CheckBox ID="chkAcepto" runat="server" Text="Acepto los terminos y condiciones" />                                   
                                   <asp:LinkButton ID="lbtnTerminos" runat="server" OnClick="LinkButton_Click"><span class="glyphicon glyphicon-info-sign"></span></asp:LinkButton>
                                   
                               </div>
                               <div class="col-lg-12">
                                <asp:CheckBox ID="chkPublicidad" runat="server" Text="Aceptar publicidad" />
                              </div>
                           </div>

                           <div class="form-group">
                               <%--   <label for="RazonSocial" class="col-lg-2 control-label"></label>--%>
                               <div class="col-lg-12" style="text-align:right">
                                   <asp:Button ID="btnAtras" runat="server" Text="Atras" class="button"  Style="background-color:orangered" OnClick="btnAtras_Click"  formnovalidate />
                                   <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" class="button" OnClick="btnRegistrar_Click" />
                               </div>
                           </div>
                                                                                 
                       </div>
                   </div>
               </div>
               
               
               <div class="col-sm-2" style="padding:10px">               
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
         
        
       <div id="pnlTerminos" class="modal fade" role="dialog">
       <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #D6EAF8">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h5 class="modal-title msgcentrado"><b>Autorización para el Tratamiento de Datos Personales y Envío de Información</b></h5>
                    </div>
                    <div class="modal-body">
                        <div>
                            <p align="justify" style="padding:10px">Autorizo de forma voluntaria, previa, expresa, inequívoca e informada, el tratamiento 
                                y transferencia de mis datos personales al banco de datos de titularidad de Hermes 
                                Corredores de Seguros a nivel nacional. Hermes Corredores de Seguros utilizará los 
                                datos personales proporcionados, conjuntamente con otros que se pongan a su 
                                disposición durante la relación comercial, y con aquellos obtenidos en fuentes
                                 accesibles al público, con la finalidad de evaluar la calidad del servicio. Asimismo,
                                 Hermes Corredores de Seguros utilizará los datos personales con fines publicitarios
                                 y comerciales a fin de remitir al Cliente información sobre productos y servicios 
                                en el mercado financiero y de seguros que considere de su interés. El cliente 
                                reconoce y acepta que Hermes Corredores de Seguros podrá encargar el tratamiento 
                                de los datos personales a un tercero, y que se podrá realizar un procesamiento 
                                automatizado o no con dichos terceros por temas técnicos o comerciales. Los datos
                                 proporcionados serán incorporados, con las mismas finalidades a las bases de datos
                                 de empresas subsidiarias, filiales, asociadas, afiliadas o miembros del Grupo 
                                Económico al cual pertenece y/o terceros con los que éstas mantengan una relación 
                                contractual. Los datos suministrados por el cliente son esenciales para las 
                                finalidades indicadas. Las bases de datos donde se almacena la información cuentan
                                 con estrictas medidas de seguridad. En caso el cliente decida no proporcionarlos,
                                 no será posible la prestación de servicios por parte de la Hermes Corredores de 
                                Seguros.</p>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                    </div>
                </div>
            </div>
    </div>   
    </form>
</body>
</html>
