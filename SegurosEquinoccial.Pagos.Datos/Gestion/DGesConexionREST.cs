using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesConexionREST
    {
        public static async Task<string> GesEjecutarSolicitudWebREST (string url, string headerData, string bodyData, string type)
        {
            var resultado = "";
            var client = new HttpClient();

            //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", headerData);

            var uri = url;

            HttpResponseMessage response;

            byte[] byteData = Encoding.UTF8.GetBytes(bodyData);
            using (var content = new ByteArrayContent(byteData))
            {

                if (type.Equals("GET"))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.GetAsync(uri);
                    resultado = response.Content.ReadAsStringAsync().Result;
                }
                else if (type.Equals("POST"))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    response = await client.PostAsync(uri, content);
                    resultado = response.Content.ReadAsStringAsync().Result;
                }
            }

            return resultado;
        }

        public static async Task<string> GesEjecutarSolicitudREST (string url, string headerData, string bodyData, string type)
        {
            var resultado = "";

            try
            {
                var client = new HttpClient();

                var uri = url;

                HttpResponseMessage response;

                byte[] byteData = Encoding.UTF8.GetBytes(bodyData);
                using (var content = new ByteArrayContent(byteData))
                {

                    if (type.Equals("GET"))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.GetAsync(uri);
                        resultado = response.Content.ReadAsStringAsync().Result;
                    }
                    else if (type.Equals("POST"))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync(uri, content);
                        resultado = response.Content.ReadAsStringAsync().Result;
                    }
                }

            }
            catch (HttpRequestException e)
            {
                resultado = e.InnerException.Message;
            }
            
            return resultado;
        }

        public static async Task<string> GesEjecutarSolicitudWebRESTPay(string url, string headerData, string bodyData, string type)
        {
            var resultado = "";

            try
            {
                var client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", headerData);

                var uri = url;

                HttpResponseMessage response;

                byte[] byteData = Encoding.UTF8.GetBytes(bodyData);
                using (var content = new ByteArrayContent(byteData))
                {

                    if (type.Equals("GET"))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.GetAsync(uri);
                        resultado = response.Content.ReadAsStringAsync().Result;
                    }
                    else if (type.Equals("POST"))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync(uri, content);
                        resultado = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (HttpException)
            {
                throw;
            }


            return resultado;
        }
    }
}
