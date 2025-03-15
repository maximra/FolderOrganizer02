using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Organizer__Command_Line_Tool___5
{
    abstract class verification_handling_       // Our abstract base for handling verifications 
    {
        public string address { get; set; }
        public bool is_valid_address { get; set; }
        public verification_handling_(string address)
        {
            this.address = address;
            is_valid_address = false;
        }
        public verification_handling_(verification_handling_ other)
        {
            this.address = other.address;
            is_valid_address = other.is_valid_address;
        }
        public virtual bool isValidFolderPath()
        {
            if (string.IsNullOrWhiteSpace(address)) return false;

            //check if there are any illegal characters
            char[] invalidChars = { ':', '*', '?', '"', '<', '>', '|' };

            foreach (char c in address)
            {
                if (invalidChars.Contains(c))
                {
                    if (c == ':' && address[1] == ':' && address[0] == 'C')
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
            if (address.Length > 260) return false;

            // Extract folder name (last part of the path)
            string folderName = Path.GetFileName(address.TrimEnd(Path.DirectorySeparatorChar));

            // Check for reserved names
            string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                               "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            if (reservedNames.Contains(folderName, StringComparer.OrdinalIgnoreCase)) return false;

            return true;
        }
        public virtual void FixFolderPath()
        {
            address = address.Trim();

            // Replace all types of slashes with the correct separator
            address = address.Replace('/', '\\');

            // Remove redundant backslashes
            while (address.Contains("\\\\"))
            {
                address = address.Replace("\\\\", "\\");
            }

            // Ensure absolute path
            try
            {
                address = Path.GetFullPath(address);
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
                if (string.IsNullOrWhiteSpace(address)) return false;
                string fullPath = Path.GetFullPath(address.Trim());

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

    class verification_handling_source_folder: verification_handling_
    {
        public verification_handling_source_folder(string address) : base(address)
        {
        }
        public verification_handling_source_folder(verification_handling_ other) : base(other)
        {
        }

        public virtual void perform_validation_check()      // perform entire validation check for source folder
        {
            if (isValidFolderPath())     // Check whether or not it is even relevant 
            {
                FixFolderPath();          // Fix minor mistakes **before** checking existence
                if (isFolderExisting())
                {
                    is_valid_address = true;
                }
                else
                {
                    is_valid_address = false;
                }
            }
            else
            {
                is_valid_address = false;
            }
        }
    }
    class verification_handling_destination_folder: verification_handling_
    {
        public verification_handling_destination_folder(string address) : base(address)
        {
        }
        public verification_handling_destination_folder(verification_handling_destination_folder other): base(other)
        {
        }
        public virtual void perform_validation_check()      // perform entire validation check for destination folder
        {
            if (isValidFolderPath())     // Check whether or not it is even relevant 
            {
                FixFolderPath();          // Fix minor mistakes **before** checking existence
                if (!Directory.Exists(address))
                {
                    try
                    {
                        Directory.CreateDirectory(address);
                        Console.WriteLine($"Created destination directory: {address}");
                    }
                    catch (Exception e)     // In case the user gives a junk input
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                }
                if (isFolderExisting())
                {
                    is_valid_address = true;
                }
                else
                {
                    is_valid_address = false;
                }
            }
            else
            {
                is_valid_address = false;
            }
        }
    }

}
