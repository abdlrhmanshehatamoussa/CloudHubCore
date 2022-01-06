namespace CloudHub.Domain.Entities
{
    public class Admin : IBaseTrackableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public EAdminTypes AdminTypeId { get; set; }
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual AdminType AdminType { get; set; } = null!;
    }
}