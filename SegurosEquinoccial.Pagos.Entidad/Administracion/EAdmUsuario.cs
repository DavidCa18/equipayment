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
    public class EAdmUsuario
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Contrasena { get; set; }

        [DataMember]
        public string Contrasena2 { get; set; }

        [DataMember]
        public string Rol { get; set; }


    }
}
