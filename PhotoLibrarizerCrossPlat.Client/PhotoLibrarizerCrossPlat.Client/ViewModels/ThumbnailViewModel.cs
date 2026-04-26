using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ReactiveUI;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels;

public class ThumbnailViewModel : ViewModelBase
{
    


    public ThumbnailViewModel()
    {
        MouseHoverCommand = ReactiveCommand.Create(OnMouseHover);
        ClickCommand = ReactiveCommand.Create(OnClick);
        DoubleClickCommand = ReactiveCommand.Create(OnDoubleClick);
    }

    /*public ThumbnailViewModel()
    {
        MouseHoverCommand = ReactiveCommand.Create<ThumbnailModel>(OnMouseHover);
        ClickCommand = ReactiveCommand.Create<ThumbnailModel>(OnClick);
        DoubleClickCommand = ReactiveCommand.Create<ThumbnailModel>(OnDoubleClick);

        // Load your thumbnail data here (e.g., from an image collection)
        // Populate the Thumbnails collection
    }*/

    private void OnMouseHover(ThumbnailModel thumbnail)
    {
        // ... (Your logic for handling mouse hover on a thumbnail)
    }

    private void OnClick(ThumbnailModel thumbnail)
    {
        // ... (Your logic for handling single click on a thumbnail)
    }

    private void OnDoubleClick(ThumbnailModel thumbnail)
    {
        // ... (Your logic for handling double click on a thumbnail)
    }
    
    private ObservableCollection<ThumbnailModel> _thumbnailsItems = new ObservableCollection<ThumbnailModel>();
    
    
    //[Reactive]
    public ObservableCollection<ThumbnailModel> ThumbnailsItems
    {
        get => _thumbnailsItems;
        set => this.RaiseAndSetIfChanged(ref _thumbnailsItems, value);
    }
    
    
    public ReactiveCommand<Unit, Unit> OnThumbnailPointerEnter { get; }
    
    public event PropertyChangedEventHandler PropertyChanged;
    public event Action<ThumbnailViewModel> MouseHovered;
    public event Action<ThumbnailViewModel> Clicked;
    public event Action<ThumbnailViewModel> DoubleClicked;

    private byte[] _thumbnail;
    public byte[] Thumbnail
    {
        get => _thumbnail;
        set
        {
            _thumbnail = value;
            OnPropertyChanged();
        }
    }

    public ICommand MouseHoverCommand { get; }
    public ICommand ClickCommand { get; }
    public ICommand DoubleClickCommand { get; }

   

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnMouseHover()
    {
        MouseHovered?.Invoke(this);
    }

    private void OnClick()
    {
        Clicked?.Invoke(this);
    }

    private void OnDoubleClick()
    {
        DoubleClicked?.Invoke(this);
    }
    
    
    /*public event PropertyChangedEventHandler PropertyChanged;
    public event Action<ThumbnailViewModel> MouseHovered;
    public event Action<ThumbnailViewModel> Clicked;
    public event Action<ThumbnailViewModel> DoubleClicked;

    private byte[] _thumbnail;
    public byte[] Thumbnail
    {
        get => _thumbnail;
        set
        {
            _thumbnail = value;
            OnPropertyChanged();
        }
    }

    public object Thumbnails { get; }

    public void OnThumbnailPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (sender is Image image && image.DataContext is ThumbnailViewModel viewModel)
        {
            if (e.ClickCount == 1)
            {
                viewModel.OnClick();
            }
            else if (e.ClickCount == 2)
            {
                viewModel.OnDoubleClick();
            }
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void OnMouseHover()
    {
        MouseHovered?.Invoke(this);
    }

    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void OnDoubleClick()
    {
        DoubleClicked?.Invoke(this);
    }*/
    
}