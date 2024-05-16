using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;

namespace PhotoLibrarizer.BusinessLogic.Models;

public class DestinationBusinessLogicModel
{
    public string BasePath { get; set; } = null!;
    
 
    public DestinationsBusinessLogicEnum Destination { get; set; } = DestinationsBusinessLogicEnum.BaseLibraryWithDate;
        
    public  IDestinationPathBusinessLogicModel DestinationPathDirectory { get; set; } = null!;
    
    public IDestinationPathBusinessLogicModelV2 DestinationPathDirectoryV2 { get; set; } = null!;
        
    public  IDestinationNamingBusinessLogicModel NamingPatternFiles { get; set; } = null!;
        
    public  List<IDestinationNamingBusinessLogicModel> NamingPatternFilesV2 { get; set; } = null!;
    
    
    
}