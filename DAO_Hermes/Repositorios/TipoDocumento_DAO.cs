using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DAO_Hermes.Repositorios
{
    public class TipoDocumento_DAO : IDisposable
    {
        public List<TipoDocumento> Listar()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.TipoDocumento.Where(p => p.Estado == true).ToList();
            }
        }

        public List<TipoDocumento> ListarTodos()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.TipoDocumento.ToList();
            }
        }

        public List<Roles> ListarRol()
        {
            using (BDHermesBancarizacionEntities db = new BDHermesBancarizacionEntities())
            {
                return db.Roles.ToList();
            }
        }

        //kevin
        public List<TipoDocumento> ListarDocAccidente()
        {

            List<TipoDocumento> tdoc = new List<TipoDocumento>();

            //Crear una conexion
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                //Crear un comando para ejecutar un procedimiento almacenado
                //using (SqlCommand cmd = new SqlCommand("SELECT CODIGO, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOMBRE NOT IN ('RUC','LICENCIA DE CONDUCIR','BREVETE NACIONAL')", cn))
                using (SqlCommand cmd = new SqlCommand("SELECT CODIGO, NOMBRE FROM MAESTRO.TipoDocumento WHERE Estado=1", cn))
                {
                    //Definir el tipo de comando a ejecutar a Store procedure
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    //Abrir la conexion
                    cn.Open();
                    //Ejecutar el procedimiento y leer el registro del cliente encontrado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Leer el registro
                        while (dr.Read())
                        {
                            TipoDocumento Odoc = new TipoDocumento();
                            Odoc.Codigo = Convert.ToString(dr["Codigo"]);
                            Odoc.Nombre = dr["Nombre"].ToString();

                            tdoc.Add(Odoc);
                        }
                        //Devolver el objeto cliente encontrado
                        return tdoc;
                    }
                }
            }
        }

        public List<TipoDocumento> ListarDocAccidenteTodos()
        {

            List<TipoDocumento> tdoc = new List<TipoDocumento>();

            //Crear una conexion
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                //Crear un comando para ejecutar un procedimiento almacenado
                //using (SqlCommand cmd = new SqlCommand("SELECT CODIGO, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOMBRE NOT IN ('RUC','LICENCIA DE CONDUCIR','BREVETE NACIONAL')", cn))
                using (SqlCommand cmd = new SqlCommand("SELECT CODIGO, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOT CODIGO IS NULL", cn))
                {
                    //Definir el tipo de comando a ejecutar a Store procedure
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    //Abrir la conexion
                    cn.Open();
                    //Ejecutar el procedimiento y leer el registro del cliente encontrado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Leer el registro
                        while (dr.Read())
                        {
                            TipoDocumento Odoc = new TipoDocumento();
                            Odoc.Codigo = Convert.ToString(dr["Codigo"]);
                            Odoc.Nombre = dr["Nombre"].ToString();

                            tdoc.Add(Odoc);
                        }
                        //Devolver el objeto cliente encontrado
                        return tdoc;
                    }
                }
            }
        }

        public List<TipoDocumento> ListarDocRenta()
        {

            List<TipoDocumento> tdoc = new List<TipoDocumento>();

            //Crear una conexion
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                //Crear un comando para ejecutar un procedimiento almacenado
                //using (SqlCommand cmd = new SqlCommand("SELECT CodigoRenta, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOMBRE NOT IN ('RUC','CODIGO DE ALUMNO')", cn))
                using (SqlCommand cmd = new SqlCommand("SELECT CodigoRenta, NOMBRE FROM MAESTRO.TipoDocumento WHERE Estado=1", cn))
                {
                    //Definir el tipo de comando a ejecutar a Store procedure
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    //Abrir la conexion
                    cn.Open();
                    //Ejecutar el procedimiento y leer el registro del cliente encontrado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Leer el registro
                        while (dr.Read())
                        {
                            TipoDocumento Odoc = new TipoDocumento();
                            Odoc.CodigoRenta = Convert.ToString(dr["CodigoRenta"]);
                            Odoc.Nombre = dr["Nombre"].ToString();

                            tdoc.Add(Odoc);
                        }
                        //Devolver el objeto cliente encontrado
                        return tdoc;
                    }
                }
            }
        }

        public List<TipoDocumento> ListarDocRentaTodos()
        {

            List<TipoDocumento> tdoc = new List<TipoDocumento>();

            //Crear una conexion
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                //Crear un comando para ejecutar un procedimiento almacenado
                //using (SqlCommand cmd = new SqlCommand("SELECT CodigoRenta, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOMBRE NOT IN ('RUC','CODIGO DE ALUMNO')", cn))
                using (SqlCommand cmd = new SqlCommand("SELECT CodigoRenta, NOMBRE FROM MAESTRO.TipoDocumento WHERE NOT CODIGORENTA IS NULL", cn))
                {
                    //Definir el tipo de comando a ejecutar a Store procedure
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    //Abrir la conexion
                    cn.Open();
                    //Ejecutar el procedimiento y leer el registro del cliente encontrado
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Leer el registro
                        while (dr.Read())
                        {
                            TipoDocumento Odoc = new TipoDocumento();
                            Odoc.CodigoRenta = Convert.ToString(dr["CodigoRenta"]);
                            Odoc.Nombre = dr["Nombre"].ToString();

                            tdoc.Add(Odoc);
                        }
                        //Devolver el objeto cliente encontrado
                        return tdoc;
                    }
                }
            }
        }
        //finkevin

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
        // ~TipoDocumento_DAO() {
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
