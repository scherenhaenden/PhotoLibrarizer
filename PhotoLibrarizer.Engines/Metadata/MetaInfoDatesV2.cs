using System.Globalization;
using MetadataExtractor;

namespace PhotoLibrarizer.Engines.Metadata;

public class MetaInfoDatesV2
{
    public DateTime? PhotoExif(string pathToPicture)
    {
        IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(pathToPicture);
        
        foreach (var directory in directories)
        {
            var tags = directory.Tags;
            foreach (var tag in tags)
            {
                if (tag.Type == 306 || tag.Type == 36867)
                {
                    try
                    {
                        DateTime dtObject = DateTime.Parse(tag.Description);
                        return dtObject;
                    }
                    catch (Exception)
                    {
                        string format = "yyyy:MM:dd HH:mm:ss";
                        DateTime dtObject = DateTime.ParseExact(tag.Description, format, CultureInfo.InvariantCulture);
                        return dtObject;
                    }
                }
            }
        }

        return null;
    }

    public DateTime? MetadataExtractorMethod(string path)
    {
        List<MetadataExtractor.Directory> directories = new List<MetadataExtractor.Directory>();
        
        try
        {
            directories = MetadataExtractor.ImageMetadataReader.ReadMetadata(path).ToList();
            var resultDateCreated = FindXMPDirectoryTag(directories);
            if (resultDateCreated != null)
            {
                return resultDateCreated;
            }
        }
        catch (Exception)
        {
            return Loops(directories.SelectMany(d => d.Tags).ToList());
        }

        foreach (var directory in directories)
        {
            try
            {
                var tags = directory.Tags;
                var result = Loops(tags.ToList());
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception)
            {
                // Handle exception if needed
            }
        }

        return null;
    }

    private DateTime? FindXMPDirectoryTag(List<MetadataExtractor.Directory> directories)
    {
        try
        {
            var xmpDirectory = directories.Find(d => d.Name.ToLower().Contains("xmp"));
            if (xmpDirectory != null)
            {
                var createDateTag = xmpDirectory.Tags.FirstOrDefault(t => t.Name.ToLower().Contains("create"));
                if (createDateTag != null)
                {
                    if (DateTime.TryParse(createDateTag.Description, out DateTime result))
                    {
                        return result;
                    }
                }
            }
        }
        catch (Exception)
        {
            // Handle exception if needed
        }

        return null;
    }

    private DateTime? Loops(List<MetadataExtractor.Tag> tags)
    {
        foreach (var tag in tags)
        {
            if (tag.Type == 306 || tag.Type == 36867)
            {
                try
                {
                    if (DateTime.TryParse(tag.Description, out DateTime result))
                    {
                        return result;
                    }
                    else
                    {
                        string format = "yyyy:MM:dd HH:mm:ss";
                        DateTime dtObject = DateTime.ParseExact(tag.Description, format, CultureInfo.InvariantCulture);
                        return dtObject;
                    }
                }
                catch (Exception)
                {
                    // Handle exception if needed
                }
            }
            else if (tag.DirectoryName == "Quicktime Movie Header" && tag.Name.ToLower() == "created")
            {
                try
                {
                    if (DateTime.TryParse(tag.Description, out DateTime result))
                    {
                        return result;
                    }
                    else
                    {
                        string format = "MMM d HH:mm:ss yyyy";
                        CultureInfo culture = new CultureInfo("de-DE");

                        string value = tag.Description;
                        value = value.Remove(0, 3);

                        DateTime dtObject = DateTime.ParseExact(value, format, culture);
                        return dtObject;
                    }
                }
                catch (Exception)
                {
                    // Handle exception if needed
                }
            }
        }

        foreach (var tag in tags)
        {
            if (tag.Name.ToLower().Contains("file modified date"))
            {
                try
                {
                    string format = "MMM d HH:mm:ss K yyyy";
                    CultureInfo culture = new CultureInfo("de-DE");

                    string value = tag.Description;
                    value = value.Remove(0, 3);

                    DateTime dtObject = DateTime.ParseExact(value, format, culture);
                    return dtObject;
                }
                catch (Exception)
                {
                    // Handle exception if needed
                }
            }
        }

        return null;
    }
}