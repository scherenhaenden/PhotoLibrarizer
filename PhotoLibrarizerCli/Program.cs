using System;
using System.Collections.Generic;
using System.IO;
using PhotoLibrazierCore.Tools.FileSystem.Seeker;
using PhotoLibrerizerData.Connection;
using PhotoLibrerizerData.Models.Sqlite;

namespace PhotoLibrarizerCli
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Todo: CreateConfiguration
            //Todo: CreateLibraryOfPictures  




            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/");
            var result=GenerateFilesTest(files);

            new TestClass().SecondTest(result);



        }

        public static List<Files> GenerateFilesTest(List<string> Files)
        {
            List<Files> files = new List<Files>();
            DateTime dt = DateTime.Now;
            foreach(var fil in Files) 
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
