import { Component, OnInit } from '@angular/core';
import { SesionService } from '../../servicio/sesion/sesion.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../../servicio/api/api.service';
import * as XLSX from 'xlsx';
import { GlobalesPipe } from '../../metodos/globales.pipe';
type AOA = any[][];
declare var $: any;

@Component({
  selector: 'app-masivos',
  templateUrl: './masivos.component.html',
  styleUrls: ['./masivos.component.css']
})
export class MasivosComponent implements OnInit {

  public lstDocumentoClientes: AOA = [];
  public nombreDocumento = "Seleccionar Archivo";
  public p: any;
  public q: any;

  public usuario: any;
  public globales: GlobalesPipe = new GlobalesPipe();
  public lstLinksExitosos = [];
  public lstLinksFallidos = [];

  constructor(private conexion: ApiService, private spinner: NgxSpinnerService, private sesion: SesionService) { }

  ngOnInit() {
    this.sesion.verificarCredencialesRutas();
    this.usuario = this.sesion.obtenerDatos();
  }

  public seleccionarArchivo(evt: any) {
    const target: DataTransfer = <DataTransfer>(evt.target);
    this.nombreDocumento = target.files[0].name;
    this.lstDocumentoClientes = [];
    if (this.validarDocumentosExcel(target)) {
      const reader: FileReader = new FileReader();

      reader.onload = (e: any) => {
        const bstr: string = e.target.result;
        const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });

        const wsname: string = wb.SheetNames[0];
        const ws: XLSX.WorkSheet = wb.Sheets[wsname];

        var data = <AOA>(XLSX.utils.sheet_to_json(ws, { header: 1 }));
        var aux = [];
        var lstObjetos = [];
        for (let i = 1; i < data.length; i++) {
          aux = data[i];
          lstObjetos.push({
            id: (i + 1),
            codigo: 15,
            identificacion: this.globales.validarIndefinidos(aux[0]) + "",
            nombre: this.globales.validarIndefinidos(aux[1]),
            email: this.globales.validarIndefinidos(aux[2]),
            celular: this.globales.validarIndefinidos(aux[3]),
            ramo: this.globales.validarIndefinidos(aux[4]),
            poliza: this.globales.validarIndefinidos(aux[5]),
            idpv: this.globales.validarIndefinidos(aux[6]),
            deuda: this.globales.validarIndefinidos(aux[7]),
            cobranza: this.globales.validarIndefinidos(aux[8]),
            saldo: this.globales.validarIndefinidos(aux[9]),
            cuota: this.globales.validarIndefinidos(aux[10]),
            aplicacion: "",
            valores: ""
          });
        };

        this.lstDocumentoClientes = this.agruparPagosCliente(lstObjetos);
        console.log(this.lstDocumentoClientes);
        console.log(JSON.stringify(this.lstDocumentoClientes));
      };
      reader.readAsBinaryString(target.files[0]);
    } else {
      this.lstDocumentoClientes = [];
    }
  }

  public generarLinks() {
    var datos = {
      Masivos: JSON.stringify(this.lstDocumentoClientes)
    };
    console.log(datos);
    this.spinner.show();
    this.conexion.post("Gestion/SGesGestion.svc/pago/masivos/links", datos).subscribe(
      (res: any) => {
        this.spinner.hide();

        this.lstDocumentoClientes = [];
        this.lstLinksFallidos = [];
        this.lstLinksExitosos = [];
        this.nombreDocumento = "Seleccionar Archivo";

        for (let link of JSON.parse(res)) {
          if (link.IdPago == "0") {
            this.lstLinksFallidos.push(link);
          } else {
            this.lstLinksExitosos.push(link);
          }
        }

        $('#ModalErrores').modal('toggle');
        console.log(this.lstLinksFallidos);
        console.log(this.lstLinksExitosos);
      },
      err => {
        this.spinner.hide();
        this.globales.mostrarAlerta("Error al procesar los datos del archivo", "warning");
        this.conexion.error("masivos.component.ts", "generarLinks", "Gestion/SGesGestion.svc/pago/masivos/links", err.status, err.url, err.error, this.usuario.IdUsuario, this.usuario.Nombre);
        console.log(err);
      }
    );
  }


  public agruparPagosCliente(lista) {

    var agrupacion = function (miarray, prop) {
      return miarray.reduce(function (groups, current) {
        var val = current[prop];
        groups[val] = groups[val] || {
          id: current.id,
          codigo: current.codigo,
          identificacion: current.identificacion,
          nombre: current.nombre,
          email: current.email,
          celular: current.celular,
          ramo: current.ramo,
          poliza: current.poliza,
          idpv: current.idpv,
          deuda: 0,
          cobranza: current.cobranza,
          saldo: 0,
          cuota: 0,
          aplicacion: "",
          valores: ""
        };
        groups[val].deuda += Math.round(current.deuda * 100) / 100;
        groups[val].saldo += Math.round(current.saldo * 100) / 100;
        groups[val].cuota++;
        groups[val].aplicacion += '{"cuota":' + current.cuota + ',"deuda":' + current.deuda + ', "estado":0},';
        return groups;
      }, {});
    }

    var trama: any = Object["values"](agrupacion(lista, 'idpv'));
    var lstPagos = [];
    for (let dato of trama) {
      lstPagos.push({
        id: dato.id,
        codigo: dato.codigo,
        identificacion: dato.identificacion,
        nombre: dato.nombre,
        email: dato.email,
        celular: dato.celular,
        ramo: dato.ramo,
        poliza: dato.poliza,
        idpv: dato.idpv,
        deuda: Math.round(dato.deuda * 100) / 100,
        cobranza: dato.cobranza,
        saldo: Math.round(dato.saldo * 100) / 100,
        cuota: dato.cuota,
        aplicacion: "[" + dato.aplicacion.substring(0, dato.aplicacion.length - 1) + "]",
        valores: isNaN(dato.deuda) == true ? this.globales.calculoValoresPagar(0) : this.globales.calculoValoresPagar(parseFloat(dato.deuda))
      });
    }

    return lstPagos;
  }

  public validarDocumentosExcel(target) {
    var estado = true;
    if (target.files.length !== 1) {
      this.globales.mostrarAlerta("Solo se permite un archivo por transacción de masivos.", "info");
      estado = false;
    } else if (target.files[0].type != "application/vnd.ms-excel" && target.files[0].type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
      this.globales.mostrarAlerta("Solo se permite archivo formato Microsoft Excel.", "info");
      estado = false;
    }
    return estado;
  }



}
