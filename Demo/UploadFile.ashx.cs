using DAO_Hermes.Repositorios;
using DAO_Hermes.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
namespace Demo
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        private List<GestionCorreo> listgestioncorreo;
        private GestionCorreo entidad;
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            context.Response.Expires = -1;

            try

            {

                HttpPostedFile postedFile = context.Request.Files["Filedata"];

                listgestioncorreo = new List<GestionCorreo>();


                string savepath = "";

                string tempPath = "";

                tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];

                savepath = context.Server.MapPath(tempPath);

                string filename = postedFile.FileName;

                if (!Directory.Exists(savepath))

                    Directory.CreateDirectory(savepath);


                string files = savepath + @"\" + filename;
                postedFile.SaveAs(files);


                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                Excel.Range range;

                string str;
                int rCnt;
                int cCnt;
                int rw = 0;
                int cl = 0;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(files, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;

                ClientResponse response;
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.getGrupoCorreoCombo();
                }
                List<GrupoCorreo> list = JsonConvert.DeserializeObject<List<GrupoCorreo>>(response.DataJson);

                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    if (rCnt > 1)
                    {
                        entidad = new GestionCorreo();
                        for (cCnt = 1; cCnt <= cl; cCnt++)
                        {
                            if (cCnt == 1)
                            {
                                GrupoCorreo grupocorreo = new GrupoCorreo();
                                grupocorreo.descripcion = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                                entidad.grupocorreo = grupocorreo;
                                //Validamos si el grupo de correo esta registrado en la base de datos   
                                grupocorreo.origen = 2;//Carga de Correo externo
                                grupocorreo.estado = 1;
                                GrupoCorreo objeto = list.Where(i => i.descripcion.ToUpper() == grupocorreo.descripcion.ToUpper()).FirstOrDefault();
                                if (objeto != null)
                                {
                                    grupocorreo.id = objeto.id;
                                }
                            }
                            if (cCnt == 2)
                            {
                                entidad.Nombre1 = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            }
                            if (cCnt == 3)
                            {
                                entidad.Nombre2 = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            }
                            if (cCnt == 4)
                            {
                                entidad.ApePaterno = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            }
                            if (cCnt == 5)
                            {
                                entidad.ApeMaterno = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            }
                            if (cCnt == 6)
                            {
                                entidad.Email = str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                            }
                        }
                        entidad.id_estado = 1;
                        listgestioncorreo.Add(entidad);
                    }
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);



                if ((System.IO.File.Exists(files)))
                {
                    System.IO.File.Delete(files);
                }

                if (listgestioncorreo.Count() > 0) {
                    ClientResponse response1;
                    using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                    {
                        response1 = dbGestionCorreo.InsertGestionCorreoAutomatico(listgestioncorreo);
                    }
                }
                     

                var wapper = new
                {
                    Result = "Ok",
                    Mensaje = "Se cargaron correctaente la trama."
                };
                context.Response.Write(JsonConvert.SerializeObject(wapper));

            }

            catch (Exception ex)

            {
                var wapper = new
                {
                    Result = "Error",
                    Mensaje = ex.Message
                };
                context.Response.Write(JsonConvert.SerializeObject(wapper));
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