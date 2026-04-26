using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using ImageMagick;
using ReactiveUI;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;



public class ImageViewerViewModel : ViewModelBase
{
    private byte[] _mainImage = Array.Empty<byte>();
    public byte[] MainImage
    {
        get => _mainImage;
        set => this.RaiseAndSetIfChanged(ref _mainImage, value);
    }

    private ObservableCollection<ThumbnailModel> _thumbnails = new ObservableCollection<ThumbnailModel>();
    public ObservableCollection<ThumbnailModel> Thumbnails
    {
        get => _thumbnails;
        set => this.RaiseAndSetIfChanged(ref _thumbnails, value);
    }
    
    public ThumbnailViewModel ThumbnailViewMo { get; set; } = new ThumbnailViewModel();

    public ImageViewerViewModel()
    {
        Thumbnails = new ObservableCollection<ThumbnailModel>();
        foreach (var thumbnail in Thumbnails)
        {
            //thumbnail.MouseHoverCommand.Subscribe(_ => OnThumbnailMouseHovered(thumbnail));
            //thumbnail.ClickCommand.Subscribe(_ => OnThumbnailClicked(thumbnail));
            //thumbnail.DoubleClickCommand.Subscribe(_ => OnThumbnailDoubleClicked(thumbnail));
        }
    }

    private void OnThumbnailClicked(ThumbnailViewModel viewModel)
    {
        // Handle thumbnail click
    }

    private void OnThumbnailMouseHovered(ThumbnailViewModel viewModel)
    {
        // Handle thumbnail hover
    }

    private void OnThumbnailDoubleClicked(ThumbnailViewModel viewModel)
    {
        // Handle thumbnail double-click
    }
    
    

    public async Task LoadImagesAsync(string directoryPath)
    {
        if (!Directory.Exists(directoryPath)) return;

        // Preparation
        var thumbnails = new ObservableCollection<ThumbnailModel>();
        var supportedExtensions = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" };
        var files = supportedExtensions.SelectMany(ext => Directory.GetFiles(directoryPath, ext)).ToList();

        // Load Main Image (if any)
        if (files.Count > 0)
        {
            using (var mainImage = new MagickImage(files[0]))
            {
                MainImage = await Task.Run(() => mainImage.ToByteArray(MagickFormat.Jpeg)); // Run on background thread
            }
        }
        Thumbnails.Clear();

        foreach (var file in files)
        {
            // Maximum thumbnail dimensions
            int maxWidth = 100;  // Set your desired maximum width
            int maxHeight = 100; // Set your desired maximum height
            var innerThumbnail = new ThumbnailModel();
            using (var image = new MagickImage(file))
            {
                image.Resize(maxWidth, maxHeight);
                var thumbnailBytes = await Task.Run(() => image.ToByteArray(MagickFormat.Jpeg));
                innerThumbnail = new ThumbnailModel { Thumbnail = thumbnailBytes };
                thumbnails.Add(innerThumbnail);
            }
            
            await Dispatcher.UIThread.InvokeAsync(() => 
            {
                //Thumbnails.Clear();
                Thumbnails = thumbnails; // Replace the collection efficiently
                ThumbnailViewMo.ThumbnailsItems.Add(innerThumbnail);
            });
        }
        // Load Thumbnails (concurrently)
        /*var thumbnailTasks = files.Select(async file => 
        {
            using (var image = new MagickImage(file))
            {
                var thumbnailBytes = await Task.Run(() => image.ToByteArray(MagickFormat.Jpeg));
                return new ThumbnailModel { Thumbnail = thumbnailBytes };
            }
        });

        var thumbnailResults = await Task.WhenAll(thumbnailTasks);
        thumbnails = new ObservableCollection<ThumbnailModel>(thumbnailResults); // Directly create from results
        
        

        // Update UI
        await Dispatcher.UIThread.InvokeAsync(() => 
        {
            Thumbnails.Clear();
            Thumbnails = thumbnails; // Replace the collection efficiently
            ThumbnailViewMo.ThumbnailsItems = thumbnails;
        });*/
    }

}
