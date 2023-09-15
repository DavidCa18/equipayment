using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmCatalogoBines : DGesConexion
    {
        public static EAdmCatalogoBines MAdmConsultarCatalogoBines(string bin)
        {

            EAdmCatalogoBines rsBines = new EAdmCatalogoBines();

            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Catalogo_Bines_Payment WHERE Bin = @bin AND Estado != 0", getCnn());
                cmd.Parameters.AddWithValue("@bin", bin);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    rsBines.IdCatalagoBines = Convert.ToInt32(rdr["IdCatalagoBines"]);
                    rsBines.CodigoConducto = rdr["CodigoConducto"].ToString();
                    rsBines.CodigoBanco = rdr["CodigoBanco"].ToString();
                    rsBines.Nombre = rdr["Nombre"].ToString();
                    rsBines.Descripcion = rdr["Descripcion"].ToString();
                    rsBines.Estado = Convert.ToInt32(rdr["Estado"]);
                    rsBines.Bin = rdr["Bin"].ToString();


                }
                rdr.Close();
                return rsBines;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
