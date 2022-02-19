namespace CloudHub.Domain.Models
{
    public interface ITrackableEntity
    {
        public bool Active { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
