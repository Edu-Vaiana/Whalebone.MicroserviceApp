using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Whalebone.MicroserviceApp.IntegrationTests.Exceptions;
using Whalebone.MicroserviceApp.Models;
using Xunit;

namespace Whalebone.MicroserviceApp.IntegrationTests;

public class PersonEndpointsOnDockerTests
{
    private readonly string _baseUrl = "http://localhost:8080"; // Docker-exposed port

    private static Person CreateTestPerson()
    {
        return new Person
        {
            ExternalId = Guid.NewGuid(),
            Name = "Test User",
            Email = "test@example.com",
            DateOfBirth = new DateTime(2000, 1, 1)
        };
    }

    [Fact]
    public async Task PostPerson_Works()
    {
        try
        {
            using var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var person = CreateTestPerson();

            var postResponse = await client.PostAsJsonAsync("/person/save", person);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            throw new PersonEndpointException("Please make sure the Docker containers are running", ex);
        }
    }

    [Fact]
    public async Task GetPerson_Works()
    {
        try
        {
            using var client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
            var person = CreateTestPerson();

            // Ensure the person exists before GET
            var postResponse = await client.PostAsJsonAsync("/person/save", person);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var getResponse = await client.GetAsync($"/person/{person.ExternalId}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var returned = await getResponse.Content.ReadFromJsonAsync<Person>();
            Assert.NotNull(returned);
            Assert.Equal(person.ExternalId, returned.ExternalId);
            Assert.Equal(person.Name, returned.Name);
            Assert.Equal(person.Email, returned.Email);
            Assert.Equal(person.DateOfBirth, returned.DateOfBirth);
        }
        catch (HttpRequestException ex)
        {
            throw new PersonEndpointException("Please make sure the Docker containers are running", ex);
        }
    }
}
