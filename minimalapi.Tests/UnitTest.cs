using Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net;
using System.Net.Http.Json;

// Start de API, maar met een tijdelijke database in het geheugen losgekoppeld van SQL
public class TestFactory : WebApplicationFactory<Program>
{
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

            // tijdelijke database in het geheugen
            services.AddDbContext<ProjectDb>(o => o.UseInMemoryDatabase("TestDb"));
        });
    }
}

public class ApiTests : IClassFixture<TestFactory>
{
    private readonly TestFactory _factory;

    public ApiTests(TestFactory factory)
    {
        _factory = factory;
    }

    // Maakt een gebruiker met een rol, logt in en geeft de ingelogde client terug
    private async Task<HttpClient> MaakClientMetRol(string rol)
    {
        var email = $"{Guid.NewGuid()}@test.nl";
        var wachtwoord = "Test1234!";

        using (var scope = _factory.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(rol))
                await roleManager.CreateAsync(new IdentityRole(rol));

            var user = new User { UserName = email, Email = email };
            await userManager.CreateAsync(user, wachtwoord);
            await userManager.AddToRoleAsync(user, rol);
        }

        var client = _factory.CreateClient();
        var login = await client.PostAsJsonAsync("/auth/login", new { Email = email, Password = wachtwoord });
        Assert.Equal(HttpStatusCode.OK, login.StatusCode);
        return client;
    }

    // Projecten

    [Fact]
    public async Task GetProjecten_Publiek_GeeftOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/projects");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProjectOpId_BestaandId_GeeftJuisteTitel()
    {
        // Arrange
        var admin = await MaakClientMetRol("Admin");
        var postResponse = await admin.PostAsJsonAsync("/projects", new { Title = "Mijn project", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });
        var pad = postResponse.Headers.Location!.ToString();
        var client = _factory.CreateClient();

        // Act
        var inhoud = await client.GetStringAsync(pad);

        // Assert
        Assert.Contains("Mijn project", inhoud);
    }

    [Fact]
    public async Task PostProject_AlsAdmin_MaaktProjectAan()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");

        // Act
        var response = await client.PostAsJsonAsync("/projects", new { Title = "Mijn project", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });
        var inhoud = await client.GetStringAsync(response.Headers.Location!.ToString());

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains("Mijn project", inhoud);
    }

    [Fact]
    public async Task PutProject_AlsAdmin_WijzigtProject()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");
        var postResponse = await client.PostAsJsonAsync("/projects", new { Title = "Oud", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });
        var pad = postResponse.Headers.Location!.ToString();

        // Act
        var response = await client.PutAsJsonAsync(pad, new { Title = "Nieuw", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });
        var inhoud = await client.GetStringAsync(pad);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Contains("Nieuw", inhoud);
    }

    [Fact]
    public async Task DeleteProject_AlsAdmin_VerwijdertProject()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");
        var postResponse = await client.PostAsJsonAsync("/projects", new { Title = "Mijn project", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });
        var pad = postResponse.Headers.Location!.ToString();

        // Act
        var deleteResponse = await client.DeleteAsync(pad);
        var getResponse = await client.GetAsync(pad);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    // Blogs

    [Fact]
    public async Task GetBlogs_Publiek_GeeftOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/blogs");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetBlogOpId_BestaandId_GeeftJuisteTitel()
    {
        // Arrange
        var admin = await MaakClientMetRol("Admin");
        var postResponse = await admin.PostAsJsonAsync("/blogs", new { Title = "Mijn blog", Beschrijving = "Omschrijving", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl" });
        var pad = postResponse.Headers.Location!.ToString();
        var client = _factory.CreateClient();

        // Act
        var inhoud = await client.GetStringAsync(pad);

        // Assert
        Assert.Contains("Mijn blog", inhoud);
    }

    [Fact]
    public async Task PostBlog_AlsAdmin_MaaktBlogAan()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");

        // Act
        var response = await client.PostAsJsonAsync("/blogs", new { Title = "Mijn blog", Beschrijving = "Omschrijving", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl" });
        var inhoud = await client.GetStringAsync(response.Headers.Location!.ToString());

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains("Mijn blog", inhoud);
    }

    [Fact]
    public async Task PutBlog_AlsAdmin_WijzigtBlog()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");
        var postResponse = await client.PostAsJsonAsync("/blogs", new { Title = "Hotel simulator", Beschrijving = "Omschrijving", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl" });
        var pad = postResponse.Headers.Location!.ToString();

        // Act
        var response = await client.PutAsJsonAsync(pad, new { Title = "geleerd van gameflow", Beschrijving = "Omschrijving", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl" });
        var inhoud = await client.GetStringAsync(pad);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Contains("geleerd van gameflow", inhoud);
    }

    [Fact]
    public async Task DeleteBlog_AlsAdmin_VerwijdertBlog()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");
        var postResponse = await client.PostAsJsonAsync("/blogs", new { Title = "Mijn blog", Beschrijving = "Omschrijving", Datum = "2026-11-15", ContentLink = "https://voorbeeld.nl" });
        var pad = postResponse.Headers.Location!.ToString();

        // Act
        var deleteResponse = await client.DeleteAsync(pad);
        var getResponse = await client.GetAsync(pad);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    // Ongeldige invoer

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
    public async Task PostProject_LegeTitel_GeeftBadRequest()
    {
        // Arrange
        var client = await MaakClientMetRol("Admin");

        // Act
        var response = await client.PostAsJsonAsync("/projects", new { Title = "", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostProject_ZonderLogin_GeeftUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/projects", new { Title = "Mijn project", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PostProject_AlsGast_GeeftForbidden()
    {
        // Arrange
        var client = await MaakClientMetRol("Gast");

        // Act
        var response = await client.PostAsJsonAsync("/projects", new { Title = "Mijn project", Beschrijving = "Omschrijving", Afbeedling = "https://example.test/image.png", Alt = "Alt", ProjectLink = "https://example.test/project", Tag = "Tag", Datum = "2026-11-15" });

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    // Registratie en login

    [Fact]
    public async Task Register_GeldigeGegevens_GeeftOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new { Email = "DimiStoyanov@test.nl", Password = "Test1234!" };

        // Act
        var response = await client.PostAsJsonAsync("/auth/register", body);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_GeldigeGegevens_GeeftOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var body = new { Email = "TimCheese@test.nl", Password = "Test1234!" };
        await client.PostAsJsonAsync("/auth/register", body);

        // Act
        var response = await client.PostAsJsonAsync("/auth/login", body);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}