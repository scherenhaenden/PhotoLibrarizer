namespace PhotoLibrarizerCore.Services.NamingPictures
{
    public class NamerModel
    {

        public List<DatesModel> Dates { get; set; }
        public long FileSize { get; set; }
        public string? Hashtag { get; set; }
    }
}