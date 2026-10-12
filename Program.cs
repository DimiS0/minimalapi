using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

builder.Services.AddScoped<IProjectStore, EfProjectStore>();

var connectionString = builder.Configuration.GetConnectionString("ProjectsDatabase");
builder.Services.AddDbContext<ProjectDb>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapProjectEndpoints();

app.Run();