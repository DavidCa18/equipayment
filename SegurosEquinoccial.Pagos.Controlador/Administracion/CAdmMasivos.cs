using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmMasivos
    {
        public static string AdmGestionMasivos(EAdmAuxiliares auxiliares)
        {
            return DAdmMasivos.AdmGestionMasivos(auxiliares);
        }

        public static string AdmGestionReEnvioMasivos(EAdmAuxiliares auxiliares)
        {
            return DAdmMasivos.AdmGestionReEnvioMasivos(auxiliares);
        }

        public static string AdmObtenerCodigoAsegurado(string documento)
        {
            return DAdmMasivos.AdmObtenerCodigoAsegurado(documento);
        }

        public static string AdmAplicarPago(EAdmAplicacionPago aux)
        {
            return DAdmMasivos.AdmAplicarPago(aux);
        }
    }
}
