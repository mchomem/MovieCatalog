using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Repositories;
using MovieCatalog.WebApp.Repositories.Interfaces;
using MovieCatalog.WebApp.Services;
using MovieCatalog.WebApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieCatalogContext>(options =>
    options
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging()
        .UseSqlServer(builder.Configuration.GetConnectionString("MovieCatalogContext") ?? throw new InvalidOperationException("Connection string 'MovieCatalogContext' not found.")));

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.InitializeDataBaseWithMokaData(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRequestLocalization("pt-BR");

app.Run();
