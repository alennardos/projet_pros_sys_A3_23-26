namespace ConsoleApp1.src.SaveType
{
    internal class SaveComplete : TypeSave
    {

        public SaveComplete()
        {

        }

        public void save(FileInfo file, string targetFilePath)
        {
            try
            {
                if (File.Exists(targetFilePath))
                {
                    try
                    {
                        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempfile");
                        file.CopyTo(tempFilePath, true);
                        Console.WriteLine("copie dans fichier temp réussit");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("erreur de copie dans temp");
                    }

                    File.Delete(targetFilePath);
                    file.CopyTo(targetFilePath);
                }

                else
                    file.CopyTo(targetFilePath);
            }

            catch (Exception ex)
            {
                //erreur
            }
        }
    }
}
