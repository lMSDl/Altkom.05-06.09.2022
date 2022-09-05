using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Models;
using Services.Bogus;
using Services.Bogus.Fakers;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization()
    .AddDataAnnotationsLocalization(x => x.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(Program)));
    
    
    ;
builder.Services.AddTransient<EntityFaker<User>, UserFaker>();
builder.Services.AddSingleton<ICrudService<User>, CrudService<User>>();

builder.Services.AddLocalization(x => x.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(x =>
{
    x.SetDefaultCulture("en-US");
    x.AddSupportedCultures("en-US", "pl-PL");
    x.AddSupportedUICultures("en-US", "pl-PL");

    //x.RequestCultureProviders.RemoveAt(0);
});


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

app.UseRequestLocalization();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "greetings",
    pattern: "greetings/Hello/{action=Hi}/{id?}/{id2?}");

app.Run();
