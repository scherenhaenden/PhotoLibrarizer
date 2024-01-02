using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;
using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.IoEngines.FilesModelsMapper;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;
using Renci.SshNet;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public class OrderingFilesV1: IOrderingFilesV1
{
    
    IFilesSeekerV2 _filesSeekerV2;

    public OrderingFilesV1()
    {
        _filesSeekerV2 = new FilesSeekerV2();
    
    }
    
    bool usingSsh = false;

    private string CreateDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel)
    {
       
        IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
                
        var currentDateOfFile = metadataManager.GetDateOfMediaCreation();
        
             var kindOfDestination = filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.PathDestination;

                    if (string.IsNullOrEmpty(kindOfDestination))
                    {
                        kindOfDestination = string.Empty;
                        
                        filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.DirectoryPathCreationBusinessLogicEnums
            
                            .Select(x =>
                            {
                                switch (x)
                                {
                                    case DirectoryPathCreationBusinessLogicEnum.Year:
                                        kindOfDestination += currentDateOfFile.Value.ToString("yyyy");
                                        return "yyyy";
                                    case DirectoryPathCreationBusinessLogicEnum.Month:
                                        kindOfDestination += currentDateOfFile.Value.ToString("MM");
                                        return "MM";
                                    case DirectoryPathCreationBusinessLogicEnum.Day:
                                        kindOfDestination += currentDateOfFile.Value.ToString("dd");
                                        return "dd";
                                    case DirectoryPathCreationBusinessLogicEnum.Hour:
                                        kindOfDestination += currentDateOfFile.Value.ToString("HH");
                                        return "HH";
                                    case DirectoryPathCreationBusinessLogicEnum.Minutes:
                                        kindOfDestination += currentDateOfFile.Value.ToString("mm");
                                        return "mm";
                                    case DirectoryPathCreationBusinessLogicEnum.Seconds:
                                        kindOfDestination += currentDateOfFile.Value.ToString("ss");
                                        return "ss";
                                    case DirectoryPathCreationBusinessLogicEnum.Milliseconds:
                                        kindOfDestination += currentDateOfFile.Value.ToString("mmm");
                                        return "mmm";
                                    case DirectoryPathCreationBusinessLogicEnum.Separator:
                                        kindOfDestination += "/";
                                        return "/";
                                    case DirectoryPathCreationBusinessLogicEnum.MetaCameraModel:
                                        kindOfDestination += metadataManager.GetModelOfCamera();
                                        return metadataManager.GetModelOfCamera();
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }
                            }).Aggregate((x, y) => x + y);
                        
                        
                        
                       
                    }
                    
        
        return kindOfDestination;
    }
    
    SshClient _sshClient;
    public string? ExecuteSshCommand(string host, string username, string password, string command)
    {

        if (_sshClient is null)
        {
            _sshClient = new SshClient(host, username, password);
            
        }

        if (!_sshClient.IsConnected)
        {
            _sshClient.Connect();
        }
            
        
        var cmd = _sshClient.CreateCommand(command);
        var result = cmd.Execute();
        
        if(!string.IsNullOrEmpty(cmd.Error))
        {
            Console.WriteLine(cmd.Error);
            return cmd.Error;
        }
        
        return result;
    }
    
    public void DisconnectSsh()
    {
        _sshClient.Disconnect();
    }
    
    
    public Task OrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh=false)
    {
        usingSsh = useSsh;
        List<string> files = _filesSeekerV2.GetFilesInPath(filterBusinessLogicModel.PathsForSourceFiles, filterBusinessLogicModel.Extensions);
        
        IFileModelsMapper fileModelsMapper = new FileModelsMapper();
        
        var fileModels = fileModelsMapper.MapPaths(files)/*.Take(filterBusinessLogicModel.MaxFiles ?? 10000)*/.ToList();
        
        int counter = 0;
        // for now only for dates
        foreach (var fileModel in fileModels)
        {
            
          
            Console.WriteLine( "following is about to start: "+counter);
            // get date from file
            IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
            fileModel.Directories = metadataManager.Directories;
            
            var currentDateOfFile = metadataManager.GetDateOfMediaCreation();
            
            if(currentDateOfFile is null)
            {
                Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                continue;
            }

            if (currentDateOfFile is not null)
            {
                var kindOfDestination = filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.PathDestination;

                if (string.IsNullOrEmpty(kindOfDestination))
                {
                    kindOfDestination = CreateDestinationDirectory(filterBusinessLogicModel, fileModel);
                }
                
                // create destination path string
                var destinationPathWithoutRoot = "/" + kindOfDestination;//currentDateOfFile.Value.ToString(kindOfDestination).Replace(".", "/");
                    
                //var destinationPath = Path.Combine(filterBusinessLogicModel.DestinationModel.BasePath, destinationPathWithoutRoot);

                var destinationPath =
                    filterBusinessLogicModel.DestinationModel.BasePath + destinationPathWithoutRoot;
                
                // create directory if not exists
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                
                // create file name string
                var fileName = fileModel.FileName;
                var createdName = string.Empty;
               
                
                fileModel.Hash = GetFileHash(fileModel);

                if (string.IsNullOrEmpty(fileModel.Hash))
                {
                    Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                    continue;
                }
                    
                
                
                filterBusinessLogicModel.DestinationModel.NamingPatternFiles.FileNameCreationBusinessLogicEnums.ForEach(x =>
                {
                    switch (x)
                    {
                        case FileNameCreationBusinessLogicEnum.Year:
                            createdName += currentDateOfFile.Value.ToString("yyyy");
                            break;
                        case FileNameCreationBusinessLogicEnum.Month:
                            createdName += currentDateOfFile.Value.ToString("MM");
                            break;
                        case FileNameCreationBusinessLogicEnum.Day:
                            createdName += currentDateOfFile.Value.ToString("dd");
                            break;
                        case FileNameCreationBusinessLogicEnum.Hour:
                            createdName += currentDateOfFile.Value.ToString("HH");
                            break;
                        case FileNameCreationBusinessLogicEnum.Minute:
                            createdName += currentDateOfFile.Value.ToString("mm");
                            break;
                        case FileNameCreationBusinessLogicEnum.Seconds:
                            createdName += currentDateOfFile.Value.ToString("ss");
                            break;
                        case FileNameCreationBusinessLogicEnum.Millisecond:
                            createdName += currentDateOfFile.Value.ToString("mmm");
                            break;
                        case FileNameCreationBusinessLogicEnum.Size:
                            createdName += fileModel.Size.ToString();
                            break;
                        case FileNameCreationBusinessLogicEnum.Hash:
                            createdName += fileModel.Hash.ToString();
                            break;
                        case FileNameCreationBusinessLogicEnum.OwnDateFormat:
                            break;
                        case FileNameCreationBusinessLogicEnum.Separator:
                            createdName += "_";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });
                
                
                var possibleNewName = createdName;
                
                var newFileNamePath = Path.Combine(destinationPath, possibleNewName);
                
                var newFileNamePathWithExtension = newFileNamePath + fileModel.GeneralFileInformation.Extension;
                
                if (File.Exists(newFileNamePathWithExtension))
                {
                    if(GetFileHash(newFileNamePathWithExtension) == fileModel.Hash)
                    {
                        File.Delete(fileModel.FullPathOfFile);
                        continue;
                    }
                    
                    /*var counter = 1;
                    while (File.Exists(newFileNamePathWithExtension))
                    {
                        possibleNewName = createdName + "_" + counter;
                        newFileNamePath = Path.Combine(destinationPath, possibleNewName);
                        newFileNamePathWithExtension = newFileNamePath + fileModel.GeneralFileInformation.Extension;
                        counter++;
                    }*/
                }
                counter++;
                if(counter >= filterBusinessLogicModel.MaxFiles)
                {
                    break;
                }
                NeedToBeCopied.Add(new Tuple<FileModel, string>(fileModel, newFileNamePathWithExtension));
              
            
            }
        }
        
        ResolveTheList();
        
        return Task.CompletedTask;
    }
    
    public List<Tuple<FileModel, string> >NeedToBeCopied = new List<Tuple<FileModel, string>>();

    public void ResolveTheList()
    {
        int counter1 = 0;
        int counter2 = 0;
        foreach (var copy in NeedToBeCopied)
        {
            Console.WriteLine( "following is about to start: "+counter1);
            LastCheckOfMaybeAlreadyCopiedPart1(copy.Item1, copy.Item2);
            counter1++;
        }
        
        foreach (var copy in NeedToBeCopied)
        {
            Console.WriteLine( "following is about to start: "+counter2);
            LastCheckOfMaybeAlreadyCopiedPart2(copy.Item1, copy.Item2);
            counter2++;
        }
    }
    
    

    public void LastCheckOfMaybeAlreadyCopiedPart1(FileModel fileModel, string newFileNamePathWithExtension)
    {
        try
        {
            CopyFileWithSSh(fileModel.FullPathOfFile, newFileNamePathWithExtension);
            CheckIfFileExistAndDeleteIt(fileModel.FullPathOfFile, newFileNamePathWithExtension);
        }
        catch (Exception e)
        {
           
               
            Console.WriteLine("error on:" +fileModel.FullPathOfFile);
            Console.WriteLine("error on:" +e.Message);
        }
        
                    

    }
    
    public void LastCheckOfMaybeAlreadyCopiedPart2(FileModel fileModel, string newFileNamePathWithExtension)
    { 
        
        if (File.Exists(newFileNamePathWithExtension) &&  !File.Exists(fileModel.FullPathOfFile))
        {
            return;
           
        }
        
        
        if (File.Exists(newFileNamePathWithExtension))
        {
            File.Delete(fileModel.FullPathOfFile);
           
        }
        else
        {
            try
            {
                File.Copy(fileModel.FullPathOfFile, newFileNamePathWithExtension);
                if (File.Exists(newFileNamePathWithExtension))
                {
                    File.Delete(fileModel.FullPathOfFile);
           
                }
                else
                {
                    Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                }
            }catch(Exception e)
            {
                if (File.Exists(newFileNamePathWithExtension))
                {
                    if(GetFileHash(newFileNamePathWithExtension) == fileModel.Hash)
                    {
                        File.Delete(fileModel.FullPathOfFile);
                        return;
                    }
           
                }
               
              
               
                Console.WriteLine("error on:" +fileModel.FullPathOfFile);
            }
        
           
        }     // check if file exists on destination
    }
    
    public string ReplacementForSshPath(string path)
    {
        return path.Replace("Volumes", "volume1").Replace(" ", "\\ ");
    }
    
    public string ReplacementForSshPathVolumnes(string path)
    {
        return path.Replace("Volumes", "volume1");
    }
    
    public void CopyFile(FileModel fileModel, string destinationPath)
    {
        CopyFileWithSSh(fileModel.FullPathOfFile, destinationPath);
    }
    
    public void CopyFileWithSSh(string sourcePath, string destinationPath)
    {
        if (usingSsh == false)
        {
            File.Copy(sourcePath, destinationPath);
            return;
        }

        var file = ReplacementForSshPathVolumnes(sourcePath);
        var destination = ReplacementForSshPathVolumnes(destinationPath);
        
        string command = $"cp \"{file}\" \"{destination}\"";

        var hash = ProxyExecuter(command);
        
        if(hash?.Contains("cp:")== true)
        {
            file = ReplacementForSshPathVolumnes(sourcePath).Replace(" ", "\\ ");
            destination = ReplacementForSshPathVolumnes(destinationPath).Replace(" ", "\\ ");
            
            command = $"cp \"{file}\" \"{destination}\"";

            hash = ProxyExecuter(command);
        }
        
        if(hash?.Contains("cp:")== true)
        {
            file = ReplacementForSshPathVolumnes(sourcePath).Replace(" ", "*");
            destination = ReplacementForSshPathVolumnes(destinationPath).Replace(" ", "*");
            
            command = $"cp {file} {destination}";

            hash = ProxyExecuter(command);
        }
        
        var result = hash;
    }
    
    public void CheckIfFileExistAndDeleteIt(string sourcePath, string destinationPath)
    {
        if (usingSsh == false)
        {
            //File.Copy(sourcePath, destinationPath);
            return;
        }

        var file = ReplacementForSshPath(sourcePath);
        var destination = ReplacementForSshPath(destinationPath);
        var destinationReplacedOnlyBaseRootPath = ReplacementForSshPathVolumnes(destinationPath);
        
        // check if file exists on destination
        //string command = $"ls {destination}";
        
        //var command2 = $"[ -f "+destination+" ] && echo ""file exists"" || echo ""file does not exist"";
        var command = $"[ -f \"{destinationReplacedOnlyBaseRootPath}\" ] && echo \"file exists\" || echo \"file does not exist\"";

        
        var result = ProxyExecuter(command);

        if (!string.IsNullOrEmpty(result) && result.Contains("file exists"))
        {
            // delete file on destination
            command = $"rm \"{file}\"";
            ProxyExecuter(command);
        }


    }

    private string? ProxyExecuter(string command)
    {
        return ExecuteSshCommand("192.168.68.102", "Edward", "Caracas23!", command);
    }

    private string GetFileHash(FileModel fileModel)
    {
        if (usingSsh == false)
        {
            return GetFileHash(fileModel.FullPathOfFile);
        }

        var file = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile);
        
        string command = $"md5sum \"{file}\"";

        var hash = ProxyExecuter(command);
        
        if(hash?.Contains("md5sum: can't open")== true)
        {
            file = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile).Replace(" ", "\\ ");
            
            command = $"md5sum \"{file}\"";

            hash = ProxyExecuter(command);
        }
        
        
        if(hash?.Contains("md5sum: can't open")== true)
        {
            file = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile).Replace(" ", "*");
            
            command = $"md5sum {file}";

            hash = ProxyExecuter(command);
        }
        
        if(hash?.Contains("md5sum: can't open")== true)
        {
            return string.Empty;
        }

        if (!string.IsNullOrEmpty(hash))
        {
            hash = hash?.Split(" ")[0].ToUpper();
        
            
        
            return hash ?? string.Empty;
        }
        return GetFileHash(fileModel.FullPathOfFile);
       
    }
    
    private string GetFileHash(string fullPathOfFile)
    {
        IFileHasher fileHasher = new FileHasher();
        return fileHasher.GetMD5ByFilePath(fullPathOfFile);
    }
}