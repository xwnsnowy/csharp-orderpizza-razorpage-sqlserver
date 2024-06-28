using Microsoft.EntityFrameworkCore;
using OrderLibrary.Interfaces;
using OrderLibrary.Repositories;
using OrderLibrary.Services;
using OrderFoodWeb.Hubs;
using OrderLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OrderFoodDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add scoped repositories and services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
// Add other repositories and services as needed

// Add session and SignalR
builder.Services.AddSession();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Use session
app.UseSession();

app.UseAuthorization();

// Map Razor Pages and SignalR hubs
app.MapRazorPages();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
