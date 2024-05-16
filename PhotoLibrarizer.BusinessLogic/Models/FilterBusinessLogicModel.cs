using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;

namespace PhotoLibrarizer.BusinessLogic.Models
{
    public class FilterBusinessLogicModel
    {
        public List<string> PathsForSourceFiles { get; set; } = new List<string>();
        public List<string>? Extensions { get; set; } = new List<string>();
    
        public DestinationBusinessLogicModel DestinationModel { get; set; } = new DestinationBusinessLogicModel();
    
        public int? MaxFiles { get; set; }
    }


    public class DestinationPathCustomPatternBusinessLogicModel : IDestinationPathBusinessLogicModel
    {
        public DestinationPathCustomPatternBusinessLogicModel(string? combinablePathDestination)
        {
            PathDestination = combinablePathDestination;
        }
        
        
        public string? PathDestination { get; private set; }
        public List<DirectoryPathCreationBusinessLogicEnum>? DirectoryPathCreationBusinessLogicEnums { get; set; }
    }
    
    
    public class DestinationPathCustomByPropertiesPatternBusinessLogicModel : IDestinationPathBusinessLogicModel
    {
        private readonly List<DirectoryPathCreationBusinessLogicEnum> _directoryPathCreationBusinessLogicEnums;

        public DestinationPathCustomByPropertiesPatternBusinessLogicModel(List<DirectoryPathCreationBusinessLogicEnum> directoryPathCreationBusinessLogicEnums)
        {
            _directoryPathCreationBusinessLogicEnums = directoryPathCreationBusinessLogicEnums;
        }
        
        
        public string PathDestination { get; private set; }
        public List<DirectoryPathCreationBusinessLogicEnum>? DirectoryPathCreationBusinessLogicEnums { get; set; }
    }


    public enum DestinationsBusinessLogicEnum
    {
        BaseLibraryWithoutDate,
        BaseLibraryWithDate,
        CameraBasedDirectoryWithoutDate,
        CameraBasedDirectoryWithDate,
    }
    
    public enum DirectorySplit
    {
        Date,
        Camera,
    }
    
    public enum DateDirectoriesBusinessLogic
    {
        Year = 1,
        Month = 2,
        Day = 3,
        Hour = 4,
        Minute = 5,
        Millisecond = 6,
        Size = 7,
        Hash = 8,
        OwnDateFormat = 9, }
}