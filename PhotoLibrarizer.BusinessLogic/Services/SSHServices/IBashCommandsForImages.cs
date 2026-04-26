using System.Globalization;
using PhotoLibrarizer.Engines.SSH;

namespace PhotoLibrarizer.BusinessLogic.Services.SSHServices;

public interface IBashCommandsForImages
{
    // public string GetImageSize(string imagePath);
    // public string RunExifTool(string imagePath);
    public string? RunExifTool(string imagePath, out string error);
    public DateTime? GetImageDate(string imagePath, out string error);
}

public class BashCommandsForImages: IBashCommandsForImages
{
    private readonly ISshServices _sshServices;

    public BashCommandsForImages(ISshServices _sshServices)
    {
        this._sshServices = _sshServices;
    }
    
    
    public string? RunExifTool(string imagePath, out string error)
    {
        var command = $"exiftool '{imagePath}'";
        var result = RunCommand(command, out error);
        return result;
    }
    private DateTime? MapStringDateToDateTime(string createDateUnClean)
    {
        try
        {
            var createDate = createDateUnClean.Split(':', 2)[1].Trim();
            string format = "yyyy:MM:dd HH:mm:ss";
            if (createDate.Contains("+"))
            {
                format = "yyyy:MM:dd HH:mm:sszzz"; // Add zzz for the timezone offset

            }
            
            DateTime dtObject = DateTime.ParseExact(createDate,format,CultureInfo.InvariantCulture);
            return dtObject;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
       
    }

    public DateTime? GetImageDate(string imagePath, out string error)
    {
        var result = RunExifTool(imagePath, out error);
        if (string.IsNullOrEmpty(error))
        {
            //return hashi?.Split(" ")[0].ToUpper() ?? string.Empty;
            var lines = result.Split("\n");

            try
            { 
            
                var createDateUnClean = lines.FirstOrDefault(x => x.Contains("Create Date"));

                if (!string.IsNullOrEmpty(createDateUnClean))
                {
                    var value = MapStringDateToDateTime(createDateUnClean);
                    if (value != null)
                    {
                        return value;
                    }
                        
                }
                
                var fileModificationDateUnClean = lines.FirstOrDefault(x => x.Contains("File Modification Date/Time"));
                
                
                if (!string.IsNullOrEmpty(fileModificationDateUnClean))
                {
                    var value = MapStringDateToDateTime(fileModificationDateUnClean);
                    if (value != null)
                    {
                        return value;
                    }
                        
                }
               
                
            }
            catch (Exception e)
            {
                Console.WriteLine("error on:" +imagePath);
                Console.WriteLine("error on:" +e.Message);
            }
            
            try
            {
                var createDateUnClean = lines.FirstOrDefault(x => x.Contains("Date/Time Original"));
                
                var createDate = createDateUnClean.Split(':', 2)[1].Trim();
            
                string format = "yyyy:MM:dd HH:mm:ss";
                DateTime dtObject = DateTime.ParseExact(createDate,format,CultureInfo.InvariantCulture);
                return dtObject;
                
            }
            catch (Exception e)
            {
                Console.WriteLine("error on:" +imagePath);
                Console.WriteLine("error on:" +e.Message);
            }
            
        }

        return null;
    }

    private string RunCommand(string command, out string error)
    {
        var hashi= _sshServices!.ExecuteCommand(command, out error);
        return hashi;
    }
}