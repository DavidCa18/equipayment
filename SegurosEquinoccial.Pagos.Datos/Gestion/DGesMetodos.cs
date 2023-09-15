using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesMetodos
    {
        public static bool AdmVerificarJson(string pagoJson, int tipo)
        {
            bool estado = true;
            if (tipo == 1)
            {
                try
                {
                    dynamic datosJson = JObject.Parse(pagoJson);
                    if (datosJson.payments == null)
                    {
                        estado = false;
                    }
                }
                catch (Exception e)
                {
                    estado = false;
                }
            }
            else if (tipo == 2)
            {
                try
                {
                    string payphone = "{\"platform\":\"PAYPHONE\",\"payments\": " + pagoJson  + " }";
                    JObject datosJson = JObject.Parse(payphone);
                    JArray transacciones = (JArray)datosJson["payments"];
                }
                catch (Exception e)
                {
                    estado = false;
                }
            }

            return estado;
        }

        public static string limpiarJson (string pagoJson)
        {
            string dato = pagoJson.Replace("\\", "");
            return dato.Substring(1, (dato.Length - 2));
        }

        public static string limpiarStringJson(string pagoJson)
        {
            string datos = JsonConvert.SerializeObject(pagoJson);
            string datos1 = datos.Replace("\\r", "");
            string datos2 = datos1.Replace("\\n", "");
            string datos3 = datos2.Replace("\\\"", "\"");
            return datos3.Substring(1, (datos3.Length - 2));
        }

        public static string obtenerFecha(string parametro, int tipo)
        {
            string resultado = "";
            try
            {
                if (tipo == 1)
                {
                    DateTime fechaObtenida = DateTime.Parse(parametro);
                    DateTime fechaCorrecta = fechaObtenida.AddHours(-5);
                    resultado = fechaCorrecta.ToString("dd/MM/yyyy HH:mm:ss");
                }else if (tipo == 2)
                {
                    DateTime fechaObtenida = DateTime.Parse(parametro);
                    DateTime fechaCorrecta = fechaObtenida.AddHours(-5);
                    resultado = fechaCorrecta.ToString("dd/MM/yyyy");
                }else if (tipo == 3)
                {
                    DateTime fechaObtenida = DateTime.Parse(parametro);
                    DateTime fechaCorrecta = fechaObtenida.AddHours(-5);
                    resultado = fechaCorrecta.ToString("HH:mm:ss");
                }
            }
            catch (Exception e)
            {
                if (tipo == 1)
                {
                    resultado = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else if (tipo == 2)
                {
                    resultado = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else if (tipo == 3)
                {
                    resultado = DateTime.Now.ToString("HH:mm:ss");
                }
                
            }

            return resultado;
        }

    }
}