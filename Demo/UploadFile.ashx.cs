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

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            context.Response.Expires = -1;

            try

            {

                HttpPostedFile postedFile = context.Request.Files["Filedata"];



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


                for (rCnt = 1; rCnt <= rw; rCnt++)
                {
                    for (cCnt = 1; cCnt <= cl; cCnt++)
                    {
                        str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                       // MessageBox.Show(str);
                    }
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                //context.Response.Write(tempPath + "/" + filename);

                //context.Response.StatusCode = 200;

                //Excel.Application xlApp = new Excel.Application();
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(files);
                //Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                //Excel.Range xlRange = xlWorksheet.UsedRange;

                //int rowCount = xlRange.Rows.Count;
                //int colCount = xlRange.Columns.Count;

                ////iterate over the rows and columns and print to the console as it appears in the file
                ////excel is not zero based!!
                //for (int i = 1; i <= rowCount; i++)
                //{
                //    for (int j = 1; j <= colCount; j++)
                //    {
                //        //new line
                //        if (j == 1)
                //            Console.Write("\r\n");

                //        //write the value to the console
                //        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                //            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                //    }
                //}

                ////cleanup
                //GC.Collect();
                //GC.WaitForPendingFinalizers();

                var wapper = new
                {
                    query = "Ok" ,
                    suggestions = "" 
                };
                context.Response.Write(JsonConvert.SerializeObject(wapper));

            }

            catch (Exception ex)

            {

                context.Response.Write("Error: " + ex.Message);

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