using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;
using System.Net.Http.Json;

public class TestFactory : WebApplicationFactory<Program>
{
    // naam één keer bepalen, anders krijgt elk request een nieuwe lege database
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // de echte SQL Server instellingen weghalen
            var oud = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ProjectDb>));
            if (oud != null) services.Remove(oud);

            var oud2 = services.SingleOrDefault(
                d => d.ServiceType == typeof(IDbContextOptionsConfiguration<ProjectDb>));
            if (oud2 != null) services.Remove(oud2);

            // tijdelijke database in het geheugen ervoor in de plaats
            services.AddDbContext<ProjectDb>(o => o.UseInMemoryDatabase("TestDb"));
        });
    }
}

public class ProjectTests : IClassFixture<TestFactory>
{
   private readonly TestFactory _factory;
    public ProjectTests(TestFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetProjecten_Publiek_GeeftOk()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync("/projects");

        //assert 
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }   

    [Fact]
    public async Task GetProjectOpId_OnbekendId_GeeftNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/projects/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostProject_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nieuw = new { Title = "Test", Beschrijving = "Test" };

        // Act
        var response = await client.PostAsJsonAsync("/projects", nieuw);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProject_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("/projects/1");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PutProject_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nieuw = new { Title = "Test", Beschrijving = "Test" };

        // Act
        var response = await client.PutAsJsonAsync("/projects/1", nieuw);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBlogs_Publiek_GeeftOk()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync("/blogs");

        //assert 
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }   

    [Fact]
    public async Task GetBlogOpId_OnbekendId_GeeftNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/blogs/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostBlog_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nieuw = new  {Title = "Test", Beschrijving = "Test", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl"};

        // Act
        var response = await client.PostAsJsonAsync("/blogs", nieuw);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteBlog_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("/blogs/1");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PutBlog_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var nieuw = new  {Title = "Test", Beschrijving = "Test", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl"};

        // Act
        var response = await client.PutAsJsonAsync("/blogs/1", nieuw);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}