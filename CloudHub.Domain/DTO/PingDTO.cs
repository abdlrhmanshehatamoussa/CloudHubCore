using CloudHub.Domain.Models;

namespace CloudHub.Domain.DTO
{
    public class PingDTO
    {
        public PingDTO(string timeStamp, IEnvironmentSettings environmentSettings)
        {
            TimeStamp = timeStamp;
            EnvironmentSettings = environmentSettings;
        }

        public string TimeStamp { get; set; }
        public IEnvironmentSettings EnvironmentSettings { get; set; }
    }
}
