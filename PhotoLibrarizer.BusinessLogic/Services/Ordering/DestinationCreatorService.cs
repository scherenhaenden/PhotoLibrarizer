using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public class DestinationCreatorService: IDestinationCreatorService
{
    
    public string CreateDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel)
    {
       
        IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
                
        var currentDateOfFile = fileModel.DateCreation ??  metadataManager.GetDateOfMediaCreation();
        
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
    
    
    public string CreateDestinationFile(List<FileNameCreationBusinessLogicEnum> fileNameCreationBusinessLogicEnums, FileModel fileModel, DateTime? currentDateOfFile)
    {
        
        
        string createdName = string.Empty;
         fileNameCreationBusinessLogicEnums.ForEach(x =>
                {
                    switch (x)
                    {
                        case FileNameCreationBusinessLogicEnum.Year:
                            createdName += currentDateOfFile.Value.ToString("yyyy");
                            break;
                        case FileNameCreationBusinessLogicEnum.Month:
                            createdName += currentDateOfFile.Value.ToString("MM");
                            break;
                        case FileNameCreationBusinessLogicEnum.Day:
                            createdName += currentDateOfFile.Value.ToString("dd");
                            break;
                        case FileNameCreationBusinessLogicEnum.Hour:
                            createdName += currentDateOfFile.Value.ToString("HH");
                            break;
                        case FileNameCreationBusinessLogicEnum.Minute:
                            createdName += currentDateOfFile.Value.ToString("mm");
                            break;
                        case FileNameCreationBusinessLogicEnum.Seconds:
                            createdName += currentDateOfFile.Value.ToString("ss");
                            break;
                        case FileNameCreationBusinessLogicEnum.Millisecond:
                            createdName += currentDateOfFile.Value.ToString("mmm");
                            break;
                        case FileNameCreationBusinessLogicEnum.Size:
                            createdName += fileModel.Size.ToString();
                            break;
                        case FileNameCreationBusinessLogicEnum.Hash:
                            createdName += fileModel.Hash.ToString();
                            break;
                        case FileNameCreationBusinessLogicEnum.OwnDateFormat:
                            break;
                        case FileNameCreationBusinessLogicEnum.Separator:
                            createdName += "_";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });

                return createdName;



    }
    
}