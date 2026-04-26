using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public class PathBarViewModel : ViewModelBase
{
    private string _currentPath;
    public string CurrentPath
    {
        get => _currentPath;
        set => this.RaiseAndSetIfChanged(ref _currentPath, value);
    }

    public ImageViewerViewModel ImageViewer { get; }

    public ReactiveCommand<Unit, Unit> LoadPathCommand { get; }
    public ReactiveCommand<Unit, Unit> BrowseCommand { get; }

    public PathBarViewModel()
    {
        ImageViewer = new ImageViewerViewModel();
        LoadPathCommand = ReactiveCommand.Create(LoadPath);
        BrowseCommand = ReactiveCommand.CreateFromTask(Browse);
    }

    private void LoadPath()
    {
        // make this task work
        Task.Run(() => ImageViewer.LoadImagesAsync(CurrentPath));
        
        //ImageViewer.LoadImagesAsync(CurrentPath);
    }

    private async Task Browse()
    {
        var dialog = new OpenFolderDialog();
        var result = await dialog.ShowAsync(App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null);

        if (!string.IsNullOrEmpty(result))
        {
            CurrentPath = result;
            LoadPath();
        }
    }
}
