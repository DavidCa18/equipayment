import { Component, OnInit } from '@angular/core';
import { SesionService } from '../../../servicio/sesion/sesion.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  usuario: any;
  public url = "";
  constructor(private sesion: SesionService, private router: Router) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.url = this.router.url;
   
  }

}
