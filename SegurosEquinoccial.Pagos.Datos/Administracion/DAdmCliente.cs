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
    public class DAdmCliente : DGesConexion
    {
        public static EAdmClientes AdmGestionCliente(EAdmClientes pCliente)
        {

            EAdmClientes cliente = new EAdmClientes();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("GestionCliente", getCnn());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@identificador", SqlDbType.Int);
                cmd.Parameters.Add("@idCliente", SqlDbType.Int);
                cmd.Parameters.Add("@identificacion", SqlDbType.NVarChar);
                cmd.Parameters.Add("@primerNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@segundoNombre", SqlDbType.NVarChar);
                cmd.Parameters.Add("@apellido", SqlDbType.NVarChar);
                cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar);
                cmd.Parameters.Add("@estado", SqlDbType.Int);
                cmd.Parameters.Add("@idAplicacion", SqlDbType.Int);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                cmd.Parameters["@identificador"].Value = pCliente.Identificador;
                cmd.Parameters["@idCliente"].Value = pCliente.IdCliente;
                cmd.Parameters["@identificacion"].Value = pCliente.Identificacion;
                cmd.Parameters["@primerNombre"].Value = pCliente.PrimerNombre;
                cmd.Parameters["@segundoNombre"].Value = pCliente.SegundoNombre;
                cmd.Parameters["@apellido"].Value = pCliente.Apellido;
                cmd.Parameters["@email"].Value = pCliente.Email;
                cmd.Parameters["@telefono"].Value = pCliente.Telefono;
                cmd.Parameters["@estado"].Value = pCliente.Estado;
                cmd.Parameters["@idAplicacion"].Value = pCliente.Aplicacion.IdAplicacion;
               
                cmd.ExecuteNonQuery();

                cliente.IdCliente = Convert.ToInt32(cmd.Parameters["@valor"].Value);

                return cliente;

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

        public static List<EAdmClientes> AdmConsultarClientes()
        {

            List<EAdmClientes> lstDatos = new List<EAdmClientes>();

            EAdmAplicacion rsAplicacion;
            EAdmClientes rsClientes;
            try
            {
                Conectar();

                SqlCommand cmd = new SqlCommand("SELECT * FROM ConsultarAplicacionCliente ORDER BY IdCliente DESC", getCnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rsAplicacion = new EAdmAplicacion();
                    rsClientes = new EAdmClientes();

                    rsAplicacion.IdAplicacion = Convert.ToInt32(rdr["IdAplicacion"]);
                    rsAplicacion.Nombre = rdr["Nombre"].ToString();
                    rsAplicacion.LogoPrimario = rdr["LogoPrimario"].ToString();
                    rsAplicacion.LogoSecundario = rdr["LogoSecundario"].ToString();
                    rsAplicacion.FondoPrimario = rdr["FondoPrimario"].ToString();

                    rsClientes.IdCliente = Convert.ToInt32(rdr["IdCliente"]);
                    rsClientes.Identificacion = rdr["Identificacion"].ToString();
                    rsClientes.NombreCompleto = rdr["NombreCompleto"].ToString();
                    rsClientes.PrimerNombre = rdr["PrimerNombre"].ToString();
                    rsClientes.SegundoNombre = rdr["SegundoNombre"].ToString();
                    rsClientes.Apellido = rdr["Apellido"].ToString();
                    rsClientes.Email = rdr["Email"].ToString();
                    rsClientes.Telefono = rdr["Telefono"].ToString();

                    rsClientes.Aplicacion = rsAplicacion;

                    lstDatos.Add(rsClientes);
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
