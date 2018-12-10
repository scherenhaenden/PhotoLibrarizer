


using System.Configuration;
using System.Reflection;

namespace PhotoLibrazierCore.Tools.CliConfiguration
{
    public class LoaderDraft
    {
        public LoaderDraft()
        {
        }

        private static void UpdateSetting(string key, string value)
        {


            var hjhj = Assembly.GetExecutingAssembly().Location;
            Configuration configuration = ConfigurationManager.
                OpenExeConfiguration(hjhj);

            var h = configuration.AppSettings;
            var entry = configuration.AppSettings.Settings[key];

            if (entry == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            else
            {
                configuration.AppSettings.Settings[key].Value = value;

            }


            configuration.Save(ConfigurationSaveMode.Modified);
            configuration.Save();


            ConfigurationManager.RefreshSection("appSettings");
        }


        private void UpdateSetting2(string key, string value)
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
            var cliConfig= new CliConfigurationModel();

            cliConfig.LibraryPath = "";






        }
    }
}
