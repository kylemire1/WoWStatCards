using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Security.Claims;
using WowStatCards.Clients;
using WowStatCards.DataAccess;
using WowStatCards.DataAccess.Repository;
using WowStatCards.DataAccess.Repository.IRepository;
using WoWStatCards.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _tokenSecret;
    private readonly string _myAllowSpecificOrigins = "CorsPolicy";


    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
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

        services.AddAutoMapper(typeof(MappingConfig));
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000").AllowCredentials();
            });
        });
        services.AddSwaggerDocument();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = _configuration["AppSettings:AuthAuthority"];
            options.Audience = _configuration["AppSettings:AuthAudience"];
            options.RequireHttpsMetadata = _webHostEnvironment.IsDevelopment();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
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