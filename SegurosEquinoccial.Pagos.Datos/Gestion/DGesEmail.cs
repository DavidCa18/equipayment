using SegurosEquinoccial.Pagos.Datos.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Administracion;
using SegurosEquinoccial.Pagos.Entidad.Auxiliares;
using SegurosEquinoccial.Pagos.Entidad.Globales;
using System;
using System.IO;
using System.Net.Mail;
//using EASendMail;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesEmail
    {

        public static string enviaEmail(EAdmEmail correo, int idPago)
        {
            /*if (EGloGlobales.ambiente.Equals("PRODUCCION"))
            {
                return enviaEmailServicio(correo, idPago);
            }
            else
            {
                return enviaEmailNormal(correo, idPago);
            }*/
            return enviaEmailNormal(correo, idPago);
        }

        public static string enviaEmailNormal(EAdmEmail correo, int idPago)
        {
            string resultado = "0";

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CORREOELECTRONICO");
            EAdmPago pago = DAdmPago.AdmConsultaReciboPago(idPago);

            MailMessage mensaje = new MailMessage();
            SmtpClient cliente = new SmtpClient(credenciales.EntityId);

            cliente.Host = credenciales.EntityId;
            cliente.Port = Convert.ToInt32(credenciales.MID);
            cliente.EnableSsl = true;
            cliente.Timeout = 100000;

            mensaje.IsBodyHtml = true;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.Priority = MailPriority.Normal;

            string correos = correo.Para;
            string[] lstCorreos = correos.Split(';');

            if (lstCorreos.Length > 1)
            {
                foreach (string dest in lstCorreos)
                {
                    mensaje.To.Add(dest);
                }
            }
            else
            {
                mensaje.To.Add(correo.Para);
            }

            mensaje.Subject = correo.Asunto;
            mensaje.Body = correo.Mensaje;
            mensaje.From = new MailAddress(credenciales.UserId);
            cliente.Credentials = new System.Net.NetworkCredential(credenciales.UserId, credenciales.Password);

            byte[] stemp = Convert.FromBase64String(pago.Recibo);
            Stream stream = new MemoryStream(stemp);
            mensaje.Attachments.Add(new Attachment(stream, Path.GetFileName("voucher_" + pago.Voucher + ".pdf"), "application/pdf"));

            try
            {
                cliente.Send(mensaje);
                resultado = "Exito ";
            }
            catch (SmtpException)
            {
                throw;

            }

            return resultado;
        }


        public static string enviaEmailServicio(EAdmEmail correo, int idPago)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CORREOELECTRONICO");
            EAdmPago pago = DAdmPago.AdmConsultaReciboPago(idPago);

            string ServicioURL = credenciales.Url;
            string AccionSOAP = credenciales.UserId;

            string head = "<soap:Header>"
            + "</soap:Header>";

            string body = ("<ema:envioCorreoCertificado>"
                + "<token>" + credenciales.Password + "</token>"
                + "<contenidoCorreo><![CDATA[" + correo.Mensaje + "]]></contenidoCorreo>"
                + "<asunto>" + correo.Asunto + "</asunto>"
                + "<destinatarios>" + correo.Para + "</destinatarios>"
                + "<copiaOculta></copiaOculta>"
                + "<referencia></referencia>"
                + "<archivosAdjuntos>"
                + "<data>" + pago.Recibo + "</data>"
                + "<fileName>" + pago.Voucher + ".pdf" + "</fileName>"
                + "<mime>application/pdf</mime>"
                + "</archivosAdjuntos>"
                + "<logo></logo>"
                + "</ema:envioCorreoCertificado>");


            string resultado = DGesConexionSOAP.BroEjecutarSolicitudWebSOAPEmail(ServicioURL, AccionSOAP, true, head, body);

            return resultado;

        }


        public static string enviarEmailMasivos(string cliente_, string ramo, string poliza, string deuda, string link, string para, string asunto)
        {

            /*if (EGloGlobales.ambiente.Equals("PRODUCCION"))
            {
                return enviaEmailMasivosServicio(cliente_, ramo, poliza, deuda, link, para, asunto);
            }
            else
            {
                return enviarEmailMasivosNormal(cliente_, ramo, poliza, deuda, link, para, asunto);
            }*/

            return enviarEmailMasivosNormal(cliente_, ramo, poliza, deuda, link, para, asunto);
        }

        public static string enviarEmailMasivosNormal(string cliente_, string ramo, string poliza, string deuda, string link, string para, string asunto)
        {
            string resultado = "0";

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CORREOELECTRONICOMASIVOS");
            string texto_mensaje = DGesPlantilla.plantillaPago(cliente_, ramo, poliza, deuda, link);

            MailMessage mensaje = new MailMessage();
            SmtpClient cliente = new SmtpClient(credenciales.EntityId);

            cliente.Host = credenciales.EntityId;
            cliente.Port = Convert.ToInt32(credenciales.MID);
            cliente.EnableSsl = true;
            cliente.Timeout = 100000;

            mensaje.IsBodyHtml = true;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.Priority = MailPriority.Normal;


            string correos = para;
            string[] lstCorreos = correos.Split(';');

            if (lstCorreos.Length > 1)
            {
                foreach (string dest in lstCorreos)
                {
                    mensaje.To.Add(dest);
                }
            }
            else
            {
                mensaje.To.Add(para);
            }

            mensaje.Subject = asunto;
            mensaje.Body = texto_mensaje;
            mensaje.From = new MailAddress(credenciales.UserId);
            cliente.Credentials = new System.Net.NetworkCredential(credenciales.UserId, credenciales.Password);

            try
            {
                cliente.Send(mensaje);
                resultado = "Exito";
            }
            catch (SmtpException)
            {
                throw;

            }
            return resultado;
        }


        public static string enviaEmailMasivosServicio(string cliente_, string ramo, string poliza, string deuda, string link, string para, string asunto)
        {

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CORREOELECTRONICOMASIVOS");
            string texto_mensaje = DGesPlantilla.plantillaPago(cliente_, ramo, poliza, deuda, link);

            string ServicioURL = credenciales.Url;
            string AccionSOAP = credenciales.UserId;

            string head = "<soap:Header>"
            + "</soap:Header>";

            string body = ("<ema:envioCorreoCertificado>"
                + "<token>" + credenciales.Password + "</token>"
                + "<contenidoCorreo><![CDATA[" + texto_mensaje + "]]></contenidoCorreo>"
                + "<asunto>" + asunto + "</asunto>"
                + "<destinatarios>" + para + "</destinatarios>"
                + "<copiaOculta></copiaOculta>"
                + "<referencia></referencia>"
                + "<archivosAdjuntos>"
                + "</archivosAdjuntos>"
                + "<logo></logo>"
                + "</ema:envioCorreoCertificado>");


            string resultado = DGesConexionSOAP.BroEjecutarSolicitudWebSOAPEmail(ServicioURL, AccionSOAP, true, head, body);

            return resultado;

        }


        public static string enviaEmailNormalReverso(EAdmEmail correo, int idPago)
        {
            string resultado = "0";

            EAdmCredenciales credenciales = DAdmCredenciales.AdmConsultaCredenciales(EGloGlobales.ambiente, "CORREOELECTRONICO");
            EAdmPago pago = DAdmPago.AdmConsultaReciboPago(idPago);

            MailMessage mensaje = new MailMessage();
            SmtpClient cliente = new SmtpClient(credenciales.EntityId);

            cliente.Host = credenciales.EntityId;
            cliente.Port = Convert.ToInt32(credenciales.MID);
            cliente.EnableSsl = true;
            cliente.Timeout = 100000;

            mensaje.IsBodyHtml = true;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.Priority = MailPriority.Normal;

            string correos = correo.Para;
            string[] lstCorreos = correos.Split(';');

            if (lstCorreos.Length > 1)
            {
                foreach (string dest in lstCorreos)
                {
                    mensaje.To.Add(dest);
                }
            }
            else
            {
                mensaje.To.Add(correo.Para);
            }

            mensaje.Subject = correo.Asunto;
            mensaje.Body = correo.Mensaje;
            mensaje.From = new MailAddress(credenciales.UserId);
            cliente.Credentials = new System.Net.NetworkCredential(credenciales.UserId, credenciales.Password);

            try
            {
                cliente.Send(mensaje);
                resultado = "Exito ";
            }
            catch (SmtpException)
            {
                throw;

            }

            return resultado;
        }

    }
}
