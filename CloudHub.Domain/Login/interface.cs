namespace CloudHub.Domain
{
    public interface ILogin
    {
        int ID { get; set; }

        int UserId { get; set; }

        string Passcode { get; set; }

        int LoginTypeId { get; set; }

        IUser User { get; set; }

        ILoginType LoginType { get; set; }
    }
}
