using Newtonsoft.Json;
using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.Web.Blazor.Models;

namespace PhotoLibrarizer.Web.Blazor.Mapper
{
    public class MapperFilters
    {
        public static FilterBusinessLogicModel Map(FilterWebModel filterWebModel)
    {
        /*var filterBusinessLogicModel = new FilterBusinessLogicModel();
        filterBusinessLogicModel.PathsForSourceFiles = filterWebModel.PathsForSourceFiles;
        return filterBusinessLogicModel;*/

        // map using json
        var filterBusinessLogicModel = JsonConvert.DeserializeObject<FilterBusinessLogicModel>(JsonConvert.SerializeObject(filterWebModel));
        return filterBusinessLogicModel;
    }
    
        public static FilterWebModel Map(FilterBusinessLogicModel filterBusinessLogicModel)
    {
        /*var filterWebModel = new FilterWebModel();
        filterWebModel.PathsForSourceFiles = filterBusinessLogicModel.PathsForSourceFiles;
        return filterWebModel;*/

        // map using json
        var filterWebModel = JsonConvert.DeserializeObject<FilterWebModel>(JsonConvert.SerializeObject(filterBusinessLogicModel));
        return filterWebModel;
    }
    }
}