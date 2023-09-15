using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmPago
    {
        public static string AdmGestionPago(EAdmPago pPago)
        {
            return DAdmPago.AdmGestionPago(pPago);
        }

        public static int AdmConsultaIntentosPago(int idPago)
        {
            return DAdmPago.AdmConsultaIntentosPago(idPago);
        }

        public async Task<EAdmPago> AdmConsultaPago(string idPago)
        {
            EAdmPago resultado = await DAdmPago.AdmConsultaPago(Convert.ToInt32(idPago));
            return resultado;
        }

        public static EAdmPago AdmConsultarPagoCliente(string cedula, string certificado, string valor)
        {
            return DAdmPago.AdmConsultarPagoCliente(cedula, certificado, valor);
        }

        public static List<EAdmPago> AdmConsultaListaPagosExitosos(string plataforma)
        {
            return DAdmPago.AdmConsultaListaPagosExitosos(plataforma);
        }

        public static List<EAdmPago> AdmConsultaListaPagos(EAdmAuxiliares aux)
        {
            return DAdmPago.AdmConsultaListaPagos(aux);
        }

        public static string AdmConsultaRecibo(int idPago)
        {
            return DAdmPago.AdmConsultaRecibo(idPago);
        }

        public static EAdmPago AdmConsultaEstadoPago(int idPago)
        {
            return DAdmPago.AdmConsultaEstadoPago(idPago);
        }

        public static string generarReciboPayphone(EAdmPago pago)
        {
            return DAdmPago.recorrerJSON(pago);
        }

        public static int AdmActualizarPago(int idPago)
        {
            return DAdmPago.AdmActualizarPago(idPago);
        }

        public static EAdmPago AdmConsultaPagoInicio(int idPago)
        {
            return DAdmPago.AdmConsultaPagoInicio(idPago);
        }

        public static EAdmAuxiliares AdmConsultaResumenPago(string fechaInicio, string fechaFin)
        {
            return DAdmPago.AdmConsultaResumenPago(fechaInicio, fechaFin);
        }

        public static EAdmAnulacion AdmAnularPago(int idPago)
        {
            return DAdmPago.AdmAnularPago(idPago);
        }

        public static EAdmAnulacion AdmExpirarPago(int idPago)
        {
            return DAdmPago.AdmExpirarPago(idPago);
        }

        public static EAdmFactura AdmConsultaDetallePago(int idpago)
        {
            return DAdmPago.AdmConsultaDetallePago(idpago);
        }
    }
}
