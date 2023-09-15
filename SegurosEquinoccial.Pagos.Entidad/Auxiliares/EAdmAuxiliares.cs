using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Auxiliares
{
    [DataContract]
    [Serializable]
    public class EAdmAuxiliares
    {
        [DataMember]
        public string EstadoReporte { get; set; }

        [DataMember]
        public string TotalReporte { get; set; }

        [DataMember]
        public string FechaInicio { get; set; }

        [DataMember]
        public string FechaFin { get; set; }

        [DataMember]
        public string Cadena { get; set; }

        [DataMember]
        public string Masivos { get; set; }
    }
}
