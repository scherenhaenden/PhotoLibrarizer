namespace PhotoLibrarizer.BusinessLogic.Services.IOService
{
   public interface IDestinationManager
   {
      public string FindWhereToMoveFile(string sourcePath, string destinationPathBase);
   }
}