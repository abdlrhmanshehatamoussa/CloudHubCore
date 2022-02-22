using CloudHub.Domain.Models;
namespace CloudHub.ServiceProvider.Data
{
    internal class LoginTypeMapper : LookupMapper<LoginType, ELoginTypes>
    {
        protected override string TableName => "login_types";
    }
}
