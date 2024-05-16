using PhotoLibrarizerCore.Services.Hash;

namespace PhotoLibrarizerCore.Services.FilesManagement.Models
{
    public class FileModelDeleteDuplicates
    {
        private readonly IHashFileGenerator _hashFileGenerator;

        public FileModelDeleteDuplicates(IHashFileGenerator hashFileGenerator)
        {
            this._hashFileGenerator = hashFileGenerator;
        }

        private bool StreamsContentsAreEqual(string pathtoFile, string pathToFile2)
        {

            FileStream fsSource1 = new FileStream(pathtoFile,
                FileMode.Open, FileAccess.Read);
                
            FileStream fsSource2 = new FileStream(pathtoFile,
                FileMode.Open, FileAccess.Read);

            return StreamsContentsAreEqual(fsSource1, fsSource2);
        }


        private bool StreamsContentsAreEqual(Stream stream1, Stream stream2)
    {
        const int bufferSize = 2048 * 2;
        var buffer1 = new byte[bufferSize];
        var buffer2 = new byte[bufferSize];

        while (true)
        {
            int count1 = stream1.Read(buffer1, 0, bufferSize);
            int count2 = stream2.Read(buffer2, 0, bufferSize);

            if (count1 != count2)
            {
                return false;
            }

            if (count1 == 0)
            {
                return true;
            }

            int iterations = (int)Math.Ceiling((double)count1 / sizeof(Int64));
            for (int i = 0; i < iterations; i++)
            {
                if (BitConverter.ToInt64(buffer1, i * sizeof(Int64)) != BitConverter.ToInt64(buffer2, i * sizeof(Int64)))
                {
                    return false;
                }
            }
        }
    }


        private static List<FileModel> _allFilesGlobal = new List<FileModel>();
        private int _totalDone;
        
        public IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize=30)  
    {        
        for (int i = 0; i < locations.Count; i += nSize) 
        { 
            yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i)); 
        }  
    }

        public List<FileModel> Run(List<FileModel> allFiles)
    {
        _allFilesGlobal = allFiles;
        var totalDupeItems = _allFilesGlobal.GroupBy (x => x.GeneralFileInformation.Length).Where (x => x.Skip (1).Any ()).ToList();
        Console.WriteLine("totalDupeItems: not yet hashed" + totalDupeItems.Count);
        _totalDone = totalDupeItems.Count;
        List<Task> tasks = new List<Task>();


        foreach(var groupd in totalDupeItems)
        {
            // 
            /*Task t2 = new Task(delegate ()
                
                 Doings(groupd);
                });*/                
            var t2=new Task(() => Doings(groupd));
                
            tasks.Add(t2);
                
                
            /*tasks[0].Start();
                tasks[0].Start();


                while (tasks.Any(t => !t.IsCompleted)) { } */
                
            // 
            /*var h=groupd.Select(y => y).ToList().Count;
                if (h == 2)
                
                 var names = groupd.Select(x => x.FullPathOfFile).ToList();
                    var result =StreamsContentsAreEqual(names[0], names[1]);
                    if (!result)
                    
                     totalDone = totalDone - 1;
                        Console.WriteLine("Rest: " + totalDone);
                        continue;
                    
                     

                 var resultwithHash=EachGroupGetHash (groupd);
                var ListToDelete= DeleteDuplicates(resultwithHash);
                foreach (var eachHashedFile in ListToDelete) 
                
                 File.Delete (eachHashedFile.FullPathOfFile);
                    allFilesGlobal.RemoveAll (x => x.FullPathOfFile == eachHashedFile.FullPathOfFile);
                


                 totalDone = totalDone - 1;
                Console.WriteLine("Rest: " + totalDone);*/        }
            
            
        var listsOfListsOfTasks = SplitList(tasks, 30).ToList();
        List<Thread> listOfThreads = new List<Thread>();
        var awaiterPivotCounter = 4;
        var counter = 0;
            
        foreach (var listOfTasks in listsOfListsOfTasks)
        {
            listOfThreads.Add(new Thread(delegate()
            {
                ResolveTasks(listOfTasks);

            }));
            counter++;
            if (counter >= awaiterPivotCounter || counter >= listsOfListsOfTasks.Count)
            {
                ResolveThreads(listOfThreads);
                while (listOfThreads.Any(t => (t.ThreadState == ThreadState.Running))) { }
                listOfThreads = new List<Thread>();
                counter = 0;
            }
        }

        if (listOfThreads.Count > 0)
        {
            ResolveThreads(listOfThreads);
            while (listOfThreads.Any(t => (t.ThreadState == ThreadState.Running))) { }
        }

        return _allFilesGlobal;            
    }
        
        public void ResolveThreads(List<Thread> threadsList)
    {
        foreach (var thread in threadsList)
        {
            thread.Start();
        }
            
        while (threadsList.Any(t => (t.ThreadState == ThreadState.Running))) { }
    }

        public void ResolveTasks(List<Task> tasksList)
    {
        foreach (var thread in tasksList)
        {
            thread.Start();
        }
            
        while (tasksList.Any(t => !t.IsCompleted)) { }
    }



        public void Doings(IGrouping<long, FileModel> groupd )
    {

        var resultwithHash=EachGroupGetHash (groupd);
        var listToDelete= DeleteDuplicates(resultwithHash);
        foreach (var eachHashedFile in listToDelete) 
        {
            File.Delete (eachHashedFile.FullPathOfFile);
            DoRemovall(eachHashedFile);
        }
                

        _totalDone = _totalDone - 1;
        Console.WriteLine("Rest: " + _totalDone);
            
    }

        public static void DoRemovall(FileModel eachHashedFile)
    {
        _allFilesGlobal.RemoveAll (x => x.FullPathOfFile == eachHashedFile.FullPathOfFile);
    }

        public List<FileModel> EachGroupGetHash(IGrouping<long,FileModel> igr)
    {
        List<FileModel> files = new List<FileModel> ();
        foreach (var gr in igr) 
        {
            gr.Hash = _hashFileGenerator.GetHashByFilePath (gr.FullPathOfFile);
            files.Add (gr);
        }
        return files;

    }
        public List<FileModel> DeleteDuplicates(List<FileModel> eachGroupGetHash)
    {
        string pivotValue = eachGroupGetHash [0].FullPathOfFile;
        List<FileModel> deletable = new List<FileModel> ();

        var notSame=eachGroupGetHash.Where (x => x.FullPathOfFile != pivotValue).ToList ();

        foreach (var file in notSame) 
        {
            if (file.FullPathOfFile != pivotValue && file.Hash == eachGroupGetHash [0].Hash) 
            {
                deletable.Add (file);
            }
        }
        return deletable;
    }
    }
}