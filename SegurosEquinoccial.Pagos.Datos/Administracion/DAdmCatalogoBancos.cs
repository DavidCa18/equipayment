using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Administracion
{
    public class DAdmCatalogoBancos : DGesConexion
    {
        public static EAdmCatalogoBancos AdmGestionCatalogoBancos(EAdmCatalogoBancos pBanco)
        {

            EAdmCatalogoBancos banco = new EAdmCatalogoBancos();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionAplicacion", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@IdCatalagoBancos", SqlDbType.Int);
                cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Imagen", SqlDbType.Xml);
                cmd.Parameters.Add("@Plataforma", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Diferidos", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Gracia", SqlDbType.Int);
                cmd.Parameters.Add("@Estado", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pBanco.Identificador;
                cmd.Parameters["@IdCatalagoBancos"].Value = pBanco.IdCatalagoBancos;
                cmd.Parameters["@Nombre"].Value = pBanco.Nombre;
                cmd.Parameters["@Imagen"].Value = pBanco.Imagen;
                cmd.Parameters["@Plataforma"].Value = pBanco.Plataforma;
                cmd.Parameters["@Diferidos"].Value = pBanco.Diferidos;
                cmd.Parameters["@Gracia"].Value = pBanco.Gracia;
                cmd.Parameters["@Estado"].Value = pBanco.Estado;


                cmd.ExecuteNonQuery();

                banco.IdCatalagoBancos = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return banco;

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


        public static List<EAdmCatalogoBancos> AdmConsultarCatalogoBancos()
        {

            List<EAdmCatalogoBancos> lstDatos = new List<EAdmCatalogoBancos>();

            EAdmCatalogoBancos rsBancos;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Catalogo_Bancos_Payment ORDER BY @IdCatalagoBancos DESC", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsBancos = new EAdmCatalogoBancos();

                    rsBancos.IdCatalagoBancos = Convert.ToInt32(rdr["IdCatalagoBancos"]);
                    rsBancos.Nombre = rdr["Nombre"].ToString();
                    rsBancos.Imagen = rdr["Imagen"].ToString();
                    rsBancos.Plataforma = rdr["Plataforma"].ToString();
                    rsBancos.Diferidos = rdr["Diferidos"].ToString();
                    rsBancos.Gracia = Convert.ToInt32(rdr["Gracia"]);
                    rsBancos.Estado = Convert.ToInt32(rdr["Estado"]);

                    lstDatos.Add(rsBancos);
                }
                rdr.Close();
                return lstDatos;
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
