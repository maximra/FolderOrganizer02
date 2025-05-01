using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace File_Organizer__Command_Line_Tool___5
{
    class EncryptionAndDecryptionHandler
    {
        byte[] _key= Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes = 128-bit key 
        byte[] _iv= Encoding.UTF8.GetBytes("abcdefghijklmnop");
        string _myInputFileName;
        string _myEncryptedFileName;
        string _myDecryptedFileName;
        public EncryptionAndDecryptionHandler(string inputFileName,string encryptedFileName, string decryptedFileName)
        {
            _myInputFileName = inputFileName;
            _myEncryptedFileName = encryptedFileName;
            _myDecryptedFileName = decryptedFileName;

        }
        public void EncryptFile()
        {
            try
            {
                string plainText = File.ReadAllText(_myInputFileName);
                byte[] encrypted = EncryptStringToBytes_Aes(plainText, _key, _iv);
                File.WriteAllBytes(_myEncryptedFileName, encrypted);
                Console.WriteLine("File encrypted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void DecryptFile()
        {
            try
            {
                byte[] encryptedData = File.ReadAllBytes(_myEncryptedFileName);
                string decryptedText = DecryptStringFromBytes_Aes(encryptedData, _key, _iv);
                File.WriteAllText(_myDecryptedFileName, decryptedText);
                Console.WriteLine("File decrypted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            return msEncrypt.ToArray();
        }
        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new MemoryStream(cipherText);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}
