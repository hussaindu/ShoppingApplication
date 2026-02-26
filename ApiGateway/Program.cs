using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        RoleClaimType = "role",
        NameClaimType = "email"
    };

    options.MapInboundClaims = false;
});

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Gateway",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: JWT token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



/* monitoring */

builder.Services
    .AddOcelot()
    .AddPolly();    

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Welcome to API Gateway ");
});

app.UseAuthentication();
app.UseAuthorization();

/* monitoring */

app.Use(async (context, next) =>
{
    var start = DateTime.UtcNow;

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"? ERROR: {ex.Message}");
        throw;
    }
    finally
    {
        var elapsed = DateTime.UtcNow - start;
        var status = context.Response.StatusCode;
        var path = context.Request.Path;

        Console.WriteLine(
            $"?? {DateTime.Now:HH:mm:ss} | {path} | {status} | {elapsed.TotalMilliseconds} ms"
        );
    }
});


await app.UseOcelot();

app.Run();
