namespace PhotoLibrarizerCore.Services.NamingPictures
{
    public class PicturesNamerServiceV1 : IPicturesNamerService
    {
        public string GenerateName(NamerModel model)
    {
        //TODO: Implement
        
        var name = string.Empty;
        
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        
        if (model.Dates.Count == 0)
        {
            throw new ArgumentException("Dates cannot be empty");
        }
        
        name = model.Dates[0].Date.ToString("yyyy_MM_dd_HH_mm_ss");
        
        name += $"_size_{model.FileSize}";
        
        if (!string.IsNullOrEmpty(model.Hashtag))
        {
            name += $"_hashtag_{model.Hashtag}";
        }

        return name;
    }
    }
}