import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SesionService } from '../../servicio/sesion/sesion.service';
declare var $: any;

@Component({
  selector: 'app-masivos-aplicacion',
  templateUrl: './masivos-aplicacion.component.html',
  styleUrls: ['./masivos-aplicacion.component.css']
})
export class MasivosAplicacionComponent implements OnInit {

  public usuario: any;
  public mensajeSpinner = "Procesando Datos...";
  public globales: GlobalesPipe = new GlobalesPipe();
  public filtros = { inicio: null, fin: null, identificacion: "" };
  public lstPagos = [];
  public lstPagosAplicar = [];
  public p: any;

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.filtros.inicio = new Date();
    this.filtros.fin = new Date();
    this.listarPagos();
  }

  public listarPagos() {

    var datos = {
      FechaInicio: this.globales.obtenerFormatoFecha(this.filtros.inicio, "-"),
      FechaFin: this.globales.obtenerFormatoFecha(this.filtros.fin, "-"),
      Cadena: this.filtros.identificacion == "" ? " AND EstadoPago = 2 AND Codigo = 15 " : " AND EstadoPago = 2 AND Codigo = 15 AND Identificacion = '" + this.filtros.identificacion + "' "
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
        this.conexion.error("masivos-aplicacion.component.ts", "listarPagos", "Gestion/SGesGestion.svc/pago/pago/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public generarListaPagosAplicacion(datos_) {
    console.log(datos_);
    var datos = datos_;
    var lista = datos.Factura.Aplicacion == "" || datos.Factura.Aplicacion == null || datos.Factura.Aplicacion == undefined ? [] : JSON.parse(datos.Factura.Aplicacion);
    this.lstPagosAplicar = [];

    for (let datosCuota of lista) {
      this.lstPagosAplicar.push({
        Bin: datos.Bin,
        IdFactura: datos.Factura.IdFactura,
        Identificacion: datos.Factura.Cliente.Identificacion,
        Plataforma: datos.Plataforma,
        Cuotas: datosCuota.cuota,
        CodigoAutenticacion: datos.CodigoAutenticacion,
        Voucher: datos.Voucher,
        FechaTransaccion: datos.FechaTransaccion,
        Apoderado: datos.Apoderado,
        IdPv: datos.Factura.IdPv,
        Total: datosCuota.deuda,
        EstadoCuota: datosCuota.estado,
        Aplicacion: lista
      });
    }

    console.log(this.lstPagosAplicar);

    $('#ModalPagos').modal('toggle');
  }

  public consultarBin(datosPago) {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/bin/listar/" + datosPago.Bin).subscribe(
      (res: any) => {
        this.spinner.hide();
        console.log(res);
        var datosBin = res;
        this.consultarCodigoAsegurado(datosPago, datosBin);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Consultar BIN", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-aplicacion.component.ts", "consultarBin", "Gestion/SGesGestion.svc/bin/listar", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public consultarCodigoAsegurado(datosPago, datosBin) {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/masivos/codigo/asegurado/" + datosPago.Identificacion).subscribe(
      (res: any) => {
        this.spinner.hide();
        var codigo = this.globales.obtenerCodigoPagador(res);
        console.log(codigo);
        if (codigo == 0) {
          this.globales.mostrarAlerta("No se logró consultar el código de asegurado, intente nuevamente mas tarde. | SISE", "warning");
        } else {
          this.aplicarPago(datosPago, datosBin, codigo);
        }
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Consultar Código de Asegurado | SISE", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-aplicacion.component.ts", "consultarCodigoAsegurado", "Gestion/SGesGestion.svc/pago/masivos/codigo/asegurado/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public aplicarPago(datosPago, datosBin, codigoPagador) {

    var longitud = datosPago.Aplicacion.length;

    var pago = {
      Canal: "USR" + datosPago.Plataforma,
      CodPagador: codigoPagador,
      Cuotas: datosPago.Cuotas,
      NroTarjeta: datosPago.Bin,
      NroAutorizacion: longitud == 1 ? datosPago.CodigoAutenticacion : datosPago.CodigoAutenticacion + this.globales.obtenerHora(""),
      CodBanco: datosBin.CodigoBanco,
      CodConducto: datosBin.CodigoConducto,
      NroVoucher: datosPago.Voucher,
      FechaVoucher: this.globales.obtenerFechaVoucher(datosPago.FechaTransaccion),
      ApoderadoTarjeta: datosPago.Apoderado,
      IdPvs: datosPago.IdPv + "$" + datosPago.Total
    };

    console.log(pago);

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/masivos/aplicacion/pago", pago).subscribe(
      (res: any) => {
        this.spinner.hide();
        console.log(res);
        var aplicacion = this.globales.obtenerRespuestaAplicacionPago(res);
        if (aplicacion.estado == 0) {
          this.globales.mostrarAlerta("El pago no se pudo aplicar, el servicio no esta disponible u ocurrió un error en la manipulación de datos. | " + aplicacion.descripcion, "warning");
          this.conexion.error("masivos-aplicacion.component.ts", "aplicarPago", "Gestion/SGesGestion.svc/pago/masivos/aplicacion/pago", "500", "Gestion/SGesGestion.svc/pago/masivos/aplicacion/pago/IdFactura=" + datosPago.IdFactura, aplicacion.descripcion, this.usuario.IdUsuario, this.usuario.Nombre);
        } else {
          this.guardarAplicacionPago(datosPago);
        }
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Aplicar Pago | SISE", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-aplicacion.component.ts", "aplicarPago", "Gestion/SGesGestion.svc/pago/masivos/aplicacion/pago", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }


  public guardarAplicacionPago(datosPago) {

    for (let cuota of datosPago.Aplicacion) {
      if (cuota.cuota == datosPago.Cuotas) {
        cuota.estado = 1;
      }
    }

    var factura = {
      Identificador: 5,
      IdFactura: datosPago.IdFactura,
      Numero: 0,
      Comercio: 0,
      Subtotal12: 0,
      Subtotal0: 0,
      Iva: 0,
      Total: 0,
      Estado: 0,
      UrlRetorno: 0,
      Aplicacion: JSON.stringify(datosPago.Aplicacion),
      Cliente: {
        IdCliente: 0
      }
    };
    console.log(factura);
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/factura/gestion", factura).subscribe(
      (res: any) => {
        this.spinner.hide();
        console.log(res);
        this.globales.mostrarAlerta("Cuota N° " + datosPago.Cuotas + " aplicada el pago exitosamente.", "success");
        this.listarPagos();
        $('#ModalPagos').modal('toggle');
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Consultar BIN", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-aplicacion.component.ts", "guardarAplicacionPago", "Gestion/SGesGestion.svc/pago/factura/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }


  public verificarEstado(json) {
    var estado = true;
    var datos = JSON.parse(json);
    var longitud = datos.length;
    var totalEstado = 0;
    for (let dat of datos) {
      if (dat.estado) {
        totalEstado++;
      }
    }

    if (longitud == totalEstado) {
      estado = false;
    }
    return estado;
  }
}
