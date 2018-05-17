using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO_Hermes.ViewModel;
using System.Data.SqlClient;
using System.Data;
namespace DAO_Hermes.Repositorios
{
    public   class MonedaDAO:IDisposable
    {
    
            
        BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities();

        public List<Moneda> Listar()
        {
            List<Moneda> Listado = new List<Moneda>();
            
            foreach(Moneda mon in db.Moneda.ToList())
                {
                Moneda moneda = new Moneda();
                moneda.ID = mon.ID;
                moneda.Nombre = mon.Nombre.ToUpper();
                Listado.Add(moneda);
            }
            
            return Listado;
        }

        public List<Moneda> getLstbyInstAsegProd(Int32 InstId, Int32 CiaId, Int32 ProdId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Moneda_getLstbyInstAsegProd", cn))
                {
                    cmd.Parameters.AddWithValue("@InstId", InstId);
                    cmd.Parameters.AddWithValue("@CiaId", CiaId);
                    cmd.Parameters.AddWithValue("@ProdId", ProdId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Moneda> LstMon = new List<Moneda>();
                        while (dr.Read())
                        {
                            Moneda oMon = new Moneda();
                            oMon.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oMon.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            LstMon.Add(oMon);
                        }
                        return LstMon;
                    }
                }
            }
        }

        //public List<USP_LISTARMONEDA_Result> ListarMoneda()
        //{
        //    using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
        //    {
        //        return db.USP_LISTARMONEDA().ToList();
        //    }
        //}


        public List<Tipo_Moneda> ListarMoneda()
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("USP_LISTARMONEDA", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Tipo_Moneda> ListarMoneda = new List<Tipo_Moneda>();
                        while (dr.Read())
                        {
                            Tipo_Moneda Moneda = new Tipo_Moneda();
                            Moneda.ID = Convert.ToInt32(dr["ID"]);
                            Moneda.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            Moneda.Simbolo = Convert.ToString(dr["Simbolo"]);
                            Moneda.Estado = Convert.ToInt32(dr["Estado"]);
                            ListarMoneda.Add(Moneda);
                        }
                        return ListarMoneda;
                    }
                }
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~MonedaDAO() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
