using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



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
    name: "greetings",
    pattern: "greetings/{controller=Hello}/{action=Hi}/{id?}/{id2?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
