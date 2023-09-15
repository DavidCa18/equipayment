using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesConexion
    {
        public static SqlConnection cnn;

        public static SqlConnection getCnn()
        {
            return cnn;
        }

        public static void Conectar()
        {
            EAdmCredenciales credenciales = EGloGlobales.obtenerCredenciales();
            cnn = new SqlConnection("Data Source=" + credenciales.HostDB + ";Initial Catalog=" + credenciales.NameDB + ";User ID=" + credenciales.UserDB + ";Password=" + credenciales.PasswordDB + ";");
            cnn.Open();
        }

        public static void Cerrar()
        {
            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
        }
    }
}
