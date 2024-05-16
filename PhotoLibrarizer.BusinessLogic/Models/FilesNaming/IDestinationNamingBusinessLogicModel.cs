namespace PhotoLibrarizer.BusinessLogic.Models.FilesNaming;

public interface IDestinationNamingBusinessLogicModel
{
    public string NewName { get; }
    public List<FileNameCreationBusinessLogicEnum> FileNameCreationBusinessLogicEnums { get;  }
}



public interface IDestinationNamingBusinessLogicModelV2
{
    public List<FileNameCreationBusinessLogicOptions> ListFileNameCreationBusinessLogicOptions { get;  }
}


public class DestinationNamingBusinessLogicModelV2: IDestinationNamingBusinessLogicModelV2
{
    public DestinationNamingBusinessLogicModelV2(List<FileNameCreationBusinessLogicOptions> listFileNameCreationBusinessLogicOptions)
    {
        ListFileNameCreationBusinessLogicOptions = listFileNameCreationBusinessLogicOptions;
    }

    public List<FileNameCreationBusinessLogicOptions> ListFileNameCreationBusinessLogicOptions { get; }
}