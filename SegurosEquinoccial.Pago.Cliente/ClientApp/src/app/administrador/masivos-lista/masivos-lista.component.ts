import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { ApiService } from '../../servicio/api/api.service';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-masivos-lista',
  templateUrl: './masivos-lista.component.html',
  styleUrls: ['./masivos-lista.component.css']
})
export class MasivosListaComponent implements OnInit {

  public usuario: any;
  public mensajeSpinner = "Procesando Datos...";
  public globales: GlobalesPipe = new GlobalesPipe();
  public filtros = { inicio: null, fin: null, identificacion: "" };
  public lstPagosExitosos = [];
  public lstPagosErroneos = [];
  public lstPagosOtros = [];
  public p: any;
  public q: any;
  public r: any;
  public urlRecibo = "";
  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService,  private dom: DomSanitizer) { }

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
      Cadena: this.filtros.identificacion == "" ? " AND Codigo = 15 " : " AND Codigo = 15 AND Identificacion = '" + this.filtros.identificacion + "' "
    }

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/pago/listar/completo", datos).subscribe(
      (res: any) => {
        this.spinner.hide();

        for (let pago of res) {
          if (pago.Estado == 2) {
            this.lstPagosExitosos.push(pago);
          } else if (pago.Estado == 3) {
            this.lstPagosErroneos.push(pago);
          } else {
            this.lstPagosOtros.push(pago);
          }
        }
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Pagos", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-lista.component.ts", "listarPagos", "Gestion/SGesGestion.svc/pago/pago/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public enviarLinks(lista) {

    var lstDocumentoClientes = [];
    for (let cliente of lista) {
      lstDocumentoClientes.push({
        codigo: 15,
        idpago: cliente.IdPago,
        nombre: cliente.Factura.Cliente.Apellido,
        ramo: cliente.Factura.Comercio,
        poliza: "00000",
        deuda: cliente.Factura.Total,
        email: cliente.Factura.Cliente.Email
      });
    }

    var datos = {
      Masivos: JSON.stringify(lstDocumentoClientes)
    };

    console.log(datos);

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/masivos/envio/email", datos).subscribe(
      (res: any) => {
        this.spinner.hide();
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.mostrarAlerta("Error al procesar enviar links de pago", "warning");
        this.conexion.error("masivos-lista.component.ts", "enviarLinks", "Gestion/SGesGestion.svc/pago/masivos/envio/email", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );

  }

  public buscarVoucher(datosPago){
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/pago/consultar/recibo/" + datosPago.IdPago).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.urlRecibo = "data:application/pdf;base64," + res;
      },
      err => {
        this.spinner.hide();
        console.log(err);
        this.globales.notificacion("Error con el servidor de datos:<br>Hablitar pagos fallidos", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("admin-pagos.component.ts", "habilitarPagosFallidos", "Gestion/SGesGestion.svc/enviar/pago/recibo/", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
      }
    );
  }

  public descargarRecibo() {
    return this.dom.bypassSecurityTrustUrl(this.urlRecibo);
  }

}
