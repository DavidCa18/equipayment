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
    public class DAdmRecurrencia : DGesConexion
    {

        public static EAdmRecurrencia AdmGestionRecurrecia(EAdmRecurrencia pRecurrencia)
        {

            EAdmRecurrencia recurrencia = new EAdmRecurrencia();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionRecurrencia", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idRecurrencia", SqlDbType.Int);
                cmd.Parameters.Add("@tokenTarjeta", SqlDbType.NVarChar, -1);
                cmd.Parameters.Add("@holderTarjeta", SqlDbType.NVarChar);
                cmd.Parameters.Add("@numeroDiferidosTotal", SqlDbType.Int);
                cmd.Parameters.Add("@numeroDiferidosActual", SqlDbType.Int);
                cmd.Parameters.Add("@idFactura", SqlDbType.Int);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@plataforma", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pRecurrencia.Identificador;
                cmd.Parameters["@idRecurrencia"].Value = pRecurrencia.IdRecurrencia;
                cmd.Parameters["@tokenTarjeta"].Value = pRecurrencia.TokenTarjeta;
                cmd.Parameters["@holderTarjeta"].Value = pRecurrencia.Holder;
                cmd.Parameters["@numeroDiferidosTotal"].Value = pRecurrencia.NumeroDiferidosTotal;
                cmd.Parameters["@numeroDiferidosActual"].Value = pRecurrencia.NumeroDiferidosActual;
                cmd.Parameters["@idFactura"].Value = pRecurrencia.Factura.IdFactura;
                cmd.Parameters["@estado"].Value = pRecurrencia.Estado;
                cmd.Parameters["@plataforma"].Value = pRecurrencia.Plataforma;


                cmd.ExecuteNonQuery();

                recurrencia.IdRecurrencia = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return recurrencia;

            }
            catch (SqlException)
            {
                Cerrar();
                throw;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
