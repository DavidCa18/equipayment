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
    public class EAdmCatalogoBines
    {
        [DataMember]
        public int Identificador { get; set; }
        [DataMember]
        public string Resultado { get; set; }
        [DataMember]
        public int IdCatalagoBines { get; set; }
        [DataMember]
        public string CodigoConducto { get; set; }
        [DataMember]
        public string CodigoBanco { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public string Bin { get; set; }
    }
}
