using System;
using PhotoLibrerizerData.Models.Sqlite;
using SQLite;

namespace PhotoLibrerizerData.Connection
{
    public class TestClass
    {
        public void FirstTest(string dataSource = "@Data Source={0}\\SimpleDatabase.s3db")
        {



            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            var db = new SQLiteConnection(dataSource);
            //var db2 = connection;
            db.CreateTable<Files>();

            var files = new Files();
            files.FileCreatedDate = DateTime.Now;
            files.FilName = "somename.dbp";
            files.FullPath = "./";
            files.Hash = "";


            var s = db.Insert(files);
            




        }
    }
}
