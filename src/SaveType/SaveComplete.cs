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
