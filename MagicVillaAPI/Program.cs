using MagicVillaAPI.Controllers;
using MagicVillaAPI.Data;
using MagicVillaAPI.Mapper;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddSingleton<IMapper, Mapper>();
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo
    .File("log/villalogs.txt", rollingInterval: RollingInterval.Hour).CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddDbContext<DataContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefautlSQLConnection")));
builder.Services.AddAutoMapper(typeof(MapperConfig));

// builder.Services.AddSingleton<ILogging, Logging>();
// builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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