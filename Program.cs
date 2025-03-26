
using FileOrganizerSoftware;
using Serilog;
using System;
using System.Net.Http.Headers;

public class FileOrganizerApp
{
    public static void Main()
    {
        try
        {
            if(!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return ;                                       
        }
        AppLogger.Instance.Information("Logging information started.");




        FileOrganizerSoftware.EventHandler softwareTester = new FileOrganizerSoftware.EventHandler("unvalid$$$", "unvalid$$$");  
        softwareTester.StartSequence();
        softwareTester.ModeSelection();
        softwareTester.InsertSourceDirectory();
        softwareTester.InsertDestinationDirectory();
        softwareTester.OperationMenuSelection();

        AppLogger.Instance.Information("Logging infromation ended.");

        AppLogger.Instance.CloseAndFlush();


    }
}