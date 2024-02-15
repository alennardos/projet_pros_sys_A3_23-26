using System.Diagnostics;
using System.IO;

namespace ConsoleApp1.src.SaveType
{
    internal class SaveComplete : TypeSave
    {

        Process cryptosoft;
        public SaveComplete()
        {
            this.cryptosoft = new Process();
            string pathcrypto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Cryptosoft\Cryptosoft.exe");
            cryptosoft.StartInfo.FileName = pathcrypto;
        }

        // The "save" method used to save every single file of a folder.
        public int save(FileInfo file, string targetFilePath, bool crypt)
        {
            Console.WriteLine("Saving " + file.FullName);
            try
            {
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }

                if (crypt == true)
                {
                    cryptosoft.StartInfo.Arguments = file.FullName + " " + targetFilePath;

                    cryptosoft.Start();
                    cryptosoft.WaitForExit();
                    int exit = cryptosoft.ExitCode;

                    return exit;
                }
                else
                {
                    file.CopyTo(targetFilePath);
                    return 0;
                }
                
            }

            catch (Exception ex)
            {
                return -1;
                // Error
            }
        }

        public String toString()
        {
            return "comp";
        }
    }
}
