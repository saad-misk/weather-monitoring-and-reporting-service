using Newtonsoft.Json;

namespace weatherMonitoringAndReportingService.AppSettings
{
    public class AppSettingsInitializer
    {
        private static AppSettingsModel? _appSettings;

        private AppSettingsInitializer()
        {
            string  appSettingsJson = File.ReadAllText("../../../../weather-monitoring-and-reporting-service/AppSettings/appsettings.json");
            _appSettings = JsonConvert.DeserializeObject<AppSettingsModel>(appSettingsJson);
        }

        public static AppSettingsModel AppSettingsInstance()
        {
            if (_appSettings == null) new AppSettingsInitializer();
            return _appSettings!;
        }
    }
}
