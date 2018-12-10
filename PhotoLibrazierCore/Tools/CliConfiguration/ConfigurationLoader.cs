using System.IO;
using PhotoLibrazierCore.Tools.Serialization;

namespace PhotoLibrazierCore.Tools.CliConfiguration
{
    public class ConfigurationLoader
    {
       

        public ConfigurationLoader(ISerialization iSerialize)
        {
            this.iSerialize = iSerialize;
        }

        ISerialization iSerialize;
        string FileConfigPath = @"../configuration.json";

        public CliConfigurationModel RunAndGetModel() 
        {
            if(!File.Exists(FileConfigPath))
            {
                FirstDraft();
            }
            var r = new StreamReader(FileConfigPath);
            var myJson = r.ReadToEnd();

            return iSerialize.ToObjectByString<CliConfigurationModel>(myJson);

        }




        public void FirstDraft() 
        {
            var cliConfig= new CliConfigurationModel();

            cliConfig.LibraryPath = "";

            var json= iSerialize.ToJsonByObject(cliConfig);

            System.IO.File.WriteAllText(@"../configuration.json", json);
        }
    }
}
