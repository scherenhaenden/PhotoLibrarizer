using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public interface IDestinationCreatorService
{
    public string CreateDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel);

    public string CreateDestinationFile(List<FileNameCreationBusinessLogicEnum> fileNameCreationBusinessLogicEnums,
        FileModel fileModel, DateTime? currentDateOfFile);
}