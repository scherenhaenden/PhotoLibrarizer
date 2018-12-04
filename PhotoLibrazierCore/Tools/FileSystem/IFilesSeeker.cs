using System;
using System.Collections.Generic;

namespace PhotoLibrazierCore.Tools.FileSystem
{
    public interface IFilesSeeker
    {
        List<string> GetFilesInPath(string path);

        List<string> GetFilesInPath(string path, List<string> extensions);
    }
}
