using System;
using System.IO;

namespace PhotoLibrazierCore.Tools.FileSystem.Renamer
{
    public class ChageNameOfFiles : IChageNameOfFiles
    {
        public string SetNewNameWithEnding(string oldnameFullPath, string newName)
        {
            string extension = Path.GetExtension(oldnameFullPath).ToLower();
            if(string.IsNullOrEmpty(extension))
            {
                return newName;
             
            }
            return newName + "." + extension;
        }
    }
}
