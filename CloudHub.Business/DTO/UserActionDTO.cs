using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;

namespace CloudHub.Business.DTO
{
    public struct UserActionCreationParams
    {
        public UserActionCreationParams(string category, string description, int? appVersion, DateTime createdOn, string payload)
        {
            if (category == null) { throw new MissingParameterException("[category] missing"); }
            if (description == null) { throw new MissingParameterException("[description] missing"); }
            if (payload == null) { throw new MissingParameterException("[payload] missing"); }
            Category = category;
            Description = description;
            AppVersion = appVersion;
            CreatedOn = createdOn;
            Payload = payload;
        }

        public string Category { get; set; }
        public string Description { get; set; }
        public int? AppVersion { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Payload { get; set; }

        public UserAction ToModel(int applicationId)
        {
            return new UserAction()
            {
                ApplicationId = applicationId,
                AppVersion = AppVersion,
                Category = Category,
                Description = Description,
                Payload = Payload,
                CreatedOn = CreatedOn
            };
        }
    }
}
