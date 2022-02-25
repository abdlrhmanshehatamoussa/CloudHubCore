using CloudHub.Domain.DTO;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;

namespace CloudHub.Domain.Models
{
    public class User : IBaseEntity, ITrackableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Email { get; set; } = null!;
        public string GlobalId { get; set; } = null!;
        public int TenantId { get; set; }
        public bool Active { get; set; } = false;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public virtual Login Login { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
        public virtual ICollection<UserToken> UserTokens { get; set; } = new HashSet<UserToken>();
        public virtual ICollection<PrivateDocument> PrivateDocuments { get; set; } = new HashSet<PrivateDocument>();
        public virtual Tenant Tenant { get; set; } = null!;



        public async Task CreateLogin(CreateUserDTO dto, IOAuthService _oAuthService)
        {
            string passcode;
            if (dto.login_type == ELoginTypes.LOGIN_TYPE_BASIC)
            {
                passcode = dto.password;
            }
            else
            {
                OAuthUser? oAuthUser = await _oAuthService.GetUserByToken(dto.password, dto.login_type);
                if (oAuthUser == null || oAuthUser.Email != dto.email) { throw new NotAuthenticatedException(); }
                passcode = oAuthUser.OpenId;
            }
            /*TODO: Encrypt password before saving it, using client secret
            #
            string clientSecret = consumerInfo.ClientApplication.Client.ClientSecret;
            login.Passcode = Encrypt(passcode,clientSecret);
            #
            Note: You will have to decrypt the passwords coming from the database in the Login usecase
             */
            this.Login = new() { LoginTypeId = dto.login_type, Passcode = passcode };
        }

        public void GenerateGlobalId(IEncryptionService _encryptionService)
        {
            double timeStamp = DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalMilliseconds;
            GlobalId = _encryptionService.Hash(string.Format("{0}{1}{2}", this.Email, this.TenantId, timeStamp));
        }

        //Static
        public static async Task<User> FromDTO(CreateUserDTO dto, int tenantId, IEncryptionService encryptionService, IOAuthService oAuthService)
        {
            User user = new()
            {
                Email = dto.email,
                Name = dto.name,
                ImageUrl = dto.image_url,
                TenantId = tenantId
            };
            user.GenerateGlobalId(encryptionService);
            await user.CreateLogin(dto, oAuthService);
            return user;
        }
    }
}
