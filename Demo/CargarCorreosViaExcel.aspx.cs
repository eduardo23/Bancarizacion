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
    public partial class CargarCorreosViaExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static ClientResponse CargarArchivo()
        {
            ClientResponse response = new ClientResponse() ;

            try
            {
                /*string Files = htp1.Items.ToString();
                HttpPostedFile f1 = htp1.Request.Files["file1"];*/
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                //using (ParametrosMaestrosDAO dbParametrosMaestro = new ParametrosMaestrosDAO())
                //{
                //    response = dbParametrosMaestro.getLstParametroMaestro(skey);
                //}
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
    }
}