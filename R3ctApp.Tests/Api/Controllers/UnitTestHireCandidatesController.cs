using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using R3ctApp.DataAccess;
using System;
using System.Threading.Tasks;
using R3ctApp.Api.Controllers;
using R3ctApp.Api.Models;
using Xunit;
using Newtonsoft.Json;
using System.Net.Http;

namespace R3ctApp.Tests.Api.Controllers
{
    public class UnitTestHireCandidatesController
        : IClassFixture<CustomWebApplicationFactory<R3ctApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<R3ctApp.Startup> _factory;

        public UnitTestHireCandidatesController(CustomWebApplicationFactory<R3ctApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHireCandidates_ReturnListHireCandidates()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirecandidates");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetHireCandidate_ReturnHireCandidate_WithHireCandidateId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirecandidates/1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutHireCandidate_ReturnNoContent_WithHireCandidato()
        {
            var client = _factory.CreateClient();

            HireCandidate hireCandidate = new HireCandidate
            {
                Id = 1,
                Name = "Maria Aparecida",
                Age = 20,
                City = "Belo Horizonte",
                Phone = "(31) 98888-9999",
                Email = "maria@tempmail.com",
                Status = "Active"
            };

            string json = JsonConvert.SerializeObject(hireCandidate);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/hirecandidates/1", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task PostHireCandidate_ReturnHireCandidate_WithHireCandidate()
        {
            var client = _factory.CreateClient();

            HireCandidate hireCandidate = new HireCandidate
            {
                Name = "Sabrina Lima",
                Age = 22,
                City = "Belo Horizonte",
                Phone = "(31) 95555-4444",
                Email = "sabrina@tempmail.com"
            };

            string json = JsonConvert.SerializeObject(hireCandidate);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/hirecandidates", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteHireCandidate_ReturnNotContent_WithHireCandidateId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/hirecandidates/3");

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task GetHireCurriculum_ReturnHireCurriculum_WithHireCandidateId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirecandidates/1/hirecurriculum");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}