using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Demo
{
    /// <summary>
    /// Summary description for UploadPlantilla
    /// </summary>
    public class UploadPlantilla : IHttpHandler
    {

        private List<Plantilla_Detalle> list_plantilla_detalle = new List<Plantilla_Detalle>();
        private Plantilla_Detalle plantilla_detalle;
        private Plantilla plantilla = new Plantilla();
        
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            context.Response.Expires = -1;

            try

            {
                string tempPath = "";               
                string txt_descripcion = context.Request["txt_descripcion"];
                string FiledataHTMLName = context.Request["FiledataHTMLName"];
                HttpFileCollection uploadFiles = context.Request.Files;
                plantilla.descripcion = txt_descripcion;
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    
                    HttpPostedFile postedFile = uploadFiles[i];
                    if (postedFile.FileName.Equals(FiledataHTMLName))
                    {
                        tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"]+"/PlantillaHtml";
                        string savepath = "";
                        savepath = context.Server.MapPath(tempPath);
                        if (!Directory.Exists(savepath))
                            Directory.CreateDirectory(savepath);

                        string filename = postedFile.FileName;
                        string files = savepath + @"\" + filename;
                        postedFile.SaveAs(files); 
                        plantilla.NombreArchivoHtml = filename;
                        plantilla.id_estado = 1;
                        plantilla.ruta_plantilla_html = files;
                    }
                    else
                    {
                        plantilla_detalle = new Plantilla_Detalle();
                        tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"] + "/Imagenes";
                        string savepath = "";
                        savepath = context.Server.MapPath(tempPath);
                        if (!Directory.Exists(savepath))
                            Directory.CreateDirectory(savepath);

                        string filename = postedFile.FileName;
                        string files = savepath + @"\" + filename;
                        postedFile.SaveAs(files);
                        plantilla_detalle.NombreArchivoImagen = filename;
                        plantilla_detalle.ruta_imagen = files;
                        plantilla_detalle.id_estado = 1;
                        list_plantilla_detalle.Add(plantilla_detalle);
                    }                    
                }
                plantilla.list_plantilla_detalle = list_plantilla_detalle;
                ClientResponse response;               
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    response = dbPlanilla.InsertPantilla(plantilla);
                }               
               
                //if ((System.IO.File.Exists(files)))
                //{
                //    System.IO.File.Delete(files);
                //}

                var result = new
                {
                    Result = response.Status,
                    Mensaje = response.Mensaje
                };
                context.Response.Write(JsonConvert.SerializeObject(result));

            }

            catch (Exception ex)

            {
                var result = new
                {
                    Result = "Error",
                    Mensaje = ex.Message
                };
                context.Response.Write(JsonConvert.SerializeObject(result));
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}