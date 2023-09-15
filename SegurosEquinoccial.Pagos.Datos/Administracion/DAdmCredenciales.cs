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
    public class DAdmCredenciales: DGesConexion
    {
        public static EAdmCredenciales AdmGestionCredenciales(EAdmCredenciales pCredenciales)
        {
            EAdmCredenciales credenciales = new EAdmCredenciales();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionCredenciales", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int, 1);

                cmd.Parameters.Add("@idCredenciales", SqlDbType.Int);
                cmd.Parameters.Add("@url", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@userId", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@entityId", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@mid", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@tip", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@modo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@identificador_", SqlDbType.NVarChar);
                
                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pCredenciales.Identificador;

                cmd.Parameters["@idCredenciales"].Value = pCredenciales.IdCredenciales;
                cmd.Parameters["@url"].Value = (pCredenciales.Url);
                cmd.Parameters["@userId"].Value = (pCredenciales.UserId);
                cmd.Parameters["@password"].Value = (pCredenciales.Password);
                cmd.Parameters["@entityId"].Value = (pCredenciales.EntityId);
                cmd.Parameters["@mid"].Value = (pCredenciales.MID);
                cmd.Parameters["@tip"].Value = (pCredenciales.TIP);
                cmd.Parameters["@modo"].Value = pCredenciales.Modo;
                cmd.Parameters["@estado"].Value = pCredenciales.Estado;
                cmd.Parameters["@identificador_"].Value = pCredenciales.Identificador_;

                cmd.ExecuteNonQuery();

                credenciales.IdCredenciales = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return credenciales;

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

        public static EAdmCredenciales AdmConsultaCredenciales(string modo, string identificador)
        {

            EAdmCredenciales rsCredenciales = new EAdmCredenciales();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Catalago_Credenciales_Payment WHERE Modo = @modo AND Identificador = @identificador", getCnn());
                cmd.Parameters.AddWithValue("@modo", modo);
                cmd.Parameters.AddWithValue("@identificador", identificador);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    rsCredenciales.IdCredenciales = Convert.ToInt32(rdr["IdCredenciales"]);
                    rsCredenciales.Url = (rdr["Url"].ToString());
                    rsCredenciales.UserId = (rdr["UserId"].ToString());
                    rsCredenciales.Password = (rdr["Password"].ToString());
                    rsCredenciales.EntityId = (rdr["EntityId"].ToString());
                    rsCredenciales.MID = (rdr["MID"].ToString());
                    rsCredenciales.TIP = (rdr["TIP"].ToString());
                    rsCredenciales.Modo = rdr["Modo"].ToString();
                    rsCredenciales.Estado = Convert.ToInt32(rdr["Estado"]);

                }
                rdr.Close();
                return rsCredenciales;
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
