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
  public  class CobranzasDAO
    {

        public List<Cobranzas> ListarCobranzasPorTipo_Seguro(Cobranzas objeCobranzas)
        {
            using (SqlConnection cn = new SqlConnection(ConexionDAO.cnx))
            {
                using (SqlCommand cmd = new SqlCommand("[Operacion].[Usp_Sel_Reporte_Cobranza]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoReporte", objeCobranzas.TipoReporte);
                    cmd.Parameters.AddWithValue("@CampaniaID", objeCobranzas.CampaniaID);
                    cmd.Parameters.AddWithValue("@CIASeguroID", objeCobranzas.CIASeguroID);
                    cmd.Parameters.AddWithValue("@FechaInicioCIASeguro", objeCobranzas.FechaInicioCIASeguro);
                    cmd.Parameters.AddWithValue("@FechaFinCIASeguro", objeCobranzas.FechaFinCIASeguro);
                    cmd.Parameters.AddWithValue("@InstitucionEducativaID", objeCobranzas.InstitucionEducativaID);
                    cmd.Parameters.AddWithValue("@FechaInicioInstitucionEducativa", objeCobranzas.FechaInicioInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@FechaFinInstitucionEducativa", objeCobranzas.FechaFinInstitucionEducativa);
                    cmd.Parameters.AddWithValue("@ProductoID", objeCobranzas.ProductoID);
                    cmd.Parameters.AddWithValue("@FechaInicioProducto", objeCobranzas.FechaInicioProducto);
                    cmd.Parameters.AddWithValue("@FechaFinProducto", objeCobranzas.FechaFinProducto);
                    cn.Open();                 
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    { List<Cobranzas> ListarCobranzas = new List<Cobranzas>();  
                        
                        while (dr.Read())
                        {Cobranzas TipoSeguroCobranzas = new Cobranzas();
                            TipoSeguroCobranzas.Nombre = Convert.ToString(dr["Nombre"]);
                            TipoSeguroCobranzas.AccidentesExisteProducto = Convert.ToInt32(dr["AccidentesExisteProducto"]);
                            TipoSeguroCobranzas.AccidentesNombre = Convert.ToString(dr["AccidentesNombre"]);
                            //TipoSeguroCobranzas.AccidentesPrimaSoles = 

                            if (TipoSeguroCobranzas.AccidentesPrimaSoles != null)
                            {
                                TipoSeguroCobranzas.AccidentesPrimaSoles = Convert.ToString(dr["AccidentesPrimaSoles"]);
                            }
                            else { TipoSeguroCobranzas.AccidentesPrimaSoles = null; }

                            TipoSeguroCobranzas.AccidentesTotalAseguradosSoles = Convert.ToInt32(dr["AccidentesTotalAseguradosSoles"]);
                            //TipoSeguroCobranzas.AccidentesPrimaDolares = Convert.ToInt16(dr["AccidentesPrimaDolares"]);

                            if (TipoSeguroCobranzas.AccidentesPrimaDolares != null)
                            {
                                TipoSeguroCobranzas.AccidentesPrimaDolares = Convert.ToString(dr["AccidentesPrimaDolares"]);
                            }
                            else { TipoSeguroCobranzas.AccidentesPrimaDolares = null; }

                            TipoSeguroCobranzas.AccidentesTotalAseguradosDolares = Convert.ToInt32(dr["AccidentesTotalAseguradosDolares"]);
                            TipoSeguroCobranzas._AccidentesTotalRecaudadoSoles = Convert.ToDecimal(dr["AccidentesTotalRecaudadoSoles"]);
                            TipoSeguroCobranzas._AccidentesTotalPagadoSoles = Convert.ToDecimal(dr["AccidentesTotalPagadoSoles"]);
                            TipoSeguroCobranzas._AccidentesTotalPendienteSoles = Convert.ToDecimal(dr["AccidentesTotalPendienteSoles"]);
                            TipoSeguroCobranzas._AccidentesTotalRecaudadoDolares = Convert.ToDecimal(dr["AccidentesTotalRecaudadoDolares"]);
                            TipoSeguroCobranzas._AccidentesTotalPagadoDolares = Convert.ToDecimal(dr["AccidentesTotalPagadoDolares"]);
                            TipoSeguroCobranzas._AccidentesTotalPendienteDolares = Convert.ToDecimal(dr["AccidentesTotalPendienteDolares"]);
                            TipoSeguroCobranzas.RentaExisteProducto = Convert.ToInt32(dr["RentaExisteProducto"]);
                            TipoSeguroCobranzas.RentaNombre = Convert.ToString(dr["RentaNombre"]);
                            
                            if (TipoSeguroCobranzas.RentaPrimaSoles != null)
                            {
                                TipoSeguroCobranzas.RentaPrimaSoles = Convert.ToString(dr["RentaPrimaSoles"]);
                            }
                            else { TipoSeguroCobranzas.RentaPrimaSoles = null; }

                            TipoSeguroCobranzas.RentaTotalAseguradosSoles = Convert.ToInt32(dr["RentaTotalAseguradosSoles"]);
                            
                            if (TipoSeguroCobranzas.RentaPrimaDolares != null)
                            {
                                TipoSeguroCobranzas.RentaPrimaDolares = Convert.ToString(dr["RentaPrimaDolares"]);
                            }
                            else { TipoSeguroCobranzas.RentaPrimaDolares = null; }

                            TipoSeguroCobranzas.RentaTotalAseguradosSoles = Convert.ToInt32(dr["RentaTotalAseguradosSoles"]);
                            TipoSeguroCobranzas._RentaTotalRecaudadoSoles = Convert.ToDecimal(dr["RentaTotalRecaudadoSoles"]);
                            TipoSeguroCobranzas._RentaTotalPagadoSoles = Convert.ToDecimal(dr["RentaTotalPagadoSoles"]);
                            TipoSeguroCobranzas._RentaTotalPendienteSoles = Convert.ToDecimal(dr["RentaTotalPendienteSoles"]);
                            TipoSeguroCobranzas._RentaTotalRecaudadoDolares = Convert.ToDecimal(dr["RentaTotalRecaudadoDolares"]);
                            TipoSeguroCobranzas._RentaTotalPagadoDolares = Convert.ToDecimal(dr["RentaTotalPagadoDolares"]);
                            TipoSeguroCobranzas._RentaTotalPendienteDolares = Convert.ToDecimal(dr["RentaTotalPendienteDolares"]);
                            ListarCobranzas.Add(TipoSeguroCobranzas);
                        }
                        return ListarCobranzas;
                    }
                }
            }
        }



    }
}
