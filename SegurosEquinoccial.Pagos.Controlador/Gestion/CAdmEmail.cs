using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Gestion
{
    public class CAdmEmail
    {
        public static string enviaEmail(EAdmEmail correo, int idPago)
        {
            return DGesEmail.enviaEmail(correo, idPago);
        }
        public static string enviaEmailNormalReverso(EAdmEmail correo, int idPago)
        {
            return DGesEmail.enviaEmailNormalReverso(correo, idPago);
        }
    }
}
