using System;
using System.Configuration;


namespace PhotoLibrazierCore.Tools.CliConfiguration
{
    public class Loader
    {
        public Loader()
        {
        }

        public void FirstDraft() 
        {
            strhostnameLoad = ConfigurationManager.AppSettings["hostname"].ToString();


            ;

            /* = System.ConfigurationManager
            .OpenExeConfiguration(Application.ExecutablePath);

            config.AppSettings.Settings.Remove("MySetting");
            config.AppSettings.Settings.Add("MySetting", "some value");

            config.Save(ConfigurationSaveMode.Modified);-*/

        }
    }
}
