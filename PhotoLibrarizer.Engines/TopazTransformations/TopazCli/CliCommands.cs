namespace PhotoLibrarizer.Engines.TopazTransformations.TopazCli
{
    public class CliCommands : ICliCommands
    {
        public string Cli()
    {
        return "--cli";
    }

        public string Output(string path)
    {
        return $"--output {path}";
    }

        public bool Overwrite(bool enable)
    {
        return enable;
    }

        public bool Recursive(bool enable)
    {
        return enable;
    }

        public string Format(string format)
    {
        return $"--format {format}";
    }

        public int JPEGQuality(int quality)
    {
        return quality;
    }

        public int PNGCompression(int compression)
    {
        return compression;
    }

        public int TIFFBitDepth(int depth)
    {
        return depth;
    }

        public string TIFFCompression(string compression)
    {
        return $"--tiff-compression {compression}";
    }

        public bool ShowSettings(bool enable)
    {
        return enable;
    }

        public bool SkipProcessing(bool enable)
    {
        return enable;
    }

        public bool Verbose(bool enable)
    {
        return enable;
    }

        public int ReturnValue(int value)
    {
        return value;
    }
    }
}