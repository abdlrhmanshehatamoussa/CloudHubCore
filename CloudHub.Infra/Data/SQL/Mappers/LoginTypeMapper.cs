using CloudHub.Domain.Entities;
namespace CloudHub.Infra.Data
{
    internal class LoginTypeMapper : LookupMapper<LoginType, LoginTypeValues>
    {
        protected override string TableName => "login_types";
    }
}
