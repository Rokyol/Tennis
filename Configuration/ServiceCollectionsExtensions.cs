using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tennis.API.Configuration;
using Tennis.Repository;

namespace Tennis.Configuration
{
    public static class  ServiceCollectionsExtensions
    {
        public static void AddTennisDbConfiguration(this IServiceCollection services)
        {
            IConfiguration _configuration;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _configuration = serviceScope.ServiceProvider.GetService<IConfiguration>()!;
            }

            var applicationOptions = new ApplicationOptions();
            _configuration.GetSection(ApplicationOptions.Section).Bind(applicationOptions);

            services.AddDbContext<TennisContext>(options => options.UseSqlServer(applicationOptions.ConnectionString));
        }
        public static void AddEcnryptionOptions(this IServiceCollection services)
        {
            IConfiguration _configuration;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _configuration = serviceScope.ServiceProvider.GetService<IConfiguration>()!;
            }

            var encryptionOptions = new EncryptionOptions();
            _configuration.GetSection(EncryptionOptions.Section).Bind(encryptionOptions);
            services.AddSingleton(typeof(EncryptionOptions), encryptionOptions);
        }

        public static void AddAuthenticationOptions(this IServiceCollection services)
        {
            IConfiguration _configuration;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _configuration = serviceScope.ServiceProvider.GetService<IConfiguration>()!;
            }

            var authenticationOptions = new AuthenticationOptions();
            _configuration.GetSection(AuthenticationOptions.Section).Bind(authenticationOptions);
            services.AddSingleton(typeof(AuthenticationOptions), authenticationOptions);
        }

        public static void ConfigureJwt(this IServiceCollection services)
        {
            AuthenticationOptions _authenticationOptions;

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                _authenticationOptions = serviceScope.ServiceProvider.GetService<AuthenticationOptions>()!;
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _authenticationOptions.Issuer,
                    ValidAudience = _authenticationOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }

    }
}
