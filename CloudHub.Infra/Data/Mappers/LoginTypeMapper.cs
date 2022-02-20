using CloudHub.Domain.Models;
namespace CloudHub.Infra.Data
{
    internal class LoginTypeMapper : LookupMapper<LoginType, ELoginTypes>
    {
        protected override string TableName => "login_types";
    }
}
