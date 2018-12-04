using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoLibrazierCore.Tools.FileSystem
{
    public class FileSeeker: IFilesSeeker
    {

        public List<string> GetFilesInPath(string path)
        {
            return GetSeekedFiles(path, new ListOfDefaultExtensions().Extensions());
        }

        public List<string> GetFilesInPath(string path, List<string> extensions)
        {
            return GetSeekedFiles(path, extensions.ToArray());
        }

        private List<string> GetSeekedFiles(string pathtolookfor, string[] allowedExtensions)
        {

            List<string> files = Directory
                  .GetFiles(pathtolookfor, "*.*", SearchOption.AllDirectories)
                  .Where(file => allowedExtensions.Any(file.EndsWith))?
                  .ToList();
            return files;

        }
    }
}
