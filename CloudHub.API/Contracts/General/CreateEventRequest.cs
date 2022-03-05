using CloudHub.Domain.DTO;
using CloudHub.Domain.Exceptions;

namespace CloudHub.API.Contracts
{
#pragma warning disable IDE1006 // Naming Styles
    public class CreateEventRequest
    {
        public CreateEventRequest(string description, string category, string build_id, string? source, string? payload, string? created_on)
        {
            if (description == null) throw new MissingParameterException();
            if (category == null) throw new MissingParameterException();
            if (build_id == null) throw new MissingParameterException();
            this.description = description;
            this.category = category;
            this.build_id = build_id;
            this.source = source;
            this.payload = payload;
            this.created_on = created_on;
        }

        public string description { get; set; }
        public string category { get; set; }
        public string build_id { get; set; }
        public string? source { get; set; }
        public string? payload { get; set; }
        public string? created_on { get; set; }

        public CreateEventDTO ToDTO() => new()
        {
            Category = category,
            Description = description,
            Payload = payload,
            CreatedOn = created_on,
            Source = source,
            BuildId = build_id
        };
    }
}
