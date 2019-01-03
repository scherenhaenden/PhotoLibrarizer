using System;
using System.Globalization;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.FileSystem;
namespace PhotoLibrazierCore.Tools.Metadata.MetaFactory
{
    public class ExifDataByMetaExtractor:IExifData
    {
        public ExifDataByMetaExtractor(string pathToMediaFile)
        {
            this.pathToMediaFile = pathToMediaFile;
        }
        FileMetadataReader fileMetadataReader = new FileMetadataReader();
        string pathToMediaFile;

        public dynamic GetData()
        {
            var metaData=ImageMetadataReader.ReadMetadata(pathToMediaFile);
            var data = metaData.Where(x => x.Name.Contains("Exif IFD0"))
            .FirstOrDefault().Tags
            .ToList();

            var mini=metaData.Where(x => x.Name.Contains("Exif IFD0")).FirstOrDefault();
            var vj=mini.Tags.Where(x => x.Type == 36867).FirstOrDefault();


            string format = "yyyy:MM:dd HH:mm:ss";


            DateTime dtObject = DateTime.ParseExact(vj.Description, format, CultureInfo.InvariantCulture);
            return dtObject;



        }
    }
}
