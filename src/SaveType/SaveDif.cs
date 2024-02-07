using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.src.SaveType
{
    internal class SaveDif : TypeSave
    {

        public SaveDif() { }

        public void save(FileInfo file, string targetFilePath)
        {
            try
            {
                file.CopyTo(targetFilePath);
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    FileInfo tempoFile = new FileInfo(targetFilePath);
                    if (file.LastWriteTime != tempoFile.LastWriteTime)
                    {
                        file.LastWriteTime = tempoFile.LastWriteTime;
                        tempoFile.Delete();
                        file.CopyTo(targetFilePath);
                    }
                }
            }
        }
    }
}
