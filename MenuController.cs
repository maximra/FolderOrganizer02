using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FileOrganizerSoftware
{
    class MenuController
    {
        FileOperationsHandler _myHandler;
        public bool ModeKey { get; set; }
        public MenuController(string inputSourceDirectory, string inputDestinationDirectory)
        {
            _myHandler = new FileOperationsHandler(inputSourceDirectory, inputDestinationDirectory);
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
        public void FilterSelectionMenu()
        {
            string[] tempStringArray = _myHandler.ReturnAvalialbeExtensions();
            while(true)
            { 
                int temp = 1;
                Console.WriteLine("You have entered the filtration menu, here is the available list of extensions:");
                foreach (string s in tempStringArray)
                {
                    Console.Write(" "+temp);
                    Console.WriteLine(".  "+s);
                    temp++;
                }

                Console.Write("In total you have: ");
                Console.Write(_myHandler.ReturnNumberOfExtension());
                Console.WriteLine(" Extensions available");
                Console.WriteLine("");
                Console.WriteLine("Enter a number that corresponds to the specific extension to apply a filter");
                Console.WriteLine("Enter 4 to see the status of each extension, press 5 to disabled all filters, press 6 to exit submenu");
                Console.WriteLine("");
                string readInput = Console.ReadLine();
                Console.Clear();
                if (int.TryParse(readInput, out int result))
                {
                    switch(result)
                    {
                        case 1:
                            _myHandler.SetExtensionAasFalse(result - 1);
                            break;
                        case 2:
                            _myHandler.SetExtensionAasFalse(result - 1);
                            break;
                        case 3:
                            _myHandler.SetExtensionAasFalse(result - 1);
                            break;
                        case 4:
                            ShowFilterStatus(tempStringArray);
                            break;
                        case 5:
                            RemoveAllFilters();
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Wrong numerical input");
                            break;
                    }
                    if(result==6)
                    {
                        break;
                    }
                    Console.WriteLine("Press any key continue");
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
        public void ShowFilterStatus(string[] tempStringArray)
        {
            int num = 0;
            foreach(string s in tempStringArray)
            {
                Console.Write(" " + num + 1);
                Console.Write(".  " + s+" Status: ");
                Console.WriteLine(_myHandler.ReturnExtensionStatus(num));
                num++;
            }
        }
        public void RemoveAllFilters()
        {
            for(int i=0;i<_myHandler.ReturnNumberOfExtension();i++)
            {
                _myHandler.SetExtensionAsTrue(i);
            }
            Console.WriteLine("All filters were removed");

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
                Console.WriteLine("Enter 8 to apply filters :) ");
                Console.WriteLine("Enter 9 to exit software");
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
                            FilterSelectionMenu();
                            break;
                        case 9:
                            break;
                        default:
                            Console.WriteLine("Wrong numerical input");
                            break;
                    }
                    if (result == 9)
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
