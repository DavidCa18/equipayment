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
    public class EAdmAnulacion
    {
        [DataMember]
        public int IdPago { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}
