using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.FileSystem;
using Newtonsoft.Json;
using System.Reflection;
namespace PhotoLibrazierCore.Tools.Metadata.MetaFactory
{
    public class DataByMetaExtractor: IAllMetadataData
    {
        public DataByMetaExtractor(string pathToMediaFile)
        {
            this.pathToMediaFile = pathToMediaFile;
        }
        FileMetadataReader fileMetadataReader = new FileMetadataReader();
        string pathToMediaFile;

        


        private Directory GetExifDirectory(IReadOnlyList<Directory> directories) 
        {
            Directory data = directories
            .Where(x => x.Name.Contains("Exif IFD0"))
                .FirstOrDefault();
            return data;

           
        }
        private Tag GetValueFromTypeInDirectory(Directory directory, int typeId)
        {
            return directory.Tags.Where(x => x.Type == typeId).FirstOrDefault();
        }

        private dynamic GetDate(Directory directory)
        {
            var item =GetValueFromTypeInDirectory(directory, 36867);
            if (item != null) 
            {
                string format = "yyyy:MM:dd HH:mm:ss";


                return DateTime.ParseExact(item.Description, format, CultureInfo.InvariantCulture);

            }
            return null;

        }

        public dynamic GetData()
        {
            IReadOnlyList<Directory> metaData = ImageMetadataReader.ReadMetadata(pathToMediaFile);

            var directory =GetExifDirectory(metaData);
            if(directory!=null)
            {
                var result=GetDate(directory);
                return result;
            }
            return null;



            /*var jsons = JsonConvert.SerializeObject(metaData);
            float oi = jsons.Length;

            var data = metaData.Where(x => x.Name.Contains("Exif IFD0"))
            .FirstOrDefault().Tags
            .ToList();

            List<dynamic> dn = new List<dynamic>();


            int i = 0;
            dynamic[] d = new dynamic[100];
            dynamic ds;

            foreach (var minidata in data)
            {


                var json = JsonConvert.SerializeObject(minidata);



            }

            //var json = JsonConvert.SerializeObject(d);

            var mini = metaData.Where(x => x.Name.Contains("Exif IFD0")).FirstOrDefault();
            var vj = mini.Tags.Where(x => x.Type == 36867).FirstOrDefault();


            string format = "yyyy:MM:dd HH:mm:ss";


            DateTime dtObject = DateTime.ParseExact(vj.Description, format, CultureInfo.InvariantCulture);
            return dtObject;*/



        }
    }
}
