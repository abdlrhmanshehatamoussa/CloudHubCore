namespace CloudHub.Domain.Models
{
    public interface ILookupEntity<T>: ITrackableEntity where T : struct
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
