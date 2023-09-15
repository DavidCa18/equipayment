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
    public class EAdmRecurrencia
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdRecurrencia { get; set; }

        [DataMember]
        public string TokenTarjeta { get; set; }

        [DataMember]
        public string Holder { get; set; }

        [DataMember]
        public int NumeroDiferidosTotal { get; set; }

        [DataMember]
        public int NumeroDiferidosActual { get; set; }

        [DataMember]
        public EAdmFactura Factura { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public int Plataforma { get; set; }

    }
}
