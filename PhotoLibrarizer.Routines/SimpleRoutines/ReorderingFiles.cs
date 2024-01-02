using PhotoLibrarizer.Engines.AsyncTaskManagement;
using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;
using PhotoLibrarizer.Engines.Models.DateModels;


namespace PhotoLibrarizer.Routines.SimpleRoutines
{
    public class ReorderingFiles : IReorderingFiles
    {
        private readonly IFilesSeekerV2 _filesSeekerV2;
        private readonly IFilesSeeker _filesSeeker;
        private readonly string _pathOfBaseLibrary;
        private readonly OutputDelegate _outputDelegate;

        public ReorderingFiles(IFilesSeekerV2 filesSeekerV2, OutputDelegate outputDelegate)
        {
            _filesSeekerV2 = filesSeekerV2;
            _outputDelegate = outputDelegate;
        }

            [Obsolete]
            public ReorderingFiles(IFilesSeeker filesSeeker, string pathOfBaseLibrary, OutputDelegate outputDelegate)
        {
            _filesSeeker = filesSeeker;
            _pathOfBaseLibrary = pathOfBaseLibrary;
            _outputDelegate = outputDelegate;
        }
        
            public async Task<string> DoRenameFilesAsync_SimpleDraft(string path, bool subDirectory = true, bool movesToBaseLibrary = false)
        {
            // TODO: Implement this method
            // 1.- get files in path
            var fileModels = seekfileAndGenerateFileModels(path, subDirectory);

            // 2.- get metadata from files
            // 3.- get datetime from metadata (creation, modification, etc)
            IReadFileInfo readFileInfo = new ReadFileInfo();
            
            //DateTime DateOfCreation = readFileInfo.GetDateOfMediaCreation(file.FullPathOfFile);
            var countOfPictures = fileModels.Count;
            var currentNumberOfPicture = 0;
            ITaskQueueManager taskQueueManager = new TaskQueueManager(maxConcurrentTasks: 3);
            //var taskQueue = new TaskQueue(maxConcurrentTasks: 3);
            var listFilesToBeMoved = new List<FilesToBeMoved>();
            /*var test = fileModels.Where(x=>x.FullPathOfFile.Contains("/1/") ||
                                           x.FullPathOfFile.Contains("/4/")||
                                           x.FullPathOfFile.Contains("/5/")||
                                           x.FullPathOfFile.Contains("/6/")||
                                           x.FullPathOfFile.Contains("/7/")).ToList();*/
            foreach (var fileModel in fileModels)
            {
            
                OutputInvocation(currentNumberOfPicture, countOfPictures);
                DateTime? dateOfCreationNullable = readFileInfo.GetDateOfMediaCreation(fileModel.FullPathOfFile);
                
                if (dateOfCreationNullable==null)
                {
                    continue;
                }
                
                DateTime dateOfCreation = dateOfCreationNullable.Value;
                
                
                
                fileModel.KeyDatesMetadata.Add("onlyDate", dateOfCreation);

                var fullNewNameOfFileOnlyDateAndSize = CreateNewNameOfFileOnlyDateAndSize(fileModel, dateOfCreation);
                if (movesToBaseLibrary)
                {
                    
                    
                    var directoryForPicture = GetDirectoryForPicture(dateOfCreation);
                    // check if directory exists
                    if (!Directory.Exists(directoryForPicture))
                    {
                        Directory.CreateDirectory(directoryForPicture);
                    }
                    
                    if (fileModel.PathOfFile == directoryForPicture)
                    {
                        if (fileModel.FullPathOfFile != fullNewNameOfFileOnlyDateAndSize)
                        {
                            // check uf file exists
                            if (File.Exists(fullNewNameOfFileOnlyDateAndSize))
                            {
                                IFileHasher fileHasher = new FileHasher();
                                var hashOld = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
                                var hashNew = fileHasher.GetMD5ByFilePath(fullNewNameOfFileOnlyDateAndSize);
                    
                                if (hashOld==hashNew)
                                {
                                    // delete file
                                    File.Delete(fileModel.FullPathOfFile);
                                    continue;
                                }
                                else
                                {
                                    int io = 0;
                                }
                            }
                            else
                            {
                                int io = 0;
                            }
                        
                        }
                    }
                    else
                    {
                        
                        var fullNewNameOfFileOnlyDateAndSizeWithDirectory = Path.Combine(directoryForPicture, Path.GetFileName(fullNewNameOfFileOnlyDateAndSize));
                        if (File.Exists(fullNewNameOfFileOnlyDateAndSizeWithDirectory))
                        {
                            IFileHasher fileHasher = new FileHasher();
                            var hashSourceFile = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
                            var hashExistingFile = fileHasher.GetMD5ByFilePath(fullNewNameOfFileOnlyDateAndSizeWithDirectory);
                    
                            if (hashSourceFile==hashExistingFile)
                            {
                                // delete file
                                try
                                {
                                    File.Delete(fileModel.FullPathOfFile);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                    
                                    //File.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                                    //Directory.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                                    
                                    
                                   //var passwd = Syscall.getpwnam()
                                    //var groupId = Syscall.getgrnam(group).gr_gid;
                                    
                                    // get full path of directory
                                    //var fullpath =Path.GetFullPath(fileModel.FullPathOfFile);
                                    
                                    
                                    //var unixDirInfo = new Mono.Unix.UnixDirectoryInfo(fullpath);
                                    //unixDirInfo.canAccess(AccessModes.F_OK) 
                                    
                                }
                                
                                continue;
                            }
                            else
                            {
                                // add hash to name
                                var extension = Path.GetExtension(fullNewNameOfFileOnlyDateAndSizeWithDirectory);
                                var newName = Path.GetFileNameWithoutExtension(fullNewNameOfFileOnlyDateAndSizeWithDirectory);
                                var newNameWithHash = newName + "_" + hashSourceFile + extension;
                                var newPathWithHash = Path.Combine(directoryForPicture, newNameWithHash);

                                if (File.Exists(newPathWithHash))
                                {
                                    fileHasher = new FileHasher();
                                    var anotherExistingFile = fileHasher.GetMD5ByFilePath(newPathWithHash);
                                    
                                    if (hashSourceFile==anotherExistingFile)
                                    {
                                        // 
                                        File.Delete(fileModel.FullPathOfFile);
                                        continue;
                                    }
                                    else
                                    {
                                        // only god knows
                                        int io = 0;
                                    }
                                    

                                }
                                else
                                {
                                    listFilesToBeMoved.Add(new FilesToBeMoved()
                                    {
                                        SourcePath = fileModel.FullPathOfFile,
                                        DestinationPath = newPathWithHash
                                    });
                                }
                                
                                
                                

                            }
                        }
                        else
                        {
                            
                            listFilesToBeMoved.Add(new FilesToBeMoved()
                            {
                                SourcePath = fileModel.FullPathOfFile,
                                DestinationPath = fullNewNameOfFileOnlyDateAndSizeWithDirectory
                            });
                            
                            /*File.Move(fileModel.FullPathOfFile, fullNewNameOfFileOnlyDateAndSizeWithDirectory);
                            fileModel.FullPathOfFile = fullNewNameOfFileOnlyDateAndSizeWithDirectory;*/
                             
                        }
                    }

                    if (fileModel.FullPathOfFile != fullNewNameOfFileOnlyDateAndSize)
                    {
                        // do without hash
                        
                    }
                    
                    

                    
                    
                    // get only path of file
                    //var currentPath = Path.Combine(directoryForPicture, Path.GetFileName(fileModel.FullPathOfFile));
                    
                    
                    //var fullNewNameOfFileOnlyDateAndSize = Path.Combine(directoryForPicture, newNameOfFileOnlyDateAndSize);
                }

                var newNameOfFileOnlyDateAndSize = fullNewNameOfFileOnlyDateAndSize;
                
                // check if new name and old name are the same
                if (fileModel.FullPathOfFile==newNameOfFileOnlyDateAndSize)
                {
                    //continue;
                }
                else
                {
                }

                
                _outputDelegate.Invoke(currentNumberOfPicture.ToString() + " of " + countOfPictures.ToString());
                currentNumberOfPicture++;
                
                //var pathForPicture = _pathOfBaseLibrary + ;
                
            }


            foreach (var filesToBeMoved in listFilesToBeMoved)
            {
                taskQueueManager.AddTask(() => MoveFileAsync(filesToBeMoved.SourcePath, filesToBeMoved.DestinationPath));
            }
            
            await taskQueueManager.ProcessQueueWithDynamicParallelism();
            
          
            
            
            // 7.2. - create path if not exists
            
            // write return
            return "ok";
        }

        private List<FileModel> seekfileAndGenerateFileModels(string path, bool subDirectories = true)
        {
            var files = _filesSeeker.GetFilesInPath(path, subDirectories);
            // 1.1 - translate path to models
            GenerateFiles generateFiles = new GenerateFiles();
            var fileModels = generateFiles.StringsToModels(files.ToArray());
            return fileModels;
        }

        private void OutputInvocation(int currentNumberOfPicture, int countOfPictures)
        {
            _outputDelegate.Invoke(currentNumberOfPicture.ToString() + " of " + countOfPictures.ToString());
        }

            int countOfPictures = 0;
            async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            Console.WriteLine($"Moving file: {sourcePath} to {destinationPath}");
            await Task.Delay(1); // Simulate some work
            
            
            if(sourcePath == destinationPath)
            {
                return;
            }
            
            File.Copy(sourcePath, destinationPath);
            
            // check if both files are the same size
            var sourceFileInfo = new FileInfo(sourcePath);
            var destinationFileInfo = new FileInfo(destinationPath);
            
            if (sourceFileInfo.Length == destinationFileInfo.Length)
            {
                File.Delete(sourcePath);
            }
            else
            {
                Console.WriteLine($"File move failed: {sourcePath} to {destinationPath}");
            }
            countOfPictures++;
        }

        private string GetDirectoryForPicture(DateTime DateOfCreation)
        {
            var year = DateOfCreation.ToString("yyyy");
            var month = DateOfCreation.ToString("MM");
            var day = DateOfCreation.ToString("dd");

            var directoryForPicutre = Path.Combine(_pathOfBaseLibrary, year, month, day);
            return directoryForPicutre;
        }


        private string CreateNewNameOfFileOnlyDateAndSize(FileModel fileModel, DateTime dateOfCreation)
        {
            IReadFileInfo readFileInfo = new ReadFileInfo();
            var newName=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");
            var newNameFullname=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");

            var filename=fileModel.FileName;

            var directoryname=fileModel.PathOfFile;

            var extension=Path.GetExtension(fileModel.FullPathOfFile).ToLower();
                
            var size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);
            var  maybeOfficialName=directoryname+"/"+newName+"_"+size+extension;
            
            // check if new name and old name are the same
            if (fileModel.FullPathOfFile==maybeOfficialName) {
            
             return maybeOfficialName;
            }     
            // 4.- try rename files with datetime
            // 5.- if rename fails, try rename with datetime and size
            // actually doing it right away
            // check if file exists
            //*if (File.Exists(maybeOfficialName))
            
             // add hash of file to name
            // 6.- if rename fails, try rename with datetime and size and hash
            IFileHasher fileHasher = new FileHasher();
            var hash = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
            maybeOfficialName=directoryname+"/"+newName+"_"+size+"_"+hash+extension;


            if (File.Exists(maybeOfficialName))
            {
                return maybeOfficialName;
            }
            else
            {
                File.Move(fileModel.FullPathOfFile, maybeOfficialName);
            }

            return maybeOfficialName;
            
        }

    
        private string RenameFileAndGetNewName(FileModel fileModel, DateTime dateOfCreation)
        {
            IReadFileInfo readFileInfo = new ReadFileInfo();
            var newName=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");
            var newNameFullname=dateOfCreation.ToString("yyyy_MM_dd_HH_mm_ss");

            var filename=fileModel.FileName;

            var directoryname=fileModel.PathOfFile;

            var extension=Path.GetExtension(fileModel.FullPathOfFile).ToLower();
                
            var size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);
            var  maybeOfficialName=directoryname+"/"+newName+"_"+size+extension;
            
            // check if new name and old name are the same
            if (fileModel.FullPathOfFile==maybeOfficialName) {
            
             return maybeOfficialName;
            }     
            // 4.- try rename files with datetime
            // 5.- if rename fails, try rename with datetime and size
            // actually doing it right away
            // check if file exists
            if (File.Exists(maybeOfficialName))
            {
                // add hash of file to name
                // 6.- if rename fails, try rename with datetime and size and hash
                IFileHasher fileHasher = new FileHasher();
                var hash = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
                maybeOfficialName=directoryname+"/"+newName+"_"+size+"_"+hash+extension;
                  
                    
                if (File.Exists(maybeOfficialName))
                {
                    // add size of file to name
                    //Size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);
                    return maybeOfficialName;
                }
            
                
                 File.Move(fileModel.FullPathOfFile, maybeOfficialName);
                }             
                    
          
            return maybeOfficialName;
            
        }

        public Task DoRenameFilesAsync(string path, string pattern, string replacement, bool recursive, bool dryRun)
    {
        throw new NotImplementedException();
    }
    
        private static bool CanBeAddThroughCamera(FilterModel filterModel, string camera)
    {
        
        bool cameraExistsV1 = false;
        
        if(filterModel.CamerasShouldBe != null)
        {
            cameraExistsV1 = filterModel.CamerasShouldBe.Any(x => camera.ToLower().Contains(x.ToLower()));
        }
        
        if(filterModel.CamerasShouldNotBe != null)
        {
            cameraExistsV1 = !filterModel.CamerasShouldNotBe.Any(x => camera.ToLower().Contains(x.ToLower()));
        }
      
        return cameraExistsV1;
    }

        private FileModel LoadMetaData(FileModel fileModel)
    {
        IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile);
        fileModel.Directories = metadataManager.Directories;
        return fileModel;
    }
    
        ITaskQueueManager taskQueueManager = new TaskQueueManager(maxConcurrentTasks: 3);

        public async Task DoByFileModelAsync(FilterModel filterModel)
    {
        // 1.- get files in path
        
        var result = _filesSeekerV2.GetFilesInPath(filterModel.PathsForSourceFiles, filterModel?.Extensions, true, false);

        var files = new List<string>();
        foreach (var path in filterModel.PathsForSourceFiles) {
        
         files.AddRange(_filesSeekerV2.GetFilesInPath(path, filterModel?.Extensions, true, false));
        }      // 1.1 - translate path to models
        GenerateFiles generateFiles = new GenerateFiles();
        var fileModels = generateFiles.StringsToModels(result.ToArray());
        var filesSwapped = new List<FileModel>();
        var listFilesToBeMoved = new List<FilesToBeMoved>();

        if (filterModel.DestinationModel.Destination == Destinations.CameraBasedDirectoryWithoutDate)
        {

            foreach (var fileModel in fileModels)
            {
                IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
                fileModel.Directories = metadataManager.Directories;
                var camera = metadataManager.GetModelOfCamera();
                if (string.IsNullOrEmpty(camera))
                {
                    camera = "unknown";
                }
                fileModel.KeyMetadata.Add("camera", camera);
                

                if (filterModel.CamerasShouldBe != null || filterModel.CamerasShouldNotBe != null)
                {
                    
                    var cameraExistsV1 = CanBeAddThroughCamera(filterModel, camera);
                    if (cameraExistsV1)
                    {
                        filesSwapped.Add(fileModel);
                    }
                    
                    
                }
                
                if(filterModel?.MaxFiles != null)
                {
                    if (filesSwapped.Count >= filterModel.MaxFiles)
                    {
                        break;
                    }
                }
                
           

            }

            foreach (var fileModel in filesSwapped)
            {
                
                // get camera information form fileModel.KeyMetadata.Add("camera", camera);
                var camera = fileModel.KeyMetadata["camera"];
                
                
                var directoryForPicture = Path.Combine(filterModel.DestinationModel.BasePath, camera );
                var moveHere = Path.Combine(directoryForPicture, fileModel.FileName);

                if (!File.Exists(moveHere))
                {
                    if (!Directory.Exists(directoryForPicture))
                    {
                        Directory.CreateDirectory(directoryForPicture);
                    }
                    /*var fullNewNameOfFileOnlyDateAndSizeWithDirectory = Path.Combine(fileModel.PathOfFile,
                        Path.GetFileName(fullNewNameOfFileOnlyDateAndSize));*/
                
                    listFilesToBeMoved.Add(new FilesToBeMoved()
                    {
                        SourcePath = fileModel.FullPathOfFile,
                        DestinationPath = moveHere
                    });
                }
                
            }
            
          
            
            
        }
        
        foreach (var filesToBeMoved in listFilesToBeMoved)
        {
            taskQueueManager.AddTask(() => MoveFileAsync(filesToBeMoved.SourcePath, filesToBeMoved.DestinationPath));
        }

        await taskQueueManager.ProcessQueueWithDynamicParallelism();
        
        // 2.- get metadata from files
        // 3.- get datetime from metadata (creation, modification, etc)

    }
    }
}