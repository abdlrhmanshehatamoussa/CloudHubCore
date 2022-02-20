using CloudHub.Domain.DTO;

namespace CloudHub.API.Contracts
{
    public struct PingResponseContract
    {
        public string timestamp { get; set; }
        public string build_id { get; set; }
        public bool production_mode { get; set; }
        public string environment { get; set; }

        public static PingResponseContract FromDTO(PingDTO dto)
        {
            return new PingResponseContract()
            {
                timestamp = dto.TimeStamp,
                production_mode = dto.EnvironmentSettings.IsProductionModeEnabled,
                environment = dto.EnvironmentSettings.EnvironmentName,
                build_id = dto.EnvironmentSettings.BuildId
            };
        }
    }
}
