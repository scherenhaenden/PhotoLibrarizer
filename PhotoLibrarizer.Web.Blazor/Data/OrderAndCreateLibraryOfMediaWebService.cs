using PhotoLibrarizer.BusinessLogic.Services.FileSeeking;
using PhotoLibrarizer.BusinessLogic.Services.Ordering;
using PhotoLibrarizer.Web.Blazor.Mapper;
using PhotoLibrarizer.Web.Blazor.Models;

namespace PhotoLibrarizer.Web.Blazor.Data
{
    public class OrderAndCreateLibraryOfMediaWebService: IOrderAndCreateLibraryOfMediaWebService
    {
        readonly IOrderAndCreateLibraryOfMediaBusinessLogicService _orderAndCreateLibraryOfMediaBusinessLogicService;
        public OrderAndCreateLibraryOfMediaWebService()
    {
        _orderAndCreateLibraryOfMediaBusinessLogicService = new OrderAndCreateLibraryOfMediaBusinessLogicService();
    }
    
    
        public List<string> GetFilesInPath(FilterWebModel filterBusinessLogicModel)
    {
        return _orderAndCreateLibraryOfMediaBusinessLogicService.GetFilesInPath(MapperFilters.Map(filterBusinessLogicModel));
    }

        public async Task<List<string>> OrderRoutine(FilterWebModel filterBusinessLogicModel)
    {
        return await _orderAndCreateLibraryOfMediaBusinessLogicService.OrderRoutine(MapperFilters.Map(filterBusinessLogicModel));
    }

        public List<string> GetPresentExtensions()
    {
        return _orderAndCreateLibraryOfMediaBusinessLogicService.GetPresentExtensions();
    }
    }
}