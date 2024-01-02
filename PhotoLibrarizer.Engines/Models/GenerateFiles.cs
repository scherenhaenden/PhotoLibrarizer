namespace PhotoLibrarizer.Engines.Models
{
    public class GenerateFiles
    {
        public List<FileModel> StringsToModels(string [] Files)
    {
        List<FileModel> AllFiles = new List<FileModel> ();
        FileModel TempFile;
        foreach(var filestring in Files)
        {
            TempFile = new FileModel ();
            TempFile.FullPathOfFile = filestring;
            AllFiles.Add (TempFile);
        }
        return AllFiles;
    }

    }
}