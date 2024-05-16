using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Routines.SimpleRoutines;

namespace PhotoLibrarizer.Routines.Tests.SimpleRoutines
{
    public class ReorderingFilesTests
    {
        [Test]
        public async Task Test1()
    {
        
        IReorderingFiles reorderingFiles = new ReorderingFiles(
            new FilesSeekerV2(),
            //"/Users/edwardflores/Pictures/Tests/ReorderingFilesFindByPropertiesTests",
            //"/Volumes/Edward/Gallery",
            
            Console.WriteLine
            );


        var filterModel = new FilterModel();
        var destinationModel = new DestinationModel();
        destinationModel.BasePath = "/Volumes/Edward/TakeALookV4";
        //destinationModel.BasePath = "/Users/edwardflores/Pictures/TakeALook";
        destinationModel.Destination = Destinations.CameraBasedDirectoryWithoutDate;
        
        var pathsForSourceFiles = new List<string>();
        //pathsForSourceFiles.Add("/Users/edwardflores/Pictures/NewPics");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2003");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2004");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2005");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2006");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2007");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2008");
        pathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2009");

        filterModel.PathsForSourceFiles = pathsForSourceFiles;

        filterModel.DestinationModel = destinationModel;
        
        var extensions = new List<string>();
        
        extensions.Add("jpg");
        extensions.Add("jpeg");
        extensions.Add("nef");

        filterModel.Extensions = extensions;
        
        //var camerasShouldBe = new List<string>();
        //camerasShouldBe.Add("NIKON");
        
        var camerasShouldNotBe = new List<string>();
        camerasShouldNotBe.Add("NIKON");
        

        //filterModel.CamerasShouldBe = camerasShouldBe;
        filterModel.CamerasShouldNotBe = camerasShouldNotBe;
        filterModel.MaxFiles = 150;
        
        
        // model to json
        //var json = JsonSerializer.Serialize(filterModel);
        //var json = JsonConvert.SerializeObject(filterModel, Formatting.Indented);
        
        // json to io
        //File.WriteAllText("configuration.json", json);

        await reorderingFiles.DoByFileModelAsync(filterModel);
        
        
       /* IReorderingFilesFindByProperties reorderingFilesFindByProperties = new ReorderingFilesFindByPropertiesV1(
            new FilesSeeker(), 
            //"/Users/edwardflores/Pictures/Tests/ReorderingFilesFindByPropertiesTests",
            //"/Volumes/Edward/Gallery",
            "/Volumes/Edward/TakeALook",
            Console.WriteLine
            );
        
        FilterModel filterModel = new FilterModel();
        List<string> cameras = new List<string>();
        List<string> camerasNot = new List<string>();
        //cameras.Add("NIKON D750");
        //cameras.Add("sanyo");
        cameras.Add("s50");
        cameras.Add("ipad");
        cameras.Add("iphone");
        camerasNot.Add("unknown");
        //cameras.Add("NIKON");
        //cameras.Add("blackberry");
        //filterModel.CamerasShouldBe = cameras;
        //filterModel.CamerasShouldNotBe =camerasNot;
        filterModel.MaxFiles = 600;**/
   
         Assert.Pass();
    }
    }
}