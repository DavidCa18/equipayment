var wpwlOptions = {
  style: "plain",
  locale: "es",
  labels: {
    cardHolder: "Titular de la Tarjeta",
    cvv: "CVV",
    cardNumber: "Número de Tarjeta",
    expiryDate: "Expiración",
    submit: "PAGAR"
  },
  spinner: {
    lines: 17,
    length: 0,
    scale: 0.85,
    width: 13,
    color: "#9D9D9D",
    fadeColor: "#595959"
  },
  errorMessages: {
    cvvError: "CVV no válido"
  },
  onReady: function () {
    $(".wpwl-group-brand").before($(".wpwl-label-cardNumber"));
    $(".wpwl-group-cvv").after($(".wpwl-group-cardHolder").detach());

    var createRegistrationHtml = '<div style="display: none">¿Desea almacenar de manera segura sus datos? <input type="checkbox" id="token" name="createRegistration"></div>';

    var diferidos =
      '<div class="wpwl-label wpwl-label-custom" style="display:inline-block;">Diferidos</div>' +
      '<div class="wpwl-wrapper wpwl-wrapper-custom" style="display:inline-block">' +
      '<select id="diferidos" name="recurring.numberOfInstallments" class="wpwl-wrapper-custom-installments" style="outline: none; width: 100%;">' +
      `<option value="0">Corriente | Sin Diferir</option>
       <option value="3">3 Meses Sin Intereses</option>
       <option value="6">6 Meses Sin Intereses</option>
       <option value="9">9 Meses Sin Intereses</option>
       <option value="12">12 Meses Sin Intereses</option>` +
      "</select > " +
      "<br><br></div>";

    var tipoCredito =
      '<div class="wpwl-label wpwl-label-custom" style="display: none">Tipo Crédito:</div>' +
      '<div class="wpwl-wrapper wpwl-wrapper-custom" style="display: none">' +
      '<input type="text" id="tipoCredito" name="customParameters[SHOPPER_TIPOCREDITO]">' +
      "</div>";

    $("form.wpwl-form-card")
      .find(".wpwl-button")
      .before(diferidos);
    $("form.wpwl-form-card")
      .find(".wpwl-button")
      .before(tipoCredito);

    $("form.wpwl-form-card")
      .find(".wpwl-button")
      .before(createRegistrationHtml);
  },
  registrations: {
    requireCvv: true
  }
};
