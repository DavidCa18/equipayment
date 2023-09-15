using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmFactura : DGesConexion
    {
        public static EAdmFactura AdmGestionFactura(EAdmFactura pFactura)
        {

            EAdmFactura factura = new EAdmFactura();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionFactura", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idFactura", SqlDbType.Int);
                cmd.Parameters.Add("@numero", SqlDbType.NVarChar);
                cmd.Parameters.Add("@comercio", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal12", SqlDbType.NVarChar);
                cmd.Parameters.Add("@subtotal0", SqlDbType.NVarChar);
                cmd.Parameters.Add("@iva", SqlDbType.NVarChar);
                cmd.Parameters.Add("@total", SqlDbType.NVarChar);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                cmd.Parameters.Add("@urlRetorno", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@aplicacion", SqlDbType.NVarChar, -1);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pFactura.Identificador;
                cmd.Parameters["@idFactura"].Value = pFactura.IdFactura;
                cmd.Parameters["@numero"].Value = pFactura.Numero;
                cmd.Parameters["@comercio"].Value = pFactura.Comercio;
                cmd.Parameters["@subtotal12"].Value = pFactura.Subtotal12;
                cmd.Parameters["@subtotal0"].Value = pFactura.Subtotal0;
                cmd.Parameters["@iva"].Value = pFactura.Iva;
                cmd.Parameters["@total"].Value = pFactura.Total;
                cmd.Parameters["@estado"].Value = pFactura.Estado;
                cmd.Parameters["@idCliente"].Value = pFactura.Cliente.IdCliente;
                cmd.Parameters["@urlRetorno"].Value = pFactura.UrlRetorno;
                cmd.Parameters["@aplicacion"].Value = pFactura.Aplicacion;

                cmd.ExecuteNonQuery();

                factura.IdFactura = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return factura;

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

        public static ECliPagoResultado CliGestionClienteFactura(EAdmPago pPago)
        {

            ECliPagoResultado pagoResultado = new ECliPagoResultado();
            EAdmAplicacion validacion = DAdmAplicacion.AdmConsultarDatosAplicacion(pPago.Factura.Cliente.Aplicacion.IdAplicacion);
            EAdmGestion gestion = DAdmGestion.AdmConsultarGestion(pPago.Factura.Cliente.Identificacion);

            //VALIDACION 1
            var totalSuma = Convert.ToDouble(pPago.Factura.Subtotal12) + Convert.ToDouble(pPago.Factura.Subtotal0) + Convert.ToDouble(pPago.Factura.Iva);
            var total = Convert.ToDouble(pPago.Factura.Total);
            decimal valorTotal = Convert.ToDecimal(totalSuma);
            string sumatoria = valorTotal.ToString("N2").Replace(@",", "");

            /*
             Identificacion -> 1 = CEDULA
             Identificacion -> 2 = RUC
             Identificacion -> 3 = CEDULA, RUC Y PASAPORTE
             */

            if (validacion.Identificacion.Equals("1") && pPago.Factura.Cliente.Identificacion.Trim().Length != 10)
            {
                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "La plataforma equipayment permite solo procesar cédulas de identidad.", "identificacion");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "La plataforma equipayment permite solo procesar cédulas de identidad.";
            }
            else if (validacion.Identificacion.Equals("2") && pPago.Factura.Cliente.Identificacion.Trim().Length != 13)
            {
                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "La plataforma equipayment permite solo procesar ruc's.", "identificacion");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "La plataforma equipayment permite solo procesar ruc's.";
            }
            else if (validacion.Identificacion.Equals("3") &&
                !(pPago.Factura.Cliente.Identificacion.Trim().Length >= 5 && pPago.Factura.Cliente.Identificacion.Trim().Length <= 15))
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "El número de documento que ingreso no es correcto.", "identificacion");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El número de documento que ingreso no es correcto.";
            }
            else if (totalSuma > validacion.MontoMaximo)
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "La cantidad total ingresada supera el límite permitido.", "total");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "La cantidad total ingresada supera el límite permitido.";
            }
            else if (pPago.Factura.Cliente.Email.Equals(""))
            {
                // DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //     "Ingresar datos en el campo de correo electrónico.", "email");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "Ingresar datos en el campo de correo electrónico.";
            }
            else if (!verificarNumeroEmails(pPago.Factura.Cliente.Email))
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "Se permite máximo 2 correos electrónicos por pago. Si el mensaje persiste retire el ; del final.", "email");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "Se permite máximo 2 correos electrónicos por pago. Si el mensaje persiste retire el ; del final.";
            }
            else if (!verificarContenidoEmails(pPago.Factura.Cliente.Email))
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "El segundo correo eletrónico está vacio. Si desea enviar solo uno retire el ;.", "email");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El segundo correo eletrónico está vacio. Si desea enviar solo uno retire el ;.";
            }
            else if (!verificarEstructuraEmails(pPago.Factura.Cliente.Email))
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "La estructura de uno de los dos correos electrónicos no es correcta.", "email");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "La estructura de uno de los dos correos electrónicos no es correcta.";
            }
            else if (pPago.Factura.Cliente.Telefono.Length != 10)
            {
                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El número de teléfono ingresado no posee 10 caracteres.";
            }
            else if (!AdmVerificarClienteCodigo(pPago.Factura.Cliente.Identificacion, "800.110.100", gestion.Estado))
            {

                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "Cliente con transacción duplicada (pendiente de verificación), por favor contacte con el departamento de soporte técnico.", "pago");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "Cliente con transacción duplicada (pendiente de verificación), por favor contacte con el departamento de soporte técnico.";
            }
            else if (!AdmVerificarClienteCodigo(pPago.Factura.Cliente.Identificacion, "100.400.147", gestion.Estado))
            {
                //DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "El cliente se encuentra en listas negras en la plataforma de pagos, no podrá transaccionar por la plataforma durante un periodo de tiempo determinado.", "pago");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El cliente se encuentra en listas negras en la plataforma de pagos, no podrá transaccionar por la plataforma durante un periodo de tiempo determinado.";
            }
            else if (!AdmVerificarClienteErrorPago(pPago.Factura.Cliente.Identificacion, gestion.Estado))
            {

                // DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones(pPago.Factura.Cliente.Identificacion, pPago.Factura.Cliente.PrimerNombre, pPago.Factura.Cliente.SegundoNombre, pPago.Factura.Cliente.Apellido, pPago.Factura.Cliente.Email, pPago.Factura.Cliente.Telefono, "0000", "0000", pPago.Factura.Cliente.Aplicacion.IdAplicacion.ToString(), pPago.Factura.Cliente.Aplicacion.Identificacion, pPago.Factura.Numero, pPago.Factura.Comercio.ToUpper(), pPago.Factura.Subtotal12, pPago.Factura.Subtotal0, pPago.Factura.Iva, pPago.Factura.Total, pPago.Factura.UrlRetorno,
                //    "El cliente tiene varias transacciones fallidas, por favor contactese con el departamento de soporte técnico.", "pago");

                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El cliente tiene varias transacciones fallidas, por favor contactese con el departamento de soporte técnico.";
            }
            else if (Convert.ToDouble(pPago.Factura.Total) <= 0)
            {
                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El total no deberá ser menor o igual a 0.";
            }
            else if (Convert.ToDouble(pPago.Factura.Iva) > Convert.ToDouble(pPago.Factura.Total))
            {
                pagoResultado.IdPago = "0";
                pagoResultado.Url = "None";
                pagoResultado.Descripcion = "El iva no debe ser mayor al total.";
            }
            else
            {
                EAdmCredenciales credenciales = EGloGlobales.obtenerCredenciales();

                try
                {
                    Conectar();
                    SqlCommand cmd = new SqlCommand("GestionIngresoClienteFactura", getCnn());
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@primerNombre", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@segundoNombre", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@apellido", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@telefono", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@codigo", SqlDbType.Int);

                    cmd.Parameters.Add("@numero", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@comercio", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@subtotal12", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@subtotal0", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@iva", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@total", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@urlRetorno", SqlDbType.NVarChar, -1);
                    cmd.Parameters.Add("@ip", SqlDbType.NVarChar);

                    cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                    cmd.Parameters["@identificacion"].Value = pPago.Factura.Cliente.Identificacion;
                    cmd.Parameters["@primerNombre"].Value = RemoverCaracteres(pPago.Factura.Cliente.PrimerNombre.ToUpper(), "letras");
                    cmd.Parameters["@segundoNombre"].Value = RemoverCaracteres(pPago.Factura.Cliente.SegundoNombre.ToUpper(), "letras");
                    cmd.Parameters["@apellido"].Value = RemoverCaracteres(pPago.Factura.Cliente.Apellido.ToUpper(), "letras");
                    cmd.Parameters["@email"].Value = pPago.Factura.Cliente.Email;
                    cmd.Parameters["@telefono"].Value = "593" + obtenerTelefono(pPago.Factura.Cliente.Telefono);
                    cmd.Parameters["@codigo"].Value = pPago.Factura.Cliente.Aplicacion.IdAplicacion;

                    cmd.Parameters["@numero"].Value = "0";
                    cmd.Parameters["@comercio"].Value = pPago.Factura.Comercio.ToUpper();
                    cmd.Parameters["@subtotal12"].Value = pPago.Factura.Subtotal12;
                    cmd.Parameters["@subtotal0"].Value = pPago.Factura.Subtotal0;
                    cmd.Parameters["@iva"].Value = pPago.Factura.Iva;
                    cmd.Parameters["@total"].Value = pPago.Factura.Total;
                    cmd.Parameters["@urlRetorno"].Value = pPago.Factura.UrlRetorno;
                    cmd.Parameters["@ip"].Value = AdmGenerarIP();

                    cmd.ExecuteNonQuery();

                    string pago = DGesEncriptacion.CodificarBase64(cmd.Parameters["@valor"].Value.ToString());
                    string url = "";
                    string recurrencia = "";

                    if (String.IsNullOrEmpty(pPago.Recurrencia) == true)
                    {
                        recurrencia = "&r=0";
                    }
                    else
                    {
                        if (pPago.Recurrencia.Equals("1") && validacion.Recurrencia == 1)
                        {
                            recurrencia = "&r=1";
                        }
                        else
                        {
                            recurrencia = "&r=0";
                        }
                    }

                    if (pPago.Factura.Numero == "1")
                    {
                        url = credenciales.urlPlataforma + "?c=" + pago + "&p=" + pPago.Factura.Cliente.Aplicacion.IdAplicacion + "&d=corriente" + recurrencia;
                    }
                    else if (pPago.Factura.Numero == "2")
                    {
                        url = credenciales.urlPlataforma + "?c=" + pago + "&p=" + pPago.Factura.Cliente.Aplicacion.IdAplicacion + "&d=especial&o=" + pPago.Factura.Cliente.Aplicacion.Identificacion + recurrencia;
                    }
                    else
                    {

                        if (validacion.Gracia == 1)
                        {

                            url = credenciales.urlPlataforma + "?c=" + pago + "&p=" + pPago.Factura.Cliente.Aplicacion.IdAplicacion + "&d=gracia" + recurrencia;
                        }
                        else
                        {
                            url = credenciales.urlPlataforma + "?c=" + pago + "&p=" + pPago.Factura.Cliente.Aplicacion.IdAplicacion + recurrencia;
                        }

                    }

                    pagoResultado.IdPago = pago;
                    pagoResultado.Url = url;
                    pagoResultado.Descripcion = "Transacción realizada exitosamente.";

                }
                catch (SqlException e)
                {
                    Cerrar();

                    DAdmHistorialTransacciones.AdmRegistrarHistorialTransacciones("", "", "", "", "", "", "0000", "0000", "", "", "", "", "", "", "", "", "", e.ToString(), "generador de link de pagos");

                    pagoResultado.IdPago = "0";
                    pagoResultado.Url = "None";
                    pagoResultado.Descripcion = "Error al invocar al servicio: " + e.ToString();
                }
                finally
                {
                    Cerrar();
                }
            }
            return pagoResultado;
        }

        public static string AdmGenerarIP()
        {
            var random = new Random();
            string ip1 = "190." + "107." + "79." + random.Next(1, 255);
            string ip2 = "190." + "120." + "79." + random.Next(1, 255);
            string ip3 = "190." + "152." + "255." + random.Next(1, 255);
            string ip4 = "200." + "31." + "10." + random.Next(1, 255);
            string ip5 = "200." + "41." + "81." + random.Next(1, 255);
            string ip6 = "200." + "115." + "47." + random.Next(1, 255);
            string ip7 = "200." + "107." + "63." + random.Next(1, 255);

            string[] ips = { ip2, ip3, ip1, ip4, ip5, ip6, ip7 };
            string ip = ips[random.Next(0, 7)];
            return ip;
        }

        public static int AdmValizacionNumIPsBD(string ip)
        {
            int numero = 0;

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT COUNT(Pago_Payment.Ip) AS 'Ip' FROM Pago_Payment WHERE Pago_Payment.Ip = @ip", getCnn());
                cmd.Parameters.AddWithValue("@ip", ip);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    numero = Convert.ToInt32(rdr["ip"]);
                }
                rdr.Close();

            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                Cerrar();
            }

            return numero;
        }

        public static string verificarIPClienteDiario(string identificacion)
        {
            string numero = "";

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT DISTINCT "
                                + " Pago_Payment.Ip "
                                + " FROM "
                                + " Cliente_Payment "
                                + " INNER JOIN Factura_Payment ON Factura_Payment.IdCliente = Cliente_Payment.IdCliente "
                                + " INNER JOIN Pago_Payment ON Pago_Payment.IdFactura = Factura_Payment.IdFactura "
                                + " WHERE "
                                + " Cliente_Payment.Identificacion = '" + identificacion + "' "
                                + " AND "
                                + " ( "
                                + " CONVERT(DATE, Pago_Payment.FechaTransaccion, 23) "
                                + " BETWEEN "
                                + " CONVERT(DATE, DATEADD(HH, -5, GETDATE()), 23) "
                                + " AND "
                                + " CONVERT(DATE, DATEADD(HH, -5, GETDATE()), 23) )", getCnn());
                cmd.Parameters.AddWithValue("@ip", identificacion);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    numero = rdr["ip"].ToString();
                }
                rdr.Close();

            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                Cerrar();
            }

            return numero;
        }

        public static string AdmVerificarIPs(string identificacion, string url)
        {
            string ip = "";

            string ipExistente = verificarIPClienteDiario(identificacion);

            if (!ipExistente.Equals(""))
            {
                ip = ipExistente;
            }
            else
            {
                if (url.Equals("0"))
                {
                    string gestionIP = AdmGenerarIP();
                    int cantidadVentana = AdmValizacionNumIPsBD(gestionIP);

                    if (cantidadVentana > 0)
                    {
                        AdmVerificarIPs(identificacion, url);
                    }
                    else
                    {
                        ip = gestionIP;
                    }
                }
                else
                {
                    string gestionIP = AdmGenerarIP();
                    int cantidadVentana = AdmValizacionNumIPsBD(gestionIP);

                    if (cantidadVentana > 0)
                    {
                        AdmVerificarIPs(identificacion, url);
                    }
                    else
                    {
                        ip = gestionIP;
                    }
                }
            }
            return ip;
        }

        public static bool AdmVerificarClienteErrorPago(string identificacion, int habilitar)
        {

            bool estado = false;
            int numero = 0;
            string ambiente = EGloGlobales.ambiente;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT "
                    + "COUNT(Pago_Payment.IdPago) AS 'Numero' "
                    + "FROM "
                    + "Cliente_Payment "
                    + "INNER JOIN Factura_Payment ON Factura_Payment.IdCliente = Cliente_Payment.IdCliente "
                    + "INNER JOIN Pago_Payment ON Pago_Payment.IdFactura = Factura_Payment.IdFactura "
                    + "WHERE "
                    + "Pago_Payment.Estado = 3 "
                    + "AND "
                    + "Cliente_Payment.Identificacion = @identificacion AND "
                    + "CONVERT(DATE, Pago_Payment.FechaTransaccion, 23) BETWEEN CONVERT(DATE, Pago_Payment.FechaTransaccion, 23) AND CONVERT(DATE,DATEADD(DD,2, DATEADD(HH, -5, GETDATE())), 23)", getCnn());
                cmd.Parameters.AddWithValue("@identificacion", identificacion);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    numero = Convert.ToInt32(rdr["Numero"]);
                }
                rdr.Close();
                //if (habilitar == 1 || ambiente.Equals("PRUEBAS"))
                if (habilitar == 1)
                {
                    estado = true;
                }
                else
                {
                    if (numero < 2)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
                    }
                }

                return estado;
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

        public static bool AdmVerificarClienteCodigo(string identificacion, string codigo, int habilitar)
        {

            bool estado = false;
            int numero = 0;
            string ambiente = EGloGlobales.ambiente;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT "
                    + "COUNT(Pago_Payment.ResultadoCodigo) AS 'Numero' "
                    + "FROM "
                    + "Cliente_Payment "
                    + "INNER JOIN Factura_Payment ON Factura_Payment.IdCliente = Cliente_Payment.IdCliente "
                    + "INNER JOIN Pago_Payment ON Pago_Payment.IdFactura = Factura_Payment.IdFactura "
                    + "WHERE "
                    + "Pago_Payment.ResultadoCodigo = @codigo "
                    + "AND "
                    + "Cliente_Payment.Identificacion = @identificacion AND "
                    + "CONVERT(DATE, Pago_Payment.FechaTransaccion, 23) BETWEEN CONVERT(DATE, Pago_Payment.FechaTransaccion, 23) AND CONVERT(DATE,DATEADD(DD,2, DATEADD(HH, -5, GETDATE())), 23)", getCnn());
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@identificacion", identificacion);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    numero = Convert.ToInt32(rdr["Numero"]);
                }
                rdr.Close();
                //if (habilitar == 1 || ambiente.Equals("PRUEBAS"))
                if (habilitar == 1)
                {
                    estado = true;
                }
                else
                {
                    if (numero <= 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
                    }
                }
                return estado;
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

        public static EAdmFactura AdmConsultarClienteFactura(int idPago)
        {

            EAdmClientes rsCliente = new EAdmClientes();
            EAdmFactura rsFactura = new EAdmFactura();
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarClienteFacturaPago WHERE IdPago = @idPago", getCnn());
                cmd.Parameters.AddWithValue("@idPago", idPago);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

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
                    rsCliente.Ip = rdr["Ip"].ToString();

                    rsFactura.Cliente = rsCliente;

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

        public static List<EAdmFactura> AdmConsultarFacturas()
        {

            List<EAdmFactura> lstDatos = new List<EAdmFactura>();

            EAdmAplicacion rsAplicacion;
            EAdmClientes rsClientes;
            EAdmFactura rsFactura;

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacionClienteFactura ORDER BY IdFactura DESC", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();
                    rsClientes = new EAdmClientes();
                    rsFactura = new EAdmFactura();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();

                    rsClientes.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.NombreCompleto = rdr["NombreCompleto"].ToString();
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

                    rsFactura.Cliente = rsClientes;
                    rsFactura.Cliente.Aplicacion = rsAplicacion;

                    lstDatos.Add(rsFactura);
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

        //METODOS ADICIONALES

        public static bool verificarNumeroEmails(string email)
        {
            bool estado = false;
            string[] separacion = email.Split(';');
            int valor = separacion.Length;

            if (valor > 2)
            {
                estado = false;
            }
            else
            {
                estado = true;
            }

            return estado;
        }

        public static bool verificarContenidoEmails(string email)
        {
            bool estado = false;
            string[] separacion = email.Split(';');

            int cont = separacion.Length;

            if (cont == 1)
            {
                estado = true;
            }
            else
            {
                string valor = separacion[1];

                if (valor.Trim().Equals(""))
                {
                    estado = false;
                }
                else
                {
                    estado = true;
                }
            }


            return estado;
        }

        public static bool verificarEstructuraEmails(string email)
        {
            bool estado = false;
            string formatoEmail = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

            string[] separacion = email.Split(';');
            int contador = 0;
            foreach (var emails in separacion)
            {
                string dato = emails.Trim();
                if (!Regex.IsMatch(dato, formatoEmail))
                {
                    contador++;
                }
            }

            if (contador == 0)
            {
                estado = true;
            }
            else
            {
                estado = false;
            }

            return estado;
        }

        public static bool VerificaIdentificacion(string identificacion)
        {
            bool estado = false;
            char[] valced = new char[13];
            int provincia;
            if (identificacion.Length >= 10)
            {
                valced = identificacion.Trim().ToCharArray();
                provincia = int.Parse((valced[0].ToString() + valced[1].ToString()));
                if (provincia > 0 && provincia < 25)
                {
                    if (int.Parse(valced[2].ToString()) < 6)
                    {
                        estado = VerificarCedula(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 6)
                    {
                        estado = VerificarSectorPublico(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 9)
                    {

                        estado = VerificarPersonaJuridica(valced);
                    }
                }
            }
            return estado;
        }

        public static bool VerificarCedula(char[] validarCedula)
        {
            try
            {
                int aux = 0, par = 0, impar = 0, verifi;
                for (int i = 0; i < 9; i += 2)
                {
                    aux = 2 * int.Parse(validarCedula[i].ToString());
                    if (aux > 9)
                        aux -= 9;
                    par += aux;
                }
                for (int i = 1; i < 9; i += 2)
                {
                    impar += int.Parse(validarCedula[i].ToString());
                }

                aux = par + impar;
                if (aux % 10 != 0)
                {
                    verifi = 10 - (aux % 10);
                }
                else
                    verifi = 0;
                if (verifi == int.Parse(validarCedula[9].ToString()))
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool VerificarPersonaJuridica(char[] validarCedula)
        {
            try
            {
                int aux = 0, prod, veri;
                veri = int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
                if (veri > 0)
                {
                    int[] coeficiente = new int[9] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                    for (int i = 0; i < 9; i++)
                    {
                        prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                        aux += prod;
                    }
                    if (aux % 11 == 0)
                    {
                        veri = 0;
                    }
                    else if (aux % 11 == 1)
                    {
                        return false;
                    }
                    else
                    {
                        aux = aux % 11;
                        veri = 11 - aux;
                    }

                    if (veri == int.Parse(validarCedula[9].ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool VerificarSectorPublico(char[] validarCedula)
        {
            try
            {
                int aux = 0, prod, veri;
                veri = int.Parse(validarCedula[9].ToString()) + int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
                if (veri > 0)
                {
                    int[] coeficiente = new int[8] { 3, 2, 7, 6, 5, 4, 3, 2 };

                    for (int i = 0; i < 8; i++)
                    {
                        prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                        aux += prod;
                    }

                    if (aux % 11 == 0)
                    {
                        veri = 0;
                    }
                    else if (aux % 11 == 1)
                    {
                        return false;
                    }
                    else
                    {
                        aux = aux % 11;
                        veri = 11 - aux;
                    }

                    if (veri == int.Parse(validarCedula[8].ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool VerificarTelefono(string telefono)
        {
            bool estado = false;

            string numero = telefono;
            if (numero.Length == 10)
            {
                string inicio = numero.Substring(0, 2);
                if (inicio == "09")
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
            }
            else
            {
                estado = false;
            }

            return estado;
        }

        private static string RemoverCaracteres(string texto, string tipo)
        {
            string cadena = "";
            if (tipo.Equals("numero"))
            {
                cadena = "[^0-9]";
            }
            else if (tipo.Equals("letras"))
            {
                cadena = "[^a-zA-ZñÑáéíóúÁÉÍÓÚ\\s]";
            }
            else if (tipo.Equals("letras-numeros"))
            {
                cadena = "[^0-9A-Za-z ]";
            }

            string limpiar = Regex.Replace(texto, @"" + cadena + "", "", RegexOptions.None);
            return limpiar.Trim();
        }

        public static string obtenerTelefono(string id)
        {
            string codigo = id;
            int longitud = id.Length - 1;
            return codigo.Substring(1, longitud);
        }
    }
}
