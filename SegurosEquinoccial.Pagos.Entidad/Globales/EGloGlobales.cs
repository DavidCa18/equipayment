using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Globales
{
    public class EGloGlobales
    {

        public static string ambiente = "PRUEBAS";

        public static EAdmCredenciales obtenerCredenciales ()
        {
            EAdmCredenciales reCredenciales = new EAdmCredenciales();
            if (ambiente == "DESARROLLO")
            {
                reCredenciales.HostDB = "DESKTOP-U5FLSSN\\SQLEXPRESS";
                reCredenciales.NameDB = "Equipayment";
                reCredenciales.UserDB = "sa";
                reCredenciales.PasswordDB = "123";
                reCredenciales.urlPlataforma = "https://localhost:44332";
            }
            else if (ambiente == "PRUEBAS")
            {
                reCredenciales.HostDB = "equinoccialpymesproduccionsrv.database.windows.net";
                reCredenciales.NameDB = "EquinoccialEquipaymentProduccionDB";
                reCredenciales.UserDB = "pymes_admin";
                reCredenciales.PasswordDB = "pym3s_adm1n";
                reCredenciales.urlPlataforma = "https://equi-prodbpaymentappservice.azurewebsites.net";
            }
            else if (ambiente == "PRODUCCION")
            {
                reCredenciales.HostDB = "equipaymentproduccionsrv.database.windows.net";
                reCredenciales.NameDB = "EquipaymentProduccionDB";
                reCredenciales.UserDB = "equipayment";
                reCredenciales.PasswordDB = "3qu1payment_01";
                reCredenciales.urlPlataforma = "https://equipayment.azurewebsites.net";

                /*reCredenciales.HostDB = "equipayment2server.database.windows.net";
                reCredenciales.NameDB = "equipayment2db";
                reCredenciales.UserDB = "equipaymentadmin";
                reCredenciales.PasswordDB = "3qu1paym3nt.s3rv3r";
                reCredenciales.urlPlataforma = "https://equipayment2.azurewebsites.net";*/

            }

            return reCredenciales;
        }

    }
}
