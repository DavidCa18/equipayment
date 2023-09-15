import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../../servicio/api/api.service';
import { GridComponent, GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { GroupResult, groupBy, DataResult, GroupDescriptor, process, State } from '@progress/kendo-data-query'
declare var $: any;

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css'],
  providers: [GlobalesPipe]
})
export class ClienteComponent implements OnInit {

  usuario: any;
  lstClientes = [];
  cliente: any = { Apellido: null, Aplicacion: { Nombre: null, LogoPrimario: null, FondoPrimario: null, LogoSecundario: null }, Email: null, Estado: 0, IdCliente: 0, Identificacion: null, Identificador: 0, NombreCompleto: null, PrimerNombre: null, SegundoNombre: null, Telefono: null };
  lstAplicaciones = [];
  aplicaciones = { ColorPrimario: null, ColorSecundario: null, Estado: 0, FondoPrimario: null, FondoSecundario: null, IdAplicacion: 0, Identificador: 0, LogoPrimario: null, LogoPrimarioTamano: null, LogoSecundario: null, LogoSecundarioTamano: null, Nombre: null };
  aplicacionSeleccionada = {};
  public gruposCliente: GroupDescriptor[] = [{ field: 'Aplicacion.Nombre' }];
  public tablaGestion: State = {
    skip: 0,
    take: 10
  };
  public tablaCliente: GridDataResult = process(this.lstClientes, this.tablaGestion);
  public fmrCliente = {
    Identificador: 0,
    IdCliente: 0,
    Identificacion: "",
    PrimerNombre: "",
    SegundoNombre: "",
    Apellido: "",
    Email: "",
    Telefono: "",
    Estado: 0,
    Aplicacion: {
      IdAplicacion: 0
    }
  };

  public modalCliente = {
    Titulo: "",
    BtnGuardar: false,
    BtnModificar: false,
  }
  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, public globales: GlobalesPipe) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.listarClientes();
  }

  public listarClientes() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/cliente/listar/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstClientes = res;
        this.tablaCliente = process(this.lstClientes, this.tablaGestion);
        this.cargarCliente();
        this.listarAplicaciones();
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Clientes", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("cliente.component.ts", "listarClientes", "Gestion/SGesGestion.svc/pago/cliente/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public listarAplicaciones() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstAplicaciones = res;
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Aplicaciones", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("cliente.component.ts", "listarAplicaciones", "Gestion/SGesGestion.svc/pago/aplicacion/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public agregarCliente() {
    this.fmrCliente.Aplicacion.IdAplicacion = this.aplicaciones.IdAplicacion;

    if (this.validarFormularioCliente() == true) {

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/cliente/gestion", this.fmrCliente).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalGestionCliente').modal('toggle');
          this.globales.notificacion("Cliente Registrado con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarClientes();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Agregar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("cliente.component.ts", "agregarCliente", "Gestion/SGesGestion.svc/pago/cliente/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public modificarCliente() {
    this.fmrCliente.Aplicacion.IdAplicacion = this.aplicaciones.IdAplicacion;

    if (this.validarFormularioCliente() == true) {

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/cliente/gestion", this.fmrCliente).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalGestionCliente').modal('toggle');
          this.globales.notificacion("Cliente Modificado con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarClientes();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Modificar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("cliente.component.ts", "modificarCliente", "Gestion/SGesGestion.svc/pago/cliente/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public limpiarFormulario() {
    this.fmrCliente = {
      Identificador: 0,
      IdCliente: 0,
      Identificacion: "",
      PrimerNombre: "",
      SegundoNombre: "",
      Apellido: "",
      Email: "",
      Telefono: "",
      Estado: 0,
      Aplicacion: {
        IdAplicacion: 0
      }
    };
    this.aplicacionSeleccionada = {};
  }

  public abrirModalDetalleCliente(datos) {
    this.cliente = datos;
    $('#ModalDetallesCliente').modal('show');
  }

  public abrirModalAgregarCliente() {
    this.limpiarFormulario();
    this.modalCliente.Titulo = "Agregar Cliente";
    this.modalCliente.BtnGuardar = true;
    this.modalCliente.BtnModificar = false;
    this.fmrCliente.Identificador = 1;
    $('#ModalGestionCliente').modal('show');
  }

  public abrirModalModificarCliente(datos) {
    this.limpiarFormulario();
    this.modalCliente.Titulo = "Modificar Cliente";
    this.modalCliente.BtnGuardar = false;
    this.modalCliente.BtnModificar = true;
    this.fmrCliente.Identificador = 3;
    this.fmrCliente.IdCliente = datos.IdCliente;
    this.fmrCliente.Identificacion = datos.Identificacion;
    this.fmrCliente.PrimerNombre = datos.PrimerNombre;
    this.fmrCliente.SegundoNombre = datos.SegundoNombre;
    this.fmrCliente.Apellido = datos.Apellido;
    this.fmrCliente.Email = datos.Email;
    this.fmrCliente.Telefono = datos.Telefono;
    this.aplicaciones = datos.Aplicacion;
    $('#ModalGestionCliente').modal('show');
  }

  public validarFormularioCliente() {
    var estado = false;
    if (this.fmrCliente.Identificacion.trim() == "") {
      this.globales.notificacion("Ingresar Número de Identificación", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    }else if (this.fmrCliente.Identificacion.trim().length != 10 && this.fmrCliente.Identificacion.trim().length != 13) {
      this.globales.notificacion("Ingresar Número de Identificación Válido", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.PrimerNombre.trim() == "") {
      this.globales.notificacion("Ingresar Primer Nombre", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.SegundoNombre.trim() == "") {
      this.globales.notificacion("Ingresar Segundo Nombre", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.Apellido.trim() == "") {
      this.globales.notificacion("Ingresar Apellido", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.Email.trim() == "") {
      this.globales.notificacion("Ingresar Correo Electrónico", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (!/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(this.fmrCliente.Email.trim())) {
      this.globales.notificacion("Ingresar Correo Electrónico Válido", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.Telefono.trim() == "") {
      this.globales.notificacion("Ingresar Número de Teléfono", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrCliente.Aplicacion.IdAplicacion == 0) {
      this.globales.notificacion("Seleccionar una Aplicación / Plataforma", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else {
      estado = true
    }
    return estado;
  }

  public cambioEstadoCliente(state: DataStateChangeEvent): void {
    this.tablaGestion = state;
    this.tablaCliente = process(this.lstClientes, this.tablaGestion);
  }

  public cambioEstadoGrupoCliente(groups: GroupDescriptor[]): void {
    this.gruposCliente = groups;
    this.cargarCliente();
  }

  private cargarCliente(): void {
    this.tablaCliente = process(this.lstClientes, { group: this.gruposCliente });
  }
}
