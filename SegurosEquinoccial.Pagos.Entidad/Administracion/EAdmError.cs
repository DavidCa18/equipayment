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
    public class EAdmError
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdError { get; set; }

        [DataMember]
        public string Clase { get; set; }

        [DataMember]
        public string Metodo { get; set; }

        [DataMember]
        public string UriTemplate { get; set; }

        [DataMember]
        public string Estatus { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Fecha { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public string NombreUsuario { get; set; }
    }
}
