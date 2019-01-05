using System;
using System.Collections.Generic;
using System.IO;
using PhotoLibrerizerData.Models.Sqlite;

namespace PhotoLibrarizerCli.CliDrafts
{
    public class Draft1
    {
        public Draft1()
        {
        }

        public static List<Files> GenerateFilesTest(List<string> Files)
        {
            List<Files> files = new List<Files>();
            DateTime dt = DateTime.Now;
            foreach (var fil in Files)
            {
                var file = new Files();


                file.FilName = Path.GetFileName(fil);
                //_Path = Path.GetDirectoryName(fil);
                FileInfo FInfo = new FileInfo(fil);

                file.Inserted = dt;
                file.Updated = dt;



                files.Add(file);
            }

            return files;



        }
    }
}
