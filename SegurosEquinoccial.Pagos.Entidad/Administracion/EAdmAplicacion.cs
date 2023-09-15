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
    public class EAdmAplicacion
    {

        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdAplicacion { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string LogoPrimario { get; set; }

        [DataMember]
        public string LogoSecundario { get; set; }

        [DataMember]
        public string FondoPrimario { get; set; }

        [DataMember]
        public string FondoSecundario { get; set; }

        [DataMember]
        public string ColorPrimario { get; set; }

        [DataMember]
        public string ColorSecundario { get; set; }

        [DataMember]
        public string LogoPrimarioTamano { get; set; }

        [DataMember]
        public string LogoSecundarioTamano { get; set; }

        [DataMember]
        public int Estado { get; set; }

        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public double MontoMaximo { get; set; }

        [DataMember]
        public double MontoMinimo { get; set; }

        [DataMember]
        public int VisualizacionBin { get; set; }

        [DataMember]
        public string Caducidad { get; set; }

        [DataMember]
        public int Recurrencia { get; set; }

        [DataMember]
        public int Gracia { get; set; }
        
    }
}
