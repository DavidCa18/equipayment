using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SegurosEquinoccial.Pagos.Datos.Gestion
{
    public class DGesEncriptacion
    {

        private static string cadenaSeguridad = "SegurosEquinoccial//Pagos//{2019}";
        private static string entrada = "DataSegurosEquinoccial";

        public static string Encriptar(string valor)
        {
            byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(valor);
            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(cadenaSeguridad));
            MyMD5CryptoService.Clear();
            var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            MyTripleDESCryptoService.Key = MysecurityKeyArray;
            MyTripleDESCryptoService.Mode = CipherMode.ECB;
            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;
            var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();
            byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);
            MyTripleDESCryptoService.Clear();
            return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
        }

        public static string Desencriptar(string valor)
        {
            byte[] MyDecryptArray = Convert.FromBase64String(valor);
            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(cadenaSeguridad));
            MyMD5CryptoService.Clear();
            var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            MyTripleDESCryptoService.Key = MysecurityKeyArray;
            MyTripleDESCryptoService.Mode = CipherMode.ECB;
            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;
            var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();
            byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyDecryptArray, 0, MyDecryptArray.Length);
            MyTripleDESCryptoService.Clear();
            return UTF8Encoding.UTF8.GetString(MyresultArray);
        }

        public static string CrearKeyAutorizacion(string valor)
        {
            string result = null;

            if (valor != null || valor != "")
            {
                string key = "{0}-{1}";
                key = string.Format(key, valor, DateTime.Now.Ticks);

                DeriveBytes condificacionKey = new Rfc2898DeriveBytes(cadenaSeguridad, Encoding.Unicode.GetBytes(entrada));
                SymmetricAlgorithm algoritmoEncriptacion = new TripleDESCryptoServiceProvider();
                byte[] claveSimetrica = condificacionKey.GetBytes(algoritmoEncriptacion.KeySize >> 3);
                byte[] vectorInicializacion = condificacionKey.GetBytes(algoritmoEncriptacion.BlockSize >> 3);

                ICryptoTransform transform = new TripleDESCryptoServiceProvider().CreateEncryptor(claveSimetrica, vectorInicializacion);
                byte[] input = Encoding.Default.GetBytes(key);
                byte[] buff = new byte[input.Length];
                buff = transform.TransformFinalBlock(input, 0, input.Length);

                result = Convert.ToBase64String(buff);
            }

            return result;
        }

        public static string CodificarBase64(string str)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        public static string DecodificarBase64(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                return "";
            }
        }
    }
}
