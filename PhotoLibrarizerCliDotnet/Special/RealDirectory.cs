using PhotoLibrarizer.BusinessLogic.Models;
using PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;
using PhotoLibrarizer.BusinessLogic.Models.FilesNaming;
using PhotoLibrarizer.BusinessLogic.Services.Ordering;

namespace PhotoLibrarizerCliDotnet.Special;

public class RealDirectory
{
  public async Task RunAsync()
  {
    FilterBusinessLogicModel filterBusinessLogicModel = new FilterBusinessLogicModel();
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/30");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Users/edwardflores/Pictures/test");
        //filterBusinessLogicModel.PathsForSourceFiles.Add("/Volumes/Edward/_ForGalleryRightAhead");
        
        // /Volumes/Edward/_ForGalleryRightAhead/NIKON Z 6/2023
        
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Users/edwardflores/Pictures/organized/2023/08/01");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Users/edwardflores/Pictures/organized/2023/08/02");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Users/edwardflores/Pictures/organized/2023/08/03");
        //filterBusinessLogicModel.PathsForSourceFiles.Add(@"/Users/edwardflores/Pictures/Photos/DCIM/131ND750");
        // /Volumes/TRANSCEND/DCIM/102NCZ_6
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
        
        
        
        filterBusinessLogicModel.MaxFiles = 1000;
        filterBusinessLogicModel.Extensions = new List<string>() {".jpg", ".nef", ".dng"};
        
        
        IOrderingFilesV1 orderingFilesV1 = new OrderingFilesV1();
        //await orderingFilesV1.OrderFiles(filterBusinessLogicModel, true);
        await orderingFilesV1.MultiThreadedOrderFiles(filterBusinessLogicModel, true);
   
  }
}