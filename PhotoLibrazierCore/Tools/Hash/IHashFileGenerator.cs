using System;
namespace PhotoLibrazierCore.Tools.Hash
{
    public interface IHashFileGenerator
    {
        string GetHashByFilePath(string path);
    }
}
