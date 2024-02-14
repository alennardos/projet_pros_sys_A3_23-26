using System.Diagnostics;

namespace ConsoleApp1.src.SaveType
{
    internal class SaveComplete : TypeSave
    {
        public SaveComplete()
        {

        }

        // The "save" method used to save every single file of a folder.
        public void save(FileInfo file, string targetFilePath)
        {
            try
            {
                if (File.Exists(targetFilePath))
                {
                    try
                    {
                        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempfile");

                        Process cryptosoft = new Process();
                        cryptosoft.StartInfo.FileName = "C:\\Users\\mahra\\Desktop\\COURS CESI\\2 - Prog System\\projet_pros_sys_A3_23-26\\cryptoSoft\\cryptoSoft.exe";
                        cryptosoft.StartInfo.Arguments = $"\"{targetFilePath}\" \"{tempFilePath}\"";

                        cryptosoft.Start();
                        cryptosoft.WaitForExit();
                        int exit = cryptosoft.ExitCode;

                        Console.WriteLine("exit code = " + exit);

                        file.CopyTo(tempFilePath, true);
                    }
                    catch (Exception ex)
                    {
                    }

                    File.Delete(targetFilePath);
                    file.CopyTo(targetFilePath);
                }

                else
                    file.CopyTo(targetFilePath);
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
