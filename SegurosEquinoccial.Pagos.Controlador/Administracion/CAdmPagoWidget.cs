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
    public class CAdmPagoWidget
    {

        public static async Task<string> AdmObtenerChekoutId(EAdmPago cliente)
        {
            string resultado = await DAdmPagoWidget.AdmObtenerChekoutId(cliente);
            return resultado;
        }

        public static async Task<EAdmPago> AdmObtenerResultadoPago(ECliParametros parametros)
        {
            EAdmPago resultado = await DAdmPagoWidget.AdmObtenerResultadoPago(parametros);
            return resultado;
        }

        public static async Task<string> AdmReversarPago(EAdmPago pago)
        {
            string resultado = await DAdmPagoWidget.AdmReversarPago(pago);
            return resultado;
        }

        public static async Task<string> AdmVerificarResultadoPago(ECliParametros parametros)
        {
            string resultado = await DAdmPagoWidget.AdmVerificarResultadoPago(parametros);
            return resultado;
        }

    }
}
