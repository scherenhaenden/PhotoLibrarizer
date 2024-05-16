using System.Diagnostics;
using System.Globalization;
using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.Engines.Hashing;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Engines.IoEngines.FilesModelsMapper;
using PhotoLibrarizer.Engines.Metadata;
using PhotoLibrarizer.Engines.Models;
using PhotoLibrarizer.Engines.SSH;
using Renci.SshNet;

namespace PhotoLibrarizer.BusinessLogic.Services.Ordering;

public class OrderingFilesV1: IOrderingFilesV1
{
    
    IFilesSeekerV2 _filesSeekerV2;

    public OrderingFilesV1()
    {
        _filesSeekerV2 = new FilesSeekerV2();
    
    }
    
    bool _usingSsh = false;
    
    
    SshClient? _sshClient;
    ISshServices? _sshServices;

    public Task MultiThreadedOrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh = false)
    {
        
        // get subdirectories from filterBusinessLogicModel.PathsForSourceFiles not recursive
        //var subDirectories = Path.Combine(filterBusinessLogicModel.PathsForSourceFiles[0], "subDirectories"); 
            //_filesSeekerV2.GetSubDirectories(filterBusinessLogicModel.PathsForSourceFiles, false);
            
            var listOfThreads = new List<Thread>();
            
        
        
        filterBusinessLogicModel.PathsForSourceFiles.ForEach(x =>
        {
            var filter = new FilterBusinessLogicModel()
            {
                PathsForSourceFiles = new List<string>() {x},
                Extensions = filterBusinessLogicModel.Extensions,
                DestinationModel = filterBusinessLogicModel.DestinationModel,
                MaxFiles = filterBusinessLogicModel.MaxFiles
            };
            
            ;
            //listOfThreads.Add(new Thread(() => Task.Run(async () => await OrderFiles(filter, useSsh))));
            listOfThreads.Add(new Thread(()  => OrderFiles(filter, useSsh).Wait()));
            
            
            //listOfTasks.Add(OrderFiles(filter, useSsh));
            //OrderFiles(filter, useSsh);
        });
        
        //listOfTasks.ForEach(x => x.Start());
        //Parallel.ForEach(listOfTasks, x => x.Start());
        
        // run all in parallel
        //Task.WhenAll(listOfTasks);
        
        // start all thread and wait for all to finish
        listOfThreads.ForEach(x => x.Start());
        //listOfThreads.ForEach(x => x.Join());
        
        // Wait for threads to finish
        while (listOfThreads.Any(t => t.IsAlive)) 
        {
            Thread.Sleep(800); // Avoid a busy waiting loop
        }
        //  var gh = listOfThreads.Where(x => x.IsAlive).ToList();
        
        
        return Task.CompletedTask;
        
        
        //throw new NotImplementedException();
    }
    
    
    
    public static void DeleteEmptySubdirectories(string parentDirectory){
        Parallel.ForEach(Directory.GetDirectories(parentDirectory), directory => {
            DeleteEmptySubdirectories(directory);
            
            var result =Directory.EnumerateFileSystemEntries(directory).Count() + Directory.EnumerateDirectories(directory).Count() + Directory.EnumerateFiles(directory).Count();
            
            if(result == 0)
            {
                try
                {
                    bool subDirectory = true;
                    
                    var files =Directory
                        .GetFiles(directory, "*.*",
                            subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    
                    var directories =Directory
                        .GetDirectories(directory, "*.*",
                            subDirectory ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    
                    var counter = files.Count() + directories.Count();
                    
                    if(counter == 1)
                    {
                        var fileName = Path.GetFileName(files[0]);
                        if(fileName.ToLower()== ".DS_store".ToLower())
                        {
                            File.Delete(files[0]);
                            DeleteEmptySubdirectories(directory);
                        }
                
                    }
            
                    if (counter == 0)
                    {
                        try
                        {
                            Directory.Delete(directory, false);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error on:" +directory);
                            Console.WriteLine("error on:" +e.Message);
                        }
                
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error on:" +directory);
                    Console.WriteLine("error on:" +e.Message);
                }
            }
           

            
        });   
    }
    
    static string fileNameNoDateAndPath = "../../../nodate.txt";
    static List<string>? nodateFiles = new List<string>();
    static List<string>  filesInNoDates = new List<string>();
    
    private static bool IsNameinFile(string fileName)
    {
        // get all files with no date name
        // get all files
        lock (nodateFiles)
        {
            lock (filesInNoDates)
            {
                     if(nodateFiles.Count() == 0 && filesInNoDates.Count() == 0)
            {
                var pathOfNoDate = Path.GetFullPath(fileNameNoDateAndPath);
                // get directory of file
                var directory = Path.GetDirectoryName(pathOfNoDate);
            
            
            
                if(nodateFiles == null || nodateFiles?.Count() == 0)
                {
                    FilesSeekerV2 _filesSeekerV = new FilesSeekerV2();
            
            
            
                    var files = _filesSeekerV.GetFilesInPath(directory, new List<string>() {".txt"});
                    nodateFiles = new List<string>();
                    nodateFiles = files.Where(x => x.Contains("nodate")).ToList();
                    foreach (var file in nodateFiles)
                    {
            
                
                        var lines = File.ReadAllLines(file);
                        filesInNoDates.AddRange(lines);
                    }
                    filesInNoDates = filesInNoDates.Distinct().ToList();
                }
            }
            }
            
            
       

        }
        
        
        
        
  
        var result =  filesInNoDates.Any(x => x.Contains(fileName));
        return result;
       
        
        /*if(File.Exists(fileNameNoDate))
        {
            var reader = new StreamReader(fileNameNoDate);
            var lines = File.ReadAllLines(fileNameNoDate);
            reader.Close();
            if(lines.Contains(fileName))
            {
                return true;
            }
        }*/
        
        return false;
    }
    
    private static void WriteNoDate(string fileWithDate) 
    {
              
        if(File.Exists(fileNameNoDateAndPath))
        {
            //File.Create(fileNameNoDate);
            // fine out how many lines does the file have
            var reader = new StreamReader(fileNameNoDateAndPath);
            var lines = File.ReadAllLines(fileNameNoDateAndPath);
            reader.Close();
            if(lines.Length > 10000000)
            {
                // delete from list all the paths found in the file
                //files = files.Where(x => !lines.Contains(x)).ToList();
                //File.Delete(fileNameNoDate);
                
                var directory = Path.GetDirectoryName(fileNameNoDateAndPath);
                
                
                var newFileName = "nodate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                var newfileNameNoDateAndPath = Path.Combine(directory, newFileName);
                File.Move(fileNameNoDateAndPath, newfileNameNoDateAndPath);
            }
                    
        }

      
        lock (fileNameNoDateAndPath)
        {
            if (!File.Exists(fileNameNoDateAndPath))
            {
                File.Create(fileNameNoDateAndPath);
            }
            File.AppendAllText(fileNameNoDateAndPath, fileWithDate + Environment.NewLine);
        }
                
        //File.AppendAllText(fileNameNoDate, fileWithDate + Environment.NewLine);
    }
    
    private ISshServices? LoadSshervices()
    {
        if(_sshServices == null)
        {
            _sshServices = new SshServices("192.168.68.102", "Edward", "Caracas23!");
        }
        return _sshServices;
    }

    private DateTime? HandleGetDateFromFile(FileModel fileModel, bool useSsh = false)
    {
        if (useSsh)
        {
            //var  possibleRemoveNameOfFile = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile);
            var result = GetDateFromFileViaSsh(fileModel);
            if (result is not null)
            {
                return result;
            }
        }
        
        IMetadataManager metadataManager = new MetadataManager(fileModel.FullPathOfFile, fileModel.Directories);
        var currentDateOfFile = metadataManager.GetDateOfMediaCreation();
        fileModel.Directories = metadataManager.Directories;
            
        
        return currentDateOfFile;
    }

    public DateTime? GetDateFromFileViaSsh(FileModel fileModel)
    {

        try
        {
            var  possibleRemoveNameOfFile = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile);
        
            string commandi = $"exiftool \"{fileModel.CorrectBashFullFileName}\"";
            var hashi= _sshServices.ExecuteCommand(commandi, out string error);
            if (string.IsNullOrEmpty(error))
            {
                //return hashi?.Split(" ")[0].ToUpper() ?? string.Empty;
                var lines = hashi.Split("\n");

                try
                { 
                
            
                    var createDateUnClean = lines.FirstOrDefault(x => x.Contains("Create Date"));
                
                    var createDate = createDateUnClean.Split(':', 2)[1].Trim();
            
                    string format = "yyyy:MM:dd HH:mm:ss";
                    DateTime dtObject = DateTime.ParseExact(createDate,format,CultureInfo.InvariantCulture);
                    return dtObject;
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                    Console.WriteLine("error on:" +e.Message);
                }
            
                try
                {
                    var createDateUnClean = lines.FirstOrDefault(x => x.Contains("Date/Time Original"));
                
                    var createDate = createDateUnClean.Split(':', 2)[1].Trim();
            
                    string format = "yyyy:MM:dd HH:mm:ss";
                    DateTime dtObject = DateTime.ParseExact(createDate,format,CultureInfo.InvariantCulture);
                    return dtObject;
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                    Console.WriteLine("error on:" +e.Message);
                }
            
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("error on:" +fileModel.FullPathOfFile);
            Console.WriteLine("error on:" +e.Message);
        }



        return null;

    }
    
    private string HandleCreateNameOfDestinationDirectory(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel)
    {
        IDestinationCreatorService destinationCreatorService = new DestinationCreatorService();
        var kindOfDestination = filterBusinessLogicModel.DestinationModel.DestinationPathDirectory.PathDestination;
                
        if (string.IsNullOrEmpty(kindOfDestination))
        {
            kindOfDestination = destinationCreatorService.CreateDestinationDirectory(filterBusinessLogicModel, fileModel);
        }
        
        // create destination path string
        var destinationPathWithoutRoot = "/" + kindOfDestination;//currentDateOfFile.Value.ToString(kindOfDestination).Replace(".", "/");
                    
        //var destinationPath = Path.Combine(filterBusinessLogicModel.DestinationModel.BasePath, destinationPathWithoutRoot);

        var destinationPath =
            filterBusinessLogicModel.DestinationModel.BasePath + destinationPathWithoutRoot;
        
        return destinationPath;
    }
    

    public Task OrderFiles(FilterBusinessLogicModel filterBusinessLogicModel, bool useSsh=false)
    {
        _usingSsh = useSsh;
        var filePath = "not_to_use"; 
        if (_usingSsh)
        {
            LoadSshervices();
        }
        
        var needToBeCopied = new List<Tuple<FileModel, string>>();
        
        // get list of alll empty subdirectories
        //var directories = _filesSeekerV2.GetSubDirectories(filterBusinessLogicModel.PathsForSourceFiles, true);
        filterBusinessLogicModel.PathsForSourceFiles.ForEach(x => DeleteEmptySubdirectories(x));
        
        
        

        List<string> files = _filesSeekerV2
            .GetFilesInPath(filterBusinessLogicModel.PathsForSourceFiles, filterBusinessLogicModel.Extensions)
            .Where(x => !x.ToLower().Contains("Screen".ToLower())).ToList();
        
        //files = files.Where(x => !x.Contains("DS_Store")).ToList();

        var countBeforeFiltering = files.Count();
        
        files = files.Where(x => IsNameinFile(x) == false).ToList();

        var countAfterFiltering = files.Count();
        
        // get all lines of file if file exits
        if(File.Exists(filePath)) 
        {
            var reader = new StreamReader(filePath);
           // read all lines
           var lines = File.ReadAllLines(filePath);
           if(lines.Length > 0)
           {
               // delete from list all the paths found in the file
               files = files.Where(x => !lines.Contains(x)).ToList();
           }
           reader.Close();
           
        }
        else
        {
            // create file if not exists
            File.Create(filePath);
        }
        
        
        var writer = new StreamWriter(filePath);
        
        var uniqueExtensions = files
            .Select(Path.GetExtension) // Extract the extension from each path
            .Distinct() // Remove duplicates
            .Where(extension => !string.IsNullOrEmpty(extension)) // Optional: Remove empty strings if there are paths with no extension
            .ToList();
        
        IFileModelsMapper fileModelsMapper = new FileModelsMapper();

        var fileModels = fileModelsMapper.MapPaths(files) /*.Take(filterBusinessLogicModel.MaxFiles ?? 10000)*/
            .ToList();
            // bigger than 30Kb
            //.Where(x=>x.GeneralFileInformation.Length > 30000).ToList();*/
            
            
            
        
        int counter = 0;
        int counterUnderMark = 0;
        int counterUnderExistsOnTarget = 0;
        int counterUnderNoDate = 0;
        int counterTotal = 0;
        // for now only for dates
        
        // start timer right here
        var timer = new Stopwatch();
        
        timer.Start();
        
        
        foreach (var fileModel in fileModels)
        {
            counterTotal++;
            /*if(fileModel.GeneralFileInformation.Length < 30000)
            {
                writer.WriteLine(fileModel.FullPathOfFile);
                writer.Flush();
                counterUnderMark++;
                continue;
            }*/
            
            /*if(IsNameinFile(fileModel.FileName))
            {
                counterUnderMark++;
                continue;
            }*/
            
            
            // get date from file
            if (!PrepareInformationToDoActions(filterBusinessLogicModel, fileModel, writer, needToBeCopied,
                    ref counterUnderNoDate, ref counter, ref counterUnderExistsOnTarget))
                break;
            
            // calculate average time
            var timeTillNow = timer.ElapsedMilliseconds;
            
            // calculate average time
            var averageTime = timeTillNow / counterTotal;
            
            // calculate time left
            var timeLeft = averageTime * (files.Count - counterTotal);
            
            // calculate time left in minutes and seconds
            var timeLeftMinutes = timeLeft / 60000;
            
            // calculate time left in minutes and seconds timespan
            var timeLeftSeconds = TimeSpan.FromMilliseconds(timeLeft);
        }
        
        ResolveTheList(needToBeCopied);
        writer.Close();
        filterBusinessLogicModel.PathsForSourceFiles.ForEach(x => DeleteEmptySubdirectories(x));
        
        return Task.CompletedTask;
    }

    private bool PrepareInformationToDoActions(FilterBusinessLogicModel filterBusinessLogicModel, FileModel fileModel,
        StreamWriter writer, List<Tuple<FileModel, string>> needToBeCopied, ref int counterUnderNoDate, ref int counter,
        ref int counterUnderExistsOnTarget)
    {
        
        if (_usingSsh)
        {
            var  possibleRemoveNameOfFile = ReplacementForSshPathVolumnes(fileModel.FullPathOfFile);
            var result = _sshServices.GetCorrectNameOfFile(possibleRemoveNameOfFile, out string error);
                    
            if (string.IsNullOrEmpty(error))
            {
                fileModel.CorrectBashFullFileName = result.Replace("\n", "");
            }
                
        }
        
        var currentDateOfFile = HandleGetDateFromFile(fileModel, _usingSsh);
        
        fileModel.DateCreation = currentDateOfFile;
            
        if(currentDateOfFile is null)
        {
            counterUnderNoDate++;
            writer.WriteLine(fileModel.FullPathOfFile);
            writer.Flush();
            Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                
            WriteNoDate(fileModel.FullPathOfFile);
            // Create file nodate.txt if it does not exist
            return true;
        }

            
        Console.WriteLine( "following is about to start: "+counter);

       
           
        if (currentDateOfFile is not null)
        {
            // create method in here

            var destinationPath = HandleCreateNameOfDestinationDirectory(filterBusinessLogicModel, fileModel);
                
            // create directory if not exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
                
            // create file name string
            var createdName = string.Empty;
               
                
            fileModel.Hash = GetFileHash(fileModel);

            if (string.IsNullOrEmpty(fileModel.Hash))
            {
                Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                return true;
            }
                
            IDestinationCreatorService destinationCreatorService = new DestinationCreatorService();
            createdName = destinationCreatorService.CreateDestinationFile(filterBusinessLogicModel.DestinationModel.NamingPatternFiles.FileNameCreationBusinessLogicEnums, fileModel, currentDateOfFile);
                
            var possibleNewName = createdName;
                
            var newFileNamePath = Path.Combine(destinationPath, possibleNewName);
                
            var newFileNamePathWithExtension = newFileNamePath + fileModel.GeneralFileInformation.Extension;
                
            if (File.Exists(newFileNamePathWithExtension))
            {
                if(GetFileHashSsh(newFileNamePathWithExtension) == fileModel.Hash)
                {
                    try
                    {
                        File.Delete(fileModel.FullPathOfFile);
                            
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error on:" +fileModel.FullPathOfFile);
                        Console.WriteLine("error on:" +e.Message);
                    }
                    counterUnderExistsOnTarget++;
                    return true;
                }
            }
            counter++;
            if(counter >= filterBusinessLogicModel.MaxFiles)
            {
                return false;
            }
                
            if(counter%30 ==0) // for testing
            {
                ResolveTheList(needToBeCopied);
                needToBeCopied.Clear();
            }
                
            needToBeCopied.Add(new Tuple<FileModel, string>(fileModel, newFileNamePathWithExtension));
        }

        return true;
    }

    //public List<Tuple<FileModel, string> >NeedToBeCopied = new List<Tuple<FileModel, string>>();
    
    public void ResolveTheList(List<Tuple<FileModel, string> >needToBeCopied)
    {
        int counter1 = 0;
        int counter2 = 0;
        foreach (var copy in needToBeCopied)
        {
            Console.WriteLine( "following is about to start: "+counter1);
            LastCheckOfMaybeAlreadyCopiedPart1(copy.Item1, copy.Item2);
            counter1++;
        }
        
        var normal = needToBeCopied.Where(x => x.Item1.DeletedFromTheSource == false).ToList();
        
        foreach (var copy in normal)
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
            //var name = fileModel.CorrectBashFullFileName;
            if(!string.IsNullOrEmpty(fileModel.CorrectBashFullFileName))
            {
                CopyFileWithSSh(fileModel, newFileNamePathWithExtension);
                CheckIfFileExistAndDeleteIt(fileModel, newFileNamePathWithExtension);
            }
            else
            {
                CopyFileWithSSh(fileModel, newFileNamePathWithExtension);
                CheckIfFileExistAndDeleteIt(fileModel, newFileNamePathWithExtension);
            }
            
           
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
                    if(GetFileHashDirectlyDotnet(newFileNamePathWithExtension) == fileModel.Hash)
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
        return path.Replace("/Volumes", "/volume1");
    }
    
    public void CopyFile(FileModel fileModel, string destinationPath)
    {
        CopyFileWithSSh(fileModel, destinationPath);
    }
    
    public void CopyFileWithSSh(FileModel fileModel, string destinationPath)
    {
        string sourcePath = fileModel.FullPathOfFile;
        
        if (_usingSsh == false)
        {
            File.Copy(sourcePath, destinationPath);
            fileModel.CopiedToDestination = true;
            return;
        }
        
        if (!string.IsNullOrEmpty(fileModel.CorrectBashFullFileName))
        {
            // get full path of file
            var destination2 = Directory.GetParent(destinationPath).FullName;
            

            var  destinationSshProbablyRight = ReplacementForSshPathVolumnes(destination2);
            
            var sshDestination = _sshServices.GetCorrectNameOfDirectory(destinationSshProbablyRight, out string error2);
            
            
            //get name of file from path
            var fileName = Path.GetFileName(destinationPath);
            //sshDestination = Path.Combine(sshDestination, fileName);
            sshDestination = sshDestination + "/" + fileName;

            
            //CopyFileWithSSh(fileModel.CorrectBashFullFileName, destinationPath);
            string command2 = $"cp \"{fileModel.CorrectBashFullFileName}\" \"{sshDestination}\"";
            
            var result2 = _sshServices.ExecuteCommand(command2, out error2);
            
            if (string.IsNullOrEmpty(error2))
            {
                fileModel.CorrectBashFullFileNameDestination = sshDestination;
                //CopyFileWithSSh(fileModel.CorrectBashFullFileName, destinationPath);
                //command2 = $"cp \"{fileModel.CorrectBashFullFileName.Replace(" ", "\\ ")}\" \"{destination2.Replace(" ", "\\ ")}\"";
            
                //result2 = sshServices.ExecuteCommand(command2, out error2);
                fileModel.CopiedToDestination = true;
                return;
            }
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
    
    public void CheckIfFileExistAndDeleteIt(FileModel fileModel, string destinationPath)
    {
        if (_usingSsh == false)
        {
            //File.Copy(sourcePath, destinationPath);
            return;
        }
        
        if(fileModel.CorrectBashFullFileNameDestination != string.Empty)
        {
            // delete file on destination
            
            
            var command2 = $"[ -f \"{fileModel.CorrectBashFullFileNameDestination}\" ] && echo \"file exists\" || echo \"file does not exist\"";

        
            var result2 = _sshServices.ExecuteCommand(command2, out string error2);

            if (string.IsNullOrEmpty(error2) && result2.Contains("file exists"))
            {
                // delete file on destination
                fileModel.CheckedIfCopied = true;
                command2 = $"rm \"{fileModel.CorrectBashFullFileName}\"";
                
                
                var resul = _sshServices.ExecuteCommand(command2, out error2);
                if (string.IsNullOrEmpty(error2))
                {
                    fileModel.DeletedFromTheSource = true;
                }
            }
            
            return;
        }
        
        string sourcePath = fileModel.FullPathOfFile;

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
            fileModel.DeletedFromTheSource = true;
            
        }


    }

    private string? ProxyExecuter(string command)
    {
        if(_sshServices is null)
        {
            _sshServices = new SshServices("192.168.68.102", "Edward", "Caracas23!");
        }
        
        var result =  _sshServices.ExecuteCommand(command, out string error);
        
        if (!string.IsNullOrEmpty(error))
        {
            Console.WriteLine(error);
            return null;
        }
        
        return result;
    }

    private string GetFileHashSsh(string fullPathOfFile, bool userReplacement = true)
    {
        
        if(_usingSsh == false)
        {
            return GetFileHashDirectlyDotnet(fullPathOfFile);
        }
        
        var fileName = fullPathOfFile;
        if(userReplacement)
        {
            fileName = ReplacementForSshPathVolumnes(fileName);
        }
        
        
        string commandi = $"md5sum \"{fileName}\"";
        var hashi= _sshServices.ExecuteCommand(commandi, out string error);
        if (string.IsNullOrEmpty(error))
        {
            return hashi?.Split(" ")[0].ToUpper() ?? string.Empty;
        }
        
        fileName = fileName.Replace(" ", "*");
        commandi = $"md5sum \"{fileName}\"";
        hashi= _sshServices.ExecuteCommand(commandi, out string error2);
        if (string.IsNullOrEmpty(error2))
        {
            return hashi?.Split(" ")[0].ToUpper() ?? string.Empty;
        }


        return GetFileHashDirectlyDotnet(fullPathOfFile);
    }

    private string GetFileHash(FileModel fileModel)
    {
        if (_usingSsh == false)
        {
            return GetFileHashDirectlyDotnet(fileModel.FullPathOfFile);
        }

        if (!string.IsNullOrEmpty(fileModel.CorrectBashFullFileName))
        {
            string commandi = $"md5sum \"{fileModel.CorrectBashFullFileName}\"";
            var hashi= _sshServices.ExecuteCommand(commandi, out string error);
            if (string.IsNullOrEmpty(error))
            {
                return hashi?.Split(" ")[0].ToUpper() ?? string.Empty;
            }
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
            
            command = $"ls {file}";
            
            var fileName = ProxyExecuter(command);
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName =fileName.Replace("\n", "");

                var dotnetNameLength = fileModel.FullPathOfFile.Length;
                var correctBashFullFileNameLength = fileName.Length;
                
                if(dotnetNameLength == correctBashFullFileNameLength)
                {
                    //fileName = fileName.Substring(dotnetNameLength);
                    fileModel.CorrectBashFullFileName = fileName;
                }
                else
                {
                    Console.WriteLine("error on file name:" + fileModel.FullPathOfFile);
                }
                
                
            }
            else
            {
                return GetFileHashDirectlyDotnet(fileModel.FullPathOfFile);
            }
            
            
            command = $"md5sum {file}";

            hash = ProxyExecuter(command);
        }
        
        if(hash?.Contains("md5sum: can't open")== true)
        {
            return GetFileHashDirectlyDotnet(fileModel.FullPathOfFile);
        }

        if (!string.IsNullOrEmpty(hash))
        {
            hash = hash?.Split(" ")[0].ToUpper();
        
            
        
            return hash ?? string.Empty;
        }
        return GetFileHashDirectlyDotnet(fileModel.FullPathOfFile);
       
    }
    
    private string GetFileHashDirectlyDotnet(string fullPathOfFile)
    {
        IFileHasher fileHasher = new FileHasherDotnet();
        return fileHasher.GetMD5ByFilePath(fullPathOfFile);
    }
}