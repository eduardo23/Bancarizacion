using DAO_Hermes.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class AdministrarPlantillas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static ClientResponse getPlanilla()
        {
            ClientResponse response;
            try
            {
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    response = dbPlanilla.getPlantilla();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse AnularPlantilla(int plantilla)
        {
            ClientResponse response;
            try
            {
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    response = dbPlanilla.AnularPlantilla(plantilla);
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