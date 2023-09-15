using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmHistorialTransacciones : DGesConexion
    {

        public static EAdmHistorialTransacciones AdmRegistrarHistorialTransacciones(
            string Identificacion,
            string PrimerNombre,
            string SegundoNombre,
            string Apellido,
            string Email,
            string Telefono,
            string IdPv,
            string Poliza,
            string Aplicacion,
            string Diferidos,
            string TipoDiferido,
            string Comercio,
            string Subtotal12,
            string Subtotal0,
            string Iva,
            string Total,
            string UrlRetorno,
            string Mensaje,
            string Tipo
            )
        {

            EAdmHistorialTransacciones transacciones = new EAdmHistorialTransacciones();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionHistorialTransacciones", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@primerNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@segundoNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@apellido", SqlDbType.NVarChar);
                cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                cmd.Parameters.Add("@celular", SqlDbType.NVarChar);
                cmd.Parameters.Add("@idpv", SqlDbType.NVarChar);
                cmd.Parameters.Add("@poliza", SqlDbType.NVarChar);
                cmd.Parameters.Add("@codigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@diferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@tipoDiferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@comercio", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal12", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal0", SqlDbType.NVarChar);
                cmd.Parameters.Add("@iva", SqlDbType.NVarChar);
                cmd.Parameters.Add("@total", SqlDbType.NVarChar);
                cmd.Parameters.Add("@urlRetorno", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@tipo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@trama", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@cuotas", SqlDbType.NVarChar);
                cmd.Parameters.Add("@facturaAplicacion", SqlDbType.NVarChar, -1);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = 1;
                cmd.Parameters["@identificacion"].Value = Identificacion;
                cmd.Parameters["@primerNombre"].Value = PrimerNombre;
                cmd.Parameters["@segundoNombre"].Value = SegundoNombre;
                cmd.Parameters["@apellido"].Value = Apellido;
                cmd.Parameters["@email"].Value = Email;
                cmd.Parameters["@celular"].Value = Telefono;
                cmd.Parameters["@idpv"].Value = IdPv;
                cmd.Parameters["@poliza"].Value = Poliza;
                cmd.Parameters["@codigo"].Value = Aplicacion;
                cmd.Parameters["@diferidos"].Value = Diferidos;
                cmd.Parameters["@tipoDiferidos"].Value = TipoDiferido;
                cmd.Parameters["@comercio"].Value = Comercio;
                cmd.Parameters["@subtotal12"].Value = Subtotal12;
                cmd.Parameters["@subtotal0"].Value = Subtotal0;
                cmd.Parameters["@iva"].Value = Iva;
                cmd.Parameters["@total"].Value = Total;
                cmd.Parameters["@urlRetorno"].Value = UrlRetorno;
                cmd.Parameters["@mensaje"].Value = Mensaje;
                cmd.Parameters["@tipo"].Value = Tipo;
                cmd.Parameters["@trama"].Value = "-";
                cmd.Parameters["@cuotas"].Value = "-";
                cmd.Parameters["@facturaAplicacion"].Value = "-";

                cmd.ExecuteNonQuery();

                transacciones.IdHistorialTransacciones = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return transacciones;

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


        public static EAdmHistorialTransacciones AdmGestionTransacciones(EAdmHistorialTransacciones pHistorial)
        {
            EAdmHistorialTransacciones transacciones = new EAdmHistorialTransacciones();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionHistorialTransacciones", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@primerNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@segundoNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@apellido", SqlDbType.NVarChar);
                cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                cmd.Parameters.Add("@celular", SqlDbType.NVarChar);
                cmd.Parameters.Add("@idpv", SqlDbType.NVarChar);
                cmd.Parameters.Add("@poliza", SqlDbType.NVarChar);
                cmd.Parameters.Add("@codigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@diferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@tipoDiferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@comercio", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal12", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal0", SqlDbType.NVarChar);
                cmd.Parameters.Add("@iva", SqlDbType.NVarChar);
                cmd.Parameters.Add("@total", SqlDbType.NVarChar);
                cmd.Parameters.Add("@urlRetorno", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@tipo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@trama", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@cuotas", SqlDbType.NVarChar);
                cmd.Parameters.Add("@facturaAplicacion", SqlDbType.NVarChar, -1);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pHistorial.Identificador;
                cmd.Parameters["@identificacion"].Value = pHistorial.Identificacion;
                cmd.Parameters["@primerNombre"].Value = pHistorial.PrimerNombre;
                cmd.Parameters["@segundoNombre"].Value = pHistorial.SegundoNombre;
                cmd.Parameters["@apellido"].Value = pHistorial.Apellido;
                cmd.Parameters["@email"].Value = pHistorial.Email;
                cmd.Parameters["@celular"].Value = pHistorial.Telefono;
                cmd.Parameters["@idpv"].Value = pHistorial.IdPv;
                cmd.Parameters["@poliza"].Value = pHistorial.Poliza;
                cmd.Parameters["@codigo"].Value = pHistorial.Aplicacion;
                cmd.Parameters["@diferidos"].Value = pHistorial.Diferidos;
                cmd.Parameters["@tipoDiferidos"].Value = pHistorial.TipoDiferido;
                cmd.Parameters["@comercio"].Value = pHistorial.Comercio;
                cmd.Parameters["@subtotal12"].Value = pHistorial.Subtotal12;
                cmd.Parameters["@subtotal0"].Value = pHistorial.Subtotal0;
                cmd.Parameters["@iva"].Value = pHistorial.Iva;
                cmd.Parameters["@total"].Value = pHistorial.Total;
                cmd.Parameters["@urlRetorno"].Value = pHistorial.UrlRetorno;
                cmd.Parameters["@mensaje"].Value = pHistorial.Mensaje;
                cmd.Parameters["@tipo"].Value = pHistorial.Tipo;
                cmd.Parameters["@trama"].Value = pHistorial.Trama;
                cmd.Parameters["@cuotas"].Value = pHistorial.Cuotas;
                cmd.Parameters["@facturaAplicacion"].Value = pHistorial.FacturaAplicacion;

                cmd.ExecuteNonQuery();

                transacciones.Resultado = cmd.Parameters["@valor"].Value.ToString();

                return transacciones;

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

        public static List<EAdmHistorialTransacciones> AdmConsultarHistorialTransacciones(EAdmAuxiliares aux)
        {
            List<EAdmHistorialTransacciones> lista = new List<EAdmHistorialTransacciones>();
            EAdmHistorialTransacciones rsHistorialTransacciones;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM HistorialTransacciones_Payment WHERE (CONVERT(DATE, FechaIngreso, 23) BETWEEN @fechaInicio AND @fechaFin) " + aux.Cadena + "", getCnn());
                cmd.Parameters.AddWithValue("@fechaInicio", aux.FechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", aux.FechaFin);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    rsHistorialTransacciones = new EAdmHistorialTransacciones();

                    rsHistorialTransacciones.IdHistorialTransacciones = rdr["IdHistorialTransacciones"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["IdHistorialTransacciones"]);
                    rsHistorialTransacciones.Identificacion = rdr["Identificacion"].ToString();
                    rsHistorialTransacciones.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsHistorialTransacciones.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsHistorialTransacciones.Apellido = rdr["Apellido"].ToString();
                    rsHistorialTransacciones.Email = rdr["Email"].ToString();
                    rsHistorialTransacciones.Telefono = rdr["Telefono"].ToString();
                    rsHistorialTransacciones.IdPv = rdr["IdPv"].ToString();
                    rsHistorialTransacciones.Poliza = rdr["Poliza"].ToString();
                    rsHistorialTransacciones.Aplicacion = rdr["Aplicacion"].ToString();
                    rsHistorialTransacciones.Diferidos = rdr["Diferidos"].ToString();
                    rsHistorialTransacciones.TipoDiferido = rdr["TipoDiferido"].ToString();
                    rsHistorialTransacciones.Comercio = rdr["Comercio"].ToString();
                    rsHistorialTransacciones.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsHistorialTransacciones.Subtotal0 = rdr["Iva"].ToString();
                    rsHistorialTransacciones.Total = rdr["Total"].ToString();
                    rsHistorialTransacciones.UrlRetorno = rdr["UrlRetorno"].ToString();
                    rsHistorialTransacciones.Mensaje = rdr["Mensaje"].ToString();
                    rsHistorialTransacciones.Estado = rdr["Estado"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Estado"]);
                    rsHistorialTransacciones.FechaIngreso = rdr["FechaIngreso"].ToString();
                    rsHistorialTransacciones.Tipo = rdr["Tipo"].ToString();
                    rsHistorialTransacciones.Cuotas = rdr["Cuotas"].ToString();
                    rsHistorialTransacciones.FacturaAplicacion = rdr["FacturaAplicacion"].ToString();

                    lista.Add(rsHistorialTransacciones);
                }
                rdr.Close();
                return lista;
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
