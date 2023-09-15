using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmPagoDiferidos : DGesConexion
    {
        public static EAdmPagoDiferidos AdmGestionPagoDiferidos(EAdmPagoDiferidos pPagosDiferidos)
        {

            EAdmPagoDiferidos pagoDiferidos = new EAdmPagoDiferidos();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionPagosDiferidos", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;



                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idPagoDiferidos", SqlDbType.Int);
                cmd.Parameters.Add("@token", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@holder", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar);
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@json", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@idPago", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pPagosDiferidos.Identificador;
                cmd.Parameters["@idPagoDiferidos"].Value = pPagosDiferidos.IdPagoDiferidos;
                cmd.Parameters["@token"].Value = pPagosDiferidos.Token;
                cmd.Parameters["@holder"].Value = pPagosDiferidos.Holder;
                cmd.Parameters["@telefono"].Value = pPagosDiferidos.Telefono;
                cmd.Parameters["@email"].Value = pPagosDiferidos.Email;
                cmd.Parameters["@identificacion"].Value = pPagosDiferidos.Identificacion;
                cmd.Parameters["@json"].Value = pPagosDiferidos.JSON;
                cmd.Parameters["@idPago"].Value = pPagosDiferidos.IdPago;


                cmd.ExecuteNonQuery();

                pagoDiferidos.IdPagoDiferidos = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return pagoDiferidos;

            }
            catch (SqlException)
            {
                Cerrar();
                throw;
            }
            finally
            {
                Cerrar();
            }
        }

        public static List<EAdmPagoDiferidos> AdmConsultarPagosRecurrentes()
        {
            List<EAdmPagoDiferidos> lstDatos = new List<EAdmPagoDiferidos>();

            EAdmPagoDiferidos rsPago; 

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarPagosRecurrentes", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsPago = new EAdmPagoDiferidos();

                    rsPago.IdPagoDiferidos = rdr["IdPagoDiferidos"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["IdPagoDiferidos"]);
                    rsPago.Token = rdr["Token"].ToString();
                    rsPago.Holder = rdr["Holder"].ToString();
                    rsPago.Telefono = rdr["Telefono"].ToString();
                    rsPago.Email = rdr["Email"].ToString();
                    rsPago.Identificacion = rdr["Identificacion"].ToString();
                    rsPago.IdPago = rdr["IdPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Numero = rdr["Numero"].ToString();
                    rsPago.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsPago.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsPago.Iva = rdr["Iva"].ToString();
                    rsPago.Total = rdr["Total"].ToString();
                    rsPago.Fecha = rdr["Fecha"].ToString();
                    rsPago.Estado = rdr["Estado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Estado"]);

                    lstDatos.Add(rsPago);
                }
                rdr.Close();
                return lstDatos;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
