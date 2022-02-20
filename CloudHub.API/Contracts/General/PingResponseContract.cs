using CloudHub.Domain.DTO;

namespace CloudHub.API.Contracts
{
    public struct PingResponseContract
    {
        public PingResponseContract(string timestamp, string build_id, bool production_mode, string environment)
        {
            this.timestamp = timestamp;
            this.build_id = build_id;
            this.production_mode = production_mode;
            this.environment = environment;
        }

        public string timestamp { get; set; }
        public string build_id { get; set; }
        public bool production_mode { get; set; }
        public string environment { get; set; }
    }
}
