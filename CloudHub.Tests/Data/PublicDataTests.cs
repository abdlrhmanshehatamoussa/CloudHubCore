using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CloudHub.Tests.Data
{
    public class PublicDataTests
    {
        private PublicDataService publicDataService = null!;
        private const string ClientKey = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";
        private const string Nonce = "e9d6fdbd-be67-49fe-badb-a81415f27ef211055fbe-a5a7-4b2a-a6ac-ddfb1561afc5bd478363-620e-446b-b48e-12ad7cd0a25d";

        [SetUp]
        public void Setup()
        {
            Mock<ITenantsService> mock = new Mock<ITenantsService>();
            mock.Setup(x => x.CurrentTenant).Returns(new Tenant() { ConnectionString = Constants.PSQL_HOST, Id = "1", Name = "" });
            UnitOfWork uow = new(new PostgreContext(mock.Object));
            publicDataService = new PublicDataService(uow, new TestSettings());
        }

        [Test]
        public void FetchAll_InvalidCollectionName()
        {
            ConsumerCredentials consumerCredentials = new() { ClientKey = ClientKey, Nonce = Nonce };
            Assert.ThrowsAsync<InvalidCollectionException>(async () =>
            {
                List<PublicDocument> result = await publicDataService.FetchAll(consumerCredentials, "");
            });
        }

        [Test]
        public void FetchAll_HappeScenario()
        {
            ConsumerCredentials consumerCredentials = new() { ClientKey = ClientKey, Nonce = Nonce };
            Assert.DoesNotThrowAsync(async () =>
            {
                List<PublicDocument> results = await publicDataService.FetchAll(consumerCredentials, "posts");
            });
        }
    }
}
