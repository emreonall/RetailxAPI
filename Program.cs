using Microsoft.EntityFrameworkCore;
using RetailxAPI.Data;
using RetailxAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ShopRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<QFormRepository>();
builder.Services.AddScoped<UserQformRepository>();
builder.Services.AddScoped<CategoryQuestionsRepository>();



//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowBlazorClient", policy =>
//    {
//        policy.WithOrigins("https://localhost:7171") // Blazor Client adresi
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");


app.Run();
