using DataAccessLayer.Database;
using DataAccessLayer.Interfaces.Mappers;
using DataAccessLayer.Mappers;
using DataAccessLayer.Repositories.DictionaryRepositories;
using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Interfaces.Services;
using DomainLayer.Services;
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
builder.Services.AddTransient<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddTransient<IDictionaryElementRepository, DictionaryElementRepository>();

/** Мапперы **/
builder.Services.AddTransient<IDictionaryMapper, DictionaryMapper>();
builder.Services.AddTransient<IDictionaryElementMapper, DictionaryElementMapper>();

/** Сервисы **/
builder.Services.AddTransient<IDictionaryService, DictionaryService>();
builder.Services.AddTransient<IDictionaryElementService, DictionaryElementService>();

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