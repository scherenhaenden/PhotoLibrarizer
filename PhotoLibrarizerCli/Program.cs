using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using PhotoLibrarizerCli.Tools.Data;
using PhotoLibrarizerCli.Tools.Models;

namespace PhotoLibrarizerCli
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Todo: CreateConfiguration
            //Todo: CreateLibraryOfPictures         

            var myConnection = "Server=localhost;Port=3306;Database=cmsbackup5;Uid=cms;Pwd=albanicus$5$;charset=utf8;SslMode=none;";
            var myconnection = "server=localhost;database=PhotoLibrerizer;User I=photo;pwd=password;Pooling=false;SslMode=none;";
            var connectionString2 = ConfigurationManager.AppSettings["myConnectionString"].ToString();

            new Test().Run();

            string connectionString =
        "Server=localhost;" +
        "Database=test;" +
        "User ID=myuserid;" +
        "Password=mypassword;" +
        "Pooling=false";
            try
            {
                var dbcon = new MySqlConnection(connectionString2);
                var hk = dbcon.State;

                dbcon.Open();
                dbcon.Close();

            }
            catch(TypeInitializationException ex)
            {

            }






        }
    }
}
