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
using NsExcel = Microsoft.Office.Interop.Excel;


namespace Demo
{
    public partial class GestionarCorreos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
      
        [WebMethod]
        public static ClientResponse getGrupoCorreoCombo(int origen)
        {
            ClientResponse response;
            try
            {
                using (GrupoCorreoDAO dbGrupoCorreo = new GrupoCorreoDAO())
                {
                    response = dbGrupoCorreo.getGrupoCorreoCombo(origen);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse getListParametrosMaestro(string skey)
        {
            ClientResponse response;
            try
            {
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    response = dbParametrosMaestro.getLstParametroMaestro(skey);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse getExportarExcel(int id_cbo_origenfil, int id_cbo_grupo_consultar)
        {
            ClientResponse response;
            try
            {
                string hora = DateTime.Now.ToString("yyyyMMddhhmmss");
                ClientResponse responserutaexcel;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responserutaexcel = dbParametrosMaestro.getObjParametroMaestro("RUTA_DESCARGA_EXCEL");
                }
                ParametrosMaestros rutaexcel = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responserutaexcel.DataJson);


                ClientResponse responserutaexcelsite;
                using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                {
                    responserutaexcelsite = dbParametrosMaestro.getObjParametroMaestro("RUTA_SITE_DESCARGA_EXCEL");
                }
                ParametrosMaestros rutaexcelsite = Newtonsoft.Json.JsonConvert.DeserializeObject<ParametrosMaestros>(responserutaexcelsite.DataJson);

                if (Directory.Exists(rutaexcel.valor))
                    Directory.Delete(rutaexcel.valor, true);

                string savepath = rutaexcel.valor + hora;

                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.getExportarCorreos(id_cbo_origenfil, id_cbo_grupo_consultar);
                }
                List<GestionCorreo> listCorreos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GestionCorreo>>(response.DataJson);
               
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                // Create empty workbook
                excel.Workbooks.Add();
                // Create Worksheet from active sheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
                try
                {
                    // ------------------------------------------------
                    // Creation of header cells
                    // ------------------------------------------------
                    // ------------------------------------------------
                    // Creation of header cells
                    // ------------------------------------------------
                    workSheet.Cells[1, "A"] = "Grupo";
                    workSheet.Cells[1, "B"] = "Nombre 1";
                    workSheet.Cells[1, "C"] = "Nombre 2";
                    workSheet.Cells[1, "D"] = "Paterno";
                    workSheet.Cells[1, "E"] = "Materno";
                    workSheet.Cells[1, "F"] = "Email";
                    workSheet.Cells[1, "G"] = "Estado";
                    workSheet.Cells[1, "H"] = "Fecha Baja";
                    // ------------------------------------------------
                    // Populate sheet with some real data from "cars" list
                    // ------------------------------------------------
                    int row = 2; // start row (in row 1 are header cells)
                    foreach (GestionCorreo car in listCorreos)
                    {
                        workSheet.Cells[row, "A"] = car.grupocorreo.descripcion;
                        workSheet.Cells[row, "B"] = car.Nombre1;
                        workSheet.Cells[row, "C"] = car.Nombre2;
                        workSheet.Cells[row, "D"] = car.ApePaterno;
                        workSheet.Cells[row, "E"] = car.ApeMaterno;
                        workSheet.Cells[row, "F"] = car.Email;
                        workSheet.Cells[row, "G"] = car.descestado;
                        workSheet.Cells[row, "H"] = car.fechabaja;
                        row++;
                    }
                    // Apply some predefined styles for data to look nicely :)
                    workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);
                    // Define filename
                    string fileNamea = string.Format(@"{0}\ExcelData.xlsx", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                    string fileName = savepath + "\\" + "ExcelData.xlsx";
                    // Save this data as a file
                    workSheet.SaveAs(fileName);
                    // Display SUCCESS message
                    response.Mensaje = "Ok";
                    response.DataJson = rutaexcelsite.valor + hora + "/" + "ExcelData.xlsx";
                }
                catch (Exception exception)
                {
                    response.Mensaje = "Error";
                    response.DataJson = exception.Message;
                }
                finally
                {
                    // Quit Excel application
                    excel.Quit();

                    // Release COM objects (very important!)
                    if (excel != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                    if (workSheet != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                    // Empty variables
                    excel = null;
                    workSheet = null;

                    // Force garbage collector cleaning
                    GC.Collect();
                }

              
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        [WebMethod]
        public static ClientResponse getListGestionCorreo(int paginaActual, int RegistroXpagina, int id_cbo_origenfil, int id_cbo_grupo_consultar)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.getLstGestionCorreo(paginaActual, RegistroXpagina, id_cbo_origenfil, id_cbo_grupo_consultar);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

     
        [WebMethod]
        public static ClientResponse ImportarCorreos()
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.ImportarCorreos();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }


        [WebMethod]
        public static ClientResponse ActualiarGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.ActualiarGestionCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse EliminarGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.EliminarGestionCorreo(objeto);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
        [WebMethod]
        public static ClientResponse InsertGestionCorreo(GestionCorreo objeto)
        {
            ClientResponse response;
            try
            {
                using (GestionCorreoDAO dbGestionCorreo = new GestionCorreoDAO())
                {
                    response = dbGestionCorreo.InsertGestionCorreo(objeto);
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