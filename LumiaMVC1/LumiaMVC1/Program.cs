using Business.Services.Abstracts;
using Business.Services.Concrates;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Data.RepositoryConcrates;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt=>
opt.UseSqlServer("Server=CA-R213-PC08\\SQLEXPRESS;Database=LumiaNaz;Trusted_Connection=true;Integrated Security=true;TrustServerCertificate=true;"));
builder.Services.AddIdentity<AppUser,IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric=true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireUppercase=true;
    opt.Password.RequireLowercase=true;
    opt.Password.RequireDigit=true;
    opt.User.RequireUniqueEmail=false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
