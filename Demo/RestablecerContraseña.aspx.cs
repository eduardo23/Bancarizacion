using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes;
using DAO_Hermes.ViewModel;
using System.Text;
using System.Data.SqlClient;

namespace Demo
{
    public partial class RestablecerContraseña : System.Web.UI.Page {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAtrasRest_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            Users oUser = new Users();           
            oUser.Email = txtusuarioNombreRest.Text;           
            EnviarMensajeEmailRest(oUser.Email, "Hermes Seguros - Confirmacion de Afiliacion");
            string jss2 = "openModal()";
            txtmensajeRest.Text = "Se ha enviado la contraseña a su Correo Electrónico";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", jss2, true);
            return;
        }

        public void EnviarMensajeEmailRest(string para, string asunto)
        {
            Usuario_DAO db = new Usuario_DAO();
            string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
            string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
            string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
            int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));

            string mensaje = "";          
            string url = Server.MapPath(@"\Templates\TemplateRestPassword.html");
            mensaje = cUtil.LeerTemplateHTML(url);           
            mensaje = mensaje.Replace("[usuario]", txtusuarioNombreRest.Text);
            mensaje = mensaje.Replace("[clave]", db.BuscarUsuarioPorEmailRest(txtusuarioNombreRest.Text));            

            mensaje = HttpUtility.HtmlDecode(mensaje);

            cUtil.EnvioMail(para, usuario, asunto, mensaje, null, true, clave, smtp, puerto);
        }
    }
}