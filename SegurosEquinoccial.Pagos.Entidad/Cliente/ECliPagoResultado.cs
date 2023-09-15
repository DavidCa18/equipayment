using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Cliente
{
    [DataContract]
    [Serializable]
    public class ECliPagoResultado
    {
        [DataMember]
        public string IdPago { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}
