using System;
using System.Collections.Generic;

namespace PhotoLibrarizerCli.Tools.FileSystem
{
    public interface IFilesSeeker
    {
        List<string> GetFilesInPath(string path);

        List<string> GetFilesInPath(string path, List<string> extensions);
    }
}
