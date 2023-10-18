using FakeItEasy;
using FluentAssertions;
using HerrenHaus_API.Logging;
using HerrenHaus_API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerrenHaus_API_IntegrationTests
{
    public class HerrenHausAPIControllerTests:IDisposable
    {
        #region "Initialization"
        private CustomWebApplicationFactory customWebApplicationFactory;
        private HttpClient httpClient;

        public HerrenHausAPIControllerTests()
        {
            customWebApplicationFactory = new CustomWebApplicationFactory();
            httpClient = customWebApplicationFactory.CreateClient();
        }
        public void Dispose()
        {
            customWebApplicationFactory.Dispose();
            httpClient.Dispose();
        }

        #endregion

        #region "GetAllHerrenHaus Integration Tests"

        [Fact]
        public async Task GetAllHerrenHaus_returnList()
        {
            //Arrange
            A.CallTo(() => customWebApplicationFactory.logging.log("",""));
            //Act
            var result = await httpClient.GetAsync("api/HerrenHausAPI");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = JsonConvert.DeserializeObject<IEnumerable<HerrenHausDto>>(await result.Content.ReadAsStringAsync());
            data.Should().NotBeNullOrEmpty();
            data.Should().BeOfType<List<HerrenHausDto>>();
        }

        #endregion

        #region "GetHerrenHausByID Integration Tests"

        [Fact]
        public async Task GetHerrenHausByID_returnOK() {

            //Arrange
            //Act
            var result = await httpClient.GetAsync("api/HerrenHausAPI/1");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = JsonConvert.DeserializeObject<HerrenHausDto>(await result.Content.ReadAsStringAsync());
            data.Should().NotBeNull();
            data.Should().BeOfType<HerrenHausDto>();
        }

        [Fact]
        public async Task GetHerrenHausByID_returnBadRequest()
        {

            //Arrange
            A.CallTo(() => customWebApplicationFactory.logging.log("", ""));
            //Act
            var result = await httpClient.GetAsync("api/HerrenHausAPI/0");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetHerrenHausByID_returnNotFound()
        {
            //Arrange
            //Act
            var result = await httpClient.GetAsync("api/HerrenHausAPI/10");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region "CreateHerrenHaus Integration Tests"

        [Fact]
        public async Task CreateHerrenHaus_return201Created()
        {
            //Arrange
            var input = new HerrenHausDto() { 
                ID=0,
                Name="Samsooma",
                Location="Palestine",
                price="0$"
            };
            //Act
            var result = await httpClient.PostAsync("api/HerrenHausAPI",JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            var data = JsonConvert.DeserializeObject<HerrenHausDto>(await result.Content.ReadAsStringAsync());
            data.Should().NotBeNull();
            data.Should().BeOfType<HerrenHausDto>();
        }

        [Fact]
        public async Task CreateHerrenHaus_NullInput_returnBadRequest()
        {
            //Arrange            
            //Act
            var result = await httpClient.PostAsync("api/HerrenHausAPI", JsonContent.Create(""));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateHerrenHaus_DuplicateName_returnBadRequest()
        {
            //Arrange            
            var input = new HerrenHausDto()
            {
                ID = 0,
                Name = "HerrenHaus am Strand",
                Location = "Palestine",
                price = "0$"
            };
            //Act
            var result = await httpClient.PostAsync("api/HerrenHausAPI", JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateHerrenHaus_returnInternalServerError()
        {
            //Arrange
            var input = new HerrenHausDto()
            {
                ID = 5,
                Name = "Samsooma",
                Location = "Palestine",
                price = "0$"
            };
            //Act
            var result = await httpClient.PostAsync("api/HerrenHausAPI", JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        #endregion

        #region"DeleteHerrenHaus Integration Tests"

        [Fact]
        public async Task DeleteHerrenHaus_returnNoContent()
        {
            //Arrange          
            //Act
            var result = await httpClient.DeleteAsync("api/HerrenHausAPI/2");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteHerrenHaus_returnBadRequest()
        {
            //Arrange          
            //Act
            var result = await httpClient.DeleteAsync("api/HerrenHausAPI/0");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteHerrenHaus_returnNotFound()
        {
            //Arrange          
            //Act
            var result = await httpClient.DeleteAsync("api/HerrenHausAPI/20");
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region "UpdateHerrenHaus Integration Tests"

        [Fact]
        public async Task UpdateHerrenHaus_returnNoContent()
        {
            //Arrange            
            var input = new HerrenHausDto()
            {
                ID = 1,
                Name = "HerrenHaus am Strand",
                Location = "Palestine",
                price = "0$"
            };
            //Act
            var result = await httpClient.PutAsync("api/HerrenHausAPI/1", JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateHerrenHaus_NoInputObject_returnBadRequest()
        {
            //Arrange            
            
            //Act
            var result = await httpClient.PutAsync("api/HerrenHausAPI/1", JsonContent.Create(""));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateHerrenHaus_DifferentIds_returnBadRequest()
        {
            //Arrange            
            var input = new HerrenHausDto()
            {
                ID = 1,
                Name = "HerrenHaus am Strand",
                Location = "Palestine",
                price = "0$"
            };
            //Act
            var result = await httpClient.PutAsync("api/HerrenHausAPI/14", JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateHerrenHaus_returnNotFound()
        {
            //Arrange            
            var input = new HerrenHausDto()
            {
                ID = 15,
                Name = "HerrenHaus am Strand",
                Location = "Palestine",
                price = "0$"
            };
            //Act
            var result = await httpClient.PutAsync("api/HerrenHausAPI/15", JsonContent.Create(input));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion


        #region "UpdatePartialHerrenHaus Integration Tests"

        [Fact]
        public async Task UpdatePartialHerrenHaus_returnNoContent()
        {
            //Arrange
            dynamic dynamic = new System.Dynamic.ExpandoObject();
            dynamic.path = "name";
            dynamic.op = "replace";
            dynamic.value = "Sarah HerrenHaus";
            var listOfDynamic = new List<dynamic>();
            listOfDynamic.Add(dynamic);
            //Act
            var result = await httpClient.PatchAsync("api/HerrenHausAPI/1",JsonContent.Create(listOfDynamic));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdatePartialHerrenHaus_NullInput_returnBadRequest()
        {
            //Arrange
            
            //Act
            var result = await httpClient.PatchAsync("api/HerrenHausAPI/1", JsonContent.Create(""));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePartialHerrenHaus_NoID_returnBadRequest()
        {
            //Arrange
            dynamic dynamic = new System.Dynamic.ExpandoObject();
            dynamic.path = "name";
            dynamic.op = "replace";
            dynamic.value = "Sarah HerrenHaus";
            var listOfDynamic = new List<dynamic>();
            listOfDynamic.Add(dynamic);
            //Act
            var result = await httpClient.PatchAsync("api/HerrenHausAPI/0", JsonContent.Create(listOfDynamic));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdatePartialHerrenHaus_returnNotFound()
        {
            //Arrange
            dynamic dynamic = new System.Dynamic.ExpandoObject();
            dynamic.path = "name";
            dynamic.op = "replace";
            dynamic.value = "Sarah HerrenHaus";
            var listOfDynamic = new List<dynamic>();
            listOfDynamic.Add(dynamic);
            //Act
            var result = await httpClient.PatchAsync("api/HerrenHausAPI/17", JsonContent.Create(listOfDynamic));
            //Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion
    }
}