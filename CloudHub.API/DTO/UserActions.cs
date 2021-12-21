using CloudHub.Business.DTO;
using CloudHub.Domain.Exceptions;

namespace CloudHub.API.DTO
{
    public struct UserActionCreationParamsJson : IJson<UserActionCreationParams>
    {
        public string category { get; set; }
        public string description { get; set; }
        public int? app_version { get; set; }
        public string created_on { get; set; }
        public string payload { get; set; }

        public UserActionCreationParams ToObject()
        {
            UserActionCreationParams result = new UserActionCreationParams();
            if (category == null) { throw new MissingParameterException("[category] missing"); }
            if (description == null) { throw new MissingParameterException("[description] missing"); }
            if (payload == null) { throw new MissingParameterException("[payload] missing"); }

            if (created_on != null)
            {
                result.CreatedOn = DateTime.Parse(created_on).ToUniversalTime();
            };
            result.AppVersion = app_version;
            result.Description = description;
            result.Payload = payload;
            result.Category = category;
            return result;
        }
    }

    public struct ActionsSaveRequest
    {
        public UserActionCreationParamsJson[] actions { get; set; }
    }
}
