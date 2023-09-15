"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Globales = /** @class */ (function () {
    function Globales() {
        this.ambiente = "PRUEBAS";
    }
    Globales.prototype.obtenerCredenciales = function () {
        var credenciales = {};
        if (this.ambiente == "DESARROLLO") {
            credenciales.conexionAPI = "http://localhost/SegurosEquinoccial.Pagos.Servicio.V2/";
            credenciales.conexionCheckoutIdDatafast = "https://test.oppwa.com/v1/paymentWidgets.js?checkoutId=";
            credenciales.conexionResultDatafast = "https://localhost:44332/pago/resultado";
            credenciales.conexionIP = "https://api.ipify.org";
            credenciales.conexionAplicacion = "https://localhost:44332";
            credenciales.codigoEncriptacion = "e9dcdf8c4bed413d9678cd682f58dfc8";
        }
        else if (this.ambiente == "PRUEBAS") {
            credenciales.conexionAPI = "https://equi-prodbpaymentapiappservice.azurewebsites.net/";
            credenciales.conexionCheckoutIdDatafast = "https://test.oppwa.com/v1/paymentWidgets.js?checkoutId=";
            credenciales.conexionResultDatafast = "https://equi-prodbpaymentappservice.azurewebsites.net/pago/resultado";
            credenciales.conexionIP = "https://api.ipify.org";
            credenciales.conexionAplicacion = "https://equi-prodbpaymentappservice.azurewebsites.net";
            credenciales.codigoEncriptacion = "e9dcdf8c4bed413d9678cd682f58dfc8";
        }
        else if (this.ambiente == "PRODUCCION") {
            credenciales.conexionAPI = "https://equipaymentservice.azurewebsites.net/";
            credenciales.conexionCheckoutIdDatafast = "https://oppwa.com/v1/paymentWidgets.js?checkoutId=";
            credenciales.conexionResultDatafast = "https://equipayment.azurewebsites.net/pago/resultado";
            credenciales.conexionIP = "https://api.ipify.org";
            credenciales.conexionAplicacion = "https://equipayment.azurewebsites.net";
            credenciales.codigoEncriptacion = "478612b638e54149b18f4d5634176720";
        }
        return credenciales;
    };
    return Globales;
}());
exports.Globales = Globales;
//# sourceMappingURL=globales.service.js.map