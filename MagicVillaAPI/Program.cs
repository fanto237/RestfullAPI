using MagicVillaAPI.Data;
using MagicVillaAPI.Logging;
using MagicVillaAPI.Mapper;
using MagicVillaAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// configure serilog and register it to the DI Container
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo
    .File("log/villalogs.txt", rollingInterval: RollingInterval.Hour).CreateLogger();


builder.Host.UseSerilog();

// DB Context
builder.Services.AddDbContext<DataContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

// add automapper to the DI Container
builder.Services.AddAutoMapper(typeof(MapperConfig));

// Register repository
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers().AddNewtonsoftJson();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();