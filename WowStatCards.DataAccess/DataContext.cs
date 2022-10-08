using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WowStatCards.Models.Domain;

namespace WowStatCards.DataAccess
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StatCard> StatCards { get; set; }
        public DbSet<Faction> Factions { get; set; }
    }
}