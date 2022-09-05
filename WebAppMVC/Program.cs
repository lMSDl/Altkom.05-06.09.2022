using Microsoft.Extensions.FileProviders;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();
builder.Services.AddSingleton<ICrudService<User>, CrudService<User>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    //using Microsoft.Extensions.FileProviders;
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Downloads")),
    RequestPath = "/pliki",

    OnPrepareResponse = x => x.Context.Response.Headers.Append("Cache-Control", "public, max-age=60000")
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Downloads")),
    RequestPath = "/pliki"

});

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "greetings",
    pattern: "greetings/Hello/{action=Hi}/{id?}/{id2?}");

app.Run();
