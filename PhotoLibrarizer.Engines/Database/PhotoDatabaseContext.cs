
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace PhotoLibrarizer.Engines.Database;

// name of the database


public class PhotoDatabaseContext: DbContext
{
    private readonly string connectionString;

    
    public PhotoDatabaseContext(DbContextOptions<PhotoDatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<FileDatabaseModel> Files { get; set; }

}





[Table("CustomTableName")]
public class FileDatabaseModel
{
    
    public string FullPathOfFile { get; set; }
    
    public string Path { get; set; }
    public string FileName { get; set; }
    public string Hash { get; set; }
    
    public FileInfo GeneralFileInformation { get; set; }
    public DateTime? DateCreation { get; set; }
    public long Size { get; set; }
    public string PathOfFile { get; set; }


    public string CorrectBashFullFileName { get; set; }
    public string CorrectBashFullFileNameDestination { get; set; }

    public Dictionary<string, DateTime> KeyDatesMetadata { get; set; }
    public Dictionary<string, string> KeyMetadata { get; set; }

    public string Directories { get; set; }

    public string SourcePath { get; set; }
    public string DestinationPath { get; set; }
    public bool CopiedToDestination { get; set; }
    public bool CheckedIfCopied { get; set; }
    public bool DeletedFromTheSource { get; set; }

   
    /*public FileDatabaseModelPlain()
    {
        GeneralFileInformation = new FileInfo("");
        KeyDatesMetadata = new Dictionary<string, DateTime>();
        KeyMetadata = new Dictionary<string, string>();
        Directories = new List<Directory>();
        CorrectBashFullFileName = string.Empty;
        CorrectBashFullFileNameDestination = string.Empty;
        SourcePath = string.Empty;
        DestinationPath = string.Empty;
        CopiedToDestination = false;
        CheckedIfCopied = false;
        DeletedFromTheSource = false;
    }*/
    
    
    

   


        
    

}