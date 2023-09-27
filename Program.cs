using AutoMapper;
using FoodOrderingApi;
using FoodOrderingApi.Data;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Helper;
using FoodOrderingApi.Interfaces;
using FoodOrderingApi.Model;
using FoodOrderingApi.Repository;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Set up the Stripe API key from configuration.
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Add controllers with JSON options to handle reference loops.
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Transiently register the Seed class.
builder.Services.AddTransient<Seed>();

// Add AutoMapper and configure it to scan the current assembly.
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add endpoints API explorer for Swagger/OpenAPI documentation.
builder.Services.AddEndpointsApiExplorer();

// Add Swagger documentation generation.
builder.Services.AddSwaggerGen();

// Configure Cloudinary settings.
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Set up the database context with SQL Server as the database provider.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register scoped repositories and services using dependency injection.
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();


// Retrieve the frontend URL from configuration and configure CORS policies.
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddCors(options =>
{
    var frontendURL = configuration.GetValue<string>("frontend_url");
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service?.SeedDataContext();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
