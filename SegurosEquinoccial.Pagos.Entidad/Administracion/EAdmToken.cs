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
    public class EAdmToken
    {

        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdToken { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string Marca { get; set; }

        [DataMember]
        public string Banco { get; set; }

        [DataMember]
        public EAdmClientes Cliente { get; set; }

        [DataMember]
        public int Estado { get; set; }
    }
}
