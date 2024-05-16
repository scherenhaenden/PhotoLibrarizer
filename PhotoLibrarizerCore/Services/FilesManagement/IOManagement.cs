namespace PhotoLibrarizerCore.Services.FilesManagement
{
    public class IoManagement : IIoManagement
    {
        public List<string> GetFiles(string path, bool recursive = false)
    {
        return GetFiles(path, null, recursive);
    }

        public List<string> GetFiles(string path, string[]? extensions, bool recursive = false)
    {
        var filesV2 = Directory.GetFiles(path,
            "*.*",
            recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).AsEnumerable();
            
        // filter by extensions
        if(extensions != null && extensions.Length > 0)
        {
            filesV2 = filesV2.Where(files => extensions.Any(files.EndsWith));
        }
        // return
        
        return filesV2.ToList();
    }
    
        private string[] Extensions()
    {

        var imagesExtensions = new List<string>();

        /*jpg*/
        imagesExtensions.AddRange(
            new List<string>() {".JPG",".jpg",".jpeg",}
        );

        /*nef*/
        imagesExtensions.AddRange(
            new List<string>() {".NEF",".nef",}
        );

        /*ASF*/
        imagesExtensions.AddRange(
            new List<string>() {".ASF",".asf"}
        );
        /*CR2*/
        imagesExtensions.AddRange(
            new List<string>() {".CR2",".cr2"}
        );

        /*mp3*/
        imagesExtensions.AddRange(
            new List<string>() {".mp3", ".mp3"}
        );

        imagesExtensions.AddRange(
            new List<string>() {".OGG", ".ogg"}
        );

        imagesExtensions.AddRange(
            new List<string>() {".MOV", ".mov"}
        );

        /*mpg*/
        imagesExtensions.AddRange(
            new List<string>() {".MPG", ".mpg"}
        );

        var AllowedExtensions = new [] 
            { ".MPG",".mpg",".db",".avi",".3gp",".mp4",".BMP",".bmp",".png",".PNG",".directory",}; 

        imagesExtensions.AddRange(AllowedExtensions.ToList());

        AllowedExtensions = imagesExtensions.ToArray();
        return AllowedExtensions;
    }

        public List<string> GetFiles(string path, string[]? extensions, bool recursive = false, bool knownMediaExtensions = true)
    {
        if(knownMediaExtensions) extensions = Extensions();
        
        return GetFiles(path, extensions, recursive);
    }
    }
}