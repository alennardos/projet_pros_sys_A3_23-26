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
