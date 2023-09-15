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
    public class CAdmHistorialTransacciones
    {
        public static EAdmHistorialTransacciones AdmGestionTransacciones(EAdmHistorialTransacciones pHistorial)
        {
            return DAdmHistorialTransacciones.AdmGestionTransacciones(pHistorial);
        }

        public static List<EAdmHistorialTransacciones> AdmConsultarHistorialTransacciones(EAdmAuxiliares aux)
        {
            return DAdmHistorialTransacciones.AdmConsultarHistorialTransacciones(aux);
        }
    }
}
