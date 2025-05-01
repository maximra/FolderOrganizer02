
using FileOrganizerSoftware;
using Serilog;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;
using File_Organizer__Command_Line_Tool___5;

public class FileOrganizerApp
{
    public static void Main()
    {
        string encryptedFileName = "encrypted.txt";
        string decryptedFileName = "decrypted.txt";
        string inputFileName;
        {
            try
            {
                if (!Directory.Exists("logs"))
                    Directory.CreateDirectory("logs");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }
            AppLogger.Instance.Information("Logging information started.");
            inputFileName = AppLogger.GetCurrentLogFileName();       // get the name for the encryption


            FileOrganizerSoftware.MenuController softwareTester = new FileOrganizerSoftware.MenuController("unvalid$$$", "unvalid$$$");
            softwareTester.StartSequence();
            softwareTester.ModeSelection();
            softwareTester.InsertSourceDirectory();
            softwareTester.InsertDestinationDirectory();
            softwareTester.OperationMenuSelection();

            AppLogger.Instance.Information("Logging infromation ended.");

            AppLogger.Instance.CloseAndFlush();

        }



        // AES key and IV (Initialization Vector)
        // For production, securely store and handle these values
        Console.Clear();
        Console.Write("Press any key to encrypt & decrypt your logger file");
        Console.ReadKey();
        Console.Clear();
        EncryptionAndDecryptionHandler encryptionAndDecryptionTester = new EncryptionAndDecryptionHandler(inputFileName, encryptedFileName, decryptedFileName);
        encryptionAndDecryptionTester.EncryptFile();
        encryptionAndDecryptionTester.DecryptFile();


        // byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes = 128-bit key
        // byte[] iv = Encoding.UTF8.GetBytes("abcdefghijklmnop");  // 16 bytes IV
        /*
         try
         {
             string plainText = File.ReadAllText(inputFileName);
             byte[] encrypted = EncryptStringToBytes_Aes(plainText, key, iv);
             File.WriteAllBytes(encryptedFileName, encrypted);
             Console.WriteLine("File encrypted successfully.");
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Error: {ex.Message}");
         }
         try
         {
             byte[] encryptedData = File.ReadAllBytes(encryptedFileName);
             string decryptedText = DecryptStringFromBytes_Aes(encryptedData, key, iv);
             File.WriteAllText(decryptedFileName, decryptedText);
             Console.WriteLine("File decrypted successfully.");
         }
         catch (Exception ex)
         {
             Console.WriteLine($"Error: {ex.Message}");
         }
         */
    }
    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] key, byte[] iv)
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
    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key, byte[] iv)
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