namespace ConsoleApp1.src.SaveType
{

    public interface TypeSave
    {
        // The "save" method (Complete or differential)
        int save(FileInfo file, string targetFilePath, bool crypt);

        String toString();
    }
}