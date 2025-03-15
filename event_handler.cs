using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace File_Organizer__Command_Line_Tool___5
{
    class event_handler
    {
        file_operations_handling my_handler;
        public bool mode_key { get; set; }
        public event_handler(string source_directory, string destination_directory)
        {
            my_handler = new file_operations_handling(source_directory, destination_directory);
            mode_key = false;
        }
        public void start_sequence()
        {
            Console.WriteLine("This script can run in dry or active mode");
            Console.WriteLine("");
            Console.WriteLine("If you wish it to run it in active mode press 1");
            Console.WriteLine("");
            Console.WriteLine("If you wish it to run it in dry mode press 2");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue..");
            Console.WriteLine("");
            Console.ReadKey();
            Console.Clear();
        }
        public void mode_selection()
        {
            int number;
            while (true)
            {
                Console.WriteLine("Enter number:");
                Console.WriteLine("");
                string read_input = Console.ReadLine();
                Console.WriteLine("");
                if (int.TryParse(read_input, out int result))
                {
                    number = Convert.ToInt32(read_input);
                    if (number == 1)
                    {
                        mode_key = true;
                        Console.WriteLine("Active mode selected..");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key to contunue...");
                        Console.WriteLine("");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if (number == 2)
                    {
                        mode_key = false;
                        Console.WriteLine("Dry mode selected..");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key to contunue...");
                        Console.WriteLine("");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("wrong numberical input, try again..");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key to contunue...");
                    }
                }
                else
                {
                    Console.WriteLine("You didn't even enter a number..  try again");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue");

                }
                Console.WriteLine("");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void insert_source_directory()
        {
            while (true)
            {
                Console.WriteLine("Please enter the source directory..");
                Console.WriteLine("");
                Console.WriteLine("");
                my_handler.modify_source(Console.ReadLine());
                if (my_handler.verify_source())
                {
                    Console.WriteLine("Valid source directory");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid source directory");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
        public void insert_destination_directory()
        {
            while (true)
            {
                Console.WriteLine("Please enter the destination directory..");
                Console.WriteLine("");
                Console.WriteLine("");
                my_handler.modify_destination(Console.ReadLine());
                if (my_handler.verify_destination())
                {
                    Console.WriteLine("Valid destination directory");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid destination directory");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
        public void operation_menu_selection()
        {
            while(true)
            {
                Console.WriteLine("This is the operations table that is currently availabe:");
                Console.WriteLine("Enter 1 to use standard copy ");
                Console.WriteLine("Enter 2 to use organized copy");
                Console.WriteLine("Enter 3 to generate an internal subcopy folder");
                Console.WriteLine("Enter 4 to generate an internal organized subcopy folder");
                Console.WriteLine("Enter 5 to switch select dry or wet mode");
                Console.WriteLine("Enter 6 to switch source directory");
                Console.WriteLine("Enter 7 to switch destination directiory");
                Console.WriteLine("Enter 8 to exit software ");
                string read_input = Console.ReadLine();
                Console.WriteLine("");
                Console.Clear();
                Console.WriteLine("");
                if (int.TryParse(read_input, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            my_handler.perform_copy_operation(mode_key);
                            break;
                        case 2:
                            my_handler.perform_organized_copy_operation(mode_key);
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            mode_selection();
                            break;
                        case 6:
                            insert_source_directory();
                            break;
                        case 7:
                            insert_destination_directory();
                            break;
                        case 8:
                            break;
                        default:
                            Console.WriteLine("Wrong numerical input");
                            break;
                    }
                    if (result == 8)
                        break;
                    Console.WriteLine("Press any key to continue");
                    
                }
                else
                {
                    Console.WriteLine("You didn't even enter a number..  try again");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue");
                }
                Console.WriteLine("");
                Console.ReadKey();
                Console.Clear();
            }

          
        }


    }
}
