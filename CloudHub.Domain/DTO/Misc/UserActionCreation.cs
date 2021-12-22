using CloudHub.Domain.Entities;

namespace CloudHub.Domain.DTO
{
    public class UserActionCreation
    {
        public UserActionCreation(string category, string description, int? app_version, string created_on, string payload)
        {
            ValidationUtils.Mandatory(description, nameof(description));
            ValidationUtils.Mandatory(category, nameof(category));
            ValidationUtils.Mandatory(payload, nameof(payload));

            this.category = category;
            this.description = description;
            this.app_version = app_version;
            this.created_on = created_on;
            this.payload = payload;
        }


        public string category { get; set; } = null!;
        public string description { get; set; } = null!;
        public int? app_version { get; set; }
        public string created_on { get; set; } = null!;
        public string payload { get; set; } = null!;


        public UserAction ToModel(int applicationId)
        {
            DateTime createdOn = DateTime.UtcNow;
            if (created_on != null)
            {
                createdOn = DateTime.Parse(created_on).ToUniversalTime();
            }
            return new UserAction()
            {
                ApplicationId = applicationId,
                AppVersion = app_version,
                Category = category,
                Description = description,
                Payload = payload,
                CreatedOn = createdOn
            };
        }
    }
}
