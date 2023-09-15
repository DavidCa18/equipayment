using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmFactura
    {
        public static EAdmFactura AdmGestionFactura(EAdmFactura pFactura)
        {
            return DAdmFactura.AdmGestionFactura(pFactura);
        }
        public static ECliPagoResultado CliGestionClienteFactura(EAdmPago pPago)
        {
            return DAdmFactura.CliGestionClienteFactura(pPago);
        }

        public static EAdmFactura AdmConsultarClienteFactura(int idPago)
        {
            return DAdmFactura.AdmConsultarClienteFactura(idPago);
        }

        public static List<EAdmFactura> AdmConsultarFacturas()
        {
            return DAdmFactura.AdmConsultarFacturas();
        }
    }
}
