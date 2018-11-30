using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using SQLite.CodeFirst;


/*namespace PhotoLibrarizerCli.Tools.Data.Models
{
    public class FileModel: IEntity
    {
        [Autoincrement]
        public int Id { get; set; }

        [Index("IX_File_filename")] // Test for named index.
        [Required]
        public string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual DateTime InsertDate { get; set; }


    }
}*/
namespace PhotoLibrarizerCli.Tools.Data.Models
{
    public class FileModel : IEntity
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public virtual string Path { get; set; }

        public virtual DateTime InsertDate { get; set; }


    }
}