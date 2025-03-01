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

// Add scoped repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

// Add scoped services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

// Add session and SignalR
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

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

app.UseCors("AllowOrigin");

app.UseAuthorization();

// Map Razor Pages and SignalR hubs
app.MapRazorPages();
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<CartHub>("/cartHub");

app.Run();
