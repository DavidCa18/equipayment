using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmAplicacion
    {
        public static EAdmAplicacion AdmGestionAplicacion(EAdmAplicacion pAplicacion)
        {
            return DAdmAplicacion.AdmGestionAplicacion(pAplicacion);
        }

        public static EAdmAplicacion AdmConsultarDatosAplicacion(int idAplicacion)
        {
            return DAdmAplicacion.AdmConsultarDatosAplicacion(idAplicacion);
        }

        public static List<EAdmAplicacion> AdmConsultarAplicaciones()
        {
            return DAdmAplicacion.AdmConsultarAplicaciones();
        }

        public static List<EAdmAplicacion> AdmConsultarComboAplicaciones()
        {
            return DAdmAplicacion.AdmConsultarComboAplicaciones();
        }
    }
}
