namespace PhotoLibrarizer.Engines.IoEngines.Seekers
{
    [Obsolete("Use IFilesSeekerV2 instead")]
    public interface IFilesSeeker
    {
        List<string> GetFilesInPath(string path, bool subDirectory = true);

        List<string> GetFilesInPath(string path, List<string> extensions, bool subDirectory = true, bool caseSensitive = false);
    }
}