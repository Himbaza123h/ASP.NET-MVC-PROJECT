using ApiTranslateASP.NETWEB.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Configure the MongoDB client and add it to services for DI
var connectionString = configuration.GetConnectionString("MongoDb");
var mongoClient = new MongoClient(connectionString);
var database = mongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
builder.Services.AddSingleton(database);

var app = builder.Build();

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
    name: "translate",
    pattern: "translate/{controller=Translate}/{action=translate}"
);
app.MapControllerRoute(
    name: "recorded",
    pattern: "recorded/{controller=SavedTranslations}/{action=SavedTranslations}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
