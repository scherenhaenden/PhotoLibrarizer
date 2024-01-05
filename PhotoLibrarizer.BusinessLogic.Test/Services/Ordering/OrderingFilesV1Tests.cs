using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;
using PhotoLibrarizer.BusinessLogic.Services.Ordering;
using PhotoLibrarizer.Engines.Filters.Models;

namespace PhotoLibrarizer.BusinessLogic.Test.Services.Ordering;

public class OrderingFilesV1Tests
{
    [Test]
    public async Task Test1()
    {
        FilterBusinessLogicModel filterBusinessLogicModel = new FilterBusinessLogicModel();
        
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/30");
        filterBusinessLogicModel.DestinationModel.BasePath = "/Users/edwardflores/Pictures/test";
        
        var destinationModel = new DestinationBusinessLogicModel();
        
        
        destinationModel.BasePath = "/Users/edwardflores/Pictures/test";
        destinationModel.Destination = DestinationsBusinessLogicEnum.BaseLibraryWithDate;
        
        // for directory  /YYYY/MM/DD
        // for files  YYYY/MM/DD/HH/MM/SS/MMM/Size/Hash
        
        //I//DestinationPathModel destinationPathModel = new DestinationPathModelCustomPattern("/yyyy/MM/DD");


        const string directoryPattern = "/yyyy/MM/dd";
        
        //IDestinationPathBusinessLogicModel destinationPathCustomPatternBusinessLogicModel = new DestinationPathCustomPatternBusinessLogicModel(directoryPattern);
        
        
        IDestinationPathBusinessLogicModel destinationPathCustomPatternBusinessLogicModel = new DestinationPathCustomPatternBusinessLogicModel(null);
        
        destinationPathCustomPatternBusinessLogicModel.DirectoryPathCreationBusinessLogicEnums = new List<DirectoryPathCreationBusinessLogicEnum>()
        {
            DirectoryPathCreationBusinessLogicEnum.MetaCameraModel,
            DirectoryPathCreationBusinessLogicEnum.Separator,
            DirectoryPathCreationBusinessLogicEnum.Year,
        };
  

        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV1 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
        });
        
        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV2 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hash,
        });
        destinationModel.DestinationPathDirectory = destinationPathCustomPatternBusinessLogicModel;
        destinationModel.NamingPatternFiles = destinationNamingBusinessLogicModelV1;
        
        destinationModel.NamingPatternFilesV2 = new List<IDestinationNamingBusinessLogicModel>()
        {
            destinationNamingBusinessLogicModelV1,
            destinationNamingBusinessLogicModelV2
        };
        
        filterBusinessLogicModel.DestinationModel = destinationModel;
        
        
        
        filterBusinessLogicModel.MaxFiles = 10;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        await orderingFilesV1.OrderFiles(filterBusinessLogicModel);
        
        Assert.Pass();
    }
    
    
    
    [Test]
    public async Task Test2_ThisMovesReallyToDirectory()
    {
        FilterBusinessLogicModel filterBusinessLogicModel = new FilterBusinessLogicModel();
        
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/30");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/test");
        filterBusinessLogicModel.DestinationModel.BasePath = "/Volumes/Edward/Gallery";
        
        var destinationModel = new DestinationBusinessLogicModel();
        
        
        destinationModel.BasePath = "/Volumes/Edward/Gallery";
        destinationModel.Destination = DestinationsBusinessLogicEnum.BaseLibraryWithDate;
        
        IDestinationPathBusinessLogicModel destinationPathCustomPatternBusinessLogicModel = new DestinationPathCustomPatternBusinessLogicModel(null);
        
        destinationPathCustomPatternBusinessLogicModel.DirectoryPathCreationBusinessLogicEnums = new List<DirectoryPathCreationBusinessLogicEnum>()
        {
            DirectoryPathCreationBusinessLogicEnum.Year,
            DirectoryPathCreationBusinessLogicEnum.Separator,
            DirectoryPathCreationBusinessLogicEnum.Month,
            DirectoryPathCreationBusinessLogicEnum.Separator,
            DirectoryPathCreationBusinessLogicEnum.Day,
        };
  

        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV1 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
        });
        
        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV2 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hash,
        });
        destinationModel.DestinationPathDirectory = destinationPathCustomPatternBusinessLogicModel;
        destinationModel.NamingPatternFiles = destinationNamingBusinessLogicModelV1;
        
        destinationModel.NamingPatternFilesV2 = new List<IDestinationNamingBusinessLogicModel>()
        {
            destinationNamingBusinessLogicModelV1,
            destinationNamingBusinessLogicModelV2
        };
        
        filterBusinessLogicModel.DestinationModel = destinationModel;
        
        
        
        filterBusinessLogicModel.MaxFiles = 1000;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        await orderingFilesV1.OrderFiles(filterBusinessLogicModel);
        
        Assert.Pass();
    }
    
    
    [Test]
    public async Task Test3_ThisByCameraToDirectory()
    {
        FilterBusinessLogicModel filterBusinessLogicModel = new FilterBusinessLogicModel();
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2013");
  
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld2");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2011");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2012");
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/HuaweiCloud");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/AUseThisOne/100ANDRO");*/
        
        
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/TakeALook");*/
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/TakeALookV2");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/TakeALookV3");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/TakeALookV4");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/TakeALookV5");*/
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/HuaweiCloud");*/
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/warn");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld2");
        //filterBusinessLogicModel.DestinationModel.BasePath = "/Volumes/Edward/ByCameras";
        
        var destinationModel = new DestinationBusinessLogicModel();
        
        destinationModel.BasePath = "/Volumes/Edward/ByCameras";
        ///volume1/Edward/ByCameras
        destinationModel.Destination = DestinationsBusinessLogicEnum.BaseLibraryWithDate;
        
        IDestinationPathBusinessLogicModel destinationPathCustomPatternBusinessLogicModel = new DestinationPathCustomPatternBusinessLogicModel(null);
        
        destinationPathCustomPatternBusinessLogicModel.DirectoryPathCreationBusinessLogicEnums = new List<DirectoryPathCreationBusinessLogicEnum>()
        {
            DirectoryPathCreationBusinessLogicEnum.MetaCameraModel,
            DirectoryPathCreationBusinessLogicEnum.Separator,
            DirectoryPathCreationBusinessLogicEnum.Year,
            DirectoryPathCreationBusinessLogicEnum.Separator,
            DirectoryPathCreationBusinessLogicEnum.Month,
        };
  

        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV1 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
        });
        
        
        IDestinationNamingBusinessLogicModel destinationNamingBusinessLogicModelV2 = new DestinationNamingBusinessLogicModel(new List<FileNameCreationBusinessLogicEnum>()
        {
            FileNameCreationBusinessLogicEnum.Year,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Month,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Day,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hour,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Minute,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Millisecond,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Size,
            FileNameCreationBusinessLogicEnum.Separator,
            FileNameCreationBusinessLogicEnum.Hash,
        });
        destinationModel.DestinationPathDirectory = destinationPathCustomPatternBusinessLogicModel;
        destinationModel.NamingPatternFiles = destinationNamingBusinessLogicModelV1;
        
        destinationModel.NamingPatternFilesV2 = new List<IDestinationNamingBusinessLogicModel>()
        {
            destinationNamingBusinessLogicModelV1,
            destinationNamingBusinessLogicModelV2
        };
        
        filterBusinessLogicModel.DestinationModel = destinationModel;
        
        
        
        filterBusinessLogicModel.MaxFiles = 2000;
        filterBusinessLogicModel.MaxFiles = 2000;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        await orderingFilesV1.OrderFiles(filterBusinessLogicModel, true);
        
        Assert.Pass();
    }
}