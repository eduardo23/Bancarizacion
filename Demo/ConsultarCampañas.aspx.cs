using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
namespace Demo
{
    public partial class ConsultarCampañas : System.Web.UI.Page
    {
        Campañas campañasBe = new Campañas();
        CampañasDAO objn = new CampañasDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListarCampañas();
            }
        }


        private void ListarCampañas()
        {
            List<Campañas> ListarCampañas = objn.ListarCampañas();
            ddlCampañas.DataSource = ListarCampañas;
            ddlCampañas.DataTextField = "Nombre";
            ddlCampañas.DataValueField = "ID";
            ddlCampañas.DataBind();
            //ddlCampañas.Items.Insert(0, new ListItem("TODOS", "0"));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ddlCampañas.SelectedValue=="T")
            {
                campañasBe._ID =0;
                campañasBe.TIPO = Convert.ToString(ddlCampañas.SelectedValue.ToString());
                List<Campañas> ConsultarCampañas = objn.ConsultarCampañas(campañasBe);

                grConsultarCampañas.DataSource = ConsultarCampañas;
                grConsultarCampañas.DataBind();
            }
            else
            {          
               campañasBe._ID = Convert.ToInt32(ddlCampañas.SelectedValue.ToString());
               campañasBe.TIPO = Convert.ToString(ddlCampañas.SelectedItem.Text.ToString());
               List<Campañas> ConsultarCampañas = objn.ConsultarCampañas(campañasBe);

               grConsultarCampañas.DataSource = ConsultarCampañas;
               grConsultarCampañas.DataBind();
            }
        }
    }
}