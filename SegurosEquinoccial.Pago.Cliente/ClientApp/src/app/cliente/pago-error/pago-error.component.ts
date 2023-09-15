import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../servicio/api/api.service';

@Component({
  selector: 'app-pago-error',
  templateUrl: './pago-error.component.html',
  styleUrls: ['./pago-error.component.css']
})
export class PagoErrorComponent implements OnInit {

  public errores: any = {}

  private parametro: any;
  public idAplicacion = 0;
  public idPago = 0;
  public aplicacion = {
    LogoPrimario: "assets/images/logos/equinocial-light.png",
    LogoSecundario: "assets/images/logos/light-vertical-logo.png",
    ColorPrimario: "#FFFFFF",
    FondoPrimario: "#3F3F3F"
  }

  constructor(private rutaActiva: ActivatedRoute, private conexion: ApiService) {
  }
  ngOnInit() {

    this.parametro = atob(this.rutaActiva.snapshot.params.id);
    var lstCadena = this.parametro.split(",");
    console.log(this.parametro);
    if (lstCadena[1] == "0") {
      this.idAplicacion = lstCadena[1];
      this.idPago = lstCadena[2];
      this.buscarMensaje(lstCadena[0]);
    } else {
      this.idAplicacion = lstCadena[1];
      this.idPago = lstCadena[2];
      this.buscarDatosAplicacion();
    }
  }

  public buscarDatosAplicacion() {
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.idAplicacion).subscribe(
      (res: any) => {
        this.aplicacion.LogoPrimario = res.LogoPrimario;
        this.aplicacion.LogoSecundario = res.LogoSecundario;
        this.aplicacion.FondoPrimario = res.FondoPrimario;
        this.aplicacion.ColorPrimario = res.ColorPrimario;
        this.listarPago();


      },
      err => {
        this.conexion.error("pago-error.component.ts", "buscarDatosAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.idAplicacion, err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public listarPago() {
    this.conexion.get("Gestion/SGesGestion.svc/pago/pago/estado/listar/" + this.idPago).subscribe(
      (res: any) => {
        var codigo_ = "0";
        var datos: any;

        if (res.Plataforma == "DATAFAST") {
          if (res.Estado == 2) {
            datos = JSON.parse(res.ResultadoTrama);
            codigo_ = datos.result.code;
          } else if (res.Estado == 4) {
            codigo_ = "32898";
          } else if (res.Estado == 5) {
            codigo_ = "98494";
          } else if (res.Estado == 6) {
            codigo_ = "68979";
          } else {
            datos = JSON.parse(res.ResultadoTrama);
            if (datos.resultDetails == undefined) {
              codigo_ = datos.result.code;
            } else {
              datos = JSON.parse(res.ResultadoTrama);
              if (datos.resultDetails.AcquirerResponse == undefined) {
                codigo_ = datos.result.code;
              } else {
                codigo_ = datos.resultDetails.AcquirerResponse;
              }
            }
          }
        } else {
          codigo_ = res.ResultadoCodigo;
        }

        console.log(codigo_)
        this.buscarMensajeAplicacion(codigo_);
      },
      err => {
        console.log(err);
        this.conexion.error("pago-error.component.ts", "listarPago", "Gestion/SGesGestion.svc/pago/pago/estado/listar/" + this.idPago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public buscarMensaje(codigo) {
    this.conexion.get("Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo).subscribe(
      (res: any) => {
        this.errores = res;
      },
      err => {
        console.log(err);
        this.conexion.error("pago-error.component.ts", "buscarMensaje", "Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }

  public buscarMensajeAplicacion(codigo) {
    this.conexion.get("Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo).subscribe(
      (res: any) => {
        this.errores = res;
      },
      err => {
        console.log(err);
        this.conexion.error("pago-error.component.ts", "buscarMensajeAplicacion", "Gestion/SGesGestion.svc/pago/mensajes/listar/" + codigo, err.status, err.url, err.error, 0, "PAGO CLIENTE");
      }
    );
  }
}
