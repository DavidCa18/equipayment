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
    public class EAdmCatalogoBancos
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdCatalagoBancos { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Imagen { get; set; }

        [DataMember]
        public string Plataforma { get; set; }

        [DataMember]
        public string Diferidos { get; set; }

        [DataMember]
        public int Gracia { get; set; }

        [DataMember]
        public int Estado { get; set; }
    }
}
