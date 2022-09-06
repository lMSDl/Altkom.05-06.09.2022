using Models;
using Services.Bogus.Fakers;
using Services.Bogus;
using Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(x =>
{
    x.Conventions.AuthorizeFolder("/Users").AllowAnonymousToPage("/Users/Index");
});

builder.Services.AddTransient<EntityFaker<User>, UserFaker>();
builder.Services.AddSingleton<ICrudService<User>, CrudService<User>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login";
        x.LogoutPath = "/Login/Logout";
        x.AccessDeniedPath = "/";
        x.ExpireTimeSpan = TimeSpan.FromSeconds(30);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
