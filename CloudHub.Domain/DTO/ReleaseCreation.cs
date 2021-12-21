using CloudHub.Domain.Entities;

namespace CloudHub.Domain.DTO
{
    public class ReleaseCreation
    {
        public ReleaseCreation(string release_name, string notes, string release_date)
        {
            ValidationUtils.Mandatory(release_name, nameof(release_name));
            ValidationUtils.Mandatory(notes, nameof(notes));
            ValidationUtils.ValidDateOnly(release_date, nameof(release_date));

            this.release_name = release_name;
            this.notes = notes;
            this.release_date = release_date;
        }

        public string release_name { get; set; }
        public string notes { get; set; }
        public string release_date { get; set; }

        public Release ToModel(int applicationId)
        {
            Release release = new Release();
            
            release.ApplicationId = applicationId;
            release.Notes = notes;
            release.ReleaseName = release_name;
            
            DateOnly date;
            bool validDate = DateOnly.TryParse(release_date, out date);
            if (validDate) { release.ReleaseDate = date; }

            return release;
        }
    }
}