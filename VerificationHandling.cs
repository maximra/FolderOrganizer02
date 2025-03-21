using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Organizer__Command_Line_Tool___5
{
    abstract class VerificationHandling       // Our abstract base for handling verifications 
    {
        public string Address { get; set; }
        public bool AddressStatus { get; set; }
        public VerificationHandling(string Address)
        {
            this.Address = Address;
            AddressStatus = false;
        }
        public VerificationHandling(VerificationHandling other)
        {
            this.Address = other.Address;
            AddressStatus = other.AddressStatus;
        }
        public virtual bool isValidFolderPath()
        {
            if (string.IsNullOrWhiteSpace(Address)) return false;

            //check if there are any illegal characters
            char[] invalidChars = { ':', '*', '?', '"', '<', '>', '|' };

            foreach (char c in Address)
            {
                if (invalidChars.Contains(c))
                {
                    if (c == ':' && Address[1] == ':' && Address[0] == 'C')
                    {
                        // ignore special case
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            // Check if path is too long
            if (Address.Length > 260) return false;

            // Extract folder name (last part of the path)
            string folderName = Path.GetFileName(Address.TrimEnd(Path.DirectorySeparatorChar));

            // Check for reserved names
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                               "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            if (reservedNames.Contains(folderName, StringComparer.OrdinalIgnoreCase)) return false;

            return true;
        }
        public virtual void FixFolderPath()
        {
            Address = Address.Trim();

            // Replace all types of slashes with the correct separator
            Address = Address.Replace('/', '\\');

            // Remove redundant backslashes
            while (Address.Contains("\\\\"))
            {
                Address = Address.Replace("\\\\", "\\");
            }

            // Ensure absolute path
            try
            {
                Address = Path.GetFullPath(Address);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path detected.");
            }
        }
        public virtual bool isFolderExisting()
        {
            try
            {
                // Validate and normalize the path
                if (string.IsNullOrWhiteSpace(Address)) return false;
                string fullPath = Path.GetFullPath(Address.Trim());

                // Check if the directory exists
                if (!Directory.Exists(fullPath)) return false;

                // Additional validation: Ensure it's a real directory (not a broken symlink)
                var dirInfo = new DirectoryInfo(fullPath);
                if ((dirInfo.Attributes & FileAttributes.Directory) == 0) return false;

                // Avoid unnecessary security checks (can throw exceptions on system folders)
                return true;
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is PathTooLongException || ex is NotSupportedException)
            {
                Console.WriteLine($"Error checking directory: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }
    }

    class VerificationHandlingSourceFolder: VerificationHandling
    {
        public VerificationHandlingSourceFolder(string address) : base(address)
        {
        }
        public VerificationHandlingSourceFolder(VerificationHandling other) : base(other)
        {
        }

        public virtual void PerformValidationCheck()      // perform entire validation check for source folder
        {
            if (isValidFolderPath())     // Check whether or not it is even relevant 
            {
                FixFolderPath();          // Fix minor mistakes **before** checking existence
                if (isFolderExisting())
                {
                    AddressStatus = true;
                }
                else
                {
                    AddressStatus = false;
                }
            }
            else
            {
                AddressStatus = false;
            }
        }
    }
    class VerificationHandlingDestinationFolder: VerificationHandling
    {
        public VerificationHandlingDestinationFolder(string address) : base(address)
        {
        }
        public VerificationHandlingDestinationFolder(VerificationHandlingDestinationFolder other): base(other)
        {
        }
        public virtual void PerformValidationCheck()      // perform entire validation check for destination folder
        {
            if (isValidFolderPath())     // Check whether or not it is even relevant 
            {
                FixFolderPath();          // Fix minor mistakes **before** checking existence
                if (!Directory.Exists(Address))
                {
                    try
                    {
                        Directory.CreateDirectory(Address);
                        Console.WriteLine($"Created destination directory: {Address}");
                    }
                    catch (Exception e)     // In case the user gives a junk input
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
                if (isFolderExisting())
                {
                    AddressStatus = true;
                }
                else
                {
                    AddressStatus = false;
                }
            }
            else
            {
                AddressStatus = false;
            }
        }
    }

}
