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
    public class EAdmGestion
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdGestion { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public int Estado { get; set; }
    }
}
