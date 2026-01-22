using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NkhanhAPI.Data;
using NkhanhAPI.Mappings;
using NkhanhAPI.Repositories;
using NkhanhAPI.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NkhanhDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("NkhanhAPIConnection")));
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();
