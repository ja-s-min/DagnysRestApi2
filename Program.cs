using Microsoft.EntityFrameworkCore;
using dagnys_api;
using dagnys_api.Data;
using dagnys_api.Interfaces;
using Microsoft.AspNetCore;
using dagnys_api.Entities;
using dagnys_api.Repositories;

var builder = WebApplication.CreateBuilder(args);

//var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Prod"));
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
var context = services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();
await Seed.LoadProducts(context);
await Seed.LoadSupplier(context);
await Seed.LoadRecipe(context);
await Seed.LoadPurchase(context);
await Seed.LoadRawMaterial(context);
await Seed.LoadSupplierRawMaterial(context);
await Seed.LoadAddressTypes(context);

}
catch (Exception ex)
{Console.WriteLine("{0}", ex.Message);
throw;
}

app.MapControllers();

app.Run();
