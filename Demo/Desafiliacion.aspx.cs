using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class Desafiliacion : System.Web.UI.Page
    {
        public static string tokens="";
        protected void Page_Load(object sender, EventArgs e)
        {
            tokens = Request.QueryString["tokens"];
        }
        [WebMethod]
        public static ClientResponse dardeBaja()
        {
            ClientResponse response;
            try
            {
                GestionCorreo objeto = new GestionCorreo() { Tokens = tokens };

                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.DarDebajaEnviocorreos(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

    }
}