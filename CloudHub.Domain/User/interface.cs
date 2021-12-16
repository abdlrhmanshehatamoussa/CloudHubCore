namespace CloudHub.Domain
{
    public interface IUser
    {

        int ID { get; set; }

        string Name { get; set; }

        string? ImageUrl { get; set; }

        string Email { get; set; }

        string GlobalId { get; set; }

        int ApplicationId { get; set; }


        DateTime ModifiedOn { get; set; }


        DateTime CreatedOn { get; set; }

        bool Active { get; set; }

        ILogin Login { get; set; }

        //List<Purchases> Purchases { get; set; }

        //List<IUserToken> UserTokens { get; set; }

        IApplication Application { get; set; }
    }

    public struct RegisterDTO
    {
        public RegisterDTO(string imageUrl, string email, string password, int loginTypeId, string name)
        {
            ImageUrl = imageUrl;
            Email = email;
            Password = password;
            LoginTypeId = loginTypeId;
            Name = name;
        }

        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int LoginTypeId { get; set; }
        public string Name { get; set; }
    }
  
    public struct LoginDTO
    {
        public LoginDTO(string imageUrl, string email, string password, int loginTypeId, string name)
        {
            ImageUrl = imageUrl;
            Email = email;
            Password = password;
            LoginTypeId = loginTypeId;
            Name = name;
        }

        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int LoginTypeId { get; set; }
        public string Name { get; set; }
    }
}