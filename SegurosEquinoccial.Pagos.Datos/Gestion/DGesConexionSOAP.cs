using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesConexionSOAP
    {
        public static HttpWebRequest GesCrearSolicitudWebSOAP(string url, string accion)
        {
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"" + url + "");
            Req.Headers.Add(@"SOAPAction:" + accion + "");
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            Req.Method = "POST";
            return Req;
        }

        public static string GesEjecutarSolicitudWebSOAP(string url, string accion, string body)
        {
            var ServiceResult = "";
            string resultado = "";
            HttpWebRequest request = GesCrearSolicitudWebSOAP(url, accion);

            XmlDocument SOAPReqBody = new XmlDocument();
            SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body>" + body + "</soap:Body></soap:Envelope>");

            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(ServiceResult);
                        resultado = ServiceResult;
                    }
                }
            }
            catch (WebException webex)
            {
                resultado = webex.ToString();
            }

            return resultado;
        }

        public static string BroEjecutarSolicitudWebSOAPEmail(string url, string metodo, bool validarHead, string head, string body)
        {
            var ServiceResult = "";
            string resultado = "";
            HttpWebRequest request = GesCrearSolicitudWebSOAP(url, metodo);

            string cabecera = "";

            if (validarHead)
            {
                cabecera = head;
            }

            XmlDocument SOAPReqBody = new XmlDocument();
            SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ema=""http://email.ws.tandiesbutilitarios.application.mule.org/"">" + cabecera + "<soap:Body>" + body + "</soap:Body></soap:Envelope>");
            try
            {
                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(ServiceResult);
                        resultado = ServiceResult;
                    }
                }
            }
            catch (WebException webex)
            {
                resultado = webex.ToString();
            }

            return resultado;
        }

    }
}
