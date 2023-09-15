import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { SesionService } from '../../servicio/sesion/sesion.service';
declare var $: any;

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

  usuario: any;


  public fmrContrasena = {
    contrasenaActual: '',
    contrasena: '',
    contrasenaConfirmacion: ''
  }

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, private globales: GlobalesPipe) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    console.log(this.usuario)
  }

  public abrirModificar() {
    $('#modalFoto').modal('show');
  }

  public modificarDatos() {

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/usuario/gestion", this.usuario).subscribe(
      (res: any) => {
        this.spinner.hide();
        $('#modalFoto').modal('toggle');

        this.globales.notificacion("Datos Actualizados con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Actualizados Datos Usuario", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("usuario.component.ts", "modificarDatos", "Gestion/SGesGestion.svc/pago/usuario/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public modificarContrasena() {

  }

}
