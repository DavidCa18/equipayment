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
    public class EAdmCredenciales
    {
        /* CONEXION A SERVICIOS WEB  */
        [DataMember]
        public int Identificador { get; set; }
        
        [DataMember]
        public int IdCredenciales { get; set; }

        [DataMember]
        [Required]
        public string Url { get; set; }

        [DataMember]
        [Required]
        public string UserId { get; set; }

        [DataMember]
        [Required]
        public string Password { get; set; }

        [DataMember]
        [Required]
        public string EntityId { get; set; }

        [DataMember]
        [Required]
        public string MID { get; set; }

        [DataMember]
        [Required]
        public string TIP { get; set; }

        [DataMember]
        [Required]
        public string Modo { get; set; }

        [DataMember]
        [Required]
        public string Identificador_ { get; set; }

        [DataMember]
        [Required]
        public int Estado { get; set; }

        /* LINKS */

        [DataMember]
        public string urlPlataforma { get; set; }

        /*BASE DE DATOS*/
        [DataMember]
        public string HostDB { get; set; }

        [DataMember]
        public string NameDB { get; set; }

        [DataMember]
        public string UserDB { get; set; }

        [DataMember]
        public string PasswordDB { get; set; }
    }
}
