


using System.Configuration;
using System.Reflection;

namespace PhotoLibrazierCore.Tools.CliConfiguration
{
    public class LoaderDraft
    {
        public LoaderDraft()
        {
        }

        private void UpdateSetting(string key, string value)
        {


            var hjhj = Assembly.GetExecutingAssembly().Location;
            Configuration configuration = ConfigurationManager.
                OpenExeConfiguration(hjhj);

            var h = configuration.AppSettings;

            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }

        public void FirstDraft() 
        {
            UpdateSetting("uno", "dos");
           


           

        }
    }
}
