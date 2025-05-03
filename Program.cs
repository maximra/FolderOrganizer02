
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

        Console.Clear();
        Console.Write("Press any key to encrypt & decrypt your logger file");
        Console.ReadKey();
        Console.Clear();

        AesEncryptionAndDecryptionService myFileEncryptionService = new AesEncryptionAndDecryptionService(inputFileName, encryptedFileName, decryptedFileName);
        myFileEncryptionService.EncryptFile();
        myFileEncryptionService.DecryptFile();
    }
    
}