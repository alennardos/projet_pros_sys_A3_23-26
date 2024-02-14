using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src.SaveType
{
    internal class SaveDif : TypeSave
    {
        Process cryptosoft;
        public SaveDif()
        {
            this.cryptosoft = new Process();
            string pathcrypto = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Cryptosoft\Cryptosoft.exe");
            cryptosoft.StartInfo.FileName = pathcrypto;
        }

        // The "save" method used to save every single file which has been modified since the last save.
        public int save(FileInfo file, string targetFilePath, bool crypt)
        {
            try
            {

                FileInfo tempoFile = new FileInfo(targetFilePath);
                if (tempoFile.Exists)
                {
                    Console.WriteLine("file exist");
                    //if the file exist in the target
                    if (file.LastWriteTime != tempoFile.LastWriteTime)
                    {
                        Console.WriteLine("writetime");
                        file.LastWriteTime = tempoFile.LastWriteTime;
                        tempoFile.Delete();
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (crypt)
                {
                    Console.WriteLine("crypt = true");
                    cryptosoft.StartInfo.Arguments = file.FullName + " " + targetFilePath;
                    cryptosoft.Start();
                    cryptosoft.WaitForExit();
                    int exit = cryptosoft.ExitCode;

                    return exit;
                }
                else
                {
                    Console.WriteLine("crypt = false");
                    file.CopyTo(targetFilePath);
                    return 0;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public String toString()
        {
            return "diff";
        }
    }
}
