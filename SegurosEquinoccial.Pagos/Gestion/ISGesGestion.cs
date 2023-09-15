using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Cliente;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Gestion
{
    [ServiceContract]
    public interface ISGesGestion
    {

        //*** GESTION CATALOGO BANCOS ***
        //GESTIONAR CRUD CATALOGO BANCOS
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/catalogo/bancos/gestion")]
        EAdmCatalogoBancos AdmGestionCatalogoBancos(EAdmCatalogoBancos pBanco);

        //LISTAR CATALOGO BANCOS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/catalogo/bancos/buscar")]
        List<EAdmCatalogoBancos> AdmConsultarCatalogoBancos();


        //*** GESTION CLIENTE - FACTURA ***
        //GESTIONAR CRUD FACTURA
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/factura/gestion")]
        EAdmFactura AdmGestionFactura(EAdmFactura pFactura);

        //GUARDAR DATOS DEL CLIENTE - FACTURA
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/factura/ingresar/datos")]
        ECliPagoResultado CliGestionClienteFactura(EAdmPago pPago);

        //OBTENER LOS DATOS DEL CLIENTE - FACTURA
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/factura/buscar/datos/{idPago}")]
        EAdmFactura AdmConsultarClienteFactura(string idPago);

        //OBTENER LISTA DE TODAS LAS FACTURAS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/factura/listar/completo")]
        List<EAdmFactura> AdmConsultarFacturas();

        //*** GESTION PAGO DATAFAST ***
        //GENERAR EL CHECKOUT ID 
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/datafast/generar/pago")]

        Task<string> AdmObtenerChekoutId(EAdmPago cliente);

        //OBTENER EL RESULTADO DEL PAGO 
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/datafast/obtener/resultado")]

        Task<EAdmPago> AdmObtenerResultadoPago(ECliParametros parametros);

        //REVERSAR PAGO 
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/datafast/reversar")]

        Task<string> AdmReversarPago(EAdmPago pago);


        //veriricar estado pago
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/datafast/verificar/pago")]

        Task<string> AdmVerificarResultadoPago(ECliParametros parametros);

        //*** GESTION PAGO PAYPHONE ***
        //OBTENER DIFERIDOS PAYPHONE
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/obtener/diferidos")]
        Task<string> AdmObtenerDiferidos(EAdmPago pago);

        //REALIZAR PAGO
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/generar/pago")]
        Task<string> AdmRealizarPago(EAdmPago pago);

        //REALIZAR PAGO RECURRENTE
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/generar/pago/recurrente")]
        Task<EAdmPago> AdmRealizarPagoRecurrente(EAdmPagoDiferidos pago);

        //OBTENER EL RESULTADO DEL PAGO 
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/obtener/resultado")]
        Task<EAdmPago> AdmDetallesPagoPago(ECliParametros parametros);

        //REVERSAR PAGO PAY
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/reversar")]
        Task<string> AdmReversarPagoPay(EAdmPago pago);


        //VERIFICAR DETALLES PAGO CLIENTE
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/payphone/verificar/pago/cliente/{clientId}")]
        Task<string> AdmVerificarPagoCliente(string clientId);


        //*** GESTION MENSAJES ***
        //OBTENER LISTA DE MENSAJES
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/mensajes/listar/{codigo}")]
        EAdmMensajes CliConsultarMensaje(string codigo);


        //*** GESTION CREDENCIALES ***
        //GESTIONAR CRUD CREDENCIALES
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/credenciales/gestion")]
        EAdmCredenciales AdmGestionCredenciales(EAdmCredenciales pCredenciales);


        //LISTAR CREDENCIALES
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/credenciales/listar?modo={modo}&identificador={identificador}")]
        EAdmCredenciales AdmConsultaCredenciales(string modo, string identificador);

        //*** GESTION APLICACION ***
        //GESTIONAR CRUD APLICACION
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/aplicacion/gestion")]
        EAdmAplicacion AdmGestionAplicacion(EAdmAplicacion pAplicacion);

        //LISTAR APLICACION
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/aplicacion/listar/{idAplicacion}")]
        EAdmAplicacion AdmConsultarDatosAplicacion(string idAplicacion);

        //LISTAR TODAS LAS APLICACIONES
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/aplicacion/listar/completo")]
        List<EAdmAplicacion> AdmConsultarAplicaciones();

        //LISTAR TODAS LAS APLICACIONES PARA UN COMBOBOX
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/aplicacion/listar/combo/completo")]
        List<EAdmAplicacion> AdmConsultarComboAplicaciones();

        //*** GESTION PAGO ***
        //GESTIONAR CRUD PAGO
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/gestion")]
        string AdmGestionPago(EAdmPago pPago);

        //GESTIONAR CRUD PAGO
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/token")]
        string AdmGestionTokenPago(EAdmToken pToken);

        //LISTAR UN PAGO ESPECIFICO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/listar/{idPago}")]
        Task<EAdmPago> AdmConsultaPago(string idPago);


        //LISTAR UN PAGO POR DIFERENTES PARAMETROS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/cliente?cedula={cedula}&certificado={certificado}&valor={valor}")]
        EAdmPago AdmConsultarPagoCliente(string cedula, string certificado, string valor);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/listar/inicio/{idPago}")]
        EAdmPago AdmConsultaPagoInicio(string idPago);

        //LISTAR EL ESTADO DE UN PAGO ESPECIFICO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/estado/listar/{idPago}")]
        EAdmPago AdmConsultaEstadoPago(string idPago);

        //LISTAR TODOS LOS PAGOS
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/listar/completo")]
        List<EAdmPago> AdmConsultaListaPagos(EAdmAuxiliares aux);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/consultar/recibo/{idPago}")]
        string AdmConsultaRecibo(string idPago);

        //LISTAR TODOS LOS PAGOS EXITOSOS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/listar/exitosos/{plataforma}")]
        List<EAdmPago> AdmConsultaListaPagosExitosos(string plataforma);

        //REINTENTAR PAGO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/actualizar/estado/{idPago}")]
        int AdmActualizarPago(string idPago);

        //LISTAR RESUMEN DE PAGOS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/listar/resumen?fechaInicio={fechaInicio}&fechaFin={fechaFin}")]
        EAdmAuxiliares AdmConsultaResumenPago(string fechaInicio, string fechaFin);


        //LISTAR DETALLE DE LA APLICACION CLIENTE FACTURA
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "cliente/listar/detalle/{idpago}")]
        EAdmFactura AdmConsultaDetallePago(string idpago);

        //*** GESTION REVERSO PAGO ***
        //GESTIONAR CRUD PAGO REVERSO
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/reverso/gestion")]
        string AdmGestionPagoReverso(EAdmPagoReverso pPago);

        //LISTAR LOS PAGOS REVERSADOS
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/reverso/listar/{plataforma}")]
        List<EAdmPagoReverso> AdmConsultaListaPagosReversos(string plataforma);

        //*** GESTION USUARIOS ***
        //INICIAR SESION USUARIO
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/usuario/verificar")]
        EAdmUsuario AdmVerificacionUsuario(EAdmUsuario usuario);

        //GESTIONAR CRUD USUARIO ***PRUEBA
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/usuario/gestion/contrasena")]
        string GestionUsuario(EAdmUsuario usuario);

        //*** GESTION ERRORES ***
        //GESTION CRUD ERRORES
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/error/gestion")]
        EAdmError AdmGestionError(EAdmError pError);

        //LISTAR TODOS LOS ERRORES
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/error/listar")]
        List<EAdmError> AdmConsultarErrores();

        //**** GESTION CLIENTE ****
        //GESTIONAR CRUD CLIENTE
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/cliente/gestion")]
        EAdmClientes AdmGestionCliente(EAdmClientes pCliente);

        //LISTAR TODOS LOS CLIENTES
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/cliente/listar/completo")]
        List<EAdmClientes> AdmConsultarClientes();


        //**** GENERAR RECIBO ****
        //GENERAR RECIBO PAYPHONE
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/pago/recibo/payphone")]
        string generarReciboPayphone(EAdmPago pago);

        //**** ENVIAR CORREO ELECTRONICO CON ADJUNTOS ****
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "enviar/pago/recibo/{idPago}")]
        string enviaEmail(EAdmEmail correo, string idPago);


        //**** ENVIAR CORREO ELECTRONICO CONFIRMACION REVERSO ****
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "enviar/email/reverso/{idPago}")]
        string enviaEmailNormalReverso(EAdmEmail correo, string idPago);

        //*** ENCRIPTAR DATA ***
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "encriptar/datos")]
        string encriptar(EAdmEmail datos);


        //*** DESENCRIPTAR DATA ***
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "desencriptar/datos")]
        string desencriptar(EAdmEmail datos);

        //*** CONSULTAR BINES ***
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "bin/listar/{bin}")]
        EAdmCatalogoBines MAdmConsultarCatalogoBines(string bin);

        //*** CONSUYLTAR INTENTOS ***
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/verificar/intentos/{idPago}")]
        int AdmConsultaIntentosPago(string idPago);

        //******* GESTION USUARIOS *******

        //CRUD USUARIO 
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/usuario/gestion")]
        int AdmGestionUsuario(EAdmUsuario usuariop);


        //******* GESTION PAGO ESTADOS *******

        //ANULAR PAGO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/anulacion/{idPago}")]
        EAdmAnulacion AdmAnularPago(string idPago);

        //EXPIRAR PAGO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/expiracion/{idPago}")]
        EAdmAnulacion AdmExpirarPago(string idPago);

        //******* GESTION GESTION *******

        //GUARDAR GESTION
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/gestion/gestion")]
        EAdmGestion AdmGestionGestion(EAdmGestion pGestion);


        //******* GESTION MASIVOS *******
        //GUARDAR Y GENERAR LINKS MASIVOS
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/masivos/links")]
        string AdmGestionMasivos(EAdmAuxiliares auxiliares);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/masivos/envio/email")]
        string AdmGestionReEnvioMasivos(EAdmAuxiliares auxiliares);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/masivos/codigo/asegurado/{documento}")]
        string AdmObtenerCodigoAsegurado(string documento);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/masivos/aplicacion/pago")]
        string AdmAplicarPago(EAdmAplicacionPago aux);


        //******* GESTION HISTORIAL TRANSACCIONES *******
        //CRUD TRANSACCIONES
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/historial/transacciones/gestion")]
        EAdmHistorialTransacciones AdmGestionTransacciones(EAdmHistorialTransacciones pHistorial);

        //LISTAR TRANSACCIONES POR PARAMETROS PERSONALIZADOS
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/historial/transacciones/listar/parametros")]
        List<EAdmHistorialTransacciones> AdmConsultarHistorialTransacciones(EAdmAuxiliares aux);


        //******* GESTION PAGO DIFERIDOS *******
        //CRUD PAGO DIFERIDOS
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/diferidos/gestion")]
        EAdmPagoDiferidos AdmGestionPagoDiferidos(EAdmPagoDiferidos pPagosDiferidos);


        //LISTAR PAGOS RECURRENTES 
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/diferidos/recurrentes/listar")]
        List<EAdmPagoDiferidos> AdmConsultarPagosRecurrentes();


        //******* GESTION PARAMETROS RECURRENCIA *******
        //CRUD RECURRENCIA
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "pago/recurrecia/gestion")]
        EAdmRecurrencia AdmGestionRecurrecia(EAdmRecurrencia pRecurrencia);
    }
}
