using System.Data.Entity;
using System.Data.Common;

namespace PhotoLibrarizerCli.Tools.Data.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        public LibraryContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        DbSet<Models.FileModel> FileModels  { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           ModelConfiguration.Configure(modelBuilder);
            //var initializer = new LibraryDbInitializer(modelBuilder);
            //Database.SetInitializer(initializer);
        }

    }
}
