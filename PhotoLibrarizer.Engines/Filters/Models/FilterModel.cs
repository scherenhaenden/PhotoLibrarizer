namespace PhotoLibrarizer.Engines.Filters.Models;

public class FilterModel
{
    public List<string>? Extensions { get; set; }
    
    public List<string>? PathsForSourceFiles { get; set; }
    public List<string>? CamerasShouldBe { get; set; }
    public List<string>? CamerasShouldNotBe { get; set; }
    public int MaxFiles { get; set; }

    public DestinationModel DestinationModel { get; set; } = null!;
}