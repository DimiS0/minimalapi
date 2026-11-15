using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();



builder.Services.AddScoped<IProjectStore, EfProjectStore>();
builder.Services.AddScoped<IBlogStore, EfBlogStore>();
builder.Services.AddScoped<IUserStore, EfUserStore>();

var connectionString = builder.Configuration.GetConnectionString("ProjectsDatabase");
builder.Services.AddDbContext<ProjectDb>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ProjectDb>().AddDefaultTokenProviders();
builder.Services.AddAuthorizationBuilder().AddPolicy("Admin", policy => policy.RequireRole("Admin"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapProjectEndpoints();
app.MapBlogEndpoints();
app.MapUserEndpoints();

 await DbSeeder.Seeder(app.Services);

app.Run();