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
    public class DAdmAplicacion : DGesConexion
    {
        public static EAdmAplicacion AdmGestionAplicacion(EAdmAplicacion pAplicacion)
        {

            EAdmAplicacion aplicacion = new EAdmAplicacion();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionAplicacion", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idAplicacion", SqlDbType.Int);
                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@token", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@logoPrimario", SqlDbType.Xml);
                cmd.Parameters.Add("@logoSecundario", SqlDbType.Xml);
                cmd.Parameters.Add("@colorPrimario", SqlDbType.NVarChar);
                cmd.Parameters.Add("@colorSecundario", SqlDbType.NVarChar);
                cmd.Parameters.Add("@fondoPrimario", SqlDbType.NVarChar);
                cmd.Parameters.Add("@fondoSecundario", SqlDbType.NVarChar);
                cmd.Parameters.Add("@logoPrimarioTamano", SqlDbType.NVarChar);
                cmd.Parameters.Add("@logoSecundarioTamano", SqlDbType.NVarChar);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@codigo", SqlDbType.Int);

                cmd.Parameters.Add("@montoMaximo", SqlDbType.Float);
                cmd.Parameters.Add("@montoMinimo", SqlDbType.Float);
                cmd.Parameters.Add("@visualizacionBin", SqlDbType.Int);
                cmd.Parameters.Add("@caducidad", SqlDbType.NVarChar);
                cmd.Parameters.Add("@recurrencia", SqlDbType.Int);
                cmd.Parameters.Add("@gracia", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pAplicacion.Identificador;
                cmd.Parameters["@idAplicacion"].Value = pAplicacion.IdAplicacion;
                cmd.Parameters["@nombre"].Value = pAplicacion.Nombre;
                cmd.Parameters["@token"].Value = DGesEncriptacion.CrearKeyAutorizacion(pAplicacion.Nombre);
                cmd.Parameters["@logoPrimario"].Value = pAplicacion.LogoPrimario;
                cmd.Parameters["@logoSecundario"].Value = pAplicacion.LogoSecundario;
                cmd.Parameters["@colorPrimario"].Value = pAplicacion.ColorPrimario;
                cmd.Parameters["@colorSecundario"].Value = pAplicacion.ColorSecundario;
                cmd.Parameters["@fondoPrimario"].Value = pAplicacion.FondoPrimario;
                cmd.Parameters["@fondoSecundario"].Value = pAplicacion.FondoSecundario;
                cmd.Parameters["@logoPrimarioTamano"].Value = pAplicacion.LogoPrimarioTamano;
                cmd.Parameters["@logoSecundarioTamano"].Value = pAplicacion.LogoSecundarioTamano;
                cmd.Parameters["@estado"].Value = pAplicacion.Estado;
                cmd.Parameters["@identificacion"].Value = pAplicacion.Identificacion;
                cmd.Parameters["@codigo"].Value = pAplicacion.Codigo;

                cmd.Parameters["@montoMaximo"].Value = pAplicacion.MontoMaximo;
                cmd.Parameters["@montoMinimo"].Value = pAplicacion.MontoMinimo;
                cmd.Parameters["@visualizacionBin"].Value = pAplicacion.VisualizacionBin;
                cmd.Parameters["@caducidad"].Value = pAplicacion.Caducidad;
                cmd.Parameters["@recurrencia"].Value = pAplicacion.Recurrencia;
                cmd.Parameters["@gracia"].Value = pAplicacion.Gracia;


                cmd.ExecuteNonQuery();

                aplicacion.IdAplicacion = Convert.ToInt32(cmd.Parameters["@valor"].Value);
                aplicacion.Token = DGesEncriptacion.CrearKeyAutorizacion(pAplicacion.Nombre);

                return aplicacion;

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

        public static EAdmAplicacion AdmConsultarDatosAplicacion(int idAplicacion)
        {

            EAdmAplicacion rsAplicacion = new EAdmAplicacion();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Aplicacion_Payment WHERE Codigo = @codigo", getCnn());
                cmd.Parameters.AddWithValue("@codigo", idAplicacion);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.Token = rdr["Token"].ToString();
                    rsAplicacion.LogoPrimario = rdr["LogoPrimario"].ToString();
                    rsAplicacion.LogoSecundario = rdr["LogoSecundario"].ToString();
                    rsAplicacion.ColorPrimario = rdr["ColorPrimario"].ToString();
                    rsAplicacion.ColorSecundario = rdr["ColorSecundario"].ToString();
                    rsAplicacion.FondoPrimario = rdr["FondoPrimario"].ToString();
                    rsAplicacion.FondoSecundario = rdr["FondoSecundario"].ToString();
                    rsAplicacion.LogoPrimarioTamano = rdr["LogoPrimarioTamano"].ToString();
                    rsAplicacion.LogoSecundarioTamano = rdr["LogoSecundarioTamano"].ToString();
                    rsAplicacion.Identificacion = rdr["Identificacion"].ToString();
                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    rsAplicacion.MontoMaximo = rdr["MontoMaximo"] == DBNull.Value ? 0 : Convert.ToDouble(rdr["MontoMaximo"]);
                    rsAplicacion.MontoMinimo = rdr["MontoMinimo"] == DBNull.Value ? 0 : Convert.ToDouble(rdr["MontoMinimo"]);
                    rsAplicacion.VisualizacionBin = rdr["VisualizacionBin"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["VisualizacionBin"]);
                    rsAplicacion.Caducidad = rdr["Caducidad"].ToString();
                    rsAplicacion.Recurrencia = Convert.ToInt32(rdr["Recurrencia"]);
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["Gracia"]);


                }
                rdr.Close();
                return rsAplicacion;
            }
            catch (SqlException)
            {
                Cerrar();

                rsAplicacion.Nombre = "El código de aplicación que envió no existe en la plataforma.";
                return rsAplicacion;
            }
            finally
            {
                Cerrar();
            }
        }

        public static List<EAdmAplicacion> AdmConsultarAplicaciones()
        {

            List<EAdmAplicacion> lstDatos = new List<EAdmAplicacion>();

            EAdmAplicacion rsAplicacion;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacion ORDER BY IdAplicacion DESC", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.Token = rdr["Token"].ToString();
                    rsAplicacion.LogoPrimario = rdr["LogoPrimario"].ToString();
                    rsAplicacion.LogoSecundario = rdr["LogoSecundario"].ToString();
                    rsAplicacion.ColorPrimario = rdr["ColorPrimario"].ToString();
                    rsAplicacion.ColorSecundario = rdr["ColorSecundario"].ToString();
                    rsAplicacion.FondoPrimario = rdr["FondoPrimario"].ToString();
                    rsAplicacion.FondoSecundario = rdr["FondoSecundario"].ToString();
                    rsAplicacion.LogoPrimarioTamano = rdr["LogoPrimarioTamano"].ToString();
                    rsAplicacion.LogoSecundarioTamano = rdr["LogoSecundarioTamano"].ToString();
                    rsAplicacion.Estado = Convert.ToInt32(rdr["EstadoAplicacion"]);
                    rsAplicacion.Identificacion = rdr["IdentificacionAplicacion"].ToString();
                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);

                    rsAplicacion.MontoMaximo = rdr["MontoMaximo"] == DBNull.Value ? 0 : Convert.ToDouble(rdr["MontoMaximo"]);
                    rsAplicacion.MontoMinimo = rdr["MontoMinimo"] == DBNull.Value ? 0 : Convert.ToDouble(rdr["MontoMinimo"]);
                    rsAplicacion.VisualizacionBin = rdr["VisualizacionBin"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["VisualizacionBin"]);
                    rsAplicacion.Caducidad = rdr["Caducidad"].ToString();
                    rsAplicacion.Recurrencia = Convert.ToInt32(rdr["Recurrencia"]);
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["Gracia"]);

                    lstDatos.Add(rsAplicacion);
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

        public static List<EAdmAplicacion> AdmConsultarComboAplicaciones()
        {

            List<EAdmAplicacion> lstDatos = new List<EAdmAplicacion>();

            EAdmAplicacion rsAplicacion;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdAplicacion, Nombre, Codigo FROM Aplicacion_Payment", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);

                    lstDatos.Add(rsAplicacion);
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
