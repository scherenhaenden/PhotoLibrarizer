namespace PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;

public interface IDestinationPathBusinessLogicModel
{
    public string? PathDestination { get; }
    public List<DirectoryPathCreationBusinessLogicEnum>? DirectoryPathCreationBusinessLogicEnums { get; set; }
}


public interface IDestinationPathBusinessLogicModelV2
{
    public string? PathDestination { get; }
    public List<DirectoryNameCreationBusinessLogicOptions>? ListDirectoryNameCreationBusinessLogicOptions { get; set; }
}


public class DestinationPathBusinessLogicModelV2 : IDestinationPathBusinessLogicModelV2
{
    

    public DestinationPathBusinessLogicModelV2(List<DirectoryNameCreationBusinessLogicOptions>? listDirectoryNameCreationBusinessLogicOptions)
    {
        ListDirectoryNameCreationBusinessLogicOptions = listDirectoryNameCreationBusinessLogicOptions;
    }
        
        
    public string PathDestination { get; private set; }
    public List<DirectoryNameCreationBusinessLogicOptions>? ListDirectoryNameCreationBusinessLogicOptions { get; set; }
}
