namespace PhotoLibrarizerCore.Services.NamingPictures
{
    public interface IPicturesNamerService
    {
        string GenerateName(NamerModel model);
    }
}