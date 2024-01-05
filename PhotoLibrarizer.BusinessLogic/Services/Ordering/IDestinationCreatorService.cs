using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public interface IDestinationCreatorService
{
    public string CreateDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel);
}

public class DestinationCreatorService: IDestinationCreatorService
{
    
    public string CreateDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel)
    {
       
        IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
                
        var currentDateOfFile = metadataManager.GetDateOfMediaCreation();
        
             var kindOfDestination = filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.PathDestination;

                    if (string.IsNullOrEmpty(kindOfDestination))
                    {
                        kindOfDestination = string.Empty;
                        
                        filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.DirectoryPathCreationBusinessLogicEnums
            
                            .Select(x =>
                            {
                                switch (x)
                                {
                                    case DirectoryPathCreationBusinessLogicEnum.Year:
                                        kindOfDestination += currentDateOfFile.Value.ToString("yyyy");
                                        return "yyyy";
                                    case DirectoryPathCreationBusinessLogicEnum.Month:
                                        kindOfDestination += currentDateOfFile.Value.ToString("MM");
                                        return "MM";
                                    case DirectoryPathCreationBusinessLogicEnum.Day:
                                        kindOfDestination += currentDateOfFile.Value.ToString("dd");
                                        return "dd";
                                    case DirectoryPathCreationBusinessLogicEnum.Hour:
                                        kindOfDestination += currentDateOfFile.Value.ToString("HH");
                                        return "HH";
                                    case DirectoryPathCreationBusinessLogicEnum.Minutes:
                                        kindOfDestination += currentDateOfFile.Value.ToString("mm");
                                        return "mm";
                                    case DirectoryPathCreationBusinessLogicEnum.Seconds:
                                        kindOfDestination += currentDateOfFile.Value.ToString("ss");
                                        return "ss";
                                    case DirectoryPathCreationBusinessLogicEnum.Milliseconds:
                                        kindOfDestination += currentDateOfFile.Value.ToString("mmm");
                                        return "mmm";
                                    case DirectoryPathCreationBusinessLogicEnum.Separator:
                                        kindOfDestination += "/";
                                        return "/";
                                    case DirectoryPathCreationBusinessLogicEnum.MetaCameraModel:
                                        kindOfDestination += metadataManager.GetModelOfCamera();
                                        return metadataManager.GetModelOfCamera();
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }).Aggregate((x, y) => x + y);
                        
                        
                        
                       
                    }
                    
        
        return kindOfDestination;
    }
    
}