namespace ConsoleApp1.src.SaveType
{

    public interface TypeSave
    {

        void save(FileInfo file, string targetFilePath);

        String toString();
    }
}