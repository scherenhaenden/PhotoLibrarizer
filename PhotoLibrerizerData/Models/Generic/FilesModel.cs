using System;
using System.IO;

namespace PhotoLibrerizerData.Models.Generic
{
    public abstract class FilesModel
    {       
        public string FullPath { get; set; }
        public string FilName { get; set; }
        public string Hash { get; set; }
        public DateTime Inserted { get; set; }
        public DateTime Updated { get; set; }


    }
}
