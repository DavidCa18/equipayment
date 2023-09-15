import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxMaskModule } from 'ngx-mask'
import { NgxBarcodeModule } from 'ngx-barcode';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppComponent } from './app.component';
import { PagoComponent } from './cliente/pago/pago.component';
import { PagoResultadoComponent } from './cliente/pago-resultado/pago-resultado.component';
import { ApiService } from './servicio/api/api.service';
import { HeaderComponent } from './cliente/complementos/header/header.component';
import { PagoBotonDatafastComponent } from './cliente/pago-boton/pago-boton.component';
import { GeneradorDinamicoScriptsService } from './servicio/scripts/scripts.service';
import { PagoErrorComponent } from './cliente/pago-error/pago-error.component';
import { PagoBotonPayphoneComponent } from './cliente/pago-boton-payphone/pago-boton-payphone.component';
import { GlobalesPipe } from './metodos/globales.pipe';
import { PagoResultadoPayphoneComponent } from './cliente/pago-resultado-payphone/pago-resultado-payphone.component';
import { InicioComponent } from './administrador/inicio/inicio.component';
import { AplicacionComponent } from './administrador/aplicacion/aplicacion.component';
import { SesionService } from './servicio/sesion/sesion.service';
import { ReversosComponent } from './administrador/reversos/reversos.component';
import { NavbarComponent } from './administrador/complementos/navbar/navbar.component';
import {PopoverModule} from "ngx-popover";
import { GridModule, ExcelModule } from '@progress/kendo-angular-grid';
import { ColorPickerModule } from 'ngx-color-picker';
import { PagosComponent } from './administrador/pagos/pagos.component';
import { FacturasComponent } from './administrador/facturas/facturas.component';
import { ClienteComponent } from './administrador/cliente/cliente.component';
import { UsuarioComponent } from './administrador/usuario/usuario.component';
import { MasivosComponent } from './administrador/masivos/masivos.component';
import { SidebarComponent } from './administrador/complementos/sidebar/sidebar.component';
import { MasivosAplicacionComponent } from './administrador/masivos-aplicacion/masivos-aplicacion.component';
import { MasivosGestionComponent } from './administrador/masivos-gestion/masivos-gestion.component';
import { MasivosListaComponent } from './administrador/masivos-lista/masivos-lista.component';

@NgModule({
  declarations: [
    AppComponent,
    PagoComponent,
    PagoResultadoComponent,
    HeaderComponent,
    PagoBotonDatafastComponent,
    PagoErrorComponent,
    PagoBotonPayphoneComponent,
    GlobalesPipe,
    PagoResultadoPayphoneComponent,
    InicioComponent,
    AplicacionComponent,
    ReversosComponent,
    NavbarComponent,
    PagosComponent,
    FacturasComponent,
    ClienteComponent,
    UsuarioComponent,
    MasivosComponent,
    SidebarComponent,
    MasivosAplicacionComponent,
    MasivosGestionComponent,
    MasivosListaComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    NgxMaskModule.forRoot(),
    NgxBarcodeModule,
    GridModule,
    ExcelModule,
    DropDownsModule,
    ColorPickerModule,
    PopoverModule,
    DateInputsModule,
    NgxPaginationModule,
    RouterModule.forRoot([
      { path: '', component: PagoComponent, pathMatch: 'full' },
      { path: 'pago/resultado', component: PagoResultadoComponent },
      { path: 'pago/resultado/payphone/:id', component: PagoResultadoPayphoneComponent },
      { path: 'pago/informacion/:id', component: PagoErrorComponent },
      { path: 'administrador/inicio', component: InicioComponent },
      { path: 'administrador/aplicacion', component: AplicacionComponent },
      { path: 'administrador/reversos', component: ReversosComponent },
      { path: 'administrador/pagos', component: PagosComponent },
      { path: 'administrador/facturas', component: FacturasComponent },
      { path: 'administrador/clientes', component: ClienteComponent },
      { path: 'administrador/masivos/envio', component: MasivosComponent },
      { path: 'administrador/masivos/gestion', component: MasivosGestionComponent },
      { path: 'administrador/masivos/aplicacion', component: MasivosAplicacionComponent },
      { path: 'administrador/masivos/lista', component: MasivosListaComponent },

    ])
  ],
  providers: [ApiService, GeneradorDinamicoScriptsService, SesionService],
  bootstrap: [AppComponent],
  entryComponents: [HeaderComponent, PagoBotonDatafastComponent, PagoBotonPayphoneComponent]
})
export class AppModule { }
