using Issues.Extension;
using Issues.Infrastructure.Mapper;
using Issues.Models;
using Issues.Services;
using Issues.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "server=localhost;database=issuestrackingdb;uid=root;pwd=";
var serverVersion = new MySqlServerVersion(new Version(10, 4, 28));

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IssueTrackingContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
})
    .AddEntityFrameworkStores<IssueTrackingContext>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseAuthentication();
app.UseAuthorization();
app.ConfigureDefaultAdminUser();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName : "Admin",
        pattern : "Admin/{controller=Bug}/{action=Index}/{id?}"
        );
    endpoints.MapControllerRoute("default", "{controller=Bug}/{action=Index}/{id?}");
}); 

app.Run();
