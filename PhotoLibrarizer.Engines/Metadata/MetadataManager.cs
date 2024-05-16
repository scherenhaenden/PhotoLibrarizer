using System.Globalization;
using MetadataExtractor;

namespace PhotoLibrarizer.Engines.Metadata
{
    public class MetadataManager: IMetadataManager
    {
        private readonly string _filePath;
        private List<MetadataExtractor.Directory>? _directories = null;

        public MetadataManager(string filePath)
        {
            _filePath = filePath;
        }
    
        public MetadataManager(string filePath, List<MetadataExtractor.Directory>? directories)
        {
            _filePath = filePath;
            _directories = directories;
        }

        private int ConvertHexValueToIn(string hexValue)
        {
            string hexString = hexValue.Substring(2);  // Removes the "0x"
            return Convert.ToInt32(hexString, 16);
        }

        public DateTime? GetDateOfMediaCreation()
        {
            try
            {
                if (_directories == null || _directories.Count == 0)
                {
                    _directories = ImageMetadataReader.ReadMetadata(_filePath).ToList();
                }

                List<string> hexForCreationDates = new List<string>();
                hexForCreationDates.Add("0x9003");
                hexForCreationDates.Add("0x0123");
                hexForCreationDates.Add("0x0123");

                foreach (var hexValue in hexForCreationDates)
                {
                    var tagType = ConvertHexValueToIn(hexValue);
                    var valueOfDescription = GetValueOfDescriptionByType(_directories, tagType);
                    if (!string.IsNullOrEmpty(valueOfDescription))
                    {
                        string format = "yyyy:MM:dd HH:mm:ss";
                        DateTime dtObject = DateTime.ParseExact(valueOfDescription,format,CultureInfo.InvariantCulture);
                        return dtObject;
                    }
           
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            
            }
      
        }

        private string? GetValueOfDescriptionByType(List<MetadataExtractor.Directory> directories, int tagType)
        {
            var description = directories
                .SelectMany(dir => dir.Tags.Where(tag => tag.Type == tagType))
                .FirstOrDefault()?.Description;

            return description;

        }

        public List<MetadataExtractor.Directory>? Directories => _directories;
        public string? GetModelOfCamera()
        {
            const int tagType = 272;

            if (_directories == null || _directories.Count == 0)
            {
                _directories = ImageMetadataReader.ReadMetadata(_filePath).ToList();
            }

            return GetValueOfDescriptionByType(_directories, tagType) ?? "unknown";


        }
    }
}