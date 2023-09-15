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
    public class DAdmError : DGesConexion
    {
        public static EAdmError AdmGestionError(EAdmError pError)
        {

            EAdmError error = new EAdmError();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionError", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@clase", SqlDbType.NVarChar);
                cmd.Parameters.Add("@metodo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@uriTemplate", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@estatus", SqlDbType.NVarChar);
                cmd.Parameters.Add("@url", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pError.Identificador;
                cmd.Parameters["@clase"].Value = pError.Clase;
                cmd.Parameters["@metodo"].Value = pError.Metodo;
                cmd.Parameters["@uriTemplate"].Value = pError.UriTemplate;
                cmd.Parameters["@estatus"].Value = pError.Estatus;
                cmd.Parameters["@url"].Value = pError.Url;
                cmd.Parameters["@descripcion"].Value = pError.Descripcion;
                cmd.Parameters["@idUsuario"].Value = pError.IdUsuario;
                cmd.Parameters["@nombreUsuario"].Value = pError.NombreUsuario;


                cmd.ExecuteNonQuery();

                error.IdError = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return error;

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

        public static int AdmGestionTrama(string descripcion)
        {
            
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionError", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@clase", SqlDbType.NVarChar);
                cmd.Parameters.Add("@metodo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@uriTemplate", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@estatus", SqlDbType.NVarChar);
                cmd.Parameters.Add("@url", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = 1;
                cmd.Parameters["@clase"].Value = "";
                cmd.Parameters["@metodo"].Value = "";
                cmd.Parameters["@uriTemplate"].Value = "";
                cmd.Parameters["@estatus"].Value = "";
                cmd.Parameters["@url"].Value = "";
                cmd.Parameters["@descripcion"].Value = descripcion;
                cmd.Parameters["@idUsuario"].Value = 0;
                cmd.Parameters["@nombreUsuario"].Value = "TRAMA DATAFAST";


                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.Parameters["@valor"].Value);

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

        public static List<EAdmError> AdmConsultarErrores()
        {
            List<EAdmError> lstError = new List<EAdmError>();
            EAdmError rsError;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Catalogo_Error_Payment", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rsError = new EAdmError();

                    rsError.IdError = Convert.ToInt32(rdr["IdError"]);
                    rsError.Clase = rdr["Clase"].ToString();
                    rsError.Metodo = rdr["Metodo"].ToString();
                    rsError.UriTemplate = rdr["UriTemplate"].ToString();
                    rsError.Estatus = rdr["Estatus"].ToString();
                    rsError.Url = rdr["Url"].ToString();
                    rsError.Descripcion = rdr["Descripcion"].ToString();
                    rsError.Fecha = rdr["Fecha"].ToString();
                    rsError.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);
                    rsError.NombreUsuario = rdr["NombreUsuario"].ToString();

                    lstError.Add(rsError);
                }
                rdr.Close();
                return lstError;
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
