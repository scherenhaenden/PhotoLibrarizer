using System;
using PhotoLibrazierCore.Tools.CliConfiguration;
using PhotoLibrazierCore.Tools.Serialization;

namespace PhotoLibrarizerCli
{
    class MainClass
    {

       


        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Todo: CreateConfiguration
            //Todo: CreateLibraryOfPictures  


            var config=new ConfigurationLoader(new JsonSerialization()).RunAndGetModel(); 

            /*
            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/");
            var result=GenerateFilesTest(files);

            new TestClass().SecondTest(result);*/



        }

        public static List<Files> GenerateFilesTest(List<string> Files)
        {
            List<Files> files = new List<Files>();
            DateTime dt = DateTime.Now;
            foreach(var fil in Files) 
            {
                var file = new Files();

        }

       
    }
}
