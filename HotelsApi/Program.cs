using HotelsApi.Context;
using HotelsApi.Repositories;
using HotelsApi.Services;
using Microsoft.EntityFrameworkCore;

// Scaffold Script:

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service binding
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IBarangayService, BarangayService>(); 
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Repositories binding
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBarangayRepository, BarangayRepository>();  
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Database Configuration
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
string connectionString = "server=localhost; port=3306; database=HotelsApidb; user=root; password=atrjos; SslMode=Required;Allow User Variables=true;";

builder.Services.AddDbContext<DatabaseContext>(
    dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion).EnableDetailedErrors()
);

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
