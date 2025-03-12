using HotelsApi.Context;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Database Configuration
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
string connectionString = "server=localhost; port=3306; database=HotelsApidb; user=root; password=atrjos; SslMode=Required;Allow User Variables=true;";


builder.Services.AddDbContext<DatabaseContext>(
    dbContextoptions => dbContextoptions.UseMySql(connectionString, serverVersion).EnableDetailedErrors()
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
