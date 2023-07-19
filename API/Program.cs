using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Configure the HTTP request pipeline.

builder.Services.AddControllers();
//Extension/ApplicationServices
builder.Services.AddApplicationServices(builder.Configuration);
//Extension/JWTServices
builder.Services.AddJWTAuthenticationServices(builder.Configuration);
//Extension/AuthButton
builder.Services.AddAuthButtonSwaggerServices(builder.Configuration);

//3rd step add cors
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyHeader().WithOrigins("https://localhost:4200"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        //(!1step for authorize button 2nd step be on Extension/AuthButton)
        c.SwaggerEndpoint("/swagger/v1/swagger.json","My API");
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
