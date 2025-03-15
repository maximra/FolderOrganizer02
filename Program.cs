
using File_Organizer__Command_Line_Tool___5;
using System;
using System.Net.Http.Headers;

public class main_software
{
    public static void Main()
    {
       // file_operations_handling a = new file_operations_handling("C:\\Users\\User\\Desktop", "C:\\Users\\User\\Desktop\\target_folder");
       // a.verify_destination();
        //a.verify_source();
        //a.perform_organized_copy_operation(false);
        //a.perform_copy_operation(false);
       // Console.WriteLine(a.verify_destination());



        event_handler b = new event_handler("unvalid", "unvalid");
        b.start_sequence();
        b.mode_selection();
        b.insert_source_directory();
        b.insert_destination_directory();
        b.operation_menu_selection();


    }
}