using PhotoLibrarizer.Engines.AsyncTaskManagement;
using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;
using PhotoLibrarizer.Engines.Models.DateModels;

namespace PhotoLibrarizer.Routines.SimpleRoutines
{
    public class ReorderingFilesFindByPropertiesV1 : IReorderingFilesFindByProperties
    {
        private readonly IFilesSeeker _filesSeeker;
        private readonly string _pathOfBaseLibrary;
        private readonly OutputDelegate _outputDelegate;

        public ReorderingFilesFindByPropertiesV1(IFilesSeeker filesSeeker, string pathOfBaseLibrary, OutputDelegate outputDelegate)
    {
        _filesSeeker = filesSeeker;
        _pathOfBaseLibrary = pathOfBaseLibrary;
        _outputDelegate = outputDelegate;
    }
    
        public async Task<string> DoRenameFilesAsync_SimpleDraft(string path, bool subDirectory = true, bool movesToBaseLibrary = false)
    {
        // TODO: Implement this method
        // 1.- get files in path
        var fileModels = SeekfileAndGenerateFileModels(path, subDirectory);

        await AllDoingsHere(movesToBaseLibrary, fileModels);


        // 7.2. - create path if not exists
        
        // write return
        return "ok";
    }
    
        private void MoveFile(string sourcePath, string destinationPath)
    {
        Console.WriteLine($"Moving file: {sourcePath} to {destinationPath}");
        //await Task.Delay(1); // Simulate some work
        File.Move(sourcePath, destinationPath);
    }
    
    
        int intAllDoingsHereBasedOnDirectory = 0;
        public async Task AllDoingsHereBasedOnDirectory(bool movesToBaseLibrary, List<FileModel> fileModels, bool renaming = true)
    {
        
        
        
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
            IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
            var camera = metadataManager.GetModelOfCamera();

            if (string.IsNullOrEmpty(camera))
            {
                camera = "unknown";
            }
            
            if (!string.IsNullOrEmpty(camera))
            {

                var result =fileModel.FullPathOfFile;
                var directoryForPicture = Path.Combine(_pathOfBaseLibrary, camera );
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
            Console.WriteLine($"Moving file: {intAllDoingsHereBasedOnDirectory} to {fileModels.Count}");
            intAllDoingsHereBasedOnDirectory++;
        }


        foreach (var filesToBeMoved in listFilesToBeMoved)
        {
            taskQueueManager.AddTask(() => MoveFileAsync(filesToBeMoved.SourcePath, filesToBeMoved.DestinationPath));
        }

        await taskQueueManager.ProcessQueueWithDynamicParallelism();
    }

        private async Task AllDoingsHere(bool movesToBaseLibrary, List<FileModel> fileModels, bool renaming = true)
    {
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
            DateTime? dateOfCreationNullable;
            if (fileModel.Directories != null)
            {
                IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
                dateOfCreationNullable = metadataManager.GetDateOfMediaCreation();
            }
            else
            {
                dateOfCreationNullable = readFileInfo.GetDateOfMediaCreation(fileModel.FullPathOfFile);
            }
            
            
             

            if (dateOfCreationNullable == null)
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
                            IFileHasher fileHasher = new FileHasherDotnet();
                            var hashOld = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
                            var hashNew = fileHasher.GetMD5ByFilePath(fullNewNameOfFileOnlyDateAndSize);

                            if (hashOld == hashNew)
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
                    var fullNewNameOfFileOnlyDateAndSizeWithDirectory = Path.Combine(directoryForPicture,
                        Path.GetFileName(fullNewNameOfFileOnlyDateAndSize));
                    if (File.Exists(fullNewNameOfFileOnlyDateAndSizeWithDirectory))
                    {
                        IFileHasher fileHasher = new FileHasherDotnet();
                        var hashSourceFile = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
                        var hashExistingFile = fileHasher.GetMD5ByFilePath(fullNewNameOfFileOnlyDateAndSizeWithDirectory);

                        if (hashSourceFile == hashExistingFile)
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
                                fileHasher = new FileHasherDotnet();
                                var anotherExistingFile = fileHasher.GetMD5ByFilePath(newPathWithHash);

                                if (hashSourceFile == anotherExistingFile)
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
            else if (renaming)
            {
                var fullNewNameOfFileOnlyDateAndSizeWithDirectory = Path.Combine(fileModel.PathOfFile,
                    Path.GetFileName(fullNewNameOfFileOnlyDateAndSize));
                
                listFilesToBeMoved.Add(new FilesToBeMoved()
                {
                    SourcePath = fileModel.FullPathOfFile,
                    DestinationPath = fullNewNameOfFileOnlyDateAndSizeWithDirectory
                });
            }

            var newNameOfFileOnlyDateAndSize = fullNewNameOfFileOnlyDateAndSize;

            // check if new name and old name are the same
            if (fileModel.FullPathOfFile == newNameOfFileOnlyDateAndSize)
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
    }

        private List<FileModel> SeekfileAndGenerateFileModels(string path, bool subDirectories = true)
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

        private int i = 0;
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

        i++;
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
            IFileHasher fileHasher = new FileHasherDotnet();
            var hash = fileHasher.GetMD5ByFilePath(fileModel.FullPathOfFile);
            maybeOfficialName=directoryname+"/"+newName+"_"+size+"_"+hash+extension;
            var Size=readFileInfo.GetFileSize(fileModel.FullPathOfFile);

            if (File.Exists(maybeOfficialName))
            {
                return maybeOfficialName;
            }

            
         File.Move(fileModel.FullPathOfFile, maybeOfficialName);
          
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
            IFileHasher fileHasher = new FileHasherDotnet();
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
    
   

   

        public async Task DoRenameFilesAsync(string path, FilterModel filterModel, bool cameraBasedDirectory, string pattern, bool including = true)
    {
        // TODO: Implement this method
        // 1.- get files in path
        //var fileModels = seekfileAndGenerateFileModels(path, subDirectory);
        var fileModels = SeekfileAndGenerateFileModels(path, true);

        // 2.- get metadata from files
        // 3.- get datetime from metadata (creation, modification, etc);
        List<FileModel> filesSwapped = new List<FileModel>();
        
        
        List<string> foundCameras=new List<string>();

        foreach (var fileModel in fileModels)
        {
        
            IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile);
            if(filterModel.CamerasShouldBe != null || filterModel.CamerasShouldNotBe != null)
            {
                var camera = metadataManager.GetModelOfCamera();
                if (string.IsNullOrEmpty(camera))
                {
                    camera = "unknown";
                   
                    
                   
                }
                
                if(!foundCameras.Contains(camera))
                {
                    foundCameras.Add(camera);
                }
                
                //var cameraExists = filterModel.Cameras.Contains(camera);
                var cameraExistsV1 = CanBeAddThroughCamera(filterModel, camera);
                if (cameraExistsV1)
                {
                    fileModel.Directories = metadataManager.Directories;
                    filesSwapped.Add(fileModel);
                    //filesSwaped.Last().KeyDatesMetadata.Add("camera", camera);
                        
                }
                
            }
            else
            {
                filesSwapped.Add(fileModel);
            }
            if (filterModel.MaxFiles > 0)
            {
                if (filesSwapped.Count >= filterModel.MaxFiles)
                {
                    break;
                }
            }
        }

        if (cameraBasedDirectory)
        {
            await AllDoingsHereBasedOnDirectory(true, filesSwapped, true);
        }
        else
        {
            await AllDoingsHere(true, filesSwapped, true);    
        }
        
        
        
        
        
        
        
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
    }
}