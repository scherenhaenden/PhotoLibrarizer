using System.Security.Cryptography;

namespace PhotoLibrarizerCore.Services.Hash
{
    public class HashFileGenerator: IHashFileGenerator
    {
        public string GetHashByFilePath(string path)
        {
            var sFile = new BufferedStream(File.OpenRead(path), 1200000);
            MD5 hashValue = MD5.Create();
            var first = BitConverter.ToString(hashValue.ComputeHash(sFile)).Replace("-", string.Empty);
            sFile.Flush();
            sFile.Close();
            return first;
        }
    }
}
