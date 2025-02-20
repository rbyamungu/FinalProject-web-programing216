using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HalfAndHalf.Data;
using HalfAndHalf.Models;
using HalfAndHalf.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// add DbContext for your main application
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UserConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IStorageService, MinioService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default", 
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();