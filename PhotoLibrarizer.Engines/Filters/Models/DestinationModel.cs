namespace PhotoLibrarizer.Engines.Filters.Models;

public class DestinationModel
{
    //public bool MultiDestination { get; set; }
    
    public string BasePath { get; set; } = string.Empty;
    
    //public bool DestinationBasedOnProperty { get; set; }  
    public Destinations Destination { get; set; } = Destinations.BaseLibraryWithDate;
}