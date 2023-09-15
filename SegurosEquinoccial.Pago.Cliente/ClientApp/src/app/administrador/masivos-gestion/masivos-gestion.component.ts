import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { GlobalesPipe } from '../../metodos/globales.pipe';

@Component({
  selector: 'app-masivos-gestion',
  templateUrl: './masivos-gestion.component.html',
  styleUrls: ['./masivos-gestion.component.css']
})
export class MasivosGestionComponent implements OnInit {

  public usuario: any;
  public lstHistorial = [];
  public lstErrores = [];
  public mensajeSpinner = "Procesando Datos...";
  public globales: GlobalesPipe = new GlobalesPipe();
  public fecha = { inicio: null, fin: null };
  public p: any;
  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.fecha.inicio = new Date();
    this.fecha.fin = new Date();
    this.listarHistorialTransacciones();
  }

  public listarHistorialTransacciones() {

    var filtros = {
      FechaInicio: this.globales.obtenerFormatoFecha(this.fecha.inicio, "-"),
      FechaFin: this.globales.obtenerFormatoFecha(this.fecha.fin, "-"),
      Cadena: " AND Estado = 1 AND Aplicacion = 15 "
    }
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/historial/transacciones/listar/parametros", filtros).subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstHistorial = res;
        console.log(this.lstHistorial);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Historial de Transacciones", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("masivos-gestion.component.ts", "listarHistorialTransacciones", "Gestion/SGesGestion.svc/pago/historial/transacciones/listar/parametros", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public enviarLinks() {

    if (this.validaciones()) {

      var lstDocumentoClientes = [];
      for (let cliente of this.lstHistorial) {
        lstDocumentoClientes.push({
          codigo: 15,
          identificacion: cliente.Identificacion,
          nombre: cliente.Apellido,
          email: cliente.Email,
          celular: cliente.Telefono,
          ramo: cliente.Comercio,
          poliza: cliente.Poliza,
          idpv: cliente.IdPv,
          deuda: cliente.Total,
          cobranza: 0,
          saldo: cliente.Total,
          cuota: cliente.Cuotas,
          aplicacion: cliente.FacturaAplicacion,
          valores: this.globales.calculoValoresPagar(parseFloat(cliente.Total))
        });
      }

      var datos = {
        Masivos: JSON.stringify(lstDocumentoClientes)
      };

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/masivos/links", datos).subscribe(
        (res: any) => {
          this.spinner.hide();
          console.log(res);
          this.gestionHistorialTransacciones();
        },
        err => {
          this.spinner.hide();
          this.globales.mostrarAlerta("Error al procesar los datos del archivo", "warning");
          this.conexion.error("masivos-gestion.component.ts", "generarLinks", "Gestion/SGesGestion.svc/pago/masivos/links", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    } else {
      this.globales.mostrarAlerta("Existen datos mal ingresados en la lista de clientes.", "warning");
    }

  }

  public gestionHistorialTransacciones(){

    var lstIds = [];
    for(let cliente of this.lstHistorial){
      lstIds.push({
        idHistorialTransacciones: cliente.IdHistorialTransacciones
      });
    }

    var datos = {
      Identificador: 2,
      Identificacion: "",
      PrimerNombre: "",
      SegundoNombre: "",
      Apellido: "",
      Email: "",
      Telefono: "",
      IdPv: "",
      Poliza: "",
      Aplicacion: "",
      Diferidos: "",
      TipoDiferido: "",
      Comercio: "",
      Subtotal12: "",
      Subtotal0: "",
      Iva: "",
      Total: "",
      UrlRetorno: "",
      Mensaje: "",
      Tipo: "",
      Trama: JSON.stringify(lstIds),
      Cuotas: "",
      FacturaAplicacion: ""
    };

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/historial/transacciones/gestion", datos).subscribe(
      (res: any) => {
        this.spinner.hide();
        console.log(res);
        this.globales.mostrarAlerta("Transacción Exitosa", "success");
        this.listarHistorialTransacciones();
      },
      err => {
        this.spinner.hide();
        this.globales.mostrarAlerta("Error al cambiar de estado de los clientes", "warning");
        this.conexion.error("masivos-gestion.component.ts", "gestionHistorialTransacciones", "Gestion/SGesGestion.svc/pago/historial/transacciones/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );

  }

  public validaciones() {

    var estado = true;
    this.lstErrores = [];
    for (let cliente of this.lstHistorial) {

      if (!(cliente.Identificacion.length >= 5 && cliente.Identificacion.length <= 15)) {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (!/^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/.test(cliente.Email)) {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (cliente.Apellido == "") {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (cliente.Comercio == "") {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (cliente.Poliza == "") {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (cliente.IdPv == "") {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (cliente.Total == "") {
        estado = false;
        this.lstErrores.push(cliente);
      } else if (parseFloat(cliente.Total) <= 0) {
        estado = false;
        this.lstErrores.push(cliente);
      }
    }
    console.log(this.lstErrores)
    return estado;
  }

}
