namespace CloudHub.Domain.Entities
{
    public class Release
    {
        public int Id { get; set; }
        public string ReleaseName { get; set; } = null!;
        public string Notes { get; set; } = null!;
        public int ApplicationId { get; set; }
        public DateOnly ReleaseDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public virtual Application Application { get; set; } = null!;
    }
}
