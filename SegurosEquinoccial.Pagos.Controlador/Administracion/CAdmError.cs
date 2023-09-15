﻿using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Controlador.Administracion
{
    public class CAdmError
    {
        public static EAdmError AdmGestionError(EAdmError pError)
        {
            return DAdmError.AdmGestionError(pError);
        }

        public static List<EAdmError> AdmConsultarErrores()
        {
            return DAdmError.AdmConsultarErrores();
        }
    }
}
