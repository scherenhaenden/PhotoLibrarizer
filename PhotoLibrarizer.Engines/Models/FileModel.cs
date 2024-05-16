using Directory = MetadataExtractor.Directory;

namespace PhotoLibrarizer.Engines.Models
{
    public class FileModel:GeneralFileModel
    {
        public string CorrectBashFullFileName { get; set; } = string.Empty;
        public string CorrectBashFullFileNameDestination { get; set; } = string.Empty;
    
        public Dictionary<string, DateTime> KeyDatesMetadata = new Dictionary<string, DateTime> ();
    
        public Dictionary<string, string> KeyMetadata = new Dictionary<string, string> ();

        public List<Directory>? Directories = new List<Directory>();
        
        public string SourcePath { get; set; } = string.Empty;
        public string DestinationPath { get; set; } = string.Empty;
        public bool CopiedToDestination { get; set; } = false;
        public bool CheckedIfCopied { get; set; } = false;
        public bool DeletedFromTheSource { get; set; } = false;
    
        public FileModel ()
        {
        }
        private void any()
        {
			    
        }
    }

}