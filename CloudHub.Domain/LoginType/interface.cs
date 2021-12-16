namespace CloudHub.Domain
{
    public interface ILoginType
    {
        int ID { get; set; }

        string Name  { get; set; }

        bool Active { get; set; }
    }
}
