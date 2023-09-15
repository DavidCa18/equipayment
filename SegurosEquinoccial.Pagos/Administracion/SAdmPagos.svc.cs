using SegurosEquinoccial.Pagos.Controlador.Administracion;
using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Administracion
{

    [Serializable]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SAdmPagos : ISAdmPagos
    {
      
    }
}
