using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace CryptoApp.Util
{
    class CryptCls
    {
        //prueba
        //Prueba dos
        /// <summary>
        /// Cifra una cadena de texto con el algoritmo Rijndael
        /// </summary>
        /// <param name="cadena">Cadena de texto sin cifrar</param>
        /// <returns>Texto cifrado</returns>
        public static string encryptString(String cadena)
        {
			//METODO de encriptación: Rijndael
			// Definir el tamaño de la clave y el vector de inicio a utilizarse
			int keySize = 32;
            int ivSize = 16;
			
			//Para contraseña del correo en el Portal Trabaja con nosotros
			/*
            byte[] key = UTF8Encoding.UTF8.GetBytes("G4ndH1");//Clave de cifrado para el algoritmo
            byte[] iv = UTF8Encoding.UTF8.GetBytes("St4ffp1ck");//Vector de inicio para el algoritmo
			*/

			//Para contraseña del usuario en los SW de GandhiGcom
			/*
			byte[] key = UTF8Encoding.UTF8.GetBytes("G4ndH1");//Clave de cifrado para el algoritmo
			byte[] iv = UTF8Encoding.UTF8.GetBytes("G4ndH1C0m5w");//Vector de inicio para el algoritmo
			*/

			string keyEncode = ConfigurationManager.AppSettings["Key.encode"].ToString();
			string vectorStart = ConfigurationManager.AppSettings["Vector.start"].ToString();
			byte[] key = UTF8Encoding.UTF8.GetBytes(keyEncode);//Clave de cifrado para el algoritmo
			byte[] iv = UTF8Encoding.UTF8.GetBytes(vectorStart);//Vector de inicio para el algoritmo

			Array.Resize<byte>(ref key, keySize);
            Array.Resize<byte>(ref iv, ivSize);

            //Se crea una instancia del algoritmo de encriptación Rijndael
            Rijndael rijndael = Rijndael.Create();
            //Creando un flujo de memoria para el cifrado de datos.
            MemoryStream memoryStrm = new MemoryStream();
            //Crea un flujo de cifrado que se basa en el de datos.
            CryptoStream flujoCifrado = new CryptoStream(memoryStrm, rijndael.CreateEncryptor(key, iv), CryptoStreamMode.Write);

            //Obteneniendo la representación en bytes de la información a cifrar
            byte[] cadenaBytes = UTF8Encoding.UTF8.GetBytes(cadena);

            //Se cifran los datos para enviarlos al flujo
            flujoCifrado.Write(cadenaBytes, 0, cadenaBytes.Length);
            flujoCifrado.FlushFinalBlock();

            //Obteniendo los datos cifrados como un arreglo de bytes
            byte[] mensjaeCifradoBytes = memoryStrm.ToArray();

            //Se cierran los flujos utilizados.
            memoryStrm.Close();
            flujoCifrado.Close();

            //Se retorna la representación de texto de los datos cifrados.
            return Convert.ToBase64String(mensjaeCifradoBytes);

        }

        /// <summary>
        /// Descifra una cadena de texto con el algoritmo Rijndael
        /// </summary>
        /// <param name="cadena">Cadena de texto cifrada</param>      
        /// <returns>Texto descifrado</returns>
        public static string decryptString(String cadenaCifrada)
        {

            int keySize = 32;
            int ivSize = 16;

			//Para contraseña del correo en el Portal Trabaja con nosotros
			/*
            byte[] key = UTF8Encoding.UTF8.GetBytes("G4ndH1");//Clave de cifrado para el algoritmo
            byte[] iv = UTF8Encoding.UTF8.GetBytes("St4ffp1ck");//Vector de inicio para el algoritmo						
			*/

			//Para contraseña del usuario en el SW ConsultaPedidosGcom
			/*
			byte[] key = UTF8Encoding.UTF8.GetBytes("G4ndH1");//Clave de cifrado para el algoritmo
			byte[] iv = UTF8Encoding.UTF8.GetBytes("G4ndH1C0m5w");//Vector de inicio para el algoritmo
			*/
			string keyEncode = ConfigurationManager.AppSettings["Key.encode"].ToString();
			string vectorStart = ConfigurationManager.AppSettings["Vector.start"].ToString();
			byte[] key = UTF8Encoding.UTF8.GetBytes(keyEncode);//Clave de cifrado para el algoritmo
			byte[] iv = UTF8Encoding.UTF8.GetBytes(vectorStart);//Vector de inicio para el algoritmo
																  // Garantizar el tamaño correcto de la clave y el vector de inicio
																  // mediante substring o padding
			Array.Resize<byte>(ref key, keySize);
            Array.Resize<byte>(ref iv, ivSize);

            // Obtener la representación en bytes del texto cifrado
            byte[] mensajeCifradoBytes = Convert.FromBase64String(cadenaCifrada);

            // Crear un arreglo de bytes para almacenar los datos descifrados
            byte[] cadenaBytes = new byte[mensajeCifradoBytes.Length];

            // Crear una instancia del algoritmo de Rijndael
            Rijndael rijndael = Rijndael.Create();

            // Crear un flujo en memoria con la representación de bytes de la información cifrada
            MemoryStream memoryStrm = new MemoryStream(mensajeCifradoBytes);

            // Crear un flujo de descifrado basado en el flujo de los datos
            CryptoStream flujoCifrado = new CryptoStream(memoryStrm, rijndael.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            // Obtener los datos descifrados obteniéndolos del flujo de descifrado
            int decryptedByteCount = flujoCifrado.Read(cadenaBytes, 0, cadenaBytes.Length);

            // Cerrar los flujos utilizados
            memoryStrm.Close();
            flujoCifrado.Close();

            // Retornar la representación de texto de los datos descifrados
            return Encoding.UTF8.GetString(cadenaBytes, 0, decryptedByteCount);
        }

    }
}
