namespace PhotoLibrarizer.Engines.TopazTransformations.TopazCli
{
    /*public bool TryConvertImage(string path, int width, int height, string newPath = "");

    public bool TryConvertImage(Stream input, int width, int height, out Stream? output, out string errorMessage);*/

    public interface ICliCommands
    {
    
    
        // This interface defines the CLI (Command Line Interface) commands for Topaz.

        // The Cli method should return a string representing the CLI command.
        public string Cli();

        // The Output method should return a string representing the output path.
        // It takes a path as input to specify where the output should be saved.
        public string Output(string path);
    
        // --overwrite: Allow overwriting of files. THIS IS DESTRUCTIVE.
        // Enables the option to overwrite existing files. This action is potentially destructive.
        public bool Overwrite(bool enable);

        // --recursive, -r: If given a folder path, it will recurse into subdirectories instead of just grabbing top-level files.
        // Note: If an output folder is specified, the input folder's structure will be recreated within the output as necessary.
        // Allows recursive processing of files within a folder, including subdirectories. Output structure mirrors input structure.
        public bool Recursive(bool enable);

        // File Format Options:
        // --format, -f: Set the output format. Accepts jpg, jpeg, png, tif, tiff, dng, or preserve. Default: preserve
        // Note: Preserve will attempt to preserve the exact input extension, but RAW files will still be converted to DNG.
        // Sets the desired output format for processed images. Default is 'preserve' which tries to keep the input format.
        public string Format(string format);

        // Format Specific Options:
        // --quality, -q: JPEG quality for output. Must be between 0 and 100. Default: 95
        // Sets the JPEG quality for output images. Range is 0 to 100, with a default of 95.
        public int JPEGQuality(int quality);

        // --compression, -c: PNG compression amount. Must be between 0 and 10. Default: 2
        // Sets the PNG compression amount for output images. Range is 0 to 10, with a default of 2.
        public int PNGCompression(int compression);

        // --bit-depth, -d: TIFF bit depth. Must be either 8 or 16. Default: 16
        // Sets the bit depth for TIFF format output. Options are 8 or 16, with a default of 16.
        public int TIFFBitDepth(int depth);

        // --tiff-compression: -tc: TIFF compression format. Must be "none," "lzw," or "zip."
        // Note: LZW is not allowed on 16-bit output and will be converted to zip.
        // Sets the TIFF compression format for output. Options are "none," "lzw," or "zip."
        public string TIFFCompression(string compression);

        // Debug Options:
        // --showSettings: Shows the Autopilot settings for images before they are processed
        // Displays the Autopilot settings for images before processing begins.
        public bool ShowSettings(bool enable);

        // --skipProcessing: Skips processing the image (e.g., if you just want to know the settings)
        // Allows skipping the actual image processing, useful for inspecting settings without applying changes.
        public bool SkipProcessing(bool enable);

        // --verbose, -v: Print more log entries to the console.
        // Enables verbose mode, printing more log entries to the console for debugging or detailed information.
        public bool Verbose(bool enable);
    
        // Return values:
        // 0 - Success
        // 1 - Partial Success (e.g., some files failed)
        // -1 (255) - No valid files passed.
        // -2 (254) - Invalid log token. Open the app normally to log in.
        // -3 (253) - An invalid argument was found.
        // Defines the return values of the CLI command.
        public int ReturnValue(int value);



    }
}