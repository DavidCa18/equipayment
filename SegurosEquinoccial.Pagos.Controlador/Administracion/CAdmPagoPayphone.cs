using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmPagoPayphone
    {
        public static async Task<string> AdmRealizarPago(EAdmPago pago)
        {
            string resultado = await DAdmPagoPayphone.AdmRealizarPago(pago);
            return resultado;
        }

        public static async Task<EAdmPago> AdmRealizarPagoRecurrente(EAdmPagoDiferidos pago)
        {
            EAdmPago resultado = await DAdmPagoPayphone.AdmRealizarPagoRecurrente(pago);
            return resultado;
        }

        public static async Task<EAdmPago> AdmDetallesPagoPago(ECliParametros parametros)
        {
            EAdmPago resultado = await DAdmPagoPayphone.AdmDetallesPagoPago(parametros);
            return resultado;
        }

        public static async Task<string> AdmObtenerDiferidos(EAdmPago pago)
        {
            string resultado = await DAdmPagoPayphone.AdmObtenerDiferidos(pago);
            return resultado;
        }

        public static async Task<string> AdmReversarPagoPay(EAdmPago pago)
        {
            string resultado = await DAdmPagoPayphone.AdmReversarPagoPay(pago);
            return resultado;
        }

        public static async Task<string> AdmVerificarPagoCliente(string clientId)
        {
            string resultado = await DAdmPagoPayphone.AdmVerificarPagoCliente(clientId);
            return resultado;
        }
    }
}
