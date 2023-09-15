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
    public class DAdmToken : DGesConexion
    {
        public static string AdmGestionTokenPago(EAdmToken pToken)
        {
            string resultado = "";

            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionTokenPago", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idToken", SqlDbType.Int);
                cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                cmd.Parameters.Add("@token", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@marca", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@banco", SqlDbType.NVarChar, -1);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pToken.Identificador;
                cmd.Parameters["@idToken"].Value = pToken.IdToken;
                cmd.Parameters["@idCliente"].Value = pToken.Cliente.IdCliente;
                cmd.Parameters["@token"].Value = pToken.Token;
                cmd.Parameters["@marca"].Value = pToken.Marca;
                cmd.Parameters["@banco"].Value = pToken.Banco;

                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["@valor"].Value.ToString();

                return resultado;

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
