using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.FileSystem;
using doob = photo.exif;

namespace PhotoLibrazierCore.Tools.Metadata
{
    public class TestClassMetadataReader
    {
        public TestClassMetadataReader()
        {
        }

        public void FirstDraft(string path)
        {

            photo_exif(path);

            var metadataReader = new FileMetadataReader();

            FileMetadataDirectory Readded = metadataReader.Read(path);
            IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(path);
            var tags = Readded.Tags;


            foreach (var ITEM in tags)
            {


            }
        }

        public DateTime? photo_exif(string PathToPicture)
        {
            doob.Parser Prs = new doob.Parser();
            IEnumerable<doob.ExifItem> MetaItemsPhotos = Prs.Parse(PathToPicture);
            List<doob.ExifItem> ListedMetaItemsPhotos = MetaItemsPhotos.ToList();
            foreach (var item in ListedMetaItemsPhotos)
            {
                if (item.Id == 306 || item.Id == 36867)
                {
                    try
                    {
                        DateTimeOffset dto = DateTimeOffset.Parse(item.Value.ToString());
                        DateTime dtObject = dto.DateTime;
                        return dtObject;

                    }
                    catch (Exception any)
                    {
                        string format = "yyyy:MM:dd HH:mm:ss";
                        //if(item.Value.ToString())

                        DateTime dtObjecvt = (DateTime)item.Value;

                        DateTime dtObject = DateTime.ParseExact(item.Description, format, CultureInfo.InvariantCulture);
                        return dtObject;

                    }

                }
            }
            return null;


        }
    }
}
