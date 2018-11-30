using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using FluentNHibernate.Mapping;
using Mono.Data.Sqlite;
using PhotoLibrarizerCli.Tools.Data.Models;

namespace PhotoLibrarizerCli.Tools.Temp
{
    public class CreateDB
    {
        public CreateDB()
        {
        }

        public void Run()
        {



            if (!File.Exists("MyDatabase.sqlite"))
            {
                SqliteConnection.CreateFile("MyDatabase.sqlite");
            }

            All();
        }


        public class FileMap : ClassMap<FileModel>
        {
            public FileMap()
            {
                Id(x => x.Id);
                Map(x => x.Name);
                Map(x => x.InsertDate);
                Map(x => x.Path);
                Table("tblCustomer");
            }
        }


        public void All()
        {

            var m_dbConnection = new Mono.Data.Sqlite.SqliteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();



            using (var db = new MyDBContext(m_dbConnection, true))
            {
                db.Notes.Add(new Note { Text = "Hello, world" });
                db.Notes.Add(new Note { Text = "A second note" });
                db.Notes.Add(new Note { Text = "F Sharp" });
                db.SaveChanges();
            }

        }
        public class Note
        {
            public long NoteId { get; set; }
            public string Text { get; set; }
        }

        public class MyDBContext : DbContext
        {
            // default constructor should do this automatically but fails in this case
            public MyDBContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
            {



            }
            public DbSet<Note> Notes { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
    }

    public class MongoDBContext : DbContext
    {
        public MongoDBContext() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // To remove the requests to the Migration History table
            Database.SetInitializer<MongoDBContext>(null);
            // To remove the plural names   
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<restaurants> restaurants { set; get; }
    }
    public class restaurants
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.String borough { get; set; }
        public System.String cuisine { get; set; }
    }
}
