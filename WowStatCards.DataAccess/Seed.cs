using Microsoft.AspNetCore.Identity;
using WowStatCards.Models.Domain;

namespace WowStatCards.DataAccess
{
    public class Seed
    {
        public static async Task SeedData(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<User>
                {
                    new User{UserName="bob", Email="bob@test.com", DisplayName="Bob Test"},
                    new User{UserName="tom", Email="tom@test.com", DisplayName="Tom Test"},
                    new User{UserName="mary", Email="mary@test.com", DisplayName="Mary Test"},
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd!");
                }
            }
        }
    }
}
