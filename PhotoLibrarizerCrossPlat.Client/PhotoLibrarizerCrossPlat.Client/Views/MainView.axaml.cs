using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using PhotoLibrarizerCrossPlat.Client.Events;
using PhotoLibrarizerCrossPlat.Client.ViewModels;

namespace PhotoLibrarizerCrossPlat.Client.Views
{
    public partial class MainView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;

        public MainView()
        {
            InitializeComponent();
            _eventAggregator = new EventAggregator();
            this.KeyDown += MainView_OnKeyDown;
            DataContext = new MainViewModel(_eventAggregator);
        }

        private void MainView_OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                _eventAggregator.Publish(new NextImageEvent());
            }
            else if (e.Key == Key.Left)
            {
                _eventAggregator.Publish(new PreviousImageEvent());
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}