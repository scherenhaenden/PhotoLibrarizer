using PhotoLibrarizer.BusinessLogic.Models;

namespace PhotoLibrarizer.BusinessLogic.Services.IOService
{
    public class DestinationManager: IDestinationManager
    {
        private readonly DestinationsBusinessLogicEnum _destinationsBusinessLogicEnum;

        public DestinationManager(DestinationsBusinessLogicEnum destinationsBusinessLogicEnum)
    {
        _destinationsBusinessLogicEnum = destinationsBusinessLogicEnum;
    }
   
        public string FindWhereToMoveFile(string sourcePath, string destinationPathBase)
    {
      
      
      
        throw new NotImplementedException();
    }
    }
}