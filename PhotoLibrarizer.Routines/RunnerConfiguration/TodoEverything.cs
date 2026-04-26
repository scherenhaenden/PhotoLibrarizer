using System.Text.Json;
using PhotoLibrarizer.Engines.Filters.Models;

namespace PhotoLibrarizer.Routines.RunnerConfiguration
{
    public class TodoEverything
    {
    
    }

    public interface IConfigurationLoader
    {
        public FilterModel LoadConfiguration(string pathOfJsonFile);
    }


    public class ConfigurationLoader:IConfigurationLoader
    {
        public FilterModel LoadConfiguration(string pathOfJsonFile)
    {
        var json = File.ReadAllText(pathOfJsonFile);
        var filterModel = JsonSerializer.Deserialize<FilterModel>(json);
        return filterModel;
    }
    }
}