namespace ConsoleApp1.src.SaveType
{

    public interface TypeSave
    {
        // Méthode save (Contenant Différentielle ou Complète)
        void save(FileInfo file, string targetFilePath);

    }
}