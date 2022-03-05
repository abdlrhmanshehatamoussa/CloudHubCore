using CloudHub.Domain.DTO;
using CloudHub.Domain.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudHub.Tests.Unit.Domain
{
    internal class EventsTests : DomainUnitTest
    {
        struct TestPayload
        {
            public string P1 { get; set; }
            public string P2 { get; set; }
            public string P3 { get; set; }

            public bool Exact(TestPayload payload)
            {
                return payload.P1 == P1 && payload.P2 == P2 && payload.P3 == P3;
            }
        };
        [Test]
        public async Task LogSingle_HappyScenario()
        {
            //Arrange
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();
            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret)
            };
            TestPayload inputPayload = new() { P1 = "v1", P2 = "v2", P3 = "v3" };
            CreateEventDTO dto = new()
            {
                BuildId = "1.0",
                Category = "Category1",
                Description = "desc",
                Source = "source",
                Payload = JsonConvert.SerializeObject(inputPayload),
                CreatedOn = "2022-01-13 18:13:06.623782",
            };

            //Act
            Event e = await EventsService.LogSingle(credentials, dto);

            //Assert
            Assert.IsNotNull(e);
            Assert.True(e.TenantId == client.TenantId);
            Assert.True(e.Description == dto.Description);
            Assert.True(e.Category == dto.Category);
            Assert.True(e.BuildId == dto.BuildId);
            Assert.True(e.Source == dto.Source);
            Assert.IsNotNull(e.Payload);
            TestPayload resultPayload = e.Payload!.Deserialize<TestPayload>();
            Assert.True(resultPayload.Exact(inputPayload));
            Assert.True(e.CreatedOn.Year == 2022 && e.CreatedOn.Month == 1 && e.CreatedOn.Day == 13 && e.CreatedOn.Hour == 18);
        }

        [Test]
        public async Task LogSingle_OnlyMandatory()
        {
            //Arrange
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();
            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret)
            };
            CreateEventDTO dto = new()
            {
                BuildId = "1.0",
                Category = "Category1",
                Description = "desc"
            };
            DateTime today = DateTime.UtcNow;

            //Act
            Event e = await EventsService.LogSingle(credentials, dto);

            //Assert
            Assert.IsNotNull(e);
            Assert.True(e.TenantId == client.TenantId);
            Assert.True(e.Description == dto.Description);
            Assert.True(e.Category == dto.Category);
            Assert.True(e.BuildId == dto.BuildId);
            Assert.Null(e.Payload);
            Assert.True(e.CreatedOn.Year == today.Year
                && e.CreatedOn.Month == today.Month
                && e.CreatedOn.Day == today.Day);
            Assert.Null(e.Source);
        }

        [Test]
        public async Task LogBulk_HappyScenario()
        {
            //Arrange
            Client client = NewClient();
            await UnitOfWork.ClientsRepository.Add(client);
            await UnitOfWork.Save();

            Nonce nonce = NewNonce(client.Id);
            await UnitOfWork.NoncesRepository.Add(nonce);
            await UnitOfWork.Save();
            ConsumerCredentials credentials = new()
            {
                ClientKey = client.ClientKey,
                Nonce = EncryptionService.Encrypt(nonce.Token, client.ClientSecret)
            };
            List<CreateEventDTO> dtos = new()
            {
                new()
                {
                    BuildId = "1",
                    Category = "Category1",
                    Description = "desc1"
                },
                new()
                {
                    BuildId = "2",
                    Category = "Category2",
                    Description = "desc2",
                    CreatedOn = "2022-01-13 18:13:06.623782"
                }
            };
            DateTime today = DateTime.UtcNow;

            //Act
            await EventsService.LogBulk(credentials, dtos);

            //Assert
            List<Event> events = await UnitOfWork.EventsRepository.GetAll();
            Assert.AreEqual(2, events.Count);
            Event? e1 = events.FirstOrDefault(e => e.BuildId == "1");
            Event? e2 = events.FirstOrDefault(e => e.BuildId == "2");

            Assert.NotNull(e1);
            Assert.True(e1!.Category == "Category1");
            Assert.True(e1.CreatedOn.Year == today.Year);
            Assert.True(e1.CreatedOn.Month == today.Month);
            Assert.True(e1.CreatedOn.Day == today.Day);

            Assert.NotNull(e2);
            Assert.True(e2!.Category == "Category2");
            Assert.True(e2.CreatedOn.Year == 2022);
            Assert.True(e2.CreatedOn.Month == 1);
            Assert.True(e2.CreatedOn.Day == 13);
        }
    }
}
