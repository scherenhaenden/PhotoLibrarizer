using MetadataExtractor;

namespace PhotoLibrarizer.BusinessLogic.Models.DirectoriesNaming;

public enum DirectoryPathCreationBusinessLogicEnum
{
    Year = 1,
    Month = 2,
    Day = 3,
    Hour = 4,
    Minutes = 5,
    
    Milliseconds = 6,
    Size = 7,
    Hash = 8,
    OwnDateFormat = 9, 
    MetaCameraModel = 10, // this would by a type
    Separator= 11,
    Seconds = 12,
}



public class DirectoryNameCreationBusinessLogicOptions
{
    public DirectoryNameCreationBusinessLogicOptions(DirectoryNameCreationBusinessLogicOptionsValues option)
    {
        Option = option;
    }
    
    public DirectoryNameCreationBusinessLogicOptionsValues Option { get; set; }
    
    /*public static readonly int Year = 1;
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
    public static Tag? MetaTag; // You can replace this with a Type or another appropriate representation*/
}
public class DirectoryNameCreationBusinessLogicOptionsValues
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

