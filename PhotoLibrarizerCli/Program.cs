using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using PhotoLibrarizerCli.Tools.Data.Context;
using PhotoLibrarizerCli.Tools.Data.Models;
using PhotoLibrarizerCli.Tools.Temp;

namespace PhotoLibrarizerCli
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Todo: CreateConfiguration
            //Todo: CreateLibraryOfPictures        


            //new CreateDB().Run();
            try
            {
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
