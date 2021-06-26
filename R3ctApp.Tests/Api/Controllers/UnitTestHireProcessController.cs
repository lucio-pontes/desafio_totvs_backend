using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using R3ctApp.Api.Models;
using R3ctApp.Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace R3ctApp.Tests.Api.Controllers
{
    public class UnitTestHireProcessController
        : IClassFixture<CustomWebApplicationFactory<R3ctApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<R3ctApp.Startup> _factory;

        public UnitTestHireProcessController(CustomWebApplicationFactory<R3ctApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHireProcess_ReturnListHireProcessResp_WithHireJobId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hireprocess/hirejobs/1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetHireProcess_ReturnHireProcessResp_WithHireProcessId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hireprocess/1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task PostHireProcess_ReturnHireProcessResp()
        {
            var client = _factory.CreateClient();

            HireProcess hireProcess = new HireProcess
            {
                HireCandidateId = 1,
                HireJobId = 2,
                Status = "Active"
            };

            string json = JsonConvert.SerializeObject(hireProcess);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/hireprocess", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteHireProcess_ReturnNotContent_WithHireProcessId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/hireprocess/3");

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }
    }
}
