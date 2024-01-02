namespace PhotoLibrarizer.Engines.Filters.Models
{
    public class DestinationModel
    {
        //public bool MultiDestination { get; set; }
    
        public string BasePath { get; set; } = string.Empty;
    
        //public bool DestinationBasedOnProperty { get; set; }  
        public Destinations Destination { get; set; } = Destinations.BaseLibraryWithDate;
        
        public  IDestinationPathModel DestinationPathDirectory { get; set; }
        
        public  IDestinationNamingModel DestinationPathFile { get; set; }
    }
    
    public interface IDestinationPathModel
    {
        public string PathDestination { get; }
    } 
    
    public interface IDestinationNamingModel
    {
        
    } 

    
    public class DestinationPathModelCustomPattern : IDestinationPathModel
    {
        public DestinationPathModelCustomPattern(string combinablePathDestination)
        {
            PathDestination = combinablePathDestination;
        }
        
        
        public string PathDestination { get; private set; }
    }
    
    
   
}