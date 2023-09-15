using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Administracion
{
    [DataContract]
    [Serializable]
    public class EAdmAplicacionPago
    {
        [DataMember] public string Canal { get; set; }
        [DataMember] public string CodPagador { get; set; }
        [DataMember] public string NroTarjeta { get; set; }
        [DataMember] public string NroAutorizacion { get; set; }
        [DataMember] public string CodBanco { get; set; }
        [DataMember] public string CodConducto { get; set; }
        [DataMember] public string NroVoucher { get; set; }
        [DataMember] public string FechaVoucher { get; set; }
        [DataMember] public string ApoderadoTarjeta { get; set; }
        [DataMember] public string IdPvs { get; set; }

        [DataMember] public string Cuotas { get; set; }
        
    }
}
