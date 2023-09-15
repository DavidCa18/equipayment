using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmMasivos : DGesConexion
    {
        public static string AdmGestionMasivos(EAdmAuxiliares auxiliares)
        {

            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionMasivos", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@trama", SqlDbType.NVarChar, -1);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@trama"].Value = auxiliares.Masivos;

                cmd.ExecuteNonQuery();

                string resultado = cmd.Parameters["@valor"].Value.ToString();
                string json = "[" + resultado.Substring(0, (resultado.Length - 1)) + "]";

                string linksString = "{\"platform\":\"EQUIPAYMENT\",\"links\": " + json + " }";
                JObject linksJson = JObject.Parse(linksString);
                JArray links = (JArray)linksJson["links"];

                foreach (var item in links)
                {
                    if (!item["IdPago"].ToString().Equals("0"))
                    {
                        DGesEmail.enviarEmailMasivos(item["Cliente"].ToString(), item["Ramo"].ToString(), item["Poliza"].ToString(), item["Deuda"].ToString(), item["Url"].ToString(), item["Email"].ToString(), "EQUIPAYMENT | PAGO DEUDA PÓLIZA");
                    }

                }

                return json;

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

        public static string AdmGestionReEnvioMasivos(EAdmAuxiliares auxiliares)
        {
            try
            {
                EAdmCredenciales credenciales = EGloGlobales.obtenerCredenciales();
                string plataforma = credenciales.urlPlataforma;

                string linksString = "{\"platform\":\"EQUIPAYMENT\",\"links\": " + auxiliares.Masivos + " }";
                JObject linksJson = JObject.Parse(linksString);
                JArray links = (JArray)linksJson["links"];

                foreach (var item in links)
                {
                    string url = plataforma + "?c=" + DGesEncriptacion.CodificarBase64(item["idpago"].ToString()) + "&p=15";

                    DGesEmail.enviarEmailMasivos(item["nombre"].ToString(), item["ramo"].ToString(), item["poliza"].ToString(), item["deuda"].ToString(), url, item["email"].ToString(), "EQUIPAYMENT | PAGO DEUDA PÓLIZA");
                }

                return "Exito";
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public static string AdmObtenerCodigoAsegurado(string documento)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CODIGOASEGURADO");

            string ServicioURL = credenciales.Url; ;
            string AccionSOAP = credenciales.EntityId;

            string Body = ("<ActualizarDatosAccContPOTENCIALxDoc xmlns=\"http://tempuri.org/\" >"
                          + "<sCodUsuario>" + credenciales.UserId + "</sCodUsuario>"
                          + "<sValorParametro>" + documento + "</sValorParametro>"
                          + "<iTipoParametro>1</iTipoParametro>"
                        + "</ActualizarDatosAccContPOTENCIALxDoc>");

            return DGesConexionSOAP.GesEjecutarSolicitudWebSOAP(ServicioURL, AccionSOAP, Body);
        }

        public static string AdmAplicarPago(EAdmAplicacionPago aux)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "APLICARPAGO");

            string ServicioURL = credenciales.Url; ;
            string AccionSOAP = credenciales.EntityId;

            string Body = ("<pagoTarjetaMasivos  xmlns=\"http://tempuri.org/\">"
                              + "<usuario>USR" + aux.Canal + "</usuario>"
                              + "<cod_suc_pago>1</cod_suc_pago>"
                              + "<cod_pagador>" + aux.CodPagador + "</cod_pagador>"
                              + "<nro_couta>" + aux.Cuotas + "</nro_couta>"
                              + "<nro_tarjeta>" + aux.NroTarjeta + "</nro_tarjeta>"
                              + "<nro_autorizacion>" + aux.NroAutorizacion + "</nro_autorizacion>"
                              + "<cod_banco_tarjeta>" + aux.CodBanco + "</cod_banco_tarjeta>"
                              + "<cod_conducto_pago>" + aux.CodConducto + "</cod_conducto_pago>"
                              + "<voucher_tarjeta>" + aux.NroVoucher + "</voucher_tarjeta>"
                              + "<fecha_voucher>" + aux.FechaVoucher + "</fecha_voucher>"
                              + "<apoderado_tarjeta>" + aux.ApoderadoTarjeta + "</apoderado_tarjeta>"
                              + "<IdPv_Valor>" + aux.IdPvs + "</IdPv_Valor>"
                        + "</pagoTarjetaMasivos>");

            return DGesConexionSOAP.GesEjecutarSolicitudWebSOAP(ServicioURL, AccionSOAP, Body);
        }
    }
}
