using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data;
using MyApp.Api.Filters;
using MyApp.Api.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
    options.Filters.Add<ValidationFilter>();
})
.AddNewtonsoftJson()
.AddXmlSerializerFormatters();

builder.Services.AddDbContext<ModelContext>(opt =>
    opt.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
