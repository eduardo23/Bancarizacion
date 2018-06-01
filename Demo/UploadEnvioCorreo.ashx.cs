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
    /// Summary description for UploadEnvioCorreo
    /// </summary>
    public class UploadEnvioCorreo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try

            {
                string hora = DateTime.Now.ToString("yyyyMMddhhmmss");
                string tempPath = "";
                string savepath = "";
                int cbo_origen = 0;
                int cbo_plantilla = 0;
                int.TryParse(context.Request["cbo_origen"], out cbo_origen);
                int.TryParse(context.Request["cbo_plantilla"], out cbo_plantilla);
            
                string txt_asunto = context.Request["txt_asunto"];              
                string id_grupo_correo = context.Request.Form.Get("list");
                HttpFileCollection uploadFiles = context.Request.Files;
                List<string> listrutas = new List<string>();
               
                ClientResponse responseruta;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responseruta = dbParametrosMaestro.getObjParametroMaestro("RUTA_ADJUNTO_ENVIO_CORREOS");
                }
                ParametrosMaestros rutaadjunto = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responseruta.DataJson);
           
                
                
                for (int i = 0; i < uploadFiles.Count; i++)
                {

                    HttpPostedFile postedFile = uploadFiles[i];
                    tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"] + rutaadjunto.valor+ hora;

                    savepath = context.Server.MapPath(tempPath);
                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);

                    string filename = postedFile.FileName;
                    string files = savepath + @"\" + filename;
                    postedFile.SaveAs(files);
                    listrutas.Add(files);
                }

                ClientResponse response;
                string usuario = cUtil.ObtenerValorParametro("CORREO", "USER");
                string clave = cUtil.ObtenerValorParametro("CORREO", "CLAVE");
                string smtp = cUtil.ObtenerValorParametro("CORREO", "SMTP");
                int puerto = Convert.ToInt32(cUtil.ObtenerValorParametro("CORREO", "PUERTO"));


                ClientResponse responsedesafiliacion;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responsedesafiliacion = dbParametrosMaestro.getObjParametroMaestro("DESAFILACIONPROMOCIONES");
                }
                ParametrosMaestros desafiliacion = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responsedesafiliacion.DataJson);



                ClientResponse responseplantilla;
                using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                {
                    responseplantilla = dbPlanilla.getPlantillaXId(cbo_plantilla);
                }
                Plantilla objetoplantilla = Newtonsoft.Json.JsonConvert.DeserializeObject<Plantilla>(responseplantilla.DataJson);

                string body = string.Empty;
                using (StreamReader reader = new StreamReader(objetoplantilla.ruta_plantilla_html))
                {
                    body = reader.ReadToEnd();
                }
                body = "<div style='padding: 0px 220px; '>" + body + "</div>";
                using (GestionCorreoDAO dbGestionCorreoDAO = new GestionCorreoDAO())
                {
                    response = dbGestionCorreoDAO.getLstGestionCorreoXGrupo(id_grupo_correo);
                }
                List<GestionCorreo> liscorreos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GestionCorreo>>(response.DataJson);
                string bodyaux = string.Empty;
                foreach (GestionCorreo item in liscorreos)
                {
                    bodyaux = body.Replace("{NombreUsuario}", item.Nombre1 + " " + item.Nombre2);
                    bodyaux = bodyaux.Replace("{linkdardebaja}", desafiliacion.valor+"?tokens=" +item.Tokens);
                    cUtil.EnvioMailSegundo(txt_asunto, item.Email, bodyaux, listrutas, usuario, clave, smtp, puerto);
                }

                //Directory.Delete(savepath, true);

                //plantilla.list_plantilla_detalle = list_plantilla_detalle;
                //ClientResponse response;
                //using (PlantillaDAO dbPlanilla = new PlantillaDAO())
                //{
                //    response = dbPlanilla.InsertPantilla(plantilla);
                //}

                //if ((System.IO.File.Exists(files)))
                //{
                //    System.IO.File.Delete(files);
                //}

                var result = new
                {
                    Result = "Ok",
                    Mensaje = "Se envio correo"
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