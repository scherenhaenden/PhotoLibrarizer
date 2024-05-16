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
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/30");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/test");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/_ForGalleryRightAhead");
        
        ///Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2023
        ///
        filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/_ForGalleryRightAhead");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2022");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2021");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2020");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2019");
        
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
        
        
        
        filterBusinessLogicModel.MaxFiles = 10000;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef", ".dng"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        //await orderingFilesV1.OrderFiles(filterBusinessLogicModel, true);
        await orderingFilesV1.MultiThreadedOrderFiles(filterBusinessLogicModel, true);
        
        Assert.Pass();
    }
    
    
    [Test]
    public async Task Test3_ThisByCameraToDirectory()
    {
        FilterBusinessLogicModel filterBusinessLogicModel = new FilterBusinessLogicModel();
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Users/edwardflores/Pictures/Kamera Uploads");
        ///Volumes/Edward/GalleryOld/2019/08
        // get all the direct subdirectories of path
        /*var directories = Directory.GetDirectories(@"/Volumes/Edward/Raws/Fotos_new/2016").ToList();
        
        directories.AddRange(Directory.GetDirectories(@"/Volumes/Edward/Raws/Fotos").ToList());
        
        directories = directories.Take(10).ToList();

        foreach (var directory in directories)
        {
            filterBusinessLogicModel.PathsForSourceFiles.Add(directory);
        }*/
        
        
        filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/Raws/Fotos");
        filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/Raws/Fotos_new/2016");
        filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Volumes/Edward/GalleryOld/2019/08");
        
        
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/AAUseThisOne");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Raws");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/AUseThisOne");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Los Globos");
        
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/01");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2009");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2010");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2011");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2012");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2013");*/
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/03");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/04");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/05");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/06");
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/07");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/08");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/09");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/10");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/11");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2014/12");*/

        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2023");
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/HuaweiCloud/All");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Raws");
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2007");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2008");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2009");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2010");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2011");/*
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/HuaweiCloud/All");
  
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2010");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2011");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2012");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2013");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2014");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2015");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2016");*/
        /*filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2017");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/Swap/Eva/pics/2018");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2011");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/GalleryOld/2012");
        filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/HuaweiCloud");*/

        
        
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
        
        
        
        
        filterBusinessLogicModel.MaxFiles = 150;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef", ".dng"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        await orderingFilesV1.MultiThreadedOrderFiles(filterBusinessLogicModel, true);
        
        Assert.Pass();
    }
}