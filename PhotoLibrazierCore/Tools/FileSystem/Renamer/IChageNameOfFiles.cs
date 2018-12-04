using System;
namespace PhotoLibrazierCore.Tools.FileSystem.Renamer
{
    public interface IChageNameOfFiles
    {
        string SetNewNameWithEnding(string oldName, string newName);
    }
}
