using DataAccessLayer.Database;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories.DictionaryRepositories;
using DomainLayer.Interfaces.Repositories.Dictionary;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DB_CS"), 
        new MySqlServerVersion( new Version(9, 0, 0))));

/** Репозитории **/
builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddScoped<IDictionaryElementRepository, DictionaryElementRepository>();

/** Мапперы **/
builder.Services.AddScoped<DictionaryMapper>();
builder.Services.AddScoped<DictionaryMapper>();

/** Сервисы **/


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();