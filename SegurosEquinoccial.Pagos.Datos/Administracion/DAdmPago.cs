using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmPago : DGesConexion
    {
        public static string AdmGestionPago(EAdmPago pPago)
        {
            string resultado = "";

            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionPago", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idPago", SqlDbType.Int);
                cmd.Parameters.Add("@ip", SqlDbType.NVarChar);
                cmd.Parameters.Add("@plataforma", SqlDbType.NVarChar);
                cmd.Parameters.Add("@idTransaccion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@codigoAutenticacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@referencia", SqlDbType.NVarChar);
                cmd.Parameters.Add("@lote", SqlDbType.NVarChar);
                cmd.Parameters.Add("@voucher", SqlDbType.NVarChar);
                cmd.Parameters.Add("@parametroPersonalizado", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@numeroDiferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoCodigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTexto", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTrama", SqlDbType.Xml);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@intereses", SqlDbType.NVarChar);
                cmd.Parameters.Add("@respuestaAdquiriente", SqlDbType.NVarChar);
                cmd.Parameters.Add("@idFactura", SqlDbType.Int);
                cmd.Parameters.Add("@bin", SqlDbType.NVarChar);
                cmd.Parameters.Add("@digitos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@apoderado", SqlDbType.NVarChar);
                cmd.Parameters.Add("@marca", SqlDbType.NVarChar);
                cmd.Parameters.Add("@banco", SqlDbType.NVarChar);
                cmd.Parameters.Add("@gracia", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pPago.Identificador;
                cmd.Parameters["@idPago"].Value = pPago.IdPago;
                cmd.Parameters["@ip"].Value = pPago.Ip;
                cmd.Parameters["@plataforma"].Value = pPago.Plataforma;
                cmd.Parameters["@idTransaccion"].Value = pPago.IdTransaccion;
                cmd.Parameters["@codigoAutenticacion"].Value = pPago.CodigoAutenticacion;
                cmd.Parameters["@referencia"].Value = pPago.Referencia;
                cmd.Parameters["@lote"].Value = pPago.Lote;
                cmd.Parameters["@voucher"].Value = pPago.Voucher;
                cmd.Parameters["@parametroPersonalizado"].Value = pPago.ParametroPersonalizado;
                cmd.Parameters["@numeroDiferidos"].Value = pPago.NumeroDiferidos;
                cmd.Parameters["@resultadoCodigo"].Value = pPago.ResultadoCodigo;
                cmd.Parameters["@resultadoTexto"].Value = pPago.ResultadoTexto;
                cmd.Parameters["@resultadoTrama"].Value = pPago.ResultadoTrama;
                cmd.Parameters["@estado"].Value = pPago.Estado;
                cmd.Parameters["@intereses"].Value = pPago.Intereses;
                cmd.Parameters["@respuestaAdquiriente"].Value = pPago.RespuestaAdquiriente;
                cmd.Parameters["@idFactura"].Value = pPago.Factura.IdFactura;
                cmd.Parameters["@bin"].Value = pPago.Bin;
                cmd.Parameters["@digitos"].Value = pPago.Digitos;
                cmd.Parameters["@apoderado"].Value = pPago.Apoderado;
                cmd.Parameters["@marca"].Value = pPago.Marca;
                cmd.Parameters["@banco"].Value = pPago.Banco;
                cmd.Parameters["@gracia"].Value = pPago.Gracia;

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

        public static int AdmConsultaIntentosPago(int idPago)
        {

            int intentos = 0;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Reintentos_Pago_Payment WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    intentos = Convert.ToInt32(rdr["Intentos"]);

                }
                rdr.Close();
                return intentos;
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

        public static EAdmPago AdmConsultaPagoResultado(int idPago)
        {
            EAdmPago rsPago = new EAdmPago();
            EAdmFactura rsFactura = new EAdmFactura();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Ip = rdr["Ip"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                    rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPago.Referencia = rdr["Referencia"].ToString();
                    rsPago.Lote = rdr["Lote"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                    rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.Intereses = rdr["Intereses"].ToString();
                    rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPago.FechaTransaccion = rdr["FechaTransaccion"].ToString();

                    rsPago.Bin = rdr["Bin"].ToString();
                    rsPago.Digitos = rdr["Digitos"].ToString();
                    rsPago.Apoderado = rdr["Apoderado"].ToString();
                    rsPago.Marca = rdr["Marca"].ToString();
                    rsPago.Banco = rdr["Banco"].ToString();
                    rsPago.Gracia = rdr["Gracia"].ToString();

                    if (Convert.ToInt32(rdr["EstadoPago"]) == 2)
                    {
                        rsPago.Recibo = generarRecibo(
                            rdr["Plataforma"].ToString(),
                            rdr["ResultadoTrama"].ToString(),
                            rdr["FechaRecibo"].ToString(),
                            rdr["HoraRecibo"].ToString(),
                            rdr["Subtotal12"].ToString(),
                            rdr["Subtotal0"].ToString(),
                            rdr["Subtotal"].ToString(),
                            rdr["Iva"].ToString(),
                            rdr["Total"].ToString(),
                            rdr["Lote"].ToString(),
                            rdr["Referencia"].ToString()
                            );
                    }
                    else
                    {
                        rsPago.Recibo = "";
                    }

                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                    rsPago.Factura = rsFactura;

                }
                rdr.Close();

                return rsPago;
            }
            catch (SqlException)
            {
                Cerrar();
                rsPago.ResultadoTexto = "El servicio se encuentra temporalmente ocupado, intente nuevamente en unos minutos.";
                return rsPago;
            }
            finally
            {
                Cerrar();
            }
        }

        public static EAdmPago AdmConsultarPagoCliente(string cedula, string certificado, string valor)
        {
            EAdmPago rsPago = new EAdmPago();
            EAdmFactura rsFactura = new EAdmFactura();
            EAdmClientes rsCliente = new EAdmClientes();
            EAdmAplicacion rsAplicacion = new EAdmAplicacion();

            EAdmCredenciales credenciales = EGloGlobales.obtenerCredenciales();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacionClienteFacturaPago WHERE Identificacion = @cedula AND Comercio = @certificado AND Total = @valor ORDER BY IdPago DESC", getCnn());
                cmd.Parameters.AddWithValue("@cedula", cedula);
                cmd.Parameters.AddWithValue("@certificado", certificado);
                cmd.Parameters.AddWithValue("@valor", valor);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    if (Convert.ToInt32(rdr["EstadoPago"]) == 2)
                    {
                        rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                        rsPago.Ip = rdr["Ip"].ToString();
                        rsPago.Plataforma = rdr["Plataforma"].ToString();
                        rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                        rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                        rsPago.Referencia = rdr["Referencia"].ToString();
                        rsPago.Lote = rdr["Lote"].ToString();
                        rsPago.Voucher = rdr["Voucher"].ToString();
                        rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                        rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                        rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                        rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                        rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                        rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                        rsPago.Intereses = rdr["Intereses"].ToString();
                        rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                        rsPago.FechaTransaccion = rdr["FechaTransaccion2"].ToString();

                        rsPago.Bin = rdr["Bin"].ToString();
                        rsPago.Digitos = rdr["Digitos"].ToString();
                        rsPago.Apoderado = rdr["Apoderado"].ToString();
                        rsPago.Marca = rdr["Marca"].ToString();
                        rsPago.Banco = rdr["Banco"].ToString();
                        rsPago.Gracia = rdr["Gracia"].ToString();

                        if (Convert.ToInt32(rdr["EstadoPago"]) == 2)
                        {
                            rsPago.Recibo = generarRecibo(
                                rdr["Plataforma"].ToString(),
                                rdr["ResultadoTrama"].ToString(),
                                rdr["FechaRecibo"].ToString(),
                                rdr["HoraRecibo"].ToString(),
                                rdr["Subtotal12"].ToString(),
                                rdr["Subtotal0"].ToString(),
                                rdr["Subtotal"].ToString(),
                                rdr["Iva"].ToString(),
                                rdr["Total"].ToString(),
                                rdr["Lote"].ToString(),
                                rdr["Referencia"].ToString()
                                );
                        }
                        else
                        {
                            rsPago.Recibo = "";
                        }


                        if (Convert.ToInt32(rdr["GraciaAplicacion"]) == 1)
                        {

                            rsPago.Link = credenciales.urlPlataforma + "?c=" + DGesEncriptacion.CodificarBase64(Convert.ToInt32(rdr["IdPago"]) + "") + "&p=" + Convert.ToInt32(rdr["Codigo"]) + "&d=gracia";
                        }
                        else
                        {
                            rsPago.Link = credenciales.urlPlataforma + "?c=" + DGesEncriptacion.CodificarBase64(Convert.ToInt32(rdr["IdPago"]) + "") + "&p=" + Convert.ToInt32(rdr["Codigo"]);
                        }

                        rsFactura.Total = rdr["Total"].ToString();
                        rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                        rsPago.Factura = rsFactura;

                        break;
                    }
                    else
                    {
                        rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                        rsPago.Ip = rdr["Ip"].ToString();
                        rsPago.Plataforma = rdr["Plataforma"].ToString();
                        rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                        rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                        rsPago.Referencia = rdr["Referencia"].ToString();
                        rsPago.Lote = rdr["Lote"].ToString();
                        rsPago.Voucher = rdr["Voucher"].ToString();
                        rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                        rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                        rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                        rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                        rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                        rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                        rsPago.Intereses = rdr["Intereses"].ToString();
                        rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                        rsPago.FechaTransaccion = rdr["FechaTransaccion2"].ToString();

                        rsPago.Bin = rdr["Bin"].ToString();
                        rsPago.Digitos = rdr["Digitos"].ToString();
                        rsPago.Apoderado = rdr["Apoderado"].ToString();
                        rsPago.Marca = rdr["Marca"].ToString();
                        rsPago.Banco = rdr["Banco"].ToString();
                        rsPago.Gracia = rdr["Gracia"].ToString();
                        rsPago.Recibo = "";


                        if (Convert.ToInt32(rdr["GraciaAplicacion"]) == 1)
                        {

                            rsPago.Link = credenciales.urlPlataforma + "?c=" + DGesEncriptacion.CodificarBase64(Convert.ToInt32(rdr["IdPago"]) + "") + "&p=" + Convert.ToInt32(rdr["Codigo"]) + "&d=gracia";
                        }
                        else
                        {
                            rsPago.Link = credenciales.urlPlataforma + "?c=" + DGesEncriptacion.CodificarBase64(Convert.ToInt32(rdr["IdPago"]) + "") + "&p=" + Convert.ToInt32(rdr["Codigo"]);
                        }

                        rsFactura.Total = rdr["Total"].ToString();
                        rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                        rsPago.Factura = rsFactura;
                    }

                }
                rdr.Close();

                return rsPago;
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

        public static async Task<EAdmPago> AdmConsultaPago(int idPago)
        {

            EAdmPago rsPago = new EAdmPago();
            EAdmPago datosPago = AdmConsultaEstadoTransaccionPago(idPago);
            ECliParametros parametros = new ECliParametros();
            parametros.IdAplicacion = datosPago.Factura.Cliente.Aplicacion.Gracia;
            parametros.Gracia = datosPago.Gracia;
            parametros.Banco = datosPago.Banco;
            parametros.Transaccion = datosPago.Factura.Numero;

            try
            {

                if (datosPago.Estado == 1 || datosPago.Estado == 5 || datosPago.Estado == 6)
                {
                    string resultadoDatafast = await DAdmPagoWidget.AdmVerificarResultadoPago(parametros);
                    if (DGesMetodos.AdmVerificarJson(resultadoDatafast, 1))
                    {
                        rsPago = await DAdmPagoWidget.AdmReVerificacionPago(idPago, datosPago.Factura.Cliente.Aplicacion.Gracia, datosPago.Gracia, datosPago.Banco, resultadoDatafast, datosPago);
                    }
                    else
                    {
                        rsPago = AdmConsultaPagoResultado(idPago);
                    }
                }
                else if (datosPago.ResultadoCodigo.Equals("200.300.404"))
                {
                    string resultadoDatafast = await DAdmPagoWidget.AdmVerificarResultadoPago(parametros);

                    if (DGesMetodos.AdmVerificarJson(resultadoDatafast, 1))
                    {
                        rsPago = await DAdmPagoWidget.AdmReVerificacionPago(idPago, datosPago.Factura.Cliente.Aplicacion.Gracia, datosPago.Gracia, datosPago.Banco, resultadoDatafast, datosPago);
                    }
                    else
                    {
                        rsPago = AdmConsultaPagoResultado(idPago);
                    }
                }
                else
                {
                    rsPago = AdmConsultaPagoResultado(idPago);
                }



                /*if (datosPago.Estado == 1 || datosPago.Estado == 5 || datosPago.Estado == 6)
                {
                    //DATAFAST
                    string resultadoDatafast = await DAdmPagoWidget.AdmVerificarResultadoPago(datosPago.Factura.Numero, datosPago.Factura.Cliente.Aplicacion.Codigo, datosPago.Banco);

                    //PAYPHONE
                    string resultadoPayphone = await DAdmPagoPayphone.AdmVerificarPagoCliente(datosPago.Factura.Numero);

                    if (DGesMetodos.AdmVerificarJson(resultadoDatafast, 1))
                    {
                        rsPago = DAdmPagoWidget.AdmObtenerPago(idPago, resultadoDatafast, datosPago);
                    }
                    else if (DGesMetodos.AdmVerificarJson(resultadoPayphone, 2))
                    {
                        rsPago = DAdmPagoPayphone.AdmObtenerPago(idPago, resultadoPayphone, datosPago);
                    }
                    else
                    {
                        rsPago = AdmConsultaPagoResultado(idPago);
                    }


                }
                else if (datosPago.ResultadoCodigo.Equals("200.300.404"))
                {

                    string resultadoDatafast = await DAdmPagoWidget.AdmVerificarResultadoPago(datosPago.Factura.Numero, datosPago.Factura.Cliente.Aplicacion.Codigo, datosPago.Banco);

                    if (DGesMetodos.AdmVerificarJson(resultadoDatafast, 1))
                    {
                        rsPago = DAdmPagoWidget.AdmObtenerPago(idPago, resultadoDatafast, datosPago);
                    }
                    else
                    {
                        rsPago = AdmConsultaPagoResultado(idPago);
                    }
                }
                else
                {
                    rsPago = AdmConsultaPagoResultado(idPago);
                }*/

                return rsPago;
            }
            catch (SqlException)
            {
                return rsPago;
            }
        }

        public static EAdmPago AdmConsultaPagoInicio(int idPago)
        {

            EAdmPago rsPago = new EAdmPago();
            EAdmFactura rsFactura = new EAdmFactura();
            EAdmClientes rsCliente = new EAdmClientes();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT GraciaAplicacion, IdPago, Ip, Plataforma, IdTransaccion,CodigoAutenticacion,Referencia, Lote, Voucher, ParametroPersonalizado, NumeroDiferidos, ResultadoCodigo, ResultadoTexto, ResultadoTrama, EstadoPago, Intereses, RespuestaAdquiriente, FechaTransaccion, FechaIngreso, Bin, Digitos, Apoderado, Marca, Banco, Gracia, IdFactura, Numero,Comercio,Subtotal12,Subtotal0,Iva,Total, UrlRetorno, IdCliente, Identificacion, PrimerNombre, SegundoNombre, Apellido, Email, NumeroCliente, Telefono FROM ConsultarAplicacionClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Ip = rdr["Ip"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                    rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPago.Referencia = rdr["Referencia"].ToString();
                    rsPago.Lote = rdr["Lote"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                    rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.Intereses = rdr["Intereses"].ToString();
                    rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPago.FechaTransaccion = rdr["FechaTransaccion"].ToString();
                    rsPago.FechaIngreso = rdr["FechaIngreso"].ToString();

                    rsPago.Bin = rdr["Bin"].ToString();
                    rsPago.Digitos = rdr["Digitos"].ToString();
                    rsPago.Apoderado = rdr["Apoderado"].ToString();
                    rsPago.Marca = rdr["Marca"].ToString();
                    rsPago.Banco = rdr["Banco"].ToString();
                    rsPago.Gracia = rdr["Gracia"].ToString();

                    rsFactura.IdFactura = Convert.ToInt32(rdr["IdFactura"]);
                    rsFactura.Numero = rdr["Numero"].ToString();
                    rsFactura.Comercio = rdr["Comercio"].ToString();
                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                    rsCliente.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsCliente.Identificacion = rdr["Identificacion"].ToString();
                    rsCliente.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsCliente.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsCliente.Apellido = rdr["Apellido"].ToString();
                    rsCliente.Email = rdr["Email"].ToString();
                    rsCliente.Numero = rdr["NumeroCliente"].ToString();
                    rsCliente.Telefono = rdr["Telefono"].ToString();

                    rsFactura.Cliente = rsCliente;
                    rsPago.Factura = rsFactura;


                }
                rdr.Close();
                return rsPago;
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

        public static int AdmActualizarPago(int idPago)
        {

            int datos = 0;
            try
            {

                Conectar();
                SqlCommand cmd = new SqlCommand("UPDATE [Pago_Payment] SET [Estado] = 1 WHERE [IdPago] = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                cmd.ExecuteNonQuery();

                datos = 1;

                return datos;
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

        public static List<EAdmPago> AdmConsultaListaPagosExitosos(string plataforma)
        {
            List<EAdmPago> lstDatos = new List<EAdmPago>();

            EAdmAplicacion rsAplicacion;
            EAdmClientes rsClientes;
            EAdmFactura rsFactura;
            EAdmPago rsPago;

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacionClienteFacturaPago WHERE Plataforma = @plataforma AND EstadoPago = 2 AND CONVERT (DATE, FechaTransaccion) = CONVERT (DATE, DATEADD(HH, -5, GETDATE())) ORDER BY EstadoPago ASC, FechaTransaccion DESC", getCnn());
                cmd.Parameters.AddWithValue("@plataforma", plataforma);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();
                    rsClientes = new EAdmClientes();
                    rsFactura = new EAdmFactura();
                    rsPago = new EAdmPago();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["GraciaAplicacion"]);

                    rsClientes.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsClientes.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsClientes.Apellido = rdr["Apellido"].ToString();

                    rsFactura.IdFactura = Convert.ToInt32(rdr["IdFactura"]);
                    rsFactura.Numero = rdr["Numero"].ToString();
                    rsFactura.Comercio = rdr["Comercio"].ToString();
                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Ip = rdr["Ip"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                    rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPago.Referencia = rdr["Referencia"].ToString();
                    rsPago.Lote = rdr["Lote"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                    rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.Intereses = rdr["Intereses"].ToString();
                    rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPago.FechaTransaccion = rdr["FechaTransaccion"].ToString();

                    rsPago.Bin = rdr["Bin"].ToString();
                    rsPago.Digitos = rdr["Digitos"].ToString();
                    rsPago.Apoderado = rdr["Apoderado"].ToString();
                    rsPago.Marca = rdr["Marca"].ToString();
                    rsPago.Banco = rdr["Banco"].ToString();
                    rsPago.Gracia = rdr["Gracia"].ToString();

                    rsPago.Factura = rsFactura;
                    rsPago.Factura.Cliente = rsClientes;
                    rsPago.Factura.Cliente.Aplicacion = rsAplicacion;

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

        public static List<EAdmPago> AdmConsultaListaPagos(EAdmAuxiliares aux)
        {
            List<EAdmPago> lstDatos = new List<EAdmPago>();

            EAdmCredenciales credenciales = EGloGlobales.obtenerCredenciales();
            EAdmAplicacion rsAplicacion;
            EAdmClientes rsClientes;
            EAdmFactura rsFactura;
            EAdmPago rsPago;

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdAplicacion, Nombre, Codigo, IdCliente, Identificacion, PrimerNombre, SegundoNombre, Codigo, Apellido, Email, Telefono, IdFactura, Numero, Comercio, Subtotal12, Subtotal0, Iva, Total, UrlRetorno, EstadoAplicacionPago, IdPv, IdPago, Ip, Plataforma, IdTransaccion, CodigoAutenticacion, Referencia, Lote, Voucher, ParametroPersonalizado, NumeroDiferidos, ResultadoCodigo, ResultadoTexto, ResultadoTrama, EstadoPago, Intereses, RespuestaAdquiriente, FechaTransaccion, Bin, Digitos, Apoderado, Marca, Banco, Gracia, GraciaAplicacion, Cuotas, AplicacionFactura, Recurrencia FROM ConsultarAplicacionClienteFacturaPago WHERE FechaTransaccionDate BETWEEN @fechaInicio AND @fechaFin " + aux.Cadena + " ORDER BY FechaTransaccion DESC", getCnn());
                cmd.Parameters.AddWithValue("@fechaInicio", aux.FechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", aux.FechaFin);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();
                    rsClientes = new EAdmClientes();
                    rsFactura = new EAdmFactura();
                    rsPago = new EAdmPago();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["GraciaAplicacion"]);
                    rsAplicacion.Recurrencia = Convert.ToInt32(rdr["Recurrencia"]);

                    rsClientes.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsClientes.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsClientes.Apellido = rdr["Apellido"].ToString();
                    rsClientes.Email = rdr["Email"].ToString();
                    rsClientes.Telefono = rdr["Telefono"].ToString();

                    rsFactura.IdFactura = Convert.ToInt32(rdr["IdFactura"]);
                    rsFactura.Numero = rdr["Numero"].ToString();
                    rsFactura.Comercio = rdr["Comercio"].ToString();
                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                    rsFactura.EstadoAplicacion = rdr["EstadoAplicacionPago"].ToString();
                    rsFactura.IdPv = rdr["IdPv"].ToString();
                    rsFactura.Cuotas = rdr["Cuotas"].ToString();
                    rsFactura.Aplicacion = rdr["AplicacionFactura"].ToString();

                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Ip = rdr["Ip"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                    rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPago.Referencia = rdr["Referencia"].ToString();
                    rsPago.Lote = rdr["Lote"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                    rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.Intereses = rdr["Intereses"].ToString();
                    rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPago.FechaTransaccion = rdr["FechaTransaccion"].ToString();
                    rsPago.Link = credenciales.urlPlataforma + "?c=" + DGesEncriptacion.CodificarBase64(Convert.ToInt32(rdr["IdPago"]).ToString()) + "&p=" + rdr["Codigo"].ToString();

                    rsPago.Bin = rdr["Bin"].ToString();
                    rsPago.Digitos = rdr["Digitos"].ToString();
                    rsPago.Apoderado = rdr["Apoderado"].ToString();
                    rsPago.Marca = rdr["Marca"].ToString();
                    rsPago.Banco = rdr["Banco"].ToString();
                    rsPago.Gracia = rdr["Gracia"].ToString();

                    rsPago.Recibo = "";
                    /*
                    if (Convert.ToInt32(rdr["EstadoPago"]) == 2)
                    {
                        rsPago.Recibo = generarRecibo(
                                        rdr["Plataforma"].ToString(),
                                        rdr["ResultadoTrama"].ToString(),
                                        rdr["FechaRecibo"].ToString(),
                                        rdr["HoraRecibo"].ToString(),
                                        rdr["Subtotal12"].ToString(),
                                        rdr["Subtotal0"].ToString(),
                                        rdr["Iva"].ToString(),
                                        rdr["Total"].ToString(),
                                        rdr["Lote"].ToString(),
                                        rdr["Referencia"].ToString()
                                        );
                    }
                    else
                    {
                        rsPago.Recibo = "";
                    }*/

                    rsPago.Factura = rsFactura;
                    rsPago.Factura.Cliente = rsClientes;
                    rsPago.Factura.Cliente.Aplicacion = rsAplicacion;

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

        public static EAdmPago AdmConsultaEstadoPago(int idPago)
        {

            EAdmPago rsPago = new EAdmPago();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdPago, EstadoPago, ResultadoCodigo, ResultadoTrama, Plataforma FROM ConsultarClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                }
                rdr.Close();
                return rsPago;
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

        public static string AdmConsultaRecibo(int idPago)
        {

            string recibo = "";

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdPago, EstadoPago, Plataforma, ResultadoTrama, FechaRecibo, HoraRecibo, Subtotal12, CONVERT(NVARCHAR(10), (CONVERT(NUMERIC(11,2), Subtotal0))) AS Subtotal0, CONVERT(NVARCHAR(10), (CONVERT(NUMERIC(11,2), Subtotal12)) + (CONVERT(NUMERIC(11,2), Subtotal0))) AS Subtotal, Iva, Total, Lote, Referencia FROM ConsultarAplicacionClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    if (Convert.ToInt32(rdr["EstadoPago"]) == 2)
                    {
                        recibo = generarRecibo(
                                        rdr["Plataforma"].ToString(),
                                        rdr["ResultadoTrama"].ToString(),
                                        rdr["FechaRecibo"].ToString(),
                                        rdr["HoraRecibo"].ToString(),
                                        rdr["Subtotal12"].ToString(),
                                        rdr["Subtotal0"].ToString(),
                                        rdr["Subtotal"].ToString(),
                                        rdr["Iva"].ToString(),
                                        rdr["Total"].ToString(),
                                        rdr["Lote"].ToString(),
                                        rdr["Referencia"].ToString()
                                        );
                    }
                    else
                    {
                        recibo = "";
                    }

                }
                rdr.Close();
                return recibo;
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

        public static EAdmPago AdmConsultaEstadoTransaccionPago(int idPago)
        {

            EAdmPago rsPago = new EAdmPago();
            EAdmFactura rsFactura = new EAdmFactura();
            EAdmClientes rsClientes = new EAdmClientes();
            EAdmAplicacion rsAplicacion = new EAdmAplicacion();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacionClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Ip = rdr["Ip"].ToString();
                    rsPago.Plataforma = rdr["Plataforma"].ToString();
                    rsPago.IdTransaccion = rdr["IdTransaccion"].ToString();
                    rsPago.CodigoAutenticacion = rdr["CodigoAutenticacion"].ToString();
                    rsPago.Referencia = rdr["Referencia"].ToString();
                    rsPago.Lote = rdr["Lote"].ToString();
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.ParametroPersonalizado = rdr["ParametroPersonalizado"].ToString();
                    rsPago.NumeroDiferidos = rdr["NumeroDiferidos"].ToString();
                    rsPago.ResultadoCodigo = rdr["ResultadoCodigo"].ToString();
                    rsPago.ResultadoTexto = rdr["ResultadoTexto"].ToString();
                    //rsPago.ResultadoTrama = rdr["ResultadoTrama"].ToString();
                    rsPago.Estado = rdr["EstadoPago"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EstadoPago"]);
                    rsPago.Intereses = rdr["Intereses"].ToString();
                    rsPago.RespuestaAdquiriente = rdr["RespuestaAdquiriente"].ToString();
                    rsPago.FechaTransaccion = rdr["FechaTransaccion"].ToString();

                    rsPago.Bin = rdr["Bin"].ToString();
                    rsPago.Digitos = rdr["Digitos"].ToString();
                    rsPago.Apoderado = rdr["Apoderado"].ToString();
                    rsPago.Marca = rdr["Marca"].ToString();
                    rsPago.Banco = rdr["Banco"].ToString();
                    rsPago.Gracia = rdr["Gracia"].ToString();

                    rsFactura.Numero = rdr["Numero"].ToString();

                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal = rdr["Subtotal"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();

                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["GraciaAplicacion"]);

                    rsPago.Factura = rsFactura;
                    rsPago.Factura.Cliente = rsClientes;
                    rsPago.Factura.Cliente.Aplicacion = rsAplicacion;

                }
                rdr.Close();
                return rsPago;
            }
            catch (SqlException)
            {
                Cerrar();
                rsPago.ResultadoTexto = "El servicio se encuentra temporalmente ocupado, intente nuevamente en unos minutos.";
                return rsPago;
            }
            finally
            {
                Cerrar();
            }
        }

        public static EAdmPago AdmConsultaReciboPago(int idPago)
        {

            EAdmPago rsPago = new EAdmPago();
            EAdmFactura rsFactura = new EAdmFactura();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdPago, Voucher,Plataforma, ResultadoTrama,FechaRecibo,HoraRecibo,Subtotal12,CONVERT(NVARCHAR(10), (CONVERT(NUMERIC(11,2), Subtotal0))) AS Subtotal0,CONVERT(NVARCHAR(10), (CONVERT(NUMERIC(11,2), Subtotal12)) + (CONVERT(NUMERIC(11,2), Subtotal0))) AS Subtotal,Iva,Total,Lote,Referencia FROM ConsultarAplicacionClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    rsPago.IdPago = Convert.ToInt32(rdr["IdPago"]);
                    rsPago.Voucher = rdr["Voucher"].ToString();
                    rsPago.Recibo = generarRecibo(
                    rdr["Plataforma"].ToString(),
                    rdr["ResultadoTrama"].ToString(),
                    rdr["FechaRecibo"].ToString(),
                    rdr["HoraRecibo"].ToString(),
                    rdr["Subtotal12"].ToString(),
                    rdr["Subtotal0"].ToString(),
                    rdr["Subtotal"].ToString(),
                    rdr["Iva"].ToString(),
                    rdr["Total"].ToString(),
                    rdr["Lote"].ToString(),
                    rdr["Referencia"].ToString()
                    );

                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();

                    rsPago.Factura = rsFactura;
                }
                rdr.Close();
                return rsPago;
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

        public static EAdmAuxiliares AdmConsultaResumenPago(string fechaInicio, string fechaFin)
        {

            EAdmAuxiliares rsPago = new EAdmAuxiliares();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("GestionReporte", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@fechaInicio", SqlDbType.NVarChar, 250);
                cmd.Parameters.Add("@fechaFin", SqlDbType.NVarChar, 250);

                cmd.Parameters["@fechaInicio"].Value = fechaInicio;
                cmd.Parameters["@fechaFin"].Value = fechaFin;

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsPago.EstadoReporte = rdr["Estados"].ToString();
                    rsPago.TotalReporte = rdr["Totales"].ToString();

                }
                rdr.Close();
                return rsPago;

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

        public static EAdmAnulacion AdmAnularPago(int idPago)
        {
            EAdmAnulacion anulacion = new EAdmAnulacion();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionPagoEstados", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idPago", SqlDbType.Int);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@resultadoCodigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTexto", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = 1;
                cmd.Parameters["@idPago"].Value = idPago;
                cmd.Parameters["@estado"].Value = 5;
                cmd.Parameters["@resultadoCodigo"].Value = "98494";
                cmd.Parameters["@resultadoTexto"].Value = "Pago Anulado";

                cmd.ExecuteNonQuery();

                var res = cmd.Parameters["@valor"].Value.ToString();
                dynamic valores = JObject.Parse(res);
                anulacion.IdPago = Convert.ToInt32(valores.IdPago);
                anulacion.Estado = Convert.ToInt32(valores.Estado);
                anulacion.Descripcion = valores.Descripcion;

                return anulacion;

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

        public static EAdmAnulacion AdmExpirarPago(int idPago)
        {
            EAdmAnulacion expiracion = new EAdmAnulacion();

            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionPagoEstados", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idPago", SqlDbType.Int);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@resultadoCodigo", SqlDbType.NVarChar);
                cmd.Parameters.Add("@resultadoTexto", SqlDbType.NVarChar);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = 2;
                cmd.Parameters["@idPago"].Value = idPago;
                cmd.Parameters["@estado"].Value = 6;
                cmd.Parameters["@resultadoCodigo"].Value = "68979";
                cmd.Parameters["@resultadoTexto"].Value = "Pago Expirado";

                cmd.ExecuteNonQuery();

                var res = cmd.Parameters["@valor"].Value.ToString();
                dynamic valores = JObject.Parse(res);
                expiracion.IdPago = Convert.ToInt32(valores.IdPago);
                expiracion.Estado = Convert.ToInt32(valores.Estado);
                expiracion.Descripcion = valores.Descripcion;

                return expiracion;

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

        public static EAdmFactura AdmConsultaDetallePago(int idpago)
        {

            EAdmAplicacion rsAplicacion = new EAdmAplicacion();
            EAdmClientes rsClientes = new EAdmClientes();
            EAdmFactura rsFactura = new EAdmFactura();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT IdAplicacion, Nombre, LogoPrimario, LogoSecundario, ColorPrimario, EstadoAplicacion, Recurrencia, GraciaAplicacion, ColorSecundario, FondoPrimario, FondoSecundario, LogoPrimarioTamano, LogoSecundarioTamano, IdentificacionAplicacion, Codigo, IdCliente, Identificacion, PrimerNombre, SegundoNombre, Apellido, Email, Telefono, NumeroCliente, EstadoCliente, IdFactura, Numero, Comercio, Subtotal12, Subtotal0, Iva, Total, EstadoFactura, UrlRetorno FROM ConsultarAplicacionClienteFacturaPago WHERE IdPago = @idpago", getCnn());
                cmd.Parameters.AddWithValue("@idpago", idpago);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.LogoPrimario = rdr["LogoPrimario"].ToString();
                    rsAplicacion.LogoSecundario = rdr["LogoSecundario"].ToString();
                    rsAplicacion.ColorPrimario = rdr["ColorPrimario"].ToString();
                    rsAplicacion.ColorSecundario = rdr["ColorSecundario"].ToString();
                    rsAplicacion.FondoPrimario = rdr["FondoPrimario"].ToString();
                    rsAplicacion.FondoSecundario = rdr["FondoSecundario"].ToString();
                    rsAplicacion.LogoPrimarioTamano = rdr["LogoPrimarioTamano"].ToString();
                    rsAplicacion.LogoSecundarioTamano = rdr["LogoSecundarioTamano"].ToString();
                    rsAplicacion.Identificacion = rdr["IdentificacionAplicacion"].ToString();
                    rsAplicacion.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    rsAplicacion.Estado = Convert.ToInt32(rdr["EstadoAplicacion"]);
                    rsAplicacion.Recurrencia = Convert.ToInt32(rdr["Recurrencia"]);
                    rsAplicacion.Gracia = Convert.ToInt32(rdr["GraciaAplicacion"]);

                    rsClientes.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsClientes.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsClientes.Apellido = rdr["Apellido"].ToString();
                    rsClientes.Email = rdr["Email"].ToString();
                    rsClientes.Telefono = rdr["Telefono"].ToString();
                    rsClientes.Numero = rdr["NumeroCliente"].ToString();
                    rsClientes.Estado = Convert.ToInt32(rdr["EstadoCliente"]);

                    rsFactura.IdFactura = Convert.ToInt32(rdr["IdFactura"]);
                    rsFactura.Numero = rdr["Numero"].ToString();
                    rsFactura.Comercio = rdr["Comercio"].ToString();
                    rsFactura.Subtotal12 = rdr["Subtotal12"].ToString();
                    rsFactura.Subtotal0 = rdr["Subtotal0"].ToString();
                    rsFactura.Iva = rdr["Iva"].ToString();
                    rsFactura.Total = rdr["Total"].ToString();
                    rsFactura.UrlRetorno = rdr["UrlRetorno"].ToString();
                    rsFactura.Estado = Convert.ToInt32(rdr["EstadoFactura"]);

                    rsFactura.Cliente = rsClientes;
                    rsFactura.Cliente.Aplicacion = rsAplicacion;

                }
                rdr.Close();
                return rsFactura;
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

        //METODOS ADICIONALES

        public static string generarRecibo(string plataforma, string trama, string pFecha, string pHora,
            string pSubBase12, string pSubBase0, string pSubBase, string pIva, string pTotal, string pLote, string pReferencia)
        {
            string recibo = "";

            if (trama.Equals("") || trama.Equals(null))
            {
                recibo = "0";
            }
            else
            {
                if (plataforma.Equals("PAYPHONE"))
                {
                    dynamic valores = JObject.Parse(trama);
                    string marca = valores.cardBrand;
                    string bin = valores.bin;
                    string digitos = valores.lastDigits;
                    string transaccion = valores.transactionId;
                    string diferidos = valores.deferred == true ? valores.deferredMessage : "Corriente";
                    string fecha = pFecha;
                    string hora = pHora;
                    string moneda = valores.currency;
                    string autorizacion = valores.authorizationCode;
                    string subtotal12 = pSubBase12;
                    string subtotal0 = pSubBase0;
                    string subtotal = pSubBase;
                    string iva = pIva;
                    string total = pTotal;
                    string nombre = valores.optionalParameter4;

                    recibo = generarReciboPayphone(marca, bin, digitos, transaccion, diferidos, fecha, hora, moneda, autorizacion, subtotal12, subtotal0, subtotal, iva, total, nombre);
                }
                else if (plataforma.Equals("DATAFAST"))
                {
                    dynamic valores = JObject.Parse(trama);
                    string marca = valores.paymentBrand;
                    string bin = valores.card.bin;
                    string digitos = valores.card.last4Digits;
                    string expiracion = Convert.ToInt32(valores.card.expiryMonth) + "/" + Convert.ToInt32(valores.card.expiryYear);
                    string lote = pLote;
                    string referencia = pReferencia;
                    string fecha = pFecha;
                    string hora = pHora;
                    int intereses = Convert.ToInt32(valores.customParameters.SHOPPER_interes);
                    string moneda = valores.currency;
                    string diferidos = valores.recurring == null ? "0" : valores.recurring.numberOfInstallments;
                    string aprobacion = valores.resultDetails.AuthCode;
                    string subtotal12 = pSubBase12;
                    string subtotal0 = pSubBase0;
                    string subtotal = pSubBase;
                    string iva = pIva;
                    string total = pTotal;
                    string cliente = valores.card.holder;

                    recibo = generarReciboDatafast(marca, bin, digitos, expiracion, lote, referencia, fecha, hora, intereses, moneda, diferidos, aprobacion, subtotal12, subtotal0, subtotal, iva, total, cliente);
                }
            }


            return recibo;
        }

        public static string generarReciboPayphone(string pMarcaTarjeta, string pBinTarjeta,
            string pDigitosTarjeta, string pReferencia, string pDiferidos, string pFecha, string pHora, string pMoneda,
            string pAutorizacion, string pSubBase12, string pSubBase0, string pSubBase, string pIva, string pTotal,
            string pCliente)
        {

            var pgSize = new Rectangle(297, 505);
            var doc = new Document(pgSize, 10, 10, 10, 10);
            var mem = new MemoryStream();

            PdfWriter wri = PdfWriter.GetInstance(doc, mem);
            doc.Open();

            Paragraph separador = new Paragraph();
            separador.Add("");
            separador.SpacingBefore = 12f;

            Paragraph titulo1 = new Paragraph();
            titulo1.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 15.5f, BaseColor.BLACK);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.Add("SEGUROS EQUINOCCIAL");
            titulo1.SpacingAfter = 12f;
            titulo1.SpacingBefore = 12f;

            Paragraph direccion = new Paragraph();
            direccion.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            direccion.Alignment = Element.ALIGN_CENTER;
            direccion.Add("ELOY ALFARO N 33 400 Y AYARZA");

            Paragraph direccion2 = new Paragraph();
            direccion2.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            direccion2.Alignment = Element.ALIGN_CENTER;
            direccion2.Add("2447574");
            direccion2.SpacingAfter = 12f;

            Paragraph marcaTarjeta = new Paragraph();
            marcaTarjeta.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 15.5f, BaseColor.BLACK);
            marcaTarjeta.Alignment = Element.ALIGN_CENTER;
            marcaTarjeta.Add(pMarcaTarjeta);
            marcaTarjeta.SpacingAfter = 8f;

            Paragraph tarjetaTexto = new Paragraph();
            tarjetaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tarjetaTexto.Add("TARJETA:");

            Paragraph tarjetaValor = new Paragraph();
            tarjetaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tarjetaValor.Add(pBinTarjeta + "XXXXXX" + pDigitosTarjeta);

            Paragraph referenciaTexto = new Paragraph();
            referenciaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            referenciaTexto.Add("REF:");

            Paragraph referenciaValor = new Paragraph();
            referenciaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            referenciaValor.Add("000" + pReferencia);

            Paragraph diferidosTexto = new Paragraph();
            diferidosTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            diferidosTexto.Add("DESC:");

            Paragraph diferidosValor = new Paragraph();
            diferidosValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            diferidosValor.Add(pDiferidos);

            Paragraph fechaTexto = new Paragraph();
            fechaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            fechaTexto.Add("FECHA:");

            Paragraph fechaValor = new Paragraph();
            fechaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            fechaValor.Add(pFecha);

            Paragraph horaTexto = new Paragraph();
            horaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            horaTexto.Add("HORA:");

            Paragraph horaValor = new Paragraph();
            horaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            horaValor.Add(pHora);

            Paragraph autorizacion = new Paragraph();
            autorizacion.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 13.5f, BaseColor.BLACK);
            autorizacion.Alignment = Element.ALIGN_CENTER;
            autorizacion.Add("AUTORIZACIÓN #: " + pAutorizacion);
            autorizacion.SpacingAfter = 18f;
            autorizacion.SpacingBefore = 18f;

            Paragraph tipoModena = new Paragraph();
            tipoModena.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tipoModena.Add(pMoneda);

            Paragraph subtotal12Texto = new Paragraph();
            subtotal12Texto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal12Texto.Add("BASE CONSUMO TARIFA 12:");

            Paragraph subtotal12Valor = new Paragraph();
            subtotal12Valor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal12Valor.Add("$ " + pSubBase12);

            Paragraph subtotal0Texto = new Paragraph();
            subtotal0Texto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal0Texto.Add("BASE CONSUMO TARIFA 0:");

            Paragraph subtotal0Valor = new Paragraph();
            subtotal0Valor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal0Valor.Add("$ " + pSubBase0);

            Paragraph subtotalTexto = new Paragraph();
            subtotalTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotalTexto.Add("SUBTOTAL CONSUMOS:");

            Paragraph subtotalValor = new Paragraph();
            subtotalValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotalValor.Add("$ " + pSubBase);

            Paragraph ivaTexto = new Paragraph();
            ivaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            ivaTexto.Add("IVA:");

            Paragraph ivaValor = new Paragraph();
            ivaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            ivaValor.Add("$ " + pIva);

            Paragraph totalTexto = new Paragraph();
            totalTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            totalTexto.Add("VR. TOTAL:");

            Paragraph totalValor = new Paragraph();
            totalValor.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 11f, BaseColor.BLACK);
            totalValor.Add("$ " + pTotal);

            Paragraph descripcion = new Paragraph();
            descripcion.Font = FontFactory.GetFont(FontFactory.COURIER, 7.7f, BaseColor.BLACK);
            descripcion.Alignment = Element.ALIGN_JUSTIFIED;
            descripcion.Add("DEBO Y PAGARÉ AL EMISOR INCONDICIONALMENTE, Y SIN PROTESTO EL TOTAL DE ESTE PAGARÉ MAS LOS INTERESES Y CARGOS POR SERVICIO. EN CASO DE MORA PAGARÉ LA TASA MÁXIMA AUTORIZADA PARA EL EMISOR. DECLARO QUE EL PRODUCTO DE ESTA TRANSACCIÓN NO SERÁ UTILIZADO EN ACTIVIDADES DE LAVADO DE DINERO Y ACTIVO(LEY 108).");
            descripcion.SpacingBefore = 11f;

            Paragraph nombreTexto = new Paragraph();
            nombreTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            nombreTexto.Add("NOMBRE:");

            Paragraph nombreValor = new Paragraph();
            nombreValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            nombreValor.Add(pCliente);

            Paragraph copia = new Paragraph();
            copia.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10.5f, BaseColor.BLACK);
            copia.Alignment = Element.ALIGN_CENTER;
            copia.Add("-COPIA2-");
            copia.SpacingBefore = 18f;
            copia.SpacingAfter = 13f;

            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            float[] widths = new float[] { 20, 40, 15, 25 };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(tarjetaTexto);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(tarjetaValor);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(referenciaTexto);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(referenciaValor);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            PdfPTable table2 = new PdfPTable(2) { WidthPercentage = 100 };
            float[] widths2 = new float[] { 20, 80 };
            table2.SetWidths(widths2);
            PdfPCell cell2 = new PdfPCell();

            cell2 = new PdfPCell(diferidosTexto);
            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            cell2 = new PdfPCell(diferidosValor);
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            PdfPTable table3 = new PdfPTable(4) { WidthPercentage = 100 };
            float[] widths3 = new float[] { 17, 50, 14, 20 };
            table3.SetWidths(widths3);
            PdfPCell cell3 = new PdfPCell();

            cell3 = new PdfPCell(fechaTexto);
            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(fechaValor);
            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(horaTexto);
            cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(horaValor);
            cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            PdfPTable table4 = new PdfPTable(3) { WidthPercentage = 100 };
            float[] widths4 = new float[] { 60, 10, 30 };
            table4.SetWidths(widths4);
            PdfPCell cell4 = new PdfPCell();
            PdfPCell cell5 = new PdfPCell();
            PdfPCell cell6 = new PdfPCell();
            PdfPCell cell7 = new PdfPCell();
            PdfPCell cell8 = new PdfPCell();

            cell4 = new PdfPCell(subtotal12Texto);
            cell4.HorizontalAlignment = Element.ALIGN_LEFT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell4 = new PdfPCell(tipoModena);
            cell4.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell4 = new PdfPCell(subtotal12Valor);
            cell4.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell5 = new PdfPCell(subtotal0Texto);
            cell5.HorizontalAlignment = Element.ALIGN_LEFT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell5 = new PdfPCell(tipoModena);
            cell5.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell5 = new PdfPCell(subtotal0Valor);
            cell5.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell6 = new PdfPCell(subtotalTexto);
            cell6.HorizontalAlignment = Element.ALIGN_LEFT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell6 = new PdfPCell(tipoModena);
            cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell6 = new PdfPCell(subtotalValor);
            cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell7 = new PdfPCell(ivaTexto);
            cell7.HorizontalAlignment = Element.ALIGN_LEFT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell7 = new PdfPCell(tipoModena);
            cell7.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell7 = new PdfPCell(ivaValor);
            cell7.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell8 = new PdfPCell(totalTexto);
            cell8.HorizontalAlignment = Element.ALIGN_LEFT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            cell8 = new PdfPCell(tipoModena);
            cell8.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            cell8 = new PdfPCell(totalValor);
            cell8.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            PdfPTable table5 = new PdfPTable(1) { WidthPercentage = 100 };
            PdfPCell cell9 = new PdfPCell();
            cell9 = new PdfPCell(descripcion);
            cell9.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell9.BorderWidth = 0;
            table5.AddCell(cell9);

            string imgPayphone = "iVBORw0KGgoAAAANSUhEUgAAAoYAAACeCAYAAACvkMumAAApYklEQVR4Xu2dedhV4/6H1/v2Ns6DIakMpYHIECEUmedKpQFlyJBEhhOOeR4OdQxpQFGSECciQhQnQkJpRE6lhMZB4/u7//j+0dUv73rW3mvtvdben/u67uvZXVfvXuPe67OfscDLEMUDvFIUh+Mh2Misj1WwMlbAaBFbcS2uxN9wNs7DmTi5oI+33MtbhBBCCFEQcRjcnaITnozHWgiML2ImfoBj8ROC4jZPCCGEEAqGaYTBIop22N0CYSlMHmIhjsQhBMSfPSGEEEIoGAYIhGUoLsYbsD7mBmIrjsCHCYizPCGEEEIoGJYQCAsoLsD7sA7mJqIYX8J+BMRFnhBCCCEUDHcIhY0phmJLzA/EOvwnPkFA3OrlN0IIIYSCodUS9saHsSzmH2IyXkA4XOjlJ0IIIYSCIaGwMsUwbIf5jViBXQiH73qJRwghhBCFAUNhPYrPLBQKUR3f5r7o7SUeIYQQQhQE7E84EffEHRHiIWoO+3mJRQghhBCFAULh5BJCoRD/4D550EssQgghhCh0bD6eiLugH0Lh8A4v9xBCCCEUDAmFVSjeCVBTKMSdhMMLvMQhhBBCiEKfKWmG4/4YBCGGEA6bezmDEEIIoRrDa/FcDIoQZXG0TW2UZIQQQggFQx7oB1A8iKkixL44AJOKEEIIoWBIKCykGIplMB2E6MH9dKKXWIQQQgjVGF6ER2IYCPEE4bDISxZCCCGEgiEP8PIU92NYCNEYL8WEIYQQQqjGsDvWwjAR4pYE1RoKIYQQCoY8uMtQ3IxhI0Rd7IYJQQghhFCNYXusi1EgxI2YEIQQQggFwx4IhtiGC/A9HIL3YB87T52xrdkDr8Bb8F84BqfhKgRD7B/vSa+FEEIIUWTNyLUp2mA+8xN+jFMt2M0q6OP95aUB53VPikPwSDzaLIv5ygX4JcYQIYQQQhRYgLmO4rE8rBGchG/geELgAi9iOM8VKI7Fc/Ec3APzieVYi3O9zYstQgghhILhuxSnYD7wHQ7DkQSUZV6WsInEW2MPbI/lMR84lPM+3YsdQgghhCggoJSmXIEVMVcpxrHYn1Ay2YsZXIMaFJfh1VgHc5kbuQaPerFECCGEUDBsSTkFc5GtOAIfJIzM9mKOTRl0MfbDvTDnsGb7M7zYIYQQQogiPARzkXfwJkLI915CYF83UTxDQHyOshfehtUxZ4j3/SaEEEKoxvDflL0xV1iIVxGyxnsJh2tTk+JhvBhziapcn9VerBBCCCFEITbOoX6E/XF/C4WJh+P4Ay/hZSv8CXOF/TBmCCGEEKII98WksxgvJER9GHKNXTWKg+wc7YW7Yw0sh4W4DcH7A//ERbgQZ+EC9mdrSAHxE/aF/fD+nSMTkdfHrzBmCCGEEGpKXka5GyaV97EL4en3EILgvhQnYys8Ks0BIH/hNPzM9nEK+7gxhH28iGJgwqe3uYRz8ZwXO4QQQggFw3WUFTCJPIy3WM1cOmGwK3bEphgV6/AtHGUjczensc/NKMYleG3r6zj+/l7sEEIIIRQMi73kscUGmAxJY3LpM7EPnoCZZhk+i09xDEtSPIY9LGgeiknjDo77bi9NhBBCCKFguBnPI1j8J8VA2BFvxyYxOZbn8H6O55cUjqcqxVg8HpPEXRzvnV6sEEIIIUQhJolNeGqKofAEiuk4ykJhHCiNl+Nc9u9BrBJwUMoqq/l8H/MKIYQQQigY3hp05DFhazd8iZcf4EEYR8riP/AH9rVdwHC43mpBl2FeIIQQQggFw7/wqYCh8GyKmdgZk0BtfK2YIIvVAoTDldZnMS8QQgghhILhfALQBsdAWBoH8PJN3AWTRmeczjE099yZgSkjhBBCCFGESaGyYyjcxQZkHINh8SfOxp9xOa7DDVhk+1UR62JD3Duk87o3TuF4LiUQj/D8qYIRI7geDSjqYLZZi1vwD1xm62znH7ofK1E0j0n/7/Xmcu7HFRk6fr4nsWQWsT/zvZxDCAXDvQhJTQlJ3/s8tN/F+pgO3+JHOAk/Y5u/BQgORRQHWzBtjSdixTT6Hr7Ie+7DPtzjlcxZGD3iauyDscImqp+DX+FUfN8ezrmNaIAfxTCwrrUf0tNxGn5IOJvphU93vANLYgBeizmIEJquZhKeREjaspNAdqCFwtqYCj/iSHyJ958d4gO7PMXZeL6Ft1KYCo/hDexb8U62cQbFW5quJiMBrL8Fw7izFafgc/gK5/svLxdRjeHBFr6SwEJ8CYcSEn8M6fjvdAmGbC9Hg6EQGpXcGt8jCO23XSgqhV15OTnFUDgNO+B+hJXbLRSGBu+3AUdjW6vJfALXYVD64qsca50d+lJezcvXcHuEKIWtcDguIkDciOW97CHEXngzzifQjcGGnhBCTckhcLzN+zfb+lc1xpoYlDm2NNs7XoZgWwsprmHf76ZE7/KA4bwdnsvff2fh8gCsiiUhRE18GHsTDntwH37gZQ8hCvA8bEs4fNhaEDZ6QgjVGKZJY2yZQihchzfhQRYKMw7b/R2vsuXsPkvhmjXDowOGQiHq4kTC4UNY4GUXIUrhzfhfAmI9TwihYJgFPseDCWWPxGEUJ/swg+JYC6qbMRMIcRPSlOeV8bKPEIfgVOsnnjGEEAqGT+FxcZu2gP3Zho9Yf7BfMRMI0R6Hx6TmUIg9rDa7gRcxQggFw214NeEL4zvXG/v2X4oW+C1mAiHOxzsxDgixG75lczMKITT4JBK24AWErpfTnBKhkKIRNsS9sAZWwg24Dn/F+TiTba1MIxz+j20dx8v38AhMLuI1fBLDogxWwPK4G9bDxtjc/p0q/+Sem8i9N9nLZcQ6PBPDpBoW4q5YC/fDg7ApFmAqNML+eCmGjhBCwfCGVEMhAa2GNbedgy2xmsuf8XffUk7EMWz78xTC4Sq2fZrVHO6JCUTYaguTMjSX3f52r16E9VNoNRjIexxs84PmJmJLBu/H6hSnYFc8I4WQeAnvMYz9neKFiBBCTcmL8akUAmEzfImXS3EwnoHVXHMdNsPrrTP1HLwcywcMh39SPIRCuNwvs/Aeq9HujL9gEA7AHhgGQvfjCnwZz7Ja7TcwKI9iqAghFAynBKkBIbztha/y8hvsjKUxXRriM7iA974gYEf/j1CIoAOZXragNxqDcEMEA1GEQuJcm9y/G25AV1pwP7byQkQIoWC41TEQFmAfXs7G9hGOuHsBPw4wX9c2TAWhgLjWag6fCfgj5iSMAqGAONJaX9ajK1diaAghFAxb+NWAENKqUvwH+2M5jJpjcQbbPcfz52hMCSFsTe2rcQK60gGjQigcfkRxCbpydpjLOAohFAzr44UlhMI9KSbjmZhJquHrbP/KEvatIsWNmA5C4XArxWW43v1BHN13ghDW1WEsulBetdhCKBiGio22bL+T4HWYLUV3YBbP+9Psx8NYZod929U6azfEdBEKh/+jGIgu7JaB+06I2wK1soSIEELT1ZTHVwlbn1N+gBuxBZ6GBZhtbsRO7N9Yyt9wP2wb8hrIQgzE69GFw3E2RoVQreFMvvMmO4a+I1AIoWAYOi3MsNiGU/An3B1bp9FHsR72wagQehAzKt77npdNnaauiR4hxuKxuXA/CqGmZPE1NuFh2wq742n8ex8ch3FFiM/Qhb0wLgjdjzWpXazgxRQhVGMofsYTCYMrdqiRWWp9GT/Clhg3hPgBXdgz5FUwSlE0MvfF3bEmVsdCBG8LrsXN+AcuxZ9wroc2iCYP0f1o1Mtk9wZb9eoQbIL1sTpWxQJcby7BhTgTp3OPbvRiCMeyO8X+2MCsjhWwEm7D1bgWl+E8cybH85eXQDje2hQH2THvjVW2u3brcEPCrt2B2x1LDayMpeza/W7XbYEdyzccy+bMB0PxgIXCnTXXbeZC3sLLjzFuCLEYXagSQhg82Eb7n4yHYQVMlfW83zRbP/x1QuLsgPtyKMVx6Mcy3ntUFh8C3SmqOSyz+GqOdG9YzTGvtYDiR7kMnP99bRaL0/GwgK1pm/n7KdY8Popj+z2LYaK0fe7OwtbYCIOykfeZahUdr3A8P0S0r0dSYIkM9AtuvE9Tis54poVCCHTtPqV8PdvXzo6lOUVXbJPC4Ni/+PtJlG/iaMsqGQiGYiKWxBTchGUwTgixFl0ol2KwqWRT41yK+2NYVMBW5n02kOxB/A/hYpvLlyU+jn5s4b3f4z3/yFItx3MOA+L+ibnEasdgWCXCc9+a4iY8NY0BiaXxePNB3vNZyrszGTJs4YTrsAvuhulQFluZd9oPs6HI+tU838LjVLwDS2IYbvybY25BcReegqlSGlubD2137ZZ7GcJmJrkYe2FTTJVyeKrZn/cdQfkgxzI/2j6G4k+/JcmsqjpuCLENXSgVMNQUYV/rZvEY7p+BgWRj8TO2e6DLOtIUUxxbTzpiNuiMBQ7Xb1g+rk4FhREEqbr4ptWKhTlLRQXsjbPZRqcMhIpa+Awv5+O1FgrD5nAchPPZVk8szHKtWhV8npdT8ZSQZzG52q7d+Rk6lq7WrWKgcyh0D/eX2LH0xyoKhjsihKiILqxDJ6wJZzr+C2tmYaaBL9iHLp4/g9GFbpgNOqAf7xJyF3u5RVV0YWXIweJ0ihl4NkZFTXyZbd0bcfeDWXg5lsaoqWsBcTLbbuxlAfsx+A12x6iogaPY1gNR1vDiu7wcgftiVJTCPvgd2ztWwXB7hBC1wwyGBLKOFJ9jU8wW5XAk+3KRVzJjcAX6cbT1Ncvkg66+41Raz2HOYEvdVUEXNmMo2L0yDqtjJriVbd4T8rmrZrWdz9txZJqj8Rv24bIMh8KWVvu/D2aCfmzz/giO4xT7YUKZMerhh2z7KgVDQwjhvKLJEsfmj1FYAePAs9aRfafYCMsX0IUuMawt/B3H5eP9aCwKKVicawG7EDPJP9l2u5COoY5N9XM2ZpOyONiaKQu8iLHVysZjFcwkN7PtDiEex7V2HNWyNAvNU+zDreEFQ7ECt2ISEeJIdOEXh876w2L23VEKn2ffihLYnNwV/XjROv3nEi3QhdUc+6oQgsXeFC9m8b4dwj7UTPMY9qH4LzbBuNAHh1o4jKwfJcV/LBRmg0Hswy4hHEc/isdjkLvuZV9uSC0YiqU4CDtgbb6cauAax5FW/8A3LUwKke1muz0omqMLs3Gn2JfjGCzCIGzAyTgUb8IeeB62NTvjdfg4fpTiAK7G2C2EQSiNbMqIyLF+Wk3zrRnZOMt5vsNweNJxBPQanICP4lXYCdual2N//CrFfmu3YUpYqHwf62AqLMDheBN2xbbmZXgfjrdjT4WL7XxFQQE+j7VTuHYdd7h2j+OXGJTqeGeaobA3xQNB/wxn4NN4LZ5vx9Ier8DH8NMUu1o8YjXojtPViI+xP45LZWJd/oaRUmhzSllQ7GgXszxmGiG6u4669PnifAR3QVfG4yCcYHORBZm+4SS8Bk9GV3rjMJ9aw2Mca/G+jElt4Recu+9z7IfKnu79qwhh6XMcHuIzOno0voAT7XvfpW/oDXhZgJH8Pfm7u2x+uaCTxI9CthmIVTjIppn5wXEOxFOwpwX3IPTl76eznRFeuFyAp/pcu1dweMBr1xcvD3DtLuHv7khlOiu218ZCqStLcYC1FCx2eP+qVoF1XcBZIZ7jb6fZNv4mGIofsTcnabwXEjYL+TgPuQDX2YOrV6ZGcAphXxp9Aswr993fvE8TC5iunyWWi6SWMAWs2fRtD21FoeexMvpxKPtZj+3+UsIglAEOHfY7s90bMrDiSsc8rS28JcAo2imYFj6hcBz2tbneAq0/TnGlzXs3BvdGP8pb0Pk3BuF2PAld2WZB5F72c2XA59VbHnJch1AOxBYBm1y/ttr5kChx8MfbeG2K166XXbtXHQezlMML8fEUfgSNcQygG+14H2EfnVtNrKvFUDueLvgo1nKsCR3O353E9or/f1OyGI4HWSiMBN77d7zDvkAews0YNUI8irsjOE2JssXbOdejC1/gYRYKw/jcvEbRJsA0OseFMAhldzwxA53pGzo0wY/KsdrCo60ZzIUt+C5GwSa8lJBwNs5P4/780kbpLkAX2qcwPcstAQfqHMV+3YAr0ziu6RQt8Z9YjC5UsL6UBRFPs7UZL2Mfz0zz2n1NcVSAa3ceBmUwVndcbvdI9ulu3JDi8RTjSFsxZQK60GZnXXAKPXEbJ7M7rsvQUlBrsZ9dvEkoRFTho5etROLKa3/zPmUpOjk+lM60B1KYn5lpAR6OB4Y0p2HXGNQWjuHYV+dQKGxA8WqA585H1uwaNhvwNN772ZDuz18pzsWN6McxnIfKngMWsAYFaNn7Hg9nf74I6bi24n0WiDYHmMrmMoyKDXgq+zU0pGNcZiO8/0I/juKaVAs4efXpLtfNwvw3YVVCUZwRoLXhfps+ylAwvJOTeG+W1gqdY7USr2CYCAXCAhsB92TAfi1v4M440bHj/nURLiX1DC5BP+qGtBJKO85hhaiujw248eNZzAlszepJuAe6MhCjoDOB4EMvRKwf6L/Qj8IAzbPn4FEBBo2dwH4sjeBZ9TpFxwCrJ91lQSMKukZw7WY5Dp4pwBYB+knf7zg92KlhXzfrBnMpjkY/6mAvBAXDsZy8u7wsYhfvClyPQoQ10nVCCiPgniphSpTj0I/5+DpGgu3bGyGtqzvYsQnr3AinDqrrcD4n58JE1ni7zb23Z8BRtP/BsHmMIPCmFw39cRP60cyxtvAOdGENto1yfV/e+40A+1MLL4/i/LIfYyO8dhvDuHbGVVjPoS9ox6hWNLJ+g93xe/TjGpvuK6+D4R/YM42Hb2ms5IWANZW8j0Kkcz+eimP450w8CYPwq0+n6kPRj9G2VniUTEM/KiGEshJKN4yCLujHc/bFntgfKHgXLxciJd0RgtEvgsE/i/A2jAQLZu+hH/s59v06GH2xARizvei5L8CPletCXlN5ccTX7g/HfnmNHEeR90U/BrDdT70IsX7VXXGrQ0tL+3wflXyvtcMHWYqnCx6JjbGCBcRiqwqeh9PwA/zYLkYQVmBuIarYpLphUwV3w9rYEJvj0VgZU6WvTx/bJk6jR6Pnl7C+LLk2LziM1j6J/7cb//83LyTsodHeoSZhOIZJYUT3YzmsgbVwXzwIW9rrVPkAX4si2HAt12dgurMzfZcn86dHgFHbz2eohauY0HO5zVxQyuEYT8CJIT6z13rR8gme7dvs6s9ZDi0Cq/CuDF23b7luT/Oyt8M9Nzpfg+FKHBSgWW5ICXOfFeCeZmu8EVfzd6MpnyzggnhuNMLcQvQw485Igs/LXsnsin7MxKjZimEx2CEYFmFHfBLDorVDP7t3uCZLvHCpjD9h3PkTL4qgtnRlhgLULPRjF4cpptoHGDxZnMHuTz+wfyN4eRH6cXFIwXA1DseomZnutTMuc+y6s8rLHA/jFT7TRLXh2tZkv/7Ix6bkV1yGg3OCjrCpN45JoUbnMpzBe7yLh/hsp7nVRGYaIb7Anj73ZzmKUTjcx2UYNbXQmZAGoXTT3IUZYxO2i6jP1WibZD1qfkU/qvvVVDs2vU/hmCZ5med+dOE0qyFPlzH2zI6apehHTZ/cUNlxqquhGR7PsIjiLYcfwu3sRd7xjkMorGhVqpUxHU7xkPd7jvIfOzZfs52G1t+pADOJEDNsapn1Dn1UumMcOBXDwX0llBaE4/04D/PC6AtK0QFLYjmOy9NQ2IHz/LEXDeMxExSjH6X9AhW6MCRLgybnEoC4Tl4rLIlqeBROSfvaJYfTsIz/akZZqb0fjW0d+rYOycdg+Dn6cT7ujWFxMba3mcmn4zZshRdiOcwkQkzAjkmaI88mhO6G4eG+EkpXvBPT5SSHbb1oK1DkEyvwHJsUPSo+waTQxjFIv47ZYpQFQz+ODyEYfoxZxH1wm2Nt4XuYQdy3a83JBfnWlLzeJiP1v5nDpyr2xRdxJPbMcCgUYhPehKcnLBS2slqDoghG7L2QwcmuO6sZeacPqwMjDoU/26TrSbjXa1DshX5MtsEY2eJtdOFwTIdFNmI4LhT5TkXlzzTMODYDygKHPpT75VuN4XJ0YRfMJYR4BW+1JaRij011cRz28l2KKvpBKA3Ynxacu8/Tmc/PYcTj52xjppcfzMNb8dUMDJ74EZPCYejCR1meg3cRIXY+LxtgSRySL9fOprA7AP2YmeXPXX0siWZFnsjl6WOE7uMXcLANtohzENyDYn88HI/ElpH/QLNBKGx7ikNfw274OabK6VjFd6WT3GYbfoCD8E1blzsT/IpJoTG68BVmm68cgmEdAlPFNJacXYxJoYHj3NAnck6O9bJDFfTjoHwLhq7H+y2ej0lCiFX4tQWYCTjFHr6xwJaYa4xNsIHZGBtaaHImC4NQOrH/fdPo/9cJS2I9voy5xFacZQHiQ3zHBuBlmtWYFOqgC99htpmJLtTF2SmvjZwc9nZe3jPG5GNT8q4k9QKHpovn8RashKkgxPv4EkbJelyHS60vzrKY9ZU6HI/A5ngg7h27Efjug1B2xZNwfIpNTGc5TKO1xouODXhVBoLgGvwDF9s9uSkWfWuTQz30Y0tMakF/cT2mNILhGkwK+2AuUC/fgmEZrIP/82leWkqA7Gyz75fBoAgxi6A2zMsTrDawtYWnk3F/jDfBV0LplkowhLOxXJYHnWxKxP0oaqAfy2z5yWyzBF2ojvlAVcwFahd6+UcLx4fFWzYH0xcohNjJABE8DV+xWqK38VoLhUljMPpxjtX+hT0aeR5OQSHKOnUZiQer0YXymA9UwlygWj4Gw9MD1CR8bR3h2+FELPaEUCAshd15OQfHYwcsl4F1TO9FHyJdCaUCtk2hSf1kv9pC694iRBX0YzPGgY2ugUljGBJF1XwMhh3sV7/zwuE4Fk+yYd434ie4DfMNoVDYjGIqPo8NotwUzsCH8SCCU6sMjMQcHMESeW2xjE+/vOH2WohtOqbEskUJN7lUwhtSWcnAlrF51EOrCTgBW5sHYC4jFAovtPBUNuQv04U4fztn4HSblDhWg1Bsqola7NtSzwGH2Q3e2W7SfSHWOjU3x4PyzoPk8oOVCobJph/BbjRB74c0mp7+pHjV9Hi/WhQnb+eumCsIhcIrKZ7GVPkdp+McnIdzrVxoU+okZRBKoYW9/g7nbDeKEwIMOhFiQ4IGOVRGF9YhGAqGwxUM40lZHEuYOyasebWsBuEFD3nfAhvk0gG7YC1MKkKh8IwUQuFmHI9v44eErgVe3HFfCaWrSzCEDj4T3v6Gb6EhhNPzaHc+k0Ux+EFVF134E/OBJY5ZobsXcwoxX2mEEwlxtSOoeSjGqXi9fXja4mRMFEJYl4nhAZtT/ol1CIPn4pDEhEL3QSjNOS+NQmhGfnGHCbOFWOT43N4zCXMuGr9gPvCz43dqGQXDeNMMv+RCnRjhg2YLvoHH2dqvn2NSEOJ2rIkuvIUNCTv34W+ekaNT11zgE6jrUrQMuASeEL84P7uyjfs+/A/zgZ/QhV3iHwzFHvg+4fBFrBdxbcRkmxuxVwI65ArVFlahuBRdGIJnEwiXe8lnjMN66V2sy8jf0dFnlZep1sdZiFSWmTscs81h6McC7vO/vDyA41zh2JxcLznBUHTD+YTDQXhAhOGwGJ/m5ZExr2IXoj1WRD++xF65MhefPchecFj+6qg0JrUeijsixHTH+XJPxKxhXSnqOB1PfvEV+tE0WcFQlMae+D3hcCr2xdoRBcTvrKnpZ4wjQpyCLtyQob5yFRCMmM5pyEOzvk9tyjp8xdsBIWy97LnoxxGEs2w2SZ6BLkzD/MD9eI9J7qhk0cJ8lHD4GeW7+D5+SajbGlI4XMR7n2k3U3mME0IcgX7MIxR+7GWGPTM5CIWAN8XnS7wT/6ePheIgtYWvWADYGUK8h40cKnW64L8xG1zofCz5xUS8G0uiDZmiMBvrXbPdPa0CrCQW+dcYigJsiffgVPyDk/sGXoNNsSDNcDjTRnHGCaH+hWUp9kI/PsZMcVTMBqHUwNN2FhjTmLtQiHfQhZ72/Ml0uDjcceDJMpyB+cQXDvMZ1sFjs/SdPg9/KsG5WDp4MBRV8RwcgN/hb5zwUXg+VvFS48mYjdwSopZjV5O5GQqqlSlOjuEglK477GdTnz5Ecwv6lDgdjhAf4Sr04wA8JyszFbgxNt/WALfWxPHox9WYaY53aJn8gmPYkH4wFLvg+TgKlxMOX8bjAt5MmygGYlwQoiJCbBb0vxQrxnAQytn2g1C1hSLM++4ldOF+qwnKCGyrNcWZXkloOqYX0I/2nMsDvczSA/2YEP7gE1EGO+HHXPRJ2DRQ7UR8EKI0ulAuAw+jXbPY3WKww/G3c+xfuBWHIwgRyqj1JnhzhkJheYpB6MJ3BNwvvfxkIi5x6KL2ZKa6ArCdfSjaox+vRxsMRSv8mgvSxbF2Yj7FrxgHhFiFLjSNOBQWWi1bjRivhNLN9vUwivr4d4y3pTOF8LvvvrbaGxduI2C08aJnIDZEF+7HvMSak59AP47DmzATPIqlsCS+sTEPEQdDURqHEw4P9dyYg4lAaM1W43SrSYiKh/HMmK+EcoJNa9UlxKY1Ie5EFwpxdJRNk7z3LRQXoQs/aDom7ylc5dgV4JyIawsvpGjntM+QmWAoivC6hC0dJFRjsZZiMfpRHXtH8CAqg8/w8noMQvksDEIpwK7YwWeE5nh0RegzOJXiNXShJn5ICDgi5FBRgHfx8j4E53lNt2k+Su9Bx1A/hnPcOaJQeILjnKxLcGRmg6FojS78hXFBiC/QhXusU3pYobAJxSd4OQalcpYGodyGdfHveDGFScCFuAbXBBgI+QlhIJRpbHiPmhSv4u3oypiCPvYDSDyGcxxbFl/ifP8Ly4cYCrva+vVl0Y+7uW4bMhsMxW7Og1figxATAty34wl0l2JBGoFwb3zK5j5rgalQj/coykJzcuUImpGFag2XBKyRL4uDrPbw8DRq66/g5Wxsh64sRdtXYbONXIHb0IW++J1NfVeYRiDcC1/m5QjHFpQvcWi+L4k3AS/E4/E8O3nFGCUrnJf8ig9CjMYNAZpwh+BXBLNLcNcAX2KX4Xj++SNehaXTnGbngkgHoQTnM/5+tpcaQgFjuIW9ILTGLwgIE/ECrOkQKA7AO3k5HwfiLujKFjyPfV3mbY+u3STrK+pKfRyFP3Et7sMWWOQyYhxPxZdsEutOAVope9iAmfxcEs8W+n/aM4zXOJnP2rxRe2AUfOt8U8QHoQfSSgLbc7zsha4cgkNxG387m3IWLsbVuA2rmXtjE9wNXfnaQqNfJ/vBbPsUyrlYA4/CARzPCyHUGh6DPoReWyhEH2yIx2MQ2pjFhIZZNjBkMa7EMlgNG+BBuDumypWEi0+9nSHuwyMCDqKrh7eY67h231tg/x1XYymsbtdvP2yGpVO8bt/n81rJIywU7jTVc+IPtlE552VjTiq2X9oelHFCiLuwC1bHIBTi/mYYfI+n4jCHYFiEnTDs1pExOCDguVgbwghNoZqnjYSDtrx8G1um8hZ4gBk217J/f/+M07XbxrXrZP39jk+xFaSFGSYPsG/DPCNfm5Kf8bl4v2EHXp4d8rQxrzlOXn0Elsc4IfRAWk5xMWaTT7GV7cvP6ESkg1DcecVGeKeLUMBYRXFyjBZD+Au7sV8DPD907dZbjeEEzDoWCm/xIN+D4U+OF3AcRVPsjt9iOgzDLo7rRbbHOCIUDt+waWOywSBswz786QFMifWchtEvgScUMDrhbbgVs8X/7MfaSC8oCoePYbbYgldZKAQFw9oBLuAWHI7NrH/SYzgvwIkfbx+aHrjJoRm5kk0eGleEwuFjFD1xcwYfPGew3StwowfGOFyFTmR5EMqcCPpdCQWMYrzXmhZnYCYpxqfxAPbBprMSAbPF9bw8AX/CTDIbW7D9gZSGgmGXVCcZxeuxoXXOPQV74X04AAfiI9gPT8Na/M0Z+InnztVYA+OMUDgcYl0evol4Kb57sOnO5kOzZtnHElJr+CxGhVDI+IqiOV6Jv2DUvImHsd1euMZLB127j6xl8mb8A6NkJd6IzWypRV+KMF/oTc3c25yYD9K4mL9RvGeGAvtU35oFkoBQOPzG1gTujDfgwRgG83EYPsU2VjqM8muOZ2E2GIP/xmo+LQcvYqQI1UBRPMNncihlR+yBbbAgxFDxMg5iW+H/IFTT8oNcuycpL8WL8OCQawgH45C/7+esYFhkk/D+w6auKPayDPtSgWIUUiYE8SS+gX4syuVRdhQjPbR1wNvhiXhogCkTNuJUnIJv857/DbJIvY3QvBr7YS2fdVsH42gMi0oOn9nx7OdSL3rmO4503IK5yDCc5PpZjMn1+CuigPiSraBRx0bwt8bjsG7APmgz7Jx+hBOtK4euXUSfIwtt/T3k2u1PeToei8cEbElci1/hB/ad+nXK+8SOFHv5xyTsyYmbl8VQWMZGLJ+J+cZdnPs7vZxC2D3dEPfFPbAKVrRQshZX4xKchwvtYZYWNvlrczwYa2AxrsOfcQbbWBjBcV5P8SiWxDls+z9e9hH6XFa2z2WD7T6TVXETrsdVuAznon0u44CwhQIa4D527cpj1e2+U1fiEgu1P6VR4aVgaGzCRzy0aQAyebFrWHPUCZgQFAyFsCX/ZlsA/juWYZ1kPWCFEEJrJZfBW3EBQa0fVstQKDyR4ptEhkIhRCsLhSUxPKmhUAghijxREx/AWwltwyif40t9egSB8ACKe/FcTCZCiJ75MXehEELBUFTCqz0kxP1gAwzG4xc2F2GqTcbn4oXYCpOLEGpG3sVhIvopfF/M8XIKIYSCoWhi3owbCHjfUKLX15bFKikM9rSO8EfjgTttrhfbMGkIcRGWyeXaQiGEKMT1XkmI8ngUXonl0I+H7f82+9tQKFZjohAadOLQjLwGX8FEI4RQMFyL6SGEgqHQoJMXaFFY5yUcIYSC4RrMJEKsxSQhxD/Qj6cx8QghFAx/xEwixAJMCkLNyOfZShIlMYnawlleDiCEUDCcjW6IAkwfMReTgFAobEQxGP14HBOPEEIUBnpIiz18HiJlKSp5JSF+LeiT3e4LQkv3YSmH/3c4xYdYHUtiGo7DxCOEEEU4Hd0Q52BJzUWnYckPHPE1ZhMh9scPCX7jKN+0uQd/2+7HXXPsgd0dP8+32xqlOYEQQsHwc1yHFT0/xE08OMbwEJjv7YAtqfew54eYhNlGiOp4oUkg9NZTbMDqWIhOWKh818sRhBCi0Nb0nIL+iGo4hRDYFgu3C4XHUnyK+3l+iA8wbghRAWsGDIVbsBfmHEIIrXwyAU9Bf8Tu+Dr+SSBcaP+ujf6I5TgDcwEh7qS28FsvhxBCiEIrRwdepkzUwEMChUIxqqBPTtxnQryND2DuIYRQMORhvYRiIkaJEC9i0hFiEnbM2R85QggFQ2MYRoUQs3iQfuklGyFG4umEwvVeriKEUB9D41V8COti2AjxCCYVIebgzQTCsV4+IIRQjSFfeJsp7sewEeIXHIFxQIjZ2A1fwO9wPe7IGpyGT2AbbJJ3oVAIoSXeGGVb3tZOroVhIcSVPFSf8eKL0GooFSlKI3ib1FQMQggFQwuHPSiewzAQYjYeaPNlxhghhBBCFOKODMepGAZC9E5oKBRCCCEUDG0KhktxI6aDEM8TCid6yUQIIYRQMLRwOJOiH6aKED9iH0wyQgghhIKhMQDfwKAIsdEmAF7jJR8hhBBCwZCHejHFRTgTgyDEZYTCr7zcQAghhFAwtHC4muJ0XIwuCHEHoTDXlr4TQgghFAwtHP5C0QZ/x5IQ4iFC4d1ebiKEEEIoGFo4nENxjE/NoVAoTPyAJSGEEELB0D0cHo3foSGEtw2vybFQKIQQQigYOjYrt8TXUYgVeDqh8AkvjxBCCCEUDA2bguQ8vCavJ8EWn+DBhMIJXk4ghBBCiII0F55vTDEEj8H8QKzDW/FJQuFWTwghhBAKhtuFwwKKC/BerIu5iSjGEXgzgVCDkIQQQggFwxIDYhmKHngj1sfcQGzFETbq+AdPCCGEEAqGAQJiEUU7vAhPxiJMHmIhjsTBBEJeCyGEEELBML2QuDtFRzwFj8UqGF/ETJyIY/ETWxZRCCGEEAqGoYfEUhTN8VBshA2xgYXFSlgRo0VsxbW4En/D2TjXAuEUguByL28RQgghREFxcbEnhBBCCCHE/wHbvTfZ/DhibwAAAABJRU5ErkJggg==";
            byte[] imgPayphoneBytes = Convert.FromBase64String(imgPayphone);
            Image image = Image.GetInstance(imgPayphoneBytes);
            image.ScaleToFit(200f, 49f);
            image.Alignment = Element.ALIGN_CENTER;

            PdfPTable table6 = new PdfPTable(2) { WidthPercentage = 100 };
            float[] widths6 = new float[] { 50, 50 };
            table6.SetWidths(widths6);
            PdfPCell cell10 = new PdfPCell();

            cell10 = new PdfPCell(nombreTexto);
            cell10.HorizontalAlignment = Element.ALIGN_LEFT;
            cell10.BorderWidth = 0;
            table6.AddCell(cell10);

            cell10 = new PdfPCell(nombreValor);
            cell10.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell10.BorderWidth = 0;
            table6.AddCell(cell10);

            doc.Add(image);
            doc.Add(titulo1);
            doc.Add(direccion);
            doc.Add(direccion2);
            doc.Add(marcaTarjeta);
            doc.Add(table);
            if (!pDiferidos.Equals("Corriente"))
            {
                doc.Add(table2);
            }
            doc.Add(table3);
            doc.Add(autorizacion);
            doc.Add(table4);
            doc.Add(separador);
            doc.Add(table5);
            doc.Add(separador);
            doc.Add(table6);
            doc.Add(copia);

            doc.Close();

            var pdf = mem.ToArray();

            return Convert.ToBase64String(pdf);
        }

        public static string generarReciboDatafast(string pMarcaTarjeta, string pBinTarjeta,
            string pDigitosTarjeta, string pExpiracion, string pLote, string pRef, string pFecha, string pHora,
            int pIntereses, string pMoneda, string pDiferidos, string pAprobacion,
            string pSubBase12, string pSubBase0, string pSubBase, string pIva, string pTotal,
            string pCliente)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "DATAFAST");

            var pgSize = new Rectangle(297, 499);
            var doc = new Document(pgSize, 10, 10, 20, 10);
            var mem = new MemoryStream();

            PdfWriter wri = PdfWriter.GetInstance(doc, mem);
            doc.Open();

            Paragraph separador = new Paragraph();
            separador.Add("");
            separador.SpacingBefore = 12f;

            Paragraph titulo1 = new Paragraph();
            titulo1.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 15.5f, BaseColor.BLACK);
            titulo1.Alignment = Element.ALIGN_CENTER;
            titulo1.Add("SEGUROS EQUINOCCIAL");
            //titulo1.SpacingAfter = 12f;
            //titulo1.SpacingBefore = 20f;

            Paragraph direccion = new Paragraph();
            direccion.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            direccion.Alignment = Element.ALIGN_CENTER;
            direccion.Add("ELOY ALFARO N 33 400 Y AYARZA");

            Paragraph direccion2 = new Paragraph();
            direccion2.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            direccion2.Alignment = Element.ALIGN_CENTER;
            direccion2.Add("2447574");
            //direccion2.SpacingAfter = 12f;

            Paragraph crede = new Paragraph();
            crede.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            crede.Alignment = Element.ALIGN_CENTER;
            crede.Add(credenciales.MID + "-" + credenciales.TIP);

            Paragraph marcaTarjeta = new Paragraph();
            marcaTarjeta.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 15.5f, BaseColor.BLACK);
            marcaTarjeta.Alignment = Element.ALIGN_CENTER;
            marcaTarjeta.Add(pMarcaTarjeta);
            //marcaTarjeta.SpacingAfter = 8f;

            Paragraph tarjetaTexto = new Paragraph();
            tarjetaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tarjetaTexto.Add("TARJETA:");

            Paragraph tarjetaValor = new Paragraph();
            tarjetaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tarjetaValor.Add(pBinTarjeta + "XXXXXX" + pDigitosTarjeta);

            Paragraph vencimientoTexto = new Paragraph();
            vencimientoTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            vencimientoTexto.Add("V:");

            Paragraph vencimientoValor = new Paragraph();
            vencimientoValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            vencimientoValor.Add(pExpiracion);

            Paragraph loteTexto = new Paragraph();
            loteTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            loteTexto.Add("LOTE:");

            Paragraph loteValor = new Paragraph();
            loteValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            loteValor.Add(pLote);

            Paragraph referenciaTexto = new Paragraph();
            referenciaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            referenciaTexto.Add("REF:");

            Paragraph referenciaValor = new Paragraph();
            referenciaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            referenciaValor.Add(pRef);

            Paragraph adquirienteTexto = new Paragraph();
            adquirienteTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            adquirienteTexto.Add("ADQUIRIENTE:");

            Paragraph adquirienteValor = new Paragraph();
            adquirienteValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            adquirienteValor.Add(pMarcaTarjeta);

            Paragraph fechaTexto = new Paragraph();
            fechaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            fechaTexto.Add("FECHA:");

            Paragraph fechaValor = new Paragraph();
            fechaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            fechaValor.Add(pFecha);

            Paragraph horaTexto = new Paragraph();
            horaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            horaTexto.Add("HORA:");

            Paragraph horaValor = new Paragraph();
            horaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            horaValor.Add(pHora);

            Phrase interesesDiferido = new Phrase();
            string intereses_ = pIntereses == 1 ? "CON INTERESES" : "SIN INTERESES";
            interesesDiferido.Add(new Chunk(intereses_, new Font(Font.FontFamily.COURIER, 13.5f, Font.BOLD)));
            interesesDiferido.Add(new Chunk("  MESES: " + pDiferidos, new Font(Font.FontFamily.COURIER, 10.5f)));

            Paragraph corriente = new Paragraph();
            corriente.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 13.5f, BaseColor.BLACK);
            corriente.Alignment = Element.ALIGN_CENTER;
            corriente.Add("CORRIENTE");

            Paragraph aprobacion = new Paragraph();
            aprobacion.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            aprobacion.Alignment = Element.ALIGN_CENTER;
            aprobacion.Add("APROBACIÓN #" + pAprobacion);
            aprobacion.SpacingAfter = 10f;

            Paragraph tipoModena = new Paragraph();
            tipoModena.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            tipoModena.Add(pMoneda);

            Paragraph subtotal12Texto = new Paragraph();
            subtotal12Texto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal12Texto.Add("BASE CONSUMO TARIFA 12:");

            Paragraph subtotal12Valor = new Paragraph();
            subtotal12Valor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal12Valor.Add("$ " + pSubBase12);

            Paragraph subtotal0Texto = new Paragraph();
            subtotal0Texto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal0Texto.Add("BASE CONSUMO TARIFA 0:");

            Paragraph subtotal0Valor = new Paragraph();
            subtotal0Valor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotal0Valor.Add("$ " + pSubBase0);

            Paragraph subtotalTexto = new Paragraph();
            subtotalTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotalTexto.Add("SUBTOTAL CONSUMOS:");

            Paragraph subtotalValor = new Paragraph();
            subtotalValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            subtotalValor.Add("$ " + pSubBase);

            Paragraph ivaTexto = new Paragraph();
            ivaTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            ivaTexto.Add("IVA:");

            Paragraph ivaValor = new Paragraph();
            ivaValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            ivaValor.Add("$ " + pIva);

            Paragraph totalTexto = new Paragraph();
            totalTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            totalTexto.Add("VR. TOTAL:");

            Paragraph totalValor = new Paragraph();
            totalValor.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 11f, BaseColor.BLACK);
            totalValor.Add("$ " + pTotal);

            Paragraph descripcion = new Paragraph();
            descripcion.Font = FontFactory.GetFont(FontFactory.COURIER, 7.7f, BaseColor.BLACK);
            descripcion.Alignment = Element.ALIGN_JUSTIFIED;
            descripcion.Add("DEBO Y PAGARÉ AL EMISOR INCONDICIONALMENTE, Y SIN PROTESTO EL TOTAL DE ESTE PAGARÉ MAS LOS INTERESES Y CARGOS POR SERVICIO. EN CASO DE MORA PAGARÉ LA TASA MÁXIMA AUTORIZADA PARA EL EMISOR. DECLARO QUE EL PRODUCTO DE ESTA TRANSACCIÓN NO SERÁ UTILIZADO EN ACTIVIDADES DE LAVADO DE DINERO Y ACTIVO(LEY 108).");
            descripcion.SpacingBefore = 11f;

            Paragraph nombreTexto = new Paragraph();
            nombreTexto.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            nombreTexto.Add("NOMBRE:");

            Paragraph nombreValor = new Paragraph();
            nombreValor.Font = FontFactory.GetFont(FontFactory.COURIER, 10.5f, BaseColor.BLACK);
            nombreValor.Add(pCliente);

            Paragraph copia = new Paragraph();
            copia.Font = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10.5f, BaseColor.BLACK);
            copia.Alignment = Element.ALIGN_CENTER;
            copia.Add("-COPIA2-");
            copia.SpacingBefore = 18f;
            copia.SpacingAfter = 13f;

            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            float[] widths = new float[] { 20, 40, 15, 25 };
            table.SetWidths(widths);
            PdfPCell cell = new PdfPCell();

            cell = new PdfPCell(tarjetaTexto);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(tarjetaValor);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(vencimientoTexto);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            cell = new PdfPCell(vencimientoValor);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            PdfPTable table2 = new PdfPTable(4) { WidthPercentage = 100 };
            float[] widths2 = new float[] { 15, 35, 15, 35 };
            table2.SetWidths(widths2);
            PdfPCell cell2 = new PdfPCell();

            cell2 = new PdfPCell(loteTexto);
            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            cell2 = new PdfPCell(loteValor);
            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            cell2 = new PdfPCell(referenciaTexto);
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            cell2 = new PdfPCell(referenciaValor);
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell2.BorderWidth = 0;
            table2.AddCell(cell2);

            PdfPTable table10 = new PdfPTable(2) { WidthPercentage = 100 };
            float[] widths10 = new float[] { 40, 60 };
            table10.SetWidths(widths10);
            PdfPCell cell15 = new PdfPCell();

            cell15 = new PdfPCell(adquirienteTexto);
            cell15.HorizontalAlignment = Element.ALIGN_LEFT;
            cell15.BorderWidth = 0;
            table10.AddCell(cell15);

            cell15 = new PdfPCell(adquirienteValor);
            cell15.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell15.BorderWidth = 0;
            table10.AddCell(cell15);

            PdfPTable table11 = new PdfPTable(1) { WidthPercentage = 100 };
            PdfPCell cell16 = new PdfPCell();

            cell16 = new PdfPCell(pDiferidos.Equals("0") ? corriente : interesesDiferido);
            cell16.HorizontalAlignment = Element.ALIGN_CENTER;
            cell16.BorderWidth = 0;
            table11.AddCell(cell16);

            PdfPTable table3 = new PdfPTable(4) { WidthPercentage = 100 };
            float[] widths3 = new float[] { 17, 50, 14, 20 };
            table3.SetWidths(widths3);
            PdfPCell cell3 = new PdfPCell();

            cell3 = new PdfPCell(fechaTexto);
            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(fechaValor);
            cell3.HorizontalAlignment = Element.ALIGN_LEFT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(horaTexto);
            cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            cell3 = new PdfPCell(horaValor);
            cell3.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell3.BorderWidth = 0;
            table3.AddCell(cell3);

            PdfPTable table4 = new PdfPTable(3) { WidthPercentage = 100 };
            float[] widths4 = new float[] { 60, 10, 30 };
            table4.SetWidths(widths4);
            PdfPCell cell4 = new PdfPCell();
            PdfPCell cell5 = new PdfPCell();
            PdfPCell cell6 = new PdfPCell();
            PdfPCell cell7 = new PdfPCell();
            PdfPCell cell8 = new PdfPCell();

            cell4 = new PdfPCell(subtotal12Texto);
            cell4.HorizontalAlignment = Element.ALIGN_LEFT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell4 = new PdfPCell(tipoModena);
            cell4.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell4 = new PdfPCell(subtotal12Valor);
            cell4.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell4.BorderWidth = 0;
            table4.AddCell(cell4);

            cell5 = new PdfPCell(subtotal0Texto);
            cell5.HorizontalAlignment = Element.ALIGN_LEFT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell5 = new PdfPCell(tipoModena);
            cell5.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell5 = new PdfPCell(subtotal0Valor);
            cell5.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell5.BorderWidth = 0;
            table4.AddCell(cell5);

            cell6 = new PdfPCell(subtotalTexto);
            cell6.HorizontalAlignment = Element.ALIGN_LEFT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell6 = new PdfPCell(tipoModena);
            cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell6 = new PdfPCell(subtotalValor);
            cell6.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell6.BorderWidth = 0;
            table4.AddCell(cell6);

            cell7 = new PdfPCell(ivaTexto);
            cell7.HorizontalAlignment = Element.ALIGN_LEFT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell7 = new PdfPCell(tipoModena);
            cell7.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell7 = new PdfPCell(ivaValor);
            cell7.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell7.BorderWidth = 0;
            table4.AddCell(cell7);

            cell8 = new PdfPCell(totalTexto);
            cell8.HorizontalAlignment = Element.ALIGN_LEFT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            cell8 = new PdfPCell(tipoModena);
            cell8.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            cell8 = new PdfPCell(totalValor);
            cell8.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell8.BorderWidth = 0;
            table4.AddCell(cell8);

            PdfPTable table5 = new PdfPTable(1) { WidthPercentage = 100 };
            PdfPCell cell9 = new PdfPCell();
            cell9 = new PdfPCell(descripcion);
            cell9.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell9.BorderWidth = 0;
            table5.AddCell(cell9);

            PdfPTable table6 = new PdfPTable(2) { WidthPercentage = 100 };
            float[] widths6 = new float[] { 50, 50 };
            table6.SetWidths(widths6);
            PdfPCell cell10 = new PdfPCell();

            cell10 = new PdfPCell(nombreTexto);
            cell10.HorizontalAlignment = Element.ALIGN_LEFT;
            cell10.BorderWidth = 0;
            table6.AddCell(cell10);

            cell10 = new PdfPCell(nombreValor);
            cell10.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell10.BorderWidth = 0;
            table6.AddCell(cell10);

            doc.Add(titulo1);
            doc.Add(direccion);
            doc.Add(direccion2);
            doc.Add(crede);
            doc.Add(marcaTarjeta);
            doc.Add(table);
            doc.Add(table2);
            doc.Add(table10);
            doc.Add(table3);
            doc.Add(table11);
            doc.Add(aprobacion);
            doc.Add(table4);
            doc.Add(separador);
            doc.Add(table5);
            doc.Add(separador);
            doc.Add(table6);
            doc.Add(copia);

            doc.Close();

            var pdf = mem.ToArray();

            return Convert.ToBase64String(pdf);
        }


        public static string recorrerJSON(EAdmPago pago)
        {

            var marca = "";
            var bin = "";
            var digitos = "";
            var transaccion = "";
            var diferidos = "";
            var moneda = "";

            dynamic valores = JObject.Parse(pago.ResultadoTrama);
            marca = valores.cardBrand;
            bin = valores.bin;
            digitos = valores.lastDigits;
            transaccion = valores.transactionId;
            diferidos = valores.deferred == true ? valores.deferredMessage : "Corriente";
            moneda = valores.currency;


            return marca + bin + digitos + transaccion + diferidos + moneda;
        }
    }
}
