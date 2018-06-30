using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Demo
{
    public partial class EnvioCorreo : System.Web.UI.Page
    {
        public static String UserId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnUserSession.Value = Session["Usuario"].ToString();
            }
        }

        [WebMethod]
        public static ClientResponse Preview(int plantilla)
        {
            ClientResponse response;
            try
            {
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    response = dbPlanilla.getPlantillaXId(plantilla);
                }
                Plantilla objetoplantilla = Newtonsoft.Json.JsonConvert.DeserializeObject<Plantilla>(response.DataJson);

                string body = string.Empty;
                using (StreamReader reader = new StreamReader(objetoplantilla.ruta_plantilla_html))
                {
                    body = reader.ReadToEnd();
                }
                response.ViewResult = body;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
    }
}