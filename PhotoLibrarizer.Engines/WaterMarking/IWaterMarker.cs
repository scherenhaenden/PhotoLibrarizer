namespace PhotoLibrarizer.Engines.WaterMarking
{
    public interface IWaterMarker
    {
        void AddWatermark(string sourceFilePath, string outputFilePath, string watermarkFilePath);
    
        Stream AddWatermarkAndGetStream(Stream sourceImageStream, Stream watermarkImageStream);
    
        Stream AddWatermarkAndGetStream(Stream sourceImageStream, string watermarkFilePath);
    }
}