using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmPagoReverso
    {
        public static string AdmGestionPagoReverso(EAdmPagoReverso pPago)
        {
            return DAdmPagoReverso.AdmGestionPagoReverso(pPago);
        }

        public static List<EAdmPagoReverso> AdmConsultaListaPagosReversos(string plataforma)
        {
            return DAdmPagoReverso.AdmConsultaListaPagosReversos(plataforma);
        }
    }
}
