using System.Reactive;
using PhotoLibrarizer.Engines.Filters.Models;
using ReactiveUI;

namespace PhotoLibrarizerCrossPlat.Client.ViewModels
{
    public class FilterConfigurationViewModel : ViewModelBase
    {
        private string extensions;
        private string pathsForSourceFiles;
        private string camerasShouldBe;
        private string camerasShouldNotBe;
        private int maxFiles;
        private string basePath;
        private Destinations destination;

        public string Extensions
        {
            get => extensions;
            set => this.RaiseAndSetIfChanged(ref extensions, value);
        }

        // Add properties for other fields

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }

        public FilterConfigurationViewModel()
    {
        SaveCommand = ReactiveCommand.Create(SaveConfiguration);
    }

        private void SaveConfiguration()
    {
        // Construct FilterModel and perform save action
    }
    }
}