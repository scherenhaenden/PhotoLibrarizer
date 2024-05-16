
namespace PhotoLibrarizer.Web.Blazor.Models
{
    public class ConfigurationModel
    {
        public string FilterName { get; set; } = null!;
    }


    public class FilterWebModel
    {
        public List<string>? Extensions { get; set; } = new List<string>();
        public List<string> PathsForSourceFiles { get; set; } = new List<string>();
        public List<string>? CamerasShouldBe { get; set; } = new List<string>();
        public List<string>? CamerasShouldNotBe { get; set; } = new List<string>();
        public int MaxFiles { get; set; } = 40;

        public DestinationWebModel DestinationModel { get; set; } = new DestinationWebModel();
    }

    public class DestinationWebModel
    {
        public string BasePath { get; set; } = null!;
    
 
        public DestinationsWebEnum Destination { get; set; } = DestinationsWebEnum.BaseLibraryWithDate;
    }

    public enum DestinationsWebEnum
    {
        BaseLibraryWithoutDate,
        BaseLibraryWithDate,
        CameraBasedDirectoryWithoutDate,
        CameraBasedDirectoryWithDate,
    }
}