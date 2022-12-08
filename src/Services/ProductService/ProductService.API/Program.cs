using Microsoft.EntityFrameworkCore;
using ProductService.API.BusinessLayer;
using ProductService.API.DataLayer.Contexts;
using ProductService.API.Extensions;
using ProductService.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnstr"),
        providerOptions => providerOptions.EnableRetryOnFailure());
});

builder.Services.AddScoped<IProductRepos, ProductRepos>();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<ProductDbContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ProductDbContext>>();
}).Run();

app.Run();
