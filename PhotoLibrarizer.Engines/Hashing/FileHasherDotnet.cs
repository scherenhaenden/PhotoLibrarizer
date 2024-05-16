using System.Security.Cryptography;

namespace PhotoLibrarizer.Engines.Hashing
{
    public class FileHasherDotnet : IFileHasher
    {
        public string GetMD5ByFilePath(string path)
        {
            var sFile = new BufferedStream(File.OpenRead(path), 1200000);
            var hashvalue = MD5.Create();
            string first = BitConverter.ToString(hashvalue.ComputeHash(sFile)).Replace("-", string.Empty);
            sFile.Flush();
            sFile.Close();
            return first;
        }
    }
    

    
}