using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PhotoLibrarizer.Engines.Filters.Models;
using PhotoLibrarizerCrossPlat.Client.ViewModels;

namespace PhotoLibrarizerCrossPlat.Client.Views
{
    public partial class FilterConfigurationView : UserControl
    {
        public FilterConfigurationView()
    {
        //InitializeComponent();
        DataContext = new FilterConfigurationViewModel();
    }

    

        private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

        private void SaveButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Retrieve the values from UI elements and update the FilterModel
        /*var filterModel = new FilterModel
        {
            //*Extensions = ExtensionsComboBox.ToString().Split(',').ToList(),
            PathsForSourceFiles = PathsForSourceFilesTextBox.Text.Split(',').ToList(),
            CamerasShouldBe = CamerasShouldBeTextBox.Text.Split(',').ToList(),
            CamerasShouldNotBe = CamerasShouldNotBeTextBox.Text.Split(',').ToList(),
            MaxFiles = int.Parse(MaxFilesTextBox.Text),
            DestinationModel = new DestinationModel(),
            //BasePath = BasePathTextBox.Text,
            
      
        };*

        // You can now use the filterModel for further processing or saving.
        // For simplicity, you can display the model in a message box.
        //MessageBox.Show(this, filterModel.ToString(), "Saved Configuration", MessageBox.MessageBoxButtons.Ok);
        /*
        <!-- Extensions Text="{Binding Extensions, Mode=TwoWay}"- ->
        
            <!-- PathsForSourceFiles - ->
            <TextBox Name="PathsForSourceFilesTextBox"  PlaceholderText="Paths for Source Files" Text="{Binding PathsForSourceFiles, Mode=TwoWay}"/>
        
            <!-- CamerasShouldBe - ->
            <TextBox Name="CamerasShouldBeTextBox" PlaceholderText="Cameras Should Be" Text="{Binding CamerasShouldBe, Mode=TwoWay}"/>
        
            <!-- CamerasShouldNotBe - ->
            <TextBox Name="CamerasShouldNotBeTextBox" PlaceholderText="Cameras Should Not Be" Text="{Binding CamerasShouldNotBe, Mode=TwoWay}"/>
        
            <!-- MaxFiles - ->
            <TextBox Name="MaxFilesTextBox" PlaceholderText="Max Files" Text="{Binding MaxFiles, Mode=TwoWay}"/>
        
            <!-- BasePath - ->
            <TextBox Name="BasePathTextBox" PlaceholderText="Base Path" Text="{Binding BasePath, Mode=TwoWay}"/>
        
            <!-- Destination - ->
            <ComboBox Name="DestinationComboBox" PlaceholderText="Destination" SelectedIndex="{Binding Destination, Mode=TwoWay}">
            <ComboBoxItem>Base Library Without Date</ComboBoxItem>
            <ComboBoxItem>Base Library With Date</ComboBoxItem>
            <ComboBoxItem>Camera Based Directory Without Date</ComboBoxItem>
            <ComboBoxItem>Camera Based Directory With Date</ComboBoxItem>
            </ComboBox>
        
        
            <Button Name="SaveButton" Content="Save Configuration" Command="{Binding SaveCommand}"/>
                                                                           -->
        */
    }
    }
}