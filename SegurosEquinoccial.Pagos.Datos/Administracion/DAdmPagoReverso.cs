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
    public class DAdmPagoReverso : DGesConexion
    {
        public static string AdmGestionPagoReverso(EAdmPagoReverso pPago)
        {
            string resultado = "";

            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionPagoReverso", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idPagoReverso", SqlDbType.Int);
                cmd.Parameters.Add("@codigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@numeroReferencia", SqlDbType.NVarChar);
                cmd.Parameters.Add("@respuestaAdquiriente", SqlDbType.NVarChar);
                cmd.Parameters.Add("@codigoAutenticacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@conector", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoCodigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTexto", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTrama", SqlDbType.Xml);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@idPago", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pPago.Identificador;
                cmd.Parameters["@idPagoReverso"].Value = pPago.IdPagoReverso;
                cmd.Parameters["@codigo"].Value = pPago.Codigo;
                cmd.Parameters["@numeroReferencia"].Value = pPago.NumeroReferencia;
                cmd.Parameters["@respuestaAdquiriente"].Value = pPago.RespuestaAdquiriente;
                cmd.Parameters["@codigoAutenticacion"].Value = pPago.CodigoAutenticacion;
                cmd.Parameters["@conector"].Value = pPago.Conector;
                cmd.Parameters["@resultadoCodigo"].Value = pPago.ResultadoCodigo;
                cmd.Parameters["@resultadoTexto"].Value = pPago.ResultadoTexto;
                cmd.Parameters["@resultadoTrama"].Value = pPago.ResultadoTrama;
                cmd.Parameters["@estado"].Value = pPago.Estado;
                cmd.Parameters["@idPago"].Value = pPago.Pago.IdPago;

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

        public static List<EAdmPagoReverso> AdmConsultaListaPagosReversos(string plataforma)
        {
            List<EAdmPagoReverso> lstDatos = new List<EAdmPagoReverso>();

            EAdmPagoReverso rsPagoReverso;
            EAdmPago rsPago;
            EAdmFactura rsFactura;
            EAdmClientes rsCliente;
            EAdmAplicacion rsAplicacion;

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarPagoReverso WHERE Plataforma = @plataforma", getCnn());
                cmd.Parameters.AddWithValue("@plataforma", plataforma);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsPagoReverso = new EAdmPagoReverso();
                    rsPago = new EAdmPago();
                    rsFactura = new EAdmFactura();
                    rsCliente = new EAdmClientes();
                    rsAplicacion = new EAdmAplicacion();

                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();

                    rsPagoReverso.IdPagoReverso = Convert.ToInt32(rdr["IdPagoReverso"]);
                    rsPagoReverso.Codigo = rdr["Codigo"].ToString();
                    rsPagoReverso.NumeroReferencia = rdr["NumeroReferencia"].ToString();
                    rsPagoReverso.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPagoReverso.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPagoReverso.Conector = rdr["Conector"].ToString();
                    rsPagoReverso.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPagoReverso.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    rsPagoReverso.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPagoReverso.FechaReverso = rdr["FechaReverso"].ToString();
                    rsPagoReverso.Estado = Convert.ToInt32(rdr["Estado"]); ;

                    rsFactura.Numero = rdr["Numero"].ToString();
                    rsFactura.Comercio = rdr["Comercio"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();

                    rsCliente.Identificacion = rdr["Identificacion"].ToString();
                    rsCliente.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsCliente.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsCliente.Apellido = rdr["Apellido"].ToString();

                    rsAplicacion.Nombre = rdr["Aplicacion"].ToString();

                    rsPagoReverso.Pago = rsPago;
                    rsPagoReverso.Pago.Factura = rsFactura;
                    rsPagoReverso.Pago.Factura.Cliente = rsCliente;
                    rsPagoReverso.Pago.Factura.Cliente.Aplicacion = rsAplicacion;

                    lstDatos.Add(rsPagoReverso);
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
