namespace ConsoleApp1.src.SaveType
{

    public interface TypeSave
    {
        // The "save" method (Complete or differential)
        void save(FileInfo file, string targetFilePath);

        String toString();
    }
}