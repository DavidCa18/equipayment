import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../../servicio/api/api.service';
import { SesionService } from './../../servicio/sesion/sesion.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css'],
  providers: [GlobalesPipe]
})
export class InicioComponent implements OnInit {

  public fmrInicioSesion: FormGroup;

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, private globales: GlobalesPipe, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.fmrInicioSesion = this.formBuilder.group({
      Email: ["", [Validators.required, , Validators.minLength(5), Validators.email]],
      Contrasena: ["", [Validators.required]]
    });
  }

  public iniciarSesion(fmrValores: any) {

    if (this.fmrInicioSesion.controls.Email.status == "INVALID") {
      this.globales.notificacion("Ingresar Correo Electrónico", "error", 'top', "#fff", "#000");
    } else if (this.fmrInicioSesion.controls.Contrasena.status == "INVALID") {
      this.globales.notificacion("Ingresar Contraseña", "error", 'top', "#fff", "#000");
    } else {
      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/usuario/verificar", fmrValores).subscribe(
        (res: any) => {
          this.spinner.hide();
          if (res.IdUsuario == 0) {
            this.globales.notificacion("Credenciales Incorrectas", "warning", 'top', "#fff", "#000");
          } else {
            this.sesion.iniciarSesion(res);
          }
        },
        err => {
          this.spinner.hide();
          this.conexion.error("inicio.component.ts", "iniciarSesion", "Gestion/SGesGestion.svc/pago/usuario/verificar", err.status, err.url, err.error, 0, "INICIO SESIÓN");
          console.log(err);
        }
      );
    }
  }
}
