using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public  class DAdmMensajes : DGesConexion
    {
        public static EAdmMensajes CliConsultarMensaje(string codigo)
        {

            EAdmMensajes rsMensajes = new EAdmMensajes();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Catalago_Mensajes_Payment WHERE Codigo = @codigo", getCnn());
                cmd.Parameters.AddWithValue("@codigo", codigo);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsMensajes.IdMensaje = Convert.ToInt32(rdr["IdMensaje"]);
                    rsMensajes.Codigo = rdr["Codigo"].ToString();
                    rsMensajes.Texto = rdr["Texto"].ToString();
                    rsMensajes.Descripcion = rdr["Descripcion"].ToString();
                    rsMensajes.Estado = rdr["Estado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Estado"]);
                    rsMensajes.Plataforma = rdr["Plataforma"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Plataforma"]);
                    rsMensajes.Imagen = rdr["Imagen"].ToString();

                }
                rdr.Close();
                return rsMensajes;
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
