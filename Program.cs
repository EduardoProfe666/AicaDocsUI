using AicaDocsUI.Models;
using AicaDocsUI.Repositories.Downloads;
using AicaDocsUI.Repositories.Nomenclators;
using AicaDocsUI.Repositories.Utils;
using AicaDocsUI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddSingleton(new RootProvider { RootPage = @"https://aica-docs.onrender.com" });
builder.Services.AddScoped<INomenclatorRepository, NomenclatorRepository>();
builder.Services.AddScoped<IPaginationRepository, PaginationRepository>();
builder.Services.AddScoped<IDownloadRepository, DownloadRepository>();

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/prueba", (INomenclatorRepository nr) => nr.GetNomenclatorAsync(2, 1));

app.Run();