using Microsoft.EntityFrameworkCore;
using ProductsWebAPI.Data;
using ProductsWebAPI.Infastructure.Interfaces;
using ProductsWebAPI.Infastructure.Services;
using ProductsWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductsContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});
var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin());   
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

});*/


app.Run();
