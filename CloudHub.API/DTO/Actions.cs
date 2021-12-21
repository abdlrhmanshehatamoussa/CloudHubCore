using CloudHub.Business.DTO;

namespace CloudHub.API.DTO
{
    public struct UserActionCreationParamsJson: IJson<UserActionCreationParams>
    {
        public string category { get; set; }
        public string description { get; set; }
        public string app_version { get; set; }
        public string created_on { get; set; }
        public string payload { get; set; }

        public UserActionCreationParams ToObject()
        {
            return new UserActionCreationParams()
            {
                AppVersion = app_version,
                Category = category,
                CreatedOn = DateTime.Parse(created_on),
                Payload = payload,
                Description = description
            };
        }
    }

    public struct ActionsSaveRequest
    {
        public UserActionCreationParamsJson[] actions { get; set; }
    }
}
