using Newtonsoft.Json;
using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmPagoWidget
    {


        public static async Task<string> AdmObtenerChekoutId (EAdmPago cliente)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(cliente);
            string url = credenciales.Url + "Datafast/SDatafast.svc/datafast/obtener/checkoutid";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);

        }

        public static async Task<EAdmPago> AdmObtenerResultadoPago (ECliParametros parametros)
        {
            EAdmCredenciales credenciales_ = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(parametros);
            string url = credenciales_.Url + "Datafast/SDatafast.svc/datafast/obtener/resultado/pago";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            EAdmPago pago = JsonConvert.DeserializeObject<EAdmPago>(resultado);
            return pago;
        }

        public static async Task<string> AdmVerificarResultadoPago (ECliParametros parametros)
        {

            EAdmCredenciales credenciales_ = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(parametros);
            string url = credenciales_.Url + "Datafast/SDatafast.svc/datafast/verificar/resultado/pago";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);
        }

        public static async Task<string> AdmReversarPago (EAdmPago pago)
        {
            EAdmCredenciales credenciales_ = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");
            var body = JsonConvert.SerializeObject(pago);
            string url = credenciales_.Url + "Datafast/SDatafast.svc/datafast/reversar/pago";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");

            return DGesMetodos.limpiarJson(resultado);

        }

        public static async Task<EAdmPago> AdmReVerificacionPago (int id, int idApp, string gracia, string banco, string json, EAdmPago dpago)
        {

            EAdmCredenciales credenciales_ = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "D-SERVICIO");

            EAdmPago tempPago = new EAdmPago();
            EAdmFactura tempFactura = new EAdmFactura();

            tempPago.Ip = dpago.Ip;
            tempPago.Banco = dpago.Banco;
            tempPago.Gracia = dpago.Gracia;
            tempPago.PagoJson = json;

            tempFactura.Subtotal12 = dpago.Factura.Subtotal12;
            tempFactura.Subtotal0 = dpago.Factura.Subtotal0;
            tempFactura.Subtotal = dpago.Factura.Subtotal;
            tempFactura.Iva = dpago.Factura.Iva;
            tempFactura.Total = dpago.Factura.Total;
            tempFactura.UrlRetorno = dpago.Factura.UrlRetorno;

            tempPago.Factura = tempFactura;

            var body = JsonConvert.SerializeObject(tempPago);
            string url = credenciales_.Url + "Datafast/SDatafast.svc/datafast/reverificacion/pago?id=" + id + "&idApp=" + idApp + "&gracia=" + gracia + "&banco=" + banco + "";
            string resultado = await DGesConexionREST.GesEjecutarSolicitudREST(url, "", body, "POST");
            EAdmPago pago = JsonConvert.DeserializeObject<EAdmPago>(resultado);
            return pago;

        }

    }
}
