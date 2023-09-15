import { Component, OnInit, ViewChild, ElementRef, ViewContainerRef, ComponentFactoryResolver } from '@angular/core';
import { ApiService } from '../../servicio/api/api.service';
import { Globales } from '../../variables/globales/globales.service';
import { Router } from '@angular/router';
import { HeaderComponent } from '../complementos/header/header.component';
import { PagoBotonDatafastComponent } from '../pago-boton/pago-boton.component';
import { PagoBotonPayphoneComponent } from '../pago-boton-payphone/pago-boton-payphone.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { GlobalesPipe } from '../../metodos/globales.pipe';
import { DomSanitizer } from '@angular/platform-browser';

declare var $: any;
declare var moment: any;

@Component({
  selector: 'app-pago',
  templateUrl: './pago.component.html',
  styleUrls: ['./pago.component.css'],
  providers: [Globales]
})
export class PagoComponent implements OnInit {

  //HEADER
  @ViewChild('header', { read: ViewContainerRef }) entradaHeader: ViewContainerRef;
  public componenteHeaderRef: any;

  @ViewChild('botonDatafast', { read: ViewContainerRef }) entradaBotonDatafast: ViewContainerRef;
  public componenteBotonDatafastRef: any;

  @ViewChild('botonPayphone', { read: ViewContainerRef }) entradaBotonPayphone: ViewContainerRef;
  public componenteBotonPayphoneRef: any;

  //VARIABLE ID / CHECKOUT DATAFAST
  public chekoutid = "";

  //VARIABLE IDENTIFICADOR DE LOS DIFERIDOS DATAFAST
  public identificador = 0;

  //DATAFAST
  public banco: string = "Banco Pichincha";
  public imgBanco: string = "assets/brand-banks/banco.png";
  public plataforma = "DATAFAST";

  public dtsPago: any = {};
  public dtsClienteFactura: any = { "Cliente": { "Apellido": "", "Aplicacion": 0, "Email": "", "Estado": 0, "IdCliente": 0, "Identificacion": "", "Identificador": 0, "PrimerNombre": "", "SegundoNombre": "", "Telefono": "" }, "Comercio": "", "Estado": 0, "IdFactura": 0, "Identificador": 0, "Iva": "0", "Numero": "0", "Subtotal0": "0", "Subtotal12": "0", "Total": "0", "UrlRetorno": null };

  public parametros = {
    pago: 0,
    plataforma: 0,
    diferidos: null,
    opciones: "",
    recurrencia: 0,
  };

  variables: Globales = new Globales();
  globales: GlobalesPipe = new GlobalesPipe();

  binTarjeta: any = "";
  dtsBin = { "Bin": null, "CodigoBanco": null, "CodigoConducto": null, "Descripcion": null, "Estado": 0, "IdCatalagoBines": 0, "Identificador": 0, "Nombre": null, "Resultado": null };

  public interfaz = "deshabilitado";

  public lstEntidadFinanciera = [];
  public dataEntidadFinanciera: any;

  public entidadFinanciera: any;
  public cbxEntidadFinanciera = false;

  public imagenTarjeta = 'fas fa-credit-card';
  public documentoDatos = "";

  public css = "../../../assets/card-datafast/card-datafast.css";
  public dtsAplicacion = { "Codigo": 0, "ColorPrimario": null, "ColorSecundario": null, "Estado": 0, "FondoPrimario": null, "FondoSecundario": null, "IdAplicacion": 0, "Identificador": 0, "LogoPrimario": null, "LogoPrimarioTamano": null, "LogoSecundario": null, "LogoSecundarioTamano": null, "Nombre": null, "Token": null, "VisualizacionBin": 0, "Caducidad": "", "Recurrencia": 0, "Gracia": 0 };

  public loading = {
    load: true,
    form: false
  }

  public gracia = "";
  public vistaGracia = false;

  constructor(private conexion: ApiService, private router: Router,
    private resolver: ComponentFactoryResolver, public sanitizer: DomSanitizer) {
  }

  ngOnInit() {
    this.inicializarParametros();

    this.documentoDatos = this.variables.obtenerCredenciales().conexionAPI + "Documentos/POLITICA.pdf";

    $(document).ready(function () {
      $("#ayuda").css("display", "none");
      $("#inputBin").mouseenter(function () {
        $("#ayuda").css("display", "block");
      });
      $("#inputBin").mouseleave(function () {
        $("#ayuda").css("display", "none");
      });
    });
  }

  public recurrencia() {
    if (this.dtsAplicacion.Recurrencia == 1) {
      setTimeout(() => { this.seleccionarBanco('Produbanco'); }, 2000);
    }
  }

  public generarComponenteHeader(logoPrimario, logoSecundario, colorPrimario, colorSecundario, fondoPrimario, fondoSecundario, logoPrimarioTamano, logoSecundarioTamano) {
    this.entradaHeader.clear();
    const factory = this.resolver.resolveComponentFactory(HeaderComponent);
    const componenteHeaderRef = this.entradaHeader.createComponent(factory);
    componenteHeaderRef.instance.logoPrimario = logoPrimario;
    componenteHeaderRef.instance.logoSecundario = logoSecundario;
    componenteHeaderRef.instance.fondoPrimario = fondoPrimario;
    componenteHeaderRef.instance.fondoSecundario = fondoSecundario;
    componenteHeaderRef.instance.colorPrimario = colorPrimario;
    componenteHeaderRef.instance.colorSecundario = colorSecundario;
    componenteHeaderRef.instance.logoPrimarioTamano = logoPrimarioTamano;
    componenteHeaderRef.instance.logoSecundarioTamano = logoSecundarioTamano;
  }

  public generarComponenteBotonDataFast(identificador) {
    this.entradaBotonDatafast.clear();
    const factory = this.resolver.resolveComponentFactory(PagoBotonDatafastComponent);
    const componenteBotonTatafastRef = this.entradaBotonDatafast.createComponent(factory);
    componenteBotonTatafastRef.instance.chekoutid = this.chekoutid;
    componenteBotonTatafastRef.instance.identificador = identificador;
    componenteBotonTatafastRef.instance.parametros = this.parametros;
    componenteBotonTatafastRef.instance.cliente = this.dtsClienteFactura;
    componenteBotonTatafastRef.instance.banco = this.banco;
    componenteBotonTatafastRef.instance.gracia = this.gracia;
    componenteBotonTatafastRef.instance.vistaGracia = this.dtsAplicacion.Gracia
  }

  public generarComponenteBotonPayphone() {
    this.entradaBotonPayphone.clear();
    const factory = this.resolver.resolveComponentFactory(PagoBotonPayphoneComponent);
    const componenteBotonPayphoneRef = this.entradaBotonPayphone.createComponent(factory);
    componenteBotonPayphoneRef.instance.parametros = this.parametros;
    componenteBotonPayphoneRef.instance.dtsClienteFactura = this.dtsClienteFactura;
    componenteBotonPayphoneRef.instance.dtsAplicacion = this.dtsAplicacion;
    componenteBotonPayphoneRef.instance.banco = this.banco;
    componenteBotonPayphoneRef.instance.gracia = this.gracia;
    componenteBotonPayphoneRef.instance.vistaGracia = this.dtsAplicacion.Gracia
  }

  public inicializarParametros() {
    var url = this.router.parseUrl(this.router.url);
    var parametros = url.queryParams;
    var validacion = this.validarParametros(parametros);
    if (validacion) {
      this.parametros.pago = parseInt(atob(parametros.c));
      this.parametros.plataforma = parseInt(parametros.p);
      this.parametros.diferidos = parametros.d;
      this.parametros.opciones = parametros.o;
      this.parametros.recurrencia = parseInt(parametros.r);
      //this.parametros.frame = parametros.f == undefined ? 0 : parseInt(parametros.f);
      this.listarEntidadesFinancieras();
      console.log(this.parametros)
    } else {
      var codigo = 400;
      var aplicacion = 0;
      this.router.navigate(['/pago/informacion/' + btoa(codigo + "," + aplicacion)]);
    }

  }

  public listarEntidadesFinancieras() {
    this.lstEntidadFinanciera = [
      { banco: "Diners Club", imagen: "dinersClub.png" },
      { banco: "Banco Pichincha", imagen: "pichincha.png" },
      { banco: "B General Rumiñahui", imagen: "generalRuminahui.png" },
      { banco: "Banco de Loja", imagen: "loja.png" },
      { banco: "Banco de Manabí", imagen: "manabi.png" },
      { banco: "Banco Guayaquil", imagen: "guayaquil.png" },
      { banco: "Banco del Pacífico", imagen: "pacifico.png" },
      { banco: "Banco Internacional", imagen: "internacional.png" },
      { banco: "Produbanco", imagen: "produbanco.png" },
      { banco: "Banco Bolivariano", imagen: "bolivariano.png" },
      { banco: "Banco de Machala", imagen: "machala.png" },
      { banco: "Banco Amazonas", imagen: "amazonas.png" },
      { banco: "Banco Solidario", imagen: "solidario.png" },
      { banco: "Mutualista del Azuay", imagen: "azuay.png" },
      { banco: "Mutualista Imbabura", imagen: "imbabura.png" },
      { banco: "Cooperativa Cooprogreso", imagen: "cooprogreso.png" },
      { banco: "Cooperativa JEP", imagen: "jep.png" },
      { banco: "Internacional", imagen: "pago_internacional.png" },
      { banco: "Otra Entidad Financiera", imagen: "otros.png" }
    ];

    this.dataEntidadFinanciera = this.lstEntidadFinanciera.slice();
    this.buscarDatosAplicacion();
  }

  public buscarDatosAplicacion() {
    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.parametros.plataforma).subscribe(
      (res: any) => {
        this.spinner(false);
        this.dtsAplicacion = res;
        console.log(res);
        this.recurrencia();
        this.listarClienteFacturaPago();
      },
      err => {
        this.spinner(false);
        this.conexion.error("pago.component.ts", "buscarDatosAplicacion", "Gestion/SGesGestion.svc/pago/aplicacion/listar/" + this.parametros.plataforma, err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public listarClienteFacturaPago() {
    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/pago/pago/listar/inicio/" + this.parametros.pago).subscribe(
      (res: any) => {
        this.spinner(false);

        this.dtsPago = res;
        this.dtsClienteFactura = res.Factura;
        console.log(this.dtsClienteFactura)
        if (this.dtsPago.Estado == 1 && this.verificarExpiracion() == false) {
          // this.obtenerDatafastChekoutid();
        } else {
          var codigo = res.ResultadoCodigo == "" ? "68979" : res.ResultadoCodigo;
          var aplicacion = this.parametros.plataforma;

          if (this.dtsPago.Estado == 2 || this.dtsPago.Estado == 3 ||
            this.dtsPago.Estado == 4 || this.dtsPago.Estado == 5) {
            this.router.navigate(['/pago/informacion/' + btoa(codigo + "," + aplicacion + "," + this.parametros.pago)]);
          } else {
            this.expirarPago(codigo, aplicacion);
          }
        }
      },
      err => {
        this.spinner(false);
        this.conexion.error("pago.component.ts", "listarClienteFacturaPago", "Gestion/SGesGestion.svc/pago/pago/listar/" + this.parametros.pago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public verificarExpiracion() {
    var estado = false;

    var caducidad: any = JSON.parse(this.dtsAplicacion.Caducidad);

    var fechaCreacion = this.globales.obtenerFechaCompletaParametro(this.dtsPago.FechaIngreso, "-");
    var fechaCaducidad = moment(fechaCreacion).add(caducidad.Tiempo, caducidad.Tipo).format("YYYY-MM-DD hh:mm:ss");

    var fechaActual = this.globales.obtenerFechaCompleta("-");

    if (fechaActual > fechaCaducidad) {
      estado = true;
    } else {
      estado = false;
    }
    return estado;
  }

  public expirarPago(codigo, aplicacion) {

    this.spinner(true);
    this.conexion.get("Gestion/SGesGestion.svc/pago/expiracion/" + this.parametros.pago).subscribe(
      (resp: any) => {
        this.spinner(false);
        this.router.navigate(['/pago/informacion/' + btoa(codigo + "," + aplicacion + "," + this.parametros.pago)]);
      },
      err => {
        this.spinner(false);
        this.conexion.error("pago.component.ts", "expirarPago", "Gestion/SGesGestion.svc/pago/pago/listar/" + this.parametros.pago, err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public obtenerSubtotalFactura(sub12, sub0) {
    var subtotal = parseFloat(sub12) + parseFloat(sub0);
    return Math.round(subtotal * 100) / 100;
  }

  public consultarBin() {

    if (this.binTarjeta == "") {

    } else {
      this.spinner(true);
      this.conexion.get("Gestion/SGesGestion.svc/bin/listar/" + this.binTarjeta).subscribe(
        (res: any) => {
          this.spinner(false);
          this.dtsBin = res;
          this.interfaz = "habilitado";

          if (this.dtsBin.Descripcion == "UIO_DINERS CLUB (DIF C/IN)") {
            this.seleccionarBanco('Diners Club');
          } else if (this.dtsBin.Descripcion == "UIO_DINERS CLUB (CORR)") {
            this.seleccionarBanco('Diners Club');
          } else if (this.dtsBin.Descripcion == "UIO_DINERS CLUB (DIF CORR)") {
            this.seleccionarBanco('Diners Club');
          } else if (this.dtsBin.Descripcion == "UIO_DINERS CLUB (DIF S/IN)") {
            this.seleccionarBanco('Diners Club');
          } else if (this.dtsBin.Nombre == "PACIFICO") {
            this.seleccionarBanco('Banco del Pacífico');
          } else if (this.dtsBin.Nombre == "PICHINCHA") {
            this.seleccionarBanco('Banco Pichincha');
          } else if (this.dtsBin.Nombre == "PRODUBANCO") {
            this.seleccionarBanco('Produbanco');
          } else if (this.dtsBin.Nombre == "GUAYAQUIL") {
            this.seleccionarBanco('Banco Guayaquil');
          } else if (this.dtsBin.Nombre == "BOLIVARIANO") {
            this.seleccionarBanco('Banco Bolivariano');
          } else if (this.dtsBin.Nombre == "MACHALA") {
            this.seleccionarBanco('Banco de Machala');
          } else if (this.dtsBin.Nombre == "RUMIÑAHUI") {
            this.seleccionarBanco('B General Rumiñahui');
          } else if (this.dtsBin.Nombre == "LOJA") {
            this.seleccionarBanco('Banco de Loja');
          } else if (this.dtsBin.Nombre == "MANABÍ") {
            this.seleccionarBanco('Banco de Manabí');
          } else if (this.dtsBin.Nombre == "INTERNACIONAL") {
            this.seleccionarBanco('Banco Internacional');
          } else if (this.dtsBin.Nombre == "AMAZONAS") {
            this.seleccionarBanco('Banco Amazonas');
          } else if (this.dtsBin.Nombre == "SOLIDARIO") {
            this.seleccionarBanco('Banco Solidario');
          } else if (this.dtsBin.Nombre == "MUTUALISTA AZUAY") {
            this.seleccionarBanco('Mutualista del Azuay');
          } else if (this.dtsBin.Nombre == "MUTUALISTA IMBABURA") {
            this.seleccionarBanco('Mutualista Imbabura');
          } else if (this.dtsBin.Nombre == "COOPROGRESO") {
            this.seleccionarBanco('Cooperativa Cooprogreso');
          } else if (this.dtsBin.Nombre == "JEP") {
            this.seleccionarBanco('Cooperativa JEP');
          }

          if (this.dtsBin.IdCatalagoBines == 0) {
            this.cbxEntidadFinanciera = true;
            this.imgBanco = "assets/brand-banks/banco.png";
          } else {
            this.cbxEntidadFinanciera = false;
          }

        },
        err => {
          this.spinner(false);
          this.conexion.error("pago.component.ts", "consultarBin", "Gestion/SGesGestion.svc/bin/listar/", err.status, err.url, err.error, 0, "PAGO CLIENTE");
          console.log(err);
        }
      );
    }
  }

  public seleccionarBanco(banco: string) {
    this.css = "../../../assets/card-datafast/card-datafast.css";
    this.interfaz = "habilitado";
    this.banco = banco;
    this.gracia = "";

    if (this.banco == "Diners Club") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/dinersClub.png";
    } if (this.banco == "Banco Pichincha") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/pichincha.png";
    } if (this.banco == "B General Rumiñahui") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/generalRuminahui.png";
    } if (this.banco == "Banco de Loja") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/loja.png";
    } if (this.banco == "Banco de Manabí") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/manabi.png";
    } if (this.banco == "Banco Guayaquil") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/guayaquil.png";
    } if (this.banco == "Banco del Pacífico") {
      this.plataforma = "DATAFAST";
      this.imgBanco = "assets/brand-banks/pacifico.png";
    } if (this.banco == "Banco Internacional") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/internacional.png";
    } if (this.banco == "Produbanco") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/produbanco.png";
    } if (this.banco == "Banco Bolivariano") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/bolivariano.png";
    } if (this.banco == "Banco de Machala") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/machala.png";
    } if (this.banco == "Banco Amazonas") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/amazonas.png";
    } if (this.banco == "Banco Solidario") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/solidario.png";
    } if (this.banco == "Mutualista del Azuay") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/azuay.png";
    } if (this.banco == "Mutualista Imbabura") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/imbabura.png";
    } if (this.banco == "Cooperativa Cooprogreso") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/cooprogreso.png";
    } if (this.banco == "Cooperativa JEP") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/jep.png";
    }if (this.banco == "Internacional") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/pago_internacional.png";
    }if (this.banco == "Otra Entidad Financiera") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/otros.png";
    }

    this.verificarGracia();
  }

  public verificarGracia() {
    if (this.dtsAplicacion.Gracia == 1 && (this.parametros.diferidos == "gracia" || this.parametros.diferidos == "especial")) {
      if (this.plataforma == "DATAFAST") {
        if (this.banco == "Diners Club") {
          this.vistaGracia = true;
        } else if (this.banco == "Banco del Pacífico") {
          this.vistaGracia = true;
        } else {
          this.vistaGracia = false;
          this.gracia = "2";
          this.obtenerDatafastChekoutid();
        }
      } else {
        if(this.banco == "Internacional"){
          this.vistaGracia = false;
          this.gracia = "2";
          this.obtenerPayPhoneChekoutid();
        }else if(this.banco == "Otra Entidad Financiera"){
          this.vistaGracia = false;
          this.gracia = "2";
          this.obtenerPayPhoneChekoutid();
        }else{
          this.vistaGracia = true;
        }
      }
    } else {
      this.vistaGracia = false;
      this.gracia = "2";
      if (this.plataforma == "DATAFAST") {
        this.obtenerDatafastChekoutid();
      } else {
        this.obtenerPayPhoneChekoutid();
      }
    }
  }

  public obtenertChekoutid() {
    if (this.plataforma == "DATAFAST") {
      this.obtenerDatafastChekoutid();
    } else {
      this.obtenerPayPhoneChekoutid();
    }
  }

  public obtenerDatafastChekoutid() {

    var Pago = {
      Factura: {
        Total: this.variables.ambiente == "PRODUCCION" ? this.dtsClienteFactura.Total : "3.12",
        Iva: this.variables.ambiente == "PRODUCCION" ? this.dtsClienteFactura.Iva : "0.12",
        Subtotal12: this.variables.ambiente == "PRODUCCION" ? this.dtsClienteFactura.Subtotal12 : "1.00",
        Subtotal0: this.variables.ambiente == "PRODUCCION" ? this.dtsClienteFactura.Subtotal0 : "2.00",
        Comercio: this.dtsClienteFactura.Comercio,
        Numero: this.dtsClienteFactura.Numero,
        Cliente: {
          PrimerNombre: this.dtsClienteFactura.Cliente.PrimerNombre,
          SegundoNombre: this.dtsClienteFactura.Cliente.SegundoNombre,
          Apellido: this.dtsClienteFactura.Cliente.Apellido,
          Email: this.globales.gestionEmail(this.dtsClienteFactura.Cliente.Email)[0],
          Identificacion: this.dtsClienteFactura.Cliente.Identificacion.substring(0, 10),
          Numero: this.dtsClienteFactura.Cliente.Numero,
          Aplicacion: {
            IdAplicacion: this.dtsAplicacion.Codigo,
            Gracia: this.dtsAplicacion.Gracia
          }
        }
      },
      Ip: this.dtsPago.Ip,
      Banco: this.banco,
      Gracia: this.gracia
    };

    this.spinner(true);
    this.conexion.post("Gestion/SGesGestion.svc/pago/datafast/generar/pago", Pago).subscribe(
      (res: any) => {
        console.log(res);
        this.spinner(false);
        var resultado = JSON.parse(res);
        this.chekoutid = resultado.id;

        if (this.banco == "Diners Club") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/dinersClub.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "Banco Pichincha") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/pichincha.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "B General Rumiñahui") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/generalRuminahui.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "Banco de Loja") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/loja.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "Banco de Manabí") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/manabi.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "Banco Guayaquil") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/guayaquil.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        } if (this.banco == "Banco del Pacífico") {
          this.plataforma = "DATAFAST";
          this.imgBanco = "assets/brand-banks/pacifico.png";
          setTimeout(() => { this.generarComponenteBotonDataFast(0); }, 400);
        }

      },
      err => {
        this.spinner(false);
        this.conexion.error("pago.component.ts", "obtenerDatafastChekoutid", "Gestion/SGesGestion.svc/pago/datafast/generar/pago", err.status, err.url, err.error, 0, "PAGO CLIENTE");
        console.log(err);
      }
    );
  }

  public obtenerPayPhoneChekoutid() {
    if (this.banco == "Banco Internacional") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/internacional.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Produbanco") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/produbanco.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Banco Bolivariano") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/bolivariano.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Banco de Machala") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/machala.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Banco Amazonas") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/amazonas.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Banco Solidario") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/solidario.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Mutualista del Azuay") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/azuay.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Mutualista Imbabura") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/imbabura.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Cooperativa Cooprogreso") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/cooprogreso.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    } if (this.banco == "Cooperativa JEP") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/jep.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    }if (this.banco == "Internacional") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/pago_internacional.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    }if (this.banco == "Otra Entidad Financiera") {
      this.plataforma = "PAYPHONE";
      this.imgBanco = "assets/brand-banks/otros.png";
      setTimeout(() => { this.generarComponenteBotonPayphone(); }, 400);
    }
  }

  // *** METODOS COMPLEMENTARIOS ***
  public validarParametros(parametros) {
    var estado = false;
    if (parametros.c == undefined) {
      estado = false;
    } else if (parametros.p == undefined) {
      estado = false;
    } else if (this.verificarBase64(parametros.c) == false) {
      estado = false;
    } else {
      estado = true;
    }
    return estado;
  }

  public verificarBase64(dato) {
    var expresionBase64 = /^([0-9a-zA-Z+/]{4})*(([0-9a-zA-Z+/]{2}==)|([0-9a-zA-Z+/]{3}=))?$/;
    var base64 = expresionBase64.test(dato);
    if (base64 == true) {
      return true;
    } else {
      return false;
    }
  }

  public redondear(numero) {
    var redondeado = this.redondearDecimales(numero);
    var redondeadoString = redondeado + "";
    var total = redondeadoString.replace(".", "");
    return total;
  }

  public redondearDecimales(valor: any) {
    return parseFloat(valor).toFixed(2);
  }

  public filtrarEntidadFinanciera(value) {
    this.dataEntidadFinanciera = this.lstEntidadFinanciera.filter((s) => s.banco.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  public selBanco(value) {
    this.seleccionarBanco(value.banco);
  }

  public marcaTarjeta() {
    var number = this.binTarjeta;
    var charCount = number.length;

    if (charCount == 0) {
      this.imagenTarjeta = 'fas fa-credit-card';
    }
    if (charCount == 1) {
      if (number == "4") {
        this.imagenTarjeta = 'fab fa-cc-visa';
      }
    }
    if (charCount == 2) {
      if (number == "34" || number == "37") {
        this.imagenTarjeta = 'fab fa-cc-amex';
      } else if (number == "51" || number == "52" || number == "53" || number == "54" || number == "55") {
        this.imagenTarjeta = 'fab fa-cc-mastercard';
      } else if (number == "64" || number == "65") {
        this.imagenTarjeta = 'fab fa-cc-discover';
      } else if (number == "36" || number == "38") {
        this.imagenTarjeta = 'fab fa-cc-diners-club';
      }
    }
    if (charCount == 3) {
      if (number == "305") {
        this.imagenTarjeta = 'fab fa-cc-diners-club';
      }
    }
    if (charCount == 4) {
      if (number == "6011") {
        this.imagenTarjeta = 'fab fa-cc-discover';
      }
    }
  }

  public spinner(parametro) {
    if (parametro == true) {
      this.loading.load = true;
      this.loading.form = false;
    } else if (parametro == false) {
      this.loading.load = false;
      this.loading.form = true;
    }
  }

}
