using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    internal class AdminTypesMapper : LookupMapper<AdminType, EAdminTypes>
    {
        protected override string TableName => "admin_types";
    }
}