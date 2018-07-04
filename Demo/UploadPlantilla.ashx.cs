using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
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
            //string ruta_hota_name = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            try

            {
                string hora = DateTime.Now.ToString("yyyyMMddhhmmss");
                string tempPath = "";
                string txt_descripcion = context.Request["txt_descripcion"];
                string FiledataHTMLName = context.Request["FiledataHTMLName"];
                HttpFileCollection uploadFiles = context.Request.Files;
                plantilla.descripcion = txt_descripcion;

                //string FolderPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];

                ClientResponse responserutahtml;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responserutahtml = dbParametrosMaestro.getObjParametroMaestro("RUTA_CARGAHTMLPLANTILLACORREOS");
                }
                ParametrosMaestros rutahtml = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responserutahtml.DataJson);

     
                ClientResponse responserutaimage;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responserutaimage = dbParametrosMaestro.getObjParametroMaestro("RUTA_CARGAIMAGENPLANTILLACORREOS");
                }
                ParametrosMaestros rutaimage = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responserutaimage.DataJson);

                ClientResponse responserutasiteimage;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responserutasiteimage = dbParametrosMaestro.getObjParametroMaestro("RUTA_SITE_CARGAIMAGENPLANTILLACORREOS");
                }
                ParametrosMaestros rutasiteimage = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responserutasiteimage.DataJson);



                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    HttpPostedFile postedFile = uploadFiles[i];
                    if (postedFile.FileName.Equals(FiledataHTMLName))
                    {
                        tempPath = rutahtml.valor + hora;
                        string savepath = "";
                        // savepath = context.Server.MapPath(tempPath);
                        savepath = tempPath;// context.Server.MapPath(tempPath);
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
                        tempPath =  rutaimage.valor + hora;
                        string savepath = "";
                        //savepath = context.Server.MapPath(tempPath);
                        savepath = tempPath;// context.Server.MapPath(tempPath);
                        if (!Directory.Exists(savepath))
                            Directory.CreateDirectory(savepath);

                        string filename = postedFile.FileName;
                        string files = savepath + @"\" + filename;
                        postedFile.SaveAs(files);
                        plantilla_detalle.NombreArchivoImagen = filename;
                        plantilla_detalle.ruta_imagen = files;
                        plantilla_detalle.id_estado = 1;
                        // plantilla_detalle.ruta_site_imagen = ruta_hota_name + "/" + FolderPath + rutaimage.valor + hora + "/" + filename;
                        plantilla_detalle.ruta_site_imagen = rutasiteimage.valor + hora + "/" + filename;
                        list_plantilla_detalle.Add(plantilla_detalle);
                    }
                }

                plantilla.list_plantilla_detalle = list_plantilla_detalle;
                
                //Replace SRC  plantilla html
                HtmlDocument document = new HtmlDocument();
                document.Load(plantilla.ruta_plantilla_html);
                document.DocumentNode.Descendants("img")
                            .Where(e =>
                            {
                                string src = e.GetAttributeValue("src", null) ?? "";
                                return !string.IsNullOrEmpty(src);// && src.StartsWith("data:image");
                            })
                            .ToList()
                            .ForEach(x =>
                            {
                                string currentSrcValue = string.Empty;
                                currentSrcValue = x.GetAttributeValue("src", null);
                                Plantilla_Detalle objeto = plantilla.list_plantilla_detalle.Where(i => i.NombreArchivoImagen.ToUpper().Equals(currentSrcValue.ToUpper())).FirstOrDefault();
                                if(objeto !=null)
                                {
                                    x.SetAttributeValue("src", objeto.ruta_site_imagen);
                                }
                                
                            });
                document.Save(plantilla.ruta_plantilla_html);

                document.DocumentNode.Descendants("a")
                            .Where(e =>
                            {
                                string src = e.GetAttributeValue("href", null) ?? "";
                                return !string.IsNullOrEmpty(src);// && src.StartsWith("data:image");
                            })
                            .ToList()
                            .ForEach(x =>
                            {
                                string currentSrcValue = string.Empty;
                                currentSrcValue = x.GetAttributeValue("href", null);                                

                                if (currentSrcValue != "{linkdardebaja}" && !currentSrcValue.Contains("http")) {
                                    Plantilla_Detalle objeto = plantilla.list_plantilla_detalle.Where(i => i.NombreArchivoImagen.ToUpper().Equals(currentSrcValue.ToUpper())).FirstOrDefault();
                                    if (objeto != null)
                                    {
                                        x.SetAttributeValue("href", objeto.ruta_site_imagen);
                                    }
                                }

                            });
                document.Save(plantilla.ruta_plantilla_html);

                HtmlDocument document1 = new HtmlDocument();
                document1.Load(plantilla.ruta_plantilla_html);
                plantilla.fl_parrafo = 0;
                try
                {
                    //List<HtmlNode> list = document1.DocumentNode.SelectNodes("p").ToList();
                    //plantilla.fl_parrafo = list.Count > 0 ? 1 : 0;
                    document.DocumentNode.Descendants("p")
                                .Where(e =>
                                {
                                    string src = e.GetAttributeValue("type", null) ?? "";
                                    return !string.IsNullOrEmpty(src);
                                })
                                .ToList()
                                .ForEach(x =>
                                {
                                    string currentSrcValue = string.Empty;
                                    currentSrcValue = x.GetAttributeValue("type", null);

                                    if (currentSrcValue == "por ingresar")
                                    {
                                        plantilla.fl_parrafo = 1;
                                    }
                                });

                }
                catch (Exception)
                {
                    plantilla.fl_parrafo = 0;
                }

                ClientResponse response;
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    response = dbPlanilla.InsertPantilla(plantilla);
                }
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