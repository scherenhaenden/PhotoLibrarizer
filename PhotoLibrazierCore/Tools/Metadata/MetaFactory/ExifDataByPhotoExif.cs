using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using doob = photo.exif;
namespace PhotoLibrazierCore.Tools.Metadata.MetaFactory
{
    public class ExifDataByPhotoExif:IExifData
    {
        public ExifDataByPhotoExif(string pathToMediaFile)
        {
            this.pathToMediaFile = pathToMediaFile;
        }
        doob.Parser parserOfExifData = new doob.Parser();
        string pathToMediaFile;

        private DateTime GetDate(IEnumerable<doob.ExifItem> MetaItemsPhoto) 
        {
            var possibleDate = MetaItemsPhoto.Where(x => x.Id == 36867).FirstOrDefault();

            string format = "yyyy:MM:dd HH:mm:ss";
            string val = possibleDate.Value.ToString().Trim();
            var cleaned = val.Replace("\0", string.Empty);
            DateTime dtObject = DateTime.ParseExact(cleaned, format, CultureInfo.InvariantCulture);
            return dtObject;
        }

        private dynamic GetManufactor(IEnumerable<doob.ExifItem> MetaItemsPhoto) 
        {
            return MetaItemsPhoto.Where(x => x.Id == 271).FirstOrDefault();
        }

        public dynamic GetData()
        {

            IEnumerable<doob.ExifItem> MetaItemsPhotos = parserOfExifData.Parse(pathToMediaFile);

            var metaDataModel = new MetaDataModel();
            metaDataModel.FileCreatedAt = GetDate(MetaItemsPhotos);
            return metaDataModel;




        }
    }
}
