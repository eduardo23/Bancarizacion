using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO_Hermes.Repositorios;
using System.Data;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Demo
{
    public partial class EnvioEmailMasivo : System.Web.UI.Page
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
            CargarInstituciones();
        }

        void CargarInstituciones()
        {
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
                {
                grvResultados.DataSource = db.ListarInstitucionesResumen(3);
                grvResultados.DataBind();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}








