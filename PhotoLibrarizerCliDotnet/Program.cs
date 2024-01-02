// See https://aka.ms/new-console-template for more information


using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Routines.SimpleRoutines;

IReorderingFiles reorderingFiles = new ReorderingFiles(
    new FilesSeeker(), 
    //"/Users/edwardflores/Pictures/organized",
    "/Volumes/Edward/Gallery",
    Console.WriteLine
    );
var result =reorderingFiles.DoRenameFilesAsync_SimpleDraft("Users/edwardflores/Pictures/organized/2022/11/10", true, true);
//var result =reorderingFiles.DoRenameFilesAsync_SimpleDraft("/Volumes/Edward/Raws/2019/08", true, true);
///Users/edwardflores/Pictures/organized/2022/11/10
//var result =reorderingFiles.DoRenameFilesAsync_SimpleDraft("/Users/edwardflores/Pictures/converted", true);

//var result =reorderingFiles.DoRenameFilesAsync_SimpleDraft("/Volumes/Edward/Gallery/2021/09/14", true);
result.Wait();
Console.WriteLine("Hello, World!");