using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Organizer__Command_Line_Tool___5
{
    class file_operations_handling
    {
        verification_handling_destination_folder my_dest;
        verification_handling_source_folder my_src;
        public file_operations_handling(string my_src_Str, string my_dest_Str)
        {
            my_dest = new verification_handling_destination_folder(my_dest_Str);
            my_src = new verification_handling_source_folder(my_src_Str);
        }
        public bool verify_source()
        {
            my_src.perform_validation_check();
            return my_src.is_valid_address;
        }
        public bool verify_destination()
        {
            my_dest.perform_validation_check();
            return my_dest.is_valid_address;
        }
        public void modify_source(string new_source_address)
        {
            my_src.address = new_source_address;
        }
        public void modify_destination(string new_destination_address)
        {
            my_dest.address = new_destination_address;
        }
        public void perform_copy_operation(bool key)
        {
            if(my_src.is_valid_address && my_dest.is_valid_address)
            {
                if (my_src.address != my_dest.address)
                    CopyDirectory(my_src.address, my_dest.address, key);
                else
                    Console.WriteLine("Self copy is not allowed");
            }
            else
            {
                Console.WriteLine("Copy operation failed, source or destination are invalid...");
            }
        }
        public void perform_organized_copy_operation(bool key)
        {
            if (my_src.is_valid_address && my_dest.is_valid_address )
            {
                if (my_src.address != my_dest.address)
                    OrganizedCopyDirectory(my_src.address, my_dest.address, key);
                else
                    Console.WriteLine("Self copy is not allowed");
            }
            else
            {
                Console.WriteLine("Copy operation failed, source or destination are invalid...");
            }
        }
        protected virtual void CopyDirectory(string mySourceFolder, string myDestinationFolder, bool key)
        {
            if (!Directory.Exists(mySourceFolder))  // unlikely to happen, just in case
            {
                Console.WriteLine("Source directory does not exist.");
                return;
            }

            string[] allowed_extensions = { "txt", "jpg", "png" };      // some reserved extension for example purposes

            // Create destination directory if it doesn't exist
            try
            {
                // Check if the directory exists before attempting creation
                if (!Directory.Exists(myDestinationFolder))
                {
                    Directory.CreateDirectory(myDestinationFolder);
                    Console.WriteLine($"Created destination directory: {myDestinationFolder}");
                }
                else
                {
                    Console.WriteLine($"Destination directory already exists: {myDestinationFolder}");
                }
            }
            catch (Exception e)     // In case the user gives a junk input
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            // copy all files (that have the right extension)
            foreach (string file in Directory.GetFiles(mySourceFolder))
            {
                string destFile = Path.Combine(myDestinationFolder, Path.GetFileName(file));
                string extension = Path.GetExtension(file).ToLower().TrimStart('.');     // Get extension without leading dot
                if (allowed_extensions.Contains(extension))     // 'contains' basically just checks if our string array has anything like the that matches the 'extension' string
                {
                    if (key == true)   // This gives us the control over dry mode 
                    {
                        File.Copy(file, destFile, true); // Corrected parameters, copy from file to destination file
                        Console.WriteLine($"Copied {file} -> {destFile}");
                    }
                    else
                    {
                        Console.WriteLine($"Dry run: {file} -> {destFile} (Not copied)");
                    }
                }
            }
            // copy all subdirectories recursively 
            foreach (string subdir in Directory.GetDirectories(mySourceFolder))
            {
                string destSubDir = Path.Combine(myDestinationFolder, Path.GetFileName(subdir));
                CopyDirectory(subdir, destSubDir, key);
            }
        }
        protected virtual void OrganizedCopyDirectory(string mySourceFolder, string myDestinationFolder, bool key)
        {
            string[] myDestinationFolders = { myDestinationFolder + "\\text_files", myDestinationFolder +"\\PNG_files",myDestinationFolder+"\\JPG_files"};        // we can add more later...
            string[] allowed_extensions = { "txt", "png", "jpg" };      // some reserved extension for example purposes
            if (!Directory.Exists(mySourceFolder))  // unlikely to happen, just in case
            {
                Console.WriteLine("Source directory does not exist.");
                return;
            }


            try
            {
                // Check if the directory exists before attempting creation
                if (!Directory.Exists(myDestinationFolder))
                {
                    Directory.CreateDirectory(myDestinationFolder);
                    Console.WriteLine($"Created destination directory: {myDestinationFolder}");
                }
                else
                {
                    // no need to write this each and every time, gets annoying
                    // Console.WriteLine($"Destination directory already exists: {main_destination}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");

            }
            foreach (string current_destination_folder in myDestinationFolders)     // generate all sub directories
            {
                // Create destination directory if it doesn't exist
                try
                {
                    // Check if the directory exists before attempting creation
                    if (!Directory.Exists(current_destination_folder))
                    {
                        Directory.CreateDirectory(current_destination_folder);
                        Console.WriteLine($"Created destination directory: {current_destination_folder}");
                    }
                    else
                    {
                        // no need to write this each and every time, gets annoying
                        // Console.WriteLine($"Destination directory already exists: {current_destination_folder}");
                    }
                }
                catch (Exception e)     // In case the user gives a junk input
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            // copy all files (that have the right extension)
            int counter = 0;        // just to control the current allowed extension, 
            foreach (string current_destination_folder in myDestinationFolders)
            {
                string current_allowed_extension = allowed_extensions[counter];
                foreach (string file in Directory.GetFiles(mySourceFolder))
                {
                    string destFile = Path.Combine(current_destination_folder, Path.GetFileName(file));
                    string extension = Path.GetExtension(file).ToLower().TrimStart('.');     // Get extension without leading dot
                    if (extension == current_allowed_extension)
                    {
                        if (key == true)   // This gives us the control over dry mode 
                        {

                            File.Copy(file, destFile, true); // Corrected parameters, copy from file to destination file
                            Console.WriteLine($"Copied {file} -> {destFile}");
                        }
                        else
                        {
                            Console.WriteLine($"Dry run: {file} -> {destFile} (Not copied)");
                        }
                    }
                }
                counter++;
            }
            // copy all files accordingly recursively 
            foreach (string subdir in Directory.GetDirectories(mySourceFolder))
            {
                OrganizedCopyDirectory(subdir,myDestinationFolder, key);
            }
        }
    }
}
