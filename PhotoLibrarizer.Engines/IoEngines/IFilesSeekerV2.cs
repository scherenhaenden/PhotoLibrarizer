namespace PhotoLibrarizer.Engines.IoEngines;

public interface IFilesSeekerV2
{
    List<string> GetFilesInPath(string path, bool subDirectory = true);

    List<string> GetFilesInPath(string path, List<string> extensions, bool subDirectory = true, bool caseSensitive = false);
}