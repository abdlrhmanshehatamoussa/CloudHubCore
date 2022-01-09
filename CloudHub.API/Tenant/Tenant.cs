namespace CloudHub.API.Tenant
{
    public class Tenant
    {
        public Guid Identifier { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}
