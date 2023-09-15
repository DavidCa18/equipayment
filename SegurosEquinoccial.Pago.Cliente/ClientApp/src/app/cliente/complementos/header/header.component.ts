import { Component, Input } from '@angular/core';

@Component({
  selector: 'header-componente',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  @Input() logoPrimario: string = "light-logo.png";
  @Input() logoSecundario: string = "light-logo.png";
  @Input() fondoPrimario: string = "#000000";
  @Input() colorPrimario: string = "#FFFFFF";
  @Input() fondoSecundario: string = "#000000";
  @Input() colorSecundario: string = "#FFFFFF";
  @Input() logoPrimarioTamano: string = "#FFFFFF";
  @Input() logoSecundarioTamano: string = "#FFFFFF";

  constructor() {
  }

}
