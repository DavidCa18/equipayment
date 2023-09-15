using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SegurosEquinoccial.Pagos.Controlador.Administracion;
using SegurosEquinoccial.Pagos.Controlador.Gestion;
using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Datos.Gestion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Cliente;

namespace SegurosEquinoccial.Pagos.Gestion
{
    [Serializable]
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class SGesGestion : ISGesGestion
    {
        public EAdmCredenciales AdmConsultaCredenciales(string modo, string identificador)
        {
            return DAdmCredenciales.AdmConsultaCredenciales(modo, identificador);
        }

        public async Task<EAdmPago> AdmConsultaPago(string idPago)
        {
            EAdmPago resultado = await DAdmPago.AdmConsultaPago(Convert.ToInt32(idPago));
            return resultado;
        }

        public EAdmFactura AdmConsultarClienteFactura(string idPago)
        {
            return DAdmFactura.AdmConsultarClienteFactura(Convert.ToInt32(idPago));
        }

        public EAdmAplicacion AdmConsultarDatosAplicacion(string idAplicacion)
        {
            return DAdmAplicacion.AdmConsultarDatosAplicacion(Convert.ToInt32(idAplicacion));
        }

        public EAdmAplicacion AdmGestionAplicacion(EAdmAplicacion pAplicacion)
        {
            return DAdmAplicacion.AdmGestionAplicacion(pAplicacion);
        }

        public EAdmCredenciales AdmGestionCredenciales(EAdmCredenciales pCredenciales)
        {
            return DAdmCredenciales.AdmGestionCredenciales(pCredenciales);
        }

        public string AdmGestionPago(EAdmPago pPago)
        {
            return DAdmPago.AdmGestionPago(pPago);
        }

        public async Task<string> AdmObtenerChekoutId(EAdmPago cliente)
        {
            string resultado = await CAdmPagoWidget.AdmObtenerChekoutId(cliente);
            return resultado;
        }

        public async Task<EAdmPago> AdmObtenerResultadoPago(ECliParametros parametros)
        {
            EAdmPago resultado = await CAdmPagoWidget.AdmObtenerResultadoPago(parametros);
            return resultado;
        }

        public async Task<string> AdmObtenerDiferidos(EAdmPago pago)
        {
            string resultado = await CAdmPagoPayphone.AdmObtenerDiferidos(pago);
            return resultado;
        }


        public async Task<string> AdmRealizarPago(EAdmPago pago)
        {
            string resultado = await CAdmPagoPayphone.AdmRealizarPago(pago);
            return resultado;
        }

        public async Task<EAdmPago> AdmDetallesPagoPago(ECliParametros parametros)
        {
            EAdmPago resultado = await CAdmPagoPayphone.AdmDetallesPagoPago(parametros);
            return resultado;
        }

        public EAdmMensajes CliConsultarMensaje(string codigo)
        {
            return DAdmMensajes.CliConsultarMensaje(codigo);
        }

        public ECliPagoResultado CliGestionClienteFactura(EAdmPago pPago)
        {
            return DAdmFactura.CliGestionClienteFactura(pPago);
        }

        public EAdmUsuario AdmVerificacionUsuario(EAdmUsuario usuario)
        {
            return CAdmUsuario.AdmVerificacionUsuario(usuario);
        }

        public string GestionUsuario(EAdmUsuario usuario)
        {
            return CAdmUsuario.GestionUsuario(usuario);
        }

        public List<EAdmPago> AdmConsultaListaPagosExitosos(string plataforma)
        {
            return CAdmPago.AdmConsultaListaPagosExitosos(plataforma);
        }

        public async Task<string> AdmReversarPago(EAdmPago pago)
        {
            string resultado = await CAdmPagoWidget.AdmReversarPago(pago);
            return resultado;
        }

        public string AdmGestionPagoReverso(EAdmPagoReverso pPago)
        {
            return CAdmPagoReverso.AdmGestionPagoReverso(pPago);
        }

        public List<EAdmPagoReverso> AdmConsultaListaPagosReversos(string plataforma)
        {
            return CAdmPagoReverso.AdmConsultaListaPagosReversos(plataforma);
        }

        public async Task<string> AdmReversarPagoPay(EAdmPago pago)
        {
            string resultado = await CAdmPagoPayphone.AdmReversarPagoPay(pago);
            return resultado;
        }

        public EAdmError AdmGestionError(EAdmError pError)
        {
            return CAdmError.AdmGestionError(pError);
        }

        public List<EAdmError> AdmConsultarErrores()
        {
            return CAdmError.AdmConsultarErrores();
        }

        public List<EAdmPago> AdmConsultaListaPagos(EAdmAuxiliares aux)
        {
            return CAdmPago.AdmConsultaListaPagos(aux);
        }

        public List<EAdmFactura> AdmConsultarFacturas()
        {
            return CAdmFactura.AdmConsultarFacturas();
        }

        public EAdmFactura AdmGestionFactura(EAdmFactura pFactura)
        {
            return CAdmFactura.AdmGestionFactura(pFactura);
        }

        public List<EAdmAplicacion> AdmConsultarAplicaciones()
        {
            return CAdmAplicacion.AdmConsultarAplicaciones();
        }

        public EAdmClientes AdmGestionCliente(EAdmClientes pCliente)
        {
            return CAdmCliente.AdmGestionCliente(pCliente);
        }

        public List<EAdmClientes> AdmConsultarClientes()
        {
            return CAdmCliente.AdmConsultarClientes();
        }

        public string generarReciboPayphone(EAdmPago pago)
        {
            return CAdmPago.generarReciboPayphone(pago);
        }

        public EAdmPago AdmConsultaEstadoPago(string idPago)
        {
            return CAdmPago.AdmConsultaEstadoPago(Convert.ToInt32(idPago));
        }

        public string enviaEmail(EAdmEmail correo, string idPago)
        {
            return CAdmEmail.enviaEmail(correo, Convert.ToInt32(idPago));
        }

        public string encriptar(EAdmEmail datos)
        {
            return DGesEncriptacion.Encriptar(datos.Asunto);
        }

        public string desencriptar(EAdmEmail datos)
        {
            return DGesEncriptacion.Desencriptar(datos.Asunto);
        }

        public EAdmCatalogoBines MAdmConsultarCatalogoBines(string bin)
        {
            return CAdmCatalogoBines.MAdmConsultarCatalogoBines(bin);
        }

        public int AdmActualizarPago(string idPago)
        {
            return CAdmPago.AdmActualizarPago(Convert.ToInt32(idPago));
        }

        public EAdmPago AdmConsultaPagoInicio(string idPago)
        {
            return CAdmPago.AdmConsultaPagoInicio(Convert.ToInt32(idPago));
        }

        public async Task<string> AdmVerificarResultadoPago(ECliParametros parametros)
        {
            string resultado = await CAdmPagoWidget.AdmVerificarResultadoPago(parametros);
            return resultado;
        }

        public int AdmConsultaIntentosPago(string idPago)
        {
            return CAdmPago.AdmConsultaIntentosPago(Convert.ToInt32(idPago));
        }

        public EAdmAuxiliares AdmConsultaResumenPago(string fechaInicio, string fechaFin)
        {
            return CAdmPago.AdmConsultaResumenPago(fechaInicio, fechaFin);
        }

        public int AdmGestionUsuario(EAdmUsuario usuariop)
        {
            return CAdmUsuario.AdmGestionUsuario(usuariop);
        }

        public EAdmAnulacion AdmAnularPago(string idPago)
        {
            return CAdmPago.AdmAnularPago(Convert.ToInt32(idPago));
        }

        public EAdmAnulacion AdmExpirarPago(string idPago)
        {
            return CAdmPago.AdmExpirarPago(Convert.ToInt32(idPago));
        }

        public async Task<string> AdmVerificarPagoCliente(string clientId)
        {
            string resultado = await CAdmPagoPayphone.AdmVerificarPagoCliente(clientId);
            return resultado;
        }

        public List<EAdmAplicacion> AdmConsultarComboAplicaciones()
        {
            return CAdmAplicacion.AdmConsultarComboAplicaciones();
        }

        public EAdmGestion AdmGestionGestion(EAdmGestion pGestion)
        {
            return CAdmGestion.AdmGestionGestion(pGestion);
        }

        public string AdmGestionMasivos(EAdmAuxiliares auxiliares)
        {
            return CAdmMasivos.AdmGestionMasivos(auxiliares);
        }

        public EAdmHistorialTransacciones AdmGestionTransacciones(EAdmHistorialTransacciones pHistorial)
        {
            return CAdmHistorialTransacciones.AdmGestionTransacciones(pHistorial);
        }

        public List<EAdmHistorialTransacciones> AdmConsultarHistorialTransacciones(EAdmAuxiliares aux)
        {
            return CAdmHistorialTransacciones.AdmConsultarHistorialTransacciones(aux);
        }

        public string AdmConsultaRecibo(string idPago)
        {
            return CAdmPago.AdmConsultaRecibo(Convert.ToInt32(idPago));
        }

        public EAdmFactura AdmConsultaDetallePago(string idpago)
        {
            return CAdmPago.AdmConsultaDetallePago(Convert.ToInt32(idpago));
        }

        public EAdmPagoDiferidos AdmGestionPagoDiferidos(EAdmPagoDiferidos pPagosDiferidos)
        {
            return CAdmPagoDiferidos.AdmGestionPagoDiferidos(pPagosDiferidos);
        }

        public EAdmRecurrencia AdmGestionRecurrecia(EAdmRecurrencia pRecurrencia)
        {
            return CAdmRecurrencia.AdmGestionRecurrecia(pRecurrencia);
        }

        public List<EAdmPagoDiferidos> AdmConsultarPagosRecurrentes()
        {
            return CAdmPagoDiferidos.AdmConsultarPagosRecurrentes();
        }

        public async Task<EAdmPago> AdmRealizarPagoRecurrente(EAdmPagoDiferidos pago)
        {
            EAdmPago resultado = await CAdmPagoPayphone.AdmRealizarPagoRecurrente(pago);
            return resultado;
        }

        public string AdmGestionReEnvioMasivos(EAdmAuxiliares auxiliares)
        {
            return CAdmMasivos.AdmGestionReEnvioMasivos(auxiliares);
        }

        public EAdmPago AdmConsultarPagoCliente(string cedula, string certificado, string valor)
        {
            return CAdmPago.AdmConsultarPagoCliente(cedula, certificado, valor);
        }

        public string AdmObtenerCodigoAsegurado(string documento)
        {
            return CAdmMasivos.AdmObtenerCodigoAsegurado(documento);
        }

        public string AdmAplicarPago(EAdmAplicacionPago aux)
        {
            return CAdmMasivos.AdmAplicarPago(aux);
        }

        public string AdmGestionTokenPago(EAdmToken pToken)
        {
            return CAdmToken.AdmGestionTokenPago(pToken);
        }

        public string enviaEmailNormalReverso(EAdmEmail correo, string idPago)
        {
            return CAdmEmail.enviaEmailNormalReverso(correo, Convert.ToInt32(idPago));
        }

        public EAdmCatalogoBancos AdmGestionCatalogoBancos(EAdmCatalogoBancos pBanco)
        {
            return CAdmCatalogoBancos.AdmGestionCatalogoBancos(pBanco);
        }

        public List<EAdmCatalogoBancos> AdmConsultarCatalogoBancos()
        {
            return CAdmCatalogoBancos.AdmConsultarCatalogoBancos();
        }
    }
}
