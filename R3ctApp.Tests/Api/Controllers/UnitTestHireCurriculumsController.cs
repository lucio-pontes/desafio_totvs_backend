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
    public class UnitTestHireCurriculumsController
        : IClassFixture<CustomWebApplicationFactory<R3ctApp.Startup>>
    {
        
        private readonly CustomWebApplicationFactory<R3ctApp.Startup> _factory;

        public UnitTestHireCurriculumsController(CustomWebApplicationFactory<R3ctApp.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetHireCurriculums_ReturnListHireCurriculums()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirecurriculums");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetHireCurriculum_ReturnCurriculumResp_WithHireCurriculumId()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/hirecurriculums/1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task PutHireCurriculum_ReturnNoContent_WithHireCurriculum()
        {
            var client = _factory.CreateClient();

            HireCurriculum hireCurriculum = new HireCurriculum
            {
                Id = 1,
                HireCandidateId = 1,
                WorksHistory = "Red-tie Technology",
                AcademicsHistory = "Faculdade Sem Nome",
                Courses = "Nenhum",
                Summary = ""
            };

            string json = JsonConvert.SerializeObject(hireCurriculum);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("api/hirecurriculums/1", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }

        [Fact]
        public async Task PostHireCurriculum_ReturnCurriculum_WithHireCurriculum()
        {
            var client = _factory.CreateClient();

            HireCurriculum curriculo = new HireCurriculum
            {
                HireCandidateId = 2,
                WorksHistory = "MineTech",
                AcademicsHistory = "Faculdade Polisampa",
                Courses = "Nenhum",
                Summary = ""
            };

            string json = JsonConvert.SerializeObject(curriculo);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/hirecurriculums", httpContent);

            Assert.Equal(System.Net.HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteHireCurriculo_ReturnNotContent_WithHireCurriculoId()
        {
            var client = _factory.CreateClient();

            var response = await client.DeleteAsync("api/hirecurriculums/3");

            Assert.Equal(System.Net.HttpStatusCode.NoContent,
                response.StatusCode);
        }
    }
}
