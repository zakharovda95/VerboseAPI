using DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;
using DataAccessLayer.Database;
using DataAccessLayer.Repositories.DictionaryRepositories;
using DataAccessLayer.UnitOfWorks;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DB_CS"), 
        new MySqlServerVersion( new Version(9, 0, 0))));

builder.Services.AddAutoMapper(
    typeof(DictionaryAutoMapperProfile), 
    typeof(DictionaryElementAutoMapperProfile));

/** Репозитории **/
builder.Services.AddTransient<IRepository<DictionaryModel, DictionaryModelBase>, DictionaryRepository>();
builder.Services.AddTransient<IRepository<DictionaryElementModel, DictionaryElementModelBase>, DictionaryElementRepository>();

/** UnitOfWorks **/
builder.Services.AddScoped<IDictionaryUnitOfWork, DictionaryUnitOfWork>();

/** Сервисы **/
// builder.Services.AddTransient<IDictionaryService, DictionaryService>();
// builder.Services.AddTransient<IDictionaryElementService, DictionaryElementService>();

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