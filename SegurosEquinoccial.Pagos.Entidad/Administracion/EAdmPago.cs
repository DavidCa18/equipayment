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
    public class EAdmPago
    {
        [DataMember]
        public int Identificador { get; set; }

        [DataMember]
        public int IdPago { get; set; }

        [DataMember]
        public string Ip { get; set; }

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
        public int Estado { get; set; }

        [DataMember]
        public EAdmFactura Factura { get; set; }

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

        //RECIBOS
        [DataMember]
        public string Recibo { get; set; }

        //DATOS TARJETA

        [DataMember]
        public string NombreTarjeta { get; set; }

        [DataMember]
        public string NumeroTarjeta { get; set; }

        [DataMember]
        public string MesExpiracionTarjeta { get; set; }

        [DataMember]
        public string AnioExpiracionTarjeta { get; set; }

        [DataMember]
        public string CodigoVerificacionTarjeta { get; set; }

        [DataMember]
        public string Diferido { get; set; }

        [DataMember]
        public string Link { get; set; }

        [DataMember]
        public string FechaIngreso { get; set; }


        [DataMember]
        public string Data { get; set; }

        [DataMember]
        public string PagoJson { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string Recurrencia { get; set; }

    }
}
