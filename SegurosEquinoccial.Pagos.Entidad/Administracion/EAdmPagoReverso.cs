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
    public class EAdmPagoReverso
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdPagoReverso { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string NumeroReferencia { get; set; }

        [DataMember]
        public string RespuestaAdquiriente { get; set; }

        [DataMember]
        public string CodigoAutenticacion { get; set; }

        [DataMember]
        public string Conector { get; set; }

        [DataMember]
        public string ResultadoCodigo { get; set; }

        [DataMember]
        public string ResultadoTexto { get; set; }

        [DataMember]
        public string ResultadoTrama { get; set; }

        [DataMember]
        public string FechaReverso { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public EAdmPago Pago { get; set; }
    }
}
