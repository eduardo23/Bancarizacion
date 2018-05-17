using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System.Text;
using System.Data.SqlClient;

namespace Demo
{
    public partial class InicioSesion : System.Web.UI.Page
    {
        Usuario_DAO objn = new Usuario_DAO();
        LoguearUsuario obje = new LoguearUsuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Captcha1.ValidateCaptcha(txtCaptcha.Text.ToUpper());//Validando - CAPTCHA********************//**//***
            if (Captcha1.UserValidated)
            {
                Captcha1.Visible = true;
                obje.UserName = txtusuarioNombre.Text;
                obje.Password = txtpasswordUsuario.Text;
                List<LoguearUsuario> logueo = objn.Validar_Usuario(obje);
                if (logueo.Count > 0)
                {
                    if (logueo[0].IdUsuario == "00000000-0000-0000-0000-000000000001")//Administrador
                    {
                        Session["Usuario"] = logueo[0].NombreUsuario;
                        Session["Password"] = txtpasswordUsuario.Text;
                        Response.Redirect("afiliados.aspx");
                    }
                    else
                    {
                        string RolId = logueo[0].IdUsuario;
                        Session["Usuario"] = logueo[0].NombreUsuario;
                        Session["Password"] = txtpasswordUsuario.Text;
                        Response.Redirect("afiliados.aspx?Tipo=" + RolId);
                    }
                }
                else
                {
                    txtmensaje.Text = "El nombre de usuario o contraseña es incorrecta ";
                    string jss = "openModal()";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss, true);
                }
            }
            else
            {
                return;
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void lblOlvidaste_Click(object sender, EventArgs e)
        {
            Response.Redirect("RestablecerContraseña.aspx");
        }
    }
}