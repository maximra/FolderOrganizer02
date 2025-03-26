using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FileOrganizerSoftware
{
    class EventHandler
    {
        FileOperationsHandling _myHandler;
        public bool ModeKey { get; set; }
        public EventHandler(string inputSourceDirectory, string inputDestinationDirectory)
        {
            _myHandler = new FileOperationsHandling(inputSourceDirectory, inputDestinationDirectory);
            ModeKey = false;
        }
        public void StartSequence()
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
        public void ModeSelection()
        {
            int number;
            while (true)
            {
                Console.WriteLine("Enter number:");
                Console.WriteLine("");
                string readInput = Console.ReadLine();
                Console.WriteLine("");
                if (int.TryParse(readInput, out int result))
                {
                    number = Convert.ToInt32(readInput);
                    if (number == 1)
                    {
                        ModeKey = true;
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
                        ModeKey = false;
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
        public void InsertSourceDirectory()
        {
            while (true)
            {
                Console.WriteLine("Please enter the source directory..");
                Console.WriteLine("");
                Console.WriteLine("");
                _myHandler.ModifySource(Console.ReadLine());
                if (_myHandler.VerifySource())
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
        public void InsertDestinationDirectory()
        {
            while (true)
            {
                Console.WriteLine("Please enter the destination directory..");
                Console.WriteLine("");
                Console.WriteLine("");
                _myHandler.ModifyDestination(Console.ReadLine());
                if (_myHandler.VerifyDestination())
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
        public void OperationMenuSelection()
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
                Console.WriteLine("");
                string readInput = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(readInput, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            _myHandler.PerformCopyOperation(ModeKey);
                            break;
                        case 2:
                            _myHandler.PerformOrganizedCopyOperation(ModeKey);
                            break;
                        case 3:
                            string temp = _myHandler.GetDestinationAddress();
                            Console.WriteLine("Please enter the desired folder name");
                            Console.WriteLine("");
                            _myHandler.ModifyDestination(_myHandler.GetDestinationAddress() + "\\" +Console.ReadLine());
                            if (_myHandler.VerifyDestination())
                                _myHandler.PerformCopyOperation(ModeKey);
                            else
                                Console.WriteLine("Failed to create sub file");
                            _myHandler.ModifyDestination(temp);
                            break;
                        case 4:
                            string temp2 = _myHandler.GetDestinationAddress();
                            Console.WriteLine("Please enter the desired folder name");
                            Console.WriteLine("");
                            _myHandler.ModifyDestination(_myHandler.GetDestinationAddress() + "\\" + Console.ReadLine());
                            if (_myHandler.VerifyDestination())
                                _myHandler.PerformOrganizedCopyOperation(ModeKey);
                            else
                                Console.WriteLine("Failed to create sub file");
                            _myHandler.ModifyDestination(temp2);
                            break;
                        case 5:
                            ModeSelection();
                            break;
                        case 6:
                            InsertSourceDirectory();
                            break;
                        case 7:
                            InsertDestinationDirectory();
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
