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
    public class EAdmHistorialTransacciones
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public string Resultado { get; set; }

        [DataMember]
        public int IdHistorialTransacciones { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

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
        public string IdPv { get; set; }

        [DataMember]
        public string Poliza { get; set; }

        [DataMember]
        public string Aplicacion { get; set; }

        [DataMember]
        public string Diferidos { get; set; }

        [DataMember]
        public string TipoDiferido { get; set; }

        [DataMember]
        public string Comercio { get; set; }

        [DataMember]
        public string Subtotal12 { get; set; }

        [DataMember]
        public string Subtotal0 { get; set; }

        [DataMember]
        public string Iva { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public string UrlRetorno { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public string FechaIngreso { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public string Trama { get; set; }

        [DataMember]
        public string Cuotas { get; set; }

        [DataMember]
        public string FacturaAplicacion { get; set; }
    }
}
