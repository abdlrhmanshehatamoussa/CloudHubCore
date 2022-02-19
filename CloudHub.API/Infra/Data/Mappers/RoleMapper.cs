using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    internal class RoleMapper : LookupMapper<Role, ERoles>
    {
        protected override string TableName => "user_roles";
    }
}