using System;
using System.Data.Entity;
using MySql.Data.Entity;

using System.Data.Common;
using PhotoLibrarizerCli.Tools.Models;

namespace PhotoLibrarizerCli.Tools.Data
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContextForUpdatingTables : DbContext
    {
        public DataContextForUpdatingTables() : this("photoConnection")
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContextForUpdatingTables, Configuration>());
        }
        public DataContextForUpdatingTables(DbConnection existingConnection, bool contextOwnsConnection) :
       base(existingConnection, contextOwnsConnection)
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContextForUpdatingTables, Configuration>());

        }


        public DataContextForUpdatingTables(string connStringName) : base(connStringName)
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContextForUpdatingTables, Configuration>());

        }
        static DataContextForUpdatingTables()
        {
            // static constructors are guaranteed to only fire once per application.
            // I do this here instead of App_Start so I can avoid including EF
            // in my MVC project (I use UnitOfWork/Repository pattern instead)
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
        }
        public DbSet<Files> Files { get; set; }
        //public DbSet<Galleries> Galleries  { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.AddFromAssembly (typeof (EntityMap<>).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
