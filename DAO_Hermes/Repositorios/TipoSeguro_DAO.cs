using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_Hermes.Repositorios
{
    public class TipoSeguro_DAO: IDisposable
    {
        public List<Producto> getLstbyInstAseg(Int32 InstId, Int32 CiaId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Producto_getLstbyInstAseg", cn))
                {
                    cmd.Parameters.AddWithValue("@InstId", InstId);
                    cmd.Parameters.AddWithValue("@CiaId", CiaId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Producto> LstProd = new List<Producto>();
                        while (dr.Read())
                        {
                            Producto oProducto = new Producto();
                            oProducto.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oProducto.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            LstProd.Add(oProducto);
                        }
                        return LstProd;
                    }
                }
            }
        }

        public List<Producto> getLstbyCamp(Int32 CampId)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Producto_getLstbyCamp", cn))
                {
                    cmd.Parameters.AddWithValue("@CampaniaID", CampId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Producto> LstProd = new List<Producto>();
                        while (dr.Read())
                        {
                            Producto oProducto = new Producto();
                            oProducto.ID = Convert.ToInt32(dr["ID"] == DBNull.Value ? 0 : dr["ID"]);
                            oProducto.Nombre = Convert.ToString(dr["Nombre"] == DBNull.Value ? "" : dr["Nombre"]);
                            LstProd.Add(oProducto);
                        }
                        return LstProd;
                    }
                }
            }
        }

        public List<Producto> Listar()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.Producto.ToList();
            }        
        }

        public Int32 Anular(int id, string estado)
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                var ed = db.Producto.Find(id);
                if (ed != null)
                {
                    if (estado == "ACTIVO")
                    {
                        ed.Estado = false;
                    }
                    else
                    {
                        ed.Estado = true;
                    }
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    throw new Exception("No existe el registro con el id:" + id);
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
        // ~TipoSeguro_DAO() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
