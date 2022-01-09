using CloudHub.Domain.Entities;
using CloudHub.Domain.Exceptions;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace CloudHub.Tests.Data
{
    public class PrivateDataTests
    {
        private PrivateDataService publicDataService = null!;
        private const string ClientKey = "ce7c48fc-fcb2-4f0c-be20-2e88e94f380f";
        private const string Nonce = "e9d6fdbd-be67-49fe-badb-a81415f27ef211055fbe-a5a7-4b2a-a6ac-ddfb1561afc5bd478363-620e-446b-b48e-12ad7cd0a25d";
        private const string UserToken = "73c4bb98-953a-41a3-b3c3-d131faba1d212f339435-5e58-4476-afb8-3d41fbac4d0b";

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<PostgreDatabase> builder = new();
            builder.UseNpgsql(Constants.PSQL_HOST);
            UnitOfWork uow = new(new PostgreDatabase(builder.Options));
            publicDataService = new PrivateDataService(uow, new TestSettings());
        }

        [Test]
        public void FetchAll_InvalidCollectionName()
        {
            ConsumerCredentials consumerCredentials = new() { ClientKey = ClientKey, Nonce = Nonce, UserToken = UserToken };
            Assert.ThrowsAsync<InvalidCollectionException>(async () =>
            {
                List<PrivateDocument> result = await publicDataService.FetchAll(consumerCredentials, "");
            });
        }

        [Test]
        public void FetchAll_HappeScenario()
        {
            ConsumerCredentials consumerCredentials = new() { ClientKey = ClientKey, Nonce = Nonce, UserToken = UserToken };
            Assert.DoesNotThrowAsync(async () =>
            {
                List<PrivateDocument> results = await publicDataService.FetchAll(consumerCredentials, "comments");
            });
        }
    }
}
