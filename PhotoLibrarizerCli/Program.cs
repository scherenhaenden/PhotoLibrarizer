using System;
using System.Collections.Generic;
using PhotoLibrazierCore.Tools.CliConfiguration;
using PhotoLibrazierCore.Tools.FileSystem.Seeker;
using PhotoLibrazierCore.Tools.Metadata.MetaFactory;
using PhotoLibrazierCore.Tools.Serialization;
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


            var config = new ConfigurationLoader(new JsonSerialization()).RunAndGetModel();


            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/", new List<string>() { ".jpg" });
            TestDateFiles(files);
            var result=GenerateFilesTest(files);
            //new TestClass().SecondTest(result);
        }

        public static void TestDateFiles(List<string> Files) 
        {
            foreach(var file in Files) 
            {
                IExifData iExifData = new ExifDataByMetaExtractor(file);
                var h=iExifData.GetData();
            }
        }

        public static List<Files> GenerateFilesTest(List<string> Files)
        {
            List<Files> files = new List<Files>();
            DateTime dt = DateTime.Now;
            foreach (var fil in Files)
            {
                var file = new Files();
                file.FullPath = fil;
                files.Add(file);
            }
            return files;


        }
    }
}
