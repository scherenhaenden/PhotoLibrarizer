namespace PhotoLibrarizer.Engines.IoEngines;

public class ListOfDefaultExtensions
{
    public string[] Extensions()
    {

        var imagesExtensions = new List<string>();

        /*jpg*/
        imagesExtensions.AddRange(
            new List<string>() { ".JPG", ".jpg", ".jpeg", }
        );

        /*nef*/
        imagesExtensions.AddRange(
            new List<string>() { ".NEF", ".nef", }
        );

        /*ASF*/
        imagesExtensions.AddRange(
            new List<string>() { ".ASF", ".asf" }
        );
        /*CR2*/
        imagesExtensions.AddRange(
            new List<string>() { ".CR2", ".cr2" }
        );

        /*mp3*/
        imagesExtensions.AddRange(
            new List<string>() { ".mp3", ".mp3" }
        );

        imagesExtensions.AddRange(
            new List<string>() { ".OGG", ".ogg" }
        );

        imagesExtensions.AddRange(
            new List<string>() { ".MOV", ".mov" }
        );

        /*mpg*/
        imagesExtensions.AddRange(
            new List<string>() { ".MPG", ".mpg" }
        );
        
        /*tif*/
        imagesExtensions.AddRange(
            new List<string>() { ".TIF", ".tif" }
        );
        
        /*dng*/
        imagesExtensions.AddRange(
            new List<string>() { ".DNG", ".dng" }
        );

        var AllowedExtensions = new[]
            { ".MPG",".mpg",".db",".avi",".3gp",".mp4",".BMP",".bmp",".png",".PNG",".directory",};

        //imagesExtensions.AddRange(AllowedExtensions.ToList());

        AllowedExtensions = imagesExtensions.ToArray();
        return AllowedExtensions;
    }
}