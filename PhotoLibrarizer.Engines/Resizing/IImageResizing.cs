namespace PhotoLibrarizer.Engines.Resizing
{
    public interface IImageResizing
    {
        public bool TryConvertImage(string path, int width, int height, string newPath = "");
    
        public bool TryConvertImage(Stream input, int width, int height, out Stream? output, out string errorMessage);

    }
}