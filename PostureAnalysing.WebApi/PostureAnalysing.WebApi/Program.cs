using PostureAnalysing.WebApi.Interfaces;
using PostureAnalysing.WebApi.Models;
using PostureAnalysing.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var sensorRangeSection = builder.Configuration.GetSection("Sensor");
builder.Services.Configure<AppSettings>(sensorRangeSection);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISensorsBL, SensorsService>();

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
