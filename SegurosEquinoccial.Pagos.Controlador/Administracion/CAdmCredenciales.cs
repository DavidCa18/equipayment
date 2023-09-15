using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmCredenciales
    {
        public static EAdmCredenciales AdmGestionCredenciales(EAdmCredenciales pCredenciales)
        {
            return DAdmCredenciales.AdmGestionCredenciales(pCredenciales);
        }

        public static EAdmCredenciales AdmConsultaCredenciales(string modo, string identificador)
        {
            return DAdmCredenciales.AdmConsultaCredenciales(modo, identificador);
        }
    }
}
