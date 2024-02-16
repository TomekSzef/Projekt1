using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AutoWebApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AutoWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutoWebAppContext") ?? throw new InvalidOperationException("Connection string 'AutoWebAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

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
app.UseCookiePolicy();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
