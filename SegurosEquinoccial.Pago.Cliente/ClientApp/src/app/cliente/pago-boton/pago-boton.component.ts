import { Component, Input } from '@angular/core';
import { Globales } from '../../variables/globales/globales.service';
import { GeneradorDinamicoScriptsService } from '../../servicio/scripts/scripts.service';
declare var $: any;

@Component({
  selector: 'app-pago-boton-datafast',
  templateUrl: './pago-boton.component.html',
  styleUrls: ['./pago-boton.component.css']
})
export class PagoBotonDatafastComponent {

  @Input() identificador = 0;
  @Input() chekoutid: string = "";
  @Input() parametros: any = {};
  @Input() cliente: any = {};
  @Input() banco = "";
  @Input() gracia = "";
  @Input() vistaGracia = 0;


  public estado = true;
  public url = "";
  public titular = "";

  constructor(public global: Globales, private scripts: GeneradorDinamicoScriptsService) {

    setTimeout(() => {

      var datos = { idPago: this.parametros.pago, idAplicacion: this.parametros.plataforma, idTransaccion: 0, card: 0, cuota: 0, diferidos: 0, banco: this.banco, gracia: this.gracia, recurrencia: false };
      this.url = this.global.obtenerCredenciales().conexionResultDatafast + "?datos=" + btoa(JSON.stringify(datos));

      this.scripts.inicializarScriptsDatafast(this.chekoutid);
      this.crearScriptFormularioDataFast(this.chekoutid);
    }, 100);
  }

  public crearScriptFormularioDataFast(checkoutId) {

    const removerAcentos = (str) => {
      return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
    }

    var nombre = this.cliente.Cliente.PrimerNombre.trim() + " " + this.cliente.Cliente.SegundoNombre.trim() + " " + this.cliente.Cliente.Apellido.trim();
    this.titular = removerAcentos(nombre.replace(/\s+/g, ' '));

    if (this.parametros.diferidos == undefined) {
      this.removerScriptsVarios();
      this.scripts.removerScriptsCss(this.global.obtenerCredenciales().conexionCheckoutIdDatafast + checkoutId, "js");
      this.scripts.removerScriptsCss("card-datafast-3-6-9-12.js", "js");
      this.scripts.removerScriptsCss("card-datafast-corriente.js", "js");
      this.scripts.removerScriptsCss("card-datafast-especial.js", "js");
      this.scripts.removerScriptsCss("card-datafast-gracia.js", "js");

      this.scripts.cargarScriptsDinamicamente('checkoutId-datafast', 'card-datafast-3-6-9-12').then(data => {
        this.estado = false;
        var titular = this.titular;
        setTimeout(() => {

          $("input[name='card.holder']").val(titular);
          $("#tipoCredito").val("00");
          $("#token").prop("checked", true);
          $("#diferidos").change(function () {

            if ($("#diferidos").val() == "0") {
              $("#tipoCredito").val("00");
            } else {
              $("#tipoCredito").val("01");
            }
            console.log($("#tipoCredito").val());

          });
        }, 4000);
      }).catch(error => console.log(error));

    } else if (this.parametros.diferidos == "corriente") {
      this.removerScriptsVarios();
      this.scripts.removerScriptsCss(this.global.obtenerCredenciales().conexionCheckoutIdDatafast + checkoutId, "js");
      this.scripts.removerScriptsCss("card-datafast-3-6-9-12.js", "js");
      this.scripts.removerScriptsCss("card-datafast-corriente.js", "js");
      this.scripts.removerScriptsCss("card-datafast-especial.js", "js");
      this.scripts.removerScriptsCss("card-datafast-gracia.js", "js");

      this.scripts.cargarScriptsDinamicamente('checkoutId-datafast', 'card-datafast-corriente').then(data => {
        this.estado = false;
        var titular = this.titular;
        setTimeout(() => {

          $("input[name='card.holder']").val(titular);
          $("#tipoCredito").val("00");
          $("#token").prop("checked", true);
          $("#diferidos").change(function () {
            if ($("#diferidos").val() == "0") {
              $("#tipoCredito").val("00");
            } else {
              $("#tipoCredito").val("01");
            }
            console.log($("#tipoCredito").val());
          });

        }, 4000);
      }).catch(error => console.log(error));
    } else if (this.parametros.diferidos == "especial") {
      this.removerScriptsVarios();
      this.scripts.removerScriptsCss(this.global.obtenerCredenciales().conexionCheckoutIdDatafast + checkoutId, "js");
      this.scripts.removerScriptsCss("card-datafast-3-6-9-12.js", "js");
      this.scripts.removerScriptsCss("card-datafast-corriente.js", "js");
      this.scripts.removerScriptsCss("card-datafast-especial.js", "js");
      this.scripts.removerScriptsCss("card-datafast-gracia.js", "js");

      this.scripts.cargarScriptsDinamicamente('checkoutId-datafast', 'card-datafast-especial').then(data => {
        this.estado = false;
        var titular = this.titular;
        setTimeout(() => {

          var parametro = this.parametros.opciones;
          var opciones = parametro.split(",");

          var diferidos = "";

          const obtenerCodigos = (dif) => {
            var result = "";
            if (dif == 0) {
              result = "0";
            } else if (dif == 3) {
              result = "3";
            } else if (dif == 6) {
              result = "6";
            } else if (dif == 9) {
              result = "9";
            } else if (dif == 12) {
              result = "12";
            }
            return result;
          }

          for (let i = 0; i < opciones.length; i++) {
            diferidos += '<option value="' + obtenerCodigos(opciones[i]) + '">' + (
              opciones[i] == 0 ? "Corriente" :
                opciones[i] == 3 ? "3 Meses Sin Intereses" :
                  opciones[i] == 6 ? "6 Meses Sin Intereses" :
                    opciones[i] == 9 ? "9 Meses Sin Intereses" :
                      opciones[i] == 12 ? "12 Meses Sin Intereses" : "Diferido Incorrecto") + '</option>';
          }

          $("#diferidos").empty();
          $("#diferidos").append(diferidos);

          $("input[name='card.holder']").val(titular);
          $("#tipoCredito").val("00");
          $("#token").prop("checked", true);
          $("#diferidos").change(function () {
            if ($("#diferidos").val() == "0") {
              $("#tipoCredito").val("00");
            } else {
              $("#tipoCredito").val("01");
            }
            console.log($("#tipoCredito").val());
          });

        }, 4000);
      }).catch(error => console.log(error));
    } else if (this.parametros.diferidos == "gracia") {
      this.removerScriptsVarios();
      this.scripts.removerScriptsCss(this.global.obtenerCredenciales().conexionCheckoutIdDatafast + checkoutId, "js");
      this.scripts.removerScriptsCss("card-datafast-3-6-9-12.js", "js");
      this.scripts.removerScriptsCss("card-datafast-corriente.js", "js");
      this.scripts.removerScriptsCss("card-datafast-especial.js", "js");
      this.scripts.removerScriptsCss("card-datafast-gracia.js", "js");

      this.scripts.cargarScriptsDinamicamente('checkoutId-datafast', 'card-datafast-gracia').then(data => {
        this.estado = false;
        var banco = this.banco;
        var titular = this.titular;
        var gracia = this.gracia;
        setTimeout(() => {

          $("input[name='card.holder']").val(titular);
          $("#tipoCredito").val("00");
          $("#token").prop("checked", true);

          if (gracia == "2") {
            $("#diferidos").empty();
            $("#diferidos").append("<option value='0'>Corriente | Sin Diferir</option>"
              + "<option value='3'>3 Meses Sin Intereses</option>"
              + "<option value='6'>6 Meses Sin Intereses</option>"
              + "<option value='9'>9 Meses Sin Intereses</option>"
              + "<option value='12'>12 Meses Sin Intereses</option>");
          } else {
            if (banco == "Diners Club") {
              $("#diferidos").empty();
              $("#diferidos").append("<option value='0'>Corriente | Sin Diferir</option>"
                + "<option value='3'>3 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='6'>6 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='9'>9 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='12'>12 Meses Sin Intereses + 2 meses de gracia</option>");
            } else if (banco == "Banco del Pacífico") {
              $("#diferidos").empty();
              $("#diferidos").append("<option value='0'>Corriente | Sin Diferir</option>"
                + "<option value='3'>3 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='6'>6 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='9'>9 Meses Sin Intereses + 2 meses de gracia</option>"
                + "<option value='12'>12 Meses Sin Intereses + 2 meses de gracia</option>");
            } else {
              $("#diferidos").empty();
              $("#diferidos").append("<option value='0'>Corriente | Sin Diferir</option>"
                + "<option value='3'>3 Meses Sin Intereses</option>"
                + "<option value='6'>6 Meses Sin Intereses</option>"
                + "<option value='9'>9 Meses Sin Intereses</option>"
                + "<option value='12'>12 Meses Sin Intereses</option>");
            }
          }

          $("#diferidos").change(function () {
            if ($("#diferidos").val() == "0") {
              $("#tipoCredito").val("00");
            } else {
              if (gracia == "2") {
                $("#tipoCredito").val("01");
              } else {
                $("#tipoCredito").val("09");
              }
            }
            console.log($("#tipoCredito").val());
            
          });

        }, 4000);
      }).catch(error => console.log(error));
    }
  }

  public removerScriptsVarios() {
    this.scripts.removerScriptsCss("https://test.oppwa.com/v1/static/f23b5b6541ad0e05a4b39d836d8eab2b/js/static.min.js", "js");
    this.scripts.removerScriptsCss("https://ci-mpsnare.iovation.com/snare.js", "js");
    this.scripts.removerScriptsCss("https://ci-mpsnare.iovation.com/script/logo.js", "js");
    $("#loadDeviceId").remove();
  }


}
