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
                string tempPath = "";
                string cbo_origen = context.Request["cbo_origen"];
                string cbo_plantilla = context.Request["cbo_plantilla"];
                string txt_asunto = context.Request["txt_asunto"];
                //List<string> list = (List<string>)context.Request["list"];
                //string[] dataSource = context.Request.Form.Get("list").Split(',');
                string id_grupo_correo = context.Request.Form.Get("list");
                HttpFileCollection uploadFiles = context.Request.Files;
                List<string> listrutas = new List<string>();
                for (int i = 0; i < uploadFiles.Count; i++)
                {

                    HttpPostedFile postedFile = uploadFiles[i];
                    tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"] + "/PlantillaEnvioEmail";
                    string savepath = "";
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
                using (GestionCorreoDAO dbGestionCorreoDAO = new GestionCorreoDAO())
                {
                    response = dbGestionCorreoDAO.getLstGestionCorreoXGrupo(id_grupo_correo);
                }
                List<GestionCorreo> liscorreos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GestionCorreo>>(response.DataJson);
                cUtil.EnvioMailSegundo(txt_asunto, liscorreos, listrutas, usuario, clave, smtp, puerto);
                
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