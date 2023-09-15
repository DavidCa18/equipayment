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
    public class DAdmGestion : DGesConexion
    {
        public static EAdmGestion AdmConsultarGestion(string identificacion)
        {
            EAdmGestion rsGestion = new EAdmGestion(); ;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Gestion_Payment WHERE Gestion_Payment.Identificacion = @identificacion", getCnn());
                cmd.Parameters.AddWithValue("@identificacion", identificacion);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    rsGestion.IdGestion = rdr["IdGestion"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["IdGestion"]);
                    rsGestion.Estado = rdr["Estado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Estado"]);
                    rsGestion.Identificacion = rdr["Identificacion"].ToString();
                }
                rdr.Close();
                return rsGestion;
            }
            catch (SqlException)
            {
                Cerrar();

                rsGestion.Identificacion = "El servicio se encuentra temporalmente ocupado, intente nuevamente en unos minutos.";
                return rsGestion;
            }
            finally
            {
                Cerrar();
            }
        }

        public static EAdmGestion AdmGestionGestion(EAdmGestion pGestion)
        {

            EAdmGestion gestion = new EAdmGestion();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionGestion", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idGestion", SqlDbType.Int);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@estado", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pGestion.Identificador;
                cmd.Parameters["@idGestion"].Value = pGestion.IdGestion;
                cmd.Parameters["@identificacion"].Value = pGestion.Identificacion;
                cmd.Parameters["@estado"].Value = pGestion.Estado;

                cmd.ExecuteNonQuery();

                gestion.IdGestion = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return gestion;

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
