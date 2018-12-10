using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using PhotoLibrazierCore.Tools.FileSystem.Seeker;
using PhotoLibrerizerData.Connection;
using PhotoLibrerizerData.Models.Sqlite;
using PhotoLibrazierCore.Tools.CliConfiguration;
using System.Reflection;

namespace PhotoLibrarizerCli
{
    class MainClass
    {

       


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Todo: CreateConfiguration
            //Todo: CreateLibraryOfPictures  

           

            new LoaderDraft().FirstDraft(); 

            /*
            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/");
            var result=GenerateFilesTest(files);

            new TestClass().SecondTest(result);*/





        }

       
    }
}
