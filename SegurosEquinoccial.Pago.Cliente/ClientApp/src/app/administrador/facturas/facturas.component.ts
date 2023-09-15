import { Component, OnInit } from '@angular/core';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../../servicio/api/api.service';
import { GridComponent, GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { GroupResult, groupBy, DataResult, GroupDescriptor, process, State } from '@progress/kendo-data-query';
declare var $: any;

@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.component.html',
  styleUrls: ['./facturas.component.css'],
  providers: [GlobalesPipe]
})
export class FacturasComponent implements OnInit {

  usuario: any;
  lstFacturas = [];
  factura: any = [];
  lstClientes = [];
  cliente: any = { Apellido: null, Aplicacion: { Nombre: null, LogoPrimario: null, LogoSecundario: null }, Email: null, Estado: 0, IdCliente: 0, Identificacion: null, Identificador: 0, PrimerNombre: null, SegundoNombre: null, Telefono: null };
  clienteSeleccionado = {};

  public gruposFactura: GroupDescriptor[] = [{ field: 'Cliente.NombreCompleto' }];
  public tablaGestion: State = {
    skip: 0,
    take: 10
  };
  public tablaFactura: GridDataResult = process(this.lstFacturas, this.tablaGestion);
  public fmrFactura = {
    Identificador: 0,
    IdFactura: 0,
    Numero: "",
    Comercio: "",
    Subtotal12: "0",
    Subtotal0: "0",
    Iva: "0",
    Total: "0",
    Estado: 0,
    Cliente: {
      IdCliente: 0,
    },
    UrlRetorno: "",
  }

  public modalFactura = {
    Titulo: "",
    BtnGuardar: false,
    BtnModificar: false,
  }

  public tabs = {
    Factura: {
      Estado: true,
      Estilo: "active"
    },
    Cliente: {
      Estado: false,
      Estilo: ""
    }
  };

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, public globales: GlobalesPipe) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.listarFacturas();
  }

  public listarFacturas() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/factura/listar/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstFacturas = res;
        this.tablaFactura = process(this.lstFacturas, this.tablaGestion);
        this.cargarFactura();
        this.listarClientes();
        console.log(res)
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Facturas", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("factura.component.ts", "listarFacturas", "Gestion/SGesGestion.svc/pago/factura/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public listarClientes() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/cliente/listar/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstClientes = res;
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Clientes", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("factura.component.ts", "listarClientes", "Gestion/SGesGestion.svc/pago/cliente/listar/completo", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public agregarFactura() {
    this.fmrFactura.Cliente.IdCliente = this.cliente.IdCliente;

    if (this.validarFormularioFactura() == true) {
      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/factura/gestion", this.fmrFactura).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalGestionFactura').modal('toggle');
          this.globales.notificacion("Factura Generada con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarFacturas();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Agregar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("factura.component.ts", "agregarFactura", "Gestion/SGesGestion.svc/pago/factura/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public modificarFactura() {
    this.fmrFactura.Cliente.IdCliente = this.cliente.IdCliente;

    if (this.validarFormularioFactura() == true) {
      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/factura/gestion", this.fmrFactura).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalGestionFactura').modal('toggle');
          this.globales.notificacion("Factura Modificada con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarFacturas();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Modificar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("factura.component.ts", "modificarFactura", "Gestion/SGesGestion.svc/pago/factura/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public calcularValores() {
    var Subtotal12 = parseFloat(this.fmrFactura.Subtotal12 + "");
    var Subtotal0 = parseFloat(this.fmrFactura.Subtotal0 + "");
    var Iva = Subtotal12 * 0.12;
    var Total = Subtotal12 + Subtotal0 + Iva;

    this.fmrFactura.Iva = Math.round(Iva * 100) / 100 + "";
    this.fmrFactura.Total = Math.round(Total * 100) / 100 + "";
  }

  public limpiarFormulario() {
    this.fmrFactura = {
      Identificador: 0,
      IdFactura: 0,
      Numero: "",
      Comercio: "",
      Subtotal12: "0",
      Subtotal0: "0",
      Iva: "0",
      Total: "0",
      Estado: 0,
      Cliente: {
        IdCliente: 0,
      },
      UrlRetorno: "",
    }
    this.clienteSeleccionado = {};
  }

  public abrirModalDetalleFactura(datos) {
    this.factura = datos;
    $('#ModalDetallesFactura').modal('show');
  }

  public abrirModalAgregarFactura() {
    this.limpiarFormulario();
    this.modalFactura.Titulo = "Agregar Factura";
    this.modalFactura.BtnGuardar = true;
    this.modalFactura.BtnModificar = false;
    this.fmrFactura.Identificador = 1;
    $('#ModalGestionFactura').modal('show');
  }

  public abrirModalModificarFactura(datos) {
    this.limpiarFormulario();
    this.modalFactura.Titulo = "Modificar Factura";
    this.modalFactura.BtnGuardar = false;
    this.modalFactura.BtnModificar = true;
    this.fmrFactura.Identificador = 3;
    this.fmrFactura.IdFactura = datos.IdFactura;
    this.fmrFactura.Comercio = datos.Comercio;
    this.fmrFactura.UrlRetorno = datos.UrlRetorno;
    this.fmrFactura.Subtotal12 = parseFloat(datos.Subtotal12) + "";
    this.fmrFactura.Subtotal0 = parseFloat(datos.Subtotal0) + "";
    this.fmrFactura.Iva = parseFloat(datos.Iva) + "";
    this.fmrFactura.Total = parseFloat(datos.Total) + "";
    this.cliente = datos.Cliente;
    $('#ModalGestionFactura').modal('show');
  }

  public validarFormularioFactura() {
    var estado = false;
    if (this.fmrFactura.Comercio.trim() == "") {
      this.globales.notificacion("Ingresar Tipo de Comercio", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrFactura.UrlRetorno.trim() == "") {
      this.globales.notificacion("Ingresar URL de retorno", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (parseFloat(this.fmrFactura.Subtotal12) == 0) {
      this.globales.notificacion("Ingresar Subtotal (12%)", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (parseFloat(this.fmrFactura.Iva) == 0) {
      this.globales.notificacion("Ingresar Iva", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (parseFloat(this.fmrFactura.Total) == 0) {
      this.globales.notificacion("Ingresar Total", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (this.fmrFactura.Cliente.IdCliente == 0) {
      this.globales.notificacion("Seleccionar un Cliente", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else if (parseFloat(this.fmrFactura.Total) < 10) {
      this.globales.notificacion("El valor de la factura debe ser mayor a $ 10.00", "error", "top-end", "#E74C3CE6", "#FFF");
      estado = false
    } else {
      estado = true
    }
    return estado;
  }

  public cambioEstadoFactura(state: DataStateChangeEvent): void {
    this.tablaGestion = state;
    this.tablaFactura = process(this.lstFacturas, this.tablaGestion);
  }

  public cambioEstadoGrupoFactura(groups: GroupDescriptor[]): void {
    this.gruposFactura = groups;
    this.cargarFactura();
  }

  private cargarFactura(): void {
    this.tablaFactura = process(this.lstFacturas, { group: this.gruposFactura });
  }

  public gestionTabs(valor) {
    if (valor == "Factura") {
      this.tabs.Factura.Estado = true;
      this.tabs.Cliente.Estado = false;
      this.tabs.Factura.Estilo = "active";
      this.tabs.Cliente.Estilo = "";
    } else if (valor == "Cliente") {
      this.tabs.Factura.Estado = false;
      this.tabs.Cliente.Estado = true;
      this.tabs.Factura.Estilo = "";
      this.tabs.Cliente.Estilo = "active";
    }
  }

}
