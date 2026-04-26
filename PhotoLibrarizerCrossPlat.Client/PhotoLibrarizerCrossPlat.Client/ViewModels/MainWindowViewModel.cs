using ReactiveUI;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentView;
    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => this.RaiseAndSetIfChanged(ref _currentView, value);
    }

    public PathBarViewModel PathBar { get; }
    

    public MainWindowViewModel()
    {
        PathBar = new PathBarViewModel();
        CurrentView = new MainViewModel(new EventAggregator());
    }
}