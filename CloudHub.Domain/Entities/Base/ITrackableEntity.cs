namespace CloudHub.Domain.Entities
{
    public interface ITrackableEntity
    {
        public bool Active { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
