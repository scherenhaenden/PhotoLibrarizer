using System;
using System.Data.Entity.Migrations;

namespace PhotoLibrarizerCli.Tools.Data
{
    public class Configuration : DbMigrationsConfiguration<DataContextForUpdatingTables>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }
        protected override void Seed(DataContextForUpdatingTables dataContext)
        {


        }
    }
}
