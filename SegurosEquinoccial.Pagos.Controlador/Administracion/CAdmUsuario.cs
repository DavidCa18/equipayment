using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmUsuario
    {

        public static int AdmGestionUsuario(EAdmUsuario usuariop)
        {
            return DAdmUsuario.AdmGestionUsuario(usuariop);

        }
        public static EAdmUsuario AdmVerificacionUsuario(EAdmUsuario usuario)
        {
            return DAdmUsuario.AdmVerificacionUsuario(usuario);
        }

        public static string GestionUsuario(EAdmUsuario usuario)
        {
            return DAdmUsuario.GestionUsuario(usuario);
        }
    }
}
