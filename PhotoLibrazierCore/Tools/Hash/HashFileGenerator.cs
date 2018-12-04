using System;
using System.IO;
using System.Security.Cryptography;

namespace PhotoLibrazierCore.Tools.Hash
{
    public class HashFileGenerator: IHashFileGenerator
    {
        public string GetHashByFilePath(string path)
        {
            var sFile = new BufferedStream(File.OpenRead(path), 1200000);


            //Stream sFile= File.OpenRead(MaybeOfficialName);
            MD5 hashvalue = MD5.Create();
            string First = BitConverter.ToString(hashvalue.ComputeHash(sFile)).Replace("-", string.Empty);
            sFile.Flush();
            sFile.Close();

            return First;
        }
    }
}
