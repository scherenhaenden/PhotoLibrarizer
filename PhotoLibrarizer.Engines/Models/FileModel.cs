using Directory = MetadataExtractor.Directory;

namespace PhotoLibrarizer.Engines.Models
{
    public class FileModel:GeneralFileModel
    {
    
        public Dictionary<string, DateTime> KeyDatesMetadata = new Dictionary<string, DateTime> ();
    
        public Dictionary<string, string> KeyMetadata = new Dictionary<string, string> ();

        public List<Directory>? Directories = new List<Directory>();
    
        public FileModel ()
        {
        }
        private void any()
        {
			    
        }
    }

    public class FileModelMoving:FileModel
    {
    
    
    }
}