using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Nomenclators;
using AicaDocsUI.Utils;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddNotyf(config=> { config.DurationInSeconds = 5;config.IsDismissable = true;config.Position = NotyfPosition.BottomRight; });
builder.Services.AddHttpClient();
builder.Services.AddSingleton(new RootProvider { RootPage = @"https://aica-docs.onrender.com" });
builder.Services.AddScoped<INomenclatorRepository, NomenclatorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseNotyf();

app.MapGet("/prueba", (INomenclatorRepository nr) => nr.GetNomenclatorAsync(2, 1));

app.Run();