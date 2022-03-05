using CloudHub.API.Contracts;
using CloudHub.Utils;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudHub.Tests.Integration
{
    internal class EventsTests : IntegrationTest
    {
        [Test]
        public async Task LogSingle_HappyScenario()
        {
            //Arrange
            CreateEventRequest eventContract = new
            (
                build_id: "1",
                category: "category",
                created_on: "2022-01-13 18:13:06.623782",
                description: "desc",
                payload: null,
                source: "source1"
            );
            string nonce = await GetNonce();

            //Act
            HttpResponseMessage response = await _myHttpClient.PostAsyncJson("events", eventContract, BuildHeaders(nonce));

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That((int)response.StatusCode == 204);
        }


        [Test]
        public async Task LogBulk_HappyScenario()
        {
            //Arrange
            List<CreateEventRequest> eventContracts = new()
            {
                new(
                    build_id: "1",
                    category: "category",
                    created_on: "2022-01-13 18:13:06.623782",
                    description: "desc",
                    payload: null,
                    source: "source1"
                    ),
                new(
                    build_id: "2",
                    category: "category2",
                    description: "desc",
                    created_on: null,
                    payload: null,
                    source: null
                    )
            };
            string nonce = await GetNonce();

            //Act
            HttpResponseMessage response = await _myHttpClient.PatchAsyncJson("events", eventContracts, BuildHeaders(nonce));

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That((int)response.StatusCode == 204);
        }
    }
}
