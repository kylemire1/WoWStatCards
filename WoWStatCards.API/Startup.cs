﻿using Microsoft.EntityFrameworkCore;
using WowStatCards.Clients;
using WowStatCards.DataAccess;
using WowStatCards.DataAccess.Repository;
using WowStatCards.DataAccess.Repository.IRepository;
using WoWStatCards.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
        services.AddCors(options =>
        {
            options.AddPolicy(name: _myAllowSpecificOrigins, policy =>
            {
                policy.WithOrigins("http://localhost:3000");
            });
        });
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {

        app.UseRouting();
        app.UseCors(_myAllowSpecificOrigins);
        app.UseEndpoints(x => x.MapControllers());
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}