using Microsoft.OpenApi.Models;
using Tennis.API.Filters;
using Tennis.API.Middlewares.MiddlewaresService.Interfaces;
using Tennis.API.Middlewares.MiddlewaresService;
using Tennis.API.Services.Encryption;
using Tennis.Configuration;
using Tennis.Services.Interfaces;
using Tennis.Services;
using Tennis.API.Services.Interfaces;
using Tennis.API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTennisDbConfiguration();
builder.Services.AddEcnryptionOptions();
builder.Services.AddAuthenticationOptions();
builder.Services.ConfigureJwt();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJugadorService, JugadorService>();
builder.Services.AddScoped<ITorneoService, TorneoService>();
builder.Services.AddScoped<CustomFilter>();
builder.Services.AddScoped<IExceptionService, ExceptionService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tennis API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tennis API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
