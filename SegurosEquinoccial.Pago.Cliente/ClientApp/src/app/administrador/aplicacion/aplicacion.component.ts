import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { GridComponent, GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { GroupResult, groupBy, DataResult, GroupDescriptor, process, State } from '@progress/kendo-data-query'
declare var $: any;

@Component({
  selector: 'app-aplicacion',
  templateUrl: './aplicacion.component.html',
  styleUrls: ['./aplicacion.component.css'],
  providers: [GlobalesPipe]
})
export class AplicacionComponent implements OnInit {

  usuario: any;
  lstAplicaciones = [];
  lstTipoTiempo = [{ codigo: 'years', texto: 'AÑOS' }, { codigo: 'months', texto: 'MESES' }, { codigo: 'weeks', texto: 'SEMANAS' }, { codigo: 'days', texto: 'DÍAS' }, { codigo: 'hours', texto: 'HORAS' }, { codigo: 'minutes', texto: 'MINUTOS' }];
  lstIdentificacion = [{ codigo: 1, texto: 'CÉDULA' }, { codigo: 2, texto: 'RUC' }, { codigo: 3, texto: 'CEDULA, RUC Y PASAPORTE' }]
  aplicacion: any = {};
  p: any;
  color: any;
  toggle: any;
  toggle2: any;
  toggle3: any;
  toggle4: any;
  public tablaGestion: State = {
    skip: 0,
    take: 10
  };
  public tablaAplicacion: GridDataResult = process(this.lstAplicaciones, this.tablaGestion);
  public LogoPrimario = null;
  public LogoSecundario = null;
  public fmrAplicacion = {
    Identificador: 0,
    IdAplicacion: 0,
    Nombre: "",
    LogoPrimario: "",
    LogoSecundario: "",
    NombreLogoPrimario: "",
    NombreLogoSecundario: "",
    ColorPrimario: "",
    ColorSecundario: "",
    FondoPrimario: "",
    FondoSecundario: "",
    LogoPrimarioTamano: "",
    LogoSecundarioTamano: "",
    Estado: 0,
    Identificacion: 0,
    Codigo: 0,

    MontoMaximo: "0",
    MontoMinimo: "0",
    VisualizacionBin: 1,
    CaducidadTiempoValor: null,
    CaducidadTiempo: "",
    Caducidad: "",
    Recurrencia: 0,
    Gracia: 0,
  };

  public modalAplicacion = {
    Titulo: "",
    BtnGuardar: false,
    BtnModificar: false,
  }

  public gestionPasos = {
    paso1: true,
    paso1Estilo: "current",
    paso2: false,
    paso2Estilo: "",
    paso3: false,
    paso3Estilo: ""
  }

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService, public globales: GlobalesPipe) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
    this.listarAplicaciones();
  }

  public listarAplicaciones() {
    this.spinner.show();
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/completo").subscribe(
      (res: any) => {
        this.spinner.hide();
        this.lstAplicaciones = res;
        this.tablaAplicacion = process(this.lstAplicaciones, this.tablaGestion);
        console.log(res);
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Listar Aplicaciones", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("aplicacion.component.ts", "listarAplicaciones", "Gestion/SGesGestion.svc/pago/aplicacion/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public agregarAplicacion() {

    if (this.validarFormularioCliente(1) == true) {
      this.obtenerCaducidad();

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/aplicacion/gestion", this.fmrAplicacion).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalGestionAplicacion').modal('toggle');
          this.globales.notificacion("Aplicación / Plataforma Registrada con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarAplicaciones();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Agregar Aplicación", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("aplicacion.component.ts", "agregarAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public modificarAplicacion() {

    if (this.validarFormularioCliente(2) == true) {

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/aplicacion/gestion", this.fmrAplicacion).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalModificacionAplicacion').modal('toggle');
          this.globales.notificacion("Datos Modificados con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarAplicaciones();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Modificar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("aplicacion.component.ts", "modificarAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public modificarDisenoAplicacion() {

    if (this.validarFormularioCliente(3) == true) {

      this.spinner.show();
      this.conexion.post("Gestion/SGesGestion.svc/pago/aplicacion/gestion", this.fmrAplicacion).subscribe(
        (res: any) => {
          this.spinner.hide();
          this.limpiarFormulario();
          $('#ModalDisenoAplicacion').modal('toggle');
          this.globales.notificacion("Diseño Modificado con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
          this.listarAplicaciones();
        },
        err => {
          this.spinner.hide();
          this.globales.notificacion("Error con el servidor de datos:<br>Modificar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
          this.conexion.error("aplicacion.component.ts", "modificarDisenoAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
          console.log(err);
        }
      );
    }
  }

  public cambioEstadoAplicacion(datos) {
    this.fmrAplicacion.Identificador = 2;
    this.fmrAplicacion.IdAplicacion = datos.IdAplicacion;

    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/aplicacion/gestion", this.fmrAplicacion).subscribe(
      (res: any) => {
        console.log(res);
        this.spinner.hide();
        this.globales.notificacion("Aplicación desactivada con Éxito", "success", "top-end", "#239B56", "#FFFFFFE6");
        this.listarAplicaciones();
      },
      err => {
        this.spinner.hide();
        this.globales.notificacion("Error con el servidor de datos:<br>Modificar Factura", "error", "top-end", "#E74C3CE6", "#FFF");
        this.conexion.error("aplicacion.component.ts", "cambioEstadoAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/gestion", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }

  public limpiarFormulario() {
    this.fmrAplicacion = {
      Identificador: 0,
      IdAplicacion: 0,
      Nombre: "",
      LogoPrimario: "",
      LogoSecundario: "",
      NombreLogoPrimario: "",
      NombreLogoSecundario: "",
      ColorPrimario: "",
      ColorSecundario: "",
      FondoPrimario: "",
      FondoSecundario: "",
      LogoPrimarioTamano: "",
      LogoSecundarioTamano: "",
      Estado: 0,
      Identificacion: 1,
      Codigo: 0,


      MontoMaximo: "0",
      MontoMinimo: "0",
      VisualizacionBin: 1,
      CaducidadTiempoValor: 0,
      CaducidadTiempo: "",
      Caducidad: "",
      Recurrencia: 0,
      Gracia: 0,
    }
  }

  public abrirModalDetalleAplicacion(datos) {
    this.aplicacion = datos;
    $('#ModalDetallesAplicacion').modal('show');
    this.gestionarPasos(1);
  }

  public abrirModalAgregarAplicacion() {
    this.limpiarFormulario();
    this.modalAplicacion.Titulo = "Agregar Aplicación";
    this.modalAplicacion.BtnGuardar = true;
    this.modalAplicacion.BtnModificar = false;
    this.fmrAplicacion.Identificador = 1;
    $('#ModalGestionAplicacion').modal({ backdrop: 'static', keyboard: false });
    this.gestionarPasos(1);
  }

  public abrirModalModificarAplicacion(datos) {
    this.limpiarFormulario();
    this.modalAplicacion.Titulo = "Modificar Aplicación";
    this.modalAplicacion.BtnGuardar = false;
    this.modalAplicacion.BtnModificar = true;
    this.fmrAplicacion.Identificador = 3;
    this.fmrAplicacion.IdAplicacion = datos.IdAplicacion;

    this.fmrAplicacion.Nombre = datos.Nombre;
    this.fmrAplicacion.Identificacion = datos.Identificacion;
    this.fmrAplicacion.MontoMaximo = datos.MontoMaximo;
    this.fmrAplicacion.MontoMinimo = datos.MontoMinimo;
    this.fmrAplicacion.Recurrencia = datos.Recurrencia;
    this.fmrAplicacion.Gracia = datos.Gracia;

    var aux = JSON.parse(datos.Caducidad);
    console.log(aux);

    this.fmrAplicacion.CaducidadTiempoValor = aux.Tiempo;
    this.fmrAplicacion.CaducidadTiempo = aux.Tipo;

    this.fmrAplicacion.Caducidad = datos.Caducidad;

    $('#ModalModificacionAplicacion').modal({ backdrop: 'static', keyboard: false });
    this.gestionarPasos(1);
  }

  public abrirModalDisenoAplicacion(datos) {
    this.limpiarFormulario();

    this.fmrAplicacion.Identificador = 7;
    this.fmrAplicacion.IdAplicacion = datos.IdAplicacion;
    this.fmrAplicacion.LogoPrimario = datos.LogoPrimario;
    this.fmrAplicacion.LogoSecundario = datos.LogoSecundario;
    this.fmrAplicacion.ColorPrimario = datos.ColorPrimario;
    this.fmrAplicacion.ColorSecundario = datos.ColorSecundario;
    this.fmrAplicacion.FondoPrimario = datos.FondoPrimario;
    this.fmrAplicacion.FondoSecundario = datos.FondoSecundario;
    this.fmrAplicacion.LogoPrimarioTamano = datos.LogoPrimarioTamano;
    this.fmrAplicacion.LogoSecundarioTamano = datos.LogoSecundarioTamano;


    $('#ModalDisenoAplicacion').modal({ backdrop: 'static', keyboard: false });

  }

  public validarFormularioCliente(tipo) {
    var estado = false;

    if (tipo == 1) {
      if (this.fmrAplicacion.Nombre.trim() == "") {
        this.globales.notificacion("Ingresar Nombre de la Aplicación", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Logo Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Logo Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Texto Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Texto Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.FondoPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Fondo Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.FondoSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Fondo Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoPrimarioTamano.trim() == "") {
        this.globales.notificacion("Seleccionar un tamañano para el logo primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoSecundarioTamano.trim() == "") {
        this.globales.notificacion("Seleccionar un tamañano para el logo secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorPrimario.trim() == this.fmrAplicacion.FondoPrimario.trim()) {
        this.globales.notificacion("El color del texto primario y el<br>color de fondo primario no pueden ser iguales", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorSecundario.trim() == this.fmrAplicacion.FondoSecundario.trim()) {
        this.globales.notificacion("El color del texto secundario y el<br>color de fondo secundario no pueden ser iguales", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else {
        estado = true
      }
    } else if (tipo == 2) {
      if (this.fmrAplicacion.Nombre.trim() == "") {
        this.globales.notificacion("Ingresar Nombre de la Plataforma", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.Caducidad.trim() == "") {
        this.globales.notificacion("Ingresar Caducidad de la Plataforma", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (parseFloat(this.fmrAplicacion.MontoMaximo) <= 0) {
        this.globales.notificacion("Ingresar monto máximo correcto que va a permitir la plataforma", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (parseFloat(this.fmrAplicacion.MontoMinimo) <= 0) {
        this.globales.notificacion("Ingresar monto mínimo correcto que va a permitir la plataforma", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else {
        estado = true
      }
    } else if (tipo == 3) {
      if (this.fmrAplicacion.LogoPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Logo Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Logo Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Texto Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Texto Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.FondoPrimario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Fondo Primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.FondoSecundario.trim() == "") {
        this.globales.notificacion("Seleccionar un Color de Fondo Secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoPrimarioTamano.trim() == "") {
        this.globales.notificacion("Seleccionar un tamañano para el logo primario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.LogoSecundarioTamano.trim() == "") {
        this.globales.notificacion("Seleccionar un tamañano para el logo secundario", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorPrimario.trim() == this.fmrAplicacion.FondoPrimario.trim()) {
        this.globales.notificacion("El color del texto primario y el<br>color de fondo primario no pueden ser iguales", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else if (this.fmrAplicacion.ColorSecundario.trim() == this.fmrAplicacion.FondoSecundario.trim()) {
        this.globales.notificacion("El color del texto secundario y el<br>color de fondo secundario no pueden ser iguales", "error", "top-end", "#E74C3CE6", "#FFF");
        estado = false
      } else {
        estado = true
      }
    }


    return estado;
  }

  public obtenerImagen(id) {
    if (id == 1) {
      var files = $("#LogoPrimario").prop('files');
      var reader = new FileReader();
      if (files.length > 0) {
        reader.readAsDataURL(files[0]);
        reader.onload = () => {
          this.fmrAplicacion.LogoPrimario = reader.result + "";
          this.fmrAplicacion.NombreLogoPrimario = files[0].name;
        };
        reader.onerror = function (error) {
          console.log('Error: ', error);
        };
      }
    } else if (id == 2) {
      var files = $("#LogoSecundario").prop('files');
      var reader = new FileReader();
      if (files.length > 0) {
        reader.readAsDataURL(files[0]);
        reader.onload = () => {
          this.fmrAplicacion.LogoSecundario = reader.result + "";
          this.fmrAplicacion.NombreLogoSecundario = files[0].name;
        };
        reader.onerror = function (error) {
          console.log('Error: ', error);
        };
      }
    }
  }

  public obtenerCaducidad() {
    var caducidad = { "Tiempo": parseInt(this.fmrAplicacion.CaducidadTiempoValor), "Tipo": this.fmrAplicacion.CaducidadTiempo }
    this.fmrAplicacion.Caducidad = JSON.stringify(caducidad);
  }

  public obtenerColor(color, id) {
    if (id == 1) {
      this.fmrAplicacion.ColorPrimario = color;
    } else if (id == 2) {
      this.fmrAplicacion.FondoPrimario = color;
    } else if (id == 3) {
      this.fmrAplicacion.ColorSecundario = color;
    } else if (id == 4) {
      this.fmrAplicacion.FondoSecundario = color;
    }
  }

  public visualizarToken() {
    var x: any = document.getElementById("contrasena");
    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  }

  public gestionarPasos(paso) {
    if (paso == 1) {
      this.gestionPasos.paso1 = true;
      this.gestionPasos.paso1Estilo = "current";
      this.gestionPasos.paso2 = false;
      this.gestionPasos.paso2Estilo = "";
      this.gestionPasos.paso3 = false;
      this.gestionPasos.paso3Estilo = "";
    } else if (paso == 2) {
      this.gestionPasos.paso1 = false;
      this.gestionPasos.paso1Estilo = "";
      this.gestionPasos.paso2 = true;
      this.gestionPasos.paso2Estilo = "current";
      this.gestionPasos.paso3 = false;
      this.gestionPasos.paso3Estilo = "";
    } else if (paso == 3) {
      this.gestionPasos.paso1 = false;
      this.gestionPasos.paso1Estilo = "";
      this.gestionPasos.paso2 = false;
      this.gestionPasos.paso2Estilo = "";
      this.gestionPasos.paso3 = true;
      this.gestionPasos.paso3Estilo = "current";
    }
  }

}
