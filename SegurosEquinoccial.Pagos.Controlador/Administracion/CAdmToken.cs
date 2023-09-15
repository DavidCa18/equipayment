using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmToken
    {
        public static string AdmGestionTokenPago(EAdmToken pToken)
        {
            return DAdmToken.AdmGestionTokenPago(pToken);
        }
    }
}
