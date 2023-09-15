using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmPagoPayphone
    {
        public static async Task<string> AdmRealizarPago(EAdmPago pago)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(pago);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/realizar/pago/normal";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);
        }

        public static async Task<EAdmPago> AdmRealizarPagoRecurrente(EAdmPagoDiferidos pago)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(pago);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/realizar/pago/recurrente";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            EAdmPago pago_ = JsonConvert.DeserializeObject<EAdmPago>(resultado);

            return pago_;
        }

        public static async Task<EAdmPago> AdmDetallesPagoPago(ECliParametros parametros)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(parametros);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/detalle/pago";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            EAdmPago pago_ = JsonConvert.DeserializeObject<EAdmPago>(resultado);

            return pago_;
        }

        public static async Task<string> AdmObtenerDiferidos(EAdmPago pago)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(pago);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/obtener/diferidos";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);
        }

        public static async Task<string> AdmReversarPagoPay(EAdmPago pago)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(pago);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/reversar/pago";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);
        }

        public static async Task<string> AdmVerificarPagoCliente(string clientId)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/verificar/pago/" + clientId;
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", "", "GET");

            return DGesMetodos.limpiarJson(resultado);
        }

        public static async Task<EAdmPago> AdmObtenerPago(int idPago, string trama, EAdmPago datosPago)
        {
            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(datosPago);
            string url = credenciales.Url + "Payphone/SPayPhone.svc/payphone/obtener/pago?id=" + idPago + "&trama=" + trama;
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            EAdmPago pago_ = JsonConvert.DeserializeObject<EAdmPago>(resultado);

            return pago_;
        }

    }
}
