﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizerSoftware
{
    class FileOperationsHandling
    {
         VerificationHandlingDestinationFolder _destinationFolder;
         VerificationHandlingSourceFolder _sourceFolder;
        public FileOperationsHandling(string sourceString, string destinationString)
        {
            _destinationFolder = new VerificationHandlingDestinationFolder(destinationString);
            _sourceFolder = new VerificationHandlingSourceFolder(sourceString);
        }
        public bool VerifySource()
        {
            _sourceFolder.PerformValidationCheck();
            return _sourceFolder.AddressStatus;
        }
        public bool VerifyDestination()
        {
            _destinationFolder.PerformValidationCheck();
            return _destinationFolder.AddressStatus;
        }
        public string GetSourceAddress()
        {
            return _sourceFolder.Address;
        }
        public string GetDestinationAddress()
        {
            return _destinationFolder.Address;
        }
        public void ModifySource(string newSourceAddress)
        {
            _sourceFolder.Address = newSourceAddress;
        }
        public void ModifyDestination(string newDestinationAddress)
        {
            _destinationFolder.Address = newDestinationAddress;
        }
        public void PerformCopyOperation(bool key)
        {
            if(_sourceFolder.AddressStatus && _destinationFolder.AddressStatus)
            {
                if (_sourceFolder.Address != _destinationFolder.Address)
                    CopyDirectory(_sourceFolder.Address, _destinationFolder.Address, key);
                else
                    Console.WriteLine("Self copy is not allowed");
            }
            else
            {
                Console.WriteLine("Copy operation failed, source or destination are invalid...");
            }
        }
        public void PerformOrganizedCopyOperation(bool key)
        {
            if (_sourceFolder.AddressStatus && _destinationFolder.AddressStatus )
            {
                if (_sourceFolder.Address != _destinationFolder.Address)
                    OrganizedCopyDirectory(_sourceFolder.Address, _destinationFolder.Address, key);
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
