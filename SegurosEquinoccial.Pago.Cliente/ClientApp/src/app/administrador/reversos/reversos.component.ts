import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { Globales } from '../../variables/globales/globales.service';
import { VariablesEmailDatafastTemplate } from '../../variables/email/email-datafast-template';
declare var $: any;

@Component({
  selector: 'app-reversos',
  templateUrl: './reversos.component.html',
  styleUrls: ['./reversos.component.css'],
  providers: [GlobalesPipe]
})
export class ReversosComponent implements OnInit {

  variables: Globales = new Globales();
  public email: VariablesEmailDatafastTemplate = new VariablesEmailDatafastTemplate();

  usuario: any;
  lstPagos = [];
  lstPagosReversos = [];
  lstResultado: any = [];
  pago: any = { "AnioExpiracionTarjeta": null, "CodigoAutenticacion": "", "CodigoVerificacionTarjeta": null, "Diferido": null, "Estado": 0, "Factura": { "Cliente": { "Apellido": "", "Aplicacion": { "Codigo": 0, "ColorPrimario": null, "ColorSecundario": null, "Estado": 0, "FondoPrimario": null, "FondoSecundario": null, "IdAplicacion": 0, "Identificacion": null, "Identificador": 0, "LogoPrimario": null, "LogoPrimarioTamano": null, "LogoSecundario": null, "LogoSecundarioTamano": null, "MontoMaximo": 0, "MontoMinimo": 0, "Nombre": "", "Token": null, "VisualizacionBin": 0 }, "Email": null, "Estado": 0, "IdCliente": 0, "Identificacion": "", "Identificador": 0, "Ip": null, "NombreCompleto": null, "Numero": null, "PrimerNombre": "", "SegundoNombre": "", "Telefono": null }, "Comercio": "", "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": "", "Numero": "", "Subtotal0": "", "Subtotal12": "", "Total": "", "UrlRetorno": "0" }, "FechaTransaccion": "", "IdPago": 0, "IdTransaccion": "", "Identificador": 0, "Intereses": "", "Ip": "", "Lote": "", "MesExpiracionTarjeta": null, "NombreTarjeta": null, "NumeroDiferidos": "", "NumeroTarjeta": null, "ParametroPersonalizado": "", "Plataforma": "", "Recibo": "", "Referencia": "", "RespuestaAdquiriente": "", "ResultadoCodigo": "", "ResultadoTexto": "", "ResultadoTrama": "", "Voucher": "" };

  reverso: any = { "Codigo": "", "CodigoAutenticacion": "", "Conector": "", "Estado": 0, "FechaReverso": "", "NumeroReferencia": "", "ResultadoCodigo": "", "ResultadoTexto": "", "Pago": { "Voucher": "", "IdPago": 0, "Factura": { "Numero": "", "Total": "", "Cliente": { "Identificacion": "", "PrimerNombre": "", "SegundoNombre": "", "Apellido": "" }, } }, "ResultadoTrama": "" };

  fmrReverso = {
    NumeroTarjeta: "",
    MesExpiracionTarjeta: "",
    AnioExpiracionTarjeta: "",
    Titular: ""
  }

  //Nuevo

  public cadena = "";
  public fmrParametros = {
    Identificacion: "",
    PrimerNombre: "",
    SegundoNombre: "",
    Apellido: ""
  }
  public imgDetalles = "ruc.png";
  public p: any;
  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, public globales: GlobalesPipe) { }

  ngAfterViewInit() {
  }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.buscarPagos();
  }

  public buscarPagos() {

    var datos = {
      FechaInicio: this.globales.obtenerFormatoFecha(new Date(), "-"),
      FechaFin: this.globales.obtenerFormatoFecha(new Date(), "-"),
      Cadena: " AND EstadoPago = 2 " + this.cadena
    }

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/listar/completo", datos).subscribe(
      (res: any) => {

        this.spinner.hide();
        this.lstPagos = res;
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Pagos DataFast", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("reversos-datafast.component.ts", "buscarPagos", "Gestion/SGesGestion.svc/pago/pago/listar/exitosos/DATAFAST", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }


  public listarPagoParametros() {
    var cadena = "";
    var fmrParametros = this.fmrParametros;
    $("input:checkbox[name=parametro]:checked").each(function () {
      var parametro = $(this).val();

      if (parametro == 1) {
        cadena += " AND Identificacion = '" + fmrParametros.Identificacion + "'"
      } if (parametro == 2) {
        cadena += " AND PrimerNombre LIKE '%" + fmrParametros.PrimerNombre + "%'"
      } if (parametro == 3) {
        cadena += " AND SegundoNombre  LIKE '%" + fmrParametros.SegundoNombre + "%'"
      } if (parametro == 4) {
        cadena += " AND Apellido  LIKE '%" + fmrParametros.Apellido + "%'"
      }
    });

    this.cadena = cadena;

    this.buscarPagos();
  }

  public abrirModalReverso(datos) {
    this.pago = datos;
    console.log(this.pago)
    this.fmrReverso.Titular = this.pago.Factura.Cliente.PrimerNombre + " " + this.pago.Factura.Cliente.SegundoNombre + " " + this.pago.Factura.Cliente.Apellido;
    if (datos.Factura.Cliente.Identificacion.length == 10) {
      this.imgDetalles = "cedula.png";
    } else {
      this.imgDetalles = "ruc.png";
    }
    $('#modalDetallesPago').modal('show');
  }

  public reversarPagoPayphone(datos) {
    this.lstResultado = [];
    var Pago = {
      IdTransaccion: datos.IdTransaccion
    };
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/payphone/reversar", Pago).subscribe(
      (res: any) => {
        console.log(res);
        this.spinner.hide();

        if (res == true) {
          this.lstResultado = res;
        } else {
          this.lstResultado = JSON.parse(res);
        }

        var estado = this.obtenerEstado(this.lstResultado == true ? "3" : "2");

        this.registrarReverso(
          datos.IdTransaccion,
          datos.Referencia,
          datos.RespuestaAdquiriente,
          datos.CodigoAutenticacion,
          "0",
          this.lstResultado == true ? "3" : "2",
          this.lstResultado == true ? "Approved" : "Canceled",
          res,
          datos.IdPago,
          estado,
          "PAYPHONE"
        );
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Reversar Pagos PayPhone", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("reversos-payphone.component.ts", "reversarPago", "Gestion/SGesGestion.svc/pago/payphone/reversar", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public reversarPagoDatafast() {
    this.lstResultado = [];
    if (this.fmrReverso.NumeroTarjeta == "") {
      this.globales.notificacion("Ingresar el Número de Tarjeta", "error", "top-end", "#E74C3CE6", "#FFF");
    } else if (this.fmrReverso.MesExpiracionTarjeta == "") {
      this.globales.notificacion("Ingresar Mes de Expiración ", "error", "top-end", "#E74C3CE6", "#FFF");
    } else if (this.fmrReverso.AnioExpiracionTarjeta == "") {
      this.globales.notificacion("Ingresar Año de Expiración ", "error", "top-end", "#E74C3CE6", "#FFF");
    } else {
      var Pago = {
        CodigoAutenticacion: (this.pago.CodigoAutenticacion),
        Referencia: this.pago.Referencia,
        ParametroPersonalizado: this.pago.ParametroPersonalizado,
        IdTransaccion: this.pago.IdTransaccion,
        NumeroTarjeta: btoa(this.fmrReverso.NumeroTarjeta),
        MesExpiracionTarjeta: btoa(this.fmrReverso.MesExpiracionTarjeta),
        AnioExpiracionTarjeta: btoa(this.fmrReverso.AnioExpiracionTarjeta),
        NumeroDiferidos: this.pago.NumeroDiferidos,
        Banco: this.pago.Banco,
        Gracia: this.pago.Gracia,
        Factura: {
          Total: this.variables.ambiente == "PRODUCCION" ? this.pago.Factura.Total : "3.12",
          Cliente: {
            Aplicacion: {
              Gracia: this.pago.Factura.Cliente.Aplicacion.Gracia
            }
          }
        }
      };

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/datafast/reversar", Pago).subscribe(
        (res: any) => {
          console.log(res);
          this.spinner.hide();
          this.lstResultado = JSON.parse(res);
          var estado = this.obtenerEstado(this.lstResultado.result.code);
          this.registrarReverso(this.lstResultado.id,
            (this.lstResultado.resultDetails.ReferenceNbr == undefined || this.lstResultado.resultDetails.ReferenceNbr == null ? '0000' : this.lstResultado.resultDetails.ReferenceNbr),
            (this.lstResultado.resultDetails.AcquirerResponse == undefined || this.lstResultado.resultDetails.AcquirerResponse == null ? '0000' : this.lstResultado.resultDetails.AcquirerResponse),
            (this.lstResultado.resultDetails.AuthCode == undefined || this.lstResultado.resultDetails.AuthCode == null ? '00000' : this.lstResultado.resultDetails.AuthCode),
            (this.lstResultado.resultDetails.ConnectorTxID1 == undefined || this.lstResultado.resultDetails.ConnectorTxID1 == null ? '0000' : this.lstResultado.resultDetails.ConnectorTxID1),
            (this.lstResultado.result.code == undefined || this.lstResultado.result.code == null ? '0000' : this.lstResultado.result.code),
            (this.lstResultado.result.description == undefined || this.lstResultado.result.description == null ? '0000' : this.lstResultado.result.description),
            res, this.pago.IdPago, estado, "DATAFAST");
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Reversar Pagos DataFast", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("reversos-datafast.component.ts", "reversarPago", "Gestion/SGesGestion.svc/pago/datafast/reversar", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public registrarReverso(id, ReferenceNbr, AcquirerResponse, AuthCode, ConnectorTxID1, code, description, trama, idPago, estado, plataforma) {
    var PagoReverso = {
      Identificador: 1,
      IdPagoReverso: 0,
      Codigo: id,
      NumeroReferencia: ReferenceNbr,
      RespuestaAdquiriente: AcquirerResponse,
      CodigoAutenticacion: AuthCode,
      Conector: ConnectorTxID1,
      ResultadoCodigo: code,
      ResultadoTexto: description,
      ResultadoTrama: trama,
      Estado: estado,
      Pago: {
        IdPago: idPago
      },
    };

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/reverso/gestion", PagoReverso).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.actualizarEstadoPago(idPago, estado, plataforma);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Insertar Reverso", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("reversos-datafast.component.ts", "registrarReverso", "Gestion/SGesGestion.svc/pago/pago/reverso/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public actualizarEstadoPago(idPago, estado, plataforma) {
    var Pago = {
      Identificador: 2,
      IdPago: idPago,
      Ip: "",
      Plataforma: "",
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
      Estado: estado == 0 ? 2 : 4,
      Intereses: "",
      RespuestaAdquiriente: "",
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
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/gestion", Pago).subscribe(
      (res: any) => {
        this.spinner.hide();
        if (estado == 0) {
          this.globales.mostrarAlerta("Error al reversar el pago.<br>Verifique el error en la lista de reversos", "warning");
        } else {
          this.enviarCorreoElectronico(idPago, plataforma);
          this.globales.mostrarAlerta("Reverso Realizado con Éxito", "success");
        }

        this.buscarPagos();

        if (plataforma == "DATAFAST") {
          $('#modalDetallesPago').modal('toggle');
          this.fmrReverso = {
            NumeroTarjeta: "",
            MesExpiracionTarjeta: "",
            AnioExpiracionTarjeta: "",
            Titular: ""
          }
        }
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Actualizar Estado Pago", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("reversos-datafast.component.ts", "actualizarEstadoPago", "Gestion/SGesGestion.svc/pago/pago/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public enviarCorreoElectronico(idPago, plataforma) {

    var msmProduccion = (plataforma == "DATAFAST" ? "Confirmación de reverso Datafast" : "Confirmación de reverso Payphone");
    var msmPrueba = (plataforma == "DATAFAST" ? "[MODO PRUEBA] - Confirmación de reverso Datafast" : "[MODO PRUEBA] - Confirmación de reverso Payphone");
    var cntNormal = this.email.generarEmailReverso(this.pago.Apoderado, this.pago.CodigoAutenticacion, this.globales.obtenerFechaCompleta(""), this.pago.Factura.Total, this.pago.Estado);

    var Email = {
      "Para": this.pago.Factura.Cliente.Email,
      "Asunto": this.variables.ambiente == "PRODUCCION" ? msmProduccion : msmPrueba,
      "Mensaje": cntNormal
    }
    console.log(Email);
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/enviar/email/reverso/" + idPago, Email).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.globales.notificacion("Notificación enviada al cliente.", "success", "top-end", "#29C643E6", "#FFF");
        console.log(res);
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("reversos.component.ts", "enviarCorreoElectronico", "Gestion/SGesGestion.svc/enviar/pago/recibo/" + idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public abrirModalDetalleReverso(datos) {
    this.reverso = datos;
    $('#ModalDetallesReverso').modal('show');
  }

  public obtenerEstado(valor) {
    var estado = 0;

    if (valor == "000.000.000") {
      estado = 4;
    } else if (valor == "000.000.100") {
      estado = 4;
    } else if (valor == "000.100.110") {
      estado = 4;
    } else if (valor == "000.100.111") {
      estado = 4;
    } else if (valor == "000.100.112") {
      estado = 4;
    } else if (valor == "000.300.000") {
      estado = 4;
    } else if (valor == "000.300.100") {
      estado = 4;
    } else if (valor == "000.300.101") {
      estado = 4;
    } else if (valor == "000.300.102") {
      estado = 4;
    } else if (valor == "000.310.100") {
      estado = 4;
    } else if (valor == "000.310.101") {
      estado = 4;
    } else if (valor == "000.310.110") {
      estado = 4;
    } else if (valor == "000.600.000") {
      estado = 4;
    } else if (valor == "3") {
      estado = 4;
    } else {
      estado = 0;
    }
    return estado;
  }

  public verificarEstado(numero) {
    var descripcion = "";
    if (numero == 1) {
      descripcion = "1: SIN TRANSACCIÓN";
    } else if (numero == 2) {
      descripcion = "2: TRANSACCIÓN EXITOSA";
    } else if (numero == 3) {
      descripcion = "3: TRANSACCIÓN ERRÓNEA";
    } else if (numero == 4) {
      descripcion = "4: TRANSACCIÓN REVERSADA";
    }
    return descripcion;
  }

  //NUEVO
  public obtenerDiferidos(datos) {
    var diferido = "En Proceso";
    if (datos.Plataforma == "PAYPHONE") {
      if (datos.NumeroDiferidos == "Corriente") {
        diferido = "Corriente";
      } else {
        diferido = this.globales.obtenerNumerosCadena(datos.NumeroDiferidos) + " Meses";
      }
    } else if (datos.Plataforma == "DATAFAST") {
      if (datos.NumeroDiferidos == "0") {
        diferido = "Corriente";
      } else {
        diferido = datos.NumeroDiferidos + " Meses";
      }

    }
    return diferido;
  }
}
