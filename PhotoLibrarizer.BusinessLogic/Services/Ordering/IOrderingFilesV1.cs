using PhotoLibrarizer.BusinessLogic.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public interface IOrderingFilesV1
{
    public Task OrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh=false);
}