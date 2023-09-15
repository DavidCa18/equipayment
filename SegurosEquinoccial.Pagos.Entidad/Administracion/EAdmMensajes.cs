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
    public class EAdmMensajes
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdMensaje { get; set; }

        [DataMember]
        [Required]
        public string Codigo { get; set; }

        [DataMember]
        [Required]
        public string Texto { get; set; }

        [DataMember]
        [Required]
        public string Descripcion { get; set; }

        [DataMember]
        [Required]
        public int Estado { get; set; }

        [DataMember]
        [Required]
        public int Plataforma { get; set; }


        [DataMember]
        [Required]
        public string Imagen { get; set; }
    }
}
