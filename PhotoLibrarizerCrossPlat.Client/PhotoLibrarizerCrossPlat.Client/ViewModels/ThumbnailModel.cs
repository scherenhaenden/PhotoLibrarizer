using System;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public class ThumbnailModel : ViewModelBase
{
    public byte[] Thumbnail { get; set; } = Array.Empty<byte>(); // Initialize to avoid warning
}

