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
    public class EAdmFactura
    {

        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdFactura { get; set; }

        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public string Comercio { get; set; }

        [DataMember]
        public string Subtotal12 { get; set; }

        [DataMember]
        public string Subtotal0 { get; set; }

        [DataMember]
        public string Subtotal { get; set; }

        [DataMember]
        public string Iva { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public EAdmClientes Cliente { get; set; }

        [DataMember]
        public string UrlRetorno { get; set; }

        [DataMember]
        public string EstadoAplicacion { get; set; }


        [DataMember]
        public string IdPv { get; set; }

        [DataMember]
        public string Cuotas { get; set; }

        [DataMember]
        public string Aplicacion { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string Gracia { get; set; }
    }
}
