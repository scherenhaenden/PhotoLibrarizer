using System;
using System.Collections.Generic;
using PhotoLibrerizerData.Models.Sqlite;
using SQLite;

namespace PhotoLibrerizerData.Connection
{
    public class TestClass
    {

        string dataSource = "@Data Source={0}\\SimpleDatabase.s3db";
        public void FirstTest()
        {



            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            var db = new SQLiteConnection(dataSource);
            //var db2 = connection;
            db.CreateTable<Files>();

            var files = new Files();
            files.Inserted = DateTime.Now;
            files.FilName = "somename.dbp";
            files.FullPath = "./";
            files.Hash = "";


            var s = db.Insert(files);
            




        }

        public void SecondTest( List<Files> files)
        {



            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            var db = new SQLiteConnection(dataSource);
            //var db2 = connection;
            db.CreateTable<Files>();




            var s = db.InsertAll(files,true);
            /*foreach(var fileModel in files)
            {
                var s = db.Insert(files);
            }*/






        }
    }
}
