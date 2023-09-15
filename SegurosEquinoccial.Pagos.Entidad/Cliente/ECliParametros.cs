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
    public class ECliParametros
    {

        [DataMember]
        public string ResourcePath { get; set; }

        [DataMember]
        public int IdPago { get; set; }

        [DataMember]
        public int IdTransaccion { get; set; }

        [DataMember]
        public string Iva { get; set; }

        [DataMember]
        public string Subtotal12 { get; set; }

        [DataMember]
        public string Subtotal0 { get; set; }

        [DataMember]
        public int IdAplicacion { get; set; }

        [DataMember]
        public string Banco { get; set; }

        [DataMember]
        public string Gracia { get; set; }
        [DataMember] public string Transaccion { get; set; }
    }
}
