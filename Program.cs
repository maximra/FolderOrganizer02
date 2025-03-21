
using File_Organizer__Command_Line_Tool___5;
using Serilog;
using System;
using System.Net.Http.Headers;

public class main_software
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
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Also log to console
            .WriteTo.File("logs/userOperations_.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        Log.Information("Logging information started:");
        Log.CloseAndFlush();
        File_Organizer__Command_Line_Tool___5.EventHandler b = new File_Organizer__Command_Line_Tool___5.EventHandler("unvalid$$$", "unvalid$$$");  
        b.StartSequence();
        b.ModeSelection();
        b.InsertSourceDirectory();
        b.InsertDestinationDirectory();
        b.OperationMenuSelection();



    }
}