using Microsoft.EntityFrameworkCore;
using Mps.Application.Extensions;
using Mps.Infrastructure.Extensions;
using Mps.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();
builder.Services.AddDataAccess(x => x
    .UseLazyLoadingProxies()
    .UseNpgsql("Host=localhost;Port=5433;Database=lab6;Username=postgres;Password=123"));

builder.Services.AddControllers();

builder.Services.AddCookiesAuthentication().AddRoles();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();