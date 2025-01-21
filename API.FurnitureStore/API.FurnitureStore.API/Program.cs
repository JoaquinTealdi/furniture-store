using API.FurnitureStore.Data;
using API.FurnitureStore.Services;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Services
builder.Services.AddTransient<IClientsService, ClientsService>();   
builder.Services.AddTransient<IProductCategoriesService, ProductCategoriesService>();   
builder.Services.AddTransient<IProductsService, ProductsService>();   
builder.Services.AddTransient<IOrdersService, OrdersService>();   

builder.Services.AddDbContext<FugnitureStoreDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("FurnitureConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
