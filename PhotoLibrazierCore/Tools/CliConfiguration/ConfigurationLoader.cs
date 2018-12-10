using PhotoLibrazierCore.Tools.Serialization;

namespace PhotoLibrazierCore.Tools.CliConfiguration
{
    public class LoaderDraft
    {
        public LoaderDraft()
        {
        }

       

        public void FirstDraft() 
        {
            var cliConfig= new CliConfigurationModel();

            cliConfig.LibraryPath = "";

            var json=new ToJson().ByObject(cliConfig);

            System.IO.File.WriteAllText(@"../configuration.json", json);
        }
    }
}
