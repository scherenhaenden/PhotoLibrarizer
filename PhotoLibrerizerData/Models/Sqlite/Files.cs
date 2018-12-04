using System;
using PhotoLibrerizerData.Models.Generic;
using SQLite;

namespace PhotoLibrerizerData.Models.Sqlite
{
    public class Files : FilesModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public override string FullPath;

    }
}
