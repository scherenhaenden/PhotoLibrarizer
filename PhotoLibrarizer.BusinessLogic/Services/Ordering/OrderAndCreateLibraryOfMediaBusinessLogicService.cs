using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Services.IOService;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering
{
    public class OrderAndCreateLibraryOfMediaBusinessLogicService: IOrderAndCreateLibraryOfMediaBusinessLogicService
    {
        IFilesSeekerV2 _filesSeekerV2;
        StoreResults _storeResults;
    
        public OrderAndCreateLibraryOfMediaBusinessLogicService()
    {
        _filesSeekerV2 = new FilesSeekerV2();
        _storeResults = new StoreResults();
    }
    
    
        public List<string> GetFilesInPath(List<string> paths, bool subDirectory = true)
    {
        _storeResults.ResultRawAllFiles = _filesSeekerV2.GetFilesInPath(paths, subDirectory);
        return _storeResults.ResultRawAllFiles;
    }

        public List<string> GetFilesInPath(FilterBusinessLogicModel filterBusinessLogicModel)
    {
        _storeResults.ResultRawAllFiles = _filesSeekerV2
            .GetFilesInPath(filterBusinessLogicModel.PathsForSourceFiles,
                filterBusinessLogicModel.Extensions,
                
                true);
        return _storeResults.ResultRawAllFiles;
    }
    
        private List<FileModel> GenerateFileModels(List<string>? filePaths, int maxFiles = 0)
    {
        if(filePaths == null)
            return new List<FileModel>();
        
        
        GenerateFiles generateFiles = new GenerateFiles();

        if (maxFiles > 0)
        {
            var pathsToUse = filePaths.Take(maxFiles).ToList();
            return generateFiles.StringsToModels(pathsToUse.ToArray());
        }
            
        var fileModels = generateFiles.StringsToModels(filePaths.ToArray());
        return fileModels;
    }

        public async Task<List<string>> OrderRoutine(FilterBusinessLogicModel filterBusinessLogicModel)
    {
        if(_storeResults?.ResultRawAllFiles == null || _storeResults?.ResultRawAllFiles?.Count == 0)
            GetFilesInPath(filterBusinessLogicModel);
        
        // TODO: order files
        
        
        var fileModels = GenerateFileModels(_storeResults?.ResultRawAllFiles, filterBusinessLogicModel.MaxFiles?? 10000);
        
        if(fileModels.Count == 0)
            return new List<string>();
        //return fileModels;
        
        // order files
        
        // based on what??
        foreach (var fileModel in fileModels)
        {
            if (filterBusinessLogicModel.DestinationModel.Destination ==
                DestinationsBusinessLogicEnum.CameraBasedDirectoryWithoutDate)
            {
                
            }
            
        }
        
        
        

        foreach (var fileModel in fileModels)
        {
            IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
            var camera = metadataManager.GetModelOfCamera();
            
            if (string.IsNullOrEmpty(camera))
            {
                camera = "unknown";
            }
            
            if (!string.IsNullOrEmpty(camera))
            {
                
                var result =fileModel.FullPathOfFile;
                var directoryForPicture = Path.Combine(filterBusinessLogicModel.DestinationModel.BasePath, camera );
                var moveHere = Path.Combine(directoryForPicture, fileModel.FileName);

                if (!File.Exists(moveHere))
                {
                    if (!Directory.Exists(directoryForPicture))
                    {
                        Directory.CreateDirectory(directoryForPicture);
                    }
                    /*var fullNewNameOfFileOnlyDateAndSizeWithDirectory = Path.Combine(fileModel.PathOfFile,
                        Path.GetFileName(fullNewNameOfFileOnlyDateAndSize));*/
                    
                    IMoveFiles moveFiles = new MoveFiles();
                    await moveFiles.MoveFileAsync(fileModel.FullPathOfFile, moveHere);
                
                    //*listFilesToBeMoved.Add(new FilesToBeMoved()
                    
                
                                         }
                
                
            }
            
        }





        return new List<string>();
    }

        public List<string> GetPresentExtensions()
    {
        // get file extensions from all files
        _storeResults.FileExtensions = _storeResults.ResultRawAllFiles.Select(file => Path.GetExtension(file)).Distinct().ToList();
        return _storeResults.FileExtensions;
        
        
    }
    }
}