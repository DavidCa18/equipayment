import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { Globales } from '../../variables/globales/globales.service';

declare var $: any;
declare var moment: any;
declare var CryptoJS: any;

@Component({
  selector: 'app-pago-boton-payphone',
  templateUrl: './pago-boton-payphone.component.html',
  styleUrls: ['./pago-boton-payphone.component.css']
})
export class PagoBotonPayphoneComponent implements OnInit {

  @Input() parametros: any = {};
  @Input() dtsClienteFactura: any = {};
  @Input() dtsAplicacion: any = {};
  @Input() banco = "";
  @Input() gracia = "";
  @Input() vistaGracia = 0;

  public estado = true;

  public tarjeta = {
    Numero: "",
    Expiracion: "",
    CVV: "",
    Titular: "",
    Diferidos: ""
  }

  public validacionTarjeta = {
    Numero: false,
    mesExpiracion: false,
    anioExpiracion: false,
    CVV: false,
    Titular: false,
    Diferidos: false
  }

  public diferidos: any;

  public estadoDiferidos = false;

  public alertas = {
    mensaje: { texto: "", color: "", estado: false, spinner: false }
  };

  public errores: any = { mensaje: "", errores: [{ "message": "", "errorCode": 0, "errorDescriptions": [] }] };
  public botonVerdad = true;
  public botonInterfaz = false;
  globales: GlobalesPipe = new GlobalesPipe();
  constructor(private conexion: ApiService, private router: Router, public global: Globales) { }

  ngOnInit() {
    this.obtenerTitular();
    console.log(this.banco);
  }

  public obtenerCodigoDiferidos(dif) {

    var inicio = "PS0101";
    var fin = "00";

    var medio = dif < 10 ? "0" + dif : dif;

    var diferido = inicio + medio + fin;

    return dif == 0 ? "00000000" : diferido;
  }

  /*public obtenerCodigosDiferidoGracia() {

    const obtenerCodigos = (numero, gracia, meses) => {
      var inicio = "PS01" + gracia;
      var medio = numero < 10 ? "0" + numero : numero;
      var fin = meses;
      var diferido = inicio + medio + fin;

      return numero == 0 ? "00000000" : diferido;
    }

    this.diferidos = [];
    var parametro = this.parametros.opciones;
    var opciones = parametro.split(",");
    for (let i = 0; i < opciones.length; i++) {
      this.diferidos.push({
        code: obtenerCodigos(opciones[i], "02", "02"),
        name: (
          opciones[i] == 0 ? "Corriente" :
            opciones[i] == 3 ? "3 Meses Sin Intereses 2 Meses de Gracia" :
              opciones[i] == 6 ? "6 Meses Sin Intereses 2 Meses de Gracia" :
                opciones[i] == 9 ? "9 Meses Sin Intereses 2 Meses de Gracia" :
                  opciones[i] == 12 ? "12 Meses Sin Intereses 2 Meses de Gracia" : "Diferido Incorrecto")
      });
    }
    for (let i = 0; i < opciones.length; i++) {
      this.diferidos.push({
        code: obtenerCodigos(opciones[i], "01", "00"),
        name: (
          opciones[i] == 0 ? "Corriente" :
            opciones[i] == 3 ? "3 Meses Sin Intereses" :
              opciones[i] == 6 ? "6 Meses Sin Intereses" :
                opciones[i] == 9 ? "9 Meses Sin Intereses" :
                  opciones[i] == 12 ? "12 Meses Sin Intereses" : "Diferido Incorrecto")
      });
    }

    const elimina = (array,elem) => {
      return array.filter(e=> e.code!==elem.code);
    }

    console.log(elimina(this.diferidos,{code:'00000000'}));
    console.log(this.diferidos);
  }*/

  public obtenerTitular() {
    const removerAcentos = (str) => {
      return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
    }

    var titular = (this.dtsClienteFactura.Cliente.PrimerNombre.trim() + " " + this.dtsClienteFactura.Cliente.SegundoNombre.trim() + " " + this.dtsClienteFactura.Cliente.Apellido.trim()).trim();
    this.tarjeta.Titular = removerAcentos(titular.replace(/\s+/g, ' '));
  }

  public obtenerDiferidos() {
    if (this.tarjeta.Numero.length == 16 && this.banco == "Internacional" && this.dtsAplicacion.Recurrencia == 0) {
      this.diferidos = [{ code: "00000000", name: "Corriente" }];
    } else if (this.tarjeta.Numero.length == 16 && this.banco == "Otra Entidad Financiera" && this.dtsAplicacion.Recurrencia == 0) {
      this.diferidos = [{ code: "00000000", name: "Corriente" }];
    } else if (this.tarjeta.Numero.length == 16 && this.parametros.diferidos == undefined && this.dtsAplicacion.Recurrencia == 0) {
      this.listarDiferidos();
    } else if (this.tarjeta.Numero.length == 16 && this.parametros.diferidos == "corriente" && this.dtsAplicacion.Recurrencia == 0) {
      this.diferidos = [{ code: "00000000", name: "Corriente" }];
    } else if (this.tarjeta.Numero.length == 16 && this.parametros.diferidos == "especial" && this.dtsAplicacion.Recurrencia == 0) {

      var parametro = this.parametros.opciones;
      var opciones = parametro.split(",");

      this.diferidos = [];
      for (let i = 0; i < opciones.length; i++) {
        this.diferidos.push({
          code: this.obtenerCodigoDiferidos(opciones[i]),
          name: (
            opciones[i] == 0 ? "Corriente" :
              opciones[i] == 3 ? "3 Meses Sin Intereses" :
                opciones[i] == 6 ? "6 Meses Sin Intereses" :
                  opciones[i] == 9 ? "9 Meses Sin Intereses" :
                    opciones[i] == 12 ? "12 Meses Sin Intereses" : "Diferido Incorrecto")
        });
      }
    } else if (this.tarjeta.Numero.length == 16 && this.parametros.diferidos == "gracia" && this.dtsAplicacion.Recurrencia == 0) {
      this.listarDiferidos();
    } else {
      if (this.dtsAplicacion.Recurrencia == 1) {
        this.diferidos = [];
        this.diferidos.push(
          { code: "1", name: "Corriente" },
          { code: "3", name: "3 Meses Sin Intereses" },
          { code: "6", name: "6 Meses Sin Intereses" },
          { code: "9", name: "9 Meses Sin Intereses" },
          { code: "12", name: "12 Meses Sin Intereses" },
        );
      }
    }
  }

  public listarDiferidos() {

    var Pago = {
      NumeroTarjeta: btoa(this.tarjeta.Numero),
      Gracia: this.gracia,
      Factura: {
        Cliente: {
          Aplicacion: {
            IdAplicacion: this.dtsAplicacion.Codigo,
            Gracia: this.dtsAplicacion.Gracia
          }
        }
      }
    }

    this.gestionAlerta("Obteniendo Diferidos, Espere Por Favor...", "info", true, true);
    this.conexion.post("Gestion/SGesGestion.svc/pago/payphone/obtener/diferidos", Pago).subscribe(
      (res: any) => {
        this.gestionAlerta("", "info", false, false);
        var diferidos = JSON.parse(res);
        this.diferidos = diferidos;
      },
      err => {
        this.gestionAlerta("Error al obtener diferidos: Error en el servidor de datos, payphone/obtener/diferidos", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "obtenerDiferidos", "Gestion/SGesGestion.svc/pago/payphone/obtener/diferidos", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public realizarPago() {

    //this.realizarPagoDiferidos();
    if (this.dtsAplicacion.Recurrencia == 1 && this.parametros.recurrecia == 1) {
      this.realizarPagoDiferidos();
    } else {
      this.realizarPagoNormal();
    }
  }

  public realizarPagoNormal() {
    this.gestionAlerta("Realizando el pago, Espere Por Favor...", "info", true, true);

    var Pago = this.generarJsonPago();

    this.botonVerdad = false;
    this.botonInterfaz = true;
    this.conexion.post("Gestion/SGesGestion.svc/pago/payphone/generar/pago", Pago).subscribe(
      (res: any) => {
        console.log(res);
        var pago = JSON.parse(res);

        this.guardarToken(pago);
      },
      err => {
        this.botonVerdad = true;
        this.botonInterfaz = false;
        this.gestionAlerta("Error al realizar el pago: Error en el servidor de datos, pago/payphone/generar/pago", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "realizarPago", "Gestion/SGesGestion.svc/pago/payphone/generar/pago", err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public realizarPagoDiferidos() {

    var Pago = this.generarJsonPago();

    this.gestionAlerta("Realizando el pago, Espere Por Favor...", "info", true, true);
    this.botonVerdad = false;
    this.botonInterfaz = true;

    this.conexion.post("Gestion/SGesGestion.svc/pago/payphone/generar/pago", Pago).subscribe(
      (res: any) => {
        var pago = JSON.parse(res);
        console.log(pago);

        if (pago.transactionId == undefined) {

          this.gestionAlerta("", "info", false, false);
          this.botonVerdad = true;
          this.botonInterfaz = false;
          console.log(pago.errorDescriptions);
          if (pago.errors == undefined || pago.errors == null) {
            this.errores = ({ mensaje: pago.message, errores: [{ "message": "Error Pago", "errorCode": 127, "errorDescriptions": ['El cliente se encuentra registrado en listas negras.'] }] });
          } else {
            this.errores = ({ mensaje: pago.message, errores: pago.errors });
          }

          $('#AlertErrores').modal('toggle');

          this.conexion.error("pago-boton-payphone.component.ts", "realizarPago", "Gestion/SGesGestion.svc/pago/payphone/generar/pago", "500", "/validaciones", res, 0, "VALIDACIONES PAYPHONE");

        } else {
          this.guardarPagoDiferidos(pago);
        }

      },
      err => {
        this.botonVerdad = true;
        this.botonInterfaz = false;
        this.gestionAlerta("Error al realizar el pago: Error en el servidor de datos, pago/payphone/generar/pago", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "realizarPagoDiferidos", "Gestion/SGesGestion.svc/pago/payphone/generar/pago", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public guardarPagoDiferidos(pPago) {
    this.gestionAlerta("Gestionando Diferidos, Espere Por Favor...", "info", true, true);
    var Diferidos = this.obtenerDiferidosPago();

    var Datos = {
      Identificador: 1,
      IdPagoDiferidos: 0,
      Token: pPago.cardToken,
      Holder: this.encriptarHolderTarjeta(),
      Telefono: this.dtsClienteFactura.Cliente.Telefono,
      Email: this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email)[0],
      Identificacion: this.dtsClienteFactura.Cliente.Identificacion,
      JSON: Diferidos.Query,
      IdPago: 0
    };

    this.botonVerdad = false;
    this.botonInterfaz = true;
    this.conexion.post("Gestion/SGesGestion.svc/pago/diferidos/gestion", Datos).subscribe(
      (res: any) => {
        console.log(res);
        this.guardarRecurrencia(pPago);
      },
      err => {
        this.botonVerdad = true;
        this.botonInterfaz = false;
        this.gestionAlerta("Error al repartir diferidos mensuales", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "guardarPagoDiferidos", "Gestion/SGesGestion.svc/pago/diferidos/gestion", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public guardarRecurrencia(pPago) {

    var opciones: any = this.obtenerDiferidosPago();


    var Pago = {
      Identificador: 1,
      IdToken: 0,
      Cliente: {
        IdCliente: this.dtsClienteFactura.Cliente.IdCliente
      },
      Token: pPago.cardToken == undefined ? "" : pPago.cardToken,
      Marca: pPago.cardBrand == undefined ? "" : pPago.cardBrand,
      Banco: this.banco
    }

    this.gestionAlerta("Realizando el pago, Espere Por Favor...", "info", true, true);
    this.botonVerdad = false;
    this.botonInterfaz = true;

    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/token", Pago).subscribe(
      (res: any) => {

        console.log(res);

        var pago = pPago;

        var parametros = { idPago: this.parametros.pago, idAplicacion: this.parametros.plataforma, idTransaccion: pago.transactionId, card: pago.cardToken, cuota: opciones.Primera, diferidos: opciones.Cuotas, banco: this.banco, gracia: this.gracia, recurrencia: this.parametros.recurrencia };
        var datos = "?datos=" + btoa(JSON.stringify(parametros)) + "&id=0&resourcePath=0";

        if (pago.transactionId == undefined) {

          this.gestionAlerta("", "info", false, false);
          this.botonVerdad = true;
          this.botonInterfaz = false;
          console.log(pago.errorDescriptions);
          if (pago.errors == undefined || pago.errors == null) {
            this.errores = ({ mensaje: pago.message, errores: [{ "message": "Error Pago", "errorCode": 127, "errorDescriptions": ['El cliente se encuentra registrado en listas negras.'] }] });
          } else {
            this.errores = ({ mensaje: pago.message, errores: pago.errors });
          }

          $('#AlertErrores').modal('toggle');

          this.conexion.error("pago-boton-payphone.component.ts", "realizarPago", "Gestion/SGesGestion.svc/pago/payphone/generar/pago", "500", "/validaciones", res, 0, "VALIDACIONES PAYPHONE");

        } else {
          window.open(this.global.obtenerCredenciales().conexionResultDatafast + datos, "_self");
        }

      },
      err => {
        console.log(err);
        this.botonVerdad = true;
        this.botonInterfaz = false;
        this.gestionAlerta("Error al guardar los datos para el pago recurrente, pago/payphone/generar/pago", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "guardarRecurrencia", "estion/SGesGestion.svc/pago/recurrecia/gestion", err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public obtenerDiferidosPago() {

    var Dinero = [];

    if (this.dtsAplicacion.Recurrencia == 1) {
      if (this.tarjeta.Diferidos != "1") {

        var TotalFactura = parseFloat(this.dtsClienteFactura.Total);
        var Diferidos = parseInt(this.tarjeta.Diferidos);

        var TotalDiferidos = Math.round((TotalFactura / Diferidos) * 100) / 100;
        var ListaTotalDiferidos = [];
        var TotalObtenidoFactura = Math.round((TotalDiferidos * Diferidos) * 100) / 100;

        for (let i = 0; i < Diferidos; i++) {
          ListaTotalDiferidos.push(TotalDiferidos);
        }

        var DiferenciaTotal = Math.round((TotalFactura - TotalObtenidoFactura) * 100) / 100;

        var UltimoDiferido = Math.round((ListaTotalDiferidos[Diferidos - 1] + (DiferenciaTotal)) * 100) / 100;

        ListaTotalDiferidos[Diferidos - 1] = UltimoDiferido;

        var TotalFinal = 0;
        for (let i = 0; i < Diferidos; i++) {
          TotalFinal += ListaTotalDiferidos[i];
        }

        if ((Math.round(TotalFactura * 100) / 100) == (Math.round(TotalFinal * 100) / 100)) {

          for (let i = 0; i < ListaTotalDiferidos.length; i++) {
            var Valores = this.globales.calculoValoresPagar(ListaTotalDiferidos[i]);
            var Numero = this.dtsAplicacion.IdAplicacion + "" + this.dtsClienteFactura.IdFactura + "" + this.globales.obtenerHora("") + (i + 1);
            Dinero.push({ "Numero": Numero, "Subtotal12": this.redondear(Valores.subtotal), "Subtotal0": this.redondear("0.00"), "Iva": this.redondear(Valores.iva), "Total": this.redondear(Valores.total), "IdFactura": this.dtsClienteFactura.IdFactura, "Estado": (i == 0 ? 1 : 0), "IdPago": (i == 0 ? this.parametros.pago : 0), "Fecha": moment().add(i, "months").format("YYYY-MM-DD") });
          }

        }

      } else {
        var Numero = this.dtsAplicacion.IdAplicacion + "" + this.dtsClienteFactura.IdFactura + "" + this.globales.obtenerHora("") + 1;
        Dinero.push({ "Numero": Numero, "Subtotal12": this.redondear(parseFloat(this.dtsClienteFactura.Subtotal12)), "Subtotal0": this.redondear(parseFloat("0.00")), "Iva": this.redondear(parseFloat(this.dtsClienteFactura.Iva)), "Total": this.redondear(parseFloat(this.dtsClienteFactura.Total)), "IdFactura": this.dtsClienteFactura.IdFactura, "Estado": 1, "IdPago": this.parametros.pago, "Fecha": moment().add(0, "months").format("YYYY-MM-DD") });
      }
    }
    console.log(Dinero);
    return { "Query": JSON.stringify(Dinero), "Datos": Dinero[0], "Cuotas": Dinero.length, "Primera": parseFloat(Dinero[0].Total) / 100 };
  }

  public generarJsonPago() {
    var Pago;
    if (this.dtsAplicacion.Recurrencia == 1) {
      var Datos: any = this.obtenerDiferidosPago();
      Pago = {
        Data: this.encriptarDatosTarjeta(),
        Diferido: "00000000",
        Factura: {
          Cliente: {
            Telefono: this.dtsClienteFactura.Cliente.Telefono,
            Email: this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email)[0],
            Identificacion: this.dtsClienteFactura.Cliente.Identificacion,
            Aplicacion: {
              IdAplicacion: this.dtsAplicacion.Codigo,
              Gracia: this.dtsAplicacion.Gracia
            }
          },
          Total: Datos.Datos.Total,
          Subtotal12: Datos.Datos.Subtotal12,
          Subtotal0: Datos.Datos.Subtotal0,
          Iva: Datos.Datos.Iva,
          Numero: this.dtsClienteFactura.Numero
        },
        Banco: this.banco,
        Gracia: this.gracia
      };
    } else {
      Pago = {
        Data: this.encriptarDatosTarjeta(),
        Diferido: this.tarjeta.Diferidos,
        Factura: {
          Cliente: {
            Telefono: this.dtsClienteFactura.Cliente.Telefono,
            Email: this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email)[0],
            Identificacion: this.dtsClienteFactura.Cliente.Identificacion,
            Aplicacion: {
              IdAplicacion: this.dtsAplicacion.Codigo,
              Gracia: this.dtsAplicacion.Gracia
            }
          },
          Total: this.redondear(parseFloat(this.dtsClienteFactura.Total)),
          Subtotal12: this.redondear(parseFloat(this.dtsClienteFactura.Subtotal12)),
          Subtotal0: this.redondear(parseFloat(this.dtsClienteFactura.Subtotal0)),
          Iva: this.redondear(parseFloat(this.dtsClienteFactura.Iva)),
          Numero: this.dtsClienteFactura.Numero
        },
        Banco: this.banco,
        Gracia: this.gracia
      };
    }

    return Pago;
  }

  public validarNumeroTarjeta(tarjeta) {
    this.validacionTarjeta.Numero = true;
    var numero = tarjeta.trim();

    var VISA = /^4\d{12}(\d{3})?$/;
    var MASTERCARD = /^(5[1-5]\d{4}|677189)\d{10}$/;

    if (VISA.test(numero)) {
      this.validacionTarjeta.Numero = false;
    } else if (MASTERCARD.test(numero)) {
      this.validacionTarjeta.Numero = false;
    } else {
      this.validacionTarjeta.Numero = true;
    }
    this.obtenerDiferidos();
  }

  public validarNumeroExpiracion(expiracion) {
    this.validacionTarjeta.anioExpiracion = true;
    this.validacionTarjeta.mesExpiracion = true;

    var fecha = new Date();
    var anioActual = fecha.getFullYear();
    var m = fecha.getMonth();
    var mesActual = m + 1;
    var mes = 0;
    var anio = 0;

    if (expiracion.indexOf('/') != -1) {
      mes = parseInt(this.separador(expiracion)[0].trim());
      anio = parseInt(this.separador(expiracion)[1].trim());

      if (mes >= 1 && mes <= 12) {
        this.validacionTarjeta.mesExpiracion = false;
      } else {
        this.validacionTarjeta.mesExpiracion = true;
      }

      if (anio >= parseInt(anioActual.toString().substr(-2))) {
        this.validacionTarjeta.anioExpiracion = false;
      } else {
        this.validacionTarjeta.anioExpiracion = true;
      }

      if (anio == parseInt(anioActual.toString().substr(-2)) && mes < mesActual) {
        this.validacionTarjeta.mesExpiracion = true;
      }
    } else {
      this.validacionTarjeta.mesExpiracion = true;
      this.validacionTarjeta.anioExpiracion = true;
    }

  }

  public validarCVV(cvv) {
    var codigo = cvv.trim();
    if (codigo.length >= 3) {
      this.validacionTarjeta.CVV = false;
    } else {
      this.validacionTarjeta.CVV = true;
    }
  }

  public validarTitular(titular) {
    var nombre = titular.trim();
    if (nombre.length >= 5) {
      this.validacionTarjeta.Titular = false;
    } else {
      this.validacionTarjeta.Titular = true;
    }
  }

  public validarFormularioTarjeta() {

    var VISA = /^4\d{12}(\d{3})?$/;
    var MASTERCARD = /^(5[1-5]\d{4}|677189)\d{10}$/;

    var fecha = new Date();
    var anioActual = fecha.getFullYear();
    var m = fecha.getMonth();
    var mesActual = m + 1;
    var mes = 0;
    var anio = 0;
    var expiracion = this.tarjeta.Expiracion;

    if (expiracion.indexOf('/') != -1) {
      mes = parseInt(this.separador(expiracion)[0].trim());
      anio = parseInt(this.separador(expiracion)[1].trim());
    }

    if (!VISA.test(this.tarjeta.Numero.trim()) && !MASTERCARD.test(this.tarjeta.Numero.trim())) {
      this.gestionAlertaTiempo("Ingresar Número de Tarjeta", "danger", 2000, false);
    } else if (!(mes >= 1 && mes <= 12)) {
      this.gestionAlertaTiempo("Ingresar Mes de Expiración Correcto", "danger", 2000, false);
    } else if (!(anio >= parseInt(anioActual.toString().substr(-2)))) {
      this.gestionAlertaTiempo("Ingresar Año de Expiración Correcto", "danger", 2000, false);
    } else if ((anio == parseInt(anioActual.toString().substr(-2)) && mes < mesActual)) {
      this.gestionAlertaTiempo("Ingresar Año/Mes de Expiración Correcto", "danger", 2000, false);
    } else if (this.tarjeta.CVV.length < 3) {
      this.gestionAlertaTiempo("Ingresar Código de la Tarjeta", "danger", 2000, false);
    } else if (this.tarjeta.Titular.length < 5) {
      this.gestionAlertaTiempo("Ingresar Nombre del Titular", "danger", 2000, false);
    } else if (this.tarjeta.Diferidos == "") {
      this.gestionAlertaTiempo("Seleccionar un Diferido", "danger", 2000, false);
    } else {
      this.realizarPago();
    }
  }

  //-----------------------

  public guardarToken(pago) {

    this.gestionAlerta("Realizando el pago, Espere Por Favor...", "info", true, true);

    var Pago = {
      Identificador: 1,
      IdToken: 0,
      Cliente: {
        IdCliente: this.dtsClienteFactura.Cliente.IdCliente
      },
      Token: pago.cardToken == undefined ? "" : pago.cardToken,
      Marca: pago.cardBrand == undefined ? "" : pago.cardBrand,
      Banco: this.banco
    }

    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/token", Pago).subscribe(
      (res: any) => {

        var parametros = { idPago: this.parametros.pago, idAplicacion: this.parametros.plataforma, idTransaccion: pago.transactionId, card: pago.cardToken, cuota: 0, diferidos: 0, banco: this.banco, gracia: this.gracia, recurrencia: this.parametros.recurrencia };
        var datos = "?datos=" + btoa(JSON.stringify(parametros)) + "&id=0&resourcePath=0";

        if (pago.transactionId == undefined) {

          this.gestionAlerta("", "info", false, false);
          this.botonVerdad = true;
          this.botonInterfaz = false;

          if (pago.errors == undefined || pago.errors == null) {
            this.errores = ({ mensaje: pago.message, errores: [{ "message": "Error Pago", "errorCode": 127, "errorDescriptions": ['El cliente se encuentra registrado en listas negras.'] }] });
          } else {
            this.errores = ({ mensaje: pago.message, errores: pago.errors });
          }

          $('#AlertErrores').modal('toggle');

          this.conexion.error("pago-boton-payphone.component.ts", "realizarPago", "Gestion/SGesGestion.svc/pago/pago/token", "500", "/validaciones", res, 0, "VALIDACIONES PAYPHONE");

        } else {
          window.open(this.global.obtenerCredenciales().conexionResultDatafast + datos, "_self");
        }


      },
      err => {
        this.botonVerdad = true;
        this.botonInterfaz = false;
        this.gestionAlerta("Error al realizar el pago: Error en el servidor de datos", "danger", true, false);
        this.conexion.error("pago-boton-payphone.component.ts", "guardarToken", "Gestion/SGesGestion.svc/pago/pago/token", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  //METODOS COMPLEMENTARIOS

  public redondear(numero) {
    var redondeado = this.redondearDecimales(numero);
    var redondeadoString = redondeado + "";
    var total = redondeadoString.replace(".", "");
    return total;
  }

  public redondearDecimales(valor: any) {
    return parseFloat(valor).toFixed(2);
  }

  public separador(texto) {
    var str = texto;
    var res = str.split("/");
    return res;
  }

  public gestionAlertaTiempo(texto, color, tiempo, spinner) {

    this.alertas.mensaje.texto = texto;
    this.alertas.mensaje.color = "alert alert-" + color;
    this.alertas.mensaje.estado = true;
    this.alertas.mensaje.spinner = spinner;

    setTimeout(() => {
      this.alertas.mensaje.texto = texto;
      this.alertas.mensaje.color = "alert alert-success";
      this.alertas.mensaje.estado = false;
      this.alertas.mensaje.spinner = false;
    }, tiempo);
  }

  public gestionAlerta(texto, color, estado, spinner) {
    this.alertas.mensaje.texto = texto;
    this.alertas.mensaje.color = "alert alert-" + color;
    this.alertas.mensaje.estado = estado;
    this.alertas.mensaje.spinner = spinner;
  }

  public encriptarDatosTarjeta() {
    var codigo = this.global.obtenerCredenciales().codigoEncriptacion;
    var datos = {
      cardNumber: this.tarjeta.Numero, expirationMonth: this.separador(this.tarjeta.Expiracion)[0].trim(), expirationYear: this.separador(this.tarjeta.Expiracion)[1].trim(),
      holderName: this.tarjeta.Titular, securityCode: this.tarjeta.CVV
    };

    var key = CryptoJS.enc.Utf8.parse(codigo);
    var iv = CryptoJS.enc.Utf8.parse('');
    var encrypted = CryptoJS.AES.encrypt(JSON.stringify(datos), key, { iv: iv });

    var codificado = encrypted.ciphertext.toString(CryptoJS.enc.Base64);
    return codificado;
  }

  public encriptarHolderTarjeta() {
    var codigo = this.global.obtenerCredenciales().codigoEncriptacion;
    var key = CryptoJS.enc.Utf8.parse(codigo);
    var iv = CryptoJS.enc.Utf8.parse('');
    var encrypted = CryptoJS.AES.encrypt(this.tarjeta.Titular, key, { iv: iv });

    var codificado = encrypted.ciphertext.toString(CryptoJS.enc.Base64);
    return codificado;
  }

}
