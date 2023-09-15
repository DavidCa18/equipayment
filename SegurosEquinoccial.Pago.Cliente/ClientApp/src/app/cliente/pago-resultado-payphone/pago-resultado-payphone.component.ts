import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from './../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { HeaderComponent } from '../complementos/header/header.component';
import { Globales } from '../../variables/globales/globales.service';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { VariablesEmailDatafastTemplate } from '../../variables/email/email-datafast-template';

@Component({
  selector: 'app-pago-resultado-payphone',
  templateUrl: './pago-resultado-payphone.component.html',
  styleUrls: ['./pago-resultado-payphone.component.css']
})
export class PagoResultadoPayphoneComponent implements OnInit {

  @ViewChild('header', { read: ViewContainerRef }) entradaHeader: ViewContainerRef;
  public componenteHeaderRef: any;

  private datosPago: any;
  public parametros = {
    idPago: 0,
    idAplicacion: 0,
    transactionId: 0
  };
  public ip = "";
  public dtsClienteFactura: any = { "Cliente": { "Apellido": "", "Aplicacion": 0, "Email": "", "Estado": 0, "IdCliente": 0, "Identificacion": "", "Identificador": 0, "PrimerNombre": "", "SegundoNombre": "", "Telefono": "" }, "Comercio": "", "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": "0", "Numero": "0", "Subtotal0": "0", "Subtotal12": "0", "Total": "0", "UrlRetorno": null };
  public lstResultadoPago: any = { "email": "", "cardType": "", "bin": "", "lastDigits": "", "deferredCode": "", "deferred": false, "cardBrandCode": "", "cardBrand": "", "amount": 0, "clientTransactionId": "", "phoneNumber": "", "statusCode": 0, "transactionStatus": "", "authorizationCode": "", "messageCode": 0, "transactionId": 0, "document": "", "currency": "", "optionalParameter1": "", "optionalParameter2": "", "optionalParameter4": "", "storeName": "" };
  public estadoPago = { "Imagen": "", "Texto": "" };
  public urlPago = { Texto: "", Url: "" };

  public aplicacion = {
    LogoPrimario: "assets/images/logos/dark-vertical-logo.png",
    FondoPrimario: "#3A3A3A",
    ColorPrimario: "#FFFFFF"
  };

  variables: Globales = new Globales();
  globales: GlobalesPipe = new GlobalesPipe();
  email: VariablesEmailDatafastTemplate = new VariablesEmailDatafastTemplate();
  urlReintento = "0";
  estadoPagoVista = 0;

  constructor(private rutaActiva: ActivatedRoute, private router: Router, private conexion: ApiService,
    private resolver: ComponentFactoryResolver, private spinner: NgxSpinnerService) {
  }

  ngOnInit() {
    this.inicializarParametros();
    this.obtenerIP();
  }

  public generarComponenteHeader(logoPrimario, logoSecundario, colorPrimario, colorSecundario, fondoPrimario, fondoSecundario, logoPrimarioTamano, logoSecundarioTamano) {
    this.entradaHeader.clear();
    const factory = this.resolver.resolveComponentFactory(HeaderComponent);
    const componenteHeaderRef = this.entradaHeader.createComponent(factory);
    componenteHeaderRef.instance.logoPrimario = logoPrimario;
    componenteHeaderRef.instance.logoSecundario = logoSecundario;
    componenteHeaderRef.instance.fondoPrimario = fondoPrimario;
    componenteHeaderRef.instance.fondoSecundario = fondoSecundario;
    componenteHeaderRef.instance.colorPrimario = colorPrimario;
    componenteHeaderRef.instance.colorSecundario = colorSecundario;
    componenteHeaderRef.instance.logoPrimarioTamano = logoPrimarioTamano;
    componenteHeaderRef.instance.logoSecundarioTamano = logoSecundarioTamano;
  }

  public inicializarParametros() {
    this.datosPago = atob(this.rutaActiva.snapshot.params.id);
    var lstCadena = this.datosPago.split(",");
    if (this.verificarBase64(this.rutaActiva.snapshot.params.id) == false) {
      var codigo = 400;
      var aplicacion = 0;
      this.router.navigate(['/pago/informacion/' + btoa(codigo + "," + aplicacion)]);
    } else if (lstCadena.length != 3) {
      var codigo = 400;
      var aplicacion = 0;
      this.router.navigate(['/pago/informacion/' + btoa(codigo + "," + aplicacion)]);
    } else {
      this.parametros.idPago = lstCadena[0];
      this.parametros.idAplicacion = lstCadena[1];
      this.parametros.transactionId = lstCadena[2];
      this.buscarDatosAplicacion();
    }
  }

  public buscarDatosAplicacion() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.parametros.idAplicacion).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.aplicacion.LogoPrimario = res.LogoPrimario;
        this.aplicacion.FondoPrimario = res.FondoPrimario;
        this.aplicacion.ColorPrimario = res.ColorPrimario;
        setTimeout(() => { this.generarComponenteHeader(res.LogoPrimario, res.LogoSecundario, res.ColorPrimario, res.ColorSecundario, res.FondoPrimario, res.FondoSecundario, res.LogoPrimarioTamano, res.LogoSecundarioTamano); }, 100);
        this.buscarDatosClienteFactura();
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "buscarDatosAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.parametros.idAplicacion, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public buscarDatosClienteFactura() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/factura/buscar/datos/" + this.parametros.idPago).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.dtsClienteFactura = res;
        this.buscarDatosEstadoPago();
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "buscarDatosClienteFactura", "Gestion/SGesGestion.svc/pago/factura/buscar/datos/" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public buscarDatosEstadoPago() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/pago/estado/listar/" + this.parametros.idPago).subscribe(
      (res: any) => {
        this.spinner.hide();
        var estado = res.Estado;
        if (estado == 1) {
          this.verificarPago();
        } else {
          this.router.navigate(['/pago/informacion/' + btoa(res.ResultadoCodigo + "," + this.parametros.idAplicacion + "," + this.parametros.idPago)]);
        }
        // this.verificarPagoPrueba();
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "buscarDatosAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.parametros.idAplicacion, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }


  public verificarPagoPrueba() {
    this.lstResultadoPago = { "email": "mvasquez@tecniseguros.com.ec", "cardType": "Credit", "bin": "530054", "lastDigits": "3511", "deferredCode": "01011200", "deferredMessage": "12 Meses sin Intereses", "deferred": true, "cardBrandCode": "51", "cardBrand": "Mastercard Produbanco/Promerica", "amount": 69028, "clientTransactionId": "100826027120", "phoneNumber": "0991591940", "statusCode": 2, "transactionStatus": "Canceled", "message": "Fondos Insuficientes", "messageCode": 16, "transactionId": 2358358, "document": "0100300839", "currency": "USD", "optionalParameter1": "0991591940", "optionalParameter2": "mvasquez@tecniseguros.com.ec", "optionalParameter4": "NELLY HERRERA ZEAS", "storeName": "Seguros Equinoccial" };
    this.estadoPagoVista = this.obtenerEstado(this.lstResultadoPago.statusCode);
    this.gestionURLRetorno(this.dtsClienteFactura.UrlRetorno, this.lstResultadoPago.statusCode);
    this.actualizarPago(this.lstResultadoPago.transactionId, this.lstResultadoPago.authorizationCode, this.lstResultadoPago.deferredMessage, this.lstResultadoPago.statusCode, this.lstResultadoPago.transactionStatus, JSON.stringify(this.lstResultadoPago), this.lstResultadoPago.deferred);
  }

  public verificarPago() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/payphone/obtener/resultado?id=" + this.parametros.transactionId + "&idpago=" + this.parametros.idPago).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstResultadoPago = JSON.parse(res);
        this.estadoPagoVista = this.obtenerEstado(this.lstResultadoPago.statusCode);
        this.gestionURLRetorno(this.dtsClienteFactura.UrlRetorno, this.lstResultadoPago.statusCode);
        this.actualizarPago(this.lstResultadoPago.transactionId, this.lstResultadoPago.authorizationCode, this.lstResultadoPago.deferredMessage, this.lstResultadoPago.statusCode, this.lstResultadoPago.transactionStatus, res, this.lstResultadoPago.deferred);
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "verificarPago", "Gestion/SGesGestion.svc/pago/payphone/obtener/resultado/" + this.parametros.transactionId, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public actualizarPago(transactionId, authorizationCode, deferredMessage, statusCode, transactionStatus, trama, deferred) {

    var IdTransaccion = "";
    if (transactionId != undefined || transactionId != null) {
      var codigo = transactionId + "";
      IdTransaccion = codigo.substr(1, codigo.length);
    } else {
      IdTransaccion = "0000";
    }


    this.spinner.show();
    var Pago = {
      Identificador: 1,
      IdPago: this.parametros.idPago,
      Ip: this.ip,
      Plataforma: "PAYPHONE",
      IdTransaccion: transactionId == null || transactionId == undefined ? "0000" : transactionId,
      CodigoAutenticacion: authorizationCode == null || authorizationCode == undefined ? "0000" : authorizationCode,
      Referencia: IdTransaccion,
      Lote: IdTransaccion,
      Voucher: IdTransaccion + "-" + IdTransaccion,
      ParametroPersonalizado: "",
      NumeroDiferidos: deferred == true ? deferredMessage : "Corriente",
      ResultadoCodigo: statusCode,
      ResultadoTexto: transactionStatus,
      ResultadoTrama: trama,
      Estado: this.obtenerEstado(statusCode),
      Intereses: 0,
      RespuestaAdquiriente: 0,
      Factura: {
        IdFactura: 0,
        Iva: 0,
        Subtotal12: 0,
        Subtotal0: 0
      },
      Bin: "",
      Digitos: "",
      Apoderado: "",
      Marca: "",
      Banco: "",
      Gracia: "",
    };

    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/gestion", Pago).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.verificarMensaje(statusCode, this.obtenerEstado(statusCode), transactionId, trama);
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "actualizarPago", "Gestion/SGesGestion.svc/pago/pago/gestion", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        this.verificarMensaje(statusCode, this.obtenerEstado(statusCode), transactionId, trama);
      }
    );
  }

  public verificarMensaje(codigo, estado, autorizacion, trama) {
    var validacionEmail = this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email);

    this.conexion.get("Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo).subscribe(
      (res: any) => {
        this.estadoPago.Imagen = res.Imagen;
        this.estadoPago.Texto = res.Texto;

        if (estado == 2) {
          if (validacionEmail.length == 1) {
            setTimeout(() => {
              window.open(this.urlPago.Url, "_self");
            }, 5000);
          } else {
            this.enviarEmailAsesor(trama, autorizacion, estado);
          }
        }
      },
      err => {
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "verificarMensaje", "Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public enviarEmailAsesor(trama, autorizacion, estado) {

    var tarjeta = JSON.parse(trama);

    var Email = {
      "Para": this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email)[1].trim(),
      "Asunto": this.variables.ambiente == "PRODUCCION" ? "Confirmación de pago" : "[MODO PRUEBA]-Confirmación de pago",
      "Mensaje": this.email.generarEmailAsesor(tarjeta.optionalParameter4, autorizacion, new Date(), this.dtsClienteFactura.Total, estado)
    }

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/enviar/pago/recibo/" + this.parametros.idPago, Email).subscribe(
      (res: any) => {
        this.spinner.hide();
        if (estado == 2) {
          setTimeout(() => {
            window.open(this.urlPago.Url, "_self");
          }, 5000);
        }
        console.log(res);
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "enviarEmail", "Gestion/SGesGestion.svc/enviar/pago/recibo/" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public gestionURLRetorno(url, codigo) {
    if (url == "0") {
      this.urlPago.Texto = "Finalizar Transacción";
      var codigo = codigo;
      var aplicacion = this.parametros.idAplicacion;
      this.urlPago.Url = "/pago/informacion/" + btoa(codigo + "," + aplicacion + "," + this.parametros.idPago);
    } else if (url != "0") {
      this.urlPago.Texto = "Continuar";
      this.urlPago.Url = url;
    }
  }

  public verificarIntentos() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/verificar/intentos/" + this.parametros.idPago).subscribe(
      (res: any) => {
        this.spinner.hide();
        if (res == 3) {
          window.open(this.urlPago.Url, "_self");
        } else {
          this.reintentarPago();
        }

      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado-payphone.component.ts", "verificarIntentos", "Gestion/SGesGestion.svc/pago/verificar/intentos/" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public reintentarPago() {

    var Pago = {
      Identificador: 4,
      IdPago: this.parametros.idPago,
      Ip: "",
      Plataforma: this.dtsClienteFactura.Numero,
      IdTransaccion: "",
      CodigoAutenticacion: "",
      Referencia: "",
      Lote: "",
      Voucher: "",
      ParametroPersonalizado: "",
      NumeroDiferidos: "",
      ResultadoCodigo: "",
      ResultadoTexto: "",
      ResultadoTrama: "",
      Estado: 0,
      Intereses: "",
      RespuestaAdquiriente: "",
      Factura: {
        IdFactura: this.dtsClienteFactura.IdFactura,
        Iva: 0,
        Subtotal12: 0,
        Subtotal0: 0
      },
      Bin: "",
      Digitos: "",
      Apoderado: "",
      Marca: "",
      Banco: "",
      Gracia: "",
    };

    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/gestion", Pago).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.urlReintento = this.variables.obtenerCredenciales().conexionAplicacion + "?c=" + btoa(this.parametros.idPago + "") + "&p=" + this.parametros.idAplicacion;
        window.open(this.urlReintento, "_self");
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "reintentarPago", "Gestion/SGesGestion.svc/pago/pago/gestion", err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  //METODOS COMPLEMENTARIOS

  public convertirNumerico(numero) {
    return numero * 0.01;
  }

  public verificarBase64(dato) {
    var expresionBase64 = /^([0-9a-zA-Z+/]{4})*(([0-9a-zA-Z+/]{2}==)|([0-9a-zA-Z+/]{3}=))?$/;
    var base64 = expresionBase64.test(dato);
    if (base64 == true) {
      return true;
    } else {
      return false;
    }
  }

  public obtenerFecha(separador: any) {
    var fecha = new Date();
    var anio = fecha.getFullYear();
    var mes: any = fecha.getMonth() + 1;
    var dia: any = fecha.getDate();

    mes < 10 ? mes = "0" + mes : mes;
    dia < 10 ? dia = "0" + dia : dia;

    return anio + separador + mes + separador + dia;
  }

  public obtenerIP() {
    this.spinner.show();
    this.conexion.getIP("?format=json").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.ip = res.ip;
      },
      err => {
        this.spinner.hide();
        console.log(err);
      }
    );
  }

  public obtenerEstado(valor) {
    var estado = 0;
    if (valor == "3") {
      estado = 2;
    } else {
      estado = 3;
    }
    return estado;
  }

}
