var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction()) 
    app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();