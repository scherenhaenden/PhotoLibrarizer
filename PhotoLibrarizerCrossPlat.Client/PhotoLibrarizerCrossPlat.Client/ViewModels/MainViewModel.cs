using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageMagick;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using PhotoLibrarizerCrossPlat.Client.Events;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private byte[] _mainImage = Array.Empty<byte>();
        public byte[] MainImage
        {
            get => _mainImage;
            set => this.RaiseAndSetIfChanged(ref _mainImage, value);
        }

        private string _currentPath;
        public string CurrentPath
        {
            get => _currentPath;
            set => this.RaiseAndSetIfChanged(ref _currentPath, value);
        }

        private ObservableCollection<byte[]> _thumbnails = new ObservableCollection<byte[]>();
        public ObservableCollection<byte[]> Thumbnails
        {
            get => _thumbnails;
            set => this.RaiseAndSetIfChanged(ref _thumbnails, value);
        }

        private Dictionary<string, byte[]> _thumbnailCache = new Dictionary<string, byte[]>();

        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set => this.RaiseAndSetIfChanged(ref _currentImageIndex, value);
        }

        public ReactiveCommand<Unit, Unit> LoadPathCommand { get; }
        public ReactiveCommand<Unit, Unit> BrowseCommand { get; }

        public MainViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.Subscribe<NextImageEvent>(e => NextImage());
            _eventAggregator.Subscribe<PreviousImageEvent>(e => PreviousImage());

            LoadPathCommand = ReactiveCommand.CreateFromTask(LoadPathAsync);
            BrowseCommand = ReactiveCommand.CreateFromTask(Browse);
        }

        public async Task LoadPathAsync()
        {
            if (Directory.Exists(CurrentPath))
            {
                Thumbnails.Clear();
                _thumbnailCache.Clear();

                var supportedExtensions = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" };
                var files = supportedExtensions.SelectMany(ext => Directory.GetFiles(CurrentPath, ext)).ToList();

                await Task.WhenAll(files.Select(LoadThumbnailAsync));

                if (files.Count > 0)
                {
                    _currentImageIndex = 0;
                    MainImage = await LoadImageAsync(files[0]);
                }
            }
        }

        public async Task LoadThumbnailAsync(string filePath)
        {
            if (!_thumbnailCache.ContainsKey(filePath))
            {
                using (var image = new MagickImage(filePath))
                {
                    image.Format = MagickFormat.Jpeg;
                    var thumbnail = image.ToByteArray();
                    _thumbnailCache[filePath] = thumbnail;
                    await Task.Run(() => Thumbnails.Add(thumbnail));
                }
            }
            else
            {
                await Task.Run(() => Thumbnails.Add(_thumbnailCache[filePath]));
            }
        }

        public async Task<byte[]> LoadImageAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                using (var image = new MagickImage(filePath))
                {
                    image.Format = MagickFormat.Jpeg;
                    return image.ToByteArray();
                }
            });
        }

        public async Task Browse()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null);

            if (!string.IsNullOrEmpty(result))
            {
                CurrentPath = result;
                await LoadPathAsync();
            }
        }

        public void NextImage()
        {
            if (_currentImageIndex < Thumbnails.Count - 1)
            {
                _currentImageIndex++;
                LoadCurrentImage();
            }
        }

        public void PreviousImage()
        {
            if (_currentImageIndex > 0)
            {
                _currentImageIndex--;
                LoadCurrentImage();
            }
        }

        public async void LoadCurrentImage()
        {
            var supportedExtensions = new[] { "*.jpg", "*.jpeg", "*.png", "*.bmp", "*.gif" };
            var files = supportedExtensions.SelectMany(ext => Directory.GetFiles(CurrentPath, ext)).ToList();

            if (files.Count > 0 && _currentImageIndex >= 0 && _currentImageIndex < files.Count)
            {
                MainImage = await LoadImageAsync(files[_currentImageIndex]);
            }
        }
    }
}
