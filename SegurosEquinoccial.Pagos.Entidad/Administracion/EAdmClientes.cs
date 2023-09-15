using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Entidad.Administracion
{
    [DataContract]
    [Serializable]
    public class EAdmClientes
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdCliente { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string NombreCompleto { get; set; }

        [DataMember]
        public string PrimerNombre { get; set; }


        [DataMember]
        public string SegundoNombre { get; set; }


        [DataMember]
        public string Apellido { get; set; }


        [DataMember]
        public string Email { get; set; }


        [DataMember]
        public string Telefono { get; set; }


        [DataMember]
        public int Estado { get; set; }


        [DataMember]
        public EAdmAplicacion Aplicacion { get; set; }

        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public string Ip { get; set; }

        [DataMember]
        public string Token { get; set; }
    }
}
