using System;
using PhotoLibrazierCore.Tools.CliConfiguration;

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
