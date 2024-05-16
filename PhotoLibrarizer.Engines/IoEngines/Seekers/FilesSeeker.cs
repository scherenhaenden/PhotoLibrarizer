namespace PhotoLibrarizer.Engines.IoEngines
{
    public class FilesSeeker: IFilesSeeker
    {

        public List<string> GetFilesInPath(string path, bool subDirectory = true)
    {
        return GetSeekedFiles(path, new ListOfDefaultExtensions().Extensions());
    }

        public List<string> GetFilesInPath(string path, List<string> extensions, bool subDirectory = true, bool caseSensitive = false)
    {
        return GetSeekedFiles(path, extensions.ToArray(), caseSensitive);
    }

        private List<string> GetSeekedFiles(string pathToLookFor, string[] allowedExtensions, bool subDirectory = true, bool caseSensitive = false)
    {
        StringComparison comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

        List<string> files = Directory
            .GetFiles(pathToLookFor, "*.*", subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            .Where(file => allowedExtensions.Any(extension => file.EndsWith(extension, comparison)))
            .ToList();

        return files;
    }

    }
}