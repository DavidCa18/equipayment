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
    public  class EAdmPagoDiferidos
    {

        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdPagoDiferidos { get; set; }

        [DataMember]
        public string Numero { get; set; }

        [DataMember]
        public string Subtotal12 { get; set; }

        [DataMember]
        public string Subtotal0 { get; set; }

        [DataMember]
        public string Iva { get; set; }

        [DataMember]
        public string Total { get; set; }

        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public string Pagos { get; set; }

        [DataMember]
        public string TokenTarjeta { get; set; }

        [DataMember]
        public string Holder { get; set; }

        [DataMember]
        public EAdmFactura Factura { get; set; }

        [DataMember]
        public int IdPago { get; set; }

        [DataMember]
        public string Plataforma { get; set; }

        [DataMember]
        public string IdTransaccion { get; set; }

        [DataMember]
        public string CodigoAutenticacion { get; set; }

        [DataMember]
        public string Referencia { get; set; }

        [DataMember]
        public string Lote { get; set; }

        [DataMember]
        public string Voucher { get; set; }

        [DataMember]
        public string ParametroPersonalizado { get; set; }

        [DataMember]
        public string NumeroDiferidos { get; set; }

        [DataMember]
        public string ResultadoCodigo { get; set; }

        [DataMember]
        public string ResultadoTexto { get; set; }

        [DataMember]
        public string ResultadoTrama { get; set; }

        [DataMember]
        public string Intereses { get; set; }

        [DataMember]
        public string RespuestaAdquiriente { get; set; }

        [DataMember]
        public string FechaTransaccion { get; set; }

        [DataMember]
        public string Bin { get; set; }

        [DataMember]
        public string Digitos { get; set; }

        [DataMember]
        public string Apoderado { get; set; }

        [DataMember]
        public string Marca { get; set; }

        [DataMember]
        public string Banco { get; set; }

        [DataMember]
        public string Gracia { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Identificacion { get; set; }

        [DataMember]
        public string JSON { get; set; }

        [DataMember]
        public string Fecha { get; set; }
        


    }
}
