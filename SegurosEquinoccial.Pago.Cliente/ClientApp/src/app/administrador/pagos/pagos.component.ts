import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../../servicio/api/api.service';
import { DomSanitizer } from '@angular/platform-browser';
import Swal from 'sweetalert2';
import { VariablesEmailDatafastTemplate } from '../../variables/email/email-datafast-template';
import { Globales } from '../../variables/globales/globales.service';
declare var $: any;
declare var getColumns: any;
declare var moment: any;


@Component({
  selector: 'app-pagos',
  templateUrl: './pagos.component.html',
  styleUrls: ['./pagos.component.css'],
  providers: [GlobalesPipe]
})
export class PagosComponent implements OnInit {

  usuario: any;
  lstPagos = [];
  lstVerificacionPago: any = [];
  pago: any = { "AnioExpiracionTarjeta": null, "CodigoAutenticacion": "", "CodigoVerificacionTarjeta": null, "Diferido": null, "Estado": 0, "Factura": { "Cliente": { "Apellido": "", "Aplicacion": { "Codigo": 0, "ColorPrimario": null, "ColorSecundario": null, "Estado": 0, "FondoPrimario": null, "FondoSecundario": null, "IdAplicacion": 0, "Identificacion": null, "Identificador": 0, "LogoPrimario": null, "LogoPrimarioTamano": null, "LogoSecundario": null, "LogoSecundarioTamano": null, "MontoMaximo": 0, "MontoMinimo": 0, "Nombre": "", "Token": null, "VisualizacionBin": 0 }, "Email": null, "Estado": 0, "IdCliente": 0, "Identificacion": "", "Identificador": 0, "Ip": null, "NombreCompleto": null, "Numero": null, "PrimerNombre": "", "SegundoNombre": "", "Telefono": null }, "Comercio": "", "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": "", "Numero": "", "Subtotal0": "", "Subtotal12": "", "Total": "", "UrlRetorno": "0" }, "FechaTransaccion": "", "IdPago": 0, "IdTransaccion": "", "Identificador": 0, "Intereses": "", "Ip": "", "Lote": "", "MesExpiracionTarjeta": null, "NombreTarjeta": null, "NumeroDiferidos": "", "NumeroTarjeta": null, "ParametroPersonalizado": "", "Plataforma": "", "Recibo": "", "Referencia": "", "RespuestaAdquiriente": "", "ResultadoCodigo": "", "ResultadoTexto": "", "ResultadoTrama": "", "Voucher": "" };

  fecha = { inicio: null, fin: null };

  lstPlataformas: Array<string> = ["DATAFAST", "PAYPHONE"];
  lstEstados: Array<{ text: string, value: number }> = [
    { text: "SIN REALIZAR", value: 1 },
    { text: "EXITOSOS", value: 2 },
    { text: "RECHAZADOS", value: 3 },
    { text: "REVERSADOS", value: 4 },
    { text: "ANULADOS", value: 5 },
    { text: "EXPIRADOS", value: 6 },
  ];

  public fmrParametros = {
    Identificacion: "",
    IdPago: 0,
    Plataforma: "",
    Voucher: "",
    Comercio: "",
    EstadoPago: "",
    Aplicacion: ""
  }

  public cadena = "";
  public imgDetalles = "ruc.png";
  public p: any;
  public q: any;
  public plataforma = "";
  public lstAplicaciones = [];
  public urlRecibo = "";

  variables: Globales = new Globales();
  email: VariablesEmailDatafastTemplate = new VariablesEmailDatafastTemplate();

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, public globales: GlobalesPipe, private dom: DomSanitizer) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.fecha.inicio = new Date();
    this.fecha.fin = new Date();
    this.listarAplicaciones();
  }

  public listarAplicaciones() {

    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/combo/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstAplicaciones = res;
        this.listarPagos();
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Aplicaciones", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("pagos.component.ts", "listarAplicaciones", "Gestion/SGesGestion.svc/pago/aplicacion/listar/combo/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public listarPagos() {

    var datos = {
      FechaInicio: this.globales.obtenerFormatoFecha(this.fecha.inicio, "-"),
      FechaFin: this.globales.obtenerFormatoFecha(this.fecha.fin, "-"),
      Cadena: this.cadena
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
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Pagos", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("pagos.component.ts", "listarPagos", "Gestion/SGesGestion.svc/pago/pago/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public listarPagoParametros() {
    var cadena = "";
    var fmrParametros = this.fmrParametros;
    var globales = this.globales;
    $("input:checkbox[name=parametro]:checked").each(function () {
      var parametro = $(this).val();

      if (parametro == 1) {
        if (fmrParametros.Identificacion.trim() != "") {
          cadena += " AND Identificacion = '" + fmrParametros.Identificacion + "'"
        } else {
          globales.notificacion("Ingresar Número de Identificación", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 2) {
        if (fmrParametros.IdPago != null) {
          cadena += " AND IdPago = " + fmrParametros.IdPago + "";
        } else {
          globales.notificacion("Ingresar Código de Pago", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 3) {
        if (fmrParametros.Plataforma.trim() != "") {
          cadena += " AND Plataforma = '" + fmrParametros.Plataforma + "'";
        } else {
          globales.notificacion("Seleccionar Plataforma", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 4) {
        if (fmrParametros.Voucher.trim() != "") {
          cadena += " AND Voucher = '" + fmrParametros.Voucher + "'";
        } else {
          globales.notificacion("Ingresar Número de Voucher", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 5) {
        if (fmrParametros.Comercio.trim() != "") {
          cadena += " AND Comercio LIKE '%" + fmrParametros.Comercio + "%'";
        } else {
          globales.notificacion("Ingresar Detalle Comercio", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 6) {
        if (fmrParametros.EstadoPago.length != 0) {
          var estados = fmrParametros.EstadoPago;
          var consulta = "";
          for (let dat of estados) {
            consulta += "EstadoPago = " + dat + " OR ";
          }

          cadena += " AND (" + consulta.substr(0, (consulta.length - 4)) + ")";
        } else {
          globales.notificacion("Seleccionar Estado", "info", 'top', "#3498DB", "#fff");
        }
      } if (parametro == 7) {
        if (fmrParametros.Aplicacion.length != 0) {
          var aplicacion = fmrParametros.Aplicacion;
          var consulta = "";
          for (let dat of aplicacion) {
            consulta += "IdAplicacion = " + dat + " OR ";
          }

          cadena += " AND (" + consulta.substr(0, (consulta.length - 4)) + ")";
        } else {
          globales.notificacion("Seleccionar Línea de Negocio", "info", 'top', "#3498DB", "#fff");
        }
      }
    });
    this.cadena = cadena;
    this.listarPagos();
  }

  public abrirModalDetallePago(datos) {
    this.pago = datos;
    console.log(datos)
    if (this.pago.Estado == 2) {
      this.conexion.get("Gestion/SGesGestion.svc/pago/pago/consultar/recibo/" + this.pago.IdPago).subscribe(
        (res: any) => {
          if (datos.Factura.Cliente.Identificacion.length == 10) {
            this.imgDetalles = "cedula.png";
          } else {
            this.imgDetalles = "ruc.png";
          }
          this.lstVerificacionPago = [];
          $('#modalDetallesPago').modal('show');
          this.urlRecibo = "data:application/pdf;base64," + res;
        },
        err => {
          console.log(err);
          this.globales.notificacion("Error con el servidor de datos:<br>Hablitar pagos fallidos", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("admin-pagos.component.ts", "habilitarPagosFallidos", "Gestion/SGesGestion.svc/enviar/pago/recibo/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        }
      );
    } else {
      if (datos.Factura.Cliente.Identificacion.length == 10) {
        this.imgDetalles = "cedula.png";
      } else {
        this.imgDetalles = "ruc.png";
      }
      this.lstVerificacionPago = [];
      $('#modalDetallesPago').modal('show');
    }
  }

  public exportarExcel() {
    var data = [];

    for (let pago of this.lstPagos) {
      console.log(pago.Referencia + "");
      data.push(
        {
          Comercio: pago.Factura.Cliente.Aplicacion.Nombre,
          Identificacion: pago.Factura.Cliente.Identificacion + "-",
          Cliente: pago.Factura.Cliente.PrimerNombre + " " + pago.Factura.Cliente.SegundoNombre + " " + pago.Factura.Cliente.Apellido,
          Plataforma: pago.Plataforma,
          Ip: pago.Ip,
          IdPago: pago.IdPago + "",
          CodigoTransaccion: pago.IdTransaccion + "",
          NumeroAutenticacion: pago.CodigoAutenticacion + "",
          Referencia: pago.Referencia,
          Lote: this.limpiarLote(pago.Plataforma, pago.Lote, pago.Estado),
          Voucher: pago.Voucher,
          NumeroDiferidos: this.obtenerDiferidos(pago),
          Intereses: pago.Intereses + "",
          NumeroTransaccion: pago.Factura.Numero + "-",
          Total: pago.Factura.Total + "",
          Estado: this.verificarEstado(pago.Estado).mensaje,
          Descripcion: pago.Factura.Comercio,
          FechaTransaccion: pago.FechaTransaccion,
          BinTarjeta: pago.Bin,
          MarcaTarjeta: pago.Marca,
          ApoderadoTarjeta: pago.Apoderado
        }
      );
    }

    $("#exportar").excelexportjs({
      containerid: "exportar",
      datatype: 'json',
      dataset: data,
      columns: getColumns(data),
      worksheetName: "Lista de Pagos",
      encoding: "utf-8"
    });

  }

  public limpiarLote(plataforma, lote, estado) {
    if (plataforma == "DATAFAST") {
      if (estado == 2) {
        if (lote != "") {
          var codigo = lote;
          var longitud = lote.length - 1;
          return parseInt(codigo.substr(1, longitud));
        } else {
          return 0;
        }
      } else {
        return 0;
      }
    } else {
      return lote;
    }
  }

  public descargarRecibo() {
    return this.dom.bypassSecurityTrustUrl(this.urlRecibo);
  }

  public verificarEstado(estado) {
    var trama = {
      mensaje: "S/E",
      aviso: "fas fa-minus-circle text-info tamanio"
    }
    if (estado == 1) {
      trama.mensaje = "Sin Realizar";
      trama.aviso = "fas fa-minus-circle text-info tamanio";
    } else if (estado == 2) {
      trama.mensaje = "Exitosa";
      trama.aviso = "fas fa-check-circle text-success tamanio";
    } else if (estado == 3) {
      trama.mensaje = "Rechazada";
      trama.aviso = "fas fa-times-circle text-danger tamanio";
    } else if (estado == 4) {
      trama.mensaje = "Reversada";
      trama.aviso = "fas fa-sync-alt text-warning tamanio";
    } else if (estado == 5) {
      trama.mensaje = "Anulada";
      trama.aviso = "fas fa-ban text-danger tamanio";
    } else if (estado == 6) {
      trama.mensaje = "Expirada";
      trama.aviso = "fas fa-ban text-warning tamanio";
    }
    return trama;
  }

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

  public verificarPagoDatafast() {
    this.lstVerificacionPago = [];
    this.spinner.show();

    var Datos = {
      IdAplicacion: this.pago.Factura.Cliente.Aplicacion.Gracia,
      Gracia: this.pago.Gracia,
      Banco: this.pago.Banco,
      Transaccion:  this.pago.Factura.Numero
    }

    this.conexion.post("Gestion/SGesGestion.svc/pago/datafast/verificar/pago", Datos).subscribe(
      (res: any) => {
        this.spinner.hide();
        var respago = JSON.parse(res);

        if (respago.payments != undefined) {

          for (let det of respago.payments) {
            this.lstVerificacionPago.push(
              {
                Fecha: this.globales.obtenerFechaCompletaParametro(det.timestamp, "-"),
                Cliente: det.customer.surname,
                EstadoCodigo: det.result.code,
                EstadoDescripcion: det.result.description,
                Plataforma: "DATAFAST",
                Trama: det
              }
            );
          }

        } else {
          this.globales.mostrarAlertaSinTiempo("No se encontró ningún pago con el código enviado", "warning");
        }
        console.log(res);

      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Verificar Pago Datafast", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("admin-pagos.component.ts", "verificarPago", "Gestion/SGesGestion.svc/pago/datafast/verificar/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public verificarPagoPayphone() {
    this.lstVerificacionPago = [];
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/payphone/verificar/pago/cliente/" + this.pago.Factura.Numero).subscribe(
      (res: any) => {
        this.spinner.hide();
        var respago = JSON.parse(res);
        console.log(respago);
        if (respago.length != undefined) {
          for (let det of respago) {
            if (det.statusCode == 3) {
              this.lstVerificacionPago.push(
                {
                  Fecha: moment().format('YYYY-MM-DD HH:mm:ss'),
                  Cliente: det.optionalParameter4,
                  EstadoCodigo: det.statusCode,
                  EstadoDescripcion: det.transactionStatus,
                  Plataforma: "PAYPHONE",
                  Trama: det
                }
              );
            } else {
              this.lstVerificacionPago.push(
                {
                  Fecha: moment().format('YYYY-MM-DD HH:mm:ss'),
                  Cliente: det.optionalParameter4,
                  EstadoCodigo: det.statusCode,
                  EstadoDescripcion: det.message,
                  Plataforma: "PAYPHONE",
                  Trama: det
                }
              );
            }
          }

        } else {
          this.globales.mostrarAlertaSinTiempo("No se encontró ningún pago con el código enviado", "warning");
        }
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Verificar Pago Datafast", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("admin-pagos.component.ts", "verificarPago", "Gestion/SGesGestion.svc/pago/payphone/verificar/pago/cliente/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public actualizarPago(datos) {

    var parametros: any = {
      Ip: "",
      IdTransaccion: "",
      CodigoAutenticacion: "",
      Referencia: "",
      Lote: "",
      Voucher: "",
      NumeroDiferidos: "",
      ResultadoCodigo: "",
      ResultadoTexto: "",
      ResultadoTrama: "",
      Estado: 0,
      Intereses: "",
      RespuestaAdquiriente: "",
      TarjetaHabiente: "",
      Fecha: ""
    };

    if (datos.Plataforma == "DATAFAST") {
      parametros.Ip = datos.Trama.customer == undefined ? "000.000.000.000" : datos.Trama.customer.ip;
      parametros.IdTransaccion = datos.Trama.id;
      parametros.CodigoAutenticacion = datos.Trama.resultDetails == undefined ? "00-00" : datos.Trama.resultDetails.AuthCode;
      parametros.Referencia = datos.Trama.resultDetails == undefined ? "00" : this.obtenerReferencia(datos.Trama.resultDetails.ReferenceNbr);
      parametros.Lote = datos.Trama.resultDetails == undefined ? "00" : this.obtenerLote(datos.Trama.resultDetails.ReferenceNbr);
      parametros.Voucher = datos.Trama.resultDetails == undefined ? "00-00" : this.obtenerVoucher(datos.Trama.resultDetails.ReferenceNbr);
      parametros.NumeroDiferidos = datos.Trama.recurring == undefined ? "0" : datos.Trama.recurring.numberOfInstallments;
      parametros.ResultadoCodigo = datos.Trama.result == undefined ? "0" : datos.Trama.result.code;
      parametros.ResultadoTexto = datos.Trama.result == undefined ? "0" : datos.Trama.result.description;
      parametros.ResultadoTrama = JSON.stringify(datos.Trama);
      parametros.Estado = datos.Trama.result == undefined ? 3 : this.obtenerEstado("DATAFAST", datos.Trama.result.code);
      parametros.Intereses = datos.Trama.customParameters == undefined ? "0" : datos.Trama.customParameters.SHOPPER_interes;
      parametros.RespuestaAdquiriente = datos.Trama.resultDetails == undefined ? "0" : datos.Trama.resultDetails.AcquirerResponse;
      parametros.TarjetaHabiente = datos.Trama.card.holder;
      parametros.Fecha = this.globales.obtenerFechaCompletaParametro(datos.Trama.timestamp, "-");
    } else if (datos.Plataforma == "PAYPHONE") {
      parametros.Ip = this.pago.Ip;
      parametros.IdTransaccion = datos.Trama.transactionId;
      parametros.CodigoAutenticacion = datos.Trama.authorizationCode == undefined || datos.Trama.authorizationCode == null ? "0000" : datos.Trama.authorizationCode;
      parametros.Referencia = datos.Trama.transactionId;
      parametros.Lote = datos.Trama.transactionId;
      parametros.Voucher = datos.Trama.transactionId + "-" + datos.Trama.transactionId;
      parametros.NumeroDiferidos = datos.Trama.deferred == true ? datos.Trama.deferredMessage : "Corriente";
      parametros.ResultadoCodigo = datos.Trama.statusCode;
      parametros.ResultadoTexto = datos.Trama.transactionStatus;
      parametros.ResultadoTrama = JSON.stringify(datos.Trama);
      parametros.Estado = this.obtenerEstado("PAYPHONE", datos.Trama.statusCode);
      parametros.Intereses = "0";
      parametros.RespuestaAdquiriente = "0";
      parametros.TarjetaHabiente = datos.Trama.optionalParameter4;
      parametros.Fecha = moment().format('YYYY-MM-DD HH:mm:ss');
    }

    var Pago = {
      Identificador: 1,
      IdPago: this.pago.IdPago,
      Ip: parametros.Ip,
      Plataforma: datos.Plataforma,
      IdTransaccion: parametros.IdTransaccion,
      CodigoAutenticacion: parametros.CodigoAutenticacion,
      Referencia: parametros.Referencia,
      Lote: parametros.Lote,
      Voucher: parametros.Voucher,
      ParametroPersonalizado: "",
      NumeroDiferidos: parametros.NumeroDiferidos,
      ResultadoCodigo: parametros.ResultadoCodigo,
      ResultadoTexto: parametros.ResultadoTexto,
      ResultadoTrama: parametros.ResultadoTrama,
      Estado: parametros.Estado,
      Intereses: 0,
      RespuestaAdquiriente: parametros.RespuestaAdquiriente,
      Factura: {
        IdFactura: 0,
        Iva: this.pago.Factura.Iva,
        Subtotal12: this.pago.Factura.Subtotal12,
        Subtotal0: this.pago.Factura.Subtotal0,
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
        $('#modalDetallesPago').modal('hide');
        this.listarPagos();
        Swal.fire({
          html: '<span>Pago Actualizado Exitosamente</span><br><span>¿Desea enviar el email de confirmación al cliente?</span>',
          type: "success",
          showCancelButton: true,
          confirmButtonColor: '#b14140',
          cancelButtonColor: '#3c3c3c',
          confirmButtonText: 'Continuar',
          cancelButtonText: 'Cancelar'
        }).then((result) => {
          if (result.value) {
            this.enviarEmail(parametros.TarjetaHabiente, parametros.CodigoAutenticacion, parametros.Fecha, datos.Plataforma, parametros.Estado);
          }
        });
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.conexion.error("admin-pagos.component.ts", "actualizarPago", "Gestion/SGesGestion.svc/pago/pago/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
      }
    );
  }

  public enviarEmail(tarjetaHabiente, codigo, fecha, plataforma, estado) {

    var emails = this.globales.gestionEmail(this.pago.Factura.Cliente.Email);
    var Email = {
      "Para": emails[0].trim(),
      "Asunto": this.variables.ambiente == "PRODUCCION" ? "Confirmación de pago " + plataforma : "[MODO PRUEBA]-Confirmación de pago " + plataforma,
      "Mensaje": this.email.generarEmail(tarjetaHabiente, codigo, fecha, this.pago.Factura.Total, estado)
    }

    this.conexion.post("Gestion/SGesGestion.svc/enviar/pago/recibo/" + this.pago.IdPago, Email).subscribe(
      (res: any) => {
        console.log(res);
      },
      err => {
        console.log(err);
        this.conexion.error("admin-pagos.component.ts", "enviarEmail", "Gestion/SGesGestion.svc/enviar/pago/recibo/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
      }
    );
  }

  public habilitarPagosFallidos() {

    var datos = {
      Identificador: 1,
      IdGestion: 0,
      Identificacion: this.pago.Factura.Cliente.Identificacion,
      Estado: 0
    }

    this.conexion.post("Gestion/SGesGestion.svc/pago/gestion/gestion", datos).subscribe(
      (res: any) => {
        this.globales.notificacion("Cliente Habilitado", "success", "bottom", "#239B56", "#FFF");
        console.log(res);
      },
      err => {
        console.log(err);
        this.globales.notificacion("Error con el servidor de datos:<br>Hablitar pagos fallidos", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("admin-pagos.component.ts", "habilitarPagosFallidos", "Gestion/SGesGestion.svc/enviar/pago/recibo/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
      }
    );
  }

  public codificarBase64(valor) {
    return btoa(valor);
  }

  public decodificarBase64(valor) {
    return atob(valor);
  }

  public obtenerLote(valor) {
    var voucher = valor;
    var resultadoVoucher = voucher.replace("_", "-");

    var textoVoucher = resultadoVoucher;
    var resultadoSeparacion = textoVoucher.split("-");
    return resultadoSeparacion[0];
  }

  public obtenerReferencia(valor) {
    var voucher = valor;
    var resultadoVoucher = voucher.replace("_", "-");

    var textoVoucher = resultadoVoucher;
    var resultadoSeparacion = textoVoucher.split("-");
    return resultadoSeparacion[1];
  }

  public obtenerVoucher(valor) {
    var str = valor;
    var res = str.replace("_", "-");
    return res;
  }

  public obtenerEstado(plataforma, valor) {
    var estado = 0;

    if (plataforma == "DATAFAST") {
      if (valor == "000.000.000") {
        estado = 2;
      } else if (valor == "000.000.100") {
        estado = 2;
      } else if (valor == "000.100.110") {
        estado = 2;
      } else if (valor == "000.100.111") {
        estado = 2;
      } else if (valor == "000.100.112") {
        estado = 2;
      } else if (valor == "000.300.000") {
        estado = 2;
      } else if (valor == "000.300.100") {
        estado = 2;
      } else if (valor == "000.300.101") {
        estado = 2;
      } else if (valor == "000.300.102") {
        estado = 2;
      } else if (valor == "000.310.100") {
        estado = 2;
      } else if (valor == "000.310.101") {
        estado = 2;
      } else if (valor == "000.310.110") {
        estado = 2;
      } else if (valor == "000.600.000") {
        estado = 2;
      } else {
        estado = 3;
      }
    } else if (plataforma == "PAYPHONE") {
      if (valor == "3") {
        estado = 2;
      } else {
        estado = 3;
      }
    }

    return estado;
  }

  public listarEmails(emails) {
    if (emails != null) {
      var parametro = emails;
      var separacion = parametro.split(";");
      return separacion[0];
    }
  }

  public marcaTarjeta(bin, digitos) {

    var imagenTarjeta = 'fas fa-credit-card';
    var numeroTarjeta = '';

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

    return { icon: imagenTarjeta, card: numeroTarjeta };
  }

  public copiar() {
    var copyText: any = document.getElementById("txtTrama");
    copyText.select();
    copyText.setSelectionRange(0, 99999999)
    document.execCommand("copy");
    this.globales.notificacion("Trama Copiada Exitosamente", "success", 'top', "#3498DB", "#fff");
  }
}
