using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

builder.Services.AddScoped<IMovieStore, EfMovieStore>();

var connectionString = builder.Configuration.GetConnectionString("MovieDatabase");
builder.Services.AddDbContext<MovieDb>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapMovieEndpoints();
app.Run();