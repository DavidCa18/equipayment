import { Pipe, PipeTransform } from '@angular/core';
import Swal from 'sweetalert2';

declare var $: any;
declare var moment: any;

@Pipe({
  name: 'globales'
})
export class GlobalesPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return null;
  }

  public token() {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    for (var i = 0; i < 333; i++) text += possible.charAt(Math.floor(Math.random() * possible.length)); return text;
  };

  public obtenerDiferidos(numero){
    return parseInt(numero) - 1;
  }


  public obtenerFechaLetras() {
    var fecha = new Date();
    var anio = fecha.getFullYear();
    const meses = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Agto', 'Sep', 'Oct', 'Nov', 'Dic'];
    var dia: any = fecha.getDate();

    var horas: any = fecha.getHours();
    var minutos: any = fecha.getMinutes();
    var segundos: any = fecha.getSeconds();

    dia < 10 ? dia = "0" + dia : dia;

    horas < 10 ? horas = "0" + horas : horas;
    minutos < 10 ? minutos = "0" + minutos : minutos;
    segundos < 10 ? segundos = "0" + segundos : segundos;

    return meses[fecha.getMonth()] + " " + dia + ", " + anio + "   " + horas + ":" + minutos + ":" + segundos;
  }

  public obtenerDia() {
    var fecha = new Date();
    var dia: any = fecha.getDate();

    return dia;
  }

  public obtenerFecha(separador: any) {
    var fecha = new Date();
    var anio = fecha.getFullYear();
    var mes: any = fecha.getMonth() + 1;
    var dia: any = fecha.getDate();

    mes < 10 ? mes = "0" + mes : mes;
    dia < 10 ? dia = "0" + dia : dia;

    return anio + separador + mes + separador + dia;
  }

  public transformarFecha(fecha_: any) {
    var fecha = new Date(fecha_);

    var mes: any = fecha.getMonth() + 1;
    var dia: any = fecha.getDate();

    var mesString = this.meses(mes);
    dia < 10 ? dia = "0" + dia : dia;

    return { dia: dia, mes: mesString };
  }

  public meses(numero) {

    var mes = "";

    var meses = [{ letra: 'EN', numero: 1 }, { letra: 'FEB', numero: 2 }, { letra: 'MAR', numero: 3 }, { letra: 'ABR', numero: 4 },
    { letra: 'MAY', numero: 5 }, { letra: 'JUN', numero: 6 }, { letra: 'JUL', numero: 7 }, { letra: 'AGTO', numero: 8 }, { letra: 'SEPT', numero: 9 },
    { letra: 'OCT', numero: 10 }, { letra: 'NOV', numero: 11 }, { letra: 'DIC', numero: 12 }];

    for (let dato of meses) {
      if (dato.numero == numero) {
        mes = dato.letra;
      }
    }

    return mes;
  }

  public obtenerFormatoFecha(fecha_: any, separador: any) {
    var fecha = new Date(fecha_);
    var anio = fecha.getFullYear();
    var mes: any = fecha.getMonth() + 1;
    var dia: any = fecha.getDate();

    mes < 10 ? mes = "0" + mes : mes;
    dia < 10 ? dia = "0" + dia : dia;

    return anio + separador + mes + separador + dia;
  }

  public mostrarAlerta(descripcion: any, tipo: any) {
    Swal.fire({
      type: tipo,
      html: '<b>' + descripcion + '</b>',
      confirmButtonText: '<div style="font-family: Montserrat, sans-serif; font-size: 14px; color: white">Aceptar</div>',
      confirmButtonColor: '#17A589'
    })
  }


  public mostrarAlertaSinTiempo(descripcion: any, tipo: any) {
    Swal.fire({
      type: tipo,
      html: '<div style="font-family: Montserrat, sans-serif; font-size: 14px; color: black">' + descripcion + '</div>',
      confirmButtonText: '<div style="font-family: Montserrat, sans-serif; font-size: 14px; color: white">Aceptar</div>',
      confirmButtonColor: '#17A589',

    })
  }

  public obtenerFechaCompleta(separador: any) {
    return moment().format("YYYY-MM-DD hh:mm:ss");
  }

  public obtenerFechaCompletaParametro(fecha_, separador: any) {
    var fecha = new Date(fecha_);
    var anio = fecha.getFullYear();
    var mes: any = fecha.getMonth() + 1;
    var dia: any = fecha.getDate();

    var horas: any = fecha.getHours()
    var minutos: any = fecha.getMinutes()
    var segundos: any = fecha.getSeconds()

    horas < 10 ? horas = "0" + horas : horas;
    minutos < 10 ? minutos = "0" + minutos : minutos;
    segundos < 10 ? segundos = "0" + segundos : segundos;

    mes < 10 ? mes = "0" + mes : mes;
    dia < 10 ? dia = "0" + dia : dia;

    return anio + separador + mes + separador + dia + " " + horas + ":" + minutos + ":" + segundos;
  }

  public obtenerHora(separador: any) {
    var fecha = new Date();

    var horas: any = fecha.getHours()
    var minutos: any = fecha.getMinutes()
    var segundos: any = fecha.getSeconds()

    horas < 10 ? horas = "0" + horas : horas;
    minutos < 10 ? minutos = "0" + minutos : minutos;
    segundos < 10 ? segundos = "0" + segundos : segundos;

    return minutos + separador + segundos;
  }

  public sumarFecha(tiempo) {
    let hoy = new Date();
    let semanaEnMilisegundos = tiempo;
    let suma = hoy.getTime() + semanaEnMilisegundos;
    let fechaSumada = new Date(suma);

    return this.obtenerFechaCompletaParametro(fechaSumada, "-");
  }

  public mostrarAlertaConfirmacion(texto, tipo) {
    Swal.fire({
      html: texto,
      type: tipo,
      showCancelButton: true,
      confirmButtonColor: '#3c3c3c',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Continuar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {

    });
  }

  public notificacion(texto, tipo, posicion, background, color) {
    const Toast = Swal.mixin({
      toast: true,
      position: posicion,
      showConfirmButton: false,
      timer: 3000
    });

    var icono = '';
    if (tipo == "error") {
      icono = '<i class="fa fa-exclamation" style="padding-right: 4px; font-size: 18px"></i>';
    } else if (tipo == "success") {
      icono = '<i class="fa fa-check" style="padding-right: 4px; font-size: 18px"></i>';
    } else if (tipo == "warning") {
      icono = '<i class="fa fa-exclamation-triangle" style="padding-right: 4px; font-size: 18px"></i>';
    } else if (tipo == "info") {
      icono = '<i class="fa fa-exclamation" style="padding-right: 4px; font-size: 18px"></i>';
    }
    Toast.fire({
      html: '<div style="font-family: Montserrat, sans-serif; font-size: 14px; color: ' + color + '; padding: 10px; font-weight: bold">' + icono + ' ' + texto + '</div>',
      background: background
    })
  }


  public formatoCampo(valor, restriccion, caracteres, tipo) {
    var out = '';
    var filtro = '' + restriccion + '';
    for (var i = 0; i < valor.length; i++) {
      if (filtro.indexOf(valor.charAt(i)) != -1) {
        if (out.length >= caracteres) {
          out.substr(0, caracteres);
        } else {
          out += valor.charAt(i);
        }
      }
    }
    return (tipo == 1) ? out.toUpperCase() : out;
  }

  public formatearNumero(amount, decimals) {
    amount += '';
    amount = parseFloat(amount.replace(/[^0-9\.]/g, ''));

    decimals = decimals || 0;

    if (isNaN(amount) || amount === 0)
      return parseFloat("0").toFixed(decimals);

    amount = '' + amount.toFixed(decimals);

    var amount_parts = amount.split('.'),
      regexp = /(\d+)(\d{3})/;

    while (regexp.test(amount_parts[0]))
      amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');

    return amount_parts.join('.');
  }

  public obtenerNumerosCadena(cadena) {

    var cadena = cadena;
    var letras = cadena.replace(/\D/g, '');

    return letras;
  }

  public gestionEmail(valor) {
    var str = valor;
    var res = str.split(";");
    return res;
  }

  public codificarBase64(valor) {
    return btoa(valor);
  }

  public decodificarBase64(valor) {
    return atob(valor);
  }

  public validarIndefinidos(valor) {
    return valor == undefined || valor == null ? "" : valor;
  }

  public calculoValoresPagar(total) {

    /*var valoresCobro = {
      subtotal: 0,
      iva: 0,
      total: 0
    }
  
    var AuxSubTotal = total / 1.12;
    var AuxIva = AuxSubTotal * 0.12;
    var AuxTotal = AuxSubTotal + AuxIva;
  
    valoresCobro.total = (Math.round((AuxTotal) * 100) / 100);
    valoresCobro.iva = Math.round((AuxIva) * 100) / 100;
    valoresCobro.subtotal = Math.round((AuxSubTotal) * 100) / 100;
  
    return { subtotal: valoresCobro.subtotal.toFixed(2), iva: valoresCobro.iva.toFixed(2), total: valoresCobro.total.toFixed(2) };*/

    var totalReal = parseFloat(total);

    var subtotalCalculo = 0;
    var ivaCalculo = 0;
    var totalCalculo = 0;

    var diferencia = 0;

    subtotalCalculo = Math.round((totalReal / 1.12) * 100) / 100;
    ivaCalculo = Math.round((subtotalCalculo * 0.12) * 100) / 100;
    totalCalculo = Math.round((subtotalCalculo + ivaCalculo) * 100) / 100;

    if (totalCalculo == totalReal) {
      return { subtotal: subtotalCalculo, iva: ivaCalculo, total: totalReal };
    } else {
      diferencia = totalReal - totalCalculo;
      var nuevoIva = ivaCalculo + (diferencia);
      return { subtotal: subtotalCalculo.toFixed(2), iva: nuevoIva.toFixed(2), total: totalReal.toFixed(2) };
    }

  }

  public recortarTexto(nombre) {
    var res = "";
    var longitud = nombre.length;
    if (longitud <= 58) {
      res = nombre;
    } else {
      res = nombre.substring(0, 58) + '...';
    }
    return res;
  }

  public obtenerCodigoPagador(res) {
    var asegurado = "";
    var xml = $.parseXML(res);
    var resultado = $(xml).find("ActualizarDatosAccContPOTENCIALxDocResponse")[0];

    if (resultado != undefined) {

      $(xml).find("ActualizarDatosAccContPOTENCIALxDocResponse").each(function () {
        asegurado = $(this).find('icod_aseg').text();
      });

      if (asegurado == "-1") {
        return 0;
      } else {
        return asegurado;
      }

    } else {
      return 0;
    }
  }

  public obtenerRespuestaAplicacionPago(res) {
    var descripcion = "";
    var proceso = "";
    var recibo = "";

    var xml = $.parseXML(res);
    var pago = "";

    var resultado = $(xml).find("pagoTarjetaMasivosResponse")[0];
    if (resultado != undefined) {

      $(xml).find("pagoTarjetaMasivosResponse").each(function () {
        pago = $(this).find('pagoTarjetaMasivosResult').text();
      });

      var estado = $.parseXML(pago);
      var resultadoEstado = $(estado).find("DetalleRecaudo")[0];
      if (resultadoEstado != undefined) {
        $(estado).find("DetalleRecaudo").each(function () {
          descripcion = $(this).find('txt_descripcion').text();
          proceso = $(this).find('id_proceso').text();
          recibo = $(this).find('nro_recibo').text();
        });

        if (descripcion == "PROCESO CORRECTO") {
          return {
            descripcion: descripcion,
            proceso: proceso,
            recibo: recibo,
            estado: 1
          };
        } else {
          return {
            descripcion: descripcion,
            proceso: 0,
            recibo: 0,
            estado: 0
          };
        }
      } else {
        return {
          descripcion: descripcion,
          proceso: 0,
          recibo: 0,
          estado: 0
        };
      }
    } else {
      return {
        descripcion: descripcion,
        proceso: 0,
        recibo: 0,
        estado: 0
      };
    }
  }

  public obtenerFechaVoucher(fecha) {
    var str = fecha;
    var res = str.split(" ");
    return res[0];
  }

}
