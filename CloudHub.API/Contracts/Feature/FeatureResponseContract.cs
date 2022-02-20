using CloudHub.Domain.Models;

namespace CloudHub.API.Contracts
{
    public class FeatureResponseContract
    {
        public FeatureResponseContract(string feature_id, string feature_name, string feature_description, bool active)
        {
            this.feature_id = feature_id;
            this.feature_name = feature_name;
            this.feature_description = feature_description;
            this.active = active;
        }

        public string feature_id { get; set; }
        public string feature_name { get; set; }
        public string feature_description { get; set; }
        public bool active { get; set; }

        public static FeatureResponseContract FromFeature(Feature f)
        {
            return new(
                feature_name: f.Name,
                feature_description: f.Description,
                feature_id: f.GlobalId.ToString(),
                active: f.Active
            );
        }
    }
}
