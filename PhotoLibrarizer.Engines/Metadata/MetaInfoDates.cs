using System.Globalization;
using MetadataExtractor;
using Directory = MetadataExtractor.Directory;

namespace PhotoLibrarizer.Engines.Metadata
{
	using doob=photo.exif;

	public class MetaInfoDates
	{
		public DateTime? photo_exif(string PathToPicture)
	{
		doob.Parser Prs = new doob.Parser ();
		IEnumerable<photo.exif.ExifItem> MetaItemsPhotos = Prs.Parse (PathToPicture);
		List<photo.exif.ExifItem> ListedMetaItemsPhotos = MetaItemsPhotos.ToList ();
		foreach (var item in ListedMetaItemsPhotos) 
		{
			if (item.Id == 306 || item.Id == 36867) 
			{
				try
				{
					DateTimeOffset dto = DateTimeOffset.Parse(item.Value.ToString());
					DateTime dtObject = dto.DateTime; 
					return dtObject;
						
				}
				catch(Exception any)
				{
					string format = "yyyy:MM:dd HH:mm:ss";
					//if(item.Value.ToString())

					DateTime dtObjecvt = (DateTime)item.Value; 

					DateTime dtObject = DateTime.ParseExact(item.Description,format,CultureInfo.InvariantCulture);
					return dtObject;						
				}

			}
		}
		return null;			
	}
	
		IReadOnlyList<Directory> h;
		private IReadOnlyList<Directory> getH( string path) 
    {
        return ImageMetadataReader.ReadMetadata(path);
    }
		private async Task DoWork2Async(string path)
    {
        Task.Run(() =>
        {
            h = getH(path);
        }).Wait(3000);
    }
    
		public DateTime? GetDataOfImageCreation(string PathToImage)
    {
	    try
	    {
		    return MetadataExtractorMethod (PathToImage);

	    }
	    catch(Exception any) 
	    {
		    return photo_exif (PathToImage);
				
	    }			
    }
    
    
		public DateTime? MetadataExtractorMethod(string path)
    {
        MetadataExtractor.Formats.FileSystem.FileMetadataReader DRR = new MetadataExtractor.Formats.FileSystem.FileMetadataReader ();
        MetadataExtractor.Formats.FileSystem.FileMetadataDirectory Readded=DRR.Read (path);
        
        List<Directory> rs2 = new List<Directory> ();
        try
        {
            //var h=MetadataExtractor.ImageMetadataReader.ReadMetadata (path);
            //IReadOnlyList<MetadataExtractor.Directory> h=MetadataExtractor.ImageMetadataReader.ReadMetadata (path);
            DoWork2Async(path);

            Directory rs = h.ElementAt (0);
            rs2 = h.ToList (); 
            var resultDateCreated=FindXMPDirectoryTag(rs2);
            if(resultDateCreated!=null)
            {
                return resultDateCreated;
            }
            //var tgs = rs.Tags;

        }
        catch(Exception any) 
        {
            //rs2 = 

            //Readded.Tags
            return Loops(Readded.Tags);
        }

        foreach (Directory item in rs2) 
        {

            try
            {
                var Vals = item.Tags;
                var Result=Loops(Vals);
                if( Result!=null)
                {
                    return Result;
                }
            }
            catch(Exception any) 
            {
					
            }				
        }
        return null;			
    }
    
		private DateTime? FindXMPDirectoryTag(List<Directory> values)
    {
        try
        {
            var result=values.Find(x=>x.ToString().ToLower().Contains("xmp"));
            var resultTags=result.Tags.ToList();
            var CreateDateTag=resultTags.Find(x=>x.ToString().ToLower().Contains("create"));
            var ResultDate = CreateDateTag.Description;

            DateTimeOffset dto = DateTimeOffset.Parse(ResultDate);
            DateTime dtObject = dto.DateTime; 
            return dtObject;
        }
        catch(Exception) {
				
        }
        return null;
    }
    
		DateTime? Loops(IReadOnlyList<Tag> Vals)
		{
			foreach (Tag ITEM in Vals) 
			{
				if (ITEM.Type == 306|| ITEM.Type == 36867) 
				{
					int iiiii=0;
					try
					{
						//DateTimeOffset dto = DateTimeOffset.Parse(item.Description);
						string format = "yyyy:MM:dd HH:mm:ss";
						DateTime dtObject = DateTime.ParseExact(ITEM.Description,format,CultureInfo.InvariantCulture);
						return dtObject;
					}
					catch(Exception any) 
					{

					}
				}

				else if(ITEM.DirectoryName=="Quicktime Movie Header"&& ITEM.Name.ToLower()=="created")
				{
					var hz=ITEM.Description.ToString();

					try
					{
						DateTimeOffset dto = DateTimeOffset.Parse(hz);
						DateTime dtObject = dto.DateTime; 
						return dtObject;

					}
					catch(Exception any) 
					{
						string format = "MMM d HH:mm:ss yyyy";

						CultureInfo english =new System.Globalization.CultureInfo("de-DE");
						string []Values = ITEM.Description.Split(' ');

						DateTime test = DateTime.Now;//= DateTime.ParseExact(ITEM.Description,format,CultureInfo.InvariantCulture);
						var whatever=  test.ToString(format,english);
						string value=ITEM.Description;
						value=ITEM.Description.Remove(0,3);

						//else if()

						DateTime dtObject=DateTime.ParseExact(value,format,english);

						//DateTimeOffset dto = DateTimeOffset.Parse(ITEM.Description);
						//DateTime dtObject = dto.DateTime; 
						return dtObject;
					}
				}
			}

			foreach (Tag ITEM in Vals) 
			{
				if (ITEM.Name.ToLower().Contains("file modified date") )
				{
					int iiiii=0;
					try
					{
						string format = "MMM d HH:mm:ss K yyyy";

						CultureInfo english =new System.Globalization.CultureInfo("de-DE");
						string []Values = ITEM.Description.Split(' ');

						DateTime test = DateTime.Now;//= DateTime.ParseExact(ITEM.Description,format,CultureInfo.InvariantCulture);
						var whatever=  test.ToString(format,english);
						string value=ITEM.Description;
						value=ITEM.Description.Remove(0,3);

						//else if()

						DateTime dtObject=DateTime.ParseExact(value,format,english);
						//DateTimeOffset dto = DateTimeOffset.Parse(ITEM.Description);
						//DateTime dtObject = dto.DateTime; 
						return dtObject;

						/*DateTimeOffset dto = DateTimeOffset.Parse(item.Description);
								string format = "yyyy:MM:dd HH:mm:ss";

								DateTime dtObject = DateTime.ParseExact(ITEM.Description,format,CultureInfo.InvariantCulture);
								return dtObject;*/
					}
					catch(Exception any) 
					{

					}
				}
			}
			return null;
		}
	}
}