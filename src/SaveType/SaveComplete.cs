using System.Diagnostics;

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
        public void save(FileInfo file, string targetFilePath)
        {
            Console.WriteLine("Saving " + file.FullName);
            try
            {
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                cryptosoft.StartInfo.Arguments = file.FullName + " " + targetFilePath;

                cryptosoft.Start();
                cryptosoft.WaitForExit();
                int exit = cryptosoft.ExitCode;

                Console.WriteLine("exit code = " + exit);
            }

            catch (Exception ex)
            {
                // Error
            }
        }

        public String toString()
        {
            return "comp";
        }
    }
}
