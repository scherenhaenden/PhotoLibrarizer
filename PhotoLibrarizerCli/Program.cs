using System;
using System.Collections.Generic;
using System.Configuration;
using PhotoLibrarizerCli.Tools.Temp;
using PhotoLibrazierCore.Tools.CliConfiguration;
using PhotoLibrazierCore.Tools.FileSystem.Seeker;
using PhotoLibrazierCore.Tools.Metadata.MetaFactory;
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


            var config = new ConfigurationLoader(new JsonSerialization()).RunAndGetModel();


            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/", new List<string>() { ".jpg" });
            TestDateFiles(files);
            var result=GenerateFilesTest(files);
            //new TestClass().SecondTest(result);
//Todo: CreateLibraryOfPictures        


            //new CreateDB().Run();



           var connection= new CreateSqliteConnection().ByFileName();

           

            connection.Open();




            try
            {
               /* using (var db = new LibraryContext())
                {
                    var fileModel = new FileModel() { Name = "John" };
                    db.
                    db.SaveChanges();
                }*/


                MongoDBContext context = new MongoDBContext();
                context.Configuration.UseDatabaseNullSemantics = true;
                var query = from line in context.restaurants select line;
            }
            catch (NotImplementedException ex)
            {

            }
            catch (ConfigurationException ex)
            {

            }
           
            catch (Exception ex)
            {

            }


            /* using (var context = new LibraryContext("libraryDB"))
            {
                CreateAndSeedDatabase(context);
                //DisplaySeededData(context);
            }*/
        


        }

        public static void TestDateFiles(List<string> Files) 
        {
            foreach(var file in Files) 
            {
                IAllMetadataData iExifData = new DataByMetaExtractor(file);
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

  

      /*  static void CreateAndSeedDatabase(DbContext context)
        {
            //System.Console.WriteLine("Create and seed the database.");

            try
            {
                if (context.Set<FileModel>().Count() != 0)
                {
                    return;
                }
            }
            catch(Exception ex)
            {

            }

           

            var filemodel = new FileModel();
            filemodel.InsertDate = DateTime.Now;
            filemodel.Path = "";
            filemodel.Name = "somename";

            context.Set<FileModel>().Add(filemodel);

            context.SaveChanges();

            System.Console.WriteLine("Completed.");
            System.Console.WriteLine();
        }*/

    }
}
