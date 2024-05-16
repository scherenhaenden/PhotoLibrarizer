using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;

namespace PhotoLibrarizer.BusinessLogic.Models;

public class DestinationNamingBusinessLogicModel: IDestinationNamingBusinessLogicModel
{
    public DestinationNamingBusinessLogicModel(List<FileNameCreationBusinessLogicEnum> fileNameCreationBusinessLogicEnums)
    {
        FileNameCreationBusinessLogicEnums = fileNameCreationBusinessLogicEnums;
    }
    
    
   

    public string NewName => CreateStringName();
    public List<FileNameCreationBusinessLogicEnum> FileNameCreationBusinessLogicEnums { get; }
    


    private string CreateStringName ()
    {
        return "";
    }
}