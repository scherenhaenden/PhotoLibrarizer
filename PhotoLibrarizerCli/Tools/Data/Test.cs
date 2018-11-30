using System;
using MySql.Data.MySqlClient;
using PhotoLibrarizerCli.Tools.Models;

namespace PhotoLibrarizerCli.Tools.Data
{
    public class Test
    {
        public Test()
        {
        }
        public void Run()
        {
            MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
            b.Server = "localhost";
            b.Database = "PhotoLibrerizer";
            b.UserID = "photo";
            b.Password = "password";
            b.SslMode = MySqlSslMode.None;

            try
            {

                using (MySqlConnection conn2 = new MySqlConnection
                           (b.GetConnectionString(true)))
                {
                    conn2.Open();
                    conn2.Close();
                }

                MySqlConnection conn = new MySqlConnection(b.ConnectionString);
                conn.Open();

                using (var contextd = new DataContextForUpdatingTables(conn, true))
                {
                    var hal = new Files();
                    hal.Id = 0;
                    hal.FileName = "dfsdfs";
                    hal.Inserted = DateTime.Now;
                    hal.Path = "./";
                    contextd.Files.Add(hal);
                    contextd.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                Run();
            }

        }
    }
}
