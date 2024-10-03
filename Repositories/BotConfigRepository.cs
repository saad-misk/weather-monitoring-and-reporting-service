using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace weatherMonitoringAndReportingService.Repositories
{
    public class BotConfigRepository
    {
        private static readonly BotConfigRepository _instance = new BotConfigRepository();

        private const string _configFilePath = @"Config.json";

        public static BotConfigRepository Instance { get { return _instance; } }

        private BotConfigRepository() 
        {
            LoadConfigFromFile(_configFilePath);
        }

        private void LoadConfigFromFile(string configFilePath)
        {
            string configJson = File.ReadAllText(configFilePath);
            _bots = JsonConvert.Dese
        }
    }
}
