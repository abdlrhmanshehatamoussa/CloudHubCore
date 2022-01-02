using CloudHub.Domain.Entities;

namespace CloudHub.Infra.Data
{
    internal class CollectionTypesMapper : LookupMapper<CollectionType, CollectionTypeValues>
    {
        protected override string TableName => "collection_types";
    }
}