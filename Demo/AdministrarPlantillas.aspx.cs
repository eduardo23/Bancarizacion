using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Newtonsoft.Json;
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



        [WebMethod]
        public static ClientResponse listarImagenBD(int id_plantilla)
        {
            ClientResponse response = new ClientResponse(); 
            try
            {
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    List<Plantilla_Detalle> list = dbPlanilla.getPlantillaDetalle(id_plantilla);
                    response.Id = id_plantilla;
                    response.DataJson = JsonConvert.SerializeObject(list).ToString();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse EliminarImagenBD(int id_plantilla, int id_det_plantilla)
        {
            ClientResponse response;
            try
            {
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {

                    Plantilla_Detalle oCheque = dbPlanilla.getPlantillaDetalle(id_plantilla).Where(p => p.id == id_det_plantilla).FirstOrDefault();
                    FileInfo fi = new FileInfo(oCheque.ruta_imagen);
                    fi.Delete();     
                    response = dbPlanilla.EliminarImgen_DetallePlantilla(id_plantilla, id_det_plantilla);
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
                    //response = dbPlanilla.getPlantillaXId_Anulacion(plantilla);

                    //Plantilla desafiliacion = Newtonsoft.Json.JsonConvert.DeserializeObject<Plantilla>(response.DataJson);
                    ////Eliminamos las el html adjuntado
                    //Char delimiter = '\\';
                    //string[] arrayruta_plantilla = desafiliacion.ruta_plantilla_html.Split(delimiter);
                    //List<string> listStr = arrayruta_plantilla.ToList();
                    //int index = 0;
                    //string ruta = string.Empty;

                    //if (listStr.Count() > 0) {
                    //    index = listStr.Count() - 1;
                    //    listStr.RemoveAt(index);
                    //    ruta = string.Empty;
                    //    listStr.ForEach(delegate (string cadena)
                    //    {
                    //        ruta += cadena + "\\";

                    //    });
                    //    Directory.Delete(ruta, true);
                    //}

                    ////Eliminamos las imagenes adjuntadas
                    //arrayruta_plantilla = desafiliacion.list_plantilla_detalle[0].ruta_imagen.Split(delimiter);
                    //listStr = arrayruta_plantilla.ToList();
                    //if (listStr.Count() > 0) {
                    //    index = listStr.Count() - 1;
                    //    listStr.RemoveAt(index);
                    //    ruta = string.Empty;
                    //    listStr.ForEach(delegate (string cadena)
                    //    {
                    //        ruta += cadena + "\\";

                    //    });
                    //    Directory.Delete(ruta, true);
                    //}
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