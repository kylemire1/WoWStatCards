using Microsoft.AspNetCore.Identity;

namespace WowStatCards.Models.Domain
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
