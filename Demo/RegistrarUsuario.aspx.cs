using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;

namespace Demo.Usuario
{
    public partial class RegistrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                inicializar();
            }
        }
        void inicializar()
        {
            //cUtil.ListarTipoDocumentos(ddlTipoDoc);
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
           
            using (UserSeguridad_DAO db = new UserSeguridad_DAO())
            {
                try
                {
                    if (chkAcepto.Checked == false)
                    {
                        string jss2 = "openModal()";
                        txtmensaje.Text = "Debe activar la aceptación de terminos y condiciones";                                                
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);                        
                        return;
                    }

                    if (txtCorreoElectronico.Text != txtCorreoElectronicoConfirmar.Text)
                    {

                        txtmensaje.Text = "Verifique el correo electronico, no coinciden";
                        string jss10 = "openModal()";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss10, true);
                        return;
                    }

                    //if (txtcontraseña.Text != txtconfirmar.Text)
                    //{
                    //    //lblError.Text = "Las contraseñas proporcionadas no coinciden";
                    //    //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "message();", true);
                        
                    //    txtmensaje.Text = "Las contraseñas proporcionadas no coinciden";
                    //    string jss10 = "openModal()";
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss10, true);
                    //    return;
                    //}

                    Captcha1.ValidateCaptcha(txtCaptcha.Text.ToUpper());//Validando - CAPTCHA********************//**//***
                    if (Captcha1.UserValidated)
                    {
                        Captcha1.Visible = true;

                    
                        Users oUser = new Users();
                    
                        oUser.Id = GetUniqueKey(128);
                        oUser.ApellidoPaterno = txtapepaterno.Text;
                        oUser.ApellidoMaterno = txtapematerno.Text;
                        oUser.Nombre = txtnombres.Text;
                        oUser.Email = txtCorreoElectronico.Text;
                        oUser.UserName = txtCorreoElectronico.Text;
                        oUser.CustomPasswordHash = txtcontraseña.Text;
                        oUser.PasswordHash = txtcontraseña.Text;
                        oUser.EmailConfirmed = false;
                        //oUser.TipoDocumento = Convert.ToInt32(ddlTipoDoc.SelectedValue);
                        //oUser.NumeroDocumento = txtnumerodoc.Text;
                        oUser.PhoneNumberConfirmed = false;
                        oUser.TwoFactorEnabled = false;
                        oUser.LockoutEnabled = false;
                        oUser.AccessFailedCount = 0;
                        oUser.Discriminator = "ApplicationUser";
                        oUser.Password = txtcontraseña.Text; //txtconfirmar.Text;
                    
                        string iduser= db.Agregar(oUser);
                  
                        Session["usuAfiliacion"] = oUser.Email;
                        EnviarMensajeEmail(oUser.Email, "Hermes Seguros - Confirmacion de Afiliacion", oUser.Id);

                        //txtmensaje.Text = "Se ha enviado un correo de confirmación de usuario al correo electrónico registrado, por favor verifique e ingrese a través del LINK AFILIACIÓN WEB";
                        //string jss3 = "openModal()";
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss3, true);
                        //ROLES
                        string r = cUtil.ObtenerValorParametro("CLIENTE", "ROL");
                        UserRoles ur = new UserRoles();
                        ur.RoleId = r;
                        ur.UserId = iduser;
                        db.AgregarRol(ur);

                        //--------------------------------------------------------------------------------------
                        Usuario_DAO objn = new Usuario_DAO();
                        LoguearUsuario obje = new LoguearUsuario();

                        obje.UserName = txtCorreoElectronico.Text;
                        obje.Password = txtcontraseña.Text;
                        List<LoguearUsuario> logueo = objn.Validar_Usuario(obje);
                        if (logueo.Count > 0)
                        {
                            string RolId = logueo[0].IdUsuario;
                            Session["Usuario"] = logueo[0].NombreUsuario;
                            Session["Password"] = txtcontraseña.Text;
                            Response.Redirect("afiliados.aspx?Tipo=" + RolId);
                        }
                        //--------------------------------------------------------------------------------------
                        //FINROLES
                    }
                    else
                    {
                        string jss2 = "openModal()";
                        txtmensaje.Text = "El texto de la imagen ingresado es incorrecto.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    txtmensaje.Text = ex.Message;
                    string jss2 = "openModal()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
                }

            }
        }

        public void EnviarMensajeEmail(string para, string asunto,string userid)
        {
            string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
            string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
            string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
            int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));

            string mensaje = "";
            //string url = Server.MapPath("..") + @"\Templates\TemplateEmailAfiliacion.html";
            string url = Server.MapPath(@"\Templates\TemplateEmailAfiliacion.html");

            mensaje = cUtil.LeerTemplateHTML(url);
            mensaje = mensaje.Replace("[Nombre]", txtapepaterno.Text + " " + txtapematerno.Text + "," + txtnombres.Text);
            mensaje = mensaje.Replace("[usuario]", txtCorreoElectronico.Text);
            mensaje = mensaje.Replace("[clave]", txtcontraseña.Text);
            mensaje = mensaje.Replace("[userid]", userid);

            mensaje = HttpUtility.HtmlDecode(mensaje);
            cUtil.EnvioMail(para, usuario, asunto, mensaje, null, true, clave, smtp, puerto);
        }
        public string GetUniqueKey(int length)
        {
            string guidResult = string.Empty;

            while (guidResult.Length < length)
            {
                // Get the GUID.
                guidResult += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }

            // Make sure length is valid.
            if (length <= 0 || length > guidResult.Length)
                throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

            // Return the first length bytes.
            return guidResult.Substring(0, length);
        }

        protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTipoDoc_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}