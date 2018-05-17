using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAO_Hermes;
using DAO_Hermes.Repositorios;
using System.Web.Script.Services;
using DAO_Hermes.ViewModel;

namespace Demo.Services
{
    /// <summary>
    /// Summary description for WS_ServiceHermes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public  class WS_ServiceHermes : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string[] GetInstitucionesEducativas(string prefix)
        {
            InstitucionEducativaDAO db = new InstitucionEducativaDAO();
            return db.ListarInstitucionesNombre(prefix).ToArray();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string[] GetUsuarios(string prefix)
        {
            InstitucionEducativaDAO db = new InstitucionEducativaDAO();
            return db.ListarInstitucionesNombre(prefix).ToArray();
        }


        [WebMethod]
        public List<Campañas> getLstCampana() {

            List<Campañas> LstCampanas = new List<Campañas>();

            CampañasDAO oCampDao = new CampañasDAO();
            LstCampanas = oCampDao.ListarCampañas();
            return LstCampanas;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Institucion_Educativa> getLstInstByCampana(Int32 CampanaId) {
            List<Institucion_Educativa> LstInst = new List<Institucion_Educativa>();
            using (InstitucionEducativaDAO db = new InstitucionEducativaDAO())
            {
                LstInst = db.getLstByCampania(CampanaId);                
            }
            return LstInst;
        }

        [WebMethod]
        public List<Cia_Seguro> getLstCiabyInst(Int32 InstId)
        {
            List<Cia_Seguro> LstCia = new List<Cia_Seguro>();
            using (CiaSeguro_DAO db = new CiaSeguro_DAO())
            {
                LstCia = db.getLstbyInst(InstId);
            }
            return LstCia;
        }

        [WebMethod]
        public List<Producto> getLstProdbyInstCia(Int32 InstId, Int32 CiaId)
        {
            List<Producto> LstProd = new List<Producto>();
            using (TipoSeguro_DAO db = new TipoSeguro_DAO())
            {
                LstProd = db.getLstbyInstAseg(InstId, CiaId);
            }
            return LstProd;
        }

        [WebMethod]
        public List<Moneda> getLstMonbyInstAsegProd(Int32 InstId, Int32 CiaId, Int32 ProdId)
        {
            List<Moneda> LstMon = new List<Moneda>();
            using (MonedaDAO db = new MonedaDAO())
            {
                LstMon = db.getLstbyInstAsegProd(InstId, CiaId,ProdId);
            }
            return LstMon;
        }

        [WebMethod]
        public List<Bancos> getLstBanco()
        {
            List<Bancos> LstBan = new List<Bancos>();
            using (BancoDAO db = new BancoDAO())
            {
                LstBan = db.ListarBanco();
            }
            return LstBan;
        }
    }
}
