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
    public class DAdmUsuario : DGesConexion
    {

        public static int AdmGestionUsuario(EAdmUsuario usuariop)
        {
            string contrasena = DGesEncriptacion.Encriptar(usuariop.Contrasena);
            string contrasena2 = DGesEncriptacion.Encriptar(usuariop.Contrasena2);
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionUsuario", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                cmd.Parameters.Add("@contrasena", SqlDbType.NVarChar);
                cmd.Parameters.Add("@contrasena2", SqlDbType.NVarChar);
                cmd.Parameters.Add("@rol", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = usuariop.Identificador;
                cmd.Parameters["@idUsuario"].Value = usuariop.IdUsuario;
                cmd.Parameters["@nombre"].Value = usuariop.Nombre;
                cmd.Parameters["@email"].Value = usuariop.Email;
                cmd.Parameters["@contrasena"].Value = contrasena;
                cmd.Parameters["@contrasena2"].Value = contrasena2;
                cmd.Parameters["@rol"].Value = usuariop.Rol;

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

        public static EAdmUsuario AdmVerificacionUsuario(EAdmUsuario usuario)
        {
            EAdmUsuario rsUsuario = new EAdmUsuario();

            string contrasena = DGesEncriptacion.Encriptar(usuario.Contrasena);
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("GestionInicioSesion", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 250);
                cmd.Parameters.Add("@contrasena", SqlDbType.NVarChar, 250);

                cmd.Parameters["@email"].Value = usuario.Email;
                cmd.Parameters["@contrasena"].Value = contrasena;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsUsuario.IdUsuario = Convert.ToInt32(rdr["IdUsuario"]);
                    rsUsuario.Nombre = rdr["Nombre"].ToString();
                    rsUsuario.Email = rdr["Email"].ToString();
                    rsUsuario.Rol = rdr["Rol"].ToString();

                }
                rdr.Close();
                return rsUsuario;

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

        public static string GestionUsuario(EAdmUsuario usuario)
        {
            return DGesEncriptacion.Encriptar(usuario.Contrasena);
        }
    }
}
