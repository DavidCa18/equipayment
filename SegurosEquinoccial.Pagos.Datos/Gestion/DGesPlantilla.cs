﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesPlantilla
    {
        public static string plantillaPago(string cliente, string ramo, string poliza, string deuda, string link)
        {
            string html =
                         "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">"
                        + "<html xmlns=\"http://www.w3.org/1999/xhtml\">"
                        + "<head>"
                            + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />"
                            + "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />"
                            + "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
                            + "<title> EQUIPAYMENT </title>"
                        + "</head>"
                        + "<body style=\"Margin:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;min-width:100%;\">"
                            + "<center class=\"wrapper\""
                                + "style=\"width:100%;table-layout:fixed;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;\">"
                                + "<div class=\"webkit\" style=\"max-width:600px;\">"
                                    + "<table class=\"outer\" align=\"center\""
                                        + "style=\"border-spacing:0;font-family:sans-serif;color:#333333;Margin:0 auto;width:100%;max-width:600px;\">"
                                        + "<tr>"
                                            + "<td class=\"full-width-image\""
                                                + "style=\"padding-top:10px;padding-bottom:10px;padding-right:10px;padding-left:0; text-align: right; background-color: #003366\">"
                                                + "<img src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2Fsegurosequinoccialblanco.png?alt=media&token=0435df66-20fb-4928-8c7e-cf5252f0255d\""
                                                    + "alt=\"SEGUROS EQUINOCCIAL\" width=\"170\">"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr>"
                                            + "<td class=\"full-width-image\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<img src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2F1.1.jpg?alt=media&token=dd545b57-3542-4b98-8cc2-79b61e4bcced\" alt=\"Business\""
                                                    + "style=\"border-width:0;width:100%;height:auto;display:block;\" />"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"white-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#3c3c3c;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                            + "style=\"padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\""
                                                                + "style=\"margin:0;color:#003366;text-align:center;padding-top:10px;padding-bottom:10px;padding-right:20px;padding-left:20px;margin-top:0px !important;margin-bottom:0px !important;margin-right:0 !important;margin-left:0 !important;font-size:25px!important;line-height: 1.4 !important;\">"
                                                                + "" + cliente + ""
                                                            + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"white-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#3c3c3c;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                            + "style=\"padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\""
                                                                + "style=\"margin:0;color:#3c3c3c;text-align:center;padding-top:10px;padding-bottom:10px;padding-right:20px;padding-left:20px;margin-top:0px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:15px!important;line-height: 1.4 !important;\">"
                                                                + "Reciba un cordial saludo de <b style=\"color: #003366 !important\">Seguros Equinoccial S.A.</b>"
                                                            + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"white-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#003366;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                            + "style=\"padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\""
                                                                + "style=\"margin:0;color:#3c3c3c;text-align:center;padding-top:10px;padding-bottom:10px;padding-right:20px;padding-left:20px;margin-top:0px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:15px!important;line-height: 1.4 !important;\">"
                                                                + "A continuación detallamos el pago pendiente a realizar:"
                                                            + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"white-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#3c3c3c;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                           + "style=\"padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<div style=\"padding-left: 80px !important; padding-right: 80px !important\">"
                                                                + "<table border=\"0\""
                                                                    + "style=\"font-family: sans-serif !important; color:#3c3c3c !important; width: 100% !important; text-align: left !important; font-size: 15px !important\">"
                                                                    + "<tr>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px;\">"
                                                                            + "<b>Ramo:</b></td>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px;\">"
                                                                            + "" + ramo + "</td>"
                                                                    + "</tr>"
                                                                    + "<tr>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px;\">"
                                                                            + "<b>N° Póliza:</b></td>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px;\">"
                                                                            + "" + poliza + "</td>"
                                                                    + "</tr>"
                                                                    + "<tr>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px;\">"
                                                                            + "<b>Deuda:</b></td>"
                                                                        + "<td"
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px; font-size: 17px !important\">"
                                                                            + "$ " + deuda + "</td>"
                                                                    + "</tr>"
                                                                    + "<tr>"
                                                                        + "<td colspan=\"2\""
                                                                            + "style=\"width: 50% !important;padding-top:5px;padding-bottom:5px;padding-right:10px;padding-left:10px; font-size: 18px !important; text-align: center !important\">"
                                                                            + "<br>"
                                                                            + "<div style=\"width: 100% !important;background-color: #00c1de; color: #FFFFFF; padding-top: 16px !important; padding-bottom: 16px !important; text-align: center !important\">"
                                                                                + "<a href=\"" + link + "\" target=\"_blank\" style=\"color: #FFFFFF; text-decoration: none; width: 100% !important\">"
                                                                                    + "Realizar Pago"
                                                                                + "</a>"
                                                                            + "</div>"
                                                                            + "<br>"
                                                                        + "</td>"
                                                                    + "</tr>"
                                                                + "</table>"
                                                            + "</div>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"white-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#3c3c3c;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\" style=\"padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\" style=\"margin:0;color:#3c3c3c;text-align:center;padding-top:10px;padding-bottom:10px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:0px !important;margin-right:0 !important;margin-left:0 !important;font-size:13.5px!important;line-height:1.5 !important;\">"
                                                                + "Este mensaje ha sido generado automáticamente, por favor no respondas a este correo."
                                                                + "<br>"
                                                                + "<a href=\"https://equipaymentservice.azurewebsites.net/Documentos/POLITICA.pdf\" target=\"_blank\" style=\"text-decoration: none !important; color: #3c3c3c\">"
                                                                    + " Revise aquí nuestra <span style=\"color: #00c1de;\">Política de Privacidad de Datos</span>."
                                                                + "</a>"
                                                            + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class =\"blue-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"three-column three-column-full-width\""
                                                + "style=\"padding-top:30px;padding-bottom:10px;padding-right:0;padding-left:0;text-align:center;font-size:0;\">"
                                                + "<div class=\"column\""
                                                    + "style=\"width:100%;max-width:200px;display:inline-block;vertical-align:top;margin-top:0;margin-bottom:0;margin-right:0;margin-left:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                    + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">"
                                                        + "<tr>"
                                                            + "<td class=\"inner\" style=\"padding-top:10px;padding-bottom:0px;padding-right:0px;padding-left:0px;\">"
                                                                + "<table class=\"contents\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;width:100%;font-size:14px;text-align:center;\">"
                                                                    + "<tr>"
                                                                        + "<td style=\"padding-top:10px;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                                            + "<img src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2F3.jpg?alt=media&token=860602c0-da80-4589-963c-ca5f4356b722\" alt=\"\""
                                                                                + "style=\"border-width:0;height:auto;max-width:55px;display:block;margin:15px auto;\""
                                                                                + "width=\"55\" height=\"50\">"
                                                                            + "<p class=\"h4 center white\""
                                                                                + "style=\"margin:0;color:#2e417b;text-align:center;padding-top:0px;padding-bottom:10px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:13px!important;Margin-bottom:10px;\">"
                                                                                + "<b>1800-EQUINOCCIAL</b> <br>"
                                                                                + "(1800-378466)"
                                                                            + "</p>"
                                                                        + "</td>"
                                                                    + "</tr>"
                                                                + "</table>"
                                                            + "</td>"
                                                        + "</tr>"
                                                    + "</table>"
                                                + "</div>"
                                                + "<div class=\"column\""
                                                    + "style=\"width:100%;max-width:200px;display:inline-block;vertical-align:top;margin-top:0;margin-bottom:0;margin-right:0;margin-left:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                    + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">"
                                                        + "<tr>"
                                                            + "<td class=\"inner\""
                                                                + "style=\"padding-top:10px;padding-bottom:0px;padding-right:0px;padding-left:0px;\">"
                                                                + "<table class=\"contents\""
                                                                    + "style=\"border-spacing:0;font-family:sans-serif;color:#333333;width:100%;font-size:14px;text-align:center;\">"
                                                                    + "<tr>"
                                                                        + "<td"
                                                                            + "style=\"padding-top:0px;padding-bottom:0;padding-right:0;padding-left:0;border-left:2px solid #2e417b;border-right:2px solid #2e417b;\">"
                                                                            + "<a href=\"https://segurosequinoccial.com/\" target=\"_blank\">"
                                                                                + "<img src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2F4.jpg?alt=media&token=2846e433-b96c-42e3-b5b2-49dca1699188\""
                                                                                    + "style=\"border-width:0;height:auto;max-width:55px;display:block;margin:25px auto 15px;\""
                                                                                   + "width=\"55\" height=\"50\">"
                                                                            + "</a>"
                                                                            + "<p class=\"h4 center white\""
                                                                                + "style=\"margin:0;color:#2e417b;text-align:center;padding-top:0px;padding-bottom:10px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:12px!important;Margin-bottom:10px;\">"
                                                                                + "<b>Chat 24/7</b> <br>"
                                                                                + "www.segurosequinoccial.com"
                                                                            + "</p>"
                                                                        + "</td>"
                                                                    + "</tr>"
                                                                + "</table>"
                                                            + "</td>"
                                                        + "</tr>"
                                                    + "</table>"
                                                + "</div>"
                                                + "<div class =\"column\""
                                                    + "style=\"width:100%;max-width:200px;display:inline-block;vertical-align:top;margin-top:0;margin-bottom:0;margin-right:0;margin-left:0;padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                    + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">"
                                                        + "<tr>"
                                                            + "<td class=\"inner\""
                                                                + "style=\"padding-top:10px;padding-bottom:0px;padding-right:0px;padding-left:0px;\">"
                                                                + "<table class=\"contents\""
                                                                    + "style=\"border-spacing:0;font-family:sans-serif;color:#333333;width:100%;font-size:14px;text-align:center;\">"
                                                                    + "<tr>"
                                                                        + "<td"
                                                                            + "style=\"padding-top:10px;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                                            + "<img src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2F5.jpg?alt=media&token=bff5986e-394c-42d6-a324-980c82c747d2\" alt=\"\""
                                                                                + "style=\"border-width:0;height:auto;max-width:55px;display:block;margin:15px auto;\""
                                                                                + "width=\"55\" height=\"50\">"
                                                                            + "<p class=\"h4 center white\""
                                                                                + "style=\"margin:0;color:#2e417b;text-align:center;padding-top:0px;padding-bottom:10px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:12px !important;Margin-bottom:10px;\">"
                                                                                + "<b>Escríbenos</b> <br>"
                                                                                + "info@segurosequinoccial.com"
                                                                            + "</p>"
                                                                        + "</td>"
                                                                    + "</tr>"
                                                                + "</table>"
                                                            + "</td>"
                                                        + "</tr>"
                                                    + "</table>"
                                                + "</div>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class =\"grey-back\" style=\"background-color:#003366;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                            + "style=\"padding-top:10px;padding-bottom:0px;padding-right:10px;padding-left:10px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\""
                                                                + "style=\"Margin:0;color:#ffffff;text-align:center;padding-top:10px;padding-bottom:5px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:0px !important;margin-right:0 !important;margin-left:0 !important;font-size:13px!important;margin-bottom:0px;\">"
                                                                + "<b>Síguenos en: </b>"
                                                                + "<p"
                                                                    + "style=\"Margin:0;color:#ffffff;text-align:center;padding-top:0px;padding-bottom:0px;padding-right:10px;padding-left:10px;margin-top:0px !important;margin-bottom:20px !important;margin-right:0 !important;margin-left:0 !important;font-size:13px!important;\">"
                                                                    + "<a href=\"https://www.facebook.com/SegurosEquinoccial/?fref=ts\""
                                                                        + "target=\"_blank\" style=\"text-decoration: none;color: #ffffff;\"><img"
                                                                           + "src=\"https://firebasestorage.googleapis.com/v0/b/segurosequinoccial-45797.appspot.com/o/CorreoElectronico%2F6.png?alt=media&token=bd6817af-fae2-4340-b194-c2e54e4dd7c9\" alt=\"\""
                                                                            + "style=\"border-width:0;height:auto;max-width:170px;display:inline-block;\"></a>"
                                                                + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                        + "<tr class=\"blue-back\" style=\"background-color:#f7f7f7;\">"
                                            + "<td class=\"one-column\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">"
                                                + "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">"
                                                    + "<tr>"
                                                        + "<td class=\"inner contents\""
                                                            + "style=\"padding-top:0px;padding-bottom:0px;padding-right:20px;padding-left:20px;width:100%;text-align:center;\">"
                                                            + "<p class=\"h4 center grey\""
                                                                + "style=\"Margin:0;color:#2C2F85;text-align:center;padding-top:10px;padding-bottom:0px;padding-right:10px;padding-left:10px;margin-top:10px !important;margin-bottom:10px !important;margin-right:0 !important;margin-left:0 !important;font-size:10px!important;margin-bottom:10px;\">"
                                                                + "Este correo electrónico fue enviado por Seguros Equinoccial<br>"
                                                                + "Dirección: Av. Eloy Alfaro y Ayarza.<br>"
                                                                + "©2020 Derechos Reservados"
                                                                + "</p>"
                                                        + "</td>"
                                                    + "</tr>"
                                                + "</table>"
                                            + "</td>"
                                        + "</tr>"
                                    + "</table>"
                                + "</div>"
                            + "</center>"
                        + "</body>"
                        + "</html>";

            return html;
        }
    }
}
