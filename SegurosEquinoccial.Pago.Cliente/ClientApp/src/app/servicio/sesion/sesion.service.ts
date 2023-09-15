import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js'
import { Router } from '@angular/router';

@Injectable()
export class SesionService {

  private keyEncriptacion: string = "SegurosEquinoccial//Pago//BackEnd//{2019}";
  private keyPago: string = "l:ke";

  constructor(private router: Router) { }

  cerrarSesion() {
    localStorage.removeItem(this.keyPago);
    this.router.navigate(['/administrador/inicio']);
  }

  iniciarSesion(usuario: any) {
    var datosCifrados = CryptoJS.AES.encrypt(JSON.stringify(usuario), this.keyEncriptacion).toString();
    localStorage.setItem(this.keyPago, datosCifrados);

    var rol = usuario.Rol;

    if (rol == "ADMINISTRADOR") {
      this.router.navigate(['/administrador/pagos']);
    }
  }

  verificarCredencialesRutas() {
    var datos: any = localStorage.getItem(this.keyPago);
    if (datos === null) {
      this.router.navigate(['/administrador/inicio']);
    }
  }

  obtenerDatos() {
    var datosPlanos: any;
    var datos = localStorage.getItem(this.keyPago);
    if (datos === null) {
    } else {
      var bytes = CryptoJS.AES.decrypt(datos, this.keyEncriptacion);
      datosPlanos = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
    }
    return datosPlanos;
  }

}
