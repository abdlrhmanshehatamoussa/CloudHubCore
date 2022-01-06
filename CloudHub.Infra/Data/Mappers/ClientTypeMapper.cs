using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    internal class ClientTypeMapper : LookupMapper<ClientType, EClientTypes>
    {
        protected override string TableName => "client_types";
    }
}