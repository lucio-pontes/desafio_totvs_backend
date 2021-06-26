using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using R3ctApp.Api.Models;
using R3ctApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace R3ctApp.Tests.Api.Controllers
{
    public class UnitTestHireJobsController
        : IClassFixture<CustomWebApplicationFactory<R3ctApp.Startup>>
    {

        private readonly CustomWebApplicationFactory<R3ctApp.Startup> _factory;

        public UnitTestHireJobsController(CustomWebApplicationFactory<R3ctApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHireJobs_ReturnListHireJobs()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirejobs");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetHireJobs_ReturnHireJob_WithHireJobId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirejobs/1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutHireJob_ReturnNoContent_WithHireJob()
        {
            var client = _factory.CreateClient();

            HireJob hireJob = new HireJob
            {
                Id = 1,
                Description = "Analista de Sistemas FullStack",
                Status = "Active"
            };

            string json = JsonConvert.SerializeObject(hireJob);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/hirejobs/1", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task PostHireJob_ReturnHireJob_WithHireJob()
        {
            var client = _factory.CreateClient();

            HireJob hireJob = new HireJob
            {
                Description = "Desenvolvedor C#, Xamarim com conhecimentos em UX",
                Status = "Active"
            };

            string json = JsonConvert.SerializeObject(hireJob);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/hirejobs", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteHireJob_ReturnNotContent_WithHireJobId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/hirejobs/3");

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task GetHireProcess_ReturnListHireProcess_WithHireJobId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirejobs/1/hireprocess");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

    }
}
