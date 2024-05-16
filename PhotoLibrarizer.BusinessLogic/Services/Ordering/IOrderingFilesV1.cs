using PhotoLibrarizer.BusinessLogic.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public interface IOrderingFilesV1
{
    public Task MultiThreadedOrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh=false);
    
    public Task OrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh=false);
}