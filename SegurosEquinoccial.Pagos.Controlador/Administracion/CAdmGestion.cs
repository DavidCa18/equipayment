using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmGestion
    {
        public static EAdmGestion AdmGestionGestion(EAdmGestion pGestion)
        {
            return DAdmGestion.AdmGestionGestion(pGestion);
        }
    }
}
