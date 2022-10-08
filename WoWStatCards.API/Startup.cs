using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;
using WowStatCards.Clients;
using WowStatCards.DataAccess;
using WowStatCards.DataAccess.Repository;
using WowStatCards.DataAccess.Repository.IRepository;
using WowStatCards.Models.Domain;
using WoWStatCards.API;
using WoWStatCards.API.Services;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string _tokenSecret;
    private readonly string _myAllowSpecificOrigins = "CorsPolicy";

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenSecret = configuration["AppSettings:TokenSecret"];
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSwaggerDocument(c =>
        {
            c.DocumentName = "OpenAPI 2";
            c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            c.AddSecurity("JWT Token", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy this into the value field: Bearer {token}"
                }
            );
        });

        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddSingleton(_configuration);
        services.AddSingleton<BlizzAuthClient>();
        services.AddSingleton<CharacterStatClient>();
        services.AddScoped<IStatCardRepository, StatCardRepository>();
        services.AddScoped<TokenService>();

        services.AddAutoMapper(typeof(MappingConfig));
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });
        });
        services.AddSwaggerDocument();
        services.AddIdentityCore<User>().AddEntityFrameworkStores<DataContext>().AddSignInManager<SignInManager<User>>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSecret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
    public async void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseCors(_myAllowSpecificOrigins);

        // Configure the HTTP request pipeline.

        app.UseOpenApi();
        app.UseSwaggerUi3();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(x => x.MapControllers());

        app.Run();
    }
}