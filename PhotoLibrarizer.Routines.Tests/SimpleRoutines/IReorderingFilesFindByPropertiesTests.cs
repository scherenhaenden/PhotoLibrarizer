using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizer.Engines.IoEngines;
using PhotoLibrarizer.Routines.SimpleRoutines;

namespace PhotoLibrarizer.Routines.Tests.SimpleRoutines
{
    public class IReorderingFilesFindByPropertiesTests
    {
        [Test]
        public async Task Test1()
        {
        
            IReorderingFilesFindByProperties reorderingFilesFindByProperties = new ReorderingFilesFindByPropertiesV1(
                new FilesSeeker(), 
                //"/Users/edwardflores/Pictures/Tests/ReorderingFilesFindByPropertiesTests",
                //"/Volumes/Edward/Gallery",
                "/Volumes/Edward/TakeALookV4",
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
            filterModel.MaxFiles = 600;
        
            //await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/TakeALook/S50 ", filterModel, "true");
            await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GalleryOld/2012", filterModel,true ,"true");
            /*await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GalleryOld/2007", filterModel, "true");
            await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GalleryOld/2008", filterModel, "true");
            await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GalleryOld/2009", filterModel, "true");
            await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GunalleryOld/2010", filterModel, "true");
            await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/GalleryOld/2011", filterModel, "true");*/
        
            //await reorderingFilesFindByProperties.DoRenameFilesAsync("/Volumes/Edward/Swap/130ND750", filterModel, "true");


            Assert.Pass();
        }
    }
}