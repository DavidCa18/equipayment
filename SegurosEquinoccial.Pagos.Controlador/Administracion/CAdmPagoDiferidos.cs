using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmPagoDiferidos
    {
        public static EAdmPagoDiferidos AdmGestionPagoDiferidos(EAdmPagoDiferidos pPagosDiferidos)
        {
            return DAdmPagoDiferidos.AdmGestionPagoDiferidos(pPagosDiferidos);
        }

        public static List<EAdmPagoDiferidos> AdmConsultarPagosRecurrentes()
        {
            return DAdmPagoDiferidos.AdmConsultarPagosRecurrentes();
        }
    }
}
