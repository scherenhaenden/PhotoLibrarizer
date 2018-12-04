using System;
using System.Collections.Generic;

namespace PhotoLibrazierCore.Tools.FileSystem.Seeker
{
    public interface IFilesSeeker
    {
        List<string> GetFilesInPath(string path);

        List<string> GetFilesInPath(string path, List<string> extensions);
    }
}
