using Microsoft.AspNetCore.Identity;
using WowStatCards.DataAccess;
using WowStatCards.Models.Domain;

class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try
                {

                    await Seed.SeedData(userManager);

                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "Error occured while seeding data");
                }
            }
        }
        startup.Configure(app, builder.Environment);
    }
}