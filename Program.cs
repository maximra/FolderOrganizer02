
using File_Organizer__Command_Line_Tool___5;
using Serilog;
using System;
using System.Net.Http.Headers;

public class main_software
{
    public static void Main()
    {
        /*
                 Directory.CreateDirectory("logs");

        // Configure Serilog to log to a file
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() // Also log to console
            .WriteTo.File("logs/temperature_log_.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        Log.Information("Temperature monitoring started.");
        for(int i=0;i<100;i++)
            Log.Information("Current temperature: {Temperature}°C", 1);
        Log.Information("Temperature monitoring finished.");
        Log.CloseAndFlush(); // Ensure all logs are written before exiting
         */


        File_Organizer__Command_Line_Tool___5.EventHandler b = new File_Organizer__Command_Line_Tool___5.EventHandler("unvalid$$$", "unvalid$$$");  
        b.StartSequence();
        b.ModeSelection();
        b.InsertSourceDirectory();
        b.InsertDestinationDirectory();
        b.OperationMenuSelection();


    }
}