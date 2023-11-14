using PhotoLibrarizer.BusinessLogic.Services.FileSeeking;
using PhotoLibrarizer.Web.Blazor.Mapper;
using PhotoLibrarizer.Web.Blazor.Models;

namespace PhotoLibrarizer.Web.Blazor.Data;

public interface IOrderAndCreateLibraryOfMediaWebService
{
    public List<string> GetFilesInPath(FilterWebModel filterBusinessLogicModel);
    public List<string> GetPresentExtensions();
    
}

public class OrderAndCreateLibraryOfMediaWebService: IOrderAndCreateLibraryOfMediaWebService
{

    IOrderAndCreateLibraryOfMediaBusinessLogicService _orderAndCreateLibraryOfMediaBusinessLogicService;
    public OrderAndCreateLibraryOfMediaWebService()
    {
        _orderAndCreateLibraryOfMediaBusinessLogicService = new OrderAndCreateLibraryOfMediaBusinessLogicService();
    }
    
    
    public List<string> GetFilesInPath(FilterWebModel filterBusinessLogicModel)
    {
        return _orderAndCreateLibraryOfMediaBusinessLogicService.GetFilesInPath(MapperFilters.Map(filterBusinessLogicModel));
    }

    public List<string> GetPresentExtensions()
    {
        return _orderAndCreateLibraryOfMediaBusinessLogicService.GetPresentExtensions();
    }
}