using DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.DictionaryRepositories;
using DataAccessLayer.UnitOfWorks;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;
using DomainLayer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Настройка файла конфигурации

builder.Configuration
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");

#endregion

#region Настройка логирования

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddFile(options => builder.Configuration.GetSection("Logging.File").Bind(options));

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

#endregion

#region Настройка базы данных

var databaseClient = builder.Configuration["DatabaseClient"];
if (string.IsNullOrEmpty(databaseClient))
    throw new NullReferenceException(nameof(databaseClient));

var connectionString = databaseClient switch
{
    "musql" => builder.Configuration.GetConnectionString("DB_CS_MYSQL"),
    "sql-light" => builder.Configuration.GetConnectionString("DB_CS_SQL_LIGHT"),
    _ => string.Empty
};
if (string.IsNullOrEmpty(connectionString))
    throw new NullReferenceException(nameof(connectionString));

if (databaseClient == "mysql")
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 2))));
}
else if (databaseClient == "sql-light")
{
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
}

#endregion

#region Профили маппера

builder.Services.AddAutoMapper(
    typeof(DictionaryAutoMapperProfile),
    typeof(DictionaryElementAutoMapperProfile));

#endregion

#region Регистрация репозиториев

// Репозитории
builder.Services.AddScoped<IRepository<DictionaryModel, DictionaryModelBase>, DictionaryRepository>();
builder.Services
    .AddScoped<IRepository<DictionaryElementModel, DictionaryElementModelBase>, DictionaryElementRepository>();

#endregion

#region Регистрация UnitOfWork

builder.Services.AddScoped<IDictionaryUnitOfWork, DictionaryUnitOfWork>();

#endregion

#region Регистрация сервисов

builder.Services.AddScoped<IDictionaryService, DictionaryService>();
builder.Services.AddScoped<IDictionaryElementService, DictionaryElementService>();

#endregion

#region Прочее

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

// сваггер
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