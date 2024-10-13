using weatherMonitoringAndReportingService.Bots;
using weatherMonitoringAndReportingService.Models;

namespace weatherMonitoringAndReportingService.ConfigProcessor
{
    public interface IConfigProcessor
    {
        Dictionary<BotType, BotConfig> ReadFile();
        void Add(BotType type, BotConfig data);
        void Remove(BotType type);
        void Update(BotType type, BotConfig data);
    }
}
