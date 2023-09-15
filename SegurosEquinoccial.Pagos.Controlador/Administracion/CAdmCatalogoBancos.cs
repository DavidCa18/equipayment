using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmCatalogoBancos
    {
        public static EAdmCatalogoBancos AdmGestionCatalogoBancos(EAdmCatalogoBancos pBanco)
        {
            return DAdmCatalogoBancos.AdmGestionCatalogoBancos(pBanco);
        }

        public static List<EAdmCatalogoBancos> AdmConsultarCatalogoBancos()
        {
            return DAdmCatalogoBancos.AdmConsultarCatalogoBancos();
        }
    }
}
