import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SesionService } from '../../../servicio/sesion/sesion.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  usuario: any;
  public url = "";

  constructor(private sesion: SesionService, private router: Router) { }

  ngAfterViewInit() {

  }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.url = this.router.url;
  }

  public cerrarSesion() {
    this.sesion.cerrarSesion();
  }

}
