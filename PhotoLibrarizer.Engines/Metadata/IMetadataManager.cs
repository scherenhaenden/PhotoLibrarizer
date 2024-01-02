using DirectoryOfMetas = MetadataExtractor.Directory;

namespace PhotoLibrarizer.Engines.Metadata
{
    public interface IMetadataManager
    {
        string? GetModelOfCamera();
        DateTime? GetDateOfMediaCreation();
        public List<MetadataExtractor.Directory>? Directories { get; }
    }
}