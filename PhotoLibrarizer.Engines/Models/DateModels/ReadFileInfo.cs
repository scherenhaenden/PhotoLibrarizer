using PhotoLibrarizer.Engines.Metadata;

namespace PhotoLibrarizer.Engines.Models.DateModels;

public class ReadFileInfo : IReadFileInfo
{
    public DateTime? GetDateOfMediaCreation(string filePath) 
    {
        try
        {
            return (DateTime)new MetaInfoDates().GetDataOfImageCreation(filePath);
        }
        catch (Exception any)
        {
            return null;
        }
        
    }
    
    public string GetFileSize(string filePath) 
    {
        return new FileInfo(filePath).Length.ToString();
    }

    public string GetFileSizeHumanReadable(string filePath)
    {
        FileInfo f = new FileInfo(filePath);
        long s1 = f.Length;
        string sLen = f.Length.ToString();
        if (f.Length >= (1 << 30))
            sLen = string.Format("{0}Gb", f.Length >> 30);
        else
        if (f.Length >= (1 << 20))
            sLen = string.Format("{0}Mb", f.Length >> 20);
        else
        if (f.Length >= (1 << 10))
            sLen = string.Format("{0}Kb", f.Length >> 10);
        return sLen;
    }
}