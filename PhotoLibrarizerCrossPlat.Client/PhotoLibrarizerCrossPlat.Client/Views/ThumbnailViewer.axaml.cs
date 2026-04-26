using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using PhotoLibrarizerCrossPlat.Client.ViewModels;

namespace PhotoLibrarizerCrossPlat.Client.Views;

public partial class ThumbnailViewer : UserControl
{
    
    public static readonly StyledProperty<ObservableCollection<ThumbnailModel>> ThumbnailsItemsProperty =
        AvaloniaProperty.Register<ThumbnailViewer, ObservableCollection<ThumbnailModel>>(nameof(ThumbnailsItems),  new ObservableCollection<ThumbnailModel>());
        

    //private ObservableCollection<ThumbnailModel> _thumbnailsItems = new ObservableCollection<ThumbnailModel>();3
    public ObservableCollection<ThumbnailModel> ThumbnailsItems
    {
        get => GetValue(ThumbnailsItemsProperty);
        set => SetValue(ThumbnailsItemsProperty, value);
    }
    
    public ThumbnailViewer()
    {
        InitializeComponent();
        //DataContext = new ThumbnailViewModel(); 
        
    }
}