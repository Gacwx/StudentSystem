using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data;
using MyApp.Api.Mapping;

var builder = WebApplication.CreateBuilder(args);

//Services registration

// Подключение DbContext к Oracle
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

//Контроллеры (API)
builder.Services.AddControllers();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:7091") // sənin MVC UI portun
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

//Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowUI");


app.MapControllers();



app.Run();
