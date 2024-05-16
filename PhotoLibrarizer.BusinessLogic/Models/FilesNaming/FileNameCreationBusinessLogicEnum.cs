using MetadataExtractor;

namespace PhotoLibrarizer.BusinessLogic.Models.FilesNaming;

public enum FileNameCreationBusinessLogicEnum
{
    Year = 1,
    Month = 2,
    Day = 3,
    Hour = 4,
    Minute = 5,
    Seconds = 6,
    Millisecond = 7,
    Size = 8,
    Hash = 9,
    OwnDateFormat = 10,
    Separator = 11,
    Meta = 12 // this would by a type
}

public class FileNameCreationBusinessLogicOptions
{
    public static readonly int Year = 1;
    public static readonly int Month = 2;
    public static readonly int Day = 3;
    public static readonly int Hour = 4;
    public static readonly int Minute = 5;
    public static readonly int Seconds = 6;
    public static readonly int Millisecond = 7;
    public static readonly int Size = 8;
    public static readonly int Hash = 9;
    public static readonly int OwnDateFormat = 10;
    public static readonly int Separator = 11;
    public static Tag? MetaTag; // You can replace this with a Type or another appropriate representation
}
