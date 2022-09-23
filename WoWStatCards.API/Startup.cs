using Microsoft.EntityFrameworkCore;
using WowStatCards.Clients;
using WowStatCards.DataAccess;
using WowStatCards.DataAccess.Repository;
using WowStatCards.DataAccess.Repository.IRepository;
using WoWStatCards.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string _myAllowSpecificOrigins = "CorsPolicy";

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;

    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
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
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
            });
        });
        services.AddSwaggerDocument();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {

        app.UseRouting();
        app.UseCors(_myAllowSpecificOrigins);
        app.UseEndpoints(x => x.MapControllers());
        // Configure the HTTP request pipeline.
        app.UseOpenApi();
        app.UseSwaggerUi3();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}