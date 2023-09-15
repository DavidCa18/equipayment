import { Component, OnInit, ViewChild, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { Router } from '@angular/router';
import { HeaderComponent } from '../complementos/header/header.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { Globales } from '../../variables/globales/globales.service';
import { VariablesEmailDatafastTemplate } from '../../variables/email/email-datafast-template';
import { GlobalesPipe } from '../../metodos/globales.pipe';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-pago-resultado',
  templateUrl: './pago-resultado.component.html',
  styleUrls: ['./pago-resultado.component.css']
})
export class PagoResultadoComponent implements OnInit {

  /*Nuevos Parametros*/

  public variables: Globales = new Globales();
  public globales: GlobalesPipe = new GlobalesPipe();
  public email: VariablesEmailDatafastTemplate = new VariablesEmailDatafastTemplate();

  public url: any;
  public parametros = {
    idPago: 0,
    idAplicacion: 0,
    idTransaccion: 0,
    id: "",
    resourcePath: "",
    card: "",
    cuota: 0,
    diferidos: 0,
    banco: "",
    gracia: "",
    recurrencia: 0
  };

  public dtsAplicacionClienteFactura = { "Cliente": { "Apellido": "", "Aplicacion": { "Recurrencia": 0, "Caducidad": null, "Codigo": 0, "ColorPrimario": "#000", "ColorSecundario": "#000", "Estado": 0, "FondoPrimario": "#ddd", "FondoSecundario": "#ddd", "IdAplicacion": 0, "Identificacion": "", "Identificador": 0, "LogoPrimario": "", "LogoPrimarioTamano": "", "LogoSecundario": "", "LogoSecundarioTamano": "", "MontoMaximo": 0, "MontoMinimo": 0, "Nombre": "", "Token": null, "VisualizacionBin": 0, "Gracia": 0 }, "Email": "", "Estado": 0, "IdCliente": 0, "Identificacion": "", "Identificador": 0, "Ip": null, "NombreCompleto": null, "Numero": "", "PrimerNombre": "", "SegundoNombre": "", "Telefono": "" }, "Comercio": "", "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": "", "Numero": "", "Subtotal0": "", "Subtotal12": "", "Total": "", "UrlRetorno": "" };
  public dtsPago = { "AnioExpiracionTarjeta": null, "Apoderado": "", "Bin": "", "CodigoAutenticacion": "0", "CodigoVerificacionTarjeta": null, "Diferido": null, "Digitos": "", "Estado": 0, "Factura": { "Cliente": null, "Comercio": null, "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": null, "Numero": null, "Subtotal0": null, "Subtotal12": null, "Total": "", "UrlRetorno": "" }, "FechaIngreso": null, "FechaTransaccion": "", "IdPago": 0, "IdTransaccion": "", "Identificador": 0, "Intereses": "", "Ip": "", "Link": null, "Lote": "", "Marca": "", "MesExpiracionTarjeta": null, "NombreTarjeta": null, "NumeroDiferidos": "6", "NumeroTarjeta": null, "ParametroPersonalizado": "", "Plataforma": "", "Recibo": "", "Referencia": "", "RespuestaAdquiriente": "", "ResultadoCodigo": "", "ResultadoTexto": "", "ResultadoTrama": "", "Voucher": "" };
  public dtsMensaje = { "Codigo": "", "Descripcion": "", "Estado": 0, "IdMensaje": 0, "Identificador": 0, "Imagen": "assets/images/states/warning.png", "Plataforma": 0, "Texto": "Transacción Pendiente" };

  public reintento = 0;

  public loading = {
    load: true,
    form: false
  }

  constructor(private router: Router, private conexion: ApiService, private resolver: ComponentFactoryResolver) {
    $(document).keydown(function (event) {
      switch (event.keyCode) {
        case 116: event.returnValue = false; event.keyCode = 0; return false;
        case 82: if (event.ctrlKey) { event.returnValue = false; event.keyCode = 0; return false; }
      }
    });
  }

  ngOnInit() {

    this.url = this.router.parseUrl(this.router.url);
    console.log(this.url.queryParams['datos']);
    var datos = JSON.parse(atob(this.url.queryParams['datos']));

    this.parametros.idPago = datos.idPago;
    this.parametros.idAplicacion = datos.idAplicacion;
    this.parametros.idTransaccion = datos.idTransaccion;
    this.parametros.id = this.url.queryParams['id'];
    this.parametros.resourcePath = this.url.queryParams['resourcePath'];
    this.parametros.card = datos.card;
    this.parametros.cuota = datos.cuota;
    this.parametros.diferidos = datos.diferidos;
    this.parametros.banco = datos.banco;
    this.parametros.gracia = datos.gracia;
    this.parametros.recurrencia = datos.recurrencia;

    console.log(this.parametros);
    this.listarAplicacionClienteFactura();
  }

  public listarAplicacionClienteFactura() {
    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/cliente/listar/detalle/" + this.parametros.idPago).subscribe(
      (res: any) => {
        this.spinner(false);
        this.dtsAplicacionClienteFactura = res;
        console.log(this.dtsAplicacionClienteFactura);
        this.listarPago();
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "listarAplicacionClienteFactura", "Gestion/SGesGestion.svc/cliente/listar/detalle/" + this.parametros.idAplicacion, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public listarPago() {
    var parametros = {
      ResourcePath: this.parametros.resourcePath,
      IdPago: this.parametros.idPago,
      IdTransaccion: this.parametros.idTransaccion,
      Iva: this.variables.ambiente == "PRODUCCION" ? this.dtsAplicacionClienteFactura.Iva : "0.12",
      Subtotal12: this.variables.ambiente == "PRODUCCION" ? this.dtsAplicacionClienteFactura.Subtotal12 : "1.00",
      Subtotal0: this.variables.ambiente == "PRODUCCION" ? this.dtsAplicacionClienteFactura.Subtotal0 : "2.00",
      IdAplicacion: this.dtsAplicacionClienteFactura.Cliente.Aplicacion.Gracia,
      Banco: this.parametros.banco,
      Gracia: this.parametros.gracia
    }

    var url = (parametros.IdTransaccion == 0 ? "Gestion/SGesGestion.svc/pago/datafast/obtener/resultado" : "Gestion/SGesGestion.svc/pago/payphone/obtener/resultado");

    this.spinner(true);
    this.conexion.post(url, parametros).subscribe(
      (res: any) => {

        this.spinner(false);
        console.log(res);
        this.dtsPago = res;
        var verificacion = this.verificarResultado(parametros.IdTransaccion, this.dtsPago.ResultadoCodigo, this.dtsPago.Estado, this.dtsPago.RespuestaAdquiriente);
        this.reintento = verificacion.reintento;

        if (parametros.IdTransaccion == 0) {
          this.actualizarToken(verificacion.codigo);
        } else {
          this.listarMensaje(verificacion.codigo);
        }

      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "listarPago", url + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public actualizarToken(codigo) {
    /*var Pago = {
      Identificador: 1,
      IdCliente: this.dtsAplicacionClienteFactura.Cliente.IdCliente,
      Token: this.globales.token()
    }*/

    var Pago = {
      Identificador: 1,
      IdToken: 0,
      Cliente: {
        IdCliente: this.dtsAplicacionClienteFactura.Cliente.IdCliente
      },
      Token: this.globales.token(),
      Marca: "",
      Banco: this.parametros.banco
    }

    this.spinner(true);
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/token", Pago).subscribe(
      (res: any) => {
        this.spinner(false);
        this.listarMensaje(codigo);
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "actualizarToken", "Gestion/SGesGestion.svc/pago/pago/token" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public listarMensaje(codigo) {
    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo).subscribe(
      (res: any) => {
        this.spinner(false);
        this.dtsMensaje = res;
        if (this.dtsPago.Estado == 2) {
          this.enviarCorreoElectronico();
        }
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "verificarMensaje", "Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public enviarCorreoElectronico() {

    var msmProduccion = (this.dtsPago.Plataforma == "DATAFAST" ? "Confirmación de pago Datafast" : "Confirmación de pago Payphone");
    var msmPrueba = (this.dtsPago.Plataforma == "DATAFAST" ? "[MODO PRUEBA] - Confirmación de pago Datafast" : "[MODO PRUEBA] - Confirmación de pago Payphone");
    var cntNormal = this.email.generarEmail(this.dtsPago.Apoderado, this.dtsPago.CodigoAutenticacion, this.globales.obtenerFechaCompleta(""), this.dtsAplicacionClienteFactura.Total, this.dtsPago.Estado);
    var cntRecurrencia = this.email.generarEmailRecurrente(this.dtsPago.Apoderado, this.dtsPago.CodigoAutenticacion, this.globales.obtenerFechaCompleta(""), this.dtsAplicacionClienteFactura.Total, this.parametros.cuota, this.parametros.diferidos);

    var Email = {
      "Para": this.dtsAplicacionClienteFactura.Cliente.Email,
      "Asunto": this.variables.ambiente == "PRODUCCION" ? msmProduccion : msmPrueba,
      "Mensaje": this.dtsAplicacionClienteFactura.Cliente.Aplicacion.Recurrencia == 1 ? cntRecurrencia : cntNormal
    }

    this.spinner(true);
    this.conexion.post("Gestion/SGesGestion.svc/enviar/pago/recibo/" + this.parametros.idPago, Email).subscribe(
      (res: any) => {
        this.spinner(false);
        if (this.dtsPago.Estado == 2) {
          this.continuarUrl();
        }
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "enviarCorreoElectronico", "Gestion/SGesGestion.svc/enviar/pago/recibo/" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public verificarIntentos() {
    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/pago/verificar/intentos/" + this.parametros.idPago).subscribe(
      (res: any) => {
        console.log(res)
        this.spinner(false);
        if (res >= 4) {
          this.globales.notificacion("Ha superado el límite de reintentos.", "warning", "top-right", "#D4AC0D", "#FFFFFF");
          this.continuarUrl();
        } else {
          this.reintentarPago();
        }
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "reintentarPago", "Gestion/SGesGestion.svc/pago/actualizar/estado/" + this.parametros.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public reintentarPago() {
    var Pago = {
      Identificador: 4,
      IdPago: this.parametros.idPago,
      Ip: "",
      Plataforma: this.dtsAplicacionClienteFactura.Numero,
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
        IdFactura: this.dtsAplicacionClienteFactura.IdFactura,
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
    this.spinner(true);
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/gestion", Pago).subscribe(
      (res: any) => {
        this.spinner(false);
        this.globales.notificacion("Se está redirigiendo al formulario de pago.", "success", "top-right", "#CB4335", "#FFFFFF");
        var url = this.variables.obtenerCredenciales().conexionAplicacion + "?c=" + btoa(this.parametros.idPago + "") + "&p=" + this.parametros.idAplicacion;
        window.open(url, "_self");
      },
      err => {
        this.spinner(false);
        console.log(err);
        this.conexion.error("pago-resultado.component.ts", "reintentarPago", "Gestion/SGesGestion.svc/pago/pago/gestion", err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  /* Métodos Complementarios Actuales */
  public spinner(parametro) {
    if (parametro == true) {
      this.loading.load = true;
      this.loading.form = false;
    } else if (parametro == false) {
      this.loading.load = false;
      this.loading.form = true;
    }
  }

  public verificarResultado(tipo, codigo, estado, adquiriente) {
    var parametros = {
      reintento: 0,
      codigo: ''
    };

    if (tipo == 0) {
      if (estado == 2) {
        parametros.codigo = codigo;
      } else {
        if (adquiriente == '02') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '03') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '04') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '05') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '07') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '12') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '13') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '14') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '17') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '88') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '19') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '41') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '43') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '51') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '54') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '57') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '91') {
          parametros.codigo = adquiriente;
          parametros.reintento = 1;
        } else if (adquiriente == '89') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '83') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == '77') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == 'FORMAT_ERROR') {
          parametros.codigo = adquiriente;
        } else if (adquiriente == 'NULL_RESPONSE') {
          parametros.codigo = adquiriente;
        } else {
          parametros.codigo = codigo;
        }
      }
    } else {
      parametros.codigo = codigo;
    }

    return parametros;
  }

  public marcaTarjeta(bin, digitos) {

    var imagenTarjeta = 'fas fa-credit-card';
    var numeroTarjeta = '';

    if (bin == "") {
      imagenTarjeta = 'fas fa-credit-card';
      numeroTarjeta = 'XXXX XXXX XXXX XXXX';
    } else if (digitos == "") {
      imagenTarjeta = 'fas fa-credit-card';
      numeroTarjeta = 'XXXX XXXX XXXX XXXX';
    } else {

      var digito1 = bin.substr(0, 1);
      var digito2 = bin.substr(0, 2);
      var digito3 = bin.substr(0, 3);
      var digito4 = bin.substr(0, 4);

      if (digito1 == "4") {
        imagenTarjeta = 'fab fa-cc-visa';
        numeroTarjeta = bin + 'XXXXXX' + digitos;
      }

      if (digito2 == "34" || digito2 == "37") {
        imagenTarjeta = 'fab fa-cc-amex';
        numeroTarjeta = bin + 'XXXXX' + digitos;
      } else if (digito2 == "51" || digito2 == "52" || digito2 == "53" || digito2 == "54" || digito2 == "55") {
        imagenTarjeta = 'fab fa-cc-mastercard';
        numeroTarjeta = bin + 'XXXXXX' + digitos;
      } else if (digito2 == "64" || digito2 == "65") {
        imagenTarjeta = 'fab fa-cc-discover';
        numeroTarjeta = bin + 'XXXXXX' + digitos;
      } else if (digito2 == "36" || digito2 == "38") {
        imagenTarjeta = 'fab fa-cc-diners-club';
        numeroTarjeta = bin + 'XXXX' + digitos;
      }

      if (digito3 == "305") {
        imagenTarjeta = 'fab fa-cc-diners-club';
        numeroTarjeta = bin + 'XXXX' + digitos;
      }

      if (digito4 == "6011") {
        imagenTarjeta = 'fab fa-cc-discover';
        numeroTarjeta = bin + 'XXXXXX' + digitos;
      }
    }

    return { icon: imagenTarjeta, card: numeroTarjeta };
  }

  public continuarUrl() {
    if (this.dtsAplicacionClienteFactura.UrlRetorno != "0") {
      this.globales.notificacion("Estas siendo redireccionado.", "success", "top-right", "#27AE60", "#FFFFFF");
      setTimeout(() => { window.open(this.dtsAplicacionClienteFactura.UrlRetorno, "_self"); }, 4500);
    } else {
      //setTimeout(() => { window.open("https://www.google.com", "_self"); }, 4500);
    }
  }

}
