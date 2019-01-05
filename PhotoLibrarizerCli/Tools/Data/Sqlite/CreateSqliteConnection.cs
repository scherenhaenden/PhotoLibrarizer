using System;
using System.IO;
using Mono.Data.Sqlite;
using SQLite;

namespace PhotoLibrarizerCli.Tools.Data.Sqlite
{
    public class CreateSqliteConnection
    {
        public CreateSqliteConnection()
        {
        }

        public SQLiteConnection ByFileName(string dataSource= "@Data Source={0}\\SimpleDatabase.s3db") 
        {



            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            var db = new SQLiteConnection(dataSource);
            //var db2 = connection;
            db.CreateTable<Stock>();
            db.CreateTable<Valuation>();

            var s = db.Insert(new Stock()
            {
                Symbol = "sdas"
            });

            var connection = new SqliteConnection(string.Format(
               dataSource,
               Environment.CurrentDirectory));

            connection = new SqliteConnection(dataSource);

            return db;


        }
    }

    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Symbol { get; set; }
    }

    public class Valuation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int StockId { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
    }


}
