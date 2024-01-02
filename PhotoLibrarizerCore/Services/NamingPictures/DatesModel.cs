namespace PhotoLibrarizerCore.Services.NamingPictures
{
    public class DatesModel
    {
        public DateTime Date { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? KeyInMetaData { get; set; } = string.Empty;
    }
}