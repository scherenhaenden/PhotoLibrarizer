using System;
using System.Collections.Generic;
using PhotoLibrazierCore.Tools.FileSystem.Seeker;
using PhotoLibrazierCore.Tools.Metadata.MetaFactory;
using PhotoLibrerizerData.Models.Sqlite;

namespace PhotoLibrarizerCli.Tools
{
    public class SeekFilesAndGetData
    {
        public SeekFilesAndGetData()
        {
        }

        public dynamic RunAndGet() 
        {
            IFilesSeeker iFilesSeeker = new FileSeeker();

            var files = iFilesSeeker.GetFilesInPath("/home/edward/Bilder/TempTest/", new List<string>() { ".jpg" });
            return TestDateFiles(files);
            //var result = GenerateFilesTest(files);
        }

        public dynamic TestDateFiles(List<string> Files)
        {
            List<dynamic> gh = new List<dynamic>();
            foreach (var file in Files)
            {
                IAllMetadataData iExifData = new DataByMetaExtractor(file);
                gh.Add(iExifData.GetData());
            }
            return gh;
        }

        public static List<Files> GenerateFilesTest(List<string> Files)
        {
            List<Files> files = new List<Files>();
            DateTime dt = DateTime.Now;
            foreach (var fil in Files)
            {
                var file = new Files();
                file.FullPath = fil;
                files.Add(file);
            }
            return files;


        }
    }
}
